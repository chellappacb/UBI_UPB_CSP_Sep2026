using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class InvestmentDetails : System.Web.UI.Page
{
    Methods obj = new Methods();
    string Refno = string.Empty;
    string fselect = string.Empty;
    string TDStatus = string.Empty;
    string fIsIFPManSts = string.Empty;
    DataTable dtIsIFPManSts = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                btnUpdate.Visible = false;
                btnCancel.Visible = false;

                //txtAmt.Style.Add("border", "none");
                txtAmt.CssClass = "border-0";
                txtAmt.Enabled = false;

                ddlPeriod.Visible = false;
                txtRateOfInt.Visible = false;
                btnEdit.Visible = true;
                //fselect = "select distinct ProdName,sortorder from RATINTPF order by sortorder asc";
                //obj.LoadDDL(ddlPeriod, fselect);

                //string qry = @"SELECT CASE WHEN ProdName LIKE '%Year%' THEN CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS VARCHAR(10)) + 'Year' +
                //           CASE WHEN CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS INT) > 1 THEN 's' ELSE '' END
                //        WHEN ProdName LIKE '%Month%' THEN CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS VARCHAR(10)) + 'Months' END AS TENURE_TEXT,
                //    RateInt AS RATE FROM RATINTPF
                //    ORDER BY 
                //    CASE WHEN ProdName LIKE '%Year%' THEN
                //                CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS INT) * 12
                //            ELSE CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS INT)
                //    END";

                string qry = @"SELECT
                            CASE
                                WHEN ProdName LIKE '%Year%' THEN
                                    CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS VARCHAR(10))
                                    + 'Year'
                                    + CASE
                                        WHEN CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS INT) > 1
                                            THEN 's'
                                        ELSE ''
                                      END
                                WHEN ProdName LIKE '%Month%' THEN
                                    CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS VARCHAR(10))
                                    + 'Month'
                                    + CASE
                                        WHEN CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS INT) > 1
                                            THEN 's'
                                        ELSE ''
                                      END
                            END AS TENURE_TEXT,
                            RateInt AS RATE
                        FROM RATINTPF
                        ORDER BY
                            CASE
                                WHEN ProdName LIKE '%Year%' THEN
                                    CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS INT) * 12
                                ELSE
                                    CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS INT)
                            END";

                obj.LoadDDL(ddlPeriod, qry);

                if (!string.IsNullOrEmpty(Request.QueryString["A"]))
                {
                    string[] qstr = obj.UrlGetQString(Request.QueryString["A"]);
                    Session["RefNos"] = qstr[0];
                    if (!string.IsNullOrEmpty(Convert.ToString(Session["RefNos"])))
                    {
                        LoadDatas();
                        LoadBondStatus();

                        string sFRDate = string.Empty;
                        string sInvAmt = string.Empty;
                        fIsIFPManSts = "SELECT * FROM TDAPPIND WHERE TDREFNO='" + obj.NullToSpace(Convert.ToString(Session["RefNos"])) + "' AND MANUALIFPCL = 'Y'";
                        dtIsIFPManSts = obj.ExecuteData(fIsIFPManSts);
                        if (dtIsIFPManSts != null && dtIsIFPManSts.Rows.Count > 0)
                        {
                            ManualIFPCl.Visible = true;
                            
                            //AutoIFPClDet.Visible = false;
                            AutoIFPClDet.Attributes["class"] = AutoIFPClDet.Attributes["class"].Replace("visible", "visible-hidden").Trim();

                            TotAmt.Visible = false;
                            FRRDec.Visible = false;

                            sFRDate = obj.CTOD(obj.NullToSpace(dtIsIFPManSts.Rows[0]["TDFRDATE"]));
                            sInvAmt = obj.NullToSpace(dtIsIFPManSts.Rows[0]["TDINVAMT"]);
                            lblManualIFPCl.Text = "The funds were fully received and verified on " + obj.CTOD(sFRDate) + ". The proposed bond amount is " + sInvAmt + ".";
                        }
                        else
                        {
                            //AutoIFPClDet.Visible = true;
                            AutoIFPClDet.Attributes["class"] = AutoIFPClDet.Attributes["class"].Replace("visible-hidden", "visible").Trim();

                            ManualIFPCl.Visible = false;
                            TotAmt.Visible = true;

                            LoadGrid();
                        }

                        string sMaxAmount = obj.RetPolValue("DEPOSIT", "MAXAMOUNT");
                        if (!string.IsNullOrEmpty(sMaxAmount))
                        {
                            HdnAmt.Value = sMaxAmount;
                        }
                    }
                }
            }
            lblmsgscs.Text = "";
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", ex.ToString(), "");
        }
    }

    private void LoadBondStatus()
    {
        try
        {
            DataTable dtLoadSts = null;
            DataTable dtCQRUplSts = null;
            DataRow drLoadSts;

            lblRefNo.InnerText = Session["RefNos"].ToString();

            string sBondStatusHtml, sBondSts, sStepCompleted, sKYP, sKYPCheck, sKYS, sKYSCheck, sFRP, sFRPCheck, sFRR, sFRRCheck, sCQR, sCQRCheck;
            sBondStatusHtml = sBondSts = sStepCompleted = sKYP = sKYPCheck = sKYS = sKYSCheck = sFRP = sFRPCheck = sFRR = sFRRCheck = sCQR = sCQRCheck = string.Empty;

            sBondStatusHtml = "<div class='steps steps-vertical'>" +
                                    "<ol style='font-size:16px;'>" +
                                      "<li class='steps-completed'>" +
                                          "<div class='material-icons steps-icon'>check</div> Application Submitted" +
                                      "</li>";

            string fSelect = "SELECT * FROM TDAPPIND WHERE TDREFNO='" + obj.NullToSpace(Convert.ToString(Session["RefNos"])) + "' AND TDSTATUS IN ('KYP','KYS','FRP','FRR','CQR')";
            dtLoadSts = obj.ExecuteData(fSelect);

            string sBondStatusHtml1, sBondSts1, sKYPCheck1, sKYSCheck1, sFRACheck1, sAwaACKit, sAwaACKitChk, sKYPChk, sKYSChk, sCQRChk;
            sBondStatusHtml1 = sBondSts1 = sKYPCheck1 = sKYSCheck1 = sFRACheck1 = sAwaACKit = sAwaACKitChk = sKYPChk = sKYSChk = sCQRChk = string.Empty;
            sBondStatusHtml1 = "<div class='col-rt-12'>" +
                               "    <div class='Scriptcontent'>" +
                               "        <div class='steb steb-active'>" +
                               "            <div><div class='circla'><i class='fa fa-check'></i></div></div>" +
                               "            <div><div class='titla'>Application Submitted</div></div>" +
                               "        </div>";
            if (dtLoadSts != null && dtLoadSts.Rows.Count > 0)
            {
                drLoadSts = dtLoadSts.Rows[0];
                sBondSts1 = obj.NullToSpace(drLoadSts["TDSTATUS"]);

                switch (sBondSts1)
                {
                    case "KYP":
                        sKYPCheck1 = "steb-active";
                        sKYPChk = "fa fa-check";
                        break;
                    case "KYS":
                        sKYPCheck1 = "steb-active";
                        sKYPChk = "fa fa-check";
                        sKYSCheck1 = "steb-active";
                        sKYSChk = "fa fa-check";
                        break;
                    case "FRP":
                        sKYPCheck1 = "steb-active";
                        sKYPChk = "fa fa-check";
                        sKYSCheck1 = "steb-active";
                        sKYSChk = "fa fa-check";
                        break;
                    case "FRR":
                        sKYPCheck1 = "steb-active";
                        sKYPChk = "fa fa-check";
                        sKYSCheck1 = "steb-active";
                        sKYSChk = "fa fa-check";
                        break;
                    case "CQR":
                        sKYPCheck1 = "steb-active";
                        sKYPChk = "fa fa-check";
                        sKYSCheck1 = "steb-active";
                        sKYSChk = "fa fa-check";
                        sFRACheck1 = "steb-active";
                        sCQRChk = "fa fa-check";
                        break;
                }
            }
            sBondStatusHtml1 += "<div class='steb " + sKYPCheck1 + "'>" +
                                " <div><div class='circla'><i class='" + sKYPChk + "'></i></div></div>" +
                                " <div><div class='titla'> KYC Pending</div></div>" +
                                "</div>";
            sBondStatusHtml1 += "<div class='steb " + sKYSCheck1 + "'>" +
                                " <div><div class='circla'><i class='" + sKYSChk + "'></i></div></div>" +
                                " <div><div class='titla'> KYC Completed</div></div>" +
                                "</div>";
            sBondStatusHtml1 += "<div class='steb " + sFRACheck1 + "'>" +
                                " <div><div class='circla'><i class='" + sCQRChk + "'></i></div></div>" +
                                " <div><div class='titla'> Funds Received Fully</div></div>" +
                                "</div>";

            string fSelUplFinacle = "SELECT TDUPLOAD FROM TDAPPIND WHERE TDREFNO='" + obj.NullToSpace(Convert.ToString(Session["RefNos"])) + "' AND TDSTATUS='CQR' and TDUPLOAD='Y'";
            dtCQRUplSts = obj.ExecuteData(fSelUplFinacle);
            if (dtCQRUplSts != null && dtCQRUplSts.Rows.Count > 0)
            {
                sAwaACKit = "steb-active";
                sAwaACKitChk = "fa fa-check";
            }

            sBondStatusHtml1 += "<div class='steb " + sAwaACKit + "'>" +
                                " <div><div class='circla'><i class='" + sAwaACKitChk + "'></i></div></div>" +
                                " <div><div class='titla'> Awaiting for Account Opening Kit</div></div>" +
                                "</div>";


            sBondStatusHtml1 += "</div></div>";

            TrackBar.InnerHtml = sBondStatusHtml1;
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            Response.Redirect("GenError.aspx", false);
        }
    }

    private void LoadDatas()
    {
        try
        {
            string sRefnoVal = string.Empty;
            string ISFRRSTSDEC = string.Empty;
            if (!string.IsNullOrEmpty(Convert.ToString(Session["RefNos"])))
            {
                string TDOTHSRCODE = string.Empty;
                string TDOTHACNO = string.Empty;
                DataTable dTable = new DataTable();

                dTable = obj.ExecuteData("select top 1 TDREFNO,concat(TDFANAME,' ',TDMINAME,' ',TDSURNAME)as Name,TDFADOB,TDFAEMAIL,TDINVAMT,TDINVPERIOD,TDINVRATINS,TDSTATUS,TDOTHSRCODE,TDOTHACNO,ISFRRSTSDEC from TDAPPIND where TDREFNO='" + obj.NullToSpace(Convert.ToString(Session["RefNos"])) + "'");
                if (dTable != null && dTable.Rows.Count > 0)
                {
                    sRefnoVal = obj.NullToSpace(dTable.Rows[0]["TDREFNO"]);
                    lblName.Text = obj.NullToSpace(dTable.Rows[0]["Name"]);
                    lblDoB.Text = obj.CTOD(obj.NullToSpace(dTable.Rows[0]["TDFADOB"]));
                    lblEmailAddress.Text = obj.NullToSpace(dTable.Rows[0]["TDFAEMAIL"]);
                    lblPeriod.Text = obj.NullToSpace(dTable.Rows[0]["TDINVPERIOD"]);
                    lblRateOfInt.Text = obj.NullToSpace(dTable.Rows[0]["TDINVRATINS"]);
                    txtAmt.Text = obj.NullToSpace(dTable.Rows[0]["TDINVAMT"]);
                    
                    //ddlPeriod.SelectedValue = obj.NullToSpace(dTable.Rows[0]["TDINVPERIOD"]);
                    //Added for if the period is not available in RATINTPF table.
                    string invPeriod = obj.NullToSpace(dTable.Rows[0]["TDINVPERIOD"]);
                    if (ddlPeriod.Items.FindByValue(invPeriod) != null)
                    {
                        ddlPeriod.SelectedValue = invPeriod;
                    }
                    else
                    {
                        ddlPeriod.SelectedValue = "-1";
                    }
                    //End

                    txtRateOfInt.Text = obj.NullToSpace(dTable.Rows[0]["TDINVRATINS"]);
                    TDStatus = obj.NullToSpace(dTable.Rows[0]["TDSTATUS"]);

                    TDOTHSRCODE = obj.NullToSpace(dTable.Rows[0]["TDOTHSRCODE"]);
                    TDOTHACNO = obj.NullToSpace(dTable.Rows[0]["TDOTHACNO"]);

                    ISFRRSTSDEC = obj.NullToSpace(dTable.Rows[0]["ISFRRSTSDEC"]);
                    if (ISFRRSTSDEC == "Y")
                    {
                        FRRDec.Visible = true;
                        lblFRRDec.Text = "Your payment could not be processed. Please contact our support team for further assistance";
                    }
                    else
                    {
                        FRRDec.Visible = false;
                        lblFRRDec.Text = "";
                    }

                    if (TDStatus == "KYP")
                    {
                        btnEdit.Visible = true;
                    }
                    else if (TDStatus == "KYS")
                    {
                        DataTable dTables = new DataTable();

                        dTables = obj.ExecuteData("select * from NWIFPCLRG where REMITTER_REFERENCE='" + sRefnoVal + "' and CREDIT_INSTITUTION='" + obj.NullToSpace(TDOTHSRCODE) + "' and REMITTER_ACCT_NUM='" + obj.NullToSpace(TDOTHACNO) + "'");
                        
                        if (dTables != null && dTables.Rows.Count > 0)
                        {
                            if (obj.NullToSpace(dTables.Rows[0]["ISPARTIALAMT"]) == "N" && obj.NullToSpace(dTables.Rows[0]["PAYSTATUS"]) == "S")
                            {
                                btnEdit.Visible = false;
                            }
                            else if (obj.NullToSpace(dTables.Rows[0]["ISPARTIALAMT"]) == "N" && obj.NullToSpace(dTables.Rows[0]["PAYSTATUS"]) == "F")
                            {
                                btnEdit.Visible = false;
                            }
                            else if (obj.NullToSpace(dTables.Rows[0]["ISPARTIALAMT"]) == "Y" && obj.NullToSpace(dTables.Rows[0]["PAYSTATUS"]) == "F")
                            {
                                btnEdit.Visible = false;
                            }
                        }
                        else
                        {
                            DataTable dtChk = new DataTable();

                            dtChk = obj.ExecuteData("select b.PAYMENTREFNO,b.IFPCLRPROSTS from TDAPPIND a inner join [BONDPAYMENTSTATUS] b on a.TDREFNO=b.PAYMENTREFNO and b.PAYMENTREFNO='" + sRefnoVal + "'");
                            if (dtChk != null && dtChk.Rows.Count > 0)
                            {
                                btnEdit.Visible = false;
                            }
                            else
                            {
                                btnEdit.Visible = true;
                            }

                            //if (!string.IsNullOrEmpty(TDOTHSRCODE) && !string.IsNullOrEmpty(TDOTHACNO))
                            //{
                            //    dTables = obj.ExecuteData("select * from NWIFPCLRG where CREDIT_INSTITUTION='" + obj.NullToSpace(TDOTHSRCODE) + "' and REMITTER_ACCT_NUM='" + obj.NullToSpace(TDOTHACNO) + "'");
                            //    if (dTables != null && dTables.Rows.Count > 0)
                            //    {
                            //        if (obj.NullToSpace(dTables.Rows[0]["ISPARTIALAMT"]) == "N" && obj.NullToSpace(dTables.Rows[0]["PAYSTATUS"]) == "S")
                            //        {
                            //            btnEdit.Visible = false;
                            //        }
                            //        else if (obj.NullToSpace(dTables.Rows[0]["ISPARTIALAMT"]) == "N" && obj.NullToSpace(dTables.Rows[0]["PAYSTATUS"]) == "F")
                            //        {
                            //            btnEdit.Visible = false;
                            //        }
                            //        else if (obj.NullToSpace(dTables.Rows[0]["ISPARTIALAMT"]) == "Y" && obj.NullToSpace(dTables.Rows[0]["PAYSTATUS"]) == "F")
                            //        {
                            //            btnEdit.Visible = false;
                            //        }
                            //    }
                            //}                           
                        }
                    }
                    else
                    {
                        btnEdit.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPeriod.SelectedIndex > 0)
                txtRateOfInt.Text = obj.GetRateofInterest(Convert.ToDouble(obj.NullToZero(txtAmt.Text)), ddlPeriod.SelectedValue);
            else
                txtRateOfInt.Text = "";

        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            //lblAmt.Visible = false;
            lblPeriod.Visible = false;
            lblRateOfInt.Visible = false;
            LoadDatas();

            //txtAmt.Style.Remove("border");
            txtAmt.CssClass = "form-control Ubtxtbox border-1";
            txtAmt.Enabled = true;

            ddlPeriod.Visible = true;
            txtRateOfInt.Visible = true;
            btnUpdate.Visible = true;
            btnCancel.Visible = true;
            btnEdit.Visible = false;
            lblmsgscs.Text = "";
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (PrimaryValidation())
            {
                lblPeriod.Visible = true;
                lblRateOfInt.Visible = true;

                string strupd = "Update TDAPPIND set TDINVAMT ='" + obj.rplsSnglQots(txtAmt.Text.Trim()) + "',TDINVPERIOD='" + obj.rplsSnglQots(ddlPeriod.SelectedItem.Text.Trim()) + "',TDINVRATINS='" + obj.rplsSnglQots(txtRateOfInt.Text.Trim()) + "' where tdrefno = '" + obj.NullToSpace(Convert.ToString(Session["RefNos"])) + "'";
                obj.ExecuteCommand(strupd);
                obj.StoreEvent("99999", "", "", "", "EC Bond Details updated successfully for the Reference no. : " + Convert.ToString(Session["RefNo"]), Convert.ToString(Session["RefNo"]));

                LoadDatas();

                //txtAmt.Style.Add("border", "none");
                txtAmt.CssClass = "border-0";
                txtAmt.Enabled = false;

                ddlPeriod.Visible = false;
                txtRateOfInt.Visible = false;
                btnUpdate.Visible = false;
                btnCancel.Visible = false;
                btnEdit.Visible = true;
                lblmsg.Visible = false;
                lblmsg.Text = "";
                lblmsgscs.Visible = true;
                lblmsgscs.Text = "Bond details updated successfully";
                LoadGrid();
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private bool PrimaryValidation()
    {
        bool bStatus = false;
        lblmsgscs.Text = "";
        decimal dAmt;
        string sAmount = txtAmt.Text.Trim();
        string sPeriod = ddlPeriod.SelectedValue;
        string sROI = txtRateOfInt.Text.Trim();

        try
        {
            if (sAmount == string.Empty)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Please enter the amount";
                txtAmt.Focus();
                return false;
            }
            if (decimal.TryParse(sAmount, out dAmt) == false)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Please enter a valid deposit amount";
                txtAmt.Focus();
                return false;
            }
            else
            {
                decimal dMinAmt = Convert.ToDecimal(obj.RetPolValue("DEPOSIT", "MINAMOUNT"));
                decimal dMaxAmt = Convert.ToDecimal(obj.RetPolValue("DEPOSIT", "MAXAMOUNT"));

                string dMinAmtTxt = dMinAmt.ToString("N0");
                string dMaxAmtTxt = dMaxAmt.ToString("N0");

                if (dAmt < dMinAmt || dAmt > dMaxAmt)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Please enter an amount between £" + dMinAmtTxt + " and £" + dMaxAmtTxt;
                    txtAmt.Focus();
                    return false;
                }
            }
            if (sPeriod == string.Empty || sPeriod == "-1")
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Please select the deposit period";
                ddlPeriod.Focus();
                return false;
            }
            if (sROI == string.Empty)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Technical error please contact branch";
                txtRateOfInt.Focus();
                return false;
            }


            bStatus = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return bStatus;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            lblPeriod.Visible = true;
            lblRateOfInt.Visible = true;

            //txtAmt.Style.Add("border", "none");
            txtAmt.CssClass = "border-0";

            txtAmt.Enabled = false;
            ddlPeriod.Visible = false;
            txtRateOfInt.Visible = false;
            LoadDatas();
            btnUpdate.Visible = false;
            btnCancel.Visible = false;
            btnEdit.Visible = true;
            lblmsgscs.Text = "";
            lblmsg.Text = "";
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private void LoadGrid()
    {
        string fselect = string.Empty;
        DataTable dt = new DataTable();
        DataRow dr;
        try
        {
            DataTable dt1 = new DataTable();
            double dInvAmt = 0.0;

            fselect = "SELECT CONVERT(NVARCHAR(10),CAST(A.PAYMENTDATE AS DATETIME),103) +' '+ A.PAYMENTTIME AS [RECEIVED_TIMESTAMP],A.PAYMENTAMT AS [AMOUNT],A.PAYMENTBALAMT AS [Balance],B.TDINVAMT,A.TRANSACTIONID FROM BONDPAYMENTSTATUS A INNER JOIN TDAPPIND B ON A.PAYMENTREFNO=B.TDREFNO " +
                      "WHERE A.PAYMENTREFNO='" + Convert.ToString(Session["RefNos"]) + "'";
            dt = obj.ExecuteData(fselect);
            if (dt != null && dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
                dInvAmt = Convert.ToDouble(dr["TDINVAMT"]);
                TotAmt.Visible = true;
                lblTAmount.Text = dInvAmt.ToString("0.00");

                gvCUSACTS.DataSource = dt;
                gvCUSACTS.DataBind();
            }
            else
            {
                TotAmt.Visible = false;
                gvCUSACTS.DataSource = dt;
                gvCUSACTS.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}