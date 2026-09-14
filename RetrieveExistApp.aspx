<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RetrieveExistApp.aspx.cs"
    Inherits="RetrieveExistApp" %>

<%@ Register Src="header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="~/HeaderInclude.ascx" TagName="headerincl" TagPrefix="id" %>
<%@ Register Assembly="MacroWebControls" Namespace="MacroWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="FSCS.ascx" TagName="FSCS" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <id:headerincl ID="headerinclude" runat="server" />
    <link rel="Stylesheet" type="text/css" href="css/cReponsive.css" />
    <link type="text/css" href="website/Style.css" rel="Stylesheet" />
    <link type="text/css" href="website/Responsive.css" rel="Stylesheet" />

    <link rel="Stylesheet" type="text/css" href="css/styles.css" />
    <link rel="Stylesheet" type="text/css" href="css/hint.css" />
    <link rel="Stylesheet" type="text/css" href="css/imghoverStyle.css" />

    <link type="text/css" href="Website/custom-style.css" rel="Stylesheet" />
    <link href="website/css/bootstrap.min.css" rel="stylesheet" />
    <link type="text/css" href="css/WebResource.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="JS/disbackbtn.js"></script>
</head>
<body onload="AddRequestHandler()">
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= ModalProgress.ClientID %>';
    </script>
    <form id="form1" runat="server" autocomplete="off">
        <script language="javascript" type="text/javascript" src="JS/jsUpdateProgress.js"></script>
        <div>
            <ajaxToolkit:ToolkitScriptManager ID="ToolScriptManager1" runat="server" ScriptMode="Release">
            </ajaxToolkit:ToolkitScriptManager>
            <div class="container">

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <uc1:header ID="header" runat="server" />
                            <div class="bg-theme p-3">
                                <a id="LnkbtnHome" class="link text-white text-underline d-flex justify-content-end" href="index.aspx">Home</a>
                            </div>
                            <div class="bg-light p-2">
                                <span class="ms-3 font-18 text-dark">Retrieve Existing Application</span>
                            </div>

                            <div class="row my-3">
                                <div class="d-flex justify-content-center">
                                    <asp:Label ID="lblmsg" runat="server" Text="" CssClass="lblalert text-danger text-center d-block mb-1"></asp:Label>
                                    <asp:Label ID="errormsg" runat="server" Text="" CssClass="border border-danger rounded p-1 bg-white text-danger text-center mb-2 d-none"></asp:Label>
                                </div>
                            </div>

                            <div class="m-auto w-60 ">
                                <div class="card mb-5">
                                    <div class="card-body">

                                        <div class="form-group row mt-3">
                                            <div class="col-12 col-lg-4">
                                                <label class="form-label">Reference Number</label>
                                            </div>
                                            <div class="col-12 col-lg-8">
                                                <cc1:MacroWebTextBox ID="txtReferNo" runat="server" CssClass="form-control Utxtbox" BlurBackground="White"
                                                    Validate="IsEmail" MaxLength="12" TabIndex="1"></cc1:MacroWebTextBox>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-12 col-lg-4">
                                                <label class="form-label">Security CAPTCHA</label>
                                            </div>
                                            <div class="col-12 col-lg-8">
                                                <cc1:MacroWebTextBox ID="txtCaptcha" runat="server" CssClass="Utxtbox form-control mb-2" BlurBackground="White"
                                                    MaxLength="5" TabIndex="5"></cc1:MacroWebTextBox>
                                                <img title="Please enter the security code as shown in the image." id="Image1" alt="Visual verification"
                                                    src='image.aspx?r=<%= System.Guid.NewGuid().ToString("N")  %>' class="captcha" />
                                                <asp:LinkButton ID="lnkbtnResetCaptcha" runat="server" CausesValidation="false" OnClientClick="clearCaptchaText();">
                                                    <asp:Image ID="imgFolder" runat="server" ImageUrl="~/website/images/reCaptcha-Logo.png"
                                                        CssClass="recaptcha" />
                                                </asp:LinkButton>
                                            </div>
                                        </div>

                                        <center>
                                            <asp:Button ID="btnRetrieveApp" runat="server" Text="Submit" CssClass="button scbutton"
                                                TabIndex="6" OnClick="btnRetrieveApp_Click" OnClientClick="return submitRetExisApp();" />
                                        </center>
                                    </div>
                                </div>
                            </div>

                            <%--OTP POPUP Start--%>
                            <asp:Button ID="btnModOTPPopup" runat="server" CssClass="d-none" />
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupEmailOTP" runat="server" TargetControlID="btnModOTPPopup"
                                PopupControlID="PnlEmailOTP" CancelControlID="btnOk" BackgroundCssClass="sctableBackground">
                            </ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="PnlEmailOTP" runat="server" CssClass="d-none" DefaultButton="btnOTPVerify">
                                <table class="csmain_tbl mx-auto">
                                    <tr>
                                        <td align="center">
                                            <table class="table h-50 bg-theme text-white">
                                                <tr>
                                                    <td class="text-center w-90">Verify OTP 
                                                    </td>
                                                    <td class="text-center w-10" align="right">
                                                        <asp:LinkButton ID="lnkClose" runat="server" Text="" CssClass="linkclose" OnClick="lnkClose_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="py-2 text-center">
                                            <asp:Label ID="lblOTPAlert" runat="server" CssClass="text-danger d-none"></asp:Label>
                                            <asp:Label ID="lblOTPMsg" runat="server" CssClass="bodytext d-none"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="px-4 py-4">
                                            <cc1:MacroWebTextBox ID="txtOTP" runat="server" CssClass="form-control" placeholder="Please enter the OTP"
                                                MaxLength="6" Text='' Enabled="true" Validate="IsNumeric"
                                                ValidationGroup="OTPGroup"></cc1:MacroWebTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="mt-3 px-2">Haven't received your verification OTP?                                                                                                       
             <asp:Button ID="btnResendOTP" runat="server" Text="RESEND"
                 CausesValidation="false" Enabled="false" OnClick="btnResendOTP_Click" />
                                            <span id="timer" runat="server"></span>
                                            <asp:HiddenField ID="hdnSecondsLeft" runat="server" />
                                            <asp:HiddenField ID="otpResendAttempt" runat="server" />
                                            <input type="hidden" id="hiddenresend" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="py-4">

                                            <asp:Button ID="btnOTPVerify" runat="server" Text="Verify" CssClass="scbutton" ValidationGroup="abc"
                                                OnClick="btnOTPVerify_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <%--OTP POPUP End--%>
                            <%--Block popup--%>
                            <asp:Button ID="btnModEmailBlock" runat="server" CssClass="d-none" />
                            <ajaxToolkit:ModalPopupExtender ID="PopupEmailBlock" runat="server" TargetControlID="btnModEmailBlock"
                                PopupControlID="PnlEmailBlock" CancelControlID="btnEmailConCancel" BackgroundCssClass="sctableBackground">
                            </ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="PnlEmailBlock" runat="server" CssClass="d-none" DefaultButton="btnOTPVerify">
                                <table class="csmain_tbl mx-auto">
                                    <tr>
                                        <td align="center">
                                            <table class="table h-50 bg-theme text-white">
                                                <tr>
                                                    <td class="text-center"></td>
                                                    <td class="text-center">
                                                        <h5 class="modal-title text-white text-center">UBI UPB - Existing Customer</h5>
                                                    </td>
                                                    <td class="text-center" align="right"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="pt-2 px-4">
                                            <asp:Label ID="lblBlockTxt" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="py-4">
                                            <asp:Button ID="btnBlockOk" runat="server" Text="OK" CssClass="scbutton" ValidationGroup="abc"
                                                OnClick="btnBlockOk_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <%--End--%>

                            <uc3:FSCS ID="FSCS" runat="server" />
                            <uc2:footer ID="footer1" runat="server" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

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

    <script type="text/javascript" src="website/js/bootstrap.bundle.min.js"> </script>
    <script type="text/javascript" src="javascript/jquery-3.7.1.min.js"></script>
    <script type="text/javascript" src="javascript/JSValidation.js"></script>
    <script type="text/javascript" src="javascript/popper.min.js"></script>
    <script type="text/javascript" src="javascript/app.js"></script>
    <script type="text/javascript" src="JS/CSP/OnClickHandlers.js"></script>
    <script type="text/javascript" src="JS/CSP/Home.js" nonce="ihYxAijSER-YFSUxCDJbag"></script>

    <%--OTP Timer--%>
    <script type="text/javascript">
        var timer;
        var countdown;
        function startTimer(duration) {
            //console.log("Start");
            clearInterval(timer); // Clear existing timer
            countdown = duration;
            timer = setInterval(function () {
                countdown--;
                if (countdown < 0) {
                    clearInterval(timer);
                    $("#timer").text('');
                    $("#btnResendOTP").prop('disabled', false);
                    $('#btnResendOTP').removeClass('disabled-button');
                    //$('#btnResendOTP').addClass('scbutton');

                    //$('#btnResendOTP').removeClass('disabled-link');
                    $('#btnResendOTP').addClass('btn btn-link h-auto pt-0');

                } else {
                    $('#btnResendOTP').addClass('disabled-button');
                    $("#timer").text(countdown + " seconds remaining");
                }
            }, 1000);
        }

        $(document).ready(function () {
            $(document).on("click", "#<%= btnOTPVerify.ClientID %>", function () {
                console.log("enable s");
                if ($("#btnResendOTP").hasClass("disabled-button")) {
                    //console.log("disable");
                    $("#hiddenresend").val("false");
                } else {
                    //console.log("enable");
                    $("#hiddenresend").val("true");
                    //startTimer(0);
                }
            });
        });

        function preventEnterKey(event) {
            if (event.key === "Enter") {
                event.preventDefault();
                return false;
            }
        }

        // Prevent Enter key from submitting the form
        document.addEventListener('keydown', function (event) {
            if (event.key === 'Enter') {
                event.preventDefault();
            }
        });

        function clearCaptchaText() {
            document.getElementById('<%= txtCaptcha.ClientID %>').value = '';
        }

    </script>
    <%--End OTP Timer--%>
</body>
</html>
