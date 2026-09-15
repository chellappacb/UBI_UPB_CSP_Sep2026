// CSP-safe postback bridge.
//
// LinkButton (and cross-page-postback LinkButton) controls render
// href="javascript:__doPostBack(...)" / href="javascript:WebForm_DoPostBackWithOptions(...)".
// javascript: URIs can never be allow-listed under CSP (no nonce/hash covers them).
//
// For a full page response, CspResponseFilter.cs already rewrites these hrefs server-side
// into href="#" plus data-pb-*/data-pbo-* attributes. It deliberately does NOT do this for
// UpdatePanel async-postback responses (see Global.asax's AttachCspFilter) - that wire
// format is length-prefixed per segment, and changing a segment's content length without
// recalculating its prefix would desync the client-side parser; separately, a fresh nonce
// generated for that response could never be valid anyway, since CSP nonce validation is
// pinned to the document's policy from its original page load. So for content delivered by
// an async postback, this file rewires the same href pattern client-side instead, once the
// browser (not a byte-level filter) has already correctly parsed it into the DOM.
(function () {
    var DO_POSTBACK_RE = /^javascript:__doPostBack\('([^']*)',\s*'([^']*)'\)$/i;
    var DO_POSTBACK_OPTIONS_RE = /^javascript:WebForm_DoPostBackWithOptions\(new WebForm_PostBackOptions\(\s*"([^"]*)"\s*,\s*"([^"]*)"\s*,\s*(true|false)\s*,\s*"([^"]*)"\s*,\s*"([^"]*)"\s*,\s*(true|false)\s*,\s*(true|false)\s*\)\)$/i;

    function rewireJavascriptHrefLinks() {
        var anchors = document.querySelectorAll(
            'a[href^="javascript:__doPostBack("], a[href^="javascript:WebForm_DoPostBackWithOptions("]'
        );

        anchors.forEach(function (a) {
            var href = a.getAttribute('href');

            var m = DO_POSTBACK_RE.exec(href);
            if (m) {
                a.setAttribute('data-pb-target', m[1]);
                a.setAttribute('data-pb-arg', m[2]);
                a.setAttribute('href', '#');
                return;
            }

            var m2 = DO_POSTBACK_OPTIONS_RE.exec(href);
            if (m2) {
                a.setAttribute('data-pbo-target', m2[1]);
                a.setAttribute('data-pbo-arg', m2[2]);
                a.setAttribute('data-pbo-validation', m2[3]);
                a.setAttribute('data-pbo-group', m2[4]);
                a.setAttribute('data-pbo-action', m2[5]);
                a.setAttribute('data-pbo-trackfocus', m2[6]);
                a.setAttribute('data-pbo-clientsubmit', m2[7]);
                a.setAttribute('href', '#');
            }
        });
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', rewireJavascriptHrefLinks);
    } else {
        rewireJavascriptHrefLinks();
    }

    // Re-run after every UpdatePanel async postback completes - the same point MSAJAX
    // itself uses to re-initialize client behaviors against the refreshed DOM.
    if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(rewireJavascriptHrefLinks);
    }
})();

