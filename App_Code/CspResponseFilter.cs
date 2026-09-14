using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Response.Filter that makes the rendered page compatible with a strict, nonced
/// script-src-elem policy:
///
/// 1. Rewrites LinkButton's href="javascript:__doPostBack('t','a')" (and the
///    cross-page-postback href="javascript:WebForm_DoPostBackWithOptions(new
///    WebForm_PostBackOptions(...))" shape) into href="#" plus data-pb-*/data-pbo-*
///    attributes. A javascript: URI can never be nonce'd or hashed under CSP, so this
///    is the only way to keep those links working once script-src-elem stops carrying
///    'unsafe-inline'; JS/CSP/Home.js turns the data-* attributes back into a real
///    __doPostBack/WebForm_DoPostBackWithOptions call via a delegated click listener.
/// 2. Renames inline event-handler content attributes (onclick, onmouseover,
///    onmouseout, onkeyup, onkeydown, onblur, onkeypress, onchange, onload, onunload)
///    to a data-* prefixed form (onclick -> data-onclick, etc.), leaving the attribute
///    VALUE completely untouched. This governs script-src-attr. A prior approach left
///    the live onXXX="..." attribute in the markup and rewired it client-side (in
///    JS/CSP/Home.js) after the fact - that kept functionality working (a property
///    assignment provides the real handler) but did NOT stop the browser from logging a
///    CSP violation for the original attribute, because the browser evaluates
///    script-src-attr as soon as it parses the attribute (onload/onunload on
///    &lt;body&gt; are compiled into window event handlers immediately as that tag's
///    attributes are parsed - before any later &lt;script&gt; on the page, including our
///    own rewiring code, has run), not deferred until our JS has a chance to remove it.
///    Renaming the attribute server-side means the browser never sees a live onXXX
///    attribute at all for full-page responses, so no violation is ever generated;
///    JS/CSP/Home.js reads the data-* attribute (falling back to a live onXXX attribute
///    for markup this filter never touched - see point 4 below) to wire up the real
///    handler.
/// 3. Stamps a fresh per-request nonce onto every &lt;script&gt; tag (stripping any
///    stale hardcoded nonce first, so a pre-existing nonce="..." in markup can't shadow
///    the real one and permanently fail CSP validation for that tag).
/// 4. None of the above runs for UpdatePanel async-postback responses (see Global.asax's
///    AttachCspFilter, which skips attaching this filter entirely when the
///    X-MicrosoftAjax request header is present) - that wire format is length-prefixed
///    per segment, and changing a segment's content length without recalculating its
///    prefix would desync the client-side parser. Content delivered that way still
///    carries live onXXX="..." attributes verbatim; JS/CSP/Home.js's fallback lookup
///    (data-onXXX first, else onXXX) is what picks those up once the browser - not this
///    filter - has parsed them into the DOM.
///
/// Quote characters inside the href attribute get HTML-entity-encoded by ASP.NET's
/// HtmlTextWriter since they sit inside a double-quoted attribute value - confirmed
/// against real rendered markup that ' becomes &#39;. The regexes below accept the raw
/// character or any of the entity forms a given control/rendering path might produce
/// (decimal, hex, or named), and tolerate stray whitespace inside the parens, since one
/// LinkButton instance (lnkClose) did not match a narrower first attempt at this pattern.
///
/// The event-handler-attribute rename only ever touches the attribute NAME (matched by
/// requiring a preceding whitespace character and a following "=" plus quote, so it
/// can't fire mid-word) and never the quoted value that follows it, so there is no risk
/// of corrupting encoded content (e.g. &amp;quot;) inside that value - it is copied
/// through completely unexamined.
///
/// Buffers output and transforms it on Flush/Close rather than per Write() call, since
/// these patterns can be split across successive Write() calls but ASP.NET Web Forms
/// flushes the fully-rendered page in one shot by default (Response.Buffer = true).
/// </summary>
public class CspResponseFilter : Stream
{
    private const string Apos = "(?:'|&#39;|&#x27;|&apos;)";
    private const string Quot = "(?:\"|&quot;|&#34;|&#x22;)";

