<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicantDetails.aspx.cs"
    ValidateRequest="false" Inherits="ApplicantDetails" MaintainScrollPositionOnPostback="true" %>

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
    <link rel="Stylesheet" type="text/css" href="css/hint.css" />
    <link rel="Stylesheet" type="text/css" href="css/font.css" />
    <link rel="Stylesheet" type="text/css" href="website/Style.css" />
    <link rel="Stylesheet" type="text/css" href="website/Responsive.css" />
    <link type="text/css" href="css/WebResource.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="JS/disbackbtn.js"></script>

    <link type="text/css" href="Website/custom-style.css" rel="Stylesheet" />
    <link type="text/css" href="css/cReponsive.css" rel="Stylesheet" />
    <link rel="Stylesheet" type="text/css" href="css/imghoverStyle.css" />

    <link href="website/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="Stylesheet" type="text/css" href="Website/Style.css" />

    <script type="text/javascript">
        function isMobileDevice() {
            return (typeof window.orientation !== "undefined") || (navigator.userAgent.indexOf('IEMobile') !== -1);
        };
    </script>
    <script type="text/javascript">
        //alert("enter1");

        function AutoTab(obj, NTxt, Len, e) {
            //alert("enter");
            var FTxt = document.getElementById(obj).value;
            var code_val = e.keyCode ? e.keyCode : e.charCode;

            if (code_val == "8" || code_val == "9" || (code_val >= "35" && code_val <= "40") || code_val == "46") {
                return true;
            }

            if (FTxt.length == Len) {
                //alert("test");
                if ((window.navigator.userAgent.indexOf("MSIE ") > 0) || ((window.navigator.userAgent.indexOf("Trident") > 0))) {
                    //alert('ie');
                }
                else {
                    var key_val = String.fromCharCode(code_val);
                    document.getElementById(NTxt).value = key_val;
                }
                document.getElementById(NTxt).focus();
                return false;
            }
        }
    </script>
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
    <script src="javascript/jquery-3.7.1.min.js" type="text/javascript"></script>
    <script src="javascript/popper.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="javascript/app.js"></script>
