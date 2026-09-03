using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class BondDetails : System.Web.UI.Page
{
    Methods obj = new Methods();
    ValServer val = new ValServer();
    ValServer val1 = new ValServer();
    AccountDtl oAcDtl = null;
    Applicant oAplcnt = null;
    string str = String.Empty;
    bool check = false;
    bool RDexception = false;
    string sCreatedBy = string.Empty;
    string sModifiedBy = string.Empty;

    string fselect = string.Empty;

    protected void Page_Init(object sender, System.EventArgs e)
    {
        try
        {
            //txtPassword.Attributes.Add("onkeydown", "return (event.keyCode!=13);");

            if ((Session["RefNo"] == null) || (Convert.ToString(Session["RefNo"]) == string.Empty))
            {
                Response.Redirect("SessionTimeout.aspx", false);
                Response.End();
            }
        }
        catch (Exception ex)
        {
            //obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            //throw ex;
        }
    }    

    public void ValidatePage()
    {

        try
        {
            //validate_controls();

            if ((Session["RefNo"] == null) || (Convert.ToString(Session["RefNo"]) == string.Empty))
            {
                Response.Redirect("SessionTimeout.aspx", false);

            }
            else
            {
                string sCatchNm = "OthDtl" + Session["RefNo"];

                if ((Request.UserAgent.IndexOf("AppleWebKit") > 0))
                {
                    Request.Browser.Adapters.Clear();
                }

                if (ViewState["OthDtl"] == null)
                {
                    ViewState["OthDtl"] = System.Guid.NewGuid();
                    str = ViewState["OthDtl"].ToString().Replace("-", "");
                    check = true;
                }

                if ((string)Cache[sCatchNm] == null)
                {
                    Cache.Insert(sCatchNm, str, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20));
                }

                if (check == true)
                {
                    if (str != (string)Cache[sCatchNm])
                    {
                        try
                        {
                            Response.Redirect("GenError.aspx", true);

                        }
                        catch
                        {
                            RDexception = true;
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ValidatePage();
            Loadchkjoint();

            LoadDepositAmtLmt();

            if (Page.IsPostBack == false)
            {
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

                LoadDetails();

                //LoadAcDetails(GetAcDtl());
            }

        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private void LoadDepositAmtLmt()
    {
        try
        {
            string sMinAmount = obj.RetPolValue("DEPOSIT", "MINAMOUNT");
            string sMaxAmount = obj.RetPolValue("DEPOSIT", "MAXAMOUNT");

            DepositMinAmt.Value = sMinAmount;
            DepositMaxAmt.Value = sMaxAmount;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void Loadchkjoint()
    {
        AccountDtl AcDtl = GetAcDtl();
        try
        {
            string sIsJntAc;
            sIsJntAc = string.Empty;
            sIsJntAc = AcDtl.IsJntAc;
            if (sIsJntAc == "Yes")
            {
                pnlJoint.Visible = true;
            }
            else
            {
                pnlJoint.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private void LoadDetails()
    {
        try
        {
            AccountDtl oAcDtl = null;

            oAcDtl = (AccountDtl)Session["AcDtl"];

            if (oAcDtl != null)
            {
                //if (txtAmt.Text == string.Empty)
                //{
                //    //LoadTestDetails();
                //}
                //else
                //{
                txtAmt.Text = oAcDtl.InvAmount;
                ddlPeriod.SelectedValue = oAcDtl.InvPeriod;
                txtRateOfInt.Text = oAcDtl.InvRateOfInt;

                txtOthBankName.Text = oAcDtl.OthBankName;

                if (oAcDtl.OthBankSC.Length == 6)
                {
                    txtOthBankSC1.Text = oAcDtl.OthBankSC.Substring(0, 2);
                    txtOthBankSC2.Text = oAcDtl.OthBankSC.Substring(2, 2);
                    txtOthBankSC3.Text = oAcDtl.OthBankSC.Substring(4, 2);
                }
                else
                {
                    txtOthBankSC1.Text = string.Empty;
                    txtOthBankSC2.Text = string.Empty;
                    txtOthBankSC3.Text = string.Empty;
                }

                txtOthBankAcNo.Text = oAcDtl.OthBankAcNo;

                rblRepayInst.SelectedValue = oAcDtl.RepayIns;
                //}
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void LoadTestDetails()
    {
        try
        {
            txtAmt.Text = "5000";
            ddlPeriod.SelectedIndex = 1;
            txtRateOfInt.Text = "0.6";
            txtOthBankName.Text = "PNBIL";
            txtOthBankSC1.Text = "60";
            txtOthBankSC2.Text = "95";
            txtOthBankSC3.Text = "00";
            txtOthBankAcNo.Text = "12345678";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void AddDetails()
    {
        AccountDtl AcDtl = null;

        AcDtl = (AccountDtl)Session["AcDtl"];
        try
        {
            AcDtl.InvAmount = obj.replaceSplChr(txtAmt.Text.Trim());
            AcDtl.InvPeriod = obj.replaceSplChr(ddlPeriod.SelectedValue);
            AcDtl.InvRateOfInt = obj.replaceSplChr(txtRateOfInt.Text.Trim());
            AcDtl.OthBankName = obj.replaceSplChr(txtOthBankName.Text.Trim());
            AcDtl.OthBankSC = obj.replaceSplChr(txtOthBankSC1.Text.Trim() + txtOthBankSC2.Text.Trim() + txtOthBankSC3.Text.Trim());
            AcDtl.OthBankAcNo = obj.replaceSplChr(txtOthBankAcNo.Text.Trim());

            AcDtl.RepayIns = obj.replaceSplChr(rblRepayInst.SelectedValue.Trim());

            if (chkPromoSpeOffer.Checked == true)
                AcDtl.Mrktngcont = "Y";
            else
                AcDtl.Mrktngcont = "N";

            //chella(20171006)
            if (chkFSCSCont.Checked == true)
                AcDtl.FSCSCont = "Y";
            else
                AcDtl.FSCSCont = "N";

            //CheckTnC New enhancement 2023
            if (CheckTnC.Checked == true)
                AcDtl.CheckTnC = "Y";
            else
                AcDtl.CheckTnC = "N";
            //

            Session["AcDtl"] = AcDtl;

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public AccountDtl GetAcDtl()
    {
        AccountDtl AcDtl = null;

        try
        {
            if (Session["AcDtl"] != null)
            {
                AcDtl = (AccountDtl)Session["AcDtl"];
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return AcDtl;
    }

    public void LoadAcDetails(AccountDtl AcDetail)
    {

        try
        {

            txtAmt.Text = AcDetail.InvAmount;

            ddlPeriod.SelectedValue = AcDetail.InvPeriod;
            txtRateOfInt.Text = AcDetail.InvRateOfInt;
            txtOthBankName.Text = AcDetail.OthBankName;
            if (AcDetail.OthBankSC.Length == 6)
            {
                txtOthBankSC1.Text = AcDetail.OthBankSC.Substring(0, 2);
                txtOthBankSC2.Text = AcDetail.OthBankSC.Substring(2, 2);
                txtOthBankSC3.Text = AcDetail.OthBankSC.Substring(4, 2);
            }
            txtOthBankAcNo.Text = AcDetail.OthBankAcNo;
            rblRepayInst.SelectedValue = AcDetail.RepayIns;

        }

        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void btnAcDtlt_Click(object sender, EventArgs e)
    {
        try
        {
            Cache.Remove("AcDtl" + Convert.ToString(Session["RefNo"]));
            Response.Redirect("AccountDetails.aspx", false);

        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }

    }
    protected void btnApDtlt_Click(object sender, EventArgs e)
    {
        try
        {
            Cache.Remove("ApDtl" + Convert.ToString(Session["RefNo"]));
            Response.Redirect("ApplicantDetails.aspx", false);

        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }
    protected void btnAcDtlb_Click(object sender, EventArgs e)
    {
        try
        {
            Cache.Remove("AcDtl" + Convert.ToString(Session["RefNo"]));
            Response.Redirect("AccountDetails.aspx", false);

        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }
    protected void btnNextb_Click(object sender, EventArgs e)
    {

        obj.StoreEvent("66311", Convert.ToString(Session["RefNo"]), "", "", "", Convert.ToString(Session["RefNo"]));

        try
        {
            if (PrimaryValidation())
            {
                if (Page.IsValid == true)
                {
                    btnNextb.Enabled = false;
                    AddDetails();
                    if (CheckRequiredData() == false)
                    {
                        return;
                    }
                    Cache.Remove("PrvwDtl" + Convert.ToString(Session["RefNo"]));
                    Response.Redirect("Preview.aspx", false);

                }
            }
        }

        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
        finally
        {
            btnNextb.Enabled = true;
        }

    }

    private bool PrimaryValidation()
    {
        bool bStatus = false;

        decimal dAmt;
        int iBankAcNo;
        string sAmount = txtAmt.Text.Trim();
        string sPeriod = ddlPeriod.SelectedValue;
        string sROI = txtRateOfInt.Text.Trim();
        string sBankNm = txtOthBankName.Text.Trim();
        string sBankSC1 = txtOthBankSC1.Text.Trim();
        string sBankSC2 = txtOthBankSC2.Text.Trim();
        string sBankSC3 = txtOthBankSC3.Text.Trim();

        string sBankSC = sBankSC1 + "-" + sBankSC2 + "-" + sBankSC3;

        string sBankAcNo = txtOthBankAcNo.Text.Trim();
        string sRePayInst = rblRepayInst.SelectedValue;
        bool bTAndC = chkTermsAndConditions.Checked;
        bool bPrmoOffr = chkPromoSpeOffer.Checked;
        bool bPriAplPrvlg = chkIfJoint.Checked;
        bool bCheckTnC = CheckTnC.Checked;

        try
        {
            if (sAmount == string.Empty)
            {
                lblAlert.Text = "Please enter the amount";
                txtAmt.Focus();
                return false;
            }
            if (decimal.TryParse(sAmount, out dAmt) == false)
            {
                lblAlert.Text = "Please enter the valid amount";
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
                    lblAlert.Text = "Please enter an amount between £" + dMinAmtTxt + " and £" + dMaxAmtTxt;
                    txtAmt.Focus();
                    return false;
                }
            }
            if (sPeriod == string.Empty || sPeriod == "-1")
            {
                lblAlert.Text = "Please select the deposit period";
                ddlPeriod.Focus();
                return false;
            }
            if (sROI == string.Empty)
            {
                lblAlert.Text = "Technical error please contact branch";
                txtRateOfInt.Focus();
                return false;
            }
            if (sBankNm == string.Empty)
            {
                lblAlert.Text = "Please enter the bank name";
                txtOthBankName.Focus();
                return false;
            }
            if (sBankNm.Length < 2)
            {
                lblAlert.Text = "Bank name should have atleast 2 alpha characters";
                txtOthBankName.Focus();
                return false;
            }
            if (sBankSC1 == string.Empty)
            {
                lblAlert.Text = "Please enter bank sortcode Box-1";
                txtOthBankSC1.Focus();
                return false;
            }
            if (sBankSC1.Length != 2)
            {
                lblAlert.Text = "bank sortcode Box-1 should be 2 numeric characters";
                txtOthBankSC1.Focus();
                return false;
            }
            if (sBankSC2 == string.Empty)
            {
                lblAlert.Text = "Please enter bank sortcode Box-2";
                txtOthBankSC2.Focus();
                return false;
            }
            if (sBankSC2.Length != 2)
            {
                lblAlert.Text = "bank sortcode Box-2 should be 2 numeric characters";
                txtOthBankSC2.Focus();
                return false;
            }
            if (sBankSC3 == string.Empty)
            {
                lblAlert.Text = "Please enter bank sortcode Box-3";
                txtOthBankSC3.Focus();
                return false;
            }
            if (sBankSC3.Length != 2)
            {
                lblAlert.Text = "bank sortcode Box-3 should be 2 numeric characters";
                txtOthBankSC3.Focus();
                return false;
            }


            if (sBankSC == obj.RetPolValue("UBI", "SORTCODE"))
            {
                //lblAlert.Text = "Sort code for UBIUK is not allowed";
                lblAlert.Text = "Kindly transfer funds from other than UBI (UK) Ltd accounts";
                txtOthBankSC1.Focus();
                return false;
            }



            if (sBankAcNo == string.Empty)
            {
                lblAlert.Text = "Please enter bank account number";
                txtOthBankAcNo.Focus();
                return false;
            }
            if (sBankAcNo.Length != 8)
            {
                lblAlert.Text = "Bank account number should be 8 numeric characters";
                txtOthBankAcNo.Focus();
                return false;
            }
            if (int.TryParse(sBankAcNo, out iBankAcNo) == false)
            {
                lblAlert.Text = "Please enter the valid bank account number";
                txtOthBankAcNo.Focus();
                return false;
            }

            if (sRePayInst == string.Empty)
            {
                lblAlert.Text = "Please select the repayment instruction";
                rblRepayInst.Focus();
                return false;
            }

            if (bTAndC == false)
            {
                lblAlert.Text = "Please agree the terms and conditions";
                chkTermsAndConditions.Focus();
                return false;
            }
            else
            {
                AccountDtl oAccDtl = GetAcDtl();

                if (oAccDtl != null && oAccDtl.appList != null && oAccDtl.appList.Count > 1 && bPriAplPrvlg == false)
                {
                    lblAlert.Text = "Please accept primary applicant have all the permission of joint applicants";
                    chkIfJoint.Focus();
                    return false;
                }
            }

            if (bCheckTnC == false)
            {
                lblAlert.Text = "Kindly give your consent for accepting the condition of no premature withdrawal";
                CheckTnC.Focus();
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

    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {
        lblAlert.Text = string.Empty;
        try
        {
            btnSaveAsDraft.Enabled = false;
            AddDetails();

            string sType = Convert.ToString(Session["Type"]);

            if (sType == "NA")
            {
                oAcDtl = (AccountDtl)Session["AcDtl"];

                if (Session["AcOpenRefNo"] != null && Convert.ToString(Session["AcOpenRefNo"]) == oAcDtl.ReferenceNo)
                {
                    //ModalPopupExtender2.Show();
                    //ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-block";
                    //PnlPwdReg.CssClass = "modal bndDetModal d-block zindex_1";
                    //lblRefNoVal.Text = oAcDtl.ReferenceNo;

                    SaveAndSendEmail();
                }
                else
                {
                    Session["AcOpenRefNo"] = obj.CreateAcOpnRefNo(Methods.Customer.NewCust);

                    oAcDtl.ReferenceNo = Session["AcOpenRefNo"].ToString();

                    Session["AcDtl"] = oAcDtl;

                    if (!IsAcExist())
                    {
                        AccountDtl AcD = GetAcDtl();

                        obj.StoreEvent("99999", "", "", "", "going to insert TDKEY values", Convert.ToString(Session["RefNo"]));

                        obj.ExecuteCommand("INSERT INTO TDKEY (TDREFNO,TDDATE,TDTIME) VALUES('" + oAcDtl.ReferenceNo + "','" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("HHmmss") + "')");

                        obj.StoreEvent("99999", "", "", "", "TDKEY values inserted Successfully", Convert.ToString(Session["RefNo"]));

                        if (obj.SaveApplicant("D"))
                        {
                            //ModalPopupExtender2.Show();
                            //ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-block";
                            //PnlPwdReg.CssClass = "modal bndDetModal d-block zindex_1";
                            //lblRefNoVal.Text = oAcDtl.ReferenceNo;

                            SaveAndSendEmail();
                        }
                        else
                        {
                            lblAlert.Text = "Unable to process your request now, kindly contact branch!";
                            obj.StoreEvent("88888", "", "", "", "Failed to save the information. Please contact the nearest branch!", Convert.ToString(Session["RefNo"]));
                        }
                    }
                    else
                    {
                        AccountDtl AcD = GetAcDtl();
                        lblAlert.Text = "Unable to process your request now, kindly contact branch!";
                        obj.StoreEvent("88888", "", "", "", "Information already exist for the Reference No: " + AcD.ReferenceNo + " Kindly retrieve application and update your changes.", Convert.ToString(Session["RefNo"]));
                    }
                }
            }
            else if (sType == "RA")
            {
                AccountDtl AcD = GetAcDtl();

                obj.StoreEvent("99999", "", "", "", "Going to delete values From TDAPPIND", Convert.ToString(Session["RefNo"]));
                //20181115-chellappa
                if ((AcD.ReferenceNo != null) && (AcD.ReferenceNo != ""))
                {
                    obj.ExecuteCommand("DELETE FROM TDAPPIND WHERE TDREFNO='" + AcD.ReferenceNo + "' OR TDTRNID = '" + AcD.ReferenceNo + "'");

                    obj.StoreEvent("99999", "", "", "", "Deleted values From TDAPPIND", Convert.ToString(Session["RefNo"]));

                    if (obj.SaveApplicant("D"))
                    {
                        //ModalPopupExtender2.Show();
                        //ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-block";
                        //PnlPwdReg.CssClass = "modal bndDetModal d-block zindex_1";
                        //lblRefNoVal.Text = AcD.ReferenceNo;

                        SaveAndSendEmail();
                    }
                    else
                    {
                        lblAlert.Text = "Failed to update the information for the Reference No: " + AcD.ReferenceNo + ". Please contact the nearest branch!";
                    }
                }
                else
                {
                    obj.StoreEvent("99999", "", "", "", "Reference No is empty failed to delete RA values From TDAPPIND", Convert.ToString(Session["RefNo"]));
                }
            }

        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
        finally
        {
            btnSaveAsDraft.Enabled = true;
        }
    }

    private bool IsAcExist()
    {
        bool bStatus = false;
        string sQuery = string.Empty;
        string sValues = string.Empty;
        string sAcId = string.Empty;
        DataTable dtAcDtl = null;

        try
        {
            AccountDtl AcD = GetAcDtl();

            dtAcDtl = obj.ExecuteData("SELECT TDREFNO FROM TDAPPIND WHERE TDREFNO='" + AcD.ReferenceNo + "'");
            if ((dtAcDtl != null) && (dtAcDtl.Rows.Count > 0))
            {
                if (dtAcDtl.Rows[0]["TDREFNO"].ToString().ToUpper() == AcD.ReferenceNo.ToUpper())
                {
                    bStatus = true;
                }
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
        return bStatus;
    }

    private string GetAcStatus(AccountDtl objAcDtl)
    {
        string sStatus = "KYP";

        try
        {
            List<Applicant> Applicants = objAcDtl.appList;
            Applicant oAp = (from appl in Applicants where appl.Score < 2022 select appl).FirstOrDefault();
            if (oAp == null)
            {
                sStatus = "KYS";
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
        return sStatus;
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

    protected void btnStep1_Click(object sender, EventArgs e)
    {
        try
        {
            Cache.Remove("AcDtl" + Convert.ToString(Session["RefNo"]));
            Response.Redirect("AccountDetails.aspx", false);

        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }

    }

    protected void lblExit_Click(object sender, EventArgs e)
    {
        try
        {
            ClearSessionAndCache();
            Session.Abandon();
        }

        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private void ClearSessionAndCache()
    {
        try
        {
            if (Session["RefNo"] != null && Session["RefNo"].ToString().Length > 0)
            {
                foreach (string itm in Methods.strAryPages)
                {
                    Cache.Remove(itm + Session["RefNo"]);
                }
                Session.Abandon();
                Session.Clear();
            }

            Response.Redirect("Index.aspx", false);

        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnSubmitEmail_Click(object sender, EventArgs e)
    {
        obj.StoreEvent("68005", Convert.ToString(Session["RefNo"]), "", "", "", Convert.ToString(Session["RefNo"]));

        try
        {
            //if (SaveAndSendEmail())
            //{
            //    AccountDtl AcD = GetAcDtl();
            //    Response.Redirect("Confirmation.aspx?R=" + obj.UrlEncrypt64("Ref=" + AcD.ReferenceNo + " & AplnSts=D"), false);
            //}

            AccountDtl AcD = GetAcDtl();
            Response.Redirect("Confirmation.aspx?R=" + obj.UrlEncrypt64("Ref=" + AcD.ReferenceNo + " & AplnSts=D"), false);
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            Response.Redirect("GenError.aspx", false);

        }
        finally
        {
            ModalPopupExtender2.Hide();
            ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-none";
            PnlPwdReg.CssClass = "modal bndDetModal d-none zindex_1";
        }
    }

    private bool SaveAndSendEmail()
    {
        bool bStatus = false;

        try
        {
            //if (Page.IsValid == true)
            //{
                DataTable dtAcOp = new DataTable();
                string sQuery = string.Empty;

                sQuery = "SELECT CUSPWD,CUFNAME FROM ACOPCUST WHERE TRREF='" + obj.NullToSpace(Session["AcOpenRefNo"]) + "'";
                dtAcOp = obj.ExecuteData(sQuery);

                if (dtAcOp != null && dtAcOp.Rows.Count > 0)
                {
                    SendEmail();
                    bStatus = true;
                }
            //}
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }

        return bStatus;
    }

    private void SendEmail()
    {
        string sLogo = string.Empty;
        string MsgBody = string.Empty;
        string sHeader = string.Empty;
        string sFooter = string.Empty;
        try
        {
            AccountDtl AcDtl = GetAcDtl();
            if (AcDtl != null && AcDtl.appList != null && AcDtl.appList.Count > 0)
            {
                Applicant oApDtl = AcDtl.appList[0];

                string Content, subject;

                string pName = obj.RetPolValue("UBI", "PRODUCTNAME");

                string url = obj.RetPolValue("ACOPN", "RETAPPURL") + "?q=" + obj.Encrypt64("RefNo=" + AcDtl.ReferenceNo);

                url = "<a style='color:#00579c;font-size:14px;font-weight:bold;' href='" + url + "' target='_blank'>" + url + "</a>";

                Content = obj.GetContent("Retrieval");
                Content = Content.Replace("(TITLE)", oApDtl.Title);

                Content = Content.Replace("(CUSTOMERFIRSTNAME)", oApDtl.FirstNm);
                Content = Content.Replace("(CUSTOMERMIDNAME)", oApDtl.MidNm);
                Content = Content.Replace("(CUSTOMERSURNAME)", oApDtl.SurNm);
                Content = Content.Replace("(EMAILID)", oApDtl.EmailAddr);
                Content = Content.Replace("(REFNO)", AcDtl.ReferenceNo);
                Content = Content.Replace("(URL)", url);
                Content = Content.Replace("(PRODUCTNAME)", pName);

                subject = "UBI(UK) - Online Deposit Taking application saved successfully";

                sLogo = obj.RetPolValue("MAIL", "LOGO");
                sHeader = obj.RetPolValue("MAIL", "HEADER");
                sFooter = obj.RetPolValue("MAIL", "FOOTER");
                sHeader = sHeader.Replace("UBIUKLOGO", sLogo);
                MsgBody = sHeader + Content + sFooter;

                try
                {
                    if (obj.SendEmailMessage(oApDtl.EmailAddr, subject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer))
                    {
                        try
                        {
                            obj.StoreEvent("99999", "", "", "", "Saved application email sent successfully", Convert.ToString(Session["RefNo"]));
                            System.Threading.Thread.Sleep(5000);
                            obj.SendEmailMessage(obj.RetPolValue("MAIL", "COPY"), subject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer);
                        }
                        catch (Exception ex)
                        { }
                    }
                    else
                    {
                        try
                        {
                            obj.StoreEvent("88888", "", "", "", "Saved application email sending failure", Convert.ToString(Session["RefNo"]));
                            System.Threading.Thread.Sleep(5000);
                            obj.SendEmailMessage(obj.RetPolValue("MAIL", "COPY"), subject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer);
                        }
                        catch (Exception ex)
                        { }
                    }
                }
                catch (Exception exinr)
                {
                    obj.StoreEvent("88888", "", "", "", "Unable to send the saved application email. " + obj.replaceSplChr(exinr.ToString()), Convert.ToString(Session["RefNo"]));
                }               

                ModalPopupExtender2.Show();
                ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-block";
                PnlPwdReg.CssClass = "modal bndDetModal d-block zindex_1";
                lblRefNoVal.Text = AcDtl.ReferenceNo.Trim();
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
        finally
        {
            //ModalPopupExtender2.Hide();
            //ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-none";
            //PnlPwdReg.CssClass = "modal bndDetModal d-none zindex_1";
        }

    }

    //Commented 2023 enhancement remaining
    //private bool IsValidData()
    //{
    //    bool bStatus = false;
    //    try
    //    {
    //        if (txtPassword.Text.Trim() == string.Empty)
    //        {
    //            lblPopupAlert.Text = " Password cannot be blank!";
    //            txtPassword.Focus();
    //            ModalPopupExtender2.Show();
    //            ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-block";
    //            PnlPwdReg.CssClass = "modal bndDetModal d-block zindex_1";
    //        }
    //        else
    //        {
    //            bStatus = true;
    //            lblPopupAlert.Text = string.Empty;
    //            ModalPopupExtender2.Hide();
    //            ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-none";
    //            PnlPwdReg.CssClass = "modal bndDetModal d-none zindex_1";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
    //        throw ex;
    //    }
    //    return bStatus;
    //}

    //private bool SaveAndSendEmail()
    //{
    //    bool bStatus = false;

    //    try
    //    {
    //        if (Page.IsValid == true)
    //        {
    //            DataTable dtAcOp = new DataTable();
    //            string sQuery = string.Empty;

    //            sQuery = "SELECT CUSPWD,CUFNAME FROM ACOPCUST WHERE TRREF='" + obj.NullToSpace(Session["AcOpenRefNo"]) + "'";
    //            dtAcOp = obj.ExecuteData(sQuery);

    //            if (dtAcOp != null && dtAcOp.Rows.Count > 0)
    //            {
    //                string tmpEncrypted = "";
    //                string tmpDecrypted = "";

    //                tmpEncrypted = txtPassword.Text.Trim();

    //                byte[] decodedBytes = Convert.FromBase64String(tmpEncrypted);

    //                string decodedText;

    //                decodedText = Convert.ToString(Encoding.UTF8.GetString(decodedBytes));
    //                tmpDecrypted = decodedText;

    //                if (tmpDecrypted != "")
    //                {
    //                    string strupd = "Update acopcust set cuspwd ='" + obj.EncryPass(obj.rplsSnglQots(tmpDecrypted)) + "',CUSEMAIL='" + obj.rplsSnglQots(txtEmailId.Text.Trim()) + "' where trref = '" + obj.NullToSpace(Session["AcOpenRefNo"]) + "'";
    //                    obj.ExecuteCommand(strupd);
    //                    SendEmail(tmpDecrypted);



    //                    bStatus = true;
    //                }
    //                else
    //                {
    //                    lblPopupAlert.Text = "Unable to process your request. Please try again.";
    //                    bStatus = false;
    //                }

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
    //        throw ex;
    //    }

    //    return bStatus;
    //}

    //private void SendEmail(string sPassword)
    //{
    //    string sLogo = string.Empty;
    //    string MsgBody = string.Empty;
    //    string sHeader = string.Empty;
    //    string sFooter = string.Empty;
    //    try
    //    {
    //        AccountDtl AcDtl = GetAcDtl();
    //        if (AcDtl != null && AcDtl.appList != null && AcDtl.appList.Count > 0)
    //        {
    //            Applicant oApDtl = AcDtl.appList[0];

    //            string Content, subject;

    //            string pName = obj.RetPolValue("UBI", "PRODUCTNAME");

    //            string url = obj.RetPolValue("ACOPN", "RETAPPURL") + "?q=" + obj.Encrypt64("RefNo=" + AcDtl.ReferenceNo);

    //            url = "<a style='color:#00579c;font-size:14px;font-weight:bold;' href='" + url + "' target='_blank'>" + url + "</a>";

    //            Content = obj.GetContent("Retrieval");
    //            Content = Content.Replace("(TITLE)", oApDtl.Title);

    //            Content = Content.Replace("(CUSTOMERFIRSTNAME)", oApDtl.FirstNm);
    //            Content = Content.Replace("(CUSTOMERMIDNAME)", oApDtl.MidNm);
    //            Content = Content.Replace("(CUSTOMERSURNAME)", oApDtl.SurNm);
    //            Content = Content.Replace("(REFNO)", AcDtl.ReferenceNo);
    //            Content = Content.Replace("(EMAILID)", oApDtl.EmailAddr);
    //            Content = Content.Replace("(PASSWORD)", sPassword);
    //            Content = Content.Replace("(URL)", url);
    //            Content = Content.Replace("(PRODUCTNAME)", pName);

    //            subject = "UBI(UK) - Online Deposit Taking application saved successfully";

    //            sLogo = obj.RetPolValue("MAIL", "LOGO");
    //            sHeader = obj.RetPolValue("MAIL", "HEADER");
    //            sFooter = obj.RetPolValue("MAIL", "FOOTER");
    //            sHeader = sHeader.Replace("UBIUKLOGO", sLogo);
    //            MsgBody = sHeader + Content + sFooter;

    //            //obj.SendEmailFromUBI(oApDtl.EmailAddr, subject, Content);

    //            ////obj.SendEmailMessage(oApDtl.EmailAddr, subject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer);

    //            ////obj.StoreEvent("99999", "", "", "", "Retrieve exist Application Mail Send Successfully", Convert.ToString(Session["RefNo"]));

    //            try
    //            {
    //                //obj.SendEmailMessage(oApDtl.EmailAddr, subject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer);
    //                //obj.StoreEvent("99999", "", "", "", "Saved application email sent successfully", Convert.ToString(Session["RefNo"]));

    //                if (obj.SendEmailMessage(oApDtl.EmailAddr, subject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer))
    //                {
    //                    try
    //                    {
    //                        obj.StoreEvent("99999", "", "", "", "Saved application email sent successfully", Convert.ToString(Session["RefNo"]));
    //                        System.Threading.Thread.Sleep(5000);
    //                        obj.SendEmailMessage(obj.RetPolValue("MAIL", "COPY"), subject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer);
    //                    }
    //                    catch (Exception ex)
    //                    { }
    //                }
    //                else
    //                {
    //                    try
    //                    {
    //                        obj.StoreEvent("88888", "", "", "", "Saved application email sending failure", Convert.ToString(Session["RefNo"]));
    //                        System.Threading.Thread.Sleep(5000);
    //                        obj.SendEmailMessage(obj.RetPolValue("MAIL", "COPY"), subject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer);
    //                    }
    //                    catch (Exception ex)
    //                    { }
    //                }

    //            }
    //            catch (Exception exinr)
    //            {
    //                obj.StoreEvent("88888", "", "", "", "Unable to send the saved application email. " + obj.replaceSplChr(exinr.ToString()), Convert.ToString(Session["RefNo"]));
    //            }

    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
    //        throw ex;
    //    }
    //    finally
    //    {
    //        txtEmailId.Text = string.Empty;
    //        txtPassword.Text = string.Empty;
    //        lblPopupAlert.Text = string.Empty;
    //        ModalPopupExtender2.Hide();
    //        ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-none";
    //        PnlPwdReg.CssClass = "modal bndDetModal d-none zindex_1";
    //    }

    //}
    //End

    protected void lnkClose_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
        ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-none";
        PnlPwdReg.CssClass = "modal bndDetModal d-none zindex_1";
    }

    protected void btnLoadTestValues_Click(object sender, EventArgs e)
    {
        try
        {
            txtAmt.Text = "1000";
            ddlPeriod.SelectedIndex = 1;
            ddlPeriod_SelectedIndexChanged(sender, e);
            txtOthBankName.Text = "PNBIL";
            txtOthBankSC1.Text = "60";
            txtOthBankSC2.Text = "95";
            txtOthBankSC3.Text = "00";
            txtOthBankAcNo.Text = "12345678";
            chkTermsAndConditions.Checked = true;
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private bool CheckRequiredData()
    {
        bool bStatus = false;

        try
        {

            if (rblRepayInst.SelectedIndex == -1)
            {
                lblAlert.Text = "Please Select Repayment Instruction";
                bStatus = false;
            }

            else if (1 == 1)
            {
                bStatus = true;
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }

        return bStatus;
    }

    ////private bool IsValidSCode()
    ////{
    ////    bool bStatus = true;

    ////    string fSelect = string.Empty;
    ////    DataTable dt = new DataTable();

    ////    string BankSC = obj.replaceSplChr(txtOthBankSC1.Text.Trim()) + "-" + obj.replaceSplChr(txtOthBankSC2.Text.Trim()) + "-" + obj.replaceSplChr(txtOthBankSC3.Text.Trim());
    ////    try
    ////    {
    ////        //UBI	SORTCODE
    ////        string sCode = obj.RetPolValue("UBI", "SORTCODE");
    ////        fSelect = "SELECT POLDET FROM POLMTPF WHERE PTYPE='SORTCODE'";
    ////        dt = obj.ExecuteData(fSelect);
    ////        if (dt != null && dt.Rows.Count > 0)
    ////        {
    ////            string sCode = obj.NullToSpace(dt.Rows[0]["POLDET"]);

    ////            if (BankSC == sCode)
    ////            {
    ////                bStatus=false;
    ////            }
    ////        }
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
    ////        throw ex;
    ////    }
    ////    return bStatus;
    ////}
}