// Delegation on document (rather than binding per-element) means this keeps working for
// elements an UpdatePanel swaps in during an async postback, with no rebinding needed.
//
// This runs in the CAPTURE phase (the `true` third argument below), not bubble - some
// modal/dialog implementations call event.stopPropagation() on clicks inside the dialog
// (so they aren't mistaken for a backdrop click that should close it), which would
// silently stop a bubble-phase document listener from ever seeing the click. Capture
// fires top-down before any such bubble-phase stopPropagation() can block it.
//
// Firing in capture phase means this runs BEFORE the element's own onclick="" attribute
// (e.g. from OnClientClick, such as onclick="return confirm(...)") would naturally fire -
// so it can't rely on the browser having already run that and set event.defaultPrevented.
// Instead it invokes el.onclick itself first, mirroring what the browser would do, then
// calls stopPropagation() so that handler doesn't ALSO fire a second time naturally during
// the target/bubble phase that follows.
document.addEventListener('click', function (e) {
    var el = e.target.closest('[data-pb-target], [data-pbo-target]');
    if (!el) {
        return;
    }

    var proceed = true;
    if (typeof el.onclick === 'function') {
        proceed = el.onclick.call(el, e) !== false;
    }

    e.preventDefault();
    e.stopPropagation();

    if (!proceed) {
        return;
    }

    if (el.hasAttribute('data-pbo-target')) {
        var options = new WebForm_PostBackOptions(
            el.getAttribute('data-pbo-target'),
            el.getAttribute('data-pbo-arg'),
            el.getAttribute('data-pbo-validation') === 'true',
            el.getAttribute('data-pbo-group'),
            el.getAttribute('data-pbo-action'),
            el.getAttribute('data-pbo-trackfocus') === 'true',
            el.getAttribute('data-pbo-clientsubmit') === 'true'
        );
        WebForm_DoPostBackWithOptions(options);
    } else {
        __doPostBack(el.getAttribute('data-pb-target'), el.getAttribute('data-pb-arg'));
    }
}, true);

