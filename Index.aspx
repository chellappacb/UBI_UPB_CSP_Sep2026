<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<%@ Register Src="~/Header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="~/Footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="~/HeaderInclude.ascx" TagName="headerincl" TagPrefix="id" %>
<%@ Register Assembly="MacroWebControls" Namespace="MacroWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <id:headerincl ID="headerinclude" runat="server" />
    <link type="text/css" href="website/Style.css" rel="Stylesheet" />
    <link href="website/css/bootstrap.min.css" rel="stylesheet" />

    <link type="text/css" href="website/Responsive.css" rel="Stylesheet" />
    <link type="text/css" href="Website/custom-style.css" rel="Stylesheet" />
    <!-- Scroll Top -->
    <link href="website/scrolltop/scrolltopt.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="website/scrolltop/font-awesome.min.css" />
    <!-- Scroll Top -->

    <script language="javascript" type="text/javascript" src="JS/disbackbtn.js"></script>

    <link href="website/DynamicROI/ROI_Home.css" rel="stylesheet" />
</head>
<body onload="AddRequestHandler()">
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= ModalProgress.ClientID %>';
    </script>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnNewApplicant">
        <script language="javascript" type="text/javascript" src="JS/jsUpdateProgress.js"></script>
        <cc2:ToolkitScriptManager ID="ToolScriptManager1" runat="server" ScriptMode="Release">
        </cc2:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="container">
                    <div class="wrapp" border="0">
                        <uc1:header ID="header" runat="server" />

                        <div class="col-12 pl-3 pe-3">
                            <div class="row">
                                <div class="col p-0">
                                    <hr class="border-0 border-top-red-3 m-0 p-0" />
                                </div>
                                <div class="col p-0">
                                    <hr class="border-0 border-top-blue-3 m-0 p-0" />
                                </div>
                            </div>
                        </div>

                        <%--Dynamic ROI Integration --%>
                        <div class="banner-ROI">
                            <!-- Background photo -->
                            <%--<img class="banner-photo" src="Slide1.jpg" alt=""
                                onerror="this.style.cssText='position:absolute;inset:0;width:100%;height:100%;object-fit:cover;background:linear-gradient(135deg,#b8cfe8,#6a8fbc)';this.removeAttribute('src');" />--%>
                            <img class="banner-photo" src="website/DynamicROI/Slide1.jpg" alt="" />

                            <!-- Gradient overlay -->
                            <div class="banner-overlay"></div>

                            <!-- Wheel area -->
                            <div class="wheel-area">
                                <svg id="svg" viewBox="0 0 700 700" xmlns="http://www.w3.org/2000/svg">
                                    <defs>
                                        <radialGradient id="cg" cx="42%" cy="48%" r="58%">
                                            <stop offset="0%" stop-color="#7888d8" />
                                            <stop offset="55%" stop-color="#9060a8" />
                                            <stop offset="100%" stop-color="#b05272" />
                                        </radialGradient>
                                        <filter id="ws" x="-10%" y="-10%" width="120%" height="120%">
                                            <feDropShadow dx="0" dy="3" stdDeviation="8" flood-color="rgba(0,0,0,.22)" />
                                        </filter>
                                        <filter id="cs" x="-15%" y="-15%" width="130%" height="130%">
                                            <feDropShadow dx="0" dy="4" stdDeviation="10" flood-color="rgba(0,0,0,.28)" />
                                        </filter>
                                        <filter id="ns" x="-25%" y="-25%" width="150%" height="150%">
                                            <feDropShadow dx="0" dy="3" stdDeviation="6" flood-color="rgba(0,0,0,.3)" />
                                        </filter>
                                    </defs>
                                </svg>

                                <!-- Center text -->
                                <div class="center-lbl">
                                    <div class="bank-row">

                                        <div>
                                            <div class="bname">
                                                <img src="website/DynamicROI/ubi_center_w.png" class="w-100" alt="" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="bprod">Union Premier Bond</div>
                                </div>
                            </div>
                        </div>

                        <%--End New Banner--%>



                        <%-- <asp:Panel ID="pnlNewTenure" runat="server" Visible="false">
                            <div class="ImgContainer">
                                <asp:Image ID="imgBanner" runat="server" CssClass="bannerImg w-100" AlternateText="Home Banner" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlOldTenure" runat="server" Visible="false">
                            <div class="ImgContainer">
                                <img src="website/Images/Ibanner2.jpg" alt="banner" class="w-100" />
                                <div id="OneYr" runat="server" class="OneYr">
                                    0.00%
                                </div>
                                <div id="TwoYrs" runat="server" class="TwoYrs">
                                    0.00%
                                </div>
                                <div id="ThreeYrs" runat="server" class="ThreeYrs">
                                    0.00%
                                </div>
                                <div id="FourYrs" runat="server" class="FourYrs">
                                    0.00%
                                </div>
                                <div id="FiveYrs" runat="server" class="FiveYrs">
                                    0.00%
                                </div>
                            </div>
                        </asp:Panel>--%>




                        <div class="bg-white ">
                            <div class="cont_tbl">
                                <div class="col-12">
                                    <asp:Panel ID="pnlMarquee" runat="server" CssClass="d-none">
                                        <div class="bg-theme text-white">
                                            <marquee behavior="scroll" direction="left" onmouseover="this.stop();" onmouseout="this.start();"
                                                scrollamount="5" class="p-2">
                                                <span id="MarqueeText" runat="server" class="text-white"></span>
                                            </marquee>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div class="bg-light">
                            <div class="cont_tbl">
                                <div class="col-12 pl-3 pe-3">
                                    <div class="row">
                                        <div class="col-6 col-lg-6 border-right h-50">
                                        </div>
                                        <div class="col-6 col-lg-6">
                                        </div>
                                    </div>
                                </div>

                                <div class="row pb-5">
                                    <div class="col-12 col-lg-4">
                                        <div class="card border-bottom-blue-3">
                                            <img src="website/Images/newcusomers.jpg" alt="key" class="card-img-top img-fluid" />
                                            <div class="card-body">
                                                <h5 class="head_txt1 pb-3">new customer</h5>
                                                <p class="font-16 text-secondary font-weight-normal text-center lh-2 pb-3">
                                                    I do not have any type of account with Union Bank of India(UK) Ltd and would like to open a new fixed deposit account
                                                </p>

                                                <center>
                                                    <asp:Button ID="btnPrimaryAdd" runat="server" Text="go" CssClass="btn_red" Visible="true"
                                                        CausesValidation="false" OnClick="lbtnNew_Click" /></center>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-lg-4">
                                        <div class="card border-bottom-red-3">
                                            <img src="website/Images/oldcusomers.jpg" alt="key" class="card-img-top img-fluid" />
                                            <div class="card-body">
                                                <h5 class="head_txt1 pb-3">Existing Customer </h5>
                                                <p class="font-16 text-secondary font-weight-normal text-center lh-2 pb-3">
                                                    I have Union Premier Bond account with Union Bank of India(UK) Ltd and would like
                                                                to open a new fixed deposit account
                                                </p>

                                                <center>
                                                    <asp:Button ID="Button1" runat="server" Text="go" CssClass="btn_blue" Visible="true"
                                                        CausesValidation="false" OnClick="lbtnExistnCust_Click" /></center>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-lg-4">
                                        <div class="card border-bottom-blue-3">
                                            <img src="website/Images/application.jpg" alt="key" class="card-img-top img-fluid" />
                                            <div class="card-body">
                                                <h5 class="head_txt1 pb-3">Retrieve Your Application </h5>
                                                <p class="font-16 text-secondary font-weight-normal text-center lh-2 pb-3">
                                                    I already saved the application and would like to continue to open a new fixed deposit
                                                                account
                                                </p>

                                                <center>
                                                    <asp:Button ID="Button2" runat="server" Text="go" CssClass="btn_red" Visible="true"
                                                        CausesValidation="false" OnClick="lbtnRetrive_Click" /></center>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                                <asp:Button ID="btnModelPopup" runat="server" CssClass="d-none" />
                                <cc2:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnModelPopup"
                                    PopupControlID="PnlConfirmation" CancelControlID="btnOk" BackgroundCssClass="sctableBackground">
                                </cc2:ModalPopupExtender>
                                <asp:Panel ID="PnlConfirmation" runat="server" CssClass="d-none">
                                    <div class="csmain_tbl">
                                        <div class="">
                                            <div class="modal-header p-3 h-50 bg-theme">
                                                <h5 class="modal-title text-white text-center">UBI UPB - New Customer</h5>
                                                <asp:LinkButton ID="lnkClose" runat="server" Text="" CssClass="linkclose p-3" OnClick="lnkClose_Click" />
                                            </div>
                                            <div class="modal-body bg-lightblue p-3">
                                                <center>
                                                    <asp:Label ID="lblPopupAlert" runat="server" CssClass="text-danger"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="LblConfirmMsg" runat="server" CssClass="font-weight-bold font-14 text-secondary lh-2"></asp:Label>
                                                    <asp:Label ID="Label2" runat="server" CssClass="font-weight-bold font-14 text-secondary lh-2" Text="Do you want to proceed?"></asp:Label>
                                                </center>

                                                <asp:Panel ID="PnlNewcust" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="col-12 col-md-5">
                                                        </div>
                                                        <div class="col-12 col-md-7 mb-2 mt-2">
                                                            <asp:Label ID="Label1" runat="server" CssClass="text-danger"></asp:Label>

                                                            <center>
                                                                <img title="Please enter the security code as shown in the image." id="Img1" alt="Visual verification"
                                                                    src='image.aspx?r=<%= System.Guid.NewGuid().ToString("N")%>' class="captcha mt-2 mb-3" />
                                                                <asp:LinkButton ID="lnkbtnReset" runat="server" CausesValidation="false" OnClick="lnkbtnReset_Click">
                                                                    <asp:Image ID="imgFolder" runat="server" ImageUrl="~/website/images/reCaptcha-Logo.png"
                                                                        CssClass="recaptcha" />
                                                                </asp:LinkButton>
                                                            </center>
                                                        </div>
                                                    </div>

                                                    <div class="row mb-3">
                                                        <div class="col-12 col-lg-5">
                                                            <label class="form-label">Please type the Captcha text </label>
                                                        </div>
                                                        <div class="col-12 col-lg-7">
                                                            <cc1:MacroWebTextBox ID="txtCaptcha" runat="server" Validate="IsAlphaNum" MaxLength="5"
                                                                vali class="sctxtbox"></cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>

                                                    <hr class="hr mt-3 mb-3" />
                                                    <center>
                                                        <asp:Button ID="btnclose" runat="server" Text="No" CssClass="scbutton" ValidationGroup="abc"
                                                            OnClick="lnkClose_Click" />
                                                        <asp:Button ID="btnNewApplicant" runat="server" Text="Yes" CssClass="scbutton"
                                                            OnClick="btnNewApplicant_Click" />
                                                    </center>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <div class="col-12 col-lg-12">
                                    <p class="font-16 text-center text-underline text-secondary pt-2 lh-2 m-0 pb-5">
                                        The existing customer of Union Bank of India (UK) Ltd, not having login credentials
                                        kindly contact Bank on 020 733 24250 (Ext-1)
                                    </p>
                                </div>

                            </div>
                        </div>
                        <div class="bg-white p-5">
                            <h5 class="head_txt mb-3">Financial Services Compensation Scheme</h5>
                            <p class="font-16 font-weight-normal text-secondary text-center p-3 lh-2">
                                Union Bank of India (UK) Ltd has been authorized by the Prudential Regulation Authority
                            (PRA) and regulated by the Prudential Regulation Authority and Financial Conduct
                            Authority. Union Bank of India (UK) Ltd is a member of the Financial Services Compensation
                            Scheme (FSCS).
                            </p>

                            <center>
                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton1_Click">
                                <img src="./Website/Images/fscs.jpg" alt="fscs" />
                                </asp:LinkButton>
                            </center>

                            <asp:Button ID="btnMPopup" runat="server" CssClass="d-none" />
                            <cc2:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnMPopup"
                                PopupControlID="pnlfscspopup" CancelControlID="btnOk" BackgroundCssClass="sctableBackground">
                            </cc2:ModalPopupExtender>
                            <asp:Panel ID="pnlfscspopup" runat="server" CssClass="w-90 d-none">
                                <div class="modalpdiv">
                                    <div class="modalpdiv1 p-3">

                                        <h6></h6>
                                        <asp:LinkButton ID="LinkButton2" Text="" runat="server" CssClass="linkclose1 float-end" OnClick="popupclose_Click" />

                                        <img src="website/Images/logo.png" alt="logo" class="ml-2" />
                                        <h5 class="font-22 text-secondary p-3 pb-3 font-weight-bold">Protecting Your Money</h5>

                                        <p class="text-secondary p-2 text-justify lh-1-5">
                                            Your Eligible deposits with Union Bank Of India (UK) Ltd are protected up to the
                                            FSCS compensation limit by the Financial Services Compensation Scheme, the UK's
                                            deposit protection scheme. Most deposits are covered by the scheme. This limit is
                                            applied to the total of any deposits you have with Union Bank of India (UK) Ltd.
                                            Any deposits you hold above the FSCS compensation limit are unlikely to be covered,
                                            unless under specific circumstances, as determined by the FSCS.
                                            <br />
                                            <br />
                                            For more information on the scheme, Please select the links below to view the FSCS
                                            poster and leaflet as well as the information Sheet and Exclusions List or visit
                                            the FSCS website at <a href="http://www.fscs.org.uk/" target="_blank" rel="nofollow" class="text-theme-blue txt-decoration-none ">www.fscs.org.uk</a>
                                        </p>

                                        <div class="row">
                                            <div class="col-12 col-lg-6">
                                                <center>
                                                    <a href="website/Images/FSCS_Information_2025.pdf" target="_blank" class="txt-decoration-none font-14"
                                                        title="FSCS Information (PDF). This link will open in a new browser window.">
                                                        <img src="website/Images/protecting_money.png" alt="" class="mb-3" />
                                                        <br />
                                                        <font class="pt-2 text-theme">FSCS Information</font> </a>
                                                </center>
                                            </div>
                                            <div class="col-12 col-lg-6">
                                                <center>
                                                    <a href="website/Images/Leaflet.pdf" target="_blank" class="text-decoration-none font-14"
                                                        title="FSCS Leaflet (PDF). This link will open in a new browser window.">
                                                        <img src="website/Images/FSCS_thumb.JPG" alt="" class="mb-3" />
                                                        <br />
                                                        <font class="pt-2 text-theme">FSCS Leaflet</font> </a>
                                                </center>
                                            </div>
                                        </div>



                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                        <div class="bg-lightblue p-5">
                            <h5 class="head_txt_white mb-3">Contact Us</h5>
                            <div class="contact_tbl_main">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <p>
                                            <img src="website/Images/phone1.png" alt="phone" />
                                            +44 20 7332 4250 (Ext-1)
                                        </p>
                                        <p>
                                            <img src="website/Images/mail1.png" alt="mail" />
                                            premierbond@unionbankofindiauk.co.uk
                                        </p>
                                    </div>
                                    <div class="col-12 col-lg-6 d-flex justify-content-end">
                                        <p class="lh-2 font-weight-normal">
                                            LONDON CORPORATE OFFICE :<br />
                                            12 Arthur Street,<br />
                                            London EC4R 9AB.
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <uc2:footer ID="footer" runat="server" />

                    </div>
                </div>

            </ContentTemplate>
            <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>--%>
        </asp:UpdatePanel>

        <%-- <cc2:ModalPopupExtender runat="server" PopupControlID="PanLoading" ID="ModalProgress"
        TargetControlID="PanLoading" BackgroundCssClass="modalBackground">
    </cc2:ModalPopupExtender>
    <asp:Panel ID="PanLoading" runat="server" CssClass="scupdateProgress">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="5">
           
            <ProgressTemplate>
                <table class="table" >
                    <tr>
                        <td>
                            <img src="images/ajax-loader.gif" alt="loading" title="loading" />
                        </td>
                    </tr>
                    <tr>
                        <td class="bodytext text-center" >
                            Processing your request. Please wait ...
                        </td>
                    </tr>
                </table>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>--%>

        <div class="modal test d-none sctableBackground" id="ModalProgress" runat="server">
            <div class="modal-dialog  vh-center">
                <div class="modal-content">
                    <!-- Modal body -->
                    <div class="modal-body">
                        <asp:Panel ID="PanLoading" runat="server" CssClass="">
                            <div class=" m-auto position-relative">
                                <p class="bodytext text-center">Processing your request. Please wait ... </p>
                                <img src="images/ajax-loader.gif" class="d-block m-auto" alt="loading" title="loading" />
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>




    </form>
    <!-- Scroll Top -->
    <script src="website/js/bootstrap.bundle.min.js"></script>
    <script src="javascript/jquery-3.7.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="website/scrolltop/script.js"></script>
    <script type="text/javascript">
        (function ($) {

            $.scrolltop({
                template: '<i class="fa fa-chevron-up"></i>',
                class: 'custom-scrolltop'
            });

        })(jQuery);
    </script>
    <!-- Scroll Top -->
    <script type="text/javascript" src="JS/CSP/OnClickHandlers.js"></script>
    <script type="text/javascript" src="JS/CSP/Home.js" nonce="ihYxAijSER-YFSUxCDJbag"></script>

    <script type="text/javascript">
        var segs = <%= SegsJson %>;
    </script>
    <script type="text/javascript" src="website/DynamicROI/ROI_Home.js"></script>
</body>
</html>