    // href="javascript:__doPostBack('target','argument')"
    private static readonly Regex DoPostBackRegex = new Regex(
        "href=\"javascript:__doPostBack\\(\\s*" +
        Apos + "([^'\"&]*)" + Apos + "\\s*,\\s*" +
        Apos + "([^'\"&]*)" + Apos + "\\s*" +
        "\\)\"",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    // href="javascript:WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("t","a",bool,"g","url",bool,bool))"
    private static readonly Regex DoPostBackWithOptionsRegex = new Regex(
        "href=\"javascript:WebForm_DoPostBackWithOptions\\(new WebForm_PostBackOptions\\(\\s*" +
        Quot + "([^'\"&]*)" + Quot + "\\s*,\\s*" +
        Quot + "([^'\"&]*)" + Quot + "\\s*,\\s*" +
        "(true|false)\\s*,\\s*" +
        Quot + "([^'\"&]*)" + Quot + "\\s*,\\s*" +
        Quot + "([^'\"&]*)" + Quot + "\\s*,\\s*" +
        "(true|false)\\s*,\\s*" +
        "(true|false)\\s*" +
        "\\)\\)\"",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex ExistingScriptNonceRegex = new Regex(
        "(<script\\b[^>]*?)\\s+nonce=\"[^\"]*\"",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex ScriptTagRegex = new Regex(
        "<script",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    // Inline event-handler content attributes governed by script-src-attr. Matched by
    // requiring a preceding whitespace character (so "buttononclick" can't match mid-word)
    // and a following "=" plus an opening quote (single or double) - never touching
    // anything inside the quoted value itself, so the rename can't corrupt an encoded
    // entity (e.g. &quot;) or any other content the value happens to carry.
    private static readonly string[] EventHandlerAttrs = {
        "onclick", "onmouseover", "onmouseout", "onkeyup", "onkeydown",
        "onblur", "onkeypress", "onchange", "onload", "onunload"
    };

    private static readonly Regex EventHandlerAttrRegex = new Regex(
        "(?<=\\s)(" + string.Join("|", EventHandlerAttrs) + ")(?=\\s*=\\s*[\"'])",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private readonly Stream _inner;
    private readonly string _nonce;
    private readonly MemoryStream _buffer = new MemoryStream();

    public CspResponseFilter(Stream inner, string nonce)
    {
        _inner = inner;
        _nonce = nonce;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        _buffer.Write(buffer, offset, count);
    }

    public override void Flush()
    {
        if (_buffer.Length > 0)
        {
            byte[] pending = _buffer.ToArray();
            _buffer.SetLength(0);

            string contentType = HttpContext.Current != null ? HttpContext.Current.Response.ContentType : null;
            bool isHtml = contentType != null && contentType.IndexOf("text/html", StringComparison.OrdinalIgnoreCase) >= 0;

            if (isHtml)
            {
                Encoding encoding = HttpContext.Current.Response.ContentEncoding ?? Encoding.UTF8;
                string html = encoding.GetString(pending);

                html = DoPostBackRegex.Replace(html, RewriteDoPostBack);
                html = DoPostBackWithOptionsRegex.Replace(html, RewriteDoPostBackWithOptions);

                html = EventHandlerAttrRegex.Replace(html, "data-$1");

                html = ExistingScriptNonceRegex.Replace(html, "$1");
                html = ScriptTagRegex.Replace(html, "<script nonce=\"" + _nonce + "\"");

                pending = encoding.GetBytes(html);
            }

            _inner.Write(pending, 0, pending.Length);
        }

        _inner.Flush();
    }

    private static string RewriteDoPostBack(Match m)
    {
        string target = HttpUtility.HtmlAttributeEncode(m.Groups[1].Value);
        string argument = HttpUtility.HtmlAttributeEncode(m.Groups[2].Value);
        return "href=\"#\" data-pb-target=\"" + target + "\" data-pb-arg=\"" + argument + "\"";
    }

    private static string RewriteDoPostBackWithOptions(Match m)
    {
        string target = HttpUtility.HtmlAttributeEncode(m.Groups[1].Value);
        string argument = HttpUtility.HtmlAttributeEncode(m.Groups[2].Value);
        string validation = m.Groups[3].Value;
        string group = HttpUtility.HtmlAttributeEncode(m.Groups[4].Value);
        string action = HttpUtility.HtmlAttributeEncode(m.Groups[5].Value);
        string trackFocus = m.Groups[6].Value;
        string clientSubmit = m.Groups[7].Value;

        return "href=\"#\" data-pbo-target=\"" + target + "\" data-pbo-arg=\"" + argument + "\"" +
               " data-pbo-validation=\"" + validation + "\" data-pbo-group=\"" + group + "\"" +
               " data-pbo-action=\"" + action + "\" data-pbo-trackfocus=\"" + trackFocus + "\"" +
               " data-pbo-clientsubmit=\"" + clientSubmit + "\"";
    }

    public override void Close()
    {
        Flush();
        _inner.Close();
        base.Close();
    }

    public override bool CanRead { get { return _inner.CanRead; } }
    public override bool CanSeek { get { return _inner.CanSeek; } }
    public override bool CanWrite { get { return _inner.CanWrite; } }
    public override long Length { get { return _inner.Length; } }
    public override long Position
    {
        get { return _inner.Position; }
        set { _inner.Position = value; }
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        return _inner.Read(buffer, offset, count);
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        return _inner.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
        _inner.SetLength(value);
    }
}
