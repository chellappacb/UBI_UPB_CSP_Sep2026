<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvestmentDetails.aspx.cs"
    Inherits="InvestmentDetails" %>

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
    <script language="javascript" type="text/javascript" src="JS/disbackbtn.js"></script>
    <link rel="Stylesheet" type="text/css" href="css/styles.css" />
    <link rel="Stylesheet" type="text/css" href="css/imghoverStyle.css" />
    <link type="text/css" href="css/WebResource.css" rel="stylesheet" />
    <link type="text/css" href="Website/custom-style.css" rel="Stylesheet" />
    <link type="text/css" href="css/cReponsive.css" rel="Stylesheet" />

    <link href="website/css/bootstrap.min.css" rel="stylesheet" />   

    <script type="text/javascript" src="javascript/app.js"></script>
    <link href="website/StatusTrack/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="website/TrackStatus.css" rel="stylesheet" type="text/css" />
    
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="container">
                        <uc1:header ID="header1" runat="server" />

                        <div class="row">
                            <div class="bg-theme text-right p-3 pr-2 d-flex justify-content-end align-items-center">
                                <a id="LnkbtnHome" class="link" href="index.aspx">Home</a>
                            </div>

                            <div class="bg-light text-secondary p-3 pl-2">
                                <span class="pl-2 font-16 text-black">Bond Status</span>
                            </div>
                        </div>

                        <center>
                            <asp:PlaceHolder ID="ErrorPH" runat="server"></asp:PlaceHolder>
                        </center>

                        <div class="row mt-3">
                            <div class="col-lg-4 col-12">


                                <label class="form-label">Reference Number : <span runat="server" id="lblRefNo" class="font-weight-bold" /></label>


                                <div class="mt-2" runat="server" id="TrackBar"></div>

                            </div>
                            <div class="col-lg-8 col-12">
                                <center><asp:Label ID="lblmsg" runat="server" Text="" CssClass="lblalert text-danger"></asp:Label>
                                <asp:Label ID="lblmsgscs" runat="server" CssClass="lblalert text-success"></asp:Label>
                                <asp:HiddenField ID="HdnAmt" runat="server" />
                                </center>

                                <div class="card mt-2">
                                    <h5 class="Dtit"><span class="DPinkhead">Bond Details</span> </h5>
                                    <div class="card-body lh-2">
                                        <div class="row">
                                            <div class="col-lg-6 col-12">
                                                <label class="form-label">Customer Name <span class="lblman"></span></label>
                                            </div>
                                            <div class="col-lg-6 col-12">
                                                <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-6 col-12">
                                                <label class="form-label">Date of Birth <span class="lblman"></span></label>
                                            </div>
                                            <div class="col-lg-6 col-12">
                                                <asp:Label ID="lblDoB" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-6 col-12">
                                                <label class="form-label">Email Address<span class="lblman"></span></label>
                                            </div>
                                            <div class="col-lg-6 col-12">
                                                <asp:Label ID="lblEmailAddress" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-6 col-12">
                                                <label class="form-label">Deposit Amount (in GBP)</label>
                                            </div>
                                            <div class="col-lg-6 col-12">
                                                <div class="form-group mb-1">
                                                    <cc1:MacroWebTextBox ID="txtAmt" runat="server" CssClass="form-control Ubtxtbox" MaxLength="9"
                                                        Validate="IsAmount" BlurBackground="White" FocusBackground=""></cc1:MacroWebTextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-6 col-12">
                                                <label class="form-label">Period of Deposit <span class="lblman"></span></label>
                                            </div>
                                            <div class="col-lg-6 col-12">
                                                <div class="form-group mb-1">
                                                    <asp:Label ID="lblPeriod" CssClass="form-label" runat="server" Text=""></asp:Label>
                                                    <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="bselectbox form-control" OnSelectedIndexChanged="ddlPeriod_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                        <asp:ListItem Text="Select Period" Value="-1" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-6 col-12">
                                                <label class="form-label">Rate of Interest (%) </label>
                                            </div>
                                            <div class="col-lg-6 col-12">
                                                <asp:Label ID="lblRateOfInt" CssClass="form-label" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="txtRateOfInt" CssClass="form-label" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>



                                        <div class="">
                                            <center>
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button scbutton"
                                                    OnClick="btnCancel_Click" />
                                           
                                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button scbutton"
                                                    OnClick="btnUpdate_Click" OnClientClick="return updateBond();" />
                                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="button scbutton" CausesValidation="false"
                                                    OnClick="btnEdit_Click" />
                                             </center>
                                        </div>
                                    </div>
                                </div>

                                <p class="font-16 text-danger p-2">
                                    "Please be aware that you may only modify your bond information up until your first
                            payment."
                                </p>

                                <div class="">
                                    <div class="bg-theme">
                                        <div class="row">
                                            <div class="col-6 ">
                                                <h5 class="Dtit"><span class="DPinkhead">Payment Details </span></h5>
                                            </div>
                                            <div class="col-6 d-flex justify-content-end" id="TotAmt" runat="server">
                                                <p class="p-2 text-white">
                                                    Total Amount (in GBP)
                                                    <asp:Label ID="lblTAmount" runat="server" CssClass="text-white" Text=""></asp:Label>
                                                </p>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="card-body p-0">
                                        <div class="row visible-hidden" id="AutoIFPClDet" runat="server">
                                            <asp:GridView ID="gvCUSACTS" runat="server" AutoGenerateColumns="False" AllowPaging="True" CssClass=" w-100 font-16"
                                                PageSize="10"
                                                CellPadding="4" GridLines="Vertical" EmptyDataText="No records found"
                                                EnableModelValidation="True">
                                                <RowStyle CssClass="RowStyle" />
                                                <AlternatingRowStyle CssClass="AlternateRowStyle" />
                                                <FooterStyle BackColor="#f0f0f0" />
                                                <PagerStyle BackColor="#f7f7f7" ForeColor="#000000" HorizontalAlign="Left" Font-Size="0.9em" />
                                                <HeaderStyle CssClass="dtgridhd " />
                                                <AlternatingRowStyle BackColor="#f7f7f7" Font-Size="1.0em" />
                                                <Columns>
                                                    <asp:BoundField DataField="RECEIVED_TIMESTAMP" HeaderText="Date & Time">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle CssClass="dtgriddata pp-width-75" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TRANSACTIONID" HeaderText="Transaction Reference Number">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle CssClass="dtgriddata pp-width-75" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AMOUNT" HeaderText="Amount Received (in GBP)">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle CssClass="dtgriddata pp-width-75" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Balance" HeaderText="Balance Amount Due (in GBP)">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle CssClass="dtgriddata pp-width-75" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div class="row mb-3 visible-hidden" id="ManualIFPCl" runat="server">
                                            <asp:Label ID="lblManualIFPCl" runat="server" Text=""></asp:Label>
                                        </div>

                                        <div class="row mb-3 visible-hidden" id="FRRDec" runat="server">
                                            <asp:Label ID="lblFRRDec" runat="server" Text="" CssClass="text-danger"></asp:Label>
                                        </div>

                                    </div>





                                </div>

                            </div>
                        </div>
                        <div class="row mt-4">
                            <uc3:FSCS ID="FSCS" runat="server" />
                            <uc2:footer ID="footer1" runat="server" />
                           
                        </div>


                    </div>
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

        <div class="modal test d-none sctableBackground" id="ModalProgress" runat="server">
            <div class="modal-dialog  top-50">
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
    <script src="javascript/JSValidation.js" type="text/javascript"></script>
    <script type="text/javascript" src="website/js/bootstrap.bundle.min.js"> </script>
    <script src="javascript/jquery-3.7.1.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="website/js/popper-min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keypress", "#txtAmt", function (event) {
                //$('#txtAmt').on("keypress", function (event) {
                var charlen = $('#txtAmt').val().length;
                var ival = $('#txtAmt').val();
                //console.log(ival + event.key);
                var newval = ival + event.key;
                if (newval <= parseInt($('#HdnAmt').val())) {
                    //console.log("in");
                    $('#txtAmt').css('border-color', '#666666;');
                    if ((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode == 110 || event.keyCode == 46)) {
                        if (event.keyCode != 8 || event.which != 8) {
                            if (charlen > 10) {
                                //return false;
                                event.preventDefault();
                            }
                            if (charlen > 5) {
                                //alert(charlen);
                                //alert(event.keyCode);
                                if ((event.keyCode == 110 || event.keyCode == 46) && (!ival.includes('.'))) {
                                    return true;
                                } else if (ival.includes('.') && event.keyCode != 110 && event.keyCode != 46) {
                                    var ivalarray = ival.split('.');
                                    //alert(ivalarray[1]);
                                    if (ivalarray[1].length > 1) {
                                        return false;
                                    } else {
                                        return true;
                                    }
                                } else {
                                    return false;
                                    //event.preventDefault();
                                }
                            } else {
                                //console.log('in 2');
                                if (ival.includes('.')) {
                                    var ivalarray = ival.split('.');
                                    //alert(ivalarray[1]);
                                    if (event.keyCode != 110 && event.keyCode != 46) {
                                        return true;
                                    } else if (ivalarray[1].length > 1) {
                                        return false;
                                    } else {
                                        return false;
                                    }
                                } else {
                                    //console.log('in straight');
                                    return true;
                                }
                            }
                        }
                    } else {
                        return false;
                    }
                } else {
                    event.preventDefault();
                }
            });
        });
    </script>
       <script type="text/javascript" src="JS/CSP/Home.js" nonce="ihYxAijSER-YFSUxCDJbag"></script>
</body>
</html>
