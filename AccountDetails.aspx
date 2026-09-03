<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountDetails.aspx.cs" Inherits="AccountDetails" %>

<%@ Register Src="~/Header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Assembly="MacroWebControls" Namespace="MacroWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="footer.ascx" TagName="footer" TagPrefix="uc2" %>
<%@ Register Src="~/HeaderInclude.ascx" TagName="headerincl" TagPrefix="id" %>
<%@ Register Src="FSCS.ascx" TagName="FSCS" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <id:headerincl ID="headerinclude" runat="server" />
    <!--  <link rel="Stylesheet" type="text/css" href="css/cReponsive.css" />
    <link rel="Stylesheet" type="text/css" href="css/styles.css" />   -->
    <link rel="Stylesheet" type="text/css" href="css/font.css" />
    <link rel="Stylesheet" type="text/css" href="website/Style.css" />
    <link rel="Stylesheet" type="text/css" href="website/Responsive.css" />
    <link type="text/css" href="css/WebResource.css" rel="stylesheet" />
    <link type="text/css" href="Website/Responsive.css" rel="Stylesheet" />
    <link type="text/css" href="Website/custom-style.css" rel="Stylesheet" />
    <link type="text/css" href="Website/Style.css" rel="Stylesheet" />
    <link href="website/css/bootstrap.min.css" rel="stylesheet" />

    <script language="javascript" type="text/javascript" src="JS/disbackbtn.js"></script>
    <script language="javascript" type="text/javascript">

        function show_progress() {
            document.getElementById('lblAlert').innerHTML = '';
            document.getElementById('lblRefNo').innerHTML = '';
        }

        function validate(pObject) {
            if (document.getElementById(pObject).value == "") {
                alert('Postal code can not be empty');
                return false;
            }
        }

        function OnKeyUp(obj, NTxt, Len) {

            var FTxt = document.getElementById(obj.id).value;
            if (FTxt.length == Len) {
                document.getElementById(NTxt).focus();
                return false;
            }

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
    <link rel="stylesheet" type="text/css" href="website/scrolltop/font-awesome.min.css" />
    <script src="javascript/JSValidation.js" type="text/javascript"></script>
    <script src="javascript/jquery-3.7.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="website/js/popper-min.js"></script>
    <script type="text/javascript" src="website/js/bootstrap.bundle.min.js"> </script>
    <script type="text/javascript" src="javascript/app.js"></script>
</head>
<body onload="AddRequestHandler()">
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= ModalProgress.ClientID %>';
    </script>
    <form id="form1" runat="server" autocomplete="off">
        <script language="javascript" type="text/javascript" src="JS/jsUpdateProgress.js"></script>
        <div class="container">
            <ajaxToolkit:ToolkitScriptManager ID="ToolScriptManager1" runat="server" ScriptMode="Release">
            </ajaxToolkit:ToolkitScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="bg-white">
                        <uc1:header ID="header" runat="server" />
                        <hr class="hr" />
                        <div class="px-2 py-2">
                            <p class="lbltxt1">
                                Please fill in your details for opening your Union Premier Bond Account. Fields
                                        marked with <font class="red">* </font>are mandatory
                                  <br />
                            Note:&nbsp;Session will expire if system remains inactive for 20 minutes
                        </div>

                        <div>
                            <div align="right" class="DarkPinkhead pr-2">

                                <asp:LinkButton ID="lblExit" runat="server" Text="Exit" CssClass="text-danger font-weight-bold"
                                    CausesValidation="false" OnClientClick="return confirm('Your keyed in data will be lost. Do you really want to exit from the application ?');"
                                    OnClick="lblExit_Click"></asp:LinkButton>
                            </div>



                            <div align="center" class="border-red">
                                <img src="images/step/menu.png" alt="" class="img-fluid h-40 hide" />
                            </div>
                            <p class="display1">
                                Step1
                            </p>

                            <div class="faq-title p-2 pl-3">
                                Account Details
                            </div>


                            <div align="center">

                                <div class="p-3">
                                    <asp:Label ID="lblAlert" runat="server" Text="" CssClass="alert font-15"></asp:Label>
                                </div>


                                <div align="center">

                                    <div class="pa_tbl1">
                                        <div class="col-md-12 p-3">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="account-detail p-2 text-left">
                                                        How do you want to open this deposit ? <span class="lblman">*</span>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 align-self-center">
                                                    <asp:RadioButtonList ID="rblIsJntAc" runat="server" TextAlign="Right" RepeatDirection="Horizontal"
                                                        AutoPostBack="true" CssClass="bodytextbold11 text-theme" OnSelectedIndexChanged="rblIsJntAc_SelectedIndexChanged">
                                                        <asp:ListItem Text="Individual" Value="NO" Selected="true">&nbsp; Individual &nbsp;</asp:ListItem>
                                                        <asp:ListItem Text="Joint" Value="YES">&nbsp; Joint</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="p-3"></div>
                                <div align="center">
                                    <asp:Panel ID="pnlJointCount" runat="server" Visible="false">
                                        <div class="pa_tbl1">
                                            <div class="col-md-12 ">
                                                <div class="row p-2">
                                                    <div class="col-md-6 ">
                                                        <div class="account-detail p-2 text-left">
                                                            There can be maximum of 3 joint applicant per deposit<br />
                                                            How many joint applicant(s) ? <span class="lblman">*</span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6 pt-2 align-self-center">
                                                        <asp:RadioButtonList ID="rblIsJntCount" runat="server" TextAlign="Right" RepeatDirection="Horizontal"
                                                            AutoPostBack="true" CssClass="bodytextbold11 text-theme" OnSelectedIndexChanged="rblIsJntCount_SelectedIndexChanged">
                                                            <asp:ListItem Text="One" Value="2">&nbsp; One &nbsp;</asp:ListItem>
                                                            <asp:ListItem Text="Two" Value="3">&nbsp; Two &nbsp;</asp:ListItem>
                                                            <asp:ListItem Text="Three" Value="4">&nbsp; Three</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                </div>
                            </div>
                            <div class="p-3"></div>
                            <div align="center">
                                <asp:Panel ID="pnlPrimary" runat="server" Visible="false">
                                    <div class="pa_tbl1">
                                        <div class="col-md-12 p-3">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="account-detail p-2 text-left">
                                                        Primary Applicant
                                                    </div>
                                                </div>
                                                <div class="col-md-4 align-self-center">
                                                    <asp:Label ID="lblPrimaryName" runat="server" Text="Yet to be added" CssClass="lblAcDtlApFont text-theme"></asp:Label>
                                                </div>
                                                <div class="col-md-4 align-self-center">
                                                    <asp:Button ID="btnPrimaryAdd" runat="server" Text="Add" CssClass="button" Visible="true"
                                                        CausesValidation="false" OnClick="btnPrimaryAdd_Click" />
                                                    <asp:Button ID="btnPrimaryEdit" runat="server" Text="Modify" CssClass="button" Visible="true"
                                                        CausesValidation="false" OnClick="btnPrimaryEdit_Click" />
                                                    <asp:Button ID="btnPrimaryDelete" runat="server" Text="Delete" CssClass="button"
                                                        OnClientClick="return confirm('Are you sure you want to delete this applicant?');"
                                                        Visible="true" CausesValidation="false" OnClick="btnPrimaryDelete_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="p-3"></div>
                            <div align="center">
                                <asp:Panel ID="pnlJoint1" runat="server" Visible="false">
                                    <div class="pa_tbl1">
                                        <div class="col-md-12 p-3">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="account-detail p-2 text-left">
                                                        Joint Applicant - 1
                                                    </div>
                                                </div>
                                                <div class="col-md-4 align-self-center">
                                                    <asp:Label ID="lbljoint1" runat="server" Text="Yet to be added" CssClass="lblAcDtlApFont text-theme"></asp:Label>
                                                </div>
                                                <div class="col-md-4 align-self-center">
                                                    <asp:Button ID="btnJointAdd1" runat="server" Text="Add" CssClass="button" Visible="true"
                                                        CausesValidation="false" OnClick="btnJointAdd1_Click" />
                                                    <asp:Button ID="btnJointModify1" runat="server" Text="Modify" CssClass="button" Visible="true"
                                                        CausesValidation="false" OnClick="btnJointModify1_Click" />
                                                    <asp:Button ID="btnJointDelete1" runat="server" Text="Delete" CssClass="button" OnClientClick="return confirm('Are you sure you want to delete this applicant?');"
                                                        Visible="true" CausesValidation="false" OnClick="btnJointDelete1_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="p-3"></div>
                            <div align="center">
                                <asp:Panel ID="pnlJoint2" runat="server" Visible="false">
                                    <div class="pa_tbl1">
                                        <div class="col-md-12 p-3">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="account-detail p-2 text-left">
                                                        Joint Applicant - 2
                                                    </div>
                                                </div>
                                                <div class="col-md-4 align-self-center">
                                                    <asp:Label ID="lbljoint2" runat="server" Text="Yet to be added" CssClass="lblAcDtlApFont text-theme"></asp:Label>
                                                </div>
                                                <div class="col-md-4 align-self-center">
                                                    <asp:Button ID="btnJointAdd2" runat="server" Text="Add" CssClass="button" Visible="true"
                                                        CausesValidation="false" OnClick="btnJointAdd2_Click" />
                                                    <asp:Button ID="btnJointModify2" runat="server" Text="Modify" CssClass="button" Visible="true"
                                                        CausesValidation="false" OnClick="btnJointModify2_Click" />
                                                    <asp:Button ID="btnJointDelete2" runat="server" Text="Delete" CssClass="button" OnClientClick="return confirm('Are you sure you want to delete this applicant?');"
                                                        Visible="true" CausesValidation="false" OnClick="btnJointDelete2_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>


                            <div class="py-3"></div>
                            <div align="center">
                                <asp:Panel ID="pnlJoint3" runat="server" Visible="false">
                                    <div class="pa_tbl1">
                                        <div class="col-md-12 p-3">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="account-detail p-2 text-left">
                                                        Joint Applicant - 3
                                                    </div>
                                                </div>
                                                <div class="col-md-4 align-self-center">
                                                    <asp:Label ID="lbljoint3" runat="server" Text="Yet to be added" CssClass="lblAcDtlApFont text-theme"></asp:Label>
                                                </div>
                                                <div class="col-md-4 align-self-center">
                                                    <asp:Button ID="btnJointAdd3" runat="server" Text="Add" CssClass="button" Visible="true"
                                                        CausesValidation="false" OnClick="btnJointAdd3_Click" />
                                                    <asp:Button ID="btnJointModify3" runat="server" Text="Modify" CssClass="button" Visible="true"
                                                        CausesValidation="false" OnClick="btnJointModify3_Click" />
                                                    <asp:Button ID="btnJointDelete3" runat="server" Text="Delete" CssClass="button" OnClientClick="return confirm('Are you sure you want to delete this applicant?');"
                                                        Visible="true" CausesValidation="false" OnClick="btnJointDelete3_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>


                            <div class="d-flex justify-content-end mb-2 mr-3">
                                <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save & Return Later" CssClass="button scbutton mr-2"
                                    CausesValidation="false" OnClick="btnSaveAsDraft_Click" />
                                <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="button scbutton"
                                    CausesValidation="false" OnClick="btnNext_Click" />
                            </div>

                            <asp:Panel ID="pnlIDDocUploadNote" runat="server" Visible="false">
                                <div class="my-4">
                                    <div class="row justify-content-center">
                                        <div class="col-md-8">
                                            <div class="card shadow-sm bordercolor">
                                                <!-- Header -->
                                                <div class="card-header bg-light text-primary fw-semibold d-flex justify-content-between align-items-center">
                                                    <span class="account-detail">Complete Your Details Faster with Document Upload:   </span>
                                                </div>

                                                <!-- Collapsible Body -->
                                                <div class="card-body small">
                                                    <ul class="acctxt lh-lg">
                                                        <li>In the next step, a document upload option is available to assist you in filling in the applicant's personal details automatically. We encourage you to use this feature, as it helps ensure the details are accurate and increases the chances of a successful KYC verification.
                                            </li>
                                                        <li>If you do not have a digital copy of the applicant's identity document available, or if you prefer to fill in the details manually, please ensure that all information entered matches exactly with the respective applicant's government-issued identity document.
                                            </li>
                                                        <li>For guidance on how to upload a document, please refer to the 'Document Upload Tips' in the 'Identity Document' section.
                                               </li>
                                                        <li>Please be assured that any documents you upload are used solely to help complete your application details and are not kept with us beyond this step.
                                            </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div>
                                <asp:Button ID="btnModelPopup" runat="server" CssClass="d-none" />
                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnModelPopup"
                                    PopupControlID="PnlMsgBox" BackgroundCssClass="tableBackground">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="PnlMsgBox" runat="server" CssClass="d-none">
                                    <table cellpadding="0" cellspacing="0" class="yellowborder table bg-light" align="center">
                                        <tr>
                                            <td class="p-2">
                                                <p class="bodytext">
                                                    <asp:Label ID="lblDataLostMsg" runat="server"></asp:Label>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" class="pb-2">
                                                <asp:Button ID="btnOk" runat="server" Text="Yes" CssClass="button scbutton" CausesValidation="false"
                                                    OnClick="btnOk_Click" />
                                                &nbsp;&nbsp;
                                                        <asp:Button ID="btnCancel" runat="server" Text="No" CssClass="button scbutton"
                                                            CausesValidation="false" OnClick="btnCancel_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>

                        <%--<tr>
                <td align="center">
                    <ajaxToolkit:ModalPopupExtender ID="modpopextEmail" runat="server" PopupControlID="panEmail"
                        TargetControlID="Button1" BackgroundCssClass="modalBackground" BehaviorID="modalBehaviorEmail">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Button ID="Button1" runat="server" Text="Button" CssClass="Ndisplay" />
                    <asp:Panel ID="panEmail" runat="server" Width="80%">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" class="yellowborder2" bgcolor="#ffffff" align="center">
                                    <tr>
                                        <td colspan="2" class="violethead" valign="middle" align="left">
                                            Please register your email Id for future communication &amp; Retrieve your saved
                                            application
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="bodytext" colspan="2" valign="middle">
                                            This email id will be treated as the login id to this website. Password is case
                                            sensitive. Choose the password that is memorable to you and it should be alphanumeric.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:PlaceHolder ID="PlcHolderEmail" runat="server">
                                                <asp:ValidationSummary ID="ValidationSummary2" CssClass="bodytextbold1" runat="server"
                                                    HeaderText="Page has the following errors" ValidationGroup="RegEmail" Style="visibility: hidden;" />
                                            </asp:PlaceHolder>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 6px 6px 6px 6px;">
                                            <asp:Literal ID="lblAlert2" runat="server">
                                            </asp:Literal>
                                        </td>
                                    </tr>
                                 <tr>
                                        <td class="bodytextbold" align="left" width="170px" height="30px">
                                            Login Email Id:
                                        </td>
                                        <td align="left" width="330px">
                                            <cc1:MacroWebTextBox ID="txtEmailId" runat="server" CssClass="txtbox" MaxLength="50"
                                                Validate="IsEmail" BlurBackground="White" TabIndex="1" FocusBackground="" Width="220px"
                                                Enabled="false"></cc1:MacroWebTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="bodytextbold" align="left" height="30px">
                                            Password:
                                        </td>
                                        <td align="left">
                                            <cc1:MacroWebTextBox ID="txtPassword" runat="server" CssClass="txtbox" MaxLength="10"
                                                Validate="BlkSQuote" BlurBackground="White" TabIndex="2" FocusBackground="" Width="220px"
                                                TextMode="Password"></cc1:MacroWebTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" height="50">
                                            <asp:Button ID="btnSubmitEmail" runat="server" Text="Submit" CssClass="button" TabIndex="3"
                                                ValidationGroup="RegEmail" OnClientClick="Base64()" OnClick="btnSubmitEmail_Click" />
                                            &nbsp;
                                            <asp:Button ID="btnCancelEmail" runat="server" Text="Cancel" CssClass="button" TabIndex="4"
                                                CausesValidation="false" OnClientClick="HideModalPopup('modalBehaviorEmail');Base64();" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </td>
            </tr>--%>
                        <tr>
                            <td>
                                <asp:Button ID="btnModPopupEx2" runat="server" CssClass="d-none" />
                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnModPopupEx2"
                                    PopupControlID="PnlPwdReg" BackgroundCssClass="sctableBackground">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="PnlPwdReg" runat="server" CssClass="d-none" DefaultButton="btnSubmitEmail">

                                    <div class="w-50 m-auto">
                                        <div class="modal-header p-2 h-50 bg-theme justify-content-center">
                                            <h6 class="modal-title text-white text-center">Application Saved</h6>
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
                        <uc3:FSCS ID="FSCS" runat="server" />

                        <uc2:footer ID="footer1" runat="server" />

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <%--<ajaxToolkit:ModalPopupExtender runat="server" PopupControlID="PanLoading" ID="ModalProgress"
            TargetControlID="PanLoading" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="PanLoading" runat="server" CssClass="updateProgress">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="5">
                <ProgressTemplate>
                    <table style="position: relative; top: 7%; margin: 0 auto; left: 0px; height: 100%; width: 260px;">
                        <tr>
                            <td>
                                <img src="images/ajax-loader.gif" alt="loading" title="loading" style="margin: 0 auto; display: block;" />
                            </td>
                        </tr>
                        <tr>
                            <td class="bodytext" style="text-align: center;">Processing your request. Please wait ...
                            </td>
                        </tr>
                    </table>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </asp:Panel>--%>

        <div class="modal test d-none sctableBackground" id="ModalProgress" runat="server">
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
