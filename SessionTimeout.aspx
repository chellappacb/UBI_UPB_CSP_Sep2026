<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SessionTimeout.aspx.cs" Inherits="SessionTimeout" %>

<%@ Register Src="header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="~/HeaderInclude.ascx" TagName="headerincl" TagPrefix="id" %>
<%@ Register Src="footer.ascx" TagName="footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <id:headerincl ID="headerinclude" runat="server" />
   <!--  <link rel="Stylesheet" type="text/css" href="css/cReponsive.css" />
    <link rel="Stylesheet" type="text/css" href="css/styles.css" />   -->
    <link rel="Stylesheet" type="text/css" href="css/font.css" />
    <link rel="Stylesheet" type="text/css" href="website/Style.css" />
    <link rel="Stylesheet" type="text/css" href="website/Responsive.css" />
    <link type="text/css" href="css/WebResource.css" rel="stylesheet" />
    <link type="text/css" href="Website/Responsive.css" rel="Stylesheet" />
    <link type="text/css" href="Website/custom-style.css" rel="Stylesheet" />
    <link type="text/css" href="Website/Styles.css" rel="Stylesheet" />
    <link href="website/css/bootstrap.min.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="JS/disbackbtn.js"></script>
   
</head>
<body>
   <!-- <script type="text/javascript">
        var sessionTimeout = "<%= Session.Timeout %>";
        function DisplaySessionTimeout() {

            document.getElementById("<%= lblSession.ClientID %>").textContent = sessionTimeout;
            sessionTimeout = sessionTimeout - 1;

            if (sessionTimeout >= 0)
                window.setTimeout("DisplaySessionTimeout()", 1000);
            else {
                window.open("Index.aspx", '_self');
            }
        }

        function closeWindow() {
            window.open('', '_parent', '');
            window.close();
        }
        
    </script>  -->
    <form id="form1" runat="server">
   
        <div class="container" >
            <uc1:header ID="header1" runat="server" />
            <hr class="hr" />
           
            <div class="bg-white">
                <div class="p-5">
                    <div class="session-bg" >
                        <div class="blackhead1 text-center">
                                 Your Current session has expired! <br />
                             You will be<span> redirected to home page in</span>&nbsp;
                                <asp:Label ID="lblSession" runat="server" Text="20"></asp:Label>&nbsp; <span>seconds</span>  <br />
                                 or    <br />
                                    <a href="Index.aspx" class="hyper-link">Click here to be redirected to home page</a>
                          
                        </div>
                        
                    </div>
                </div>
               </div>
               <div >
            <uc2:footer ID="footer1" runat="server" />    </div>
        </div>
   
    </form>
     <script type="text/javascript" src="website/js/bootstrap.bundle.min.js"> </script>
    <script src="javascript/jquery-3.7.1.min.js" type="text/javascript"></script>
</body>
</html>
