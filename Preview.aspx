<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Preview.aspx.cs" Inherits="Preview" %>

<%@ Register Src="header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="~/HeaderInclude.ascx" TagName="headerincl" TagPrefix="id" %>
<%@ Register Assembly="MacroWebControls" Namespace="MacroWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="FSCS.ascx" TagName="FSCS" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <id:headerincl ID="headerinclude" runat="server" />
    <link rel="Stylesheet" type="text/css" href="css/cReponsive.css" />
    <link type="text/css" href="website/Style.css" rel="Stylesheet" />
    <link type="text/css" href="website/Responsive.css" rel="Stylesheet" />
    <link rel="Stylesheet" type="text/css" href="css/styles.css" />
    <link rel="Stylesheet" type="text/css" href="css/imghoverStyle.css" />

    <link type="text/css" href="Website/custom-style.css" rel="Stylesheet" />
    <link type="text/css" href="css/cReponsive.css" rel="Stylesheet" />

    <link href="website/css/bootstrap.min.css" rel="stylesheet" />

    <script language="javascript" type="text/javascript" src="JS/disbackbtn.js"></script>
    <script language="javascript" type="text/javascript">

        function show_progress() {
            document.getElementById('lblAlert').innerHTML = '';
            document.getElementById('lblRefNo').innerHTML = '';

        }

        /*
        function Check() {
        var chkPassport = document.getElementById("chkTermsAndConditions");
        if (chkPassport.checked) {
        } else {
        alert("Please tick the terms & conditions for the further procedings");
        return false;
        }
        }
        */
    </script>
    <script type="text/javascript">
        function showPopup() {
            $find("mpe").show();
            return false;
        }
    </script>
    <script type="text/javascript">

        function Base64() {
            //alert(document.getElementById("txtPassword").value)

            var KeyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/="

            var output = "";
            var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
            var i = 0;

            var string = document.getElementById("txtPassword").value

            string = string.replace(/\r\n/g, "\n");
            var utftext = "";
            for (var n = 0; n < string.length; n++) {
                var c = string.charCodeAt(n);
                if (c < 128) {
                    utftext += String.fromCharCode(c);
                }
                else if ((c > 127) && (c < 2048)) {
                    utftext += String.fromCharCode((c >> 6) | 192);
                    utftext += String.fromCharCode((c & 63) | 128);
                }
                else {
                    utftext += String.fromCharCode((c >> 12) | 224);
                    utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                    utftext += String.fromCharCode((c & 63) | 128);
                }
            }

            var input = utftext;

            while (i < input.length) {
                chr1 = input.charCodeAt(i++);
                chr2 = input.charCodeAt(i++);
                chr3 = input.charCodeAt(i++);
                enc1 = chr1 >> 2;
                enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
                enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
                enc4 = chr3 & 63;
                if (isNaN(chr2)) {
                    enc3 = enc4 = 64;
                } else if (isNaN(chr3)) {
                    enc4 = 64;
                }

                output = output + KeyStr.charAt(enc1) + KeyStr.charAt(enc2) + KeyStr.charAt(enc3) + KeyStr.charAt(enc4)
            }
            //alert(output)
            document.getElementById("txtPassword").value = output
        }
    </script>
    <script type="text/javascript">
        function validate(key) {
            var keycode = (key.which) ? key.which : key.keyCode;
            var pwd = document.getElementById("txtPassword");
            //allow tab,backspace,delete key
            if (keycode == 9 || keycode == 8 || keycode == 46) {
                return true;
            }

            if (pwd.value.length < 8) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <script src="javascript/JSValidation.js" type="text/javascript"></script>

    <script type="text/javascript" src="javascript/app.js"></script>

    <script type="text/javascript" src="website/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript" src="website/js/popper-min.js"></script>

    <script src="javascript/jquery-3.7.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" nonce="ihYxAijSER-YFSUxCDJbag">
        $(document).ready(function () {
            // Define the class name
            var className = "accordionContent";

            // Find all div elements with the specified class name
            $("div." + className).each(function () {
                // Remove specific styles
                $(this).css({
                    'display': '',
                });
            });
        });
    </script>

</head>
<body onload="AddRequestHandler()">
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= ModalProgress.ClientID %>';
    </script>
    <form id="form1" runat="server" autocomplete="off">
        <script language="javascript" type="text/javascript" src="JS/jsUpdateProgress.js"></script>
        <div>
            <ajaxToolkit:ToolkitScriptManager ID="ToolScriptManager1" runat="server">
            </ajaxToolkit:ToolkitScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="container">
                        <div class="wrapp" border="0">
                            <uc1:header ID="header1" runat="server" />
                            <hr class="hr" />
                            <div class="px-2 py-2">
                                <p class="lbltxt1">
                                    Note: Session will expire if system remains inactive for 20 minutes
                                </p>
                            </div>

                            <div class="row">
                                <div class="d-flex justify-content-end">
                                    <asp:LinkButton ID="lblExit" runat="server" Text="Exit" CssClass="text-danger text-decoration-none"
                                        CausesValidation="false" OnClientClick="return confirm('Your keyed in data will be lost. Do you really want to exit from the application ?');"
                                        OnClick="lblExit_Click"> </asp:LinkButton>
                                </div>

                                <div class="border-red px-0">
                                    <img src="images/step/menu3.png" alt="" class="img-fluid hide" />
                                </div>
                                <p class="display">
                                    Step4
                                </p>
                                <div class="faq-title p-2 ps-4">
                                    Preview
                                </div>
                            </div>
                            <asp:Label ID="lblAlert" runat="server" Text="" CssClass="text-danger font-weight-bold"></asp:Label>

                            <div class="row mt-3 mb-5">
                                <div class="col-lg-6 col-12">
                                    <ajaxToolkit:Accordion ID="acrDynamic" runat="server" ContentCssClass="accordionContent"
                                        HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected">
                                    </ajaxToolkit:Accordion>
                                </div>
                                <div class="col-lg-6 col-12">
                                    <div class="border-blue">
                                        <h5 class="Dtit"><span class="DOrangehead">Bond Details</span></h5>
                                        <div class="card card-body">
                                            <div class=" row">
                                                <div class="col-lg-6 col-12">
                                                    <label class="form-label">Investment Amount :</label>
                                                </div>
                                                <div class="col-lg-6 col-12">
                                                    <asp:Label ID="lblInvAmount" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>

                                            <div class=" row">
                                                <div class="col-lg-6 col-12">
                                                    <label class="form-label">Investment Period :</label>
                                                </div>
                                                <div class="col-lg-6 col-12">
                                                    <asp:Label ID="lblInvPeriod" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>

                                            <div class=" row">
                                                <div class="col-lg-6 col-12">
                                                    <label class="form-label">Rate of Interest :</label>
                                                </div>
                                                <div class="col-lg-6 col-12">
                                                    <asp:Label ID="lblInvRatOfInt" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>

                                            <div class=" row">
                                                <div class="col-lg-6 col-12">
                                                    <label class="form-label">Maturity Amount<sup>*</sup> :</label>
                                                </div>
                                                <div class="col-lg-6 col-12">
                                                    <asp:Label ID="lblMatAmt" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>

                                            <div class=" row">
                                                <div class="col-lg-12 col-12">
                                                    <label class="form-label font-10">* - Value may be differ based on the interest rate while opening the bond</label>
                                                </div>

                                            </div>
                                            <hr class="hr" />

                                            <div class=" row">
                                                <div class="col-lg-6 col-12">
                                                    <label class="form-label">Bank Name :</label>
                                                </div>
                                                <div class="col-lg-6 col-12">
                                                    <asp:Label ID="lblBankName" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>

                                            <div class=" row">
                                                <div class="col-lg-6 col-12">
                                                    <label class="form-label">Sortcode :</label>
                                                </div>
                                                <div class="col-lg-6 col-12">
                                                    <asp:Label ID="lblBankSC" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>

                                            <div class=" row">
                                                <div class="col-lg-6 col-12">
                                                    <label class="form-label">Account No. :</label>
                                                </div>
                                                <div class="col-lg-6 col-12">
                                                    <asp:Label ID="lblBankAcNo" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>

                                            <hr class="hr" />

                                            <div class=" row">
                                                <div class="col-lg-6 col-12">
                                                    <label class="form-label">Repayment :</label>
                                                </div>
                                                <div class="col-lg-6 col-12">
                                                    <asp:Label ID="lblRepayment" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>


                                            <center>
                                                <asp:Label ID="lblEValErrMsg" runat="server" Text="" CssClass="text-danger font-weight-bold"></asp:Label>
                                            </center>
                                        </div>


                                    </div>
                                </div>
                            </div>
                            <div class="d-flex justify-content-end lh-2 mb-3">
                                <asp:Button ID="btnAcDtlb" runat="server" Text="Go to step 1" CssClass="button mr-2" OnClick="btnAcDtlb_Click" />
                                <asp:Button ID="btnOtrDtl" runat="server" Text="Go to step 3" CssClass="button mr-2" OnClick="btnOtrDtl_Click" />
                                <asp:Button ID="btnSave" runat="server" Text="Save & Return Later" CssClass="button mr-2"
                                    OnClick="btnSave_Click" OnClientClick="return Check()" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit Application" CssClass="button mr-2"
                                    OnClientClick="return Check()" OnClick="btnSubmit_Click" />
                            </div>

                            <div class="row">
                                <uc3:FSCS ID="FSCS" runat="server" />
                                <uc2:footer ID="footer1" runat="server" />
                            </div>
                        </div>
                    </div>
                    <table class="container">
                        <tr>
                            <td>
                                <asp:Button ID="btnModPopupEx2" runat="server" CssClass="d-none" />
                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnModPopupEx2"
                                    PopupControlID="PnlPwdReg" BackgroundCssClass="sctableBackground">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="PnlPwdReg" runat="server" CssClass="d-none" DefaultButton="btnSubmitEmail">
                                    <div class="w-50 m-auto mt-5 pt-3">
                                        <div class="modal-header bg-theme p-2 justify-content-center">
                                            <h6 class="text-center text-white m-0">Application Saved</h6>
                                        </div>
                                        <div class="modal-body bg-lightblue p-3">
                                            <p class="mb-3">
                                                Your application has been saved successfully. Please complete and submit it at your earliest convenience to prevent the loss of the entered AOF details. Your unique application reference number, <span class="fw-bold text-primary">
                                                    <asp:Label ID="lblRefNoVal" runat="server" /></span>, has been sent to your verified email address along with instructions for accessing your application.
                                            </p>
                                            <p class="mb-0">
                                                To access your saved application, enter this reference number on the
                                                login screen under the 'Retrieve Your Application'
                                                section on the website homepage and log in using the OTP sent to your
                                                verified email address.
                                            </p>
                                        </div>

                                        <div class="modal-footer justify-content-center bg-lightblue py-2  border-top">
                                            <asp:Button ID="btnSubmitEmail" runat="server" Text="OK" CssClass="scbutton"
                                                ValidationGroup="RegEmail" OnClick="btnSubmitEmail_Click" CausesValidation="true" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnModPopupSub" runat="server" CssClass="d-none" />
                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtSub" runat="server" TargetControlID="btnModPopupSub"
                                    PopupControlID="PnlSub" BackgroundCssClass="sctableBackground">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="PnlSub" runat="server" CssClass="d-none" DefaultButton="btnSubmitPre">
                                    <div class="w-50 m-auto mt-5 pt-3">
                                        <div class="modal-header bg-theme p-2 justify-content-center">
                                            <h6 class="text-center text-white m-0">Application Submitted</h6>
                                        </div>
                                        <div class="modal-body bg-lightblue p-3">
                                            <p class="mb-3">
                                                Your Union Premier Bond application has been submitted successfully. Your unique application reference number, <span class="fw-bold text-primary">
                                                    <asp:Label ID="lblSubRefNo" runat="server" /></span>, has been sent to your verified email address along with further instructions regarding the next steps.
                                            </p>
                                            <p class="mb-0">
                                                Should you require any assistance, please do not hesitate to contact us at 0207 332 4250, Monday to Friday between 9am and 5pm (excluding bank holidays), or email us at premierbond@unionbankofindiauk.co.uk.
                                            </p>
                                        </div>

                                        <div class="modal-footer justify-content-center bg-lightblue py-2  border-top">
                                            <asp:Button ID="btnSubmitPre" runat="server" Text="OK" CssClass="scbutton"
                                                ValidationGroup="RegEmail" OnClick="btnSubmitEmail_Click" CausesValidation="true" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnModFB" runat="server" CssClass="d-none" />
                                <ajaxToolkit:ModalPopupExtender ID="ModalPopFeedBck" runat="server" TargetControlID="btnModFB"
                                    PopupControlID="PnlFB" BackgroundCssClass="sctableBackground">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="PnlFB" runat="server" CssClass="d-none" DefaultButton="btnSubmitEmail">
                                    <table class="csmain_tbl w-75 table m-auto border-light borderless">
                                        <tr>
                                            <td align="center">
                                                <table class="table bg-theme text-white border-light borderless">
                                                    <tr>
                                                        <td class="text-center"></td>
                                                        <td class="text-center">Please tell us what you think about UPB!
                                                        </td>
                                                        <td class="pr-2" align="right">
                                                            <asp:LinkButton ID="lnkFBClose" runat="server" Text="" CssClass="linkclose" OnClick="lnkFBClose_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" class="p-2 d-block">
                                                <asp:Label ID="lblFBAlert" runat="server" CssClass="text-danger"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="pb-2">
                                                <table class="table border-light borderless">
                                                    <tr>
                                                        <td>
                                                            <table class="table border-light borderless">
                                                                <tr>
                                                                    <td class="w-50"></td>
                                                                    <td class="w-50">
                                                                        <table class="table border-light borderless">
                                                                            <tr>
                                                                                <td class="w-20">
                                                                                    <div class="d-grid">
                                                                                        <div>
                                                                                            <center>
                                                                                                <img src="images/fb0.jpg" alt="img1" class="img-fluid" />
                                                                                            </center>
                                                                                        </div>
                                                                                        <div class="text-center font-12 font-weight-bold pt-1">
                                                                                            Outstanding
                                                                                        </div>
                                                                                    </div>
                                                                                </td>
                                                                                <td class="w-20">
                                                                                    <div class="d-grid">
                                                                                        <div>
                                                                                            <center>
                                                                                                <img src="images/fb1.jpg" alt="img1" class="img-fluid" />
                                                                                            </center>
                                                                                        </div>
                                                                                        <div class="text-center font-12 font-weight-bold pt-1">
                                                                                            Excellent
                                                                                        </div>
                                                                                    </div>
                                                                                </td>
                                                                                <td class="w-20">
                                                                                    <div class="d-grid">
                                                                                        <div>
                                                                                            <center>
                                                                                                <img src="images/fb2.jpg" alt="img1" class="img-fluid" />
                                                                                            </center>
                                                                                        </div>
                                                                                        <div class="text-center font-12 font-weight-bold pt-1">
                                                                                            Good
                                                                                        </div>
                                                                                    </div>
                                                                                </td>
                                                                                <td class="w-20">
                                                                                    <div class="d-grid">
                                                                                        <div>
                                                                                            <center>
                                                                                                <img src="images/fb3.jpg" alt="img1" class="img-fluid" />
                                                                                            </center>
                                                                                        </div>
                                                                                        <div class="text-center font-12 font-weight-bold pt-1">
                                                                                            Satisfactory
                                                                                        </div>
                                                                                    </div>
                                                                                </td>
                                                                                <td class="w-20">
                                                                                    <div class="d-grid">
                                                                                        <div>
                                                                                            <center>
                                                                                                <img src="images/fb4.jpg" alt="img1" class="img-fluid" />
                                                                                            </center>
                                                                                        </div>
                                                                                        <div class="text-center font-12 font-weight-bold pt-1">
                                                                                            Poor
                                                                                        </div>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="w-50">How useful do you find our Union Premier Bond website.
                                                                    </td>
                                                                    <td class="w-50 pt-1">
                                                                        <asp:RadioButtonList ID="rblWeb" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Text="" Value="Outstanding"></asp:ListItem>
                                                                            <asp:ListItem Text="" Value="Excellent"></asp:ListItem>
                                                                            <asp:ListItem Text="" Value="Good"></asp:ListItem>
                                                                            <asp:ListItem Text="" Value="Satisfactory"></asp:ListItem>
                                                                            <asp:ListItem Text="" Value="Poor"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="w-50">How easy was it to fill the Union Premier Bond application form.
                                                                    </td>
                                                                    <td class="w-50 pt-1">
                                                                        <asp:RadioButtonList ID="rblApp" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Text="" Value="Outstanding"></asp:ListItem>
                                                                            <asp:ListItem Text="" Value="Excellent"></asp:ListItem>
                                                                            <asp:ListItem Text="" Value="Good"></asp:ListItem>
                                                                            <asp:ListItem Text="" Value="Satisfactory"></asp:ListItem>
                                                                            <asp:ListItem Text="" Value="Poor"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="w-50">How good are our rate of interests compared to other UK banks.
                                                                    </td>
                                                                    <td class="w-50 pt-1">
                                                                        <asp:RadioButtonList ID="rblIntRat" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Text="" Value="Outstanding"></asp:ListItem>
                                                                            <asp:ListItem Text="" Value="Excellent"></asp:ListItem>
                                                                            <asp:ListItem Text="" Value="Good"></asp:ListItem>
                                                                            <asp:ListItem Text="" Value="Satisfactory"></asp:ListItem>
                                                                            <asp:ListItem Text="" Value="Poor"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="w-50">How would you rate our overall performance.
                                                                    </td>
                                                                    <td class="w-50 pt-1">
                                                                        <asp:RadioButtonList ID="rblAllPer" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Text="" Value="Outstanding"></asp:ListItem>
                                                                            <asp:ListItem Text="" Value="Excellent"></asp:ListItem>
                                                                            <asp:ListItem Text="" Value="Good"></asp:ListItem>
                                                                            <asp:ListItem Text="" Value="Satisfactory"></asp:ListItem>
                                                                            <asp:ListItem Text="" Value="Poor"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" class="p-2 border-top border-light">
                                                <asp:Button ID="btnFB" runat="server" Text="Submit" CssClass="scbutton" ValidationGroup="abc"
                                                    CausesValidation="true" OnClick="btnFB_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Maybe Later" CssClass="scbutton"
                                                    OnClick="btnCancel_Click" CausesValidation="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <%-- <ajaxToolkit:ModalPopupExtender runat="server" PopupControlID="PanLoading" ID="ModalProgress"
        TargetControlID="PanLoading" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanLoading" runat="server" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="5">
            <ProgressTemplate>
                <table style="position: relative; top: 7%; margin: 0 auto; left: 0px; height: 100%;
                    width: 260px;">
                    <tr>
                        <td>
                            <img src="images/ajax-loader.gif" alt="loading" title="loading" style="margin: 0 auto;
                                display: block;" />
                        </td>
                    </tr>
                    <tr>
                        <td class="bodytext" style="text-align: center;">
                            Processing your request. Please wait ...
                        </td>
                    </tr>
                </table>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>--%>

        <div class="modal test d-none" id="ModalProgress" runat="server">
            <div class="modal-dialog top-50">
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

    <script type="text/javascript" src="JS/CSP/Home.js" nonce="ihYxAijSER-YFSUxCDJbag"></script>

</body>
</html>
