<%@ Application Language="C#" %>

<%@ Import Namespace="System.Net" %>

<script RunAt="server">

    void Application_PreSendRequestHeaders(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Headers.Remove("Server");

        // NOTE: no nonce here. WebForms LinkButton/postback controls render
        // href="javascript:__doPostBack(...)" / "javascript:WebForm_DoPostBackWithOptions(...)".
        // Per the CSP spec, as soon as script-src contains a nonce-source, 'unsafe-inline' is
        // dropped for the WHOLE directive - including javascript: URI navigation, which cannot
        // be nonce'd or hashed (script-src-attr, which handles onclick="" etc., explicitly does
        // NOT cover javascript: URIs either). Adding a nonce here breaks every postback link in
        // the app. Until those links are reworked to not use javascript: hrefs, 'unsafe-inline'
        // must stay genuinely active, so script-src carries no nonce.
        string csp = "connect-src 'self'; " +
                     "img-src 'self' https://www.unionpremierbond.unionbankofindiauk.co.uk; " +
                     "style-src 'self'; " +
                     "script-src 'self' 'unsafe-inline' 'unsafe-eval';";

        HttpContext.Current.Response.Headers.Remove("Content-Security-Policy");
        HttpContext.Current.Response.Headers.Add("Content-Security-Policy", csp);
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