// CSP-safe inline-event-handler bridge (script-src-attr).
//
// script-src-attr governs onclick="", onload="", etc. Rather than allow-listing their
// content with a nonce/hash (which must byte-match the RENDERED attribute exactly - and
// we've already been burned twice in this migration by rendered output not matching
// assumptions), every distinct inline handler value found app-wide has been hand-copied
// into a named function in JS/CSP/OnClickHandlers.js (window.CspOnClickHandlers). This
// pass finds every element still carrying one of the attributes below, matches its exact
// live value (already decoded by the browser, regardless of how it was HTML-entity-encoded
// in the source) against that map, and swaps it for a real handler - via PROPERTY
// assignment (el[attr] = fn), not addEventListener. That distinction matters: the
// __doPostBack bridge above reads el.onclick as a property to decide whether a confirm()
// dialog cancels a postback (el.onclick.call(el, e) !== false); addEventListener would not
// populate that property, silently breaking that check for elements that have both an
// OnClientClick confirm() and a postback (e.g. the "Exit" links).
(function () {
    // CspResponseFilter.cs renames these attributes server-side to a data-* prefixed form
    // (onclick -> data-onclick, etc.) for any full-page response, specifically so the
    // browser never parses a live onXXX="..." attribute and therefore never has anything
    // to raise a script-src-attr violation over in the first place (a violation the
    // browser would otherwise log the moment it parses the attribute - for onload/onunload
    // on <body> that happens before any <script> on the page runs, so removing the
    // attribute here on DOMContentLoaded was always too late to prevent it). Content
    // delivered by an UpdatePanel async postback is never touched by that filter (see
    // Global.asax's AttachCspFilter), so it still carries the live onXXX attribute
    // verbatim - hence the fallback to it below.
    function getHandlerAttr(el, attr) {
        var dataAttr = 'data-' + attr;
        return el.hasAttribute(dataAttr) ? el.getAttribute(dataAttr) : el.getAttribute(attr);
    }

    function clearHandlerAttr(el, attr) {
        el.removeAttribute('data-' + attr);
        el.removeAttribute(attr);
    }

    function handlerSelector(attr) {
        return '[data-' + attr + '], [' + attr + ']';
    }

    // onkeyup/onblur and onchange were added after CSP Report-Only 'sample' data showed
    // the MacroWebTextBox custom control (onkeyup/onblur) and AutoPostBack dropdowns/radio
    // buttons (onchange) use attributes the original markup-only scan didn't cover.
    var INLINE_HANDLER_ATTRS = [
        'onclick', 'onload', 'onunload', 'onmouseover', 'onmouseout', 'onkeydown',
        'onkeyup', 'onblur', 'onchange'
    ];

    // Every watched attribute, including onkeypress (handled separately from
    // INLINE_HANDLER_ATTRS above because it needs the AutoTab/DefaultButton parsers
    // tried first) - shared by the setAttribute() override and the MutationObserver
    // further down, so both agree on exactly the same set.
    var OBSERVED_ATTRS = INLINE_HANDLER_ATTRS.concat(['onkeypress']);

    // ASP.NET doesn't always render OnClientClick text byte-for-byte: LinkButton copies it
    // verbatim, but Button rendering has been observed to append a trailing ";" when the
    // source markup didn't already end with one (e.g. Preview.aspx's OnClientClick="return
    // Check()" renders as onclick="return Check();"). Rather than hand-add both variants of
    // every entry, the lookup itself tolerates one trailing semicolon either direction.
    function lookupHandler(map, value) {
        if (map[value]) {
            return map[value];
        }
        if (value.charAt(value.length - 1) === ';') {
            return map[value.slice(0, -1)];
        }
        return map[value + ';'];
    }

    // Resolves a raw attribute value against the generic named-function map
    // (window.CspOnClickHandlers), without touching any element - used both by
    // tryGenericMap below (acting on an attribute already on the DOM) and by the
    // setAttribute() override further down (acting on a value that hasn't been written
    // to the DOM yet).
    function resolveGenericMap(value) {
        return lookupHandler(window.CspOnClickHandlers || {}, value) || null;
    }

    // Applies the generic named-function map to a single element/attribute pair. Returns
    // true if a match was found and wired up, so callers (a full sweep, or the
    // MutationObserver below acting on one element) can tell whether they need to fall
    // back to a warning.
    function tryGenericMap(el, attr) {
        var fn = resolveGenericMap(getHandlerAttr(el, attr));
        if (fn) {
            clearHandlerAttr(el, attr);
            el[attr] = fn;
            return true;
        }
        return false;
    }

    function rewireInlineHandlers() {
        INLINE_HANDLER_ATTRS.forEach(function (attr) {
            document.querySelectorAll(handlerSelector(attr)).forEach(function (el) {
                if (!tryGenericMap(el, attr)) {
                    console.warn('CSP: no registered handler for ' + attr + '="' + getHandlerAttr(el, attr) + '" on', el);
                }
            });
        });
    }

    // Controls with AutoPostBack wrap their postback in a STRING-argument setTimeout
    // (javascript:setTimeout('__doPostBack(\'id\',\'arg\')', delay)) - a longstanding
    // WebForms workaround for an old IE onchange/postback timing bug. Confirmed via real
    // rendered markup (a ddlTittle <select>'s onchange). Left as-is, this would need
    // 'unsafe-eval' to run (string-argument setTimeout is exactly what that governs) even
    // if the attribute itself were allow-listed - so this is parsed and rewired into a
    // setTimeout call with a real function reference instead, removing that eval reliance
    // entirely rather than just moving the script-src-attr problem around.
    var AUTOPOSTBACK_SETTIMEOUT_RE = /^javascript:setTimeout\('__doPostBack\(\\'([^\\]*)\\',\\'([^\\]*)\\'\)',\s*(\d+)\)$/i;

    // Resolves the AutoPostBack setTimeout(...) shape from a raw value, without touching
    // any element. Returns a handler function, or null if the value doesn't match.
    function resolveAutoPostBackSetTimeout(value) {
        var m = value ? AUTOPOSTBACK_SETTIMEOUT_RE.exec(value) : null;
        if (!m) {
            return null;
        }

        var target = m[1];
        var arg = m[2];
        var delay = parseInt(m[3], 10);
        return function () {
            setTimeout(function () {
                __doPostBack(target, arg);
            }, delay);
        };
    }

    // Tries the AutoPostBack setTimeout(...) shape on a single element/attribute pair.
    // Returns true if it matched and was wired up.
    function tryAutoPostBackSetTimeout(el, attr) {
        var fn = resolveAutoPostBackSetTimeout(getHandlerAttr(el, attr));
        if (!fn) {
            return false;
        }

        clearHandlerAttr(el, attr);
        el[attr] = fn;
        return true;
    }

    function rewireAutoPostBackSetTimeout() {
        ['onchange', 'onclick'].forEach(function (attr) {
            document.querySelectorAll(handlerSelector(attr)).forEach(function (el) {
                tryAutoPostBackSetTimeout(el, attr);
            });
        });
    }

    // Digit-entry fields (passport numbers, OTP boxes, etc.) render
    // onkeypress="AutoTab('fromId','toId','maxLen',event);" to auto-advance focus to the next
    // box - a distinct value per field (source/target ids and length vary), so like
    // __doPostBack this needs generic parsing rather than a fixed named-function map entry.
    // Found via CSP Report-Only console warnings on ApplicantDetails.aspx's passport section.
    var AUTO_TAB_RE = /^AutoTab\('([^']*)','([^']*)','([^']*)',event\);?$/i;

    // Resolves the AutoTab(...) shape from a raw value, without touching any element.
    // Returns a handler function, or null if the value doesn't match.
    function resolveAutoTab(value) {
        var m = value ? AUTO_TAB_RE.exec(value) : null;
        if (!m) {
            return null;
        }

        var fromId = m[1];
        var toId = m[2];
        var maxLen = m[3];
        return function (e) {
            return AutoTab(fromId, toId, maxLen, e || window.event);
        };
    }

    // Tries the AutoTab(...) shape on a single element. Returns true if it matched.
    function tryAutoTab(el) {
        var fn = resolveAutoTab(getHandlerAttr(el, 'onkeypress'));
        if (!fn) {
            return false;
        }

        clearHandlerAttr(el, 'onkeypress');
        el.onkeypress = fn;
        return true;
    }

    function rewireAutoTab() {
        document.querySelectorAll(handlerSelector('onkeypress')).forEach(function (el) {
            tryAutoTab(el);
        });
    }

    // <asp:Panel DefaultButton="..."> and the <form>'s own default button both render
    // onkeypress="javascript:return WebForm_FireDefaultButton(event, '<id>')" on their
    // wrapper element. This is framework-generated (not literal markup text a source grep
    // would find), but the shape is fixed and parseable, like __doPostBack - so it's handled
    // generically here rather than via the named-function map.
    var FIRE_DEFAULT_BUTTON_RE = /^javascript:return WebForm_FireDefaultButton\(event,\s*'([^']*)'\)$/i;

    // Resolves the WebForm_FireDefaultButton(...) shape from a raw value, without
    // touching any element. Returns a handler function, or null if the value doesn't
    // match.
    function resolveDefaultButton(value) {
        var m = value ? FIRE_DEFAULT_BUTTON_RE.exec(value) : null;
        if (!m) {
            return null;
        }

        var targetId = m[1];
        return function (e) {
            return WebForm_FireDefaultButton(e || window.event, targetId);
        };
    }

    // Tries the WebForm_FireDefaultButton(...) shape on a single element. Returns true if
    // it matched.
    function tryDefaultButton(el) {
        var fn = resolveDefaultButton(getHandlerAttr(el, 'onkeypress'));
        if (!fn) {
            return false;
        }

        clearHandlerAttr(el, 'onkeypress');
        el.onkeypress = fn;
        return true;
    }

    function rewireDefaultButtons() {
        document.querySelectorAll(handlerSelector('onkeypress')).forEach(function (el) {
            if (tryDefaultButton(el)) {
                return;
            }

            // Not a DefaultButton pattern - fall back to the named-function map (e.g. the
            // standard ASP.NET WebForm_TextBoxKeyHandler onkeypress) before giving up. This
            // fallback was missing before, so a matching map entry was silently never used.
            if (!tryGenericMap(el, 'onkeypress')) {
                console.warn('CSP: unrecognized onkeypress="' + getHandlerAttr(el, 'onkeypress') + '" on', el);
            }
        });
    }

    // Single entry point for handling exactly one element/attribute pair, in the same
    // precedence order the full sweeps below already use (dedicated parsers before the
    // generic map, so e.g. an AutoTab onkeypress doesn't get mistaken for a plain map
    // lookup). The MutationObserver further down calls this directly for whatever one
    // element/attribute a mutation just touched, reusing the exact same try* helpers the
    // sweeps use - there is only ever one copy of each pattern's matching/wiring logic.
    // Returns true if a matching handler was found and wired up, false otherwise.
    function processHandlerAttr(el, attr) {
        if (attr === 'onkeypress') {
            if (tryAutoTab(el)) {
                return true;
            }
            if (tryDefaultButton(el)) {
                return true;
            }
            return tryGenericMap(el, 'onkeypress');
        }

        if (attr === 'onchange' || attr === 'onclick') {
            if (tryAutoPostBackSetTimeout(el, attr)) {
                return true;
            }
        }

        if (INLINE_HANDLER_ATTRS.indexOf(attr) !== -1) {
            return tryGenericMap(el, attr);
        }

        return false;
    }

    // Resolves a raw (attr, value) pair against every pattern processHandlerAttr()
    // recognizes, in the same precedence order, but without requiring the value to
    // already be on an element. Used by the setAttribute() override below to decide, at
    // the moment a third-party script is about to write an onXXX attribute, whether a
    // real handler can be substituted for it instead.
    function resolveHandlerFn(attr, value) {
        if (attr === 'onkeypress') {
            return resolveAutoTab(value) || resolveDefaultButton(value) || resolveGenericMap(value);
        }

        if (attr === 'onchange' || attr === 'onclick') {
            var fn = resolveAutoPostBackSetTimeout(value);
            if (fn) {
                return fn;
            }
        }

        if (INLINE_HANDLER_ATTRS.indexOf(attr) !== -1) {
            return resolveGenericMap(value);
        }

        return null;
    }

    // script-src-attr is checked synchronously by the browser at the exact moment an
    // onXXX attribute is written to an element - whether that happens by the HTML parser
    // reading markup, or by a script calling setAttribute(). A MutationObserver can only
    // ever react to that write afterward, as a separate microtask; by then the check has
    // already run and the violation has already been reported (and, under the enforced
    // policy, the handler has already been blocked from running). CSP Report-Only
    // evidence traced a whole family of violations (MacroWebTextBox's Val()/Validate()
    // keyup/blur behaviors, AutoTab, WebForm_FireDefaultButton, AutoPostBack wiring) to a
    // single shared call site inside the Microsoft AJAX/AjaxControlToolkit combined
    // script bundle (ScriptResource.axd) - a generic "set this attribute" helper used by
    // several different behaviors, called after the page has already loaded, that in
    // every case ends up invoking Element.prototype.setAttribute with one of our watched
    // names. None of that script's source is in this repo to edit directly, so the only
    // way to actually prevent (not just clean up after) those violations is to intercept
    // the call itself, synchronously, before the browser's own attribute-setting
    // algorithm - and therefore its CSP check - ever runs.
    //
    // This must be installed as early as Home.js's own top-level code runs (which it is,
    // being a plain synchronous statement in this IIFE) so that it is in place before any
    // later inline script block or event-driven callback (e.g. Sys.Application's pageLoad
    // cycle, which fires only once the whole page has finished parsing) gets a chance to
    // call setAttribute first. It does not help with attributes that arrive already
    // embedded in a larger string handed to innerHTML/insertAdjacentHTML (that goes
    // through the HTML parser, not this method) - the childList-scanning MutationObserver
    // below remains the mechanism for that case (e.g. UpdatePanel async-postback content).
    var nativeSetAttribute = Element.prototype.setAttribute;

    Element.prototype.setAttribute = function (name, value) {
        var attr = typeof name === 'string' ? name.toLowerCase() : name;

        if (OBSERVED_ATTRS.indexOf(attr) === -1) {
            return nativeSetAttribute.call(this, name, value);
        }

        var fn = resolveHandlerFn(attr, value);
        if (fn) {
            this[attr] = fn;
            return;
        }

        // No known pattern matches this value - it would be blocked by script-src-attr
        // 'none' regardless, so there is nothing safe to fall back to. Warn instead of
        // writing the raw attribute, which would just reproduce the exact violation this
        // override exists to prevent.
        console.warn('CSP: no registered handler for ' + attr + '="' + value + '" (setAttribute intercepted) on', this);
    };

    // Mirrors CspResponseFilter.cs's EventHandlerAttrRegex rename (see its class comment,
    // point 2) for the one content type that filter can never reach: an UpdatePanel
    // async-postback response. Global.asax's AttachCspFilter deliberately skips that filter
    // for these responses (its own comment explains why - the wire format is
    // length-prefixed per segment, and a fresh nonce could never be valid for it anyway), so
    // this markup still carries live onXXX="..." attributes verbatim when it reaches the
    // browser. PageRequestManager applies it via an innerHTML-style write, which the HTML
    // parser processes directly - never calling Element.prototype.setAttribute - so the
    // setAttribute() override above cannot see it either. Matched the same way as the
    // server-side regex - preceded by whitespace (so "buttononclick" can't match mid-word),
    // followed by "=" plus an opening quote, never touching the quoted value itself - but
    // written without a lookbehind assertion (broader browser support) by capturing the
    // preceding whitespace and re-emitting it in the replacement instead.
    var EVENT_HANDLER_ATTR_RE = new RegExp(
        '(\\s)(' + OBSERVED_ATTRS.join('|') + ')(?=\\s*=\\s*["\'])',
        'gi'
    );

    function renameEventHandlerAttrs(html) {
        return html.replace(EVENT_HANDLER_ATTR_RE, '$1data-$2');
    }

    // script-src-attr is checked synchronously by the browser at the exact moment the HTML
    // parser writes an onXXX attribute - the same timing problem the setAttribute() override
    // above solves for script-driven writes, but here the write comes from parsing a string
    // handed to innerHTML, which no MutationObserver or setAttribute override can intercept
    // in time. The only way to prevent it is to rewrite that string before the native
    // innerHTML setter (and therefore the parser) ever sees it.
    //
    // Sys.WebForms.PageRequestManager's pageLoading/pageLoaded events are public, documented
    // MS AJAX API, not an internal implementation detail: pageLoading fires synchronously
    // immediately before panel content is written, with args.get_panelsUpdating() naming
    // exactly which elements are about to change; pageLoaded fires immediately after, once
    // they have. Recording those elements only for the brief synchronous window between the
    // two - and only transforming innerHTML writes that land on one of them - keeps this
    // override's effect scoped to exactly the operation it exists for, not a permanent,
    // page-wide behavior change. Elements outside that window, or not in that set, pass
    // through to the native setter completely untouched.
    var panelsBeingUpdated = null;

    if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_pageLoading(function (sender, args) {
            panelsBeingUpdated = args.get_panelsUpdating();
        });

        prm.add_pageLoaded(function () {
            panelsBeingUpdated = null;
        });
    }

    var nativeInnerHTMLDescriptor = Object.getOwnPropertyDescriptor(Element.prototype, 'innerHTML');

    Object.defineProperty(Element.prototype, 'innerHTML', {
        configurable: true,
        enumerable: nativeInnerHTMLDescriptor.enumerable,
        get: nativeInnerHTMLDescriptor.get,
        set: function (html) {
            if (panelsBeingUpdated && typeof html === 'string' && panelsBeingUpdated.indexOf(this) !== -1) {
                html = renameEventHandlerAttrs(html);
            }
            nativeInnerHTMLDescriptor.set.call(this, html);
        }
    });

    function rewireAll() {
        // Dedicated parsers run first so they consume the attributes they recognize before
        // the generic named-function map gets a chance to log a false "no registered
        // handler" warning for something it was never meant to handle directly.
        rewireAutoPostBackSetTimeout();
        rewireAutoTab();
        rewireDefaultButtons();
        rewireInlineHandlers();
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', rewireAll);
    } else {
        rewireAll();
    }

    if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(rewireAll);
    }

    // Defense in depth alongside the setAttribute() override above, for the cases that
    // override cannot see:
    //
    // - Attributes that arrive already embedded in an HTML string assigned via
    //   innerHTML/insertAdjacentHTML (parsed by the browser's HTML parser, which never
    //   calls Element.prototype.setAttribute) - the main known example being an
    //   UpdatePanel async-postback response, whose content CspResponseFilter.cs
    //   deliberately never touches server-side (see Global.asax's AttachCspFilter) and
    //   whose refresh MSAJAX applies via exactly this kind of HTML-string assignment.
    //   That shows up here as a `childList` mutation - a whole new chunk of markup
    //   landing in the DOM already carrying a live onXXX attribute - which an
    //   attributes-only watch could never see no matter when it ran. Only the
    //   newly-added node(s) and their descendants are scanned when this fires - never the
    //   whole document - via querySelectorAll scoped to that node.
    // - A final safety net for any `attributes` mutation that reaches the DOM by some
    //   path other than setAttribute() or the HTML parser (e.g. a library calling
    //   setAttributeNS, or any future code path this override doesn't anticipate).
    if (window.MutationObserver) {
        var OBSERVED_ATTR_NAMES = [];
        OBSERVED_ATTRS.forEach(function (attr) {
            OBSERVED_ATTR_NAMES.push(attr);
            OBSERVED_ATTR_NAMES.push('data-' + attr);
        });

        var CHILDLIST_SCAN_SELECTOR = OBSERVED_ATTR_NAMES.map(function (name) {
            return '[' + name + ']';
        }).join(', ');

        // Returns the watched attribute base-names (e.g. 'onclick', not 'data-onclick')
        // actually present - in either form - on a single element.
        function findHandlerAttrsOnElement(el) {
            var found = [];
            OBSERVED_ATTRS.forEach(function (attr) {
                if (el.hasAttribute(attr) || el.hasAttribute('data-' + attr)) {
                    found.push(attr);
                }
            });
            return found;
        }

        // Scans exactly one newly-added node (and its descendants, if any) for watched
        // attributes. Never touches anything outside this node's own subtree.
        function scanAddedNode(node) {
            if (!node || node.nodeType !== 1) {
                return;
            }

            findHandlerAttrsOnElement(node).forEach(function (attr) {
                processHandlerAttr(node, attr);
            });

            if (node.querySelectorAll) {
                node.querySelectorAll(CHILDLIST_SCAN_SELECTOR).forEach(function (descendant) {
                    findHandlerAttrsOnElement(descendant).forEach(function (attr) {
                        processHandlerAttr(descendant, attr);
                    });
                });
            }
        }

        var handlerAttrObserver = new MutationObserver(function (mutations) {
            mutations.forEach(function (mutation) {
                if (mutation.type === 'childList') {
                    mutation.addedNodes.forEach(function (node) {
                        scanAddedNode(node);
                    });
                    return;
                }

                if (mutation.type !== 'attributes') {
                    return;
                }

                var el = mutation.target;
                if (!el || el.nodeType !== 1) {
                    return;
                }

                var attrName = mutation.attributeName;
                var attr = attrName.indexOf('data-') === 0 ? attrName.slice(5) : attrName;
                if (OBSERVED_ATTRS.indexOf(attr) === -1) {
                    return;
                }

                // clearHandlerAttr() removes both the data-* and live forms of an attribute
                // as part of wiring a match up, which is itself an attribute mutation this
                // same observer watches for. By the time that follow-up mutation is
                // delivered, neither form is present any more, so this guard stops here
                // instead of re-processing an element that was just handled - this is what
                // prevents the observer from reacting to its own rewiring and looping.
                if (!el.hasAttribute('data-' + attr) && !el.hasAttribute(attr)) {
                    return;
                }

                processHandlerAttr(el, attr);
            });
        });

        handlerAttrObserver.observe(document.documentElement, {
            attributes: true,
            subtree: true,
            childList: true,
            attributeFilter: OBSERVED_ATTR_NAMES
        });
    }
})();
