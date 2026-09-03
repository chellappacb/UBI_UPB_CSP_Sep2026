<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<%@ Register Src="~/Header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="~/Footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="~/HeaderInclude.ascx" TagName="headerincl" TagPrefix="id" %>
<%@ Register Assembly="MacroWebControls" Namespace="MacroWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <id:headerincl ID="headerinclude" runat="server" />
    <link type="text/css" href="Website/Style.css" rel="Stylesheet" />

    <link href="website/css/bootstrap.min.css" rel="stylesheet" />

    <link type="text/css" href="Website/Responsive.css" rel="Stylesheet" />
    <link type="text/css" href="Website/custom-style.css" rel="Stylesheet" />

    <!-- Scroll Top -->
    <link href="website/scrolltop/scrolltopt.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="website/scrolltop/font-awesome.min.css" />
    <!-- Scroll Top -->

    <link href="website/DynamicROI/ROI_Home.css" rel="stylesheet" />

</head>
<body onload="AddRequestHandler()">
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= ModalProgress.ClientID %>';
    </script>
    <form id="form1" runat="server" autocomplete="off">
        <asp:Panel ID="cookie_panel" Visible="true" runat="server">
            <div class="container" runat="server" id="Div1">
                <div class="alert alert-cookie alert-dismissible" role="alert">
                    <div class="inner-alert font-15 text-justify text-dark">
                        <button type="button" class="close" onclick="callCloseEvent();">
                            <span aria-hidden="true">×</span></button>
                        <p class="text-start text-dark">
                            We use cookies to help provide you with the best possible online experience. By
                    using this site, you agree that we may store and access cookies on your device.
                    You can find out more by viewing the 'Cookies and IP Addresses' section of our <a class="text-theme"
                        href="https://www.unionbankofindiauk.co.uk/privacy-policy" target="_blank">Privacy Policy</a>.
                        </p>
                    </div>
                </div>
            </div>
            <!-- alert container ends -->
        </asp:Panel>
        <script language="javascript" type="text/javascript" src="JS/jsUpdateProgress.js"></script>
        <cc2:ToolkitScriptManager ID="ToolScriptManager1" runat="server" ScriptMode="Release">
        </cc2:ToolkitScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>
                <div class="container">
                    <div class="wrapp" border="0">
                        <uc1:header ID="header" runat="server" />


                        <%--Dynamic ROI Integration --%>
                        <div class="banner-ROI">
                            <!-- Background photo -->
                            <%--<img class="banner-photo" src="Slide1.jpg" alt=""
                                onerror="this.style.cssText='position:absolute;inset:0;width:100%;height:100%;object-fit:cover;background:linear-gradient(135deg,#b8cfe8,#6a8fbc)';this.removeAttribute('src');" />--%>
                            <img class="banner-photo" src="website/DynamicROI/Slide2.jpg" alt="" />

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



                        <%--<asp:Panel ID="pnlNewTenure" runat="server" Visible="false">
                            <div class="bannerHome">
                                <asp:Image ID="imgBanner" runat="server" CssClass="bannerImg" AlternateText="Home Banner" />
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlOldTenure" runat="server" Visible="false">
                            <div class="banner1"></div>
                            <div class="row int_tbl">
                                <div class="col int_txt align-self-center">
                                    <span class="year_txt">1 year</span>
                                    <asp:Label ID="lblfrstyr" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="col int_txt align-self-center">
                                    <span class="year_txt">2 years</span>
                                    <asp:Label ID="lblSecyr" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="col int_txt align-self-center">
                                    <span class="year_txt">3 years</span>
                                    <asp:Label ID="lblThirdyr" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="col int_txt align-self-center">
                                    <span class="year_txt">4 years</span>
                                    <asp:Label ID="lblFothyr" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="col int_txt align-self-center border-right-0">
                                    <span class="year_txt">5 years</span>
                                    <asp:Label ID="lblFifthyr" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                        </asp:Panel>--%>

                        <div class="bg-light">
                            <div class="cont_tbl">
                                <div class="row">
                                    <div class="col-6 col-lg-6 border-right h-50">
                                    </div>
                                    <div class="col-6 col-lg-6">
                                    </div>
                                </div>
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
                                <div class=" bg-white p-5">
                                    <div class="head_txt mb-3">
                                        <asp:Label ID="lblProdName" runat="server" Text=""></asp:Label>
                                    </div>
                                    <center>
                                        <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="Index.aspx" CssClass="mb-3">
                                                    <img src="./Website/Images/btn_apply.png" alt="btn"/>
                                        </asp:LinkButton>
                                    </center>
                                    <h6 class="text-center pt-4 font-weight-bold text-danger">Applicable only for Individuals & in Sterling pounds. </h6>
                                </div>


                                <div class="row">
                                    <div class="col-6 col-lg-6 border-right h-50">
                                    </div>
                                    <div class="col-6 col-lg-6">
                                    </div>
                                </div>
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
                                <div class="bg-white p-5">
                                    <div class="row">
                                        <div class="col-12">
                                            <h5 class="head_txt">Key Features</h5>
                                        </div>
                                        <div class="col-12 col-lg-6">
                                            <ul class="font-16 text-secondary text-justify lh-2 p-2 d-block">
                                                <li>Invest from £<span id="spMinAmt" runat="server"></span> up to £<span id="spMaxAmt" runat="server"></span>.</li>
                                                <li>Open Multiple Bonds and Get certificates Online. </li>
                                                <li>Maximum of 4 Applicants are permitted per bond. </li>
                                                <li>No premature closure or partial withdrawal allowed during the period.</li>
                                                <li>View Balance Online. </li>
                                                <li>No Paper work. </li>
                                                <li>Online option for Auto renewal, investment or withdrawal of funds on Maturity</li>
                                                <li>Bonus of 0.10% above the current interest rate for renewals of one year or above.</li>
                                                <li>Save option or retrieve option to fill application in stages. </li>
                                                <li>Interest rate is fixed for the term of deposit. </li>
                                                <li>Before maturity of bond, we will notify you on your registered Email ID.</li>
                                                <li>UK based customer support</li>
                                                <%--<li><a href="https://www.unionbankofindiauk.co.uk/Portals/0/pdf/rates/Summary_Box_Fixed_rate_GBP_deposit_GBP1_15_08_2017.pdf"
                                                target="_blank" style="color: #666666; border: none;">Summary Box</a> </li>--%>
                                            </ul>

                                        </div>
                                        <div class="col-12 col-lg-6 align-self-center">
                                            <img src="website/Images/keyfeatures.jpg" alt="newcust" class="img-fluid" />
                                        </div>
                                        <div class="col-12">
                                            <ul class="font-16 text-danger text-justify lh-2 p-2 d-block">
                                                <li>Interest paid on maturity. </li>
                                                <li>Interest is calculated as Simple Interest.</li>
                                                <li>On Maturity of your bond, your funds would be transferred to same account from where
                                                they were originally received.</li>
                                                <li>You can send us funds only via Faster Payment System / CHAPS.</li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-6 col-lg-6 border-right h-50">
                                    </div>
                                    <div class="col-6 col-lg-6">
                                    </div>
                                </div>
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
                                <div class="bg-white p-5">
                                    <div class="row">
                                        <div class="col-12">
                                            <h5 class="head_txt mb-4">To Open Union Premier Bond you must</h5>
                                        </div>
                                        <div class="col-12 col-lg-6">
                                            <img src="website/Images/onlinebond.jpg" alt="newcust" class="img-fluid" />
                                        </div>
                                        <div class="col-12 col-lg-6 align-self-center">
                                            <ul class="font-16 text-secondary text-justify lh-2 p-2" <%--style=" list-style-position: inside;"--%>>
                                                <li>be Individuals of UK resident aged 18years or above.</li>
                                                <li>have Valid Email ID.</li>
                                                <li>provide a UK based Account number with sort code of PRA regulated UK bank or Building
                                            society current account held in your name .</li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>

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
                                <div class="bg-white p-5">
                                    <h5 class="head_txt mb-3">Privacy Policy</h5>
                                    <center><a target="_blank" class="text-underline font-16 text-secondary text-center" href="https://www.unionbankofindiauk.co.uk/privacy-policy">Please click here to know more about privacy policy</a> </center>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-6 col-lg-6 border-right h-50">
                                </div>
                                <div class="col-6 col-lg-6">
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
                                            <asp:LinkButton ID="popupclose" Text="" runat="server" CssClass="linkclose1 float-end" OnClick="popupclose_Click" />

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
                                                        <a href="website/Images/Leaflet.pdf" target="_blank" class="text-theme text-decoration-none font-14"
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
                        </div>

                        <uc2:footer ID="footer" runat="server" />
                    </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>

        <%--<cc2:ModalPopupExtender runat="server" PopupControlID="PanLoading" ID="ModalProgress"
            TargetControlID="PanLoading" BackgroundCssClass="modalBackground">
        </cc2:ModalPopupExtender>
        <asp:Panel ID="PanLoading" runat="server" CssClass="scupdateProgress">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="5">                 
                <ProgressTemplate>
                    <div class=" m-auto position-relative">
                           
                        <p class="bodytext text-center"> Processing your request. Please wait ... </p>
                        <img src="images/ajax-loader.gif" class="d-block m-auto" alt="loading" title="loading" />
                    </div>                     
                </ProgressTemplate>
            </asp:UpdateProgress>
        </asp:Panel>  --%>

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

    <script src="website/js/bootstrap.bundle.min.js" nonce="ihYxAijSER-YFSUxCDJbag"> </script>
    <script src="javascript/jquery-3.7.1.min.js" type="text/javascript" nonce="ihYxAijSER-YFSUxCDJbag"></script>
    <script src="JS/app.js" type="text/javascript" nonce="ihYxAijSER-YFSUxCDJbag"></script>
    <link href="css/cookies.css" rel="stylesheet" type="text/css" />

    <!-- Scroll Top -->
    <script type="text/javascript" src="website/scrolltop/script.js" nonce="ihYxAijSER-YFSUxCDJbag"></script>
    <script type="text/javascript" nonce="ihYxAijSER-YFSUxCDJbag">
        (function ($) {

            $.scrolltop({
                template: '<i class="fa fa-chevron-up"></i>',
                class: 'custom-scrolltop'
            });

        })(jQuery);
    </script>
    <!-- Scroll Top -->
    <script type="text/javascript" src="JS/CSP/Home.js" nonce="ihYxAijSER-YFSUxCDJbag"></script>
    <script type="text/javascript">
        function callCloseEvent() {
            $.ajax({
                type: "POST",
                url: "Home.aspx/SetCookieClose",
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        }

    </script>

    <script type="text/javascript">
        var segs = <%= SegsJson %>;
    </script>
    <script type="text/javascript" src="website/DynamicROI/ROI_Home.js"></script>

</body>
</html>
