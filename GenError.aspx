<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenError.aspx.cs" Inherits="GenError" %>

<%@ Register Src="Header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="~/HeaderInclude.ascx" TagName="headerincl" TagPrefix="id" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <id:headerincl ID="headerinclude" runat="server" />
    <link rel="Stylesheet" type="text/css" href="css/cReponsive.css" />
    <link type="text/css" href="website/Style.css" rel="Stylesheet" />
    <link type="text/css" href="website/Responsive.css" rel="Stylesheet" />
    <link rel="Stylesheet" type="text/css" href="css/styles.css" />
    <link type="text/css" href="Website/custom-style.css" rel="Stylesheet" />
    <link href="website/css/bootstrap.min.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="JS/disbackbtn.js"></script>

</head>
<body>
    <script type="text/javascript">
        var sessionTimeout = "<%= Session.Timeout %>";
        function DisplaySessionTimeout() {

            document.getElementById("<%= lblSession.ClientID %>").textContent = sessionTimeout;
            sessionTimeout = sessionTimeout - 1;

            if (sessionTimeout >= 0)
                window.setTimeout(DisplaySessionTimeout, 1000);
            else {
                window.open("Index.aspx", '_self');
                //closeWindow();                                 
            }
        }

        function closeWindow() {
            window.open('', '_parent', '');
            window.close();
        }

    </script>
    <form id="form1" runat="server">
        <div class="container">
            <uc1:header ID="header1" runat="server" />
            <hr class="hr" />
            <div class="bg-white">
                <div class="p-5">
                    <div class="session-bg">
                        <div class="blackhead1 text-center">
                            Your request can not be processed at the moment.
                            <br />
                            Please contact Union Bank of India (UK) Help Desk.
                                <br />
                            Please do not click on "Back" or "Refresh" buttons during the login session due to enhanced security.
                                <br />

                            You will be<span> redirected to home page in</span>&nbsp;
                                <asp:Label ID="lblSession" runat="server" Text="20"></asp:Label>&nbsp; <span>seconds</span>
                            <br />
                            or   
                            <br />
                            <a href="Index.aspx" class="hyper-link">Click here to be redirected to home page</a>


                        </div>

                    </div>
                </div>
            </div>
        </div>

        <%--<table class="container" style="border-collapse: collapse;">
           
            <tr>
                <td align="left" style="border: 1px solid #ededed" valign="middle">
                </td>
            </tr>
            <tr>
                <td align="center" width="100%" style="padding-top: 90px;">
                    <table cellpadding="0" cellspacing="0" align="center" width="90%" bgcolor="#f1f5f9"
                        class="yellowborder2">
                        <tr>
                            <td class="blackhead1" align="center" valign="middle">
                            </td>
                        </tr>
                        <tr>
                            <td class="blackhead1" align="center" valign="middle" height="50px">
                                Your request can not be processed at the moment.
                            </td>
                        </tr>
                        <tr>
                            <td class="blackhead1" align="center" valign="middle" height="50px">
                                Please contact Union Bank of India (UK) Help Desk.
                            </td>
                        </tr>
                        <tr>
                            <td class="blackhead1" align="center" valign="middle" height="50px">
                                Please do not click on &quot;Back&quot; or &quot;Refresh&quot; buttons during the
                                login session due to enhanced security.
                            </td>
                        </tr>
                        <tr>
                            <td class="blackhead1" align="center" valign="middle" height="50px">
                                You will be<span> redirected to home page in </span>&nbsp;
                                <asp:Label ID="lblSession" runat="server" Text="20"></asp:Label>&nbsp; <span>seconds</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" style="color: #FFFFFF">
                                or
                            </td>
                        </tr>
                        <tr>
                            <td class="blackhead1" align="center" valign="middle" height="50px">
                                <a href="Index.aspx" style="color: #00579C;">Click here to be redirected to home page</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="80">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <uc2:footer ID="footer1" runat="server" />
                </td>
            </tr>
        </table>--%>
    </form>
    <script type="text/javascript" src="website/js/bootstrap.bundle.min.js"></script>
</body>
</html>
