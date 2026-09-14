<%@ WebHandler Language="C#" Class="CspReport" %>

using System;
using System.IO;
using System.Web;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;

/// <summary>
/// Collects CSP violation reports sent by the browser while a
/// Content-Security-Policy-Report-Only header is active, so the stricter candidate policy
/// (no unsafe-eval, no unsafe-inline on script-src-attr) can be evaluated against real usage
/// before it's ever enforced. Report-Only never blocks anything - this only observes.
///
/// Accepts both the legacy CSP report shape ({"csp-report": {...}}, Content-Type
/// application/csp-report) and the newer Reporting API shape ([{ "type": "csp-violation",
/// "body": {...} }], Content-Type application/reports+json) since browsers differ.
/// </summary>
public class CspReport : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.StatusCode = 204;

        if (!string.Equals(context.Request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        string rawBody;
        using (var reader = new StreamReader(context.Request.InputStream))
        {
            rawBody = reader.ReadToEnd();
        }

        if (string.IsNullOrWhiteSpace(rawBody))
        {
            return;
        }

        string disposition = null;
        string violatedDirective = null;
        string blockedUri = null;
        string documentUri = null;
        string sourceFile = null;
        string lineNumber = null;
        string columnNumber = null;
        string sample = null;

        try
        {
            JObject body = ExtractReportBody(rawBody);
            if (body != null)
            {
                disposition = GetFirst(body, "disposition");
                violatedDirective = GetFirst(body, "violatedDirective", "effectiveDirective", "violated-directive");
                blockedUri = GetFirst(body, "blockedURL", "blocked-uri");
                documentUri = GetFirst(body, "documentURL", "document-uri");
                sourceFile = GetFirst(body, "sourceFile", "source-file");
                lineNumber = GetFirst(body, "lineNumber", "line-number");
                columnNumber = GetFirst(body, "columnNumber", "column-number");
                sample = GetFirst(body, "sample", "script-sample");
            }
        }
        catch
        {
            // Malformed/unexpected report shape - still log the raw body below.
        }

        var methods = new Methods();
        try
        {
            methods.ExecuteCommandWthParam(
                "INSERT INTO CSPViolationLog " +
                "(LogDate, LogTime, Disposition, ViolatedDirective, BlockedUri, DocumentUri, SourceFile, LineNumber, ColumnNumber, Sample, RawReport) " +
                "VALUES (@LogDate, @LogTime, @Disposition, @ViolatedDirective, @BlockedUri, @DocumentUri, @SourceFile, @LineNumber, @ColumnNumber, @Sample, @RawReport)",
                new SqlParameter("@LogDate", DateTime.Now.ToString("yyyyMMdd")),
                new SqlParameter("@LogTime", DateTime.Now.ToString("HHmmss")),
                new SqlParameter("@Disposition", (object)disposition ?? DBNull.Value),
                new SqlParameter("@ViolatedDirective", (object)violatedDirective ?? DBNull.Value),
                new SqlParameter("@BlockedUri", (object)blockedUri ?? DBNull.Value),
                new SqlParameter("@DocumentUri", (object)documentUri ?? DBNull.Value),
                new SqlParameter("@SourceFile", (object)sourceFile ?? DBNull.Value),
                new SqlParameter("@LineNumber", (object)lineNumber ?? DBNull.Value),
                new SqlParameter("@ColumnNumber", (object)columnNumber ?? DBNull.Value),
                new SqlParameter("@Sample", (object)sample ?? DBNull.Value),
                new SqlParameter("@RawReport", rawBody)
            );
        }
        catch
        {
            methods.WriteLog("CSPViolationLog insert failed - raw report: " + rawBody.Replace("\r", " ").Replace("\n", " "));
        }
    }

    private static string GetFirst(JObject body, params string[] keys)
    {
        foreach (string key in keys)
        {
            JToken token = body[key];
            if (token != null && token.Type != JTokenType.Null)
            {
                return token.ToString();
            }
        }
        return null;
    }

    private static JObject ExtractReportBody(string rawBody)
    {
        string trimmed = rawBody.TrimStart();
        if (trimmed.StartsWith("["))
        {
            JArray array = JArray.Parse(rawBody);
            return array.Count > 0 ? array[0]["body"] as JObject : null;
        }

        JObject obj = JObject.Parse(rawBody);
        return (obj["csp-report"] as JObject) ?? obj;
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