</head>
<body onload="AddRequestHandler()">
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= ModalProgress.ClientID %>';
    </script>
    <form id="form1" runat="server" autocomplete="off">
        <script language="javascript" type="text/javascript" src="JS/jsUpdateProgress.js"></script>
        <%--<script language="javascript" type="text/javascript" src="javascript/jsUpdateProgress.js"></script>--%>

        <ajaxToolkit:ToolkitScriptManager ID="ToolScriptManager1" runat="server" ScriptMode="Release">
        </ajaxToolkit:ToolkitScriptManager>

        <div class="container">
            <div class="bg-white">
                <uc1:header ID="header" runat="server" />
                <hr class="hr" />
                <div class="px-2 py-2">
                    <p class="lbltxt1">
                        Please fill in your details for opening your Union Premier Bond Account. Fields
              marked with <font class="red">* </font>are mandatory
              <br />
                        Note:&nbsp;Session will expire if system remains inactive for 20 minutes
                    </p>
                </div>
                <div class="row">
                    <div class="DarkPinkhead pr-2 d-flex justify-content-end">
                        <asp:LinkButton ID="lblExit" runat="server" Text="Exit" CssClass="text-danger text-decoration-none font-weight-bold"
                            CausesValidation="false" OnClientClick="return confirm('Your keyed in data will be lost. Do you really want to exit from the application ?');"
                            OnClick="lblExit_Click"></asp:LinkButton>
                    </div>
                    <div class="border-red px-0">
                        <img src="images/step/menu1.png" alt="" class="img-fluid hide" />
                    </div>
                    <p class="display1">
                        Step2
                    </p>
                    <div class="faq-title p-2 ps-4">
                        Applicant Details
                    </div>
                </div>

                <asp:Panel ID="pnlIDDocUpload" runat="server" Visible="false">
                <div class="col-md-12 p-3">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="border-blue">
                                <h5 class="Dtit"><span class="DOrangehead">Identity Document</span></h5>
                                <div class="card card-body border-0">
                                    <div class="row align-items-center">
                                        <div class="col-lg-10 col-12 mb-2 mb-lg-0">
                                            <label class="form-label">For a seamless experience, feel free to upload a copy of the applicant's identity document here. This will allow us to auto-fill the applicant's name and document details for you.</label>
                                        </div>
                                        <div class="col-lg-2 col-12">
                                            <!-- Browse File Button -->
                                            <label for="<%= FileUploader.ClientID %>" class="upload-button">
                                                Upload Document
                                            </label>

                                            <!-- Hidden FileUpload -->
                                            <asp:FileUpload ID="FileUploader" runat="server" CssClass="file-upload-hidden" AllowMultiple="false" accept=".jpg,.jpeg,.png,.pdf" />
                                            <%--<asp:FileUpload ID="FileUploader" runat="server" CssClass="file-upload-hidden" AllowMultiple="true" />--%>

                                            <!-- Upload Button -->
                                            <asp:Button ID="btnUpload" runat="server" Text="Upload Document" CssClass="btn btn-success btn-hidden" CausesValidation="false" OnClick="btnUpload_Click" />

                                            <!-- Display Selected File Name -->
                                            <%--<span id="fileName" class="ms-2"></span>--%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 text-center">
                                            <!-- Display error message -->
                                            <asp:Label ID="lblUploadError" runat="server" Text="" CssClass="text-danger font-weight-bold font-16"></asp:Label>

                                            <!-- Display info message -->
                                            <asp:Label ID="lblUploadInfo" runat="server" Text="" CssClass="bodytext text-success font-weight-bold font-16"></asp:Label>
                                        </div>
                                    </div>

                                    <!-- Accordion Info message -->
                                    <div class="custom-accordion-wrapper mt-3">
                                        <div class="custom-accordion-item">
                                            <button type="button" class="custom-accordion-header" data-bs-toggle="collapse" data-bs-target="#collapseDoc" aria-expanded="false">
                                                Important: Document Upload Tips <span class="acc-arrow"></span>
                                            </button>

                                            <div id="collapseDoc" class="collapse">
                                                <div class="custom-accordion-body">
                                                    <ul>
                                                        <li>If you choose to upload a scanned image of the applicant's identity document, the system will automatically extract and auto-fill the relevant fields with the information available in the document, for your convenience.</li>
                                                        <li>The auto-filled fields will remain editable, allowing you to make changes if needed.</li>
                                                        <li>The accepted identity documents include currently valid passports from all countries and UK driving licences.</li>
                                                        <li>UK driving licences issued before 1998 are not supported for auto-fill. Please enter the details manually if this applies.</li>
                                                        <li>Please ensure you have a clear and legible scanned copy of the identity document ready for upload.</li>
                                                        <li>The allowed file formats for upload are <strong>.jpg, .jpeg, .png, and .pdf files</strong>, with a maximum file size of <strong>2 MB</strong>.</li>
                                                    </ul>
                                                    <p class="mb-0">
                                                        We recommend uploading identity documents for automatic data extraction, as this may reduce information discrepancies and improve the success of the KYC validation process.
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
               </asp:Panel>
                
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="">
                            <div class="col-md-12 p-3">
                                <div class="w-100">
                                    <asp:PlaceHolder ID="ErrorPH" runat="server"></asp:PlaceHolder>
                                    <div class="w-50">
                                        <asp:HiddenField ID="hfAcId" runat="server" Visible="false" />
                                        <asp:Label ID="lblRefNo" runat="server" Text="" CssClass="font-weight-bold text-danger"></asp:Label>
                                        <asp:Label ID="lblSequence" runat="server" Text="" Visible="false"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Button ID="btnApplicant1" runat="server" CssClass="button" Text="Load Sample Appl-1"
                                            CausesValidation="false" OnClick="btnApplicant1_Click" Visible="false" />
                                        <asp:Button ID="btnApplicant2" runat="server" CssClass="button" CausesValidation="false"
                                            Text="Load Sample Appl-2" OnClick="btnApplicant2_Click" Visible="false" />
                                        <asp:Button ID="btnApplicant3" runat="server" CssClass="button" CausesValidation="false"
                                            Text="Appl-3" OnClick="btnApplicant3_Click" Visible="false" />
                                        <asp:Button ID="btnApplicant4" runat="server" CssClass="button" CausesValidation="false"
                                            Text="Appl-4" OnClick="btnApplicant4_Click" Visible="false" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="border-blue">
                                            <h5 class="Dtit"><span class="DOrangehead">Personal Details</span></h5>

                                            <div class="card border-0 card-body pb-0">


                                                <div class="form-group row">
                                                    <label for="inputtext" class="col-sm-6 col-form-label">Title<span class="lblman">*</span></label>
                                                    <div class="col-sm-6">
                                                        <div class="w-100">
                                                            <asp:DropDownList ID="ddlTittle" runat="server" CssClass="selectbox form-control" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlTittle_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="mb-1 person-det">
                                                In order to carry out successful KYC validation please provide your First Name,
                              Middle Name and Surname as exactly shown on your Passport or Driving License document.
                                            </div>

                                            <div class="card border-0 card-body">

                                                <div class="form-group row">
                                                    <label for="inputtext" class="col-sm-6 col-form-label">First Name<span class="lblman">*</span></label>
                                                    <div class="col-sm-6">
                                                        <cc1:MacroWebTextBox ID="txtFirstNm" runat="server" BlurBackground="White" CssClass="Utxtbox form-control"
                                                            FocusBackground="" MaxLength="30" Validate="IsAlpha"></cc1:MacroWebTextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="inputtext" class="col-sm-6 col-form-label">
                                                        Middle Name 
                                                                <img src='images/help.png' alt='help' title="If your Middle Name is mentioned in your ID Document, it is mandatory to enter your
                                  middle name as it appears on your ID"
                                                                    data-bs-toggle="tooltip" />
                                                        <div class="MNtooltip"></div>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <cc1:MacroWebTextBox ID="txtMiddleNm" runat="server" BlurBackground="White" CssClass="Utxtbox form-control"
                                                            FocusBackground="" MaxLength="20" Validate="IsAlpha"></cc1:MacroWebTextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="inputtext" class="col-sm-6 col-form-label">
                                                        Surname<span class="lblman">*</span>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <cc1:MacroWebTextBox ID="txtSurNm" runat="server" BlurBackground="White" CssClass="Utxtbox form-control"
                                                            FocusBackground="" MaxLength="20" Validate="IsAlpha"></cc1:MacroWebTextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="inputtext" class="col-sm-6 col-form-label">
                                                        Gender<span class="lblman">*</span>
                                                    </label>
                                                    <div class="col-sm-6 align-self-center">

                                                        <asp:RadioButtonList ID="rblGender" runat="server" CssClass="bodytextbold1 lblalign"
                                                            TextAlign="Right" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Male" Value="MALE">&nbsp; Male &nbsp;</asp:ListItem>
                                                            <asp:ListItem Text="Female" Value="FEMALE">&nbsp; Female</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>

                                                <%--<tr>
                              <td align="left" class="lblwidth lbltxt">
                                  Marital Status<span class="lblman">*</span>
                              </td>
                              <td class="txtbwidth">
                                  <asp:DropDownList ID="ddlMaritalSts" runat="server" CssClass="selectbox" AutoPostBack="false">
                                      <asp:ListItem Selected="True" Value="0">-- Select Marital Status --</asp:ListItem>
                                      <asp:ListItem Value="SIR"> SIR</asp:ListItem>
                                      <asp:ListItem Value="MR"> MR</asp:ListItem>
                                      <asp:ListItem Value="MISS"> MISS</asp:ListItem>
                                      <asp:ListItem Value="MS"> MS / MRS</asp:ListItem>
                                      <asp:ListItem Value="M/S"> MESSERS</asp:ListItem>
                                      <asp:ListItem Value="LORD"> LORD</asp:ListItem>
                                      <asp:ListItem Value="ESQ"> ESQUIRE</asp:ListItem>
                                      <asp:ListItem Value="ENGR"> ENGINEER</asp:ListItem>
                                      <asp:ListItem Value="DR"> DR</asp:ListItem>
                                      <asp:ListItem Value="BANK"> BANK</asp:ListItem>
                                  </asp:DropDownList>
                              </td>
                              </tr>--%>

                                                <div class="form-group row">
                                                    <label for="inputtext" class="col-sm-6 col-form-label">Date of Birth<span class="lblman">*</span></label>
                                                    <div class="col-sm-6">
                                                        <cc1:MacroWebCalendar ID="CalDOB" name="CalDOB" runat="server" AddYear="-18" Sorting="Des"
                                                            SubtractYear="108" CssClass="sselectbox1" AutoPostBack="False" />
                                                        <cc1:MacroWebTextBox ID="txtDOB" runat="server" CssClass="txtbox11 visible-hidden "
                                                            BlurBackground="" FocusBackground="" Validate="None"></cc1:MacroWebTextBox>
                                                        <asp:Label ID="lbldoberror" runat="server" Text="" CssClass="text-danger font-12"></asp:Label>

                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label for="inputtext" class="col-sm-6 col-form-label">
                                                        Place of Birth (City)<span class="lblman">*</span>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <cc1:MacroWebTextBox ID="txtPlaceOfBirth" runat="server" BlurBackground="White" CssClass="Utxtbox form-control"
                                                            FocusBackground="" MaxLength="30" Validate="IsAlpha"></cc1:MacroWebTextBox>
                                                    </div>
                                                </div>


                                                <asp:Panel ID="pnlMemWrd" runat="server" Visible="false">
                                                    <div class="form-group row">
                                                        <label for="inputtext" class="col-sm-6 col-form-label">
                                                            Mother's Maiden Name / Memorable word<span class="lblman">*</span>
                                                        </label>
                                                        <div class="col-sm-6 align-self-center">
                                                            <cc1:MacroWebTextBox ID="txtMomName" runat="server" BlurBackground="White" CssClass="form-control Utxtbox"
                                                                FocusBackground="" MaxLength="30" Validate="IsAlpha"></cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>
                                                </asp:Panel>


                                                <div class="form-group row">
                                                    <label for="inputtext" class="col-sm-6 col-form-label">
                                                        Citizenship<span class="lblman">*</span>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <div class="w-100">
                                                            <%-- <asp:DropDownList ID="ddlCtznShp2" runat="server" CssClass="selectbox" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlTittle_SelectedIndexChanged">
                                  </asp:DropDownList>--%>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCtznShp" runat="server" CssClass="selectbox form-control">
                                                            <asp:ListItem Selected="True" Value="0">-- Select Country --</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label for="inputtext" class="col-sm-6 col-form-label">
                                                        National Insurance No.
                                                    </label>
                                                    <div class="col-sm-6">

                                                        <cc1:MacroWebTextBox ID="txtNINO" runat="server" BlurBackground="White" CssClass="Utxtbox form-control"
                                                            FocusBackground="" MaxLength="9" Validate="IsAlphaNum"></cc1:MacroWebTextBox>
                                                    </div>
                                                </div>
                                            </div>



                                            <h5 class="Dtit"><span class="DOrangehead">Contact Details</span></h5>

                                            <div class="card border-0 card-body">
                                                <div class="form-group row">
                                                    <label for="inputtext" class="col-sm-6 col-form-label">
                                                        Telephone No.
                                                    </label>
                                                    <div class="col-sm-6">

                                                        <cc1:MacroWebTextBox ID="txtHomeTelPhNo" runat="server" BlurBackground="White" CssClass="Utxtbox form-control"
                                                            FocusBackground="" MaxLength="17" Validate="IsNumeric"></cc1:MacroWebTextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="inputtext" class="col-sm-6 col-form-label">
                                                        Mobile No.<span class="lblman">*</span>
                                                        <img src='images/help.png' alt='help' title="Please enter your 10-digit UK mobile number directly, without the leading 0 or country code. Kindly make sure the number is correct and active, as it will be used for account-related communications and One-Time Password (OTP) delivery."
                                                            data-bs-toggle="tooltip" />
                                                        <div class="MNtooltip"></div>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <div class="input-group">
                                                            <!-- UK Country Code -->
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text p-ukcode uk-prefix" id="ukCode">
                                                                    <img src="website/Images/gb.png" alt="UK Flag" class="me-1 uk-flag" />
                                                                    +44
                                                                </span>
                                                            </div>
                                                            <!-- Mobile Number Textbox -->
                                                            <cc1:MacroWebTextBox ID="txtMobileNo" runat="server" BlurBackground="White" CssClass="Utxtbox form-control"
                                                                FocusBackground="" MaxLength="10" Validate="IsNumeric"></cc1:MacroWebTextBox>
                                                            <span class="small mobiletxt">Enter 10-digit UK mobile number only, without the leading 0 or country code.</span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="inputtext" class="col-sm-6 col-form-label">
                                                        E-mail Address<span class="lblman">*</span>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <cc1:MacroWebTextBox ID="txtEmailAddr" runat="server" BlurBackground="White" CssClass="Utxtbox form-control" onkeydown="preventEnterKey(event)"
                                                                    FocusBackground="" MaxLength="50" Validate="IsEmail"></cc1:MacroWebTextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row py-2">
                                                            <div class="col-lg-12">
                                                                <div class="d-flex align-items-center">
                                                                    <asp:Button ID="btnVerify" runat="server" Text="Verify" CssClass="button" Enabled="true"
                                                                        OnClick="btnSendOTP_Click" CausesValidation="true" OnClientClick="return VerifyEmailAdd();javascript:show_progress();" />
                                                                    <asp:Image ID="imgEmailVerfied" runat="server" CssClass="d-none" ImageUrl="website/Images/check-mark.png" />
                                                                    <asp:Button ID="btnUpdateEmail" runat="server" Text="Update email address" CssClass="button d-none" Enabled="true"
                                                                        OnClick="btnUpdateEmail_Click" CausesValidation="true" OnClientClick="return VerifyEmailAdd();javascript:show_progress();" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <h5 class="Dtit"><span class="DOrangehead">Passport/Driving Licence details</span></h5>

                                            <div class="card border-0 card-body">
                                                <div class="form-group row">
                                                    <label for="inputtext" class="col-sm-6 col-form-label">
                                                        Identity details<span class="lblman">*</span>
                                                    </label>
                                                    <div class="col-sm-6 align-self-center">
                                                        <asp:RadioButtonList ID="rblIdentity" runat="server" CssClass="bodytextbold1 lblalign"
                                                            RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblIdentity_SelectedIndexChanged">
                                                            <asp:ListItem Text="Driving Licence" Value="DRIVING LICENCE">&nbsp; Driving Licence &nbsp;</asp:ListItem>
                                                            <asp:ListItem Text="Passport" Value="PASSPORT">&nbsp; Passport </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>

                                                <asp:Panel ID="pnlPassport" runat="server" Visible="false">
                                                    <div class="form-group row">
                                                        <label for="inputtext" class="col-sm-6 col-form-label">
                                                            Passport<span class="lblman">*</span>
                                                        </label>
                                                        <div class="col-sm-6 align-self-center">

                                                            <asp:RadioButtonList ID="rblPassport" runat="server" CssClass="bodytextbold1 lblalign"
                                                                RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblPassport_SelectedIndexChanged">
                                                                <asp:ListItem Text="UK Passport" Value="UK">&nbsp; UK Passport &nbsp;</asp:ListItem>
                                                                <asp:ListItem Text="Non-UK Passport" Value="IT">&nbsp; Non-UK Passport</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlDIdentityNo" runat="server" Visible="false">
                                                    <div class="form-group row">
                                                        <label for="inputtext" class="col-sm-6 col-form-label">
                                                            Driving Licence Type<span class="lblman">*</span>
                                                        </label>
                                                        <div class="col-sm-6 align-self-center">

                                                            <asp:RadioButtonList ID="rbldltype" runat="server" CssClass="bodytextbold1 lblalign"
                                                                RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Photo Driving Licence" Value="P">&nbsp; Photo DL &nbsp;</asp:ListItem>
                                                                <asp:ListItem Text="Old Driving Licence" Value="O">&nbsp; Old DL</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="inputtext" class="col-sm-6 col-form-label">
                                                            Driving Licence Number<span class="lblman">*</span>
                                                        </label>
                                                        <div class="col-sm-2 pr-0">

                                                            <cc1:MacroWebTextBox ID="txtDRLNo1" runat="server" BlurBackground="White" CssClass="Ustxtbox form-control"
                                                                FocusBackground="" MaxLength="5" Validate="IsAlphaNum" onKeyPress="AutoTab('txtDRLNo1','txtDRLNo2','5',event);"></cc1:MacroWebTextBox>
                                                        </div>
                                                        <div class="col-sm-2 px-0">

                                                            <cc1:MacroWebTextBox ID="txtDRLNo2" runat="server" CssClass="Ustxtbox form-control" MaxLength="6"
                                                                Validate="IsAlphaNum" BlurBackground="White" FocusBackground="" onKeyPress="AutoTab('txtDRLNo2','txtDRLNo3','6',event);"></cc1:MacroWebTextBox>
                                                        </div>
                                                        <div class="col-sm-2 pl-0">

                                                            <cc1:MacroWebTextBox ID="txtDRLNo3" runat="server" CssClass="Ustxtbox form-control" MaxLength="5"
                                                                Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label for="inputtext" class="col-sm-6 col-form-label">
                                                            Date of expiry <span class="lblman">*</span>
                                                        </label>
                                                        <div class="col-sm-6">

                                                            <cc1:MacroWebCalendar ID="calDVLCDOE" runat="server" AddYear="40" Sorting="Des" SubtractYear="0"
                                                                CssClass="sselectbox1 border-1 " AutoPostBack="False" />
                                                            <cc1:MacroWebTextBox ID="txtDVLCDOE" runat="server" CssClass="txtbox11 visible-hidden"
                                                                BlurBackground="" FocusBackground="" Validate="None"></cc1:MacroWebTextBox>

                                                            <asp:Label ID="lblDLDEerr" runat="server" Text="" CssClass="text-danger font-12"></asp:Label>

                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label for="inputtext" class="col-sm-6 col-form-label">
                                                            Postcode on License <span class="lblman">*</span>
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <cc1:MacroWebTextBox ID="txtDVLCPcode" runat="server" BlurBackground="White" CssClass="Utxtbox form-control"
                                                                FocusBackground="" MaxLength="10" Validate="IsAlphaNum"></cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>

                                                </asp:Panel>



                                                <%-- <tr>
                                      <td class="lblwidth lbltxt" align="left">
                                          Driving Licence Number<span class="lblman">*</span>
                                      </td>
                                      <td class="txtbwidth">
                                          <table cellpadding="0" cellspacing="0" class="aln_left">
                                              <tr>
                                                  <td align="left" valign="middle">
                                                      <cc1:MacroWebTextBox ID="txtDRLNo1" runat="server" BlurBackground="White" CssClass="Ustxtbox"
                                                          FocusBackground="" MaxLength="5" Validate="IsAlphaNum" onKeyPress="OnKeyUp(this,'txtDRLNo2','5');"></cc1:MacroWebTextBox>
                                                  </td>
                                                  <td align="left" valign="middle">
                                                      <cc1:MacroWebTextBox ID="txtDRLNo2" runat="server" CssClass="Ustxtbox" MaxLength="6"
                                                          Validate="IsAlphaNum" BlurBackground="White" FocusBackground="" onKeyPress="OnKeyUp(this,'txtDRLNo3','6');"></cc1:MacroWebTextBox>
                                                  </td>
                                                  <td align="left" valign="middle">
                                                      <cc1:MacroWebTextBox ID="txtDRLNo3" runat="server" CssClass="Ustxtbox" MaxLength="5"
                                                          Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                  </td>
                                              </tr>
                                          </table>
                                      </td>
                                      </tr>--%>



                                                <asp:Panel ID="pnlUKPassport" runat="server" Visible="false">
                                                    <div class="form-group ">
                                                        <div class="col-lg-12 col-12">
                                                            <label class="form-label">
                                                                Passport Number First Line <span class="lblman">*</span> (Click
                                                    <asp:Literal ID="LitHelpUK1" runat="server"></asp:Literal>
                                                                for more help)
                                                            </label>
                                                        </div>

                                                        <div class="col-lg-12 col-12">
                                                            <cc1:MacroWebTextBox ID="txtUKPassportL1" runat="server" CssClass="txtbox inputs colorwht pp-width-8"
                                                                MaxLength="2" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtUKPassportL2" runat="server" CssClass="txtbox inputs colorwht pp-width-12"
                                                                MaxLength="3" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtUKPassportL3" runat="server" CssClass="txtbox inputs colorwht pp-width-70"
                                                                MaxLength="39" Validate="IsAlphaNum" BlurBackground="White" FocusBackground="">
                                                            </cc1:MacroWebTextBox>
                                                        </div>
                                                        <%--<cc1:MacroWebTextBox ID="txtUKPassportL1" runat="server" CssClass="txtbox" MaxLength="2"
                                                Validate="IsAlphaNum" BlurBackground="White" Width="8%" FocusBackground="" onKeyPress="OnKeyUp(this,'txtUKPassportL2','2');"></cc1:MacroWebTextBox>
                                                <cc1:MacroWebTextBox ID="txtUKPassportL2" runat="server" CssClass="txtbox" MaxLength="3"
                                                Validate="IsAlphaNum" BlurBackground="White" Width="12%" FocusBackground="" onKeyPress="OnKeyUp(this,'txtUKPassportL3','3');"></cc1:MacroWebTextBox>
                                                <cc1:MacroWebTextBox ID="txtUKPassportL3" runat="server" CssClass="txtbox" MaxLength="39" 
                                                Validate="IsAlphaNum" BlurBackground="White" Width="70%" FocusBackground="">  onKeyPress="AutoTab('txtUKPassportL1','txtUKPassportL2','2',event);" onKeyPress="AutoTab('txtUKPassportL2','txtUKPassportL3','3',event);"
                                                </cc1:MacroWebTextBox>--%>
                                                        <%--<cc1:MacroWebTextBox ID="MacroWebTextBox10" runat="server" MaxLength="1" CssClass="inputs"></cc1:MacroWebTextBox>
                                                <cc1:MacroWebTextBox ID="MacroWebTextBox11" runat="server" MaxLength="3" CssClass="inputs"></cc1:MacroWebTextBox>
                                                <cc1:MacroWebTextBox ID="MacroWebTextBox12" runat="server" MaxLength="3" CssClass="inputs"></cc1:MacroWebTextBox>
                                                <cc1:MacroWebTextBox ID="MacroWebTextBox13" runat="server" MaxLength="4" CssClass="inputs"></cc1:MacroWebTextBox>--%>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-12 col-12">
                                                            <label class="form-label">Example </label>
                                                        </div>
                                                        <div class="col-lg-12 col-12">
                                                            <cc1:MacroWebTextBox ID="txtUKPassportL1ex" runat="server" CssClass="txtbox colorred pp-width-8"
                                                                MaxLength="2" Enabled="false" Text="P<" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtUKPassportL2ex" runat="server" CssClass="txtbox colorred pp-width-12"
                                                                MaxLength="3" Enabled="false" Text="GBR" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtUKPassportL3ex" runat="server" CssClass="txtbox colorred pp-width-70"
                                                                MaxLength="39" Enabled="false" Text="SKIMPOLE<<HAROLD<JOHN<<<<<<<<<<<<<<<<<<"
                                                                BlurBackground="White" FocusBackground="">
                                                            </cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-12 col-12">
                                                            <label class="form-label">
                                                                Passport Number Second Line <span class="lblman">*</span> (Click
                                                <asp:Literal ID="LitHelpUK" runat="server"></asp:Literal>
                                                                for more help)
                                                            </label>
                                                        </div>

                                                        <div class="col-lg-12 col-12">
                                                            <cc1:MacroWebTextBox ID="txtUKPassport1" runat="server" CssClass="txtbox inputs colorwht pp-width-85"
                                                                MaxLength="10" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""
                                                                onKeyPress="AutoTab('txtUKPassport1','txtUKPassport2','10',event);"></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtUKPassport2" runat="server" CssClass="txtbox inputs colorwht pp-width-35"
                                                                MaxLength="3" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""
                                                                onKeyPress="AutoTab('txtUKPassport2','txtUKPassport3','3',event);"></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtUKPassport3" runat="server" CssClass="txtbox inputs colorwht pp-width-75"
                                                                MaxLength="7" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""
                                                                onKeyPress="AutoTab('txtUKPassport3','txtUKPassport4','7',event);"></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtUKPassport4" runat="server" CssClass="txtbox inputs colorwht pp-width-25"
                                                                MaxLength="1" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""
                                                                onKeyPress="AutoTab('txtUKPassport4','txtUKPassport5','1',event);"></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtUKPassport5" runat="server" CssClass="txtbox inputs colorwht pp-width-65"
                                                                MaxLength="7" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""
                                                                onKeyPress="AutoTab('txtUKPassport5','txtUKPassport6','7',event);"></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtUKPassport6" runat="server" CssClass="txtbox inputs colorwht pp-width-115"
                                                                MaxLength="14" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""
                                                                onKeyPress="AutoTab('txtUKPassport6','txtUKPassport7','14',event);"></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtUKPassport7" runat="server" CssClass="txtbox inputs colorwht pp-width-30"
                                                                MaxLength="2" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class="form-label">Example: </label>
                                                        </div>
                                                        <div class="col-lg-12 col-12">
                                                            <cc1:MacroWebTextBox ID="txtUKPassport1ex" runat="server" CssClass="txtbox colorred pp-width-85"
                                                                MaxLength="10" Enabled="false" Text="0876543245" BlurBackground="White"
                                                                FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtUKPassport2ex" runat="server" CssClass="txtbox colorred pp-width-35"
                                                                MaxLength="3" Enabled="false" Text="GBR" BlurBackground="White"
                                                                FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtUKPassport3ex" runat="server" CssClass="txtbox colorred pp-width-75"
                                                                MaxLength="7" Enabled="false" Text="7609245" BlurBackground="White"
                                                                FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtUKPassport4ex" runat="server" CssClass="txtbox colorred pp-width-25"
                                                                MaxLength="1" Enabled="false" Text="M" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtUKPassport5ex" runat="server" CssClass="txtbox colorred pp-width-65"
                                                                MaxLength="7" Enabled="false" Text="0810259" BlurBackground="White"
                                                                FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtUKPassport6ex" runat="server" CssClass="txtbox colorred pp-width-115"
                                                                MaxLength="14" Enabled="false" Text="<<<<<<<<<<<<<<" BlurBackground="White"
                                                                FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtUKPassport7ex" runat="server" CssClass="txtbox colorred pp-width-30"
                                                                MaxLength="2" Enabled="false" Text="08" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>


                                                </asp:Panel>
                                                <asp:Panel ID="pnlIntPassport" runat="server" Visible="false">

                                                    <div class="form-group">
                                                        <label class="form-label">
                                                            Passport Number First Line <span class="lblman">*</span> (Click
                                        <asp:Literal ID="LitHelpINT1" runat="server"></asp:Literal>
                                                            for more help)</label>
                                                        <div class="col-lg-12 col-12">
                                                            <%--<cc1:MacroWebTextBox ID="txtIntPassportL1" runat="server" CssClass="txtbox" MaxLength="2"
                                                Validate="IsAlphaNum" BlurBackground="White" Width="8%" FocusBackground="" onKeyPress="OnKeyUp(this,'txtIntPassportL2','2');"></cc1:MacroWebTextBox>
                                                <cc1:MacroWebTextBox ID="txtIntPassportL2" runat="server" CssClass="txtbox" MaxLength="3"
                                                Validate="IsAlphaNum" BlurBackground="White" Width="12%" FocusBackground="" onKeyPress="OnKeyUp(this,'txtIntPassportL3','3');"></cc1:MacroWebTextBox>
                                                <cc1:MacroWebTextBox ID="txtIntPassportL3" runat="server" CssClass="txtbox" MaxLength="39"
                                                Validate="IsAlphaNum" BlurBackground="White" Width="70%" FocusBackground=""></cc1:MacroWebTextBox>--%>
                                                            <cc1:MacroWebTextBox ID="txtIntPassportL1" runat="server" CssClass="txtbox inputs colorwht pp-width-8"
                                                                MaxLength="2" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""
                                                                onKeyPress="AutoTab('txtIntPassportL1','txtIntPassportL2','2',event);"></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtIntPassportL2" runat="server" CssClass="txtbox inputs colorwht pp-width-12"
                                                                MaxLength="3" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""
                                                                onKeyPress="AutoTab('txtIntPassportL2','txtIntPassportL3','3',event);"></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtIntPassportL3" runat="server" CssClass="txtbox inputs colorwht pp-width-70"
                                                                MaxLength="39" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-12 col-12">
                                                            <label class="form-label">Example </label>
                                                        </div>
                                                        <div class="col-lg-12 col-12">
                                                            <cc1:MacroWebTextBox ID="txtIntPassportL1ex" runat="server" CssClass="txtbox colorred pp-width-8"
                                                                MaxLength="2" Text="P<" Enabled="false" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtIntPassportL2ex" runat="server" CssClass="txtbox colorred pp-width-12"
                                                                MaxLength="3" Text="IND" Enabled="false" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtIntPassportL3ex" runat="server" CssClass="txtbox colorred pp-width-70"
                                                                MaxLength="39" Text="SHARMA<<GANESH<<<<<<<<<<<<<<<<<<<<<<<<<" Enabled="false"
                                                                BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group ">
                                                        <label class="form-label">
                                                            Passport Number Second Line <span class="lblman">*</span> (Click
                                        <asp:Literal ID="LitHelpINT" runat="server"></asp:Literal>
                                                            for more help)
                                                        </label>
                                                        <div class="col-lg-12 col-12">
                                                            <%--<cc1:MacroWebTextBox ID="txtIntPassport1" runat="server" CssClass="txtbox" MaxLength="9"
                                                Validate="IsAlphaNum" BlurBackground="White" Width="75px" FocusBackground=""
                                                onKeyPress="OnKeyUp(this,'txtIntPassport2','9');"></cc1:MacroWebTextBox>
                                                <cc1:MacroWebTextBox ID="txtIntPassport2" runat="server" CssClass="txtbox" MaxLength="1"
                                                Validate="IsAlphaNum" BlurBackground="White" Width="30px" FocusBackground=""
                                                onKeyPress="OnKeyUp(this,'txtIntPassport3','1');"></cc1:MacroWebTextBox>
                                                <cc1:MacroWebTextBox ID="txtIntPassport3" runat="server" CssClass="txtbox" MaxLength="3"
                                                Validate="IsAlphaNum" BlurBackground="White" Width="35px" FocusBackground=""
                                                onKeyPress="OnKeyUp(this,'txtIntPassport4','3');"></cc1:MacroWebTextBox>
                                                <cc1:MacroWebTextBox ID="txtIntPassport4" runat="server" CssClass="txtbox" MaxLength="7"
                                                Validate="IsAlphaNum" BlurBackground="White" Width="70px" FocusBackground=""
                                                onKeyPress="OnKeyUp(this,'txtIntPassport5','7');"></cc1:MacroWebTextBox>
                                                <cc1:MacroWebTextBox ID="txtIntPassport5" runat="server" CssClass="txtbox" MaxLength="1"
                                                Validate="IsAlphaNum" BlurBackground="White" Width="30px" FocusBackground=""
                                                onKeyPress="OnKeyUp(this,'txtIntPassport6','1');"></cc1:MacroWebTextBox>
                                                <cc1:MacroWebTextBox ID="txtIntPassport6" runat="server" CssClass="txtbox" MaxLength="7"
                                                Validate="IsAlphaNum" BlurBackground="White" Width="65px" FocusBackground=""
                                                onKeyPress="OnKeyUp(this,'txtIntPassport7','7');"></cc1:MacroWebTextBox>
                                                <cc1:MacroWebTextBox ID="txtIntPassport7" runat="server" CssClass="txtbox" MaxLength="14"
                                                Validate="IsAlphaNum" BlurBackground="White" Width="115px" FocusBackground=""
                                                onKeyPress="OnKeyUp(this,'txtIntPassport8','14');"></cc1:MacroWebTextBox>
                                                <cc1:MacroWebTextBox ID="txtIntPassport8" runat="server" CssClass="txtbox" MaxLength="1"
                                                Validate="IsAlphaNum" BlurBackground="White" Width="15px" FocusBackground=""
                                                onKeyPress="OnKeyUp(this,'txtIntPassport9','1');"></cc1:MacroWebTextBox>
                                                <cc1:MacroWebTextBox ID="txtIntPassport9" runat="server" CssClass="txtbox" MaxLength="1"
                                                Validate="IsAlphaNum" BlurBackground="White" Width="15px" FocusBackground=""></cc1:MacroWebTextBox>--%>
                                                            <cc1:MacroWebTextBox ID="txtIntPassport1" runat="server" CssClass="txtbox inputs colorwht pp-width-75"
                                                                MaxLength="9" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""
                                                                onKeyPress="AutoTab('txtIntPassport1','txtIntPassport2','9',event);"></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtIntPassport2" runat="server" CssClass="txtbox inputs colorwht pp-width-30"
                                                                MaxLength="1" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""
                                                                onKeyPress="AutoTab('txtIntPassport2','txtIntPassport3','1',event);"></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtIntPassport3" runat="server" CssClass="txtbox inputs colorwht pp-width-35"
                                                                MaxLength="3" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""
                                                                onKeyPress="AutoTab('txtIntPassport3','txtIntPassport4','3',event);"></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtIntPassport4" runat="server" CssClass="txtbox inputs colorwht pp-width-75"
                                                                MaxLength="7" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""
                                                                onKeyPress="AutoTab('txtIntPassport4','txtIntPassport5','7',event);"></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtIntPassport5" runat="server" CssClass="txtbox inputs colorwht pp-width-30"
                                                                MaxLength="1" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""
                                                                onKeyPress="AutoTab('txtIntPassport5','txtIntPassport6','1',event);"></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtIntPassport6" runat="server" CssClass="txtbox inputs colorwht pp-width-65"
                                                                MaxLength="7" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""
                                                                onKeyPress="AutoTab('txtIntPassport6','txtIntPassport7','7',event);"></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtIntPassport7" runat="server" CssClass="txtbox inputs colorwht pp-width-115"
                                                                MaxLength="14" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""
                                                                onKeyPress="AutoTab('txtIntPassport7','txtIntPassport8','14',event);"></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtIntPassport8" runat="server" CssClass="txtbox inputs colorwht pp-width-15"
                                                                MaxLength="1" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""
                                                                onKeyPress="AutoTab('txtIntPassport8','txtIntPassport9','1',event);"></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="txtIntPassport9" runat="server" CssClass="txtbox inputs colorwht pp-width-15"
                                                                MaxLength="1" Validate="IsAlphaNum" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-12 col-12">
                                                            <label class="form-label">Example </label>
                                                        </div>
                                                        <div class="col-lg-12 col-12">
                                                            <cc1:MacroWebTextBox ID="MacroWebTextBox1" runat="server" CssClass="txtbox colorred pp-width-75"
                                                                MaxLength="9" Text="F1604526<" Enabled="false" BlurBackground="White"
                                                                FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="MacroWebTextBox2" runat="server" CssClass="txtbox colorred pp-width-30"
                                                                MaxLength="1" Text="3" Enabled="false" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="MacroWebTextBox3" runat="server" CssClass="txtbox colorred pp-width-35"
                                                                MaxLength="3" Text="IND" Enabled="false" BlurBackground="White"
                                                                FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="MacroWebTextBox4" runat="server" CssClass="txtbox colorred pp-width-75"
                                                                MaxLength="7" Text="5807075" Enabled="false" BlurBackground="White"
                                                                FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="MacroWebTextBox5" runat="server" CssClass="txtbox colorred pp-width-30"
                                                                MaxLength="1" Text="M" Enabled="false" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="MacroWebTextBox6" runat="server" CssClass="txtbox colorred pp-width-65"
                                                                MaxLength="7" Text="1501135" Enabled="false" BlurBackground="White"
                                                                FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="MacroWebTextBox7" runat="server" CssClass="txtbox colorred pp-width-115"
                                                                MaxLength="14" Text="<<<<<<<<<<<<<<" Enabled="false" BlurBackground="White"
                                                                FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="MacroWebTextBox8" runat="server" CssClass="txtbox colorred pp-width-15"
                                                                MaxLength="1" Text="<" Enabled="false" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                            <cc1:MacroWebTextBox ID="MacroWebTextBox9" runat="server" CssClass="txtbox colorred pp-width-15"
                                                                MaxLength="1" Text="0" Enabled="false" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>
                                                </asp:Panel>




                                                <asp:Panel ID="pnlCntry" runat="server" Visible="false">
                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class="form-label">Date of issue<span class="lblman">*</span></label>
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <cc1:MacroWebCalendar ID="calDOI" runat="server" AddYear="0" Sorting="Des" SubtractYear="20"
                                                                CssClass="sselectbox1 border-1" AutoPostBack="False" />
                                                            <cc1:MacroWebTextBox ID="txtDOI" runat="server" CssClass="txtbox11 visible-hidden"
                                                                BlurBackground="" FocusBackground="" Validate="None"></cc1:MacroWebTextBox>
                                                            <asp:Label ID="lblDoissueerr" runat="server" Text="" CssClass="text-danger font-12"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class="form-label">Date of expiry<span class="lblman">*</span></label>
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <cc1:MacroWebCalendar ID="calDOE" runat="server" AddYear="20" Sorting="Des" SubtractYear="0"
                                                                CssClass="sselectbox1 border-1 " AutoPostBack="False" />
                                                            <cc1:MacroWebTextBox ID="txtDOE" runat="server" CssClass="txtbox11 visible-hidden"
                                                                BlurBackground="" FocusBackground="" Validate="None"></cc1:MacroWebTextBox>
                                                            <asp:Label ID="lblDoExerr" runat="server" Text="" CssClass="text-danger font-12"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class="form-label">Country of Issue <span class="lblman">*</span></label>
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <asp:DropDownList ID="ddlPsPrtIsCntry" runat="server" CssClass="selectbox">
                                                                <asp:ListItem Selected="True" Value="-1">-- Select Country --</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                </asp:Panel>
                                            </div>

                                            <h5 class="Dtit"><span class="DOrangehead">Employment details</span></h5>
                                            <div class="card card-body border-0">
                                                <div class="form-group row">
                                                    <div class="col-lg-6 col-12">
                                                        <label class="form-label">Occupation<span class="lblman">*</span> </label>
                                                    </div>
                                                    <div class="col-lg-6 col-12">
                                                        <asp:DropDownList ID="ddlEmpType" runat="server" CssClass="selectbox" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlEmpType_SelectedIndexChanged">
                                                            <asp:ListItem Selected="True" Value="-1">-- Select Type --</asp:ListItem>
                                                            <asp:ListItem>BUSINESS PERSON</asp:ListItem>
                                                            <asp:ListItem>SELF EMPLOYED</asp:ListItem>
                                                            <asp:ListItem>STUDENT</asp:ListItem>
                                                            <asp:ListItem>HOUSE WIFE</asp:ListItem>
                                                            <asp:ListItem>IT CONSULTANT</asp:ListItem>
                                                            <asp:ListItem>PAROLE AND PROBATIONARY OFFICER</asp:ListItem>
                                                            <asp:ListItem>OTHERS</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <asp:Panel ID="pnlEmpOth" runat="server" Visible="false">
                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class="form-label">Specify if Other<span class="lblman">*</span> </label>
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <cc1:MacroWebTextBox ID="txtEmptypOth" runat="server" CssClass="Utxtbox" MaxLength="30"
                                                                Validate="IsAlpha" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="border-blue">
                                            <asp:Panel ID="Panel1" runat="server" CssClass="w-100" Visible="true">
                                                <asp:Panel ID="pnlJntAplntAddr" runat="server" Visible="false" CssClass="addtop w-100">
                                                    <div class="card card-body">

                                                        <div class="form-group row">
                                                            <div class="col-lg-6 col-12">
                                                                <span class="DPinkhead">The joint applicant stays at the same address? </span>
                                                            </div>
                                                            <div class="col-lg-6 col-12 align-self-center">
                                                                <asp:RadioButtonList ID="rblJntApAddSts" runat="server" CssClass="bodytextbold2"
                                                                    RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblJntApAddSts_SelectedIndexChanged">
                                                                    <asp:ListItem Text="Yes" Value="YES">&nbsp;Yes</asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="NO" Selected="True">&nbsp;No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>


                                                    </div>
                                                </asp:Panel>
                                            </asp:Panel>

                                            <asp:Panel ID="pnlAddrDtls" runat="server" CssClass="w-100" Visible="true">
                                                <h5 class="Dtit"><span class="DOrangehead">Current Address</span>  </h5>
                                                <div class="card card-body border-0">
                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class=" form-label">Postal code<span class="lblman">*</span></label>
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <div class="row">
                                                                <div class="col-lg-8">
                                                                    <cc1:MacroWebTextBox ID="txtCurPostCode" runat="server" BlurBackground="White" CssClass="Utxtbox form-control"
                                                                        FocusBackground="" MaxLength="8" Validate="IsAlphaNum"></cc1:MacroWebTextBox>
                                                                </div>
                                                                <div class="col-lg-4">
                                                                    <asp:Button ID="btnCurSearch" runat="server" Text="Search" CssClass="button" Visible="true"
                                                                        OnClientClick="return validate('txtCurPostCode');" CausesValidation="false" OnClick="btnCurSearch_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <asp:Panel ID="pnlAddCurSearch" runat="server" CssClass="w-100" Visible="false">
                                                        <h5 class="Dtit"><span class="DPinkhead">Address List </span></h5>
                                                        <div class="form-group row">
                                                            <asp:ListBox ID="lstCur" runat="server" AutoPostBack="false" CssClass="w-100 h-100"
                                                                OnSelectedIndexChanged="lstCur_SelectedIndexChanged"></asp:ListBox>
                                                            <asp:Button ID="btnPCScnclCur" runat="server" Text="Cancel" CssClass="button scbutton"
                                                                CausesValidation="false" OnClick="btnPCScnclCur_Click" />
                                                            &nbsp;
                                        <asp:Button ID="btnPCScur" runat="server" Text="Select" CssClass="button scbutton"
                                            CausesValidation="false" OnClick="btnPCScur_Click" />
                                                        </div>
                                                    </asp:Panel>

                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class=" form-label">House / Flat No.</label>
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <cc1:MacroWebTextBox ID="txtCurDrNo" runat="server" BlurBackground="White" CssClass="Utxtbox form-control"
                                                                FocusBackground="" MaxLength="50" Validate="IsAlphaNum"></cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class=" form-label">Building Name </label>
                                                            <img src='images/help.png' alt='help' title="Please fill in the 'House/Flat No' or 'Building Name' details, as applicable." data-bs-toggle="tooltip" />
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <cc1:MacroWebTextBox ID="txtCurAddr1" runat="server" BlurBackground="White" CssClass="form-control Utxtbox"
                                                                FocusBackground="" MaxLength="50" Validate="IsAlphaNum"></cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class=" form-label">Street<span class="lblman">*</span> </label>
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <cc1:MacroWebTextBox ID="txtCurAddr2" runat="server" BlurBackground="White" CssClass="Utxtbox form-control"
                                                                FocusBackground="" MaxLength="50" Validate="IsAlphaNum"></cc1:MacroWebTextBox>

                                                            <%--<cc1:MacroWebTextBox ID="txtCurAddr3" runat="server" BlurBackground="White" CssClass="Utxtbox"
                              FocusBackground="" MaxLength="50" Validate="IsAlphaNum"></cc1:MacroWebTextBox>--%>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class=" form-label">City <span class="lblman">*</span> </label>
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <asp:DropDownList ID="ddlCurCity" runat="server" CssClass="selectbox form-control">
                                                                <asp:ListItem Selected="True" Value="0">-- Select City --</asp:ListItem>
                                                            </asp:DropDownList>

                                                            <asp:Label ID="lblCiryErr" runat="server" CssClass="text-danger font-12" Text=""></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class=" form-label">County  </label>
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <cc1:MacroWebTextBox ID="txtCurCounty" runat="server" BlurBackground="White" CssClass="Utxtbox form-control"
                                                                FocusBackground="" MaxLength="50" Validate="IsAlphaNum"></cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class="form-label">Country<span class="lblman">*</span>  </label>
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <asp:DropDownList ID="ddlCurCountry" Enabled="false" runat="server" CssClass="selectbox form-control">
                                                                <asp:ListItem Selected="True" Value="0">-- Select Country --</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class=" form-label">Residing since<span class="lblman">*</span>  </label>
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                                PopupButtonID="ImageButton2" TargetControlID="txtRsdnSnc" Enabled="True" />
                                                            <cc1:MacroWebTextBox ID="txtRsdnSnc" runat="server" MaxLength="10" Validate="IsDate"
                                                                AutoPostBack="True" BlurBackground="White" CssClass="w-75 Utxtbox pl-2" FocusBackground=""
                                                                OnTextChanged="txtRsdnSnc_TextChanged"></cc1:MacroWebTextBox>
                                                            <asp:ImageButton ID="ImageButton2" runat="server" AlternateText="Click to show calendar"
                                                                ImageUrl="~/images/Calendar_scheduleHS.png" CausesValidation="False"
                                                                ToolTip="Click to show calendar" />
                                                            <br />
                                                            <asp:Label ID="lblResErr" runat="server" Text="" CssClass="text-danger font-12"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlPreAddr" runat="server" CssClass="w-100" Visible="false">
                                                <h5 class="Dtit"><span class="DOrangehead">Previous Address</span>  </h5>
                                                <div class="card card-body border-0">
                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class=" form-label">Postal code<span class="lblman">*</span></label>
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <div class="row">
                                                                <div class="col-lg-8">
                                                                    <cc1:MacroWebTextBox ID="txtPrePostCode" runat="server" BlurBackground="White" CssClass="Utxtbox form-control "
                                                                        FocusBackground="" MaxLength="8" Validate="IsAlphaNum"></cc1:MacroWebTextBox>
                                                                </div>
                                                                <div class="col-lg-4">
                                                                    <asp:Button ID="btnPreSearch" runat="server" Text="Search" CssClass="button" Visible="true"
                                                                        OnClientClick="return validate('txtPrePostCode');" CausesValidation="false" OnClick="btnPreSearch_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <asp:Panel ID="pnlAddPreSearch" runat="server" CssClass="mb-3" Visible="false">
                                                        <div class="">
                                                            <h5 class="Dtit mb-0"><span class="DPinkhead">Address List </span></h5>
                                                            <asp:ListBox ID="lstPre" runat="server" AutoPostBack="false" CssClass="w-100 h-100"
                                                                OnSelectedIndexChanged="lstPre_SelectedIndexChanged"></asp:ListBox>
                                                        </div>
                                                        <div class="mt-3">
                                                            <center>
                                                                <asp:Button ID="btnPCScnclPre" runat="server" Text="Cancel" CssClass="button scbutton" CausesValidation="false"
                                                                    OnClick="btnPCScnclPre_Click" />

                                                                <asp:Button ID="btnPCSPre" runat="server" Text="Select" CssClass="button scbutton" CausesValidation="false"
                                                                    OnClick="btnPCSPre_Click" />
                                                            </center>
                                                        </div>
                                                    </asp:Panel>

                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class=" form-label">House / Flat No.</label>
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <cc1:MacroWebTextBox ID="txtPreDrNo" runat="server" BlurBackground="White" CssClass="form-control Utxtbox"
                                                                FocusBackground="" MaxLength="50" Validate="IsAlphaNum"></cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class="form-label">Building Name </label>
                                                            <img src='images/help.png' alt='help' title="Please fill in the 'House/Flat No' or 'Building Name' details, as applicable." data-bs-toggle="tooltip" />
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <cc1:MacroWebTextBox ID="txtPreAddr1" runat="server" BlurBackground="White" CssClass="form-control Utxtbox"
                                                                FocusBackground="" MaxLength="50" Validate="IsAlphaNum"></cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class="form-label">Street<span class="lblman">*</span> </label>
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <cc1:MacroWebTextBox ID="txtPreAddr2" runat="server" BlurBackground="White" CssClass="Utxtbox form-control"
                                                                FocusBackground="" MaxLength="50" Validate="IsAlphaNum"></cc1:MacroWebTextBox>

                                                            <%--<cc1:MacroWebTextBox ID="txtCurAddr3" runat="server" BlurBackground="White" CssClass="Utxtbox"
                              FocusBackground="" MaxLength="50" Validate="IsAlphaNum"></cc1:MacroWebTextBox>--%>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class=" form-label">City <span class="lblman">*</span> </label>
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <asp:DropDownList ID="ddlPreCity" runat="server" CssClass="selectbox form-control">
                                                                <asp:ListItem Selected="True" Value="0">-- Select City --</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class=" form-label">County  </label>
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <cc1:MacroWebTextBox ID="txtPreCounty" runat="server" BlurBackground="White" CssClass="Utxtbox form-control"
                                                                FocusBackground="" MaxLength="50" Validate="IsAlphaNum"></cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class=" form-label">Country<span class="lblman">*</span>  </label>
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <asp:DropDownList ID="ddlPreCountry" runat="server" CssClass="selectbox form-control">
                                                                <asp:ListItem Selected="True" Value="0">-- Select Country --</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel2" runat="server" CssClass="w-100" Visible="true">
                                                <h5 class="Dtit"><span class="DPinkhead">Tax Liabilities </span></h5>
                                                <div class="card card-body border-0">
                                                    <div class="form-group row">
                                                        <div class="col-lg-9 col-12">
                                                            <label class="form-label">Are you a resident for tax purposes in more than one country? <span class="lblman">*</span></label>
                                                        </div>
                                                        <div class="col-lg-3 col-12 align-self-center">
                                                            <asp:RadioButtonList ID="rblUSPerson" runat="server" CssClass="bodytextbold1 lblalign"
                                                                RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblUSPerson_SelectedIndexChanged">
                                                                <asp:ListItem Text="Yes" Value="Yes">&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
                                                                <asp:ListItem Text="No" Value="No">&nbsp;No&nbsp;</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>



                                                    <asp:Panel ID="pnlUSperson" runat="server" CssClass="w-100" Visible="false">
                                                        <div class="row">
                                                            <div class="col-lg-6 col-12">
                                                                <div class="form-group ">
                                                                    <label class="form-label">Primary Jurisdiction <span class="lblman">*</span> </label>
                                                                    <%-- <cc1:MacroWebTextBox ID="txtPriJuri" runat="server" BlurBackground="White" CssClass="Utxtbox"
                                            FocusBackground="" MaxLength="50" Validate="IsAlphaNum"></cc1:MacroWebTextBox>--%>
                                                                    <asp:DropDownList ID="ddlPriJuri" runat="server" CssClass="selectbox form-control">
                                                                        <asp:ListItem Selected="True" Value="0">-- Select Country --</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-6 col-12">
                                                                <div class="form-group">
                                                                    <label class="form-label">TIN <span class="lblman">*</span> </label>
                                                                    <cc1:MacroWebTextBox ID="txtTin1" runat="server" BlurBackground="White" CssClass="Utxtbox form-control"
                                                                        FocusBackground="" MaxLength="50" Validate="IsAlphaNum"></cc1:MacroWebTextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-6 col-12">
                                                                <div class="form-group">
                                                                    <label class="form-label">Additional Jurisdiction <span class="lblman">*</span> </label>
                                                                    <asp:DropDownList ID="ddlAddJuri" runat="server" CssClass="selectbox form-control">
                                                                        <asp:ListItem Selected="True" Value="0">-- Select Country --</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-6 col-12">
                                                                <div class="form-group">
                                                                    <label class="form-label">TIN </label>
                                                                    <cc1:MacroWebTextBox ID="txtTin2" runat="server" BlurBackground="White" CssClass="Utxtbox form-control"
                                                                        FocusBackground="" MaxLength="50" Validate="IsAlphaNum"></cc1:MacroWebTextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-6 col-12">
                                                                <div class="form-group">
                                                                    <label class="form-label">Additional Jurisdiction </label>
                                                                    <asp:DropDownList ID="ddlAddJuri1" runat="server" CssClass="selectbox form-control">
                                                                        <asp:ListItem Selected="True" Value="0">-- Select Country --</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-6 col-12">
                                                                <div class="form-group">
                                                                    <label class="form-label">TIN </label>
                                                                    <cc1:MacroWebTextBox ID="txtTin3" runat="server" BlurBackground="White" CssClass="Utxtbox form-control"
                                                                        FocusBackground="" MaxLength="50" Validate="IsAlphaNum"></cc1:MacroWebTextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-6 col-12">
                                                                <p class="text-justify">
                                                                    Please Specify ALL jurisdictions in which you are resident for the purposes of income
                                            tax. If you have more than one jurisdiction of tax residency please list each one.
                                                                </p>
                                                            </div>

                                                            <div class="col-lg-6 col-12">
                                                                <p class="text-justify">
                                                                    TIN: This is the tax identification number or equivilent that your country of residence
                            for tax purposes has issued you. This would include, for example, a National Insurance
                            Number.
                                                                </p>
                                                            </div>

                                                            <div class="col-lg-12 col-12">
                                                                <label class="form-label">
                                                                    Reasons for not being able to provide TIN(s) <font class="text-theme">[50
                                            Characters only]</font>
                                                                </label>
                                                                <cc1:MacroWebTextBox ID="txtReasonTin" runat="server" BlurBackground="White" CssClass="UtxtboxC form-control"
                                                                    FocusBackground="" MaxLength="50" Validate="IsAlphaNum"></cc1:MacroWebTextBox>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </asp:Panel>

                                            <asp:Panel ID="Panel3" runat="server" CssClass="w-100" Visible="true">
                                                <h5 class="Dtit"><span class="DPinkhead">Foreign Account Tax Compliant Act (FATCA) </span></h5>
                                                <div class="card card-body border-0">
                                                    <p class="text-justify font-16 text-secondary">
                                                        Under the Intergovernmental Agreement between the UK and US tax authorities (HMRC
                                    and IRS respectively) relating to the implementation of FATCA, the Bank is required
                                    to disclose information to HMRC in relation to accounts and/or account holders who
                                    may be liable to pay tax in the USA. You are therefore requested to answer the following
                                    questions.
                                                    </p>

                                                    <div class="form-group row">
                                                        <div class="col-lg-9">
                                                            <label class="form-label">To the best of your knowledge, are you liable to pay tax in the USA? <span class="lblman">*</span></label>
                                                        </div>
                                                        <div class="col-lg-3 align-self-center">
                                                            <asp:RadioButtonList ID="rblPayTax" runat="server" CssClass="bodytextbold1 lblalign"
                                                                RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Yes" Value="Yes">&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
                                                                <asp:ListItem Text="No" Value="No">&nbsp;No&nbsp;</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>

                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-9">
                                                            <label class="form-label">Are you a US citizen, whether by birth or naturalization or hold a US passport? <span class="lblman">*</span></label>
                                                        </div>
                                                        <div class="col-lg-3 align-self-center">
                                                            <asp:RadioButtonList ID="rblUSCitizen" runat="server" CssClass="bodytextbold1 lblalign"
                                                                RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Yes" Value="Yes">&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
                                                                <asp:ListItem Text="No" Value="No">&nbsp;No&nbsp;</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>

                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-9">
                                                            <label class="form-label">Do you hold a "Green Card"? <span class="lblman">*</span></label>
                                                        </div>
                                                        <div class="col-lg-3 align-self-center">
                                                            <asp:RadioButtonList ID="rblGreenCard" runat="server" CssClass="bodytextbold1 lblalign"
                                                                RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Yes" Value="Yes">&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
                                                                <asp:ListItem Text="No" Value="No">&nbsp;No&nbsp;</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>

                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-9">
                                                            <label class="form-label">Do you own real estate within the USA? <span class="lblman">*</span></label>
                                                        </div>
                                                        <div class="col-lg-3 align-self-center">
                                                            <asp:RadioButtonList ID="rblRealEst" runat="server" CssClass="bodytextbold1 lblalign"
                                                                RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Yes" Value="Yes">&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
                                                                <asp:ListItem Text="No" Value="No">&nbsp;No&nbsp;</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>

                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-lg-9">
                                                            <label class="form-label">
                                                                Do you expect to receive into your account any income or proceeds of sale arising
                            from any assets held in the USA and for which you have not paid tax. <span class="lblman">*</span></label>
                                                        </div>
                                                        <div class="col-lg-3 align-self-center">
                                                            <asp:RadioButtonList ID="rblassets" runat="server" CssClass="bodytextbold1 lblalign"
                                                                RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Yes" Value="Yes">&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
                                                                <asp:ListItem Text="No" Value="No">&nbsp;No&nbsp;</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>

                                                    </div>
                                                </div>
                                            </asp:Panel>

                                            <h5 class="Dtit"><span class="DPinkhead">Source Of Fund</span> </h5>
                                            <div class="card card-body border-0">
                                                <div class="form-group row">
                                                    <div class="col-lg-6 col-12">
                                                        <label class="form-label">Source Of Fund <span class="lblman">*</span></label>
                                                    </div>
                                                    <div class="col-lg-6 col-12">
                                                        <asp:DropDownList ID="ddlSOF" runat="server" CssClass="selectbox" AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlSOF_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <asp:Panel ID="pnlSOFOth" runat="server" Visible="false">
                                                    <div class="form-group row">
                                                        <div class="col-lg-6 col-12">
                                                            <label class="form-label">Specify if Other<span class="lblman">*</span></label>
                                                        </div>
                                                        <div class="col-lg-6 col-12">
                                                            <cc1:MacroWebTextBox ID="txtSOFOth" runat="server" CssClass="Utxtbox" MaxLength="30"
                                                                Validate="IsAlpha" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                        </div>
                                                    </div>
                                                </asp:Panel>


                                            </div>



                                        </div>

                                    </div>

                                    <div class="d-flex justify-content-end mt-3">
                                        <asp:Label ID="lblAlert" runat="server" Text="" CssClass="text-danger font-weight-bold font-16"></asp:Label>
                                        <asp:Label ID="errormsg" runat="server" CssClass="p-3 text-center " Text=""></asp:Label>
                                    </div>
                                    <div class="d-flex justify-content-end mt-3">
                                        <asp:Button ID="btnCancel" runat="server" Text="Go to step 1 without any change"
                                            CssClass="button mr-2" OnClick="btnCancel_Click" Visible="false" CausesValidation="false" />
                                        <asp:Button ID="btnSave" runat="server" Text="Modify Applicant" CssClass="button mr-2"
                                            Visible="false" OnClientClick="return BtnClick();javascript:show_progress();"
                                            OnClick="btnSave_Click" CausesValidation="true" />
                                    </div>

                                    <div class="d-flex justify-content-end mt-3">
                                        <asp:Button ID="btnBackToStep1" runat="server" Text="Go to Step 1" CssClass="button mr-2"
                                            Enabled="true" CausesValidation="false" OnClick="btnBackToStep1_Click" />
                                        <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save & Return Later" CssClass="button mr-2"
                                            CausesValidation="true" OnClientClick="return SaveAndRetnLater();javascript:show_progress();"
                                            OnClick="btnSaveAsDraft_Click" />
                                        <asp:Button ID="btnNextb" runat="server" Text="Next" CssClass="button mr-2" Enabled="true"
                                            OnClientClick="return BtnClick();javascript:show_progress();" OnClick="btnNextb_Click"
                                            CausesValidation="true" />
                                        <%--<asp:Button ID="btnNextb" runat="server" Text="Next" CssClass="button" Enabled="true"
                                        OnClick="btnNextb_Click"   
                                        CausesValidation="true" />--%>
                                    </div>

                                    <asp:Button ID="btnModPopupEx2" runat="server" CssClass="d-none" />
                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnModPopupEx2"
                                        PopupControlID="PnlPwdReg" CancelControlID="btnOk" BackgroundCssClass="sctableBackground">
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


                                    <asp:Button ID="btnModelPopupDL" runat="server" CssClass="d-none" />
                                    <ajaxToolkit:ModalPopupExtender ID="MPEDataLose" runat="server" TargetControlID="btnModelPopupDL"
                                        PopupControlID="PnlMsgBox" BackgroundCssClass="sctableBackground">
                                    </ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="PnlMsgBox" runat="server" CssClass="d-none">
                                        <table cellpadding="0" cellspacing="0" class="table yellowborder bg-light clearpopup" align="center">
                                            <tr>
                                                <td class="pt-2 pb-2">
                                                    <p class="bodytext">
                                                        <asp:Label ID="lblDataLostMsg" runat="server"></asp:Label>
                                                    </p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="pb-3">
                                                    <asp:Button ID="btnOkDL" runat="server" Text="Yes" CssClass="button scbutton"
                                                        CausesValidation="false" OnClick="btnOkDL_Click" />
                                                    &nbsp;&nbsp;
                                        <asp:Button ID="btnCncl" runat="server" Text="No" CssClass="button scbutton"
                                            CausesValidation="false" OnClick="btnCncl_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>

                                    <%-- Popup new --%>
                                    <asp:Button ID="btnModelPopupPass" runat="server" CssClass="d-none" />
                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupPassAvl" runat="server" TargetControlID="btnModelPopupPass"
                                        PopupControlID="PnlPass" CancelControlID="btnOk" BackgroundCssClass="sctableBackground">
                                    </ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="PnlPass" runat="server" CssClass="d-none">
                                        <table class="csmain_tbl">
                                            <tr>
                                                <td align="center">
                                                    <table class="table h-50 bg-theme text-white">
                                                        <tr>
                                                            <td class="text-center"></td>
                                                            <td class="text-center">UBI ODT
                                                            </td>
                                                            <td class="text-center" align="right">
                                                                <%--<asp:LinkButton ID="lnkClose" runat="server" Text="" CssClass="linkclose" OnClick="lnkClose_Click" />--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="font-15 text-secondary p-2 lh-2">Kindly ensure to fill middle name, if available in your Passport or DVLA
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="p-2 d-block border-top border-white">
                                                    <asp:Button ID="btnPassAvl" runat="server" Text="OK" CssClass="scbutton" ValidationGroup="abc"
                                                        OnClick="btnPassAvl_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <%-- Popup new --%>

                                    <%-- Popup Modify --%>
                                    <asp:Button ID="btnModelPopupPassModfy" runat="server" CssClass="d-none" />
                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupPassAvlModfy" runat="server" TargetControlID="btnModelPopupPassModfy"
                                        PopupControlID="PnlPassModfy" CancelControlID="btnOk" BackgroundCssClass="sctableBackground">
                                    </ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="PnlPassModfy" runat="server" CssClass="d-none">
                                        <table class="csmain_tbl">
                                            <tr>
                                                <td align="center">
                                                    <table class="table h-50 bg-theme text-white">
                                                        <tr>
                                                            <td class="text-center"></td>
                                                            <td class="text-center">UBI ODT
                                                            </td>
                                                            <td class="text-center" align="right">
                                                                <%--<asp:LinkButton ID="lnkClose" runat="server" Text="" CssClass="linkclose" OnClick="lnkClose_Click" />--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="font-15 text-secondary p-2 lh-2">Kindly ensure to fill middle name, if available in your Passport or DVLA
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="p-2 d-block border-top border-white">
                                                    <asp:Button ID="btnPassAvlModfy" runat="server" Text="OK" CssClass="scbutton" ValidationGroup="abc"
                                                        OnClick="btnPassAvlModfy_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <%-- Popup Modify --%>

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
                                                    <%--<asp:Button ID="btnOTPReSend" runat="server" Text="Re-Send PIN" CssClass="btn btn-gn"
                                                                CausesValidation="false" ValidationGroup="OTPGroup" OnClientClick="return resendClick();" />
                                                            <asp:Button ID="btnOTPReSendEmail" runat="server" Text="Send OTP by email" CssClass="btn btn-gn"
                                                                CausesValidation="false" ValidationGroup="OTPGroup" Visible="false" OnClientClick="return resendEmailClick();" />--%>
                                                    <%--<asp:Button ID="btnOTPVerify" runat="server" Text="Verify" CssClass="scbutton"
                                                        CausesValidation="true" ValidationGroup="OTPGroup" UseSubmitBehavior="False" />--%>
                                                    <%--<asp:Button ID="btnOTPVerify" runat="server" Text="Validate" CausesValidation="true" CssClass="btn btn-gn btn-default"
                                                        TabIndex="4" OnClientClick="return OTPVerify();" />--%>

                                                    <asp:Button ID="btnOTPVerify" runat="server" Text="Verify" CssClass="scbutton" ValidationGroup="abc"
                                                        OnClick="btnOTPVerify_Click" />
                                                    <%--<asp:Button ID="btnOTPCancel" runat="server" CausesValidation="false" CssClass="scbutton"
                                                        Text="Cancel" />--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <%--OTP POPUP End--%>

                                    <%--OTP Update Confirmation popup--%>
                                    <asp:Button ID="btnModUpdEmailConfPopup" runat="server" CssClass="d-none" />
                                    <ajaxToolkit:ModalPopupExtender ID="ModalUpdEmailConf" runat="server" TargetControlID="btnModUpdEmailConfPopup"
                                        PopupControlID="PnlUpdEmailConf" CancelControlID="btnOk" BackgroundCssClass="sctableBackground">
                                    </ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="PnlUpdEmailConf" runat="server" CssClass="d-none" DefaultButton="btnOTPVerify">
                                        <table class="csmain_tbl m-auto">
                                            <tr>
                                                <td align="center">
                                                    <table class="table h-50 bg-theme text-white">
                                                        <tr>
                                                            <td class="text-center"></td>
                                                            <td class="text-center">
                                                                <h5 class="modal-title text-white text-center">UBI UPB - New Customer</h5>
                                                            </td>
                                                            <td class="text-center" align="right">
                                                                <%--<asp:LinkButton ID="lnkClose" runat="server" Text="" CssClass="linkclose" />--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pt-2 px-4">Updating your email address will require re-verification. Are you sure you want to proceed?                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="py-4">
                                                    <asp:Button ID="btnECUpdate" runat="server" Text="Update" CssClass="scbutton" ValidationGroup="abc"
                                                        OnClick="btnECUpdate_Click" />
                                                    <asp:Button ID="btnECCancel" runat="server" Text="Cancel" CssClass="scbutton" ValidationGroup="abc"
                                                        OnClick="btnECCancel_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <%--End--%>

                                    <%--Email block popup--%>
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
                                                                <h5 class="modal-title text-white text-center">UBI UPB - New Customer</h5>
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
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <uc3:FSCS ID="FSCS" runat="server" />
                            <uc2:footer ID="footer1" runat="server" />
                        </div>
                    </ContentTemplate>
                    <%--<Triggers>
                          <asp:AsyncPostBackTrigger ControlID="btnNextb" eventname="Click" />
                          </Triggers>--%>
                </asp:UpdatePanel>
            </div>
        </div>

        <%--<ajaxToolkit:ModalPopupExtender runat="server" PopupControlID="PanLoading" ID="ModalProgress"
                TargetControlID="PanLoading" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="PanLoading" runat="server" CssClass="updateProgress">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="5">
                    <ProgressTemplate>
                        <table  style="position: relative; top: 7%; margin: 0 auto; left: 0px; height: 100%; width: 260px;">
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




    <script type="text/javascript">
        //alert("test");
        //var FTxt = document.getElementById(obj).value;   $("input[type='text']").keyup(function () {

        $(document).ready(function () {
            PasspoValid();
        });
        function PasspoValid() {
            $(".inputs").keyup(function (e) {

                var code_val = e.keyCode ? e.keyCode : e.charCode;

                if (code_val == "8" || code_val == "9" || (code_val >= "35" && code_val <= "40") || code_val == "46") {
                    //alert("test1");
                    return true;
                }


                if (this.value.length == this.maxLength) {
                    //alert("test2");
                    var $next = $(this).next('.inputs');
                    if ($next.length)
                        $(this).next('.inputs').focus();
                    else
                        $(this).blur();
                }
            });
        }
    </script>

    <script type="text/javascript">
        //$(document).ready(function () {
        //if (parseInt($("#otpAttempt").val()) < 3) {
        //$('#lnkbtnResend').addClass('disabled-link');
        //setTimeout(function () { $('#lnkbtnResend').removeClass('disabled-link') }, 140000);
        //}
        //});
    </script>

    <script type="text/javascript" src="website/js/popper-min.js"></script>
    <script type="text/javascript" src="website/js/bootstrap.bundle.min.js"> </script>

    <script type="text/javascript" src="JS/CSP/Home.js" nonce="ihYxAijSER-YFSUxCDJbag"></script>

    <script type="text/javascript">
        var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
        var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl)
        })
    </script>

    <%--<script type="text/javascript">
       $(document).ready(function () {
           var timer; // Variable to store the timer
           var counter = <%= CounterFromDatabase %> // Counter value retrieved from the server
               console.log(counter);
                // Function to start the timer
                function startTimer() {
                    $("#resendButton").prop("disabled", true); // Disable resend button
                    $("#timer").text("Resend OTP in " + counter + " seconds");
                    timer = setInterval(function () {
                        counter--;
                        if (counter <= 0) {
                            clearInterval(timer); // Clear the timer
                            $("#resendButton").prop("disabled", false); // Enable resend button
                            $("#timer").text(""); // Clear timer text
                        } else {
                            $("#timer").text("Resend OTP in " + counter + " seconds");
                        }
                    }, 1000); // Update every second
                }

            // Trigger resend OTP when button is clicked
            $("#resendButton").click(function () {
                startTimer(); // Start the timer
            });

            // Start the timer on initial page load
            startTimer();
           

       });

      
   </script>--%>

    <script type="text/javascript">         
        <%-- $(document).ready(function () {
            // Click event handler for Resend OTP button
            $(document).on("click", "#<%= btnOTPVerify.ClientID %>", function () {
                console.log("resend start");
                var secondsLeft = parseInt($("#<%= hdnSecondsLeft.ClientID %>").val());
                console.log("seconds");
                console.log(secondsLeft);
                startTimer(secondsLeft);
                console.log("End");
            });
        });--%>


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

        //OCR Integration Start
        function validationBeforeDocumentSubmit(inputfile) {

            if (inputfile.files.length > 0) {
                if (inputfile.files.length > 1) {
                    alertIinfo("Only one file is permitted per upload. Please remove the additional file(s) and try again.");
                    return false;
                }

                if (chkFileTypes(inputfile) == false) {
                    alertIinfo("File format not supported. Please upload a valid file.");
                    return false;
                }
                else if (inputfile.files[0].size > 2 * 1024 * 1024) {
                    alertIinfo("File size cannot be greater than 2 MB.");
                    return false;
                }

                return true;
            }
            else { return false; }
        }

        function chkFileTypes(inputfile) {
            var fields = inputfile.files[0].name.split('.');
            var flen = fields.length;
            var filetype = fields[flen - 1];
            var fileAllowedTypes = ['jpg', 'jpeg', 'png', 'pdf'];
            if (fileAllowedTypes.length > 0) {
                for (var i = 0; i < fileAllowedTypes.length; i++) {
                    var FEData = fileAllowedTypes[i].replace('.', '');
                    if (filetype.toUpperCase() == FEData.toUpperCase()) {
                        return true;
                    }
                }
            }
            return false;
        }

        function alertIinfo(_msg) {
            document.getElementById('lblUploadError').innerHTML = _msg;
        }

        function showUploadProgress() {

            var el = document.getElementById('<%= ModalProgress.ClientID %>');
            // remove class
            el.classList.remove('d-none');
            el.classList.remove('sctableBackground');

            // add class
            el.classList.add('d-block');
        }

        // Upload Document Section
        document.getElementById('<%= FileUploader.ClientID %>').addEventListener('change', function () {

            document.getElementById('lblUploadError').innerHTML = '';
            document.getElementById('lblUploadInfo').innerHTML = '';

            if (validationBeforeDocumentSubmit(this)) {

                ////Func. for remove special characters in the form fields - blocks potentially dangerous input
                //cleanBeforeDocumentSubmit();
                ////Set File Name
                //document.getElementById('fileName').innerText = this.files[0]?.name || '';
                showUploadProgress();
                document.getElementById('<%= btnUpload.ClientID %>').click();
            }
        });
        //End
    </script>
</body>
</html>
