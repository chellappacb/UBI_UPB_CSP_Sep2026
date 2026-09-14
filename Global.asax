<%@ Application Language="C#" %>

<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.Security.Cryptography" %>

<script RunAt="server">

    // Paths that never render our HTML (static/resource/handler endpoints) — skip filtering for these.
    private static readonly string[] CspFilterSkipExtensions = {
        ".axd", ".js", ".css", ".png", ".jpg", ".jpeg", ".gif", ".svg", ".ico",
        ".woff", ".woff2", ".ttf", ".map", ".pdf", ".ashx"
    };

    void Application_PreSendRequestHeaders(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Headers.Remove("Server");

        string nonce = HttpContext.Current.Items["CspNonce"] as string;
        if (!string.IsNullOrEmpty(nonce))
        {
            // script-src-elem carries the real per-request nonce + 'strict-dynamic', no
            // 'unsafe-inline' — CspResponseFilter rewrites every LinkButton's javascript:
            // href into a data-pb-*/data-pbo-* attribute pair handled by JS/CSP/Home.js, and
            // 'strict-dynamic' covers <script> elements MSAJAX's PageRequestManager creates
            // client-side during an UpdatePanel async postback (those never pass through
            // CspResponseFilter, so they can't carry the nonce directly).
            // script-src-attr is 'none' — JS/CSP/Home.js rewires every known onclick/onload/
            // onunload/onmouseover/onmouseout/onkeydown/onkeyup/onblur/onchange/onkeypress
            // (DefaultButton, AutoPostBack setTimeout) pattern found via source inspection and
            // CSP Report-Only evidence into real property-assigned handlers, so no inline
            // attribute content is relied upon.
            // 'unsafe-eval' stays on the base script-src — evidence-backed residual, not a
            // guess: CSP Report-Only reproduced the same eval at ScriptResource.axd line 2/
            // column 77826 ("Sys.Net.XMLHttpExecutor", core Microsoft Ajax Library XHR
            // plumbing used by UpdatePanel/ToolkitScriptManager) 8 times across independent
            // navigations on Index/AccountDetails/ApplicantDetails. Removing it would require
            // eliminating UpdatePanel/ToolkitScriptManager app-wide, out of scope here.
            string csp = "connect-src 'self'; " +
                         "img-src 'self' https://www.unionpremierbond.unionbankofindiauk.co.uk; " +
                         "style-src 'self'; " +
                         "object-src 'none'; " +
                         "script-src 'self' 'unsafe-eval'; " +
                         "script-src-elem 'self' 'nonce-" + nonce + "' 'strict-dynamic'; " +
                         "script-src-attr 'none';";

            HttpContext.Current.Response.Headers.Remove("Content-Security-Policy");
            HttpContext.Current.Response.Headers.Add("Content-Security-Policy", csp);

            // Report-Only: tests the fully-strict target policy (no 'unsafe-eval', no
            // 'unsafe-inline' on script-src-attr — JS/CSP/Home.js's onclick/onkeypress
            // rewiring is meant to cover every case instead) in parallel with the still-
            // enforced policy above. Report-Only never blocks anything, so this is safe to
            // ship while we gather evidence on whether AjaxControlToolkit's $create()/
            // Sys.Application internals (not in this repo — served from an embedded
            // assembly resource) genuinely need eval, and whether the onclick rewiring
            // covers every live case app-wide. Do NOT flip the enforced policy above until
            // a full regression pass shows zero relevant violations here.
            // 'report-sample' asks the browser to include the actual blocked inline content
            // (truncated) in the violation report's "sample"/"script-sample" field - without
            // it, script-src-attr violations report generic line=null/column=1 for every
            // attribute, which can't distinguish one onclick from another.
            string reportOnlyCsp = "connect-src 'self'; " +
                                   "img-src 'self' https://www.unionpremierbond.unionbankofindiauk.co.uk; " +
                                   "style-src 'self'; " +
                                   "object-src 'none'; " +
                                   "script-src 'self' 'strict-dynamic' 'nonce-" + nonce + "' 'report-sample'; " +
                                   "script-src-attr 'none' 'report-sample'; " +
                                   "report-uri /CspReport.ashx;";

            HttpContext.Current.Response.Headers.Remove("Content-Security-Policy-Report-Only");
            HttpContext.Current.Response.Headers.Add("Content-Security-Policy-Report-Only", reportOnlyCsp);
        }
    }

    void Application_Start(object sender, EventArgs e)
    {
        //// Code that runs on application startup
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
    }

    void Application_BeginRequest(object sender, EventArgs e)
    {
        HttpRequest request = HttpContext.Current.Request;
        HttpResponse response = HttpContext.Current.Response;

        string te = request.Headers["Transfer-Encoding"] ?? string.Empty;
        string cl = request.Headers["Content-Length"] ?? string.Empty;
        bool isPost = request.HttpMethod.Equals(
                            "POST", StringComparison.OrdinalIgnoreCase);

        // Rule 1: TE + CL conflict — blocks Attack 2
        if (!string.IsNullOrWhiteSpace(te) && !string.IsNullOrWhiteSpace(cl))
        {
            Send400(response);
            return;
        }

        // Rule 2: Block POST with Content-Length: 0 — blocks Attack 1
        if (isPost && cl == "0")
        {
            Send400(response);
            return;
        }

        AttachCspFilter(request, response);
    }

    private void AttachCspFilter(HttpRequest request, HttpResponse response)
    {
        string path = request.Path ?? string.Empty;
        foreach (string ext in CspFilterSkipExtensions)
        {
            if (path.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
        }

        // MSAJAX UpdatePanel async postbacks send this request header. Their response body
        // isn't plain HTML - it's a length-prefixed wire format ("<byteCount>|updatePanel|
        // <id>|<content>|..."). Rewriting text inside a segment without recalculating that
        // segment's leading byte count would desync the client-side parser. Separately, a
        // fresh per-request nonce could never be valid for this content anyway - CSP nonce
        // validation is pinned to the document's policy from its original page load, not
        // re-evaluated per XHR response. So: leave these responses untouched. JS/CSP/Home.js
        // handles rewiring the same href pattern client-side instead, after the browser (not
        // this filter) has parsed the wire format, via Sys.WebForms.PageRequestManager's
        // endRequest hook - the same event MSAJAX itself uses to re-initialize behaviors
        // after an UpdatePanel refresh.
        string ajaxHeader = request.Headers["X-MicrosoftAjax"];
        if (!string.IsNullOrEmpty(ajaxHeader))
        {
            return;
        }

        byte[] nonceBytes = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(nonceBytes);
        }

        string nonce = Convert.ToBase64String(nonceBytes);
        HttpContext.Current.Items["CspNonce"] = nonce;
        response.Filter = new CspResponseFilter(response.Filter, nonce);
    }

    private void Send400(HttpResponse response)
    {
        try
        {
            response.Clear();
            response.StatusCode = 400;
            response.StatusDescription = "Bad Request";
            response.Write("400 Bad Request - Suspicious Request Blocked");
            response.End();
        }
        catch (System.Threading.ThreadAbortException) { }
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends.
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer
        // or SQLServer, the event is not raised.

    }

</script>
