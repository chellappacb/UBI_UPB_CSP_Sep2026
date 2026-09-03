<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExistingCus.aspx.cs" Inherits="ExistingCus" %>

<%@ Register Src="header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="~/HeaderInclude.ascx" TagName="headerincl" TagPrefix="id" %>
<%@ Register Src="FSCS.ascx" TagName="FSCS" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <id:headerincl ID="headerinclude" runat="server" />
    <link rel="Stylesheet" type="text/css" href="css/cReponsive.css" />
    <link type="text/css" href="website/Style.css" rel="Stylesheet" />
    <link type="text/css" href="website/Responsive.css" rel="Stylesheet" />
    <link rel="Stylesheet" type="text/css" href="css/styles.css" />
    <script language="javascript" type="text/javascript" src="JS/disbackbtn.js"></script>

    <link href="website/css/bootstrap.min.css" rel="stylesheet" />
    <link type="text/css" href="Website/custom-style.css" rel="Stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="wrapp" border="0">
            <uc1:header ID="header1" runat="server" />

            <div class="bg-theme text-right h-50 pr-2 d-flex justify-content-end align-items-center">
                <a id="LnkbtnHome" class="link" href="index.aspx">Home</a>
            </div>

            <div class="text-center p-3">
                 <p class="text-theme text-center font-weight-bold text-decoration-underline font-22">
                        Union Bank of India (UK) - Existing Customer
                    </p>
            </div>

            <div class="pb-4">
                <div class="confirm_tbl">
                    <p class=" text-center confirm_txt">
                        <asp:Label ID="lblContent" runat="server" Text=""></asp:Label>
                        <br />
                        Click <a id="HyperLink1" href="http://www.unionpremierbond.unionbankofindiauk.co.uk/ECODT/Login.aspx"
                            target="_blank">here</a> to be redirected to existing customer security
                        portal Login screen.
                    </p>
                </div>
            </div>
              <%--<tr>
                <td style="height: 50px; text-align: center;" align="center">
                </td>
            </tr>--%>
            <%--<tr>
                <td valign="middle" style="text-align: center; padding: 15px; display: block;">
                    <a href="Index.aspx" class="lblalert">Back</a>
                </td>
            </tr>--%>

            <div class="mt-4">
                 <uc3:FSCS ID="FSCS" runat="server" />
                    <div class="row">
                        <uc2:footer ID="footer1" runat="server" />
                    </div>
            </div>
        </div>
    </div>
    </form>
     <script type="text/javascript" src="website/js/bootstrap.bundle.min.js"> </script>
    <script src="javascript/jquery-3.7.1.min.js" type="text/javascript"></script>
</body>
</html>
