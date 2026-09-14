<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Confirmation.aspx.cs" Inherits="Confirmation" %>

<%@ Register Src="header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="~/HeaderInclude.ascx" TagName="headerincl" TagPrefix="id" %>
<%@ Register Src="FSCS.ascx" TagName="FSCS" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <id:headerincl ID="headerinclude" runat="server" />
    <link rel="Stylesheet" type="text/css" href="css/cReponsive.css" />
    <link type="text/css" href="website/Style.css" rel="Stylesheet" />
    <link type="text/css" href="website/Responsive.css" rel="Stylesheet" />
    <link rel="Stylesheet" type="text/css" href="css/styles.css" />

    <link href="website/css/bootstrap.min.css" rel="stylesheet" />
    <link type="text/css" href="Website/custom-style.css" rel="Stylesheet" />

    <script language="javascript" type="text/javascript" src="JS/disbackbtn.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="wrapp" border="0">
                <uc1:header ID="header1" runat="server" />

                <div class="row">
                    <div class="bg-theme text-right p-3 pr-2 d-flex justify-content-end">
                        <a href="home.aspx" class="text-white text-decoration-underline">HOME</a>
                    </div>
                    <div class="bg-light text-secondary p-3 pl-2">
                        <asp:Label ID="lblProdName" runat="server" Text="" CssClass="font-18 text-secondary"> Confirmation </asp:Label>
                    </div>
                </div>

                 <div class="confirm_tbl mt-5 mb-5">
                <asp:Panel ID="pnlSave" runat="server" Visible="true">
                    <div class="confirm_txt">
                        <p class=" text-center">
                            You have successfully saved your application please complete your application and resubmit.
                            <br />
                            Please retrieve and complete your application as early as possible.
                            <br />
                           Your reference number for retrieval purpose is
                <asp:Label ID="lblRefNo" runat="server" CssClass="font-16" Text=""></asp:Label>  <br />
                        </p>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlKYCPending" runat="server" Visible="true">
                    <div  class="confirm_txt">
                        <p class="text-center">
                            Your unique application reference no is
                <asp:Label ID="lblKYCPending" runat="server" Text=""></asp:Label>
                            <br />

                            <asp:Label ID="lblKYCPendingMessage" runat="server" Text=""></asp:Label>
                        </p>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlKYCSuccess" runat="server" Visible="true">
                    <div class="confirm_txt ">
                        <p class="text-center">
                            Your unique application reference no is
                    <asp:Label ID="lblKYCSuccess" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:Label ID="lblKYCSuccessMessage" runat="server" Text=""></asp:Label>
                        </p>
                    </div>
                </asp:Panel>
              </div>

                <div class="mt-5 row">
                    <uc3:FSCS ID="FSCS" runat="server" />

                    <uc2:footer ID="footer1" runat="server" />

                </div>

                <%--<tr>
                <td valign="middle" height="150" align="center">
                    <a href="Index.aspx" class="lblalert" style="font-size: small; font-weight: bold;">
                        Back 
                    </a>
                </td>
            </tr>--%>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="website/js/bootstrap.bundle.min.js"> </script>
    <script src="javascript/jquery-3.7.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="JS/CSP/OnClickHandlers.js"></script>
    <script type="text/javascript" src="JS/CSP/Home.js" nonce="ihYxAijSER-YFSUxCDJbag"></script>
</body>
</html>
