<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BondDetails.aspx.cs" Inherits="BondDetails" %>

<%@ Register Src="header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Assembly="MacroWebControls" Namespace="MacroWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
    <script language="javascript" type="text/javascript" src="JS/disbackbtn.js"></script>
    <link rel="Stylesheet" type="text/css" href="css/styles.css" />
    <link rel="Stylesheet" type="text/css" href="css/imghoverStyle.css" />
    <link type="text/css" href="css/WebResource.css" rel="stylesheet" />

    <link type="text/css" href="Website/custom-style.css" rel="Stylesheet" />

    <link href="website/css/bootstrap.min.css" rel="stylesheet" />

    <script type="text/javascript"> 
        function AutoTab(obj, NTxt, Len, e) {

            var FTxt = document.getElementById(obj).value;
            var code_val = e.keyCode ? e.keyCode : e.charCode;

            if (code_val == "8" || code_val == "9" || (code_val >= "35" && code_val <= "40") || code_val == "46") {
                return true;
            }

            if (FTxt.length == Len) {
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

        function OnKeyUp(obj, NTxt, Len) {

            var FTxt = document.getElementById(obj.id).value;
            if (FTxt.length == Len) {
                document.getElementById(NTxt).focus();
                return false;
            }
        }
    </script>
    <script type="text/javascript">

        function Check() {
            var chkPassport = document.getElementById("chkTermsAndConditions");
            var chkJoint = document.getElementById("chkIfJoint");
            var chkfscs = document.getElementById("chkFSCSCont");
            var chkPrvPL = document.getElementById("chkPrvPolicy");
            var chkTnc = document.getElementById("checkTnC");

            if (!chkPassport.checked) {

                alert("Please tick the terms & conditions for the further procedings");
                return false;
            }
            if (!chkPrvPL.checked) {
                alert("Please tick the privacy policy for the further procedings");
                return false;
            }
            if (!chkfscs.checked) {

                alert("Please Tick FSCS Information for the further procedings");
                return false;
            }
            if (!chkJoint.checked) {

                alert("Please tick Primary applicant accessibility control for the further procedings");
                return false;
            }
            if (!chkTnc.checked) {
                alert("Kindly give your consent for accepting the condition of no premature withdrawal");
                return false;
            }
        }

        function showPopup() {
            $find("mpe").show();
            return false;
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
    <script type="text/javascript" src="javascript/app.js"></script>
</head>
<body onload="AddRequestHandler()">
    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= ModalProgress.ClientID %>';
    </script>
    <form id="form1" runat="server" autocomplete="off">
        <script language="javascript" type="text/javascript" src="JS/jsUpdateProgress.js"></script>
        <div class="container">
            <ajaxToolkit:ToolkitScriptManager ID="ToolScriptManager1" runat="server">
            </ajaxToolkit:ToolkitScriptManager>


            <uc1:header ID="header" runat="server" />
            <hr class="hr" />

            <div class="px-2 py-2">
                <p class="lbltxt1">
                    Please fill in your details for opening your Union Premier Bond Account. Fields
                                marked with <font class="text-danger">* </font>are mandatory
                            <br />
                    Note: Session will expire if system remains inactive for 20 minutes
                </p>
            </div>

            <div class="row">
                <div class="d-flex justify-content-end">
                    <asp:LinkButton ID="lblExit" runat="server" Text="Exit" CssClass="text-danger"
                        CausesValidation="false" OnClientClick="return confirm('Your keyed in data will be lost. Do you really want to exit from the application ?');"
                        OnClick="lblExit_Click"></asp:LinkButton>
                </div>

                <div class="border-red px-0">
                    <img src="images/step/menu2.png" alt="" class="img-fluid hide" />
                </div>
                <p class="display">
                    Step3
                </p>
                <div class="faq-title p-2 ps-4">
                    Bond Details
                </div>
            </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="pt-3 text-center">
                        <asp:Label ID="lblRefNo" runat="server" Text="" CssClass="text-danger font-weight-bold"></asp:Label>

                        <asp:PlaceHolder ID="ErrorPH" runat="server"></asp:PlaceHolder>
                        <div class="text-center">
                            <asp:Button ID="btnLoadTestValues" runat="server" Text="Load Test Values" CssClass="button"
                                CausesValidation="false" OnClick="btnLoadTestValues_Click" Visible="false" />

                            <asp:Label ID="lblAlert" runat="server" Text="" CssClass="text-danger font-weight-bold font-16"></asp:Label>
                        </div>
                    </div>
                    <div class="border-blue w-75 mb-2">
                        <h5 class="Dtit"><span class="DPinkhead">Investment Details</span> </h5>
                        <div class="card border-0 card-body">
                            <div class="form-group row">
                                <div class="col-lg-6 col-12">
                                    <label class="form-label">Amount <span class="lblman">*</span> £ </label>
                                </div>
                                <div class="col-lg-6 col-12">
                                    <cc1:MacroWebTextBox ID="txtAmt" runat="server" CssClass="Ubtxtbox form-control" MaxLength="9"
                                        Validate="IsAmount" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-6 col-12">
                                    <label class="form-label">Period <span class="lblman">*</span> </label>
                                </div>
                                <div class="col-lg-6 col-12">
                                    <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="bselectbox form-control" OnSelectedIndexChanged="ddlPeriod_SelectedIndexChanged"
                                        AutoPostBack="true">
                                        <asp:ListItem Text="Select Priod" Value="-1" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-6 col-12">
                                    <label class="form-label">Rate of Interest (%) </label>
                                </div>
                                <div class="col-lg-6 col-12">
                                    <cc1:MacroWebTextBox ID="txtRateOfInt" runat="server" CssClass="Ubtxtbox form-control" MaxLength="14"
                                        Validate="IsAmount" ReadOnly="True"></cc1:MacroWebTextBox>
                                </div>
                            </div>
                        </div>

                        <h5 class="Dtit"><span class="DPinkhead">Details of Bank (other than UBI UK LTD)/ Building Society (Source of funding this Bond) </span></h5>
                        <div class="card border-0 card-body">
                            <div class="form-group row">
                                <div class="col-lg-6 col-12">
                                    <label class="form-label">Name of the Bank <span class="lblman">*</span> </label>
                                </div>
                                <div class="col-lg-6 col-12">
                                    <cc1:MacroWebTextBox ID="txtOthBankName" runat="server" CssClass="Ubtxtbox form-control" MaxLength="50"
                                        Validate="IsAlpha" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-6 col-12">
                                    <label class="form-label">Sort Code <span class="lblman">*</span> </label>
                                </div>
                                <div class="col-lg-6 col-12">
                                    <div class="row">
                                        <div class="col-3">
                                            <cc1:MacroWebTextBox ID="txtOthBankSC1" runat="server" CssClass="txtboxbond form-control " MaxLength="2"
                                                Validate="IsNumeric" FocusBackground="" onKeyPress="AutoTab('txtOthBankSC1','txtOthBankSC2','2',event);"></cc1:MacroWebTextBox>
                                        </div>
                                        <div class="col bodytext text-center">
                                            -
                                        </div>
                                        <div class="col-3">
                                            <cc1:MacroWebTextBox ID="txtOthBankSC2" runat="server" CssClass="txtboxbond form-control " MaxLength="2"
                                                Validate="IsNumeric" FocusBackground="" onKeyPress="AutoTab('txtOthBankSC2','txtOthBankSC3','2',event);"></cc1:MacroWebTextBox>
                                        </div>
                                        <div class="col text-center">
                                            -
                                        </div>
                                        <div class="col-3">
                                            <cc1:MacroWebTextBox ID="txtOthBankSC3" runat="server" CssClass="txtboxbond form-control" MaxLength="2"
                                                Validate="IsNumeric" FocusBackground=""></cc1:MacroWebTextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-6 col-12">
                                    <label class="form-label">Account Number <span class="lblman">*</span> </label>
                                </div>
                                <div class="col-lg-6 col-12">
                                    <cc1:MacroWebTextBox ID="txtOthBankAcNo" runat="server" CssClass="Ubtxtbox form-control" MaxLength="8"
                                        Validate="IsNumeric" FocusBackground=""></cc1:MacroWebTextBox>
                                </div>
                            </div>

                            <label class="form-label">The above account particulars will not be used for initiating Direct Debits.</label>
                            <p class="text-justify">
                                1) Please Send the funds from above mentioned account only
                            </p>
                            <p class="text-justify">
                                2) Account must be in your name or for joint A/Cs you should be one the account
                            holders
                            </p>
                            <p class="text-justify">
                                3) On Maturity, fund proceeds would be transfered to above account only
                            </p>

                            <p class="text-justify">
                                Please make one remittance against one application. We will not accept multiple
                                                    remittances against one application or one remittance against multiple applications.
                            </p>
                        </div>

                        <h5 class="Dtit"><span class="DPinkhead">Repayment Instruction </span></h5>
                        <div class="card border-0 card-body">
                            <div class="form-group row">
                                <div class="col-lg-4 col-12">
                                    <label class="form-label">Repayment Instruction <span class="lblman">*</span> </label>
                                </div>
                                <div class="col-lg-8 col-12">
                                    <asp:RadioButtonList ID="rblRepayInst" runat="server" TextAlign="Right" RepeatDirection="Vertical"
                                        CssClass="bodytextbold1">
                                        <asp:ListItem Text="Send back money to original account" Value="SBM">&nbsp; Send back money to original account</asp:ListItem>
                                        <asp:ListItem Text="Renew deposit for same period" Value="RDP">&nbsp; Renew deposit for same period</asp:ListItem>
                                        <%--<asp:ListItem Text="Will instruct through Union Premier Bond portal, 15 days before maturity"
                                            Value="INS">&nbsp; Will instruct through Union Premier Bond portal, 15 days before maturity</asp:ListItem>--%>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>

                        <h5 class="Dtit"><span class="DPinkhead">General Agreement</span> </h5>
                        <div class="card border-0 card-body">
                            <asp:Panel ID="pnlJoint" runat="server" Visible="false">

                                <label class="form-label">
                                    <asp:CheckBox ID="chkIfJoint" runat="server" Text="" />
                                    Primary applicant have all the permission of the joint applicants
                                </label>

                            </asp:Panel>

                            <%-- <tr>
                                  <td colspan="2" style="padding: 10px 0 10px 10px">
                                        <asp:Panel ID="pnlJoint" runat="server" Width="95%" Visible="false">
                                            <table style="width: 100%; border-collapse: collapse;">
                                                <tr>
                                                    <td class="bodytextbold">
                                                        <asp:CheckBox ID="chkIfJoint" runat="server" Text="" />
                                                        Primary applicant have all the permission of the joint applicants
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding: 10px 0 10px 10px">
                                        <asp:CheckBox ID="chkTermsAndConditions" runat="server" Text="" />
                                        <asp:LinkButton ID="lnkTermsAndConditions" runat="server" Text="I declare and confirm that I have read and agree General Terms and Conditions."
                                            CausesValidation="false" CssClass="bodytextbold" OnClientClick="return showPopup()"></asp:LinkButton>
                                    </td>
                                </tr>--%>

                            <label class="form-label">
                                <asp:CheckBox ID="chkTermsAndConditions" runat="server" Text="" />
                                <%-- <asp:LinkButton ID="lnkTermsAndConditions" runat="server" Text="I declare and confirm that I have read and agree General Terms and Conditions."
                                    CausesValidation="false" CssClass="bodytextbold" OnClientClick="return showPopup()"></asp:LinkButton>--%>
                                <a href="TermsandCondition.aspx" target="_blank" class="text-secondary text-decoration-underline font-weight-normal">I declare and confirm that I have read
                                and agree General Terms and Conditions.</a>
                            </label>

                            <label class="form-label">
                                <asp:CheckBox ID="chkPrvPolicy" runat="server" Text="" />
                                I declare and confirm that I read and understood privacy policy 
                            </label>

                            <label class="form-label">
                                <asp:CheckBox ID="chkPromoSpeOffer" runat="server" Text="" />
                                I wish to receive the details of promotional and/or Special offers
                            </label>

                            <label class="form-label">
                                <asp:CheckBox ID="CheckTnC" runat="server" Text="" />
                                I am aware that this is a fixed-term deposit where premature withdrawal is not permitted.</label>



                        </div>

                        <h5 class="Dtit"><span class="DPinkhead">FSCS Information</span> </h5>
                        <div class="card border-0 card-body">
                            <label class="form-label">
                                <asp:CheckBox ID="chkFSCSCont" runat="server" Text="" />
                                I hereby acknowledge that I have read and understood the contents on the
                            <a href="website/Images/information sheet1.pdf" target="_blank" class="text-danger text-decoration-none border-0">Information Sheet about protection offered by FSCS and Exclusion
                                List of deposits not covered by FSCS</a>
                            </label>
                        </div>

                        <asp:Button ID="btnModelPopup" runat="server" CssClass="d-none" />
                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnModelPopup"
                            BehaviorID="mpe" PopupControlID="PnlTemrs" CancelControlID="btnOk" BackgroundCssClass="tableBackground">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="PnlTemrs" CssClass="poptbl d-none" runat="server">

                            <div class="col-12">
                                <p class="bodytext text-justify">
                                    1. That all the particulars and information given in this application form (And
                                                        all documents referred to or provided herewith) are true, correct, complete up-to-date
                                                        in all respects and I/we have not withheld any information.
                                </p>
                                <p class="bodytext text-justify">
                                    2. I / We understand that certain particulars given by me/us are required for regulatory
                                                        reasons. I / We also agree to provide any further information that Union Bank of
                                                        India (UK) Ltd or its group companies may require.
                                </p>
                                <p class="bodytext text-justify">
                                    3. I / we have had no insolvency proceedings initiated agains me/us, nor have I/we
                                                        ever been adjucated insolvent. I / we have no County Court Judgements registered
                                                        against me/us; and nor, to the best of our knowledge and belief, are we aware of
                                                        any circumstances that would give rise to such action.
                                </p>
                                <p class="bodytext text-justify">
                                    4. I / we are not blacklisted under Disqualified Director Register and adjucated/convicted
                                                        in any criminal proceedings under any criminal law.
                                </p>
                                <p class="bodytext text-justify">
                                    5. Under the Data Protection Act 1998, there are restrictions placed on data processors
                                                        regarding the transfer of data to third parties and outside the EU. I/We confirm
                                                        that the data provided by me/us or already in the Bank's records may be provided
                                                        to the Bank Office & Data Centre of Union Bank of India (UK) Ltd in India, other
                                                        members of the Group, its agents or third parties, in the UK or abroad, including
                                                        outside the EU, for the purpose of processing and storage of my/our application
                                                        and for the subsequent processing of transactions or in connection with the general
                                                        conduct of any of my/our account(s) with the bank.
                                </p>
                            </div>
                        </asp:Panel>
                        <div class="col-12 pb-2 mt-2">
                            <center>
                                <asp:Label ID="errormsg" runat="server" Text="" CssClass="p-1 text-center"></asp:Label>
                            </center>
                        </div>
                    </div>



                    <div class="d-flex justify-content-center mb-3">
                        <asp:Button ID="btnStep1" runat="server" Text="Go to Step 1" CssClass="button" CausesValidation="false"
                            OnClick="btnStep1_Click" />&nbsp;
                                <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save & Return Later" CssClass="button"
                                    CausesValidation="false" OnClick="btnSaveAsDraft_Click" />&nbsp;
                                <asp:Button ID="btnNextb" runat="server" Text="Next" CssClass="button" OnClientClick="var b=submitBond();if(b) var b=Check();return b;"
                                    OnClick="btnNextb_Click" CausesValidation="true" />
                        <%--OnClientClick="return submitBond();"--%>
                    </div>

                    <asp:Button ID="btnModPopupEx2" runat="server" CssClass="d-none" />
                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnModPopupEx2"
                        PopupControlID="PnlPwdReg" CancelControlID="btnOk" BackgroundCssClass="sctableBackground">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="PnlPwdReg" runat="server" CssClass="d-none" DefaultButton="btnSubmitEmail">
                        <div class="w-50 m-auto mt-5 pt-3">
                            <div class="modal-header bg-theme justify-content-center">
                                <h6 class="text-center text-white">Application Saved</h6>
                            </div>
                            <div class="modal-body bg-lightblue p-3">
                                <p class="mb-3">
                                    Your application has been saved successfully. Please complete and submit it at your earliest convenience to prevent the loss of the entered AOF details. Your unique application reference number, <span class="fw-bold text-primary"><asp:Label ID="lblRefNoVal" runat="server" /></span>, has been sent to your verified email address along with instructions for accessing your application.
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

                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="row">
                <uc3:FSCS ID="FSCS" runat="server" />
                <uc2:footer ID="footer1" runat="server" />
            </div>

            <asp:HiddenField ID="DepositMinAmt" runat="server" />
            <asp:HiddenField ID="DepositMaxAmt" runat="server" />

        </div>

        <div class="modal d-none" id="ModalProgress" runat="server">
            <div class="modal-dialog top-50">
                <div class="modal-content">
                    <!-- Modal body -->
                    <div class="modal-body">
                        <asp:Panel ID="PanLoading" runat="server" CssClass="">
                            <div class=" m-auto position-relative">
                                <img src="images/ajax-loader.gif" class="d-block m-auto" alt="loading" title="loading" />
                                <p class="bodytext text-center">Processing your request. Please wait ... </p>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="website/js/popper-min.js"></script>
    <script type="text/javascript" src="website/js/bootstrap.bundle.min.js"></script>

    <script src="javascript/jquery-3.7.1.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
        var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl)
        })

        var sDepMinAmt = parseInt(document.getElementById('<%= DepositMinAmt.ClientID %>').value);
        var sDepMaxAmt = parseInt(document.getElementById('<%= DepositMaxAmt.ClientID %>').value);
    </script>
    <script type="text/javascript" src="JS/CSP/OnClickHandlers.js"></script>
    <script type="text/javascript" src="JS/CSP/Home.js" nonce="ihYxAijSER-YFSUxCDJbag"></script>
</body>
</html>
