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
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using Newtonsoft.Json;


public partial class ApplicantDetails : System.Web.UI.Page
{

    Methods obj = new Methods();
    ValServer val = new ValServer();
    ValServer val1 = new ValServer();

    AccountDtl oAcDtl = null;
    Applicant oAplcnt = null;
    CultureInfo provider = new CultureInfo("en-GB");
    string str = String.Empty;
    bool check = false;
    bool RDexception = false;
    string sActionCode = string.Empty;
    string sCreatedBy = string.Empty;
    string sModifiedBy = string.Empty;

    //OTP2024
    string sCustOTP = "", sCustEMOTPREFNO = "", sCustEMOTPEMAIL = "", sCustEMOTPGNDATE = "", sCustEMOTPGNTIME = "", sCustOTPATTEMPT = "", sCustOTPRESENDCOUNT = "", sCustOTPSTATUS = "";
    //protected int CounterFromDatabase { get; set; }
    //

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
            // obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            //throw ex;
        }
    }

    private bool IsDate(string sdate) //20130826
    {

        bool isDate = true;
        try
        {

            DateTime dt;


            try
            {
                if (sdate.Length == 0)
                {
                    isDate = false;
                }
                else if ((sdate.Length > 0) && (sdate.Length == 10))
                {
                    dt = DateTime.Parse(sdate);
                }
                else
                {
                    isDate = false;
                }
            }
            catch
            {
                isDate = false;
            }


        }

        catch (Exception ex)
        {
            isDate = false;
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            // throw ex;
        }
        return isDate;

    }

    private int CalAge(string birthDate)
    {
        int years = 0;
        try
        {
            DateTime dt;
            dt = DateTime.Parse(birthDate);

            years = DateTime.Now.Year - dt.Year;

            if ((DateTime.Now.Month < dt.Month) || ((DateTime.Now.Month == dt.Month) && (DateTime.Now.Day < dt.Day)))
            {
                years = years - 1;
            }
        }

        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            // throw ex;
        }
        return years;
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
                // this.ClientScript.RegisterOnSubmitStatement(this.GetType(),"EscapeField", "EscapeField();");

                string sCatchNm = "ApDtl" + Session["RefNo"];

                if ((Request.UserAgent.IndexOf("AppleWebKit") > 0))
                {
                    Request.Browser.Adapters.Clear();
                }

                if (ViewState["ApDtl"] == null)
                {
                    ViewState["ApDtl"] = System.Guid.NewGuid();
                    str = ViewState["ApDtl"].ToString().Replace("-", "");
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
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //this.ClientScript.RegisterClientScriptBlock(this.GetType(), "EscapeField", "function EscapeField() { document.getElementById('txtIntPassportL3').value = escape(document.getElementById('txtIntPassportL3').value);}");

            //if (Session["Type"] != null && Session["Type"].ToString() == "NA")
            //{
            //    txtEmailAddr.Text = "";
            //}

            if (Page.IsPostBack == false)
            {

                ddlCurCountry.SelectedValue = "UNITED KINGDOM";
                //ddlPreCountry.SelectedValue = "UNITED KINGDOM";

                AccountDtl AcDtl = null;

                if (Session["AcDtl"] != null)
                {
                    AcDtl = (AccountDtl)Session["AcDtl"];
                }

                if (!string.IsNullOrEmpty(Request.QueryString["A"]))
                {
                    string[] qstr = obj.UrlGetQString(Request.QueryString["A"]);
                    ViewState["Sequence"] = qstr[0];
                    ViewState["ActnCode"] = qstr[1];

                    if (qstr.Length == 3 && qstr[2] == "01")
                        ViewState["FT"] = "Y";
                    else
                        ViewState["FT"] = "N";
                }


                //////Chellappa - 20180126
                ////if (AcDtl.IsJntAc.ToUpper() == "YES".ToUpper())
                ////{
                ////    Applicant oAppl = null;

                ////    if (Session["AcDtl"] != null)
                ////        AcDtl = (AccountDtl)Session["AcDtl"];

                ////    oAppl = AcDtl.appList[0];

                ////    if (Convert.ToString(ViewState["Sequence"]) == "1")
                ////    {
                ////        txtMomName.Enabled = true;
                ////    }
                ////    else
                ////    {
                ////        txtMomName.Text = oAppl.MothersMaidenNm;
                ////        txtMomName.Enabled = false;
                ////    }   
                ////}

                ////if (AcDtl.IsJntAc.ToUpper() == "YES".ToUpper())
                ////{
                ////    if (Convert.ToString(ViewState["Sequence"]) == "1")
                ////        pnlJntAplntAddr.Visible = false;
                ////    else
                ////        pnlJntAplntAddr.Visible = true;
                ////}

                if (Convert.ToString(ViewState["Sequence"]) == "1")
                {
                    pnlJntAplntAddr.Visible = false;

                    //Enable memorable word control.
                    pnlMemWrd.Visible = true;
                    txtMomName.Enabled = true;
                }
                else
                {
                    pnlJntAplntAddr.Visible = true;

                    //Disable control and loading Primary applicant Memorable word to all the joint applicants.                    
                    if (Session["AcDtl"] != null)
                        AcDtl = (AccountDtl)Session["AcDtl"];
                    txtMomName.Text = AcDtl != null && AcDtl.appList != null ? AcDtl.appList[0].MothersMaidenNm : string.Empty;

                    pnlMemWrd.Visible = false;
                    txtMomName.Enabled = false;

                    //2024 email verification disable for joint coustomers
                    btnVerify.CssClass = "d-none";
                    //End
                }

                LoadCountry();

                LoadCity();

                //Chellappa - 20190125
                LoadSOF();

                LitHelpUK1.Text = "<span class='pic'><a class='p1' href='#'  title='help' ><img src='images/help.png' alt='help' data-bs-toggle='tooltip' title='help' /><img src='images/SampleUKPassport1.jpg' alt='UK Passport sample' class='large' title='UK Passport sample' /></a></span>";
                LitHelpUK.Text = "<span class='pic'><a class='p1' href='#' title='help'><img src='images/help.png' alt='help' title='help' data-bs-toggle='tooltip' /><img src='images/SampleUKPassport.jpg' alt='UK Passport sample' class='large' title='UK Passport sample' /></a></span>";
                LitHelpINT1.Text = "<span class='pic'><a class='p1' href='#' title='help'><img src='images/help.png' alt='help' title='help' data-bs-toggle='tooltip' /><img src='images/SampleINTPassport1.jpg' alt='International passport sample' class='large' title='International passport sample' /></a></span>";
                LitHelpINT.Text = "<span class='pic'><a class='p1' href='#' title='help'><img src='images/help.png' alt='help' title='help' data-bs-toggle='tooltip' /><img src='images/SampleINTPassport.jpg' alt='International passport sample' class='large' title='International passport sample' /></a></span>";

                LoadTittle();
                //LoadMrtlSts();  //commented on 2023

                ValidatePage();

                if (Convert.ToString(ViewState["ActnCode"]) == "E")
                {
                    this.Form.DefaultButton = "btnSave";
                    LoadApplicantData();
                    ButtonVisibility(false);
                }
                else
                    ButtonVisibility(true);

            }
            else
            {
                ValidatePage();
                //OCR
                lblUploadError.Text = string.Empty;
                lblUploadInfo.Text = string.Empty;
                //End
                lblAlert.Text = string.Empty;
                lbldoberror.Text = string.Empty;
                lblDoissueerr.Text = string.Empty;
                lblDoExerr.Text = string.Empty;
                lblDLDEerr.Text = string.Empty;

            }


            txtDOB.Text = CalDOB.SelDate;
            txtDOI.Text = calDOI.SelDate;
            txtDOE.Text = calDOE.SelDate;
            txtDVLCDOE.Text = CalDOB.SelDate;


            //2024 issue fixed for page load automatically in chrome browser
            DateTime dtFormat;
            string[] formats = { "dd/MM/yyyy" };
            if (DateTime.TryParseExact(txtRsdnSnc.Text.Trim(), formats, provider, DateTimeStyles.None, out dtFormat) == false) // format check
            {
                txtRsdnSnc.Text = "DD/MM/YYYY";
            }
            //End


            ////2023 enhancements
            //if (Session["EMVerifiedSts"] != null && Session["EMVerifiedSts"].ToString() == "Y")
            //{
            //    btnVerify.CssClass = "d-none";
            //}
            //else
            //{
            //    btnVerify.CssClass = "button d-block";
            //}

            string sIDDocUpld = obj.RetPolValue("WEB", "IDDOCUPLOAD");
            if (sIDDocUpld == "Y")
            {
                pnlIDDocUpload.Visible = true;
            }
            else
            {
                pnlIDDocUpload.Visible = false;
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private void LoadSOF()
    {
        try
        {
            ddlSOF.Items.Clear();

            DataTable dt;
            dt = new DataTable();
            dt = obj.ExecuteData("SELECT UPPER(SOFTYPE) AS SOFTYPE FROM SOFDET WHERE SOFSTS='Y' ORDER BY ID ASC");

            ddlSOF.DataValueField = "SOFTYPE";
            ddlSOF.DataTextField = "SOFTYPE";
            ddlSOF.DataSource = dt;
            ddlSOF.DataBind();
            ddlSOF.Items.Insert(0, new ListItem("Select SOF Type", "-1"));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ButtonVisibility(bool isNew)
    {
        try
        {
            if (Convert.ToString(ViewState["ActnCode"]) == "E")
            {
                btnBackToStep1.Visible = false;
                btnSaveAsDraft.Visible = false;
                btnNextb.Visible = false;

                btnBackToStep1.CausesValidation = false;
                btnSaveAsDraft.CausesValidation = false;
                btnNextb.CausesValidation = false;

                btnCancel.Visible = true;
                btnSave.Visible = true;

                btnCancel.CausesValidation = false;
                btnSave.CausesValidation = true;

                if (Convert.ToString(ViewState["FT"]) != string.Empty && Convert.ToString(ViewState["FT"]) == "Y")
                {
                    btnSave.Text = "Modify & Next";
                    if (Convert.ToString(ViewState["Sequence"]) != string.Empty && Convert.ToInt32(ViewState["Sequence"]) > 1)
                    {
                        btnCancel.Text = "Clear & Go to Step-1";
                        btnCancel.Visible = true;
                    }
                    else
                    {
                        btnCancel.Visible = false;
                    }
                }
            }
            else
            {
                btnBackToStep1.Visible = true;
                btnSaveAsDraft.Visible = true;
                btnNextb.Visible = true;

                btnBackToStep1.CausesValidation = false;
                btnSaveAsDraft.CausesValidation = false;
                btnNextb.CausesValidation = true;

                btnCancel.Visible = false;
                btnSave.Visible = false;

                btnCancel.CausesValidation = false;
                btnSave.CausesValidation = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void LoadApplicantData()
    {
        try
        {
            AccountDtl AcDtl = null;
            Applicant oAppl = null;

            if (Session["AcDtl"] != null)
                AcDtl = (AccountDtl)Session["AcDtl"];

            oAppl = AcDtl.appList[Convert.ToInt32(ViewState["Sequence"]) - 1];

            LoadControls(oAppl);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    //Added by Rajesh
    private void ModifyApDtls(int seq)
    {
        List<Applicant> lstUsr = null;
        try
        {
            lstUsr = GetUserList();
            if (lstUsr != null)
            {
                lstUsr = GetUserList();
                Applicant Usr = (from d in lstUsr where d.Sequence == seq select d).First();
                clearControls();//20130823
                LoadControls(Usr);
            }
            btnCancel.Visible = true;
            btnSave.Visible = true;
            btnSave.Text = "Modify Applicant";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private List<Applicant> GetUserList()
    {
        try
        {
            List<Applicant> lstUsr = null;
            List<Applicant> lstUsrTmp = null;

            if (Session["AcDtl"] != null)
            {
                oAcDtl = (AccountDtl)Session["AcDtl"];
                lstUsr = oAcDtl.appList;

                if (lstUsr != null && lstUsr.Count > 0)
                {
                    var lstUsrOrderBy = from v in lstUsr orderby v.Sequence select v;

                    if (lstUsrOrderBy != null && lstUsrOrderBy.Count() > 0)
                    {
                        lstUsrTmp = new List<Applicant>();
                        foreach (Applicant aplcnt in lstUsrOrderBy)
                            lstUsrTmp.Add(aplcnt);
                    }

                    lstUsr = lstUsrTmp;
                }
            }

            return lstUsr;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void AddUser(Applicant Usr)
    {
        List<Applicant> lstUsr = null;

        try
        {


            if (lstUsr != null)
            {
                oAplcnt = (from d in lstUsr where d.Sequence == Usr.Sequence select d).FirstOrDefault();
                if (oAplcnt != null)
                {
                    int FindIndex = lstUsr.FindIndex(App => App.Sequence == Usr.Sequence);
                    lstUsr[FindIndex] = Usr;
                    lstUsr[0].IsPrimary = "Yes";
                }
                else
                {
                    ////20141106 strart
                    if (lstUsr.Count == 0)
                    {
                        //if (rblIsJntAc.SelectedValue.ToUpper() == "Yes".ToUpper()) Added by Rajesh
                        if (Convert.ToString(Session["JoinAccount"]) == "Y")
                        {
                            Usr.IsPrimary = "Yes";
                        }
                    }
                    ////20141106 end
                    lstUsr.Add(Usr);

                }
            }
            else
            {
                lstUsr = new List<Applicant>();
                Usr.IsPrimary = "Yes";
                lstUsr.Add(Usr);
            }

            oAcDtl = (AccountDtl)Session["AcDtl"];
            oAcDtl.appList[Convert.ToInt32(ViewState["Sequence"])] = Usr;
            Session["AcDtl"] = oAcDtl;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SaveValues()
    {
        int sSequence = 0;
        try
        {
            sSequence = Convert.ToInt32(ViewState["Sequence"]);

            oAplcnt = new Applicant();

            AccountDtl AcDtl = null;

            if (Session["AcDtl"] != null)
            {
                AcDtl = (AccountDtl)Session["AcDtl"];
            }

            oAplcnt.RegUniqueId = (string)Session["RefNo"];
            oAplcnt.Sequence = sSequence;

            //USEPRIMARY 
            oAplcnt.UsePrimaryAddr = obj.rplsSnglQots(rblJntApAddSts.SelectedValue);//20130729

            //2024-2023 Enhancements
            if (Convert.ToString(ViewState["Sequence"]) == "1")
            {
                if (Session["EMVerifiedSts"] != null && oAplcnt.EMVerSts == "")
                {
                    oAplcnt.EMVerSts = Session["EMVerifiedSts"].ToString();
                }
                else if (oAcDtl.EMVerSts == "Y")
                {
                    oAplcnt.EMVerSts = "Y";
                }
            }
            //End

            //Personal Details        
            oAplcnt.Title = obj.rplsSnglQots(ddlTittle.SelectedValue);
            oAplcnt.FirstNm = obj.rplsSnglQots(txtFirstNm.Text.Trim());
            oAplcnt.MidNm = obj.rplsSnglQots(txtMiddleNm.Text.Trim());
            oAplcnt.SurNm = obj.rplsSnglQots(txtSurNm.Text.Trim());
            oAplcnt.Gender = obj.rplsSnglQots(rblGender.SelectedValue);

            if (IsDate(CalDOB.SelDate.ToString()) == true)
                oAplcnt.DOB = CalDOB.SelDate;
            else
                oAplcnt.DOB = string.Empty;

            oAplcnt.Citizenship = obj.rplsSnglQots(ddlCtznShp.SelectedValue);

            //oAplcnt.MaritalSts = obj.rplsSnglQots(ddlMaritalSts.SelectedValue);  commented and added by chellappa on 2023 enhancements
            oAplcnt.MaritalSts = obj.rplsSnglQots("OTHERS");

            oAplcnt.MothersMaidenNm = obj.rplsSnglQots(txtMomName.Text.Trim());
            oAplcnt.PlaceOfBirth = obj.rplsSnglQots(txtPlaceOfBirth.Text.Trim());

            //Contact Details        
            oAplcnt.HomeTelNo = obj.rplsSnglQots(txtHomeTelPhNo.Text.Trim());
            oAplcnt.MobileNo = obj.rplsSnglQots(txtMobileNo.Text.Trim());
            oAplcnt.EmailAddr = obj.rplsSnglQots(txtEmailAddr.Text.Trim());

            //Employment Details
            oAplcnt.EmpType = obj.rplsSnglQots(ddlEmpType.SelectedValue);
            oAplcnt.EmptypOth = obj.rplsSnglQots(txtEmptypOth.Text.Trim());

            //fatca
            oAplcnt.IsUSperson = obj.rplsSnglQots(rblUSPerson.SelectedValue);
            if (rblUSPerson.SelectedValue.ToUpper() == "yes".ToUpper())
            {
                //chella(20171005)
                oAplcnt.PriJrsdctn = obj.rplsSnglQots(ddlPriJuri.SelectedValue);
                oAplcnt.AdJrsdctn1 = obj.rplsSnglQots(ddlAddJuri.SelectedValue);
                oAplcnt.AdJrsdctn2 = obj.rplsSnglQots(ddlAddJuri1.SelectedValue);

                oAplcnt.PriTIN = obj.rplsSnglQots(txtTin1.Text.Trim());
                oAplcnt.AdTIN1 = obj.rplsSnglQots(txtTin2.Text.Trim());
                oAplcnt.AdTIN2 = obj.rplsSnglQots(txtTin3.Text.Trim());
                oAplcnt.ResnNAPTIN = obj.rplsSnglQots(txtReasonTin.Text.Trim());
            }

            //fatca Details -chella(20171005)
            oAplcnt.PayTax = obj.rplsSnglQots(rblPayTax.SelectedValue);
            oAplcnt.USCitizen = obj.rplsSnglQots(rblUSCitizen.SelectedValue);
            oAplcnt.GreenCard = obj.rplsSnglQots(rblGreenCard.SelectedValue);
            oAplcnt.RealEst = obj.rplsSnglQots(rblRealEst.SelectedValue);
            oAplcnt.assets = obj.rplsSnglQots(rblassets.SelectedValue);

            //SOF -chella 20190129
            oAplcnt.sof = obj.rplsSnglQots(ddlSOF.SelectedValue);
            oAplcnt.sofOth = obj.rplsSnglQots(txtSOFOth.Text.Trim());

            #region 20150805 Modified by senthil based on Aug 2015 change request
            //Identification Details
            oAplcnt.IdenDtls = obj.rplsSnglQots(rblIdentity.SelectedValue);

            if (rblIdentity.SelectedValue.ToUpper() == "Passport".ToUpper())
            {
                oAplcnt.IsUkPsPrt = obj.rplsSnglQots(rblPassport.SelectedValue);

                if (rblPassport.SelectedValue.ToUpper() == "UK".ToUpper())
                {
                    oAplcnt.PsPrtName = obj.rplsSnglQots(txtUKPassportL1.Text.Trim()) + obj.rplsSnglQots(txtUKPassportL2.Text.Trim()) + obj.rplsSnglQots(txtUKPassportL3.Text.Trim());
                    oAplcnt.IdenNo = obj.rplsSnglQots(txtUKPassport1.Text.Trim()) + obj.rplsSnglQots(txtUKPassport2.Text.Trim())
                        + obj.rplsSnglQots(txtUKPassport3.Text.Trim()) + obj.rplsSnglQots(txtUKPassport4.Text.Trim()) + obj.rplsSnglQots(txtUKPassport5.Text.Trim())
                        + obj.rplsSnglQots(txtUKPassport6.Text.Trim()) + obj.rplsSnglQots(txtUKPassport7.Text.Trim());
                }
                else
                {
                    oAplcnt.PsPrtName = obj.rplsSnglQots(txtIntPassportL1.Text.Trim()) + obj.rplsSnglQots(txtIntPassportL2.Text.Trim()) + obj.rplsSnglQots(txtIntPassportL3.Text.Trim());
                    oAplcnt.IdenNo = obj.rplsSnglQots(txtIntPassport1.Text.Trim()) + obj.rplsSnglQots(txtIntPassport2.Text.Trim())
                        + obj.rplsSnglQots(txtIntPassport3.Text.Trim()) + obj.rplsSnglQots(txtIntPassport4.Text.Trim())
                        + obj.rplsSnglQots(txtIntPassport5.Text.Trim()) + obj.rplsSnglQots(txtIntPassport6.Text.Trim())
                        + obj.rplsSnglQots(txtIntPassport7.Text.Trim()) + obj.rplsSnglQots(txtIntPassport8.Text.Trim()) + obj.rplsSnglQots(txtIntPassport9.Text.Trim());
                }

                if (IsDate(calDOI.SelDate.ToString()) == true)
                    oAplcnt.PsPrtIssDt = calDOI.SelDate;
                else
                    oAplcnt.PsPrtIssDt = string.Empty; //DateTime.Now.ToString("dd/MM/yyyy");

                if (IsDate(calDOE.SelDate.ToString()) == true)
                    oAplcnt.PsPrtExpDt = calDOE.SelDate;
                else
                    oAplcnt.PsPrtExpDt = string.Empty; //DateTime.Now.ToString("dd/MM/yyyy");

                oAplcnt.PsPrtIsCntry = obj.rplsSnglQots(ddlPsPrtIsCntry.SelectedValue);
            }
            else if (rblIdentity.SelectedValue.ToUpper() == "Driving Licence".ToUpper())
            {
                oAplcnt.DrLceType = obj.rplsSnglQots(rbldltype.SelectedValue);
                oAplcnt.IdenNo = obj.rplsSnglQots(txtDRLNo1.Text.Trim()) + obj.rplsSnglQots(txtDRLNo2.Text.Trim()) + obj.rplsSnglQots(txtDRLNo3.Text.Trim());
                //oAplcnt.DrLceIdenNo = txtDRLNo1.Text.Trim() + txtDRLNo2.Text.Trim() + txtDRLNo3.Text.Trim();

                if (IsDate(calDVLCDOE.SelDate.ToString()) == true)
                    oAplcnt.DrLceExpDt = obj.rplsSnglQots(calDVLCDOE.SelDate);
                else
                    oAplcnt.DrLceExpDt = string.Empty;  //DateTime.Now.ToString("dd/MM/yyyy");

                oAplcnt.DrLcePCode = obj.rplsSnglQots(txtDVLCPcode.Text.Trim());

            }



            #endregion

            oAplcnt.NINO = txtNINO.Text.Trim();

            if (rblJntApAddSts.SelectedValue.ToUpper() == "Yes".ToUpper())
            {
                oAplcnt.CurDoorNo = oAcDtl.appList[0].CurDoorNo;
                oAplcnt.CurAddr1 = oAcDtl.appList[0].CurAddr1;
                oAplcnt.CurAddr2 = oAcDtl.appList[0].CurAddr2;
                oAplcnt.CurAddr3 = oAcDtl.appList[0].CurAddr3;
                oAplcnt.CurCounty = oAcDtl.appList[0].CurCounty;
                oAplcnt.CurPcode = oAcDtl.appList[0].CurPcode;
                oAplcnt.CurCntry = oAcDtl.appList[0].CurCntry;
                oAplcnt.ResidingSince = oAcDtl.appList[0].ResidingSince;

                oAplcnt.PreDoorNo = obj.rplsSnglQots(obj.rplsSnglQots(obj.NullToSpace(oAcDtl.appList[0].PreDoorNo)));
                oAplcnt.PreAddr1 = obj.rplsSnglQots(obj.rplsSnglQots(oAcDtl.appList[0].PreAddr1));
                oAplcnt.PreAddr2 = obj.rplsSnglQots(obj.rplsSnglQots(oAcDtl.appList[0].PreAddr2));
                oAplcnt.PreAddr3 = obj.rplsSnglQots(obj.rplsSnglQots(oAcDtl.appList[0].PreAddr3));
                oAplcnt.PreCounty = obj.rplsSnglQots(obj.rplsSnglQots(oAcDtl.appList[0].PreCounty));
                oAplcnt.PrePcode = obj.rplsSnglQots(obj.rplsSnglQots(oAcDtl.appList[0].PrePcode));
                oAplcnt.PreCntry = obj.rplsSnglQots(obj.rplsSnglQots(oAcDtl.appList[0].PreCntry));
            }
            else
            {
                oAplcnt.CurDoorNo = obj.rplsSnglQots(txtCurDrNo.Text.Trim());
                oAplcnt.CurAddr1 = obj.rplsSnglQots(txtCurAddr1.Text.Trim());
                oAplcnt.CurAddr2 = obj.rplsSnglQots(txtCurAddr2.Text.Trim());
                oAplcnt.CurAddr3 = obj.rplsSnglQots(ddlCurCity.SelectedValue.Trim());
                oAplcnt.CurCounty = obj.rplsSnglQots(txtCurCounty.Text.Trim());
                //oAplcnt.CurPcode = obj.rplsSnglQots(txtCurPostCode.Text.Trim());  Commented by chellappa 2023 enhancements
                oAplcnt.CurCntry = obj.rplsSnglQots(ddlCurCountry.SelectedValue);
                oAplcnt.ResidingSince = obj.rplsSnglQots(txtRsdnSnc.Text.Trim());
                //}

                //Added by chellappa 2023 enhancements
                if (txtCurPostCode.Text.Trim() != "")
                {
                    //string sCAPostCode = txtCurPostCode.Text.Trim();
                    //string sCAPCFromLast = Convert.ToString(sCAPostCode[sCAPostCode.Length - 4]);
                    //if (sCAPCFromLast.Contains(" "))
                    //{
                    //    oAplcnt.CurPcode = obj.rplsSnglQots(txtCurPostCode.Text.Trim());
                    //}
                    //else
                    //{
                    //    string sCurPostCode = txtCurPostCode.Text.Trim().Replace(" ", "");
                    //    sCurPostCode = sCurPostCode.Insert(sCurPostCode.Length - 3, " ");
                    //    oAplcnt.CurPcode = obj.rplsSnglQots(sCurPostCode);
                    //}

                    string sCurPostCode = txtCurPostCode.Text.Trim().Replace(" ", "");
                    sCurPostCode = sCurPostCode.Insert(sCurPostCode.Length - 3, " ");
                    oAplcnt.CurPcode = obj.rplsSnglQots(sCurPostCode);
                }
                //End


                DateTime newDt;
                if (DateTime.TryParse(txtRsdnSnc.Text.Trim(), out newDt))
                {
                    double dDiff = (Convert.ToDateTime(newDt, provider) - Convert.ToDateTime(DateTime.Today.AddYears(-1), provider)).TotalDays;
                    if (dDiff > 0)
                    {
                        oAplcnt.PreDoorNo = obj.rplsSnglQots(txtPreDrNo.Text.Trim());
                        oAplcnt.PreAddr1 = obj.rplsSnglQots(txtPreAddr1.Text.Trim());
                        oAplcnt.PreAddr2 = obj.rplsSnglQots(txtPreAddr2.Text.Trim());
                        //oAplcnt.PreAddr3 = txtPreAddr3.Text.Trim();
                        oAplcnt.PreAddr3 = obj.rplsSnglQots(ddlPreCity.SelectedValue.Trim());
                        oAplcnt.PreCounty = obj.rplsSnglQots(txtPreCounty.Text.Trim());
                        //oAplcnt.PrePcode = obj.rplsSnglQots(txtPrePostCode.Text.Trim());  Commented by chellappa 2023 enhancements
                        oAplcnt.PreCntry = obj.rplsSnglQots(ddlPreCountry.SelectedValue);

                        //Added by chellappa 2023 enhancements
                        if (txtPrePostCode.Text.Trim() != "")
                        {
                            //string sPAPostCode = txtPrePostCode.Text.Trim();
                            //string sPAPCFromLast = Convert.ToString(sPAPostCode[sPAPostCode.Length - 4]);
                            //if (sPAPCFromLast.Contains(" "))
                            //{
                            //    oAplcnt.PrePcode = obj.rplsSnglQots(txtPrePostCode.Text.Trim());
                            //}
                            //else
                            //{
                            //    string sPreAddPostCode = txtPrePostCode.Text.Trim().Replace(" ", "");
                            //    sPreAddPostCode = sPreAddPostCode.Insert(sPreAddPostCode.Length - 3, " ");
                            //    oAplcnt.PrePcode = obj.rplsSnglQots(sPreAddPostCode);
                            //}
                            string sPreAddPostCode = txtPrePostCode.Text.Trim().Replace(" ", "");
                            sPreAddPostCode = sPreAddPostCode.Insert(sPreAddPostCode.Length - 3, " ");
                            oAplcnt.PrePcode = obj.rplsSnglQots(sPreAddPostCode);
                        }
                        //End
                    }
                }
            }
            oAcDtl = (AccountDtl)Session["AcDtl"];
            oAcDtl.appList[Convert.ToInt32(ViewState["Sequence"]) - 1] = oAplcnt;
            Session["AcDtl"] = oAcDtl;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void LoadControls(Applicant applicant)
    {

        try
        {

            lblSequence.Text = Convert.ToString(applicant.Sequence);

            //USEPRIMARY

            if (applicant.IsPrimary.ToUpper() == "Yes".ToUpper())
                pnlJntAplntAddr.Visible = false;
            else
                pnlJntAplntAddr.Visible = true;

            if (applicant.Sequence > 1)
            {
                pnlJntAplntAddr.Visible = true;
                rblJntApAddSts.SelectedValue = applicant.UsePrimaryAddr.ToUpper();
            }
            else
            {
                pnlJntAplntAddr.Visible = false;
            }

            //EmailVerSts     2024
            if (applicant.EMVerSts == "Y")
            {
                imgEmailVerfied.CssClass = "";
                btnVerify.CssClass = "d-none";
                txtEmailAddr.Enabled = false;
                btnUpdateEmail.CssClass = "btn btn-link d-block";
            }
            else if (applicant.IsPrimary.ToUpper() == "Yes".ToUpper())
            {
                btnVerify.CssClass = "button d-block";
                btnUpdateEmail.CssClass = "d-none";
            }
            //End

            //Personal Details        
            if (applicant.Title.Length == 0)
                ddlTittle.SelectedIndex = 0;
            else
                ddlTittle.SelectedValue = applicant.Title;

            txtFirstNm.Text = applicant.FirstNm;
            txtMiddleNm.Text = applicant.MidNm;
            txtSurNm.Text = applicant.SurNm;
            rblGender.SelectedValue = applicant.Gender;
            //CalDOB.SelDate = Convert.ToString(Convert.ToDateTime(applicant.DOB, provider).Day);

            if (applicant.DOB != string.Empty)
            {
                CalDOB.SelDate = applicant.DOB;
            }

            if (applicant.Citizenship.Length == 0)
                ddlCtznShp.SelectedIndex = 0;
            else
                ddlCtznShp.SelectedValue = applicant.Citizenship;

            //Commented by chellappa on 2023 enhancements
            //if (applicant.MaritalSts.Length == 0)
            //    ddlMaritalSts.SelectedIndex = 0;
            //else
            //    ddlMaritalSts.SelectedValue = applicant.MaritalSts;





            if (Convert.ToString(ViewState["Sequence"]) == "1")
            {
                txtMomName.Text = applicant.MothersMaidenNm;
            }
            else
            {
                if (txtMomName.Text.Trim() == string.Empty)
                {
                    AccountDtl AcDtl = null;
                    if (Session["AcDtl"] != null)
                        AcDtl = (AccountDtl)Session["AcDtl"];
                    else
                        txtMomName.Text = AcDtl.appList != null && AcDtl.appList.Count > 0 ? AcDtl.appList[0].MothersMaidenNm : string.Empty;
                }
            }


            txtPlaceOfBirth.Text = applicant.PlaceOfBirth;

            //Employment Details
            if (applicant.EmpType.Length == 0)
                ddlEmpType.SelectedIndex = 0;
            else
                ddlEmpType.SelectedValue = applicant.EmpType;

            if (ddlEmpType.SelectedValue.ToUpper() == "others".ToUpper())
            {
                pnlEmpOth.Visible = true;
                txtEmptypOth.Text = applicant.EmptypOth;
            }

            //Contact Details    
            txtHomeTelPhNo.Text = applicant.HomeTelNo;
            txtMobileNo.Text = applicant.MobileNo;
            txtEmailAddr.Text = applicant.EmailAddr;

            //2024
            ViewState["OldEmailVal"] = applicant.EmailAddr;
            //End

            //FATCA

            rblUSPerson.SelectedValue = applicant.IsUSperson;
            if (rblUSPerson.SelectedValue.ToUpper() == "Yes".ToUpper())
            {
                pnlUSperson.Visible = true;

                //chellaa(20171005)
                if (applicant.PriJrsdctn.Length == 0)
                    ddlPriJuri.SelectedIndex = 0;
                else
                    ddlPriJuri.SelectedValue = applicant.PriJrsdctn;

                if (applicant.AdJrsdctn1.Length == 0)
                    ddlAddJuri.SelectedIndex = 0;
                else
                    ddlAddJuri.SelectedValue = applicant.AdJrsdctn1;


                if (applicant.AdJrsdctn2.Length == 0)
                    ddlAddJuri1.SelectedIndex = 0;
                else
                    ddlAddJuri1.SelectedValue = applicant.AdJrsdctn2;


                txtTin1.Text = applicant.PriTIN;
                txtTin2.Text = applicant.AdTIN1;
                txtTin3.Text = applicant.AdTIN2;
                txtReasonTin.Text = applicant.ResnNAPTIN;
            }
            else
            {
                pnlUSperson.Visible = false;
            }


            //FATCA DETAILS -chella-20171005
            rblPayTax.SelectedValue = applicant.PayTax;
            rblUSCitizen.SelectedValue = applicant.USCitizen;
            rblGreenCard.SelectedValue = applicant.GreenCard;
            rblRealEst.SelectedValue = applicant.RealEst;
            rblassets.SelectedValue = applicant.assets;

            //SOF-chella 20190129           
            ddlSOF.Text = applicant.sof;
            if (ddlSOF.Text.ToUpper() == "others".ToUpper())
            {
                pnlSOFOth.Visible = true;
                txtSOFOth.Text = applicant.sofOth;
            }

            rblIdentity.SelectedValue = applicant.IdenDtls;

            if (rblIdentity.SelectedValue.ToUpper() == "Passport".ToUpper())
            {

                pnlDIdentityNo.Visible = false;
                pnlPassport.Visible = true; pnlCntry.Visible = true;

                rblPassport.SelectedValue = applicant.IsUkPsPrt;
                if (rblPassport.SelectedValue.ToUpper() == "UK".ToUpper())
                {
                    pnlUKPassport.Visible = true;
                    pnlIntPassport.Visible = false;

                    string PassNo = applicant.IdenNo;
                    if (PassNo.Length == 44)
                    {
                        txtUKPassport1.Text = PassNo.Substring(0, 10);
                        txtUKPassport2.Text = PassNo.Substring(10, 3);
                        txtUKPassport3.Text = PassNo.Substring(13, 7);
                        txtUKPassport4.Text = PassNo.Substring(20, 1);
                        txtUKPassport5.Text = PassNo.Substring(21, 7);
                        txtUKPassport6.Text = PassNo.Substring(28, 14);
                        txtUKPassport7.Text = PassNo.Substring(42, 2);
                    }

                    string PassName = applicant.PsPrtName;
                    if (PassName.Length == 44)
                    {
                        txtUKPassportL1.Text = PassName.Substring(0, 2);
                        txtUKPassportL2.Text = PassName.Substring(2, 3);
                        txtUKPassportL3.Text = PassName.Substring(5, 39);
                    }

                }
                else
                {
                    pnlUKPassport.Visible = false;
                    pnlIntPassport.Visible = true;

                    string PassNo = applicant.IdenNo;
                    if (PassNo.Length == 44)
                    {
                        txtIntPassport1.Text = PassNo.Substring(0, 9);
                        txtIntPassport2.Text = PassNo.Substring(9, 1);
                        txtIntPassport3.Text = PassNo.Substring(10, 3);
                        txtIntPassport4.Text = PassNo.Substring(13, 7);
                        txtIntPassport5.Text = PassNo.Substring(20, 1);
                        txtIntPassport6.Text = PassNo.Substring(21, 7);
                        txtIntPassport7.Text = PassNo.Substring(28, 14);
                        txtIntPassport8.Text = PassNo.Substring(42, 1);
                        txtIntPassport9.Text = PassNo.Substring(43, 1);
                    }
                    string PassName = applicant.PsPrtName;
                    if (PassName.Length == 44)
                    {
                        txtIntPassportL1.Text = PassName.Substring(0, 2);
                        txtIntPassportL2.Text = PassName.Substring(2, 3);
                        txtIntPassportL3.Text = PassName.Substring(5, 39);
                    }
                }

                if ((applicant.PsPrtIssDt != null) && (applicant.PsPrtIssDt != string.Empty))
                    calDOI.SelDate = applicant.PsPrtIssDt;

                if ((applicant.PsPrtExpDt != null) && (applicant.PsPrtExpDt != string.Empty))     //chellappa
                    calDOE.SelDate = applicant.PsPrtExpDt;

                ddlPsPrtIsCntry.SelectedValue = applicant.PsPrtIsCntry;

            }
            else
            {
                // Driving Licence details
                if (rblIdentity.SelectedValue.ToUpper() == "Driving Licence".ToUpper())         // put if condition - chellappa
                {
                    string DrNo = applicant.IdenNo;
                    if (DrNo.Length == 16)
                    {
                        txtDRLNo1.Text = DrNo.Substring(0, 5);
                        txtDRLNo2.Text = DrNo.Substring(5, 6);
                        txtDRLNo3.Text = DrNo.Substring(11, 5);
                    }
                    rbldltype.SelectedValue = applicant.DrLceType;
                    txtDVLCPcode.Text = applicant.DrLcePCode;
                    if ((applicant.DrLceExpDt != null) && (applicant.DrLceExpDt != string.Empty))
                    {
                        calDVLCDOE.SelDate = applicant.DrLceExpDt;
                    }
                    else
                    {
                        //
                    }




                    pnlPassport.Visible = false; pnlCntry.Visible = false;
                    pnlDIdentityNo.Visible = true;

                    ddlPsPrtIsCntry.SelectedIndex = 0;
                }
            }


            txtNINO.Text = applicant.NINO;

            //Joint Applicant Address Details
            if (rblJntApAddSts.SelectedValue.ToUpper() == "Yes".ToUpper())
            {
                pnlAddrDtls.Visible = false;
            }
            else
            {
                pnlAddrDtls.Visible = true;
            }

            //Current Address
            txtCurDrNo.Text = applicant.CurDoorNo;
            txtCurAddr1.Text = applicant.CurAddr1;
            txtCurAddr2.Text = applicant.CurAddr2;
            ddlCurCity.Text = applicant.CurAddr3;
            txtCurCounty.Text = applicant.CurCounty;
            txtCurPostCode.Text = applicant.CurPcode;

            if (applicant.CurCntry.Length == 0)
                ddlCurCountry.SelectedIndex = 0;
            else
                ddlCurCountry.SelectedValue = applicant.CurCntry;

            txtRsdnSnc.Text = applicant.ResidingSince;

            DateTime newDt;
            if (DateTime.TryParse(txtRsdnSnc.Text, out newDt))
            {
                double dDiff = (Convert.ToDateTime(newDt, provider) - Convert.ToDateTime(DateTime.Today.AddYears(-1), provider)).TotalDays;

                if (dDiff > 0)
                {
                    //Previous Address
                    pnlPreAddr.Visible = true;

                    txtPreDrNo.Text = applicant.PreDoorNo;
                    txtPreAddr1.Text = applicant.PreAddr1;
                    txtPreAddr2.Text = applicant.PreAddr2;
                    //txtPreAddr3.Text = applicant.PreAddr3;
                    ddlPreCity.Text = applicant.PreAddr3;
                    txtPreCounty.Text = applicant.PreCounty;
                    txtPrePostCode.Text = applicant.PrePcode;

                    if (applicant.PreCntry.Length == 0)
                        ddlPreCountry.SelectedIndex = 0;
                    else
                        ddlPreCountry.SelectedValue = applicant.PreCntry;


                }
                else
                {
                    pnlPreAddr.Visible = false;
                }
            }

            btnSave.Text = "Modify Applicant";

        }

        catch (Exception ex)
        {
            throw ex;
        }

    }

    private void clearControls()
    {
        try
        {
            lblUploadError.Text = string.Empty;
            lblUploadInfo.Text = string.Empty;
            lblAlert.Text = string.Empty;

            lbldoberror.Text = string.Empty;
            lblDoissueerr.Text = string.Empty;
            lblDoExerr.Text = string.Empty;
            lblDLDEerr.Text = string.Empty;

            lblRefNo.Text = string.Empty;
            ddlTittle.SelectedIndex = 0;
            txtFirstNm.Text = string.Empty;
            txtMiddleNm.Text = string.Empty;
            txtSurNm.Text = string.Empty;
            //btnCancel.Visible = false;
            btnSave.Text = "Add Applicant";

            //Employment Details
            ddlEmpType.SelectedIndex = 0; ;
            txtEmptypOth.Text = string.Empty;

            //Personal Details        
            ddlTittle.SelectedIndex = 0;
            txtFirstNm.Text = string.Empty;
            txtMiddleNm.Text = string.Empty;
            txtSurNm.Text = string.Empty;
            rblGender.SelectedIndex = 0;
            CalDOB.SelDate = DateTime.Now.ToString("dd/MM/yyyy");
            txtDOB.Text = string.Empty;
            ddlCtznShp.SelectedIndex = 0;
            //ddlMaritalSts.SelectedIndex = 0;

            //Contact Details    
            txtHomeTelPhNo.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtEmailAddr.Text = string.Empty;

            #region 20150805 Modified by senthil based on Aug 2015 change request
            //Identification Details
            //rblIdentity.SelectedValue = "DRIVING LICENCE";
            rblIdentity.SelectedIndex = 0; rbldltype.SelectedIndex = 0;                                 //chellappa
            pnlPassport.Visible = false; pnlCntry.Visible = false;
            pnlUKPassport.Visible = false; pnlIntPassport.Visible = false;
            pnlDIdentityNo.Visible = true;

            //rblPassport.SelectedValue = "UK";
            rblPassport.SelectedIndex = 0;                                   //chellappa
            calDOI.SelDate = DateTime.Now.ToString("dd/MM/yyyy");
            calDOE.SelDate = DateTime.Now.ToString("dd/MM/yyyy");
            txtDOI.Text = string.Empty;
            txtDOE.Text = string.Empty;
            ddlPsPrtIsCntry.SelectedIndex = 0;

            txtDRLNo1.Text = string.Empty;
            txtDRLNo2.Text = string.Empty;
            txtDRLNo3.Text = string.Empty;

            txtIntPassportL1.Text = string.Empty;
            txtIntPassportL2.Text = string.Empty;
            txtIntPassportL3.Text = string.Empty;

            txtIntPassport1.Text = string.Empty;
            txtIntPassport2.Text = string.Empty;
            txtIntPassport3.Text = string.Empty;
            txtIntPassport4.Text = string.Empty;
            txtIntPassport5.Text = string.Empty;
            txtIntPassport6.Text = string.Empty;
            txtIntPassport7.Text = string.Empty;
            txtIntPassport8.Text = string.Empty;
            txtIntPassport9.Text = string.Empty;


            txtUKPassportL1.Text = string.Empty;
            txtUKPassportL2.Text = string.Empty;
            txtUKPassportL3.Text = string.Empty;

            txtUKPassport1.Text = string.Empty;
            txtUKPassport2.Text = string.Empty;
            txtUKPassport3.Text = string.Empty;
            txtUKPassport4.Text = string.Empty;
            txtUKPassport5.Text = string.Empty;
            txtUKPassport6.Text = string.Empty;
            txtUKPassport7.Text = string.Empty;
            #endregion
            rblJntApAddSts.SelectedValue = "NO";

            txtNINO.Text = string.Empty;

            //Current Address
            pnlAddrDtls.Visible = true;
            txtCurDrNo.Text = string.Empty;
            txtCurAddr1.Text = string.Empty;
            txtCurAddr2.Text = string.Empty;
            //txtCurAddr3.Text = string.Empty;
            ddlCurCity.SelectedIndex = 0;
            //txtCurTown.Text = string.Empty;
            txtCurCounty.Text = string.Empty;
            txtCurPostCode.Text = string.Empty;
            //ddlCurCountry.SelectedIndex = 0;
            txtRsdnSnc.Text = string.Empty;


            //Previous Address
            pnlPreAddr.Visible = false;
            txtPreDrNo.Text = string.Empty;
            txtPreAddr1.Text = string.Empty;
            txtPreAddr2.Text = string.Empty;
            //txtPreAddr3.Text = string.Empty;
            ddlPreCity.SelectedIndex = 0;
            //txtPreTown.Text = string.Empty;
            txtPreCounty.Text = string.Empty;
            txtPrePostCode.Text = string.Empty;
            ddlPreCountry.SelectedIndex = 0;

            //FATCA
            ddlPriJuri.SelectedIndex = 0;
            ddlAddJuri.SelectedIndex = 0;
            ddlAddJuri1.SelectedIndex = 0;
            txtTin1.Text = string.Empty;
            txtTin2.Text = string.Empty;
            txtTin3.Text = string.Empty;
            txtReasonTin.Text = string.Empty;

            //FATCA DETAILS - chella-20171005
            rblPayTax.SelectedIndex = 0;
            rblUSCitizen.SelectedIndex = 0;
            rblGreenCard.SelectedIndex = 0;
            rblRealEst.SelectedIndex = 0;
            rblassets.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        List<Applicant> lstUsr = null;
        int seq = 0;
        try
        {

            obj.StoreEvent("66309", txtFirstNm.Text.Trim(), Convert.ToString(Session["RefNo"]), "", "", Convert.ToString(Session["RefNo"]));

            //if (ChkVal() == false)
            //{
            //    return;
            //}

            //if (CheckRequiredData() == false)
            //{
            //    return;
            //}


            if (Page.IsValid == true)
            {
                //2024 email verification
                //if (Convert.ToString(ViewState["OldEmailVal"]) != txtEmailAddr.Text.Trim())
                if (Convert.ToString(ViewState["Sequence"]) == "1")
                {
                    if (chkEmailVerSts(txtEmailAddr.Text.Trim()) == false)
                    {
                        lblAlert.CssClass = "text-danger font-weight-bold font-16";
                        lblAlert.Text = "Please verify your email address before proceeding to the next step.";
                        txtEmailAddr.Focus();
                        return;
                    }
                }
                //End

                btnSave.Enabled = false;

                oAcDtl = (AccountDtl)Session["AcDtl"];

                if (oAcDtl != null && oAcDtl.appList != null && oAcDtl.NoOfApplicants > 0)
                {

                    if (PrimaryValidation())
                    {
                        //Chellappa - 20181128
                        if (IsExistingPendingCust())
                        {
                            lblAlert.CssClass = "text-danger font-weight-bold font-16";
                            lblAlert.Text = "Applicant is an existing customer of UBI(UK) Ltd. Kindly Contact Bank for Assistance.";
                            obj.StoreEvent("88888", "", "", "", "Applicant is an existing pending customer of UBI(UK) Ltd. Kindly Contact Bank for Assistance.", Convert.ToString(Session["RefNo"]));
                            return;
                        }

                        if (IsExistingcustomer())
                        {
                            //lblAlert.Text = "The " + txtFirstNm.Text.Trim() + " applicant already have account with UBI UK, existing active customer could not use this application. Kindly contact branch.";

                            lblAlert.CssClass = "text-danger font-weight-bold font-16";
                            lblAlert.Text = "Applicant is an existing customer of UBI(UK) Ltd. Kindly Contact Bank for Assistance.";
                            obj.StoreEvent("88888", "", "", "", "Applicant is an existing customer of UBI(UK) Ltd. Kindly Contact Bank for Assistance.", Convert.ToString(Session["RefNo"]));
                            return;
                        }

                        //if (!bIsDuplicate())
                        //{

                        if (oAcDtl.appList.Count > 1 && rblJntApAddSts.SelectedValue.ToUpper() == "YES")
                        {

                        }
                        else
                        {
                            if (!IsValidCity())
                            {
                                lblCiryErr.Text = "City name seems invalid kindly correct the city name and continue";
                                return;
                            }
                        }

                        //2024 - Enhancements
                        if (Convert.ToString(ViewState["Sequence"]) == "1")
                        {
                            string fGetEMVerSts = string.Empty;
                            string sEMVERIFIEDStsVal = string.Empty;
                            DataRow drGetEMVerSts;
                            fGetEMVerSts = "SELECT EMVERIFIED FROM OTPFOREMAIL WHERE EMOTPREFNO='" + Convert.ToString(Session["RefNo"]) + "' AND EMOTPEMAIL='" + txtEmailAddr.Text.Trim() + "' AND OTPSTATUS='Y'";
                            DataTable dtGetEMVerSts = obj.ExecuteData(fGetEMVerSts);
                            if (dtGetEMVerSts != null && dtGetEMVerSts.Rows.Count > 0)
                            {
                                drGetEMVerSts = dtGetEMVerSts.Rows[0];
                                sEMVERIFIEDStsVal = obj.NullToSpace(drGetEMVerSts["EMVERIFIED"]);
                                if (sEMVERIFIEDStsVal == "Y")
                                {
                                    oAcDtl.EMVerSts = "Y";
                                }
                            }
                        }
                        //End

                        SaveValues();



                        //ModalPopupPassAvlModfy.Show();  //chellappa-20181113

                        /* The popup is commented and below code is implemented for UPBv2 enhancement Aug-2021 */
                        clearControls();
                        Cache.Remove("AcDtl" + Convert.ToString(Session["RefNo"]));
                        Response.Redirect("AccountDetails.aspx", false);



                        //chellappa- 20181113
                        //clearControls();
                        //Cache.Remove("AcDtl" + Convert.ToString(Session["RefNo"]));
                        //Response.Redirect("AccountDetails.aspx", false);
                        //chellappa- 20181113

                        //}
                        //else
                        //{
                        //    lblAlert.Text = "The same applicant available in the list, kindly check the applicants and continue.";
                        //}
                    }
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
            btnSave.Enabled = true;
        }

    }

    //chellappa- 20181113
    protected void btnPassAvlModfy_Click(object sender, EventArgs e)
    {
        try
        {
            clearControls();
            Cache.Remove("AcDtl" + Convert.ToString(Session["RefNo"]));
            Response.Redirect("AccountDetails.aspx", false);

            ModalPopupPassAvlModfy.Hide();
            ModalPopupPassAvlModfy.BackgroundCssClass = "sctableBackground d-none";
            PnlPassModfy.CssClass = "d-none zindex_1";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void DisableControls()
    {
        try
        {
            AccountDtl AcDtl = null;
            AcDtl = (AccountDtl)Session["AcDtl"];

            if (AcDtl.IsJntAc.ToUpper() == "Yes".ToUpper())
            {
                List<Applicant> lstUsr = null;
                lstUsr = GetUserList();
                if (lstUsr.Count > 4)
                {
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(ViewState["FT"]) != string.Empty && Convert.ToString(ViewState["FT"]) == "Y")
            {
                lblDataLostMsg.Text = string.Empty;
                if (txtFirstNm.Text.Length > 0)
                    lblDataLostMsg.Text = "The current applicant (" + txtFirstNm.Text + ") data will be lost! Do you want to continue?";
                else
                    lblDataLostMsg.Text = "The current applicant data will be lost! Do you want to continue?";

                MPEDataLose.Show();
                MPEDataLose.BackgroundCssClass = "sctableBackground d-block";
                PnlMsgBox.CssClass = "d-block zindex_1";
            }
            else
            {

                pnlJntAplntAddr.Visible = true;
                clearControls();

                List<Applicant> lstUsr = null;
                lstUsr = GetUserList();
                if ((lstUsr != null) && (lstUsr.Count > 3))
                {
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                }

                Cache.Remove("AcDtl" + Convert.ToString(Session["RefNo"]));
                Response.Redirect("AccountDetails.aspx", false);

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void txtRsdnSnc_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime dt;
            lblResErr.Text = "";
            dt = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
            if (txtRsdnSnc.Text.Trim() != string.Empty)
            {
                DateTime dtFormat;
                string[] formats = { "dd/MM/yyyy" };
                if (DateTime.TryParseExact(txtRsdnSnc.Text.Trim(), formats, provider, DateTimeStyles.None, out dtFormat)) // format check
                {
                    if ((IsDate(txtRsdnSnc.Text)) == true)
                    {
                        if (DateTime.Parse(txtRsdnSnc.Text.ToString()) <= dt)
                        {
                            if (txtRsdnSnc.Text.Trim().Length > 0)
                            {
                                DateTime newDt;

                                if (DateTime.TryParse(txtRsdnSnc.Text.Trim(), out newDt))
                                {
                                    double dDiff = (Convert.ToDateTime(newDt, provider) - Convert.ToDateTime(DateTime.Today.AddYears(-1), provider)).TotalDays;
                                    if (dDiff > 0)
                                    {
                                        pnlPreAddr.Visible = true;
                                    }
                                    else
                                    {
                                        pnlPreAddr.Visible = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            lblResErr.Text = "Date of Residence should be less than or Equal to current date!";
                            pnlPreAddr.Visible = false;
                        }
                    }
                    else/* UPBv2 Aug-2021 */
                    {
                        lblResErr.Text = "Invalid date format, date must be dd/MM/yyyy format.";
                        pnlPreAddr.Visible = false;
                    }
                }
                else/* UPBv2 Aug-2021 */
                {
                    lblResErr.Text = "Invalid date format, date must be dd/MM/yyyy format.";
                    pnlPreAddr.Visible = false;
                }

            }
            else
            {
                lblResErr.Text = "";
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
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

    private bool IsDuplicate(string pType)
    {
        bool bStatus = true;

        try
        {
            oAcDtl = (AccountDtl)Session["AcDtl"];


            if (oAcDtl != null && oAcDtl.appList != null && oAcDtl.appList.Count == 1)
            {
                bStatus = false;
            }
            else if (oAcDtl != null && oAcDtl.appList != null && oAcDtl.appList.Count > 1)
            {
                string sFirstNm, sDOB;
                sFirstNm = txtFirstNm.Text.Trim().ToUpper().Replace(" ", "");


                if (IsDate(CalDOB.SelDate.ToString()) == true)
                {
                    sDOB = CalDOB.SelDate;

                    Applicant Usr = (from a in oAcDtl.appList where a.FirstNm.ToUpper().Replace(" ", "") == sFirstNm && a.DOB == sDOB && a.Sequence != Convert.ToInt32(ViewState["Sequence"]) select a).FirstOrDefault();
                    if (Usr != null && Usr.FirstNm.ToUpper() == sFirstNm && Usr.DOB == sDOB)
                        bStatus = true;
                    else
                        bStatus = false;
                }
                else
                {
                    if (pType == "SE")
                    {
                        sDOB = string.Empty;

                        Applicant Usr = (from a in oAcDtl.appList where a.FirstNm.ToUpper().Replace(" ", "") == sFirstNm && a.Sequence != Convert.ToInt32(ViewState["Sequence"]) select a).FirstOrDefault();
                        if (Usr != null && Usr.FirstNm.ToUpper() == sFirstNm)
                            bStatus = true;
                        else
                            bStatus = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return bStatus;
    }

    //chellappa-20181128
    private bool IsExistingPendingCust()
    {
        bool bBendStatus = true;
        DataTable dtben = null;
        string sNAME, sTDFANAME, sTDMINAME, sTDSURNAME, sDOB, sPostCode;
        sNAME = sDOB = sTDFANAME = sTDMINAME = sTDSURNAME = sPostCode = string.Empty;
        string sBendQuery = string.Empty;
        try
        {
            sTDFANAME = obj.replaceSplChr(txtFirstNm.Text.Trim());
            sTDMINAME = obj.replaceSplChr(txtMiddleNm.Text.Trim());
            sTDSURNAME = obj.replaceSplChr(txtSurNm.Text.Trim());
            sNAME = sTDFANAME + sTDMINAME + sTDSURNAME;

            sDOB = obj.DTOC(CalDOB.SelDate);
            sPostCode = obj.replaceSplChr(txtCurPostCode.Text.Trim());


            sBendQuery = " SELECT TDFANAME FROM TDAPPIND WHERE 1=1 " +
                            " AND  " +
                            " UPPER(LTRIM(RTRIM(REPLACE(ISNULL(TDFANAME+TDMINAME+TDSURNAME,''),' ','')))) = UPPER(REPLACE('" + sNAME + "',' ','')) " +
                            " AND  " +
                            " ISNULL(TDFADOB,'')  = '" + sDOB + "'" +
                            " AND  " +
                            " UPPER(LTRIM(RTRIM(REPLACE(ISNULL(TDFAPCODE,''),' ','')))) =  UPPER(REPLACE('" + sPostCode + "',' ','')) " +
                            " AND TDSTATUS IN ('KYP','KYS','CQR','ACO','D4R')";

            dtben = obj.ExecuteData(sBendQuery);
            if ((dtben != null) && (dtben.Rows.Count == 0))
                bBendStatus = false;
        }
        catch (Exception ex)
        {
            bBendStatus = true;
            throw ex;
        }

        return bBendStatus;
    }

    private bool IsExistingcustomer()
    {
        bool bStatus = true;
        DataTable dtAcDtl = null;
        try
        {
            string sFirstNm = obj.replaceSplChr(txtFirstNm.Text.Trim());
            //string sFullName = obj.replaceSplChr(txtFirstNm.Text.Trim() + txtMiddleNm.Text.Trim() + txtSurNm.Text.Trim());
            string sDOB = obj.DTOC(CalDOB.SelDate);
            string sPostCode = obj.replaceSplChr(txtCurPostCode.Text.Trim());

            string sQuery = " SELECT CUSTFULLNAME FROM CUSACSTS WHERE 1=1 " +
                            " AND  " +
                            " UPPER(LTRIM(RTRIM(REPLACE(ISNULL(CUSTFIRSTNAME,''),' ','')))) = UPPER(REPLACE('" + sFirstNm + "',' ','')) " +
                            " AND  " +
                            " CASE ISNULL(CUSTDOB,'') WHEN '' THEN '' ELSE SUBSTRING(CUSTDOB,7,4) + SUBSTRING(CUSTDOB,4,2) + SUBSTRING(CUSTDOB,1,2) END  = '" + sDOB + "'" +
                            " AND  " +
                            " UPPER(LTRIM(RTRIM(REPLACE(ISNULL(CUSTPOSTCD,''),' ','')))) =  UPPER(REPLACE('" + sPostCode + "',' ',''))";

            dtAcDtl = obj.ExecuteData(sQuery);
            if ((dtAcDtl != null) && (dtAcDtl.Rows.Count == 0))
                bStatus = false;
        }
        catch (Exception ex)
        {
            bStatus = true;
            throw ex;
        }

        return bStatus;
    }

    protected void btnNextb_Click(object sender, EventArgs e)
    {

        lblRefNo.Text = string.Empty;
        List<Applicant> lstUsr = null;

        try
        {

            if (Page.IsValid == true)
            {
                btnNextb.Enabled = false;
                oAcDtl = (AccountDtl)Session["AcDtl"];

                ViewState["TotalAppl"] = oAcDtl.NoOfApplicants;     //chellappa - 20181113
                ViewState["AppListCnt"] = oAcDtl.appList.Count();   //chellappa - 20181113

                //2024 - Email address verification
                if (Convert.ToString(ViewState["Sequence"]) == "1")
                {
                    if (chkEmailVerSts(txtEmailAddr.Text.Trim()) == false)
                    {
                        lblAlert.Text = "Please verify your email address before proceeding to the next step.";
                        txtEmailAddr.Focus();
                        return;
                    }
                }
                //End

                if (oAcDtl != null && oAcDtl.appList != null && oAcDtl.NoOfApplicants > 0)
                {
                    if (PrimaryValidation())
                    {
                        //Chellappa - 20181128
                        if (IsExistingPendingCust())
                        {
                            lblAlert.CssClass = "text-danger font-weight-bold font-16";
                            lblAlert.Text = "Applicant is an existing customer of UBI(UK) Ltd. Kindly Contact Bank for Assistance.";
                            obj.StoreEvent("88888", "", "", "", "Applicant is an existing pending customer of UBI(UK) Ltd. Kindly Contact Bank for Assistance.", Convert.ToString(Session["RefNo"]));
                            return;
                        }

                        if (IsExistingcustomer())
                        {
                            lblAlert.CssClass = "text-danger font-weight-bold font-16";
                            //lblAlert.Text = "The " + txtFirstNm.Text.Trim() + " applicant already have account with UBI UK, existing active customer could not use this application. Kindly contact branch.";
                            lblAlert.Text = "Applicant is an existing customer of UBI(UK) Ltd. Kindly Contact Bank for Assistance.";
                            obj.StoreEvent("88888", "", "", "", "Applicant is an existing customer of UBI(UK) Ltd. Kindly Contact Bank for Assistance.", Convert.ToString(Session["RefNo"]));

                            return;
                        }

                        //if (!IsValidCity())
                        //{
                        //    lblCiryErr.Text = "City name seems invalid kindly correct the city name and continue";
                        //    return;
                        //}

                        if (oAcDtl.appList.Count > 1 && rblJntApAddSts.SelectedValue.ToUpper() == "YES")
                        {

                        }
                        else
                        {
                            if (!IsValidCity())
                            {
                                lblCiryErr.Text = "City name seems invalid kindly correct the city name and continue";
                                return;
                            }
                        }

                        SaveValues();

                        //ModalPopupPassAvl.Show();   //chellappa - 20181113

                        /* The popup is commented and below code is implemented for UPBv2 enhancement Aug-2021 */
                        obj.StoreEvent("66308", txtFirstNm.Text.Trim(), Convert.ToString(Session["RefNo"]), "", "", Convert.ToString(Session["RefNo"]));
                        clearControls();
                        if (Convert.ToString(ViewState["ActnCode"]) == "A" && Convert.ToInt32(ViewState["TotalAppl"]) == Convert.ToInt32(ViewState["AppListCnt"]))
                        {
                            Cache.Remove("OthDtl" + Convert.ToString(Session["RefNo"]));
                            Response.Redirect("BondDetails.aspx", false);
                        }
                        else
                        {
                            Cache.Remove("AcDtl" + Convert.ToString(Session["RefNo"]));
                            Response.Redirect("AccountDetails.aspx", false);
                        }


                        //chellappa - 20181113
                        //obj.StoreEvent("66308", txtFirstNm.Text.Trim(), Convert.ToString(Session["RefNo"]), "", "", Convert.ToString(Session["RefNo"]));
                        //clearControls();
                        //if (Convert.ToString(ViewState["ActnCode"]) == "A" && oAcDtl.NoOfApplicants == oAcDtl.appList.Count())
                        //{
                        //    Cache.Remove("OthDtl" + Convert.ToString(Session["RefNo"]));
                        //    Response.Redirect("BondDetails.aspx", false);
                        //}
                        //else
                        //{
                        //    Cache.Remove("AcDtl" + Convert.ToString(Session["RefNo"]));
                        //    Response.Redirect("AccountDetails.aspx", false);
                        //}
                        //chellappa - 20181113
                    }
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

    //chellappa - 20181113
    protected void btnPassAvl_Click(object sender, EventArgs e)
    {
        try
        {
            obj.StoreEvent("66308", txtFirstNm.Text.Trim(), Convert.ToString(Session["RefNo"]), "", "", Convert.ToString(Session["RefNo"]));
            clearControls();
            if (Convert.ToString(ViewState["ActnCode"]) == "A" && Convert.ToInt32(ViewState["TotalAppl"]) == Convert.ToInt32(ViewState["AppListCnt"]))
            {
                Cache.Remove("OthDtl" + Convert.ToString(Session["RefNo"]));
                Response.Redirect("BondDetails.aspx", false);
            }
            else
            {
                Cache.Remove("AcDtl" + Convert.ToString(Session["RefNo"]));
                Response.Redirect("AccountDetails.aspx", false);
            }

            ModalPopupPassAvl.Hide();
            ModalPopupPassAvl.BackgroundCssClass = "sctableBackground d-none";
            PnlPass.CssClass = "d-none zindex_1";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool PrimaryValidation()
    {
        bool bStatus = false;

        string sTitle = ddlTittle.SelectedValue;
        string sFirstNm = txtFirstNm.Text.Trim();
        string sMidNm = txtMiddleNm.Text.Trim();
        string sSurNm = txtSurNm.Text.Trim();
        string sGender = rblGender.SelectedValue;
        //string sMaritalSts = ddlMaritalSts.SelectedValue;  commented on 2023
        string sDOB = CalDOB.SelDate;
        string sPlaceOfBirth = txtPlaceOfBirth.Text.Trim();
        string sMothMaidName = txtMomName.Text.Trim();
        string sCitizenship = ddlCtznShp.SelectedValue;
        string sNINO = txtNINO.Text.Trim();
        string sTelphoneNo = txtHomeTelPhNo.Text.Trim();
        string sMobileNo = txtMobileNo.Text.Trim();
        string sEmailAddr = txtEmailAddr.Text.Trim();

        string sIdentityDtls = rblIdentity.SelectedValue;

        string sEmpType = ddlEmpType.SelectedValue;
        string sEmpOth = txtEmptypOth.Text.Trim();

        string sCurPcode = txtCurPostCode.Text.Trim();
        string sCurDoorNo = txtCurDrNo.Text.Trim();
        string sCurAddr1 = txtCurAddr1.Text.Trim();
        string sCurAddr2 = txtCurAddr2.Text.Trim();
        //string sCurAddr3 = txtCurAddr3.Text.Trim();
        string sCurAddr3 = ddlCurCity.SelectedValue.Trim();
        string sCurCountry = ddlCurCountry.SelectedValue;
        string sResidingSince = txtRsdnSnc.Text.Trim();

        string sPrePcode = txtPrePostCode.Text.Trim();
        string sPreDoorNo = txtPreDrNo.Text.Trim();
        string sPreAddr1 = txtPreAddr1.Text.Trim();
        string sPreAddr2 = txtPreAddr2.Text.Trim();
        //string sPreAddr3 = txtPreAddr3.Text.Trim();
        string sPreAddr3 = ddlPreCity.SelectedValue.Trim();
        string sPreCountry = ddlPreCountry.SelectedValue;

        string sIsUsPerson = rblUSPerson.SelectedValue;
        string sPriJrsdctn = ddlPriJuri.SelectedValue;
        string sPriTIN = txtTin1.Text.Trim();
        string sAdJrsdctn1 = ddlAddJuri.SelectedValue;
        string sAdTIN1 = txtTin2.Text.Trim();
        string sAdJrsdctn2 = ddlAddJuri1.SelectedValue;
        string sAdTIN2 = txtTin3.Text.Trim();
        string sRFNPTin = txtReasonTin.Text.Trim();

        //20171110 - Chellappa
        string sPayTax = rblPayTax.SelectedValue;
        string sUSCitizen = rblUSCitizen.SelectedValue;
        string sGreenCard = rblGreenCard.SelectedValue;
        string sRealEst = rblRealEst.SelectedValue;
        string sAsset = rblassets.SelectedValue;

        //20190129-chellappa
        string SOF = ddlSOF.SelectedValue;
        string SOFOth = txtSOFOth.Text.Trim();

        try
        {



            #region Personal Details

            if ((sTitle == string.Empty) || (sTitle == "-1"))
            {
                lblAlert.Text = "Please select the Title";
                ddlTittle.Focus();
                bStatus = false;
                return bStatus;
            }




            if (sFirstNm == string.Empty)
            {
                lblAlert.Text = "Please enter the First name";
                txtFirstNm.Focus();
                bStatus = false;
                return bStatus;
            }
            if (sFirstNm.Length < 2)
            {
                lblAlert.Text = "First name should have atleast 2 alpha characters";
                txtFirstNm.Focus();
                bStatus = false;
                return bStatus;
            }
            if (sSurNm == string.Empty)
            {
                lblAlert.Text = "Please enter the Surname";
                txtSurNm.Focus();
                bStatus = false;
                return bStatus;
            }
            if (sSurNm.Length < 2)
            {
                lblAlert.Text = "Surname should have atleast 2 alpha characters";
                txtSurNm.Focus();
                bStatus = false;
                return bStatus;
            }
            if (sGender == string.Empty)
            {
                lblAlert.Text = "Please select the Gender";
                rblGender.Focus();
                bStatus = false;
                return bStatus;
            }


            //Chellappa - 20190326
            string[] GenderMList = { "MR", "SIR" };
            string[] GenderFList = { "MS", "MISS", "MRS" };
            if (GenderMList.Contains(sTitle.ToUpper()))
            {
                if (sGender != "MALE")
                {
                    lblAlert.Text = "Gender and Title mismatched";
                    ddlTittle.Focus();
                    bStatus = false;
                    return bStatus;
                }
            }
            if (GenderFList.Contains(sTitle.ToUpper()))
            {
                if (sGender != "FEMALE")
                {
                    lblAlert.Text = "Gender and Title mismatched";
                    ddlTittle.Focus();
                    bStatus = false;
                    return bStatus;
                }
            }
            //commented on 2023
            //if ((sMaritalSts == string.Empty) || (sMaritalSts == "-1"))
            //{
            //    lblAlert.Text = "Please select the Marital status";
            //    ddlMaritalSts.Focus();
            //    bStatus = false;
            //    return bStatus;
            //}
            if (IsDate(sDOB) == true)
            {
                if (CalAge(sDOB) < 18)
                {
                    //lbldoberror.Text = "Date of Birth should be greater than 18 years!";
                    lbldoberror.Text = "The Date of Birth should be above 18 Years!";
                    CalDOB.Focus();
                    return false;
                }
            }
            else
            {
                lbldoberror.Text = "Invalid Date of Birth !";
                CalDOB.CssClass = "error";
                CalDOB.Focus();
                return false;
            }
            if (sPlaceOfBirth == string.Empty)
            {
                lblAlert.Text = "Please enter your Place Of Birth";
                txtPlaceOfBirth.Focus();
                bStatus = false;
                return bStatus;
            }
            if (pnlMemWrd.Visible == true)
            {
                if (sMothMaidName == string.Empty)
                {
                    lblAlert.Text = "Please enter your Mothers maiden name";
                    txtMomName.Focus();
                    bStatus = false;
                    return bStatus;
                }
            }
            //if (sMothMaidName == string.Empty)
            //{
            //    lblAlert.Text = "Please enter your Mothers maiden name";
            //    txtMomName.Focus();
            //    bStatus = false;
            //    return bStatus;
            //}
            if (sCitizenship == string.Empty || sCitizenship == "-1")
            {
                lblAlert.Text = "Please select your Citizenship";
                ddlCtznShp.Focus();
                bStatus = false;
                return bStatus;
            }

            if (sNINO.Length > 0)
            {
                if (sNINO.Length == 9)
                {
                    if (Regex.IsMatch(sNINO, @"^[a-zA-Z]{2}[0-9]{6}[a-zA-Z]{1}$"))
                    {
                        // Name does not match schema
                    }
                    else
                    {
                        lblAlert.Text = "National Insurance number is invalid";
                        txtNINO.Focus();
                        return false;
                    }
                }
                else
                {
                    lblAlert.Text = "National Insurance number is invalid";
                    txtNINO.Focus();
                    return false;
                }

            }

            #endregion

            if (IsDuplicate("SN"))
            {
                //lblAlert.Text = "The same " + sFirstNm.ToUpper() + " applicant available in the applicant list, kindly go to step-1 check the applicants and continue.";
                lblAlert.Text = "The Same applicant details are available in the applicant list, kindly go to step-1 check the applicants and continue.";
                obj.StoreEvent("88888", "", "", "", "The Same applicant details are available in the applicant list, kindly go to step-1 check the applicants and continue.", Convert.ToString(Session["RefNo"]));

                return false;
            }

            # region Contact details

            if (sMobileNo == string.Empty)
            {
                lblAlert.Text = "Please enter mobile number";
                txtMobileNo.Focus();
                bStatus = false;
                return bStatus;
            }
            if (sMobileNo.Length != 10)
            {
                lblAlert.Text = "Mobile no length should be 10 numeric characters";
                txtMobileNo.Focus();
                bStatus = false;
                return bStatus;
            }
            if (sEmailAddr == string.Empty)
            {
                lblAlert.Text = "Please enter Email Address";
                txtEmailAddr.Focus();
                bStatus = false;
                return bStatus;
            }
            else
            {
                string pattern = pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

                if (!Regex.IsMatch(sEmailAddr, pattern))
                {
                    lblAlert.Text = "Please enter valid Email Address";
                    bStatus = false;
                    return bStatus;
                }
            }

            #endregion

            #region Identity Details

            if (sIdentityDtls == string.Empty)
            {
                lblAlert.Text = "Please select the Identity Details";
                rblIdentity.Focus();
                bStatus = false;
                return bStatus;
            }
            else
            {
                #region Driving License

                if (sIdentityDtls.ToUpper() == "DRIVING LICENCE")
                {
                    //Driving License
                    string sDLType = rbldltype.SelectedValue;
                    string sDLNo1 = txtDRLNo1.Text.Trim();
                    string sDLNo2 = txtDRLNo2.Text.Trim();
                    string sDLNo3 = txtDRLNo3.Text.Trim();
                    string sDL_DOE = calDVLCDOE.SelDate;
                    string sDLPostCode = txtDVLCPcode.Text.Trim();

                    if (sDLType == string.Empty)
                    {
                        lblAlert.Text = "Please select the Driving Licence Type";
                        rbldltype.Focus();
                        bStatus = false;
                        return bStatus;
                    }
                    if (sDLNo1 == string.Empty)
                    {
                        lblAlert.Text = "Please enter the Driving Licence Box-1";
                        txtDRLNo1.Focus();
                        bStatus = false;
                        return bStatus;
                    }
                    if (sDLNo1.Length != 5)
                    {
                        lblAlert.Text = "Driving Licence Box-1 should be 5 characters";
                        txtDRLNo1.Focus();
                        bStatus = false;
                        return bStatus;
                    }
                    if (sDLNo2 == string.Empty)
                    {
                        lblAlert.Text = "Please enter the Driving Licence Box-2";
                        txtDRLNo1.Focus();
                        bStatus = false;
                        return bStatus;
                    }
                    if (sDLNo2.Length != 6)
                    {
                        lblAlert.Text = "Driving Licence Box-2 should be 6 numeric characters";
                        txtDRLNo2.Focus();
                        bStatus = false;
                        return bStatus;
                    }
                    if (sDLNo3 == string.Empty)
                    {
                        lblAlert.Text = "Please enter the Driving Licence Box-3";
                        txtDRLNo3.Focus();
                        bStatus = false;
                        return bStatus;
                    }
                    if (sDLNo3.Length != 5)
                    {
                        lblAlert.Text = "Driving Licence Box-3 should be 5 characters";
                        txtDRLNo3.Focus();
                        bStatus = false;
                        return bStatus;
                    }

                    if ((sDLNo1 + sDLNo2 + sDLNo3).Length != 16)
                    {
                        lblAlert.Text = "Invalid driving licence number";
                        bStatus = false;
                        return bStatus;
                    }

                    if (IsDate(sDL_DOE))
                    {
                        DateTime dt;
                        dt = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));

                        if (DateTime.Parse(sDL_DOE) < dt)
                        {
                            lblDLDEerr.Text = "Driving licence Expiry date should be greater than current date!";
                            calDVLCDOE.CssClass = "error";
                            calDVLCDOE.Focus();
                            return false;
                        }
                    }
                    else
                    {
                        lblDLDEerr.Text = "Invalid Driving licence expiry date";
                        calDVLCDOE.CssClass = "error";
                        calDVLCDOE.Focus();
                        return bStatus;
                    }
                    if (sDLPostCode == string.Empty)
                    {
                        lblAlert.Text = "Please enter the postcode on Licence";
                        txtDVLCPcode.Focus();
                        bStatus = false;
                        return bStatus;
                    }
                }

                #endregion

                #region Passport
                if (sIdentityDtls.ToUpper() == "PASSPORT")
                {

                    string sPPType = rblPassport.SelectedValue;

                    string sPPCOI = ddlPsPrtIsCntry.SelectedValue;
                    string sPPDOI = calDOI.SelDate;
                    string sPPDOE = calDOE.SelDate;

                    if (sPPType == string.Empty)
                    {
                        lblAlert.Text = "Please select the passport type";
                        rblPassport.Focus();
                        bStatus = false;
                        return bStatus;
                    }

                    #region UK Passport

                    if (sPPType.ToUpper() == "UK")
                    {
                        string sUKPPLine1of3 = txtUKPassportL1.Text.Trim();
                        string sUKPPLine2of3 = txtUKPassportL2.Text.Trim();
                        string sUKPPLine3of3 = txtUKPassportL3.Text.Trim();

                        string sUKPPLine1of7 = txtUKPassport1.Text.Trim();
                        string sUKPPLine2of7 = txtUKPassport2.Text.Trim();
                        string sUKPPLine3of7 = txtUKPassport3.Text.Trim();
                        string sUKPPLine4of7 = txtUKPassport4.Text.Trim();
                        string sUKPPLine5of7 = txtUKPassport5.Text.Trim();
                        string sUKPPLine6of7 = txtUKPassport6.Text.Trim();
                        string sUKPPLine7of7 = txtUKPassport7.Text.Trim();

                        if (sUKPPLine1of3 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport first line Box-1";
                            txtUKPassportL1.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sUKPPLine1of3.Length != 2)
                        {
                            lblAlert.Text = "Passport first line Box-1 should be 2 characters length";
                            txtUKPassportL1.Focus();
                            bStatus = false;
                            return bStatus;
                        }

                        if (sUKPPLine2of3 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport first line Box-2";
                            txtUKPassportL2.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sUKPPLine2of3.Length != 3)
                        {
                            lblAlert.Text = "Passport first line Box-2 should be 3 characters length";
                            txtUKPassportL2.Focus();
                            bStatus = false;
                            return bStatus;
                        }

                        if (sUKPPLine3of3 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport first line Box-3";
                            txtUKPassportL3.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sUKPPLine3of3.Length != 39)
                        {
                            lblAlert.Text = "Passport first line Box-3 should be 39 characters length";
                            txtUKPassportL3.Focus();
                            bStatus = false;
                            return bStatus;
                        }

                        if (sUKPPLine1of7 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport second line Box-1";
                            txtUKPassport1.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sUKPPLine1of7.Length != 10)
                        {
                            lblAlert.Text = "Passport second line Box-3 should be 10 characters length";
                            txtUKPassport1.Focus();
                            bStatus = false;
                            return bStatus;
                        }

                        if (sUKPPLine2of7 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport second line Box-2";
                            txtUKPassport2.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sUKPPLine2of7.Length != 3)
                        {
                            lblAlert.Text = "Passport second line Box-3 should be 3 characters length";
                            txtUKPassport2.Focus();
                            bStatus = false;
                            return bStatus;
                        }

                        if (sUKPPLine3of7 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport second line Box-3";
                            txtUKPassport3.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sUKPPLine3of7.Length != 7)
                        {
                            lblAlert.Text = "Passport second line Box-3 should be 7 characters length";
                            txtUKPassport3.Focus();
                            bStatus = false;
                            return bStatus;
                        }

                        if (sUKPPLine4of7 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport second line Box-4";
                            txtUKPassport4.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sUKPPLine4of7.Length != 1)
                        {
                            lblAlert.Text = "Passport second line Box-4 should be 1 character length";
                            txtUKPassport4.Focus();
                            bStatus = false;
                            return bStatus;
                        }

                        if (sUKPPLine5of7 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport second line Box-5";
                            txtUKPassport5.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sUKPPLine5of7.Length != 7)
                        {
                            lblAlert.Text = "Passport second line Box-5 should be 7 character length";
                            txtUKPassport5.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sUKPPLine6of7 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport second line Box-6";
                            txtUKPassport6.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sUKPPLine6of7.Length != 14)
                        {
                            lblAlert.Text = "Passport second line Box-6 should be 14 character length";
                            txtUKPassport6.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sUKPPLine7of7 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport second line Box-7";
                            txtUKPassport7.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sUKPPLine7of7.Length != 2)
                        {
                            lblAlert.Text = "Passport second line Box-7 should be 2 character length";
                            txtUKPassport7.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                    }

                    #endregion

                    #region International Passport

                    if (sPPType.ToUpper() == "IT")
                    {
                        string sINTPPLine1of3 = txtIntPassportL1.Text.Trim();
                        string sINTPPLine2of3 = txtIntPassportL2.Text.Trim();
                        string sINTPPLine3of3 = txtIntPassportL3.Text.Trim();

                        string sINTPPLine1of9 = txtIntPassport1.Text.Trim();
                        string sINTPPLine2of9 = txtIntPassport2.Text.Trim();
                        string sINTPPLine3of9 = txtIntPassport3.Text.Trim();
                        string sINTPPLine4of9 = txtIntPassport4.Text.Trim();
                        string sINTPPLine5of9 = txtIntPassport5.Text.Trim();
                        string sINTPPLine6of9 = txtIntPassport6.Text.Trim();
                        string sINTPPLine7of9 = txtIntPassport7.Text.Trim();
                        string sINTPPLine8of9 = txtIntPassport8.Text.Trim();
                        string sINTPPLine9of9 = txtIntPassport9.Text.Trim();

                        if (sINTPPLine1of3 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport first line Box-1";
                            txtIntPassportL1.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sINTPPLine1of3.Length != 2)
                        {
                            lblAlert.Text = "Passport first line Box-1 should be 2 characters length";
                            txtIntPassportL1.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sINTPPLine2of3 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport first line Box-2";
                            txtIntPassportL2.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sINTPPLine2of3.Length != 3)
                        {
                            lblAlert.Text = "Passport first line Box-2 should be 3 characters length";
                            txtIntPassportL2.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sINTPPLine3of3 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport first line Box-3";
                            txtIntPassportL3.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sINTPPLine3of3.Length != 39)
                        {
                            lblAlert.Text = "Passport first line Box-3 should be 39 characters length";
                            txtIntPassportL3.Focus();
                            bStatus = false;
                            return bStatus;
                        }

                        if (sINTPPLine1of9 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport second line Box-1";
                            txtIntPassport1.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sINTPPLine1of9.Length != 9)
                        {
                            lblAlert.Text = "Passport second line Box-1 should be 9 characters length";
                            txtIntPassport1.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sINTPPLine2of9 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport second line Box-2";
                            txtIntPassport2.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sINTPPLine2of9.Length != 1)
                        {
                            lblAlert.Text = "Passport second line Box-2 should be 1 character length";
                            txtIntPassport2.Focus();
                            bStatus = false;
                            return bStatus;
                        }

                        if (sINTPPLine3of9 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport second line Box-3";
                            txtIntPassport3.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sINTPPLine3of9.Length != 3)
                        {
                            lblAlert.Text = "Passport second line Box-3 should be 3 characters length";
                            txtIntPassport3.Focus();
                            bStatus = false;
                            return bStatus;
                        }

                        if (sINTPPLine4of9 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport second line Box-4";
                            txtIntPassport4.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sINTPPLine4of9.Length != 7)
                        {
                            lblAlert.Text = "Passport second line Box-4 should be 7 characters length";
                            txtIntPassport4.Focus();
                            bStatus = false;
                            return bStatus;
                        }

                        if (sINTPPLine5of9 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport second line Box-5";
                            txtIntPassport5.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sINTPPLine5of9.Length != 1)
                        {
                            lblAlert.Text = "Passport second line Box-5 should be 1 character length";
                            txtIntPassport5.Focus();
                            bStatus = false;
                            return bStatus;
                        }

                        if (sINTPPLine6of9 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport second line Box-6";
                            txtIntPassport6.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sINTPPLine6of9.Length != 7)
                        {
                            lblAlert.Text = "Passport second line Box-6 should be 7 characters length";
                            txtIntPassport6.Focus();
                            bStatus = false;
                            return bStatus;
                        }

                        if (sINTPPLine7of9 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport second line Box-7";
                            txtIntPassport7.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sINTPPLine7of9.Length != 14)
                        {
                            lblAlert.Text = "Passport second line Box-7 should be 14 characters length";
                            txtIntPassport7.Focus();
                            bStatus = false;
                            return bStatus;
                        }

                        if (sINTPPLine8of9 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport second line Box-8";
                            txtIntPassport8.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sINTPPLine8of9.Length != 1)
                        {
                            lblAlert.Text = "Passport second line Box-8 should be 1 character length";
                            txtIntPassport8.Focus();
                            bStatus = false;
                            return bStatus;
                        }

                        if (sINTPPLine9of9 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the passport second line Box-9";
                            txtIntPassport9.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sINTPPLine9of9.Length != 1)
                        {
                            lblAlert.Text = "Passport second line Box-9 should be 1 character length";
                            txtIntPassport9.Focus();
                            bStatus = false;
                            return bStatus;
                        }

                    }

                    #endregion

                    if (sPPCOI == string.Empty || sPPCOI == "-1")
                    {
                        lblAlert.Text = "Please select the passport issuing country";
                        ddlPsPrtIsCntry.Focus();
                        bStatus = false;
                        return bStatus;
                    }

                    if (IsDate(sPPDOI))
                    {
                        DateTime dt;
                        dt = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));

                        if (DateTime.Parse(sPPDOI) > dt)
                        {
                            lblDoissueerr.Text = "Passport Issue Date should be less than or Equal to current date!";
                            calDOI.CssClass = "error";
                            calDOI.Focus();
                            return false;
                        }
                    }
                    else
                    {
                        lblDoissueerr.Text = "Invalid passport issued date";
                        calDOI.CssClass = "error";
                        calDOI.Focus();
                        return bStatus;
                    }

                    if (IsDate(sPPDOE))
                    {
                        DateTime dt;
                        dt = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
                        if (DateTime.Parse(sPPDOE) < dt)
                        {
                            lblDoExerr.Text = "Passport Expiry date should be greater than current date!";
                            calDOE.CssClass = "error";
                            calDOE.Focus();
                            return false;
                        }
                    }
                    else
                    {
                        lblDoExerr.Text = "Invalid passport expiry date";
                        calDOE.CssClass = "error";
                        calDOE.Focus();
                        return bStatus;
                    }

                }

                #endregion
            }
            #endregion

            #region Occupation Details

            if (sEmpType == string.Empty || sEmpType == "-1")
            {
                lblAlert.Text = "Please select the Occupation details";
                ddlEmpType.Focus();
                bStatus = false;
                return bStatus;
            }
            if (sEmpType.ToUpper() == "Others".ToUpper() && sEmpOth == string.Empty)
            {
                lblAlert.Text = "Please enter the occupation details";
                txtEmptypOth.Focus();
                bStatus = false;
                return bStatus;
            }

            #endregion

            AccountDtl AcDtlc = null;
            AcDtlc = (AccountDtl)Session["AcDtl"];
            List<Applicant> lstUsr1 = null;
            lstUsr1 = GetUserList();

            if (lstUsr1 != null && lstUsr1.Count > 1 && rblJntApAddSts.SelectedValue.ToUpper() == "YES")
            {

            }
            else
            {
                # region Current Address
                if (sCurPcode == string.Empty)
                {
                    lblAlert.Text = "Please enter the current address postcode";
                    txtCurPostCode.Focus();
                    bStatus = false;
                    return bStatus;
                }
                //if (!Regex.IsMatch(sCurPcode, @"^[a-zA-Z0-9\s]{6,8}$"))
                //{
                //    lblAlert.Text = "The postcode you provided does not conform to a valid UK postcode format.";
                //    txtCurPostCode.Focus();
                //    bStatus = false;
                //    return bStatus;
                //}
                if (sCurDoorNo == string.Empty && sCurAddr1 == string.Empty)
                {
                    //lblAlert.Text = "Please enter the current address house / flat no";  commented on 2023 enhancements
                    lblAlert.Text = "Both the 'House/Flat No' and 'Building Name' fields cannot be left blank. Please fill in at least one of these fields";
                    txtCurDrNo.Focus();
                    bStatus = false;
                    return bStatus;
                }

                if (sCurAddr2 == string.Empty)
                {
                    lblAlert.Text = "Please enter the current address street";
                    txtCurAddr2.Focus();
                    bStatus = false;
                    return bStatus;
                }

                if (sCurAddr3 == string.Empty || sCurAddr3 == "-1")
                {
                    lblAlert.Text = "Please select the current address city";
                    //txtCurAddr3.Focus();
                    ddlCurCity.Focus();
                    bStatus = false;
                    return bStatus;
                }
                if (sCurCountry == string.Empty || sCurCountry == "-1")
                {
                    lblAlert.Text = "Please select the current address country";
                    ddlCurCountry.Focus();
                    bStatus = false;
                    return bStatus;
                }

                if (txtRsdnSnc.Text == string.Empty)
                {
                    lblAlert.Text = "Please Select Current Address Date of Residence"; //string sResidingSince = txtRsdnSnc.Text.Trim();
                    txtRsdnSnc.Focus();
                    return false;
                }

                if (IsDate(txtRsdnSnc.Text.ToString()) == true)
                {
                    if (DateTime.Parse(txtRsdnSnc.Text.ToString()) > DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy")))
                    {
                        lblAlert.Text = "Date of Residence should be less than or Equal to current date!";
                        txtRsdnSnc.Focus();
                        return false;
                    }
                }
                # endregion

                # region Previous Address
                DateTime newDt;
                if (DateTime.TryParse(txtRsdnSnc.Text.Trim(), out newDt))
                {
                    double dDiff = (Convert.ToDateTime(newDt, provider) - Convert.ToDateTime(DateTime.Today.AddYears(-1), provider)).TotalDays;
                    if (dDiff > 0)
                    {
                        if (sPrePcode == string.Empty)
                        {
                            lblAlert.Text = "Please enter the previous address postcode";
                            txtCurPostCode.Focus();
                            bStatus = false;
                            return bStatus;
                        }

                        if (sPreDoorNo == string.Empty && sPreAddr2 == string.Empty)
                        {
                            lblAlert.Text = "Please enter the previous address house / flat no";
                            txtPreDrNo.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sPreAddr3 == string.Empty || sPreAddr3 == "-1")
                        {
                            lblAlert.Text = "Please select the previous address city";
                            ddlPreCity.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                        if (sPreCountry == string.Empty || sPreCountry == "-1")
                        {
                            lblAlert.Text = "Please select the previous address country";
                            ddlPreCountry.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                    }
                }

                #endregion
            }

            //20171110-chellappa
            # region FATCA details
            if (sPayTax == string.Empty)
            {
                lblAlert.Text = "Please Select Pay Tax";
                rblPayTax.Focus();
                bStatus = false;
                return bStatus;
            }
            if (sUSCitizen == string.Empty)
            {
                lblAlert.Text = "Please Select US citizen";
                rblUSCitizen.Focus();
                bStatus = false;
                return bStatus;
            }
            if (sGreenCard == string.Empty)
            {
                lblAlert.Text = "Please Select Green Card";
                rblGreenCard.Focus();
                bStatus = false;
                return bStatus;
            }
            if (sRealEst == string.Empty)
            {
                lblAlert.Text = "Please Select Real Estate in USA";
                rblRealEst.Focus();
                bStatus = false;
                return bStatus;
            }
            if (sAsset == string.Empty)
            {
                lblAlert.Text = "Please Select Assets held in USA";
                rblassets.Focus();
                bStatus = false;
                return bStatus;
            }
            #endregion

            # region Tax Liabilities  details

            ////if (sIsUsPerson == string.Empty)
            ////{
            ////    lblAlert.Text = "Please select the Tax information";
            ////    rblUSPerson.Focus();
            ////    bStatus = false;
            ////    return bStatus;
            ////}             
            ////if (sIsUsPerson.ToUpper() == "Yes".ToUpper())
            ////{
            ////    if (sPriJrsdctn == "-1" && sPriTIN == string.Empty && sRFNPTin == string.Empty)
            ////    {
            ////        lblAlert.Text = "Please enter the reasons for not being able to provide TIN";
            ////        txtReasonTin.Focus();
            ////        bStatus = false;
            ////        return bStatus;
            ////    }
            ////    if (sPriJrsdctn != "-1" && sPriTIN == string.Empty)
            ////    {
            ////        lblAlert.Text = "Please enter primary TIN";
            ////        txtTin1.Focus();
            ////        bStatus = false;
            ////        return bStatus;
            ////    }
            ////    if (sPriJrsdctn == "-1" && sPriTIN != string.Empty)
            ////    {
            ////        lblAlert.Text = "Please Select the primary Jurisdiction";
            ////        ddlPriJuri.Focus();
            ////        bStatus = false;
            ////        return bStatus;
            ////    }

            ////    if (sAdJrsdctn1 != "-1" && sAdTIN1 == string.Empty)
            ////    {
            ////        lblAlert.Text = "Please enter additional TIN-1";
            ////        txtTin2.Focus();
            ////        bStatus = false;
            ////        return bStatus;
            ////    }
            ////    if (sAdJrsdctn1 == "-1" && sAdTIN1 != string.Empty)
            ////    {
            ////        lblAlert.Text = "Please Select additional Jurisdiction-1";
            ////        ddlAddJuri.Focus();
            ////        bStatus = false;
            ////        return bStatus;
            ////    }
            ////    if (sAdJrsdctn2 != "-1" && sAdTIN2 == string.Empty)
            ////    {
            ////        lblAlert.Text = "Please enter additional TIN-2";
            ////        txtTin3.Focus();
            ////        bStatus = false;
            ////        return bStatus;
            ////    }
            ////    if (sAdJrsdctn2 == "-1" && sAdTIN2 != string.Empty)
            ////    {
            ////        lblAlert.Text = "Please Select additional Jurisdiction-2";
            ////        ddlAddJuri1.Focus();
            ////        bStatus = false;
            ////        return bStatus;
            ////    }
            ////}

            if (sIsUsPerson == string.Empty)
            {
                lblAlert.Text = "Please select the Tax information";
                rblUSPerson.Focus();
                bStatus = false;
                return bStatus;
            }
            if (sIsUsPerson.ToUpper() == "Yes".ToUpper())
            {
                if (sPriJrsdctn != "-1" && sPriTIN == string.Empty)
                {
                    lblAlert.Text = "Please enter primary TIN";
                    txtTin1.Focus();
                    bStatus = false;
                    return bStatus;
                }
                if (sAdJrsdctn1 == "-1" && sAdTIN1 != string.Empty)
                {
                    lblAlert.Text = "Please Select additional Jurisdiction-1";
                    ddlAddJuri.Focus();
                    bStatus = false;
                    return bStatus;
                }
                if (sAdJrsdctn2 == "-1" && sAdTIN2 != string.Empty)
                {
                    lblAlert.Text = "Please Select additional Jurisdiction-2";
                    ddlAddJuri1.Focus();
                    bStatus = false;
                    return bStatus;
                }

                if ((sPriJrsdctn != "-1" && sPriTIN != string.Empty) && ((sAdJrsdctn1 == "-1") && (sAdTIN1 == string.Empty || sRFNPTin == string.Empty)))
                {
                    lblAlert.Text = "Please Select additional Jurisdiction-1";
                    ddlAddJuri.Focus();
                    bStatus = false;
                    return bStatus;
                }

                if (sPriJrsdctn != "-1" && sPriTIN != string.Empty)
                {
                    if (sRFNPTin == string.Empty)
                    {
                        if ((sAdJrsdctn1 != "-1") && (sAdTIN1 == string.Empty))
                        {
                            lblAlert.Text = "Please enter additional TIN-1 / enter reasons for not being able to provide TIN for additional Jurisdiction";
                            txtTin2.Focus();
                            bStatus = false;
                            return bStatus;
                        }
                    }
                }
                if ((sAdJrsdctn2 != "-1") && (sAdTIN2 == string.Empty && sRFNPTin == string.Empty))
                {
                    lblAlert.Text = "Please enter additional TIN-2 / enter reasons for not being able to provide TIN for additional Jurisdiction";
                    txtTin3.Focus();
                    bStatus = false;
                    return bStatus;
                }

                ////if (sPriJrsdctn == "-1" && sPriTIN == string.Empty && sAdJrsdctn1 == "-1" && sAdTIN1 == string.Empty && sRFNPTin == string.Empty)
                ////{
                ////    lblAlert.Text = "Please enter the reasons for not being able to provide TIN";
                ////    txtReasonTin.Focus();
                ////    bStatus = false;
                ////    return bStatus;
                ////}

                ////if (sPriJrsdctn == "-1" && sPriTIN != string.Empty)
                ////{
                ////    lblAlert.Text = "Please Select the primary Jurisdiction";
                ////    ddlPriJuri.Focus();
                ////    bStatus = false;
                ////    return bStatus;
                ////}

                ////if (sAdJrsdctn1 != "-1" && sAdTIN1 != string.Empty)
                ////{
                ////    if (sRFNPTin == string.Empty)
                ////    {
                ////        if (sPriJrsdctn == "-1" && sPriTIN == string.Empty)
                ////        {
                ////            lblAlert.Text = "Please enter primary Jurisdiction & TIN";
                ////            ddlPriJuri.Focus();
                ////            bStatus = false;
                ////            return bStatus;
                ////        }
                ////    }
                ////}

                //if (sPriJrsdctn != "-1" && sPriTIN != string.Empty)
                //{
                //    if (sRFNPTin == string.Empty)
                //    {
                //        if (sAdJrsdctn1 == "-1" && sAdTIN1 == string.Empty)
                //        {
                //            lblAlert.Text = "Please enter additional Jurisdiction-1 & TIN-1";
                //            ddlAddJuri.Focus();
                //            bStatus = false;
                //            return bStatus;
                //        }
                //    }
                //}



                //if (sAdJrsdctn1 != "-1" && sAdTIN1 == string.Empty)
                //{
                //    lblAlert.Text = "Please enter additional TIN-1";
                //    txtTin2.Focus();
                //    bStatus = false;
                //    return bStatus;
                //}                



            }

            #endregion

            #region SOF Details
            if ((SOF == string.Empty) || (SOF == "-1"))
            {
                lblAlert.Text = "Please select Source of Fund";
                ddlSOF.Focus();
                bStatus = false;
                return bStatus;
            }
            if (SOF.ToUpper() == "Others".ToUpper() && SOFOth == string.Empty)
            {
                lblAlert.Text = "Please enter the SOF details";
                txtSOFOth.Focus();
                bStatus = false;
                return bStatus;
            }
            #endregion


            if (IsDate(txtRsdnSnc.Text.ToString()) == true)
            {
                if (DateTime.Parse(txtRsdnSnc.Text.ToString()) > DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy")))
                {
                    lblAlert.Text = "Date of Residence should be less than or Equal to current date!";
                    txtRsdnSnc.Focus();
                    return false;
                }
            }
            else
            {
                AccountDtl AcDtl = null;
                AcDtl = (AccountDtl)Session["AcDtl"];
                List<Applicant> lstUsr = null;
                lstUsr = GetUserList();
                if (AcDtl.IsJntAc.ToUpper() == "Yes".ToUpper())
                {
                    if ((lstUsr != null) && (lstUsr.Count > 0) && (rblJntApAddSts.SelectedValue.ToUpper() == "Yes".ToUpper()))
                    {

                    }
                    else
                    {
                        lblAlert.Text = "Invalid Date of Residence !";
                        txtRsdnSnc.Focus();
                        return false;
                    }
                }
                else
                {
                    lblAlert.Text = "Invalid Date of Residence !";
                    txtRsdnSnc.Focus();
                    return false;
                }
            }

            bStatus = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return bStatus;
    }

    private bool CheckRequiredData()
    {
        bool bStatus = false;

        try
        {
            string sTitle = ddlTittle.SelectedValue;
            string sFirstNm = txtFirstNm.Text.Trim();
            string sSurNm = txtSurNm.Text.Trim();
            string sCitizenship = ddlCtznShp.SelectedValue;
            //string sMaritalSts = ddlMaritalSts.SelectedValue;   //commented on 2023
            string sMobileNo = txtMobileNo.Text.Trim();
            string sEmailAddr = txtEmailAddr.Text.Trim();
            string sIdentityNo = "asdf";

            string sddlEmpType = ddlEmpType.SelectedValue;

            string sCurPcode = txtCurPostCode.Text.Trim();
            string sCurDoorNo = txtCurDrNo.Text.Trim();
            string sCurAddr1 = txtCurAddr1.Text.Trim();
            string sCurAddr2 = txtCurAddr2.Text.Trim();
            //string sCurAddr3 = txtCurAddr3.Text.Trim();
            string sCurAddr3 = ddlCurCity.SelectedValue.Trim();
            string sCurCountry = ddlCurCountry.SelectedValue;
            string sResidingSince = txtRsdnSnc.Text.Trim();

            string sUsePrimaryAddr = rblJntApAddSts.SelectedValue;

            if ((sTitle.Length == 0) || (sTitle == null) || (sTitle == string.Empty))
            {
                lblAlert.Text = "Please Select the Title";
                bStatus = false;
            }
            else if ((sFirstNm.Length == 0) || (sFirstNm == null) || (sFirstNm == string.Empty))
            {
                lblAlert.Text = "First name cannot be blank";
                bStatus = false;
            }
            else if ((sSurNm.Length == 0) || (sSurNm == null) || (sSurNm == string.Empty))
            {
                lblAlert.Text = " Surname cannot be blank";
                bStatus = false;
            }


            //chellappa
            else if (rblGender.SelectedIndex == -1)
            {
                lblAlert.Text = "Please Select Gender";
                bStatus = false;
            }
            else if (rblIdentity.SelectedIndex == -1)
            {
                lblAlert.Text = "Please Select Identity Details";
                bStatus = false;
            }
            else if (rbldltype.SelectedIndex == -1 && rblIdentity.SelectedValue == "DRIVING LICENCE")
            {
                lblAlert.Text = "Please Select Driving Licence Type";
                bStatus = false;
            }
            else if (rblPassport.SelectedIndex == -1 && rblIdentity.SelectedValue == "PASSPORT")
            {
                lblAlert.Text = "Please Select Passport";
                bStatus = false;
            }
            else if (rblUSPerson.SelectedIndex == -1)
            {
                lblAlert.Text = "Please Select Are You a US Person";
                bStatus = false;
            }


            else if ((sCitizenship.Length == 0) || (sCitizenship == null) || (sCitizenship == string.Empty))
            {
                lblAlert.Text = "Please Select the Citizenship";
                bStatus = false;
            }
            //commented on 2023
            //else if ((sMaritalSts.Length == 0) || (sMaritalSts == null) || (sMaritalSts == string.Empty))
            //{
            //    lblAlert.Text = "Please Select the Marital Status";
            //    bStatus = false;
            //}
            else if ((sddlEmpType.Length == 0) || (sddlEmpType == null) || (sddlEmpType == string.Empty))
            {
                lblAlert.Text = "Please Select the Employment detail";
                bStatus = false;
            }
            else if ((sIdentityNo.Length == 0) || (sIdentityNo == null) || (sIdentityNo == string.Empty))
            {
                lblAlert.Text = " Passport / Driving Licence Number cannot be blank";
                bStatus = false;
            }
            else if (Convert.ToString(ViewState["Sequence"]) == "1")
            {
                if ((sCurPcode.Length == 0) || (sCurPcode == null) || (sCurPcode == string.Empty))
                {
                    lblAlert.Text = " Current Address Postal code cannot be blank";
                    bStatus = false;
                }

                else if (sCurDoorNo.Length == 0)
                {
                    lblAlert.Text = "Current Address Line - 1 cannot be blank";
                    bStatus = false;
                }

                else if ((sCurAddr3.Length == 0) || (sCurAddr3 == null) || (sCurAddr3 == string.Empty) || (sCurAddr3 == "-1"))
                {
                    lblAlert.Text = "Current Address city cannot be blank";
                    bStatus = false;
                }

                else if ((sCurCountry.Length == 0) || (sCurCountry == null) || (sCurCountry == string.Empty))
                {
                    lblAlert.Text = "Please Select the Current Address Country";
                    bStatus = false;
                }

                else if ((sResidingSince.Length == 0) || (sResidingSince == null) || (sResidingSince == string.Empty))
                {
                    lblAlert.Text = " Residing Since date cannot be blank";
                    bStatus = false;
                }
                else if (1 == 1)
                {
                    bStatus = true;
                }
            }
            else if (Convert.ToInt32(ViewState["Sequence"]) > 1)
            {
                if (sUsePrimaryAddr == "No")
                {
                    if ((sCurPcode.Length == 0) || (sCurPcode == null) || (sCurPcode == string.Empty))
                    {
                        lblAlert.Text = " Current Address Postal code cannot be blank";
                        bStatus = false;
                    }

                    else if (sCurDoorNo.Length == 0)
                    {
                        lblAlert.Text = " Current Address Line - 1 cannot be blank";
                        bStatus = false;
                    }

                    else if ((sCurAddr3.Length == 0) || (sCurAddr3 == null) || (sCurAddr3 == string.Empty) || (sCurAddr3 == "-1"))
                    {
                        lblAlert.Text = " Current Address city cannot be blank";
                        bStatus = false;
                    }

                    else if ((sCurCountry.Length == 0) || (sCurCountry == null) || (sCurCountry == string.Empty))
                    {
                        lblAlert.Text = "Please Select the Current Address Country";
                        bStatus = false;
                    }

                    else if ((sResidingSince.Length == 0) || (sResidingSince == null) || (sResidingSince == string.Empty))
                    {
                        lblAlert.Text = " Residing Since date cannot be blank";
                        bStatus = false;
                    }
                    else if (1 == 1)
                    {
                        bStatus = true;
                    }
                }
                else
                {
                    bStatus = true;
                }
            }
            else if (1 == 1)
            {
                bStatus = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return bStatus;
    }

    private void AddApplicant()
    {
        oAcDtl = (AccountDtl)Session["AcDtl"];
        List<Applicant> lstUsr = null;

        try
        {
            lstUsr = GetUserList();

            if ((lstUsr != null) && (lstUsr.Count > 0))
            {
                SaveApplicantDtls();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void AddressVisibility()
    {
        AccountDtl AcDtl = null;
        AcDtl = (AccountDtl)Session["AcDtl"];

        List<Applicant> lstUsr = null;

        try
        {
            lstUsr = GetUserList();

            if ((lstUsr != null) && (lstUsr.Count > 0))
            {
                if (AcDtl.IsJntAc.ToUpper() == "Yes".ToUpper())
                {
                    pnlJntAplntAddr.Visible = true;
                    btnSave.Visible = true;
                    btnCancel.Visible = true;
                }

                if ((lstUsr != null) && (lstUsr.Count > 3))
                {
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                }

            }
            else
            {
                pnlJntAplntAddr.Visible = false;
                btnSave.Visible = false;
                btnCancel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SaveApplicantDtls()
    {
        List<Applicant> lstUsr = null;
        int seq = 0;
        try
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "show_progress();", true);


            //if (ChkVal() == false)
            //{
            //    return;
            //}

            //if (CheckRequiredData() == false)
            //{
            //    return;
            //}

            if (PrimaryValidation())
            {
                if (Page.IsValid == true)
                {
                    SaveValues();
                    clearControls();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private void LoadCountry()
    {
        try
        {
            ddlCtznShp.Items.Clear();
            ddlCurCountry.Items.Clear();
            ddlPreCountry.Items.Clear();
            //chella(20171005)
            ddlPriJuri.Items.Clear();
            ddlAddJuri.Items.Clear();
            ddlAddJuri1.Items.Clear();

            DataTable dt;
            dt = new DataTable();
            dt = obj.ExecuteData("Select UPPER(COUNTRYCD) AS COUNTRYCD, UPPER(COUNTRY) AS COUNTRY from COUNTRYPF order by PRIORITYID  DESC, COUNTRY ASC");

            ddlCtznShp.DataValueField = "COUNTRY";
            ddlCtznShp.DataTextField = "COUNTRY";
            ddlCtznShp.DataSource = dt;
            ddlCtznShp.DataBind();

            ddlCtznShp.Items.Insert(0, new ListItem("Select Country", "-1"));

            ddlCurCountry.DataValueField = "COUNTRY";
            ddlCurCountry.DataTextField = "COUNTRY";
            ddlCurCountry.DataSource = dt;
            ddlCurCountry.DataBind();

            ddlCurCountry.Items.Insert(0, new ListItem("Select Country", "-1"));

            ddlPreCountry.DataValueField = "COUNTRY";
            ddlPreCountry.DataTextField = "COUNTRY";
            ddlPreCountry.DataSource = dt;
            ddlPreCountry.DataBind();
            ddlPreCountry.Items.Insert(0, new ListItem("Select Country", "-1"));

            ddlPsPrtIsCntry.DataValueField = "COUNTRY";
            ddlPsPrtIsCntry.DataTextField = "COUNTRY";
            ddlPsPrtIsCntry.DataSource = dt;
            ddlPsPrtIsCntry.DataBind();
            ddlPsPrtIsCntry.Items.Insert(0, new ListItem("Select Country", "-1"));

            ddlPriJuri.DataValueField = "COUNTRY";
            ddlPriJuri.DataTextField = "COUNTRY";
            ddlPriJuri.DataSource = dt;
            ddlPriJuri.DataBind();
            ddlPriJuri.Items.Insert(0, new ListItem("Select Country", "-1"));

            ddlAddJuri.DataValueField = "COUNTRY";
            ddlAddJuri.DataTextField = "COUNTRY";
            ddlAddJuri.DataSource = dt;
            ddlAddJuri.DataBind();
            ddlAddJuri.Items.Insert(0, new ListItem("Select Country", "-1"));

            ddlAddJuri1.DataValueField = "COUNTRY";
            ddlAddJuri1.DataTextField = "COUNTRY";
            ddlAddJuri1.DataSource = dt;
            ddlAddJuri1.DataBind();
            ddlAddJuri1.Items.Insert(0, new ListItem("Select Country", "-1"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void LoadCity()
    {
        try
        {
            ddlCurCity.Items.Clear();
            ddlPreCity.Items.Clear();

            DataTable dt;
            dt = new DataTable();
            dt = obj.ExecuteData("select CITYNAME from CITYMST ORDER BY CITYNAME");

            ddlCurCity.DataValueField = "CITYNAME";
            ddlCurCity.DataTextField = "CITYNAME";
            ddlCurCity.DataSource = dt;
            ddlCurCity.DataBind();

            ddlPreCity.DataValueField = "CITYNAME";
            ddlPreCity.DataTextField = "CITYNAME";
            ddlPreCity.DataSource = dt;
            ddlPreCity.DataBind();

            ddlCurCity.Items.Insert(0, new ListItem("Select City", "-1"));
            ddlPreCity.Items.Insert(0, new ListItem("Select City", "-1"));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void LoadTittle()
    {
        try
        {
            ddlTittle.Items.Clear();

            DataTable dt;
            dt = new DataTable();
            dt = obj.ExecuteData("SELECT UPPER(TCODE) AS TCODE,UPPER(TDESC) AS TDESC FROM TITLEPF WHERE [STATUS]='Y' ORDER BY TID ASC");

            ddlTittle.DataValueField = "TCODE";
            ddlTittle.DataTextField = "TDESC";
            ddlTittle.DataSource = dt;
            ddlTittle.DataBind();
            ddlTittle.Items.Insert(0, new ListItem("Select Title", "-1"));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //commented on 2023
    //private void LoadMrtlSts()
    //{
    //    try
    //    {
    //        ddlMaritalSts.Items.Clear();

    //        DataTable dt;
    //        dt = new DataTable();
    //        dt = obj.ExecuteData("SELECT UPPER(MCODE) AS MCODE FROM MARITALSTSPF ORDER BY MID ASC");

    //        ddlMaritalSts.DataValueField = "MCODE";
    //        ddlMaritalSts.DataTextField = "MCODE";
    //        ddlMaritalSts.DataSource = dt;
    //        ddlMaritalSts.DataBind();
    //        ddlMaritalSts.Items.Insert(0, new ListItem("Select Status", "-1"));

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

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
            throw ex;
        }
    }

    #region Modified by senthil based on the Aug 2015 change request

    //protected void rblIdentity_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (rblIdentity.SelectedValue.ToUpper() == "Passport".ToUpper())
    //        {
    //            //if (rblPassport.SelectedValue.ToUpper() == "UK" || rblPassport.SelectedValue.ToUpper() == "INT")  
    //            //{
    //                pnlDIdentityNo.Visible = false;
    //                pnlPassport.Visible = true; pnlCntry.Visible = true;
    //                //txtDRLNo1.Text = string.Empty;
    //                //txtDRLNo2.Text = string.Empty;
    //                //txtDRLNo3.Text = string.Empty;
    //                calDOI.SelDate = DateTime.Now.ToString("dd/MM/yyyy");
    //                calDOE.SelDate = DateTime.Now.ToString("dd/MM/yyyy");
    //                rblPassport.SelectedIndex = 0;
    //                pnlUKPassport.Visible = true;
    //                pnlIntPassport.Visible = false;
    //                ddlPsPrtIsCntry.SelectedIndex = 1;
    //                ddlPsPrtIsCntry.Enabled = false;
    //                ClearText("PASSPORT");
    //                rbldltype.SelectedIndex = 0;
    //            //}
    //        }
    //        else
    //        {
    //            pnlUKPassport.Visible = false;
    //            pnlIntPassport.Visible = false;
    //            pnlDIdentityNo.Visible = true;
    //            pnlPassport.Visible = false; pnlCntry.Visible = false;
    //            //txtDRLNo1.Text = string.Empty;
    //            //txtDRLNo2.Text = string.Empty;
    //            //txtDRLNo3.Text = string.Empty;
    //            calDOI.SelDate = DateTime.Now.ToString("dd/MM/yyyy");
    //            calDOE.SelDate = DateTime.Now.ToString("dd/MM/yyyy");
    //            ddlPsPrtIsCntry.SelectedIndex = 0;
    //            ClearText("DRLNO");
    //            rblPassport.SelectedIndex = 0;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
    //        throw ex;
    //    }
    //}

    protected void rblIdentity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblIdentity.SelectedValue.ToUpper() == "Passport".ToUpper())
            {
                pnlPassport.Visible = true;
                pnlDIdentityNo.Visible = false;
                rbldltype.SelectedIndex = -1;
                //ClearText("PASSPORT");

                obj.StoreEvent("66313", Convert.ToString(Session["RefNo"]), rblIdentity.SelectedValue.ToUpper(), "", "", Convert.ToString(Session["RefNo"]));
            }
            else
            {
                pnlDIdentityNo.Visible = true;

                pnlPassport.Visible = false;
                pnlUKPassport.Visible = false;
                pnlIntPassport.Visible = false;
                pnlCntry.Visible = false;
                rblPassport.SelectedIndex = -1;

                //ClearText("DRLNO");

                obj.StoreEvent("66313", Convert.ToString(Session["RefNo"]), rblIdentity.SelectedValue.ToUpper(), "", "", Convert.ToString(Session["RefNo"]));
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }

    }

    protected void rblIdentity2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblIdentity.SelectedValue.ToUpper() == "Passport".ToUpper())
            {
                pnlPassport.Visible = true;
                pnlDIdentityNo.Visible = false;

                //ClearText("PASSPORT");
            }
            else
            {
                pnlDIdentityNo.Visible = true;

                pnlPassport.Visible = false;
                pnlUKPassport.Visible = false;
                pnlIntPassport.Visible = false;
                pnlCntry.Visible = false;

                //ClearText("DRLNO");
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }

    }

    private void ClearText(string PassportDrLNo)
    {
        try
        {
            switch (PassportDrLNo.Trim().ToUpper())
            {
                case "PASSPORT":
                    {
                        txtUKPassportL1.Text = string.Empty;
                        txtUKPassportL2.Text = string.Empty;
                        txtUKPassportL3.Text = string.Empty;

                        txtUKPassport1.Text = string.Empty;
                        txtUKPassport2.Text = string.Empty;
                        txtUKPassport3.Text = string.Empty;
                        txtUKPassport4.Text = string.Empty;
                        txtUKPassport5.Text = string.Empty;
                        txtUKPassport6.Text = string.Empty;
                        txtUKPassport7.Text = string.Empty;

                        txtIntPassportL1.Text = string.Empty;
                        txtIntPassportL2.Text = string.Empty;
                        txtIntPassportL3.Text = string.Empty;

                        txtIntPassport1.Text = string.Empty;
                        txtIntPassport2.Text = string.Empty;
                        txtIntPassport3.Text = string.Empty;
                        txtIntPassport4.Text = string.Empty;
                        txtIntPassport5.Text = string.Empty;
                        txtIntPassport6.Text = string.Empty;
                        txtIntPassport7.Text = string.Empty;
                        txtIntPassport8.Text = string.Empty;
                        txtIntPassport9.Text = string.Empty;

                        break;
                    }
                case "DRLNO":
                    {
                        txtDRLNo1.Text = string.Empty;
                        txtDRLNo2.Text = string.Empty;
                        txtDRLNo3.Text = string.Empty;
                        break;
                    }
                case "UK":
                    {
                        txtUKPassportL1.Text = string.Empty;
                        txtUKPassportL2.Text = string.Empty;
                        txtUKPassportL3.Text = string.Empty;

                        txtUKPassport1.Text = string.Empty;
                        txtUKPassport2.Text = string.Empty;
                        txtUKPassport3.Text = string.Empty;
                        txtUKPassport4.Text = string.Empty;
                        txtUKPassport5.Text = string.Empty;
                        txtUKPassport6.Text = string.Empty;
                        txtUKPassport7.Text = string.Empty;
                        break;
                    }
                case "NONUK":
                    {
                        txtIntPassportL1.Text = string.Empty;
                        txtIntPassportL2.Text = string.Empty;
                        txtIntPassportL3.Text = string.Empty;

                        txtIntPassport1.Text = string.Empty;
                        txtIntPassport2.Text = string.Empty;
                        txtIntPassport3.Text = string.Empty;
                        txtIntPassport4.Text = string.Empty;
                        txtIntPassport5.Text = string.Empty;
                        txtIntPassport6.Text = string.Empty;
                        txtIntPassport7.Text = string.Empty;
                        txtIntPassport8.Text = string.Empty;
                        txtIntPassport9.Text = string.Empty;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void rblPassport_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblPassport.SelectedValue.ToUpper() == "UK".ToUpper())
            {
                pnlUKPassport.Visible = true;
                pnlIntPassport.Visible = false;
                ddlPsPrtIsCntry.Items.Remove(new ListItem("UNITED KINGDOM", "UNITED KINGDOM"));
                ddlPsPrtIsCntry.Items.Insert(1, new ListItem("UNITED KINGDOM", "UNITED KINGDOM"));
                ddlPsPrtIsCntry.SelectedIndex = 1;
                ddlPsPrtIsCntry.Enabled = false;

                pnlCntry.Visible = true;
                // calDOI.SelDate = DateTime.Now.ToString("dd/MM/yyyy");
                // calDOE.SelDate = DateTime.Now.ToString("dd/MM/yyyy");

                //ClearText("UK");

                obj.StoreEvent("66313", Convert.ToString(Session["RefNo"]), rblPassport.SelectedValue.ToUpper(), "", "", Convert.ToString(Session["RefNo"]));

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "onLoad", "PasspoValid();", true);
            }
            else
            {
                pnlUKPassport.Visible = false;
                pnlIntPassport.Visible = true;
                ddlPsPrtIsCntry.SelectedIndex = 0;
                ddlPsPrtIsCntry.Enabled = true;

                pnlCntry.Visible = true;
                ddlPsPrtIsCntry.Items.Remove(new ListItem("UNITED KINGDOM", "UNITED KINGDOM"));
                // calDOI.SelDate = DateTime.Now.ToString("dd/MM/yyyy");
                // calDOE.SelDate = DateTime.Now.ToString("dd/MM/yyyy");

                //ClearText("NONUK");
                obj.StoreEvent("66313", Convert.ToString(Session["RefNo"]), rblPassport.SelectedValue.ToUpper(), "", "", Convert.ToString(Session["RefNo"]));

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "onLoad", "PasspoValid();", true);
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    //protected void rblPassport_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (rblPassport.SelectedValue.ToUpper() == "UK".ToUpper())
    //        {
    //            pnlUKPassport.Visible = true;
    //            pnlIntPassport.Visible = false;
    //            ddlPsPrtIsCntry.SelectedIndex = 1;
    //            ddlPsPrtIsCntry.Enabled = false;
    //            ClearText("UK");
    //        }
    //        else
    //        {
    //            pnlUKPassport.Visible = false;
    //            pnlIntPassport.Visible = true;
    //            ddlPsPrtIsCntry.SelectedIndex = 0;
    //            ddlPsPrtIsCntry.Enabled = true;
    //            ClearText("NONUK");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
    //        throw ex;
    //    }
    //}

    protected void rblPassport2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblPassport.SelectedValue.ToUpper() == "UK".ToUpper())
            {
                pnlUKPassport.Visible = true;
                pnlIntPassport.Visible = false;
                ddlPsPrtIsCntry.SelectedIndex = 1;
                ddlPsPrtIsCntry.Enabled = false;

                pnlCntry.Visible = true;
                calDOI.SelDate = DateTime.Now.ToString("dd/MM/yyyy");
                calDOE.SelDate = DateTime.Now.ToString("dd/MM/yyyy");

                ClearText("UK");
            }
            else
            {
                pnlUKPassport.Visible = false;
                pnlIntPassport.Visible = true;
                ddlPsPrtIsCntry.SelectedIndex = 0;
                ddlPsPrtIsCntry.Enabled = true;

                pnlCntry.Visible = true;
                calDOI.SelDate = DateTime.Now.ToString("dd/MM/yyyy");
                calDOE.SelDate = DateTime.Now.ToString("dd/MM/yyyy");

                ClearText("NONUK");
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    #endregion


    //Current address
    protected void btnCurSearch_Click(object sender, EventArgs e)
    {
        try
        {
            obj.StoreEvent("66307", Convert.ToString(Session["RefNo"]), txtCurPostCode.Text.Trim(), "", "", Convert.ToString(Session["RefNo"]));

            string sCurPostcode = txtCurPostCode.Text.Trim();
            if (sCurPostcode.Length > 0)
            {

                btnCurSearch.Enabled = false;
                //LoadItems(lstCur, txtCurPostCode.Text.Trim());

                string sKey = string.Empty;
                //sKey = obj.RetPolValue("PCAUBI", "Key");
                sKey = obj.DecryPass(obj.RetPolValue("PCAUBI", "Key"));
                //Build the url
                var url = "http://services.postcodeanywhere.co.uk/CapturePlus/Interactive/Find/v2.10/dataset.ws?";
                url += "&Key=" + System.Web.HttpUtility.UrlEncode(sKey);
                url += "&SearchTerm=" + System.Web.HttpUtility.UrlEncode(txtCurPostCode.Text.Trim());
                url += "&LastId=" + System.Web.HttpUtility.UrlEncode("");
                url += "&SearchFor=" + System.Web.HttpUtility.UrlEncode("Everything");
                url += "&Country=" + System.Web.HttpUtility.UrlEncode("GBR");
                url += "&LanguagePreference=" + System.Web.HttpUtility.UrlEncode("EN");
                //url += "&MaxSuggestions=" + System.Web.HttpUtility.UrlEncode(maxsuggestions.ToString(CultureInfo.InvariantCulture));
                //url += "&MaxResults=" + System.Web.HttpUtility.UrlEncode(maxresults.ToString(CultureInfo.InvariantCulture));

                //Create the dataset
                var dataSet = new DataSet();
                dataSet.ReadXml(url);

                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables.Count == 1 && dataSet.Tables[0].Columns.Count == 4 && dataSet.Tables[0].Columns[0].ColumnName == "Error")
                    {
                        throw new Exception(dataSet.Tables[0].Rows[0].ItemArray[1].ToString());
                    }
                    else
                    {
                        obj.StoreEvent("99999", "", "", "", "Postcode Search Service is end", Convert.ToString(Session["RefNo"]));
                        lstCur.Items.Clear();
                        if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dataSet.Tables[0].Rows)
                            {
                                lstCur.Items.Add(new ListItem(dr[1].ToString() + " " + dr[2].ToString(), dr[0].ToString()));
                            }
                        }
                    }
                }

                //Check for an error
                ////if (dataSet.Tables.Count == 1 && dataSet.Tables[0].Columns.Count == 4 && dataSet.Tables[0].Columns[0].ColumnName == "Error")
                ////{
                ////    throw new Exception(dataSet.Tables[0].Rows[0].ItemArray[1].ToString());
                ////}
                ////else
                ////{
                ////    obj.StoreEvent("99999", "", "", "", "Postcode Search Service is end", Convert.ToString(Session["RefNo"]));
                ////    lstCur.Items.Clear();
                ////    if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                ////    {
                ////        foreach (DataRow dr in dataSet.Tables[0].Rows)
                ////        {
                ////            lstCur.Items.Add(new ListItem(dr[1].ToString() + " " + dr[2].ToString(), dr[0].ToString()));
                ////        }
                ////    }
                ////}

                if (lstCur.Items.Count > 0)
                    pnlAddCurSearch.Visible = true;
                else
                    lblAlert.Text = "Please enter the valid current address postcode!";
            }
            else
            {
                lblAlert.Text = "Current Address Postal code cannot be blank";
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            lblAlert.Text = "Service did not respond. Try again!";
        }
        finally
        {
            btnCurSearch.Enabled = true;
        }
    }

    protected void btnPCScur_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstCur.Items.Count > 0)
            {

                btnPCScur.Enabled = false;
                string sKey = string.Empty;
                //sKey = obj.RetPolValue("PCAUBI", "Key");
                sKey = obj.DecryPass(obj.RetPolValue("PCAUBI", "Key"));
                //Build the url
                var url = "http://services.postcodeanywhere.co.uk/CapturePlus/Interactive/Retrieve/v2.10/dataset.ws?";
                url += "&Key=" + System.Web.HttpUtility.UrlEncode(sKey);
                url += "&Id=" + System.Web.HttpUtility.UrlEncode(lstCur.SelectedValue);

                //Create the dataset
                var dataSet = new System.Data.DataSet();
                dataSet.ReadXml(url);

                //Check for an error
                if (dataSet.Tables.Count == 1 && dataSet.Tables[0].Columns.Count == 4 && dataSet.Tables[0].Columns[0].ColumnName == "Error")
                    throw new Exception(dataSet.Tables[0].Rows[0].ItemArray[1].ToString());

                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        txtCurDrNo.Text = string.Empty;
                        txtCurAddr1.Text = string.Empty;
                        txtCurAddr2.Text = string.Empty;
                        txtCurCounty.Text = string.Empty;
                        //txtCurAddr3.Text = string.Empty;
                        ddlCurCity.SelectedIndex = 0;
                        ddlCurCountry.SelectedIndex = 1;

                        if (obj.NullToSpace(dr["BuildingNumber"]) == string.Empty)
                            txtCurDrNo.Text = dr["SubBuilding"].ToString();
                        else
                            txtCurDrNo.Text = dr["BuildingNumber"].ToString();

                        //txtCurDrNo.Text = dr["BuildingNumber"].ToString();
                        txtCurAddr1.Text = dr["BuildingName"].ToString();
                        txtCurAddr2.Text = dr["Street"].ToString();
                        //txtCurAddr3.Text = dr["City"].ToString();

                        ddlCurCity.Text = obj.NullToSpace(dr["City"]).ToUpper();

                        txtCurPostCode.Text = dr["PostalCode"].ToString();


                        //txtCurDrNo.Text = dr["Line1"].ToString();
                        //txtCurAddr1.Text = dr["Line2"].ToString();
                        //txtCurAddr2.Text = dr["Line3"].ToString();
                        //txtCurAddr3.Text = dr["City"].ToString();
                        //txtCurPostCode.Text = dr["PostalCode"].ToString();

                    }
                }
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
        }
        finally
        {
            btnPCScur.Enabled = true;
            ClearAddressList();
        }
    }

    protected void btnPCScnclCur_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAddressList();
        }

        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    //Previous address
    protected void btnPreSearch_Click(object sender, EventArgs e)
    {
        try
        {
            obj.StoreEvent("66307", Convert.ToString(Session["RefNo"]), txtCurPostCode.Text.Trim(), "", "", Convert.ToString(Session["RefNo"]));

            string sPrePostcode = txtPrePostCode.Text.Trim();
            if (sPrePostcode.Length > 0)
            {

                btnPreSearch.Enabled = false;
                //LoadItems(lstPre, txtPrePostCode.Text.Trim());

                string sKey = string.Empty;
                //sKey = obj.RetPolValue("PCAUBI", "Key");
                sKey = obj.DecryPass(obj.RetPolValue("PCAUBI", "Key"));
                //Build the url
                var url = "http://services.postcodeanywhere.co.uk/CapturePlus/Interactive/Find/v2.10/dataset.ws?";
                url += "&Key=" + System.Web.HttpUtility.UrlEncode(sKey);
                url += "&SearchTerm=" + System.Web.HttpUtility.UrlEncode(txtPrePostCode.Text.Trim());
                url += "&LastId=" + System.Web.HttpUtility.UrlEncode("");
                url += "&SearchFor=" + System.Web.HttpUtility.UrlEncode("Everything");
                url += "&Country=" + System.Web.HttpUtility.UrlEncode("GBR");
                url += "&LanguagePreference=" + System.Web.HttpUtility.UrlEncode("EN");
                //url += "&MaxSuggestions=" + System.Web.HttpUtility.UrlEncode(maxsuggestions.ToString(CultureInfo.InvariantCulture));
                //url += "&MaxResults=" + System.Web.HttpUtility.UrlEncode(maxresults.ToString(CultureInfo.InvariantCulture));

                //Create the dataset
                var dataSet = new DataSet();
                dataSet.ReadXml(url);

                //Check for an error
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables.Count == 1 && dataSet.Tables[0].Columns.Count == 4 && dataSet.Tables[0].Columns[0].ColumnName == "Error")
                    {
                        throw new Exception(dataSet.Tables[0].Rows[0].ItemArray[1].ToString());
                    }
                    else
                    {
                        obj.StoreEvent("99999", "", "", "", "Postcode Search Service is end", Convert.ToString(Session["RefNo"]));
                        lstPre.Items.Clear();
                        if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dataSet.Tables[0].Rows)
                            {
                                lstPre.Items.Add(new ListItem(dr[1].ToString() + " " + dr[2].ToString(), dr[0].ToString()));
                            }
                        }
                    }
                }

                if (lstPre.Items.Count > 0)
                    pnlAddPreSearch.Visible = true;
            }
            else
            {
                lblAlert.Text = "Previous Address Postal code cannot be blank";
            }
        }

        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            lblAlert.Text = "Service did not respond. Try again!";
        }
        finally
        {
            btnPreSearch.Enabled = true;
        }
    }

    protected void btnPCSPre_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstPre.Items.Count > 0)
            {
                btnPCSPre.Enabled = false;

                string sKey = string.Empty;
                //sKey = obj.RetPolValue("PCAUBI", "Key");
                sKey = obj.DecryPass(obj.RetPolValue("PCAUBI", "Key"));
                //Build the url
                var url = "http://services.postcodeanywhere.co.uk/CapturePlus/Interactive/Retrieve/v2.10/dataset.ws?";
                url += "&Key=" + System.Web.HttpUtility.UrlEncode(sKey);
                url += "&Id=" + System.Web.HttpUtility.UrlEncode(lstPre.SelectedValue);

                //Create the dataset
                var dataSet = new System.Data.DataSet();
                dataSet.ReadXml(url);

                //Check for an error
                if (dataSet.Tables.Count == 1 && dataSet.Tables[0].Columns.Count == 4 && dataSet.Tables[0].Columns[0].ColumnName == "Error")
                    throw new Exception(dataSet.Tables[0].Rows[0].ItemArray[1].ToString());

                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        txtPreDrNo.Text = string.Empty;
                        txtPreAddr1.Text = string.Empty;
                        txtPreAddr2.Text = string.Empty;
                        txtPreCounty.Text = string.Empty;
                        //txtPreAddr3.Text = string.Empty;
                        ddlPreCity.SelectedIndex = 0;
                        ddlPreCountry.SelectedIndex = 1;

                        if (obj.NullToSpace(dr["BuildingNumber"]) == string.Empty)
                            txtPreDrNo.Text = dr["SubBuilding"].ToString();
                        else
                            txtPreDrNo.Text = dr["BuildingNumber"].ToString();

                        //txtPreDrNo.Text = dr["BuildingNumber"].ToString();
                        txtPreAddr1.Text = dr["BuildingName"].ToString();
                        txtPreAddr2.Text = dr["Street"].ToString();
                        //txtPreAddr3.Text = dr["City"].ToString();
                        ddlPreCity.Text = obj.NullToSpace(dr["City"]).ToUpper();
                        txtPrePostCode.Text = dr["PostalCode"].ToString();


                        //ddlPreCountry.SelectedItem.Text = dr["CountryName"].ToString();

                        //txtPreDrNo.Text = dr["BuildingNumber"].ToString();
                        //txtPreAddr1.Text = dr["BuildingName"].ToString();
                        //txtPreAddr2.Text = dr["Street"].ToString();
                        //txtPreAddr3.Text = dr["City"].ToString();
                        //txtPreCounty.Text = dr["District"].ToString();
                        //ddlPreCountry.SelectedItem.Text = dr["CountryName"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
        }
        finally
        {
            btnPCSPre.Enabled = true;
            ClearAddressList();
        }
    }

    protected void btnPCScnclPre_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAddressList();
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnPCScnclMail_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAddressList();
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private void ClearAddressList()
    {

        try
        {
            pnlAddCurSearch.Visible = false;
            lstCur.Items.Clear();
            pnlAddPreSearch.Visible = false;
            lstPre.Items.Clear();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void rblJntApAddSts_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (rblJntApAddSts.SelectedValue.ToUpper() == "Yes".ToUpper())
            {
                pnlAddrDtls.Visible = false;
            }
            else
            {
                pnlAddrDtls.Visible = true;
            }

        }

        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private bool SaveAsDraftValidation()
    {
        bool bStatus = false;

        try
        {
            AccountDtl AcDtl = null;
            List<Applicant> lstUsr = null;

            AcDtl = (AccountDtl)Session["AcDtl"];

            lstUsr = GetUserList();

            if ((btnSaveAsDraft.CausesValidation == true) && (Page.IsValid == false))
            {
                bStatus = false;
            }

            if (lstUsr != null && lstUsr.Count > 0)
            {
                if ((lstUsr.Count == 4))
                {
                    if (btnSave.Text.ToUpper() == "Add Applicant".ToUpper())
                    {
                        lblAlert.Text = "There can be a maximum of 4 Applicants per application.";
                        bStatus = false;
                    }
                }
            }

            string sFirstNm = txtFirstNm.Text.Trim();
            string sSurNm = txtSurNm.Text.Trim();
            if (lstUsr != null && lstUsr.Count > 1)
            {
                if (sFirstNm == string.Empty)
                {
                    lblAlert.Text = "First name cannot be blank";
                    bStatus = false;
                }
            }
            else
            {
                if (sFirstNm == string.Empty)
                {
                    lblAlert.Text = "First name cannot be blank";
                    bStatus = false;
                }
                if (sSurNm == string.Empty)
                {
                    lblAlert.Text = " Surname cannot be blank";
                    bStatus = false;
                }
                if (sSurNm.Length < 2)
                {
                    lblAlert.Text = " Surname should have atleast 2 characters length";
                    bStatus = false;
                }
                if (!string.IsNullOrEmpty(txtEmailAddr.Text.Trim()))
                {
                    string pattern = null;
                    pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

                    if (!Regex.IsMatch(txtEmailAddr.Text.Trim(), pattern))
                    {
                        lblAlert.Text = "Email address is invalid, please provide valid email address !";
                        bStatus = false;
                    }
                }
                else
                {
                    lblAlert.Text = "Please provide email address to retrieve your saved application !";
                    bStatus = false;
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

        return bStatus;
    }

    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {
        lblRefNo.Text = string.Empty;
        string sType = Convert.ToString(Session["Type"]);
        AccountDtl AcDtl = null;
        List<Applicant> lstUsr = null;

        try
        {
            btnSaveAsDraft.Enabled = false;

            AcDtl = (AccountDtl)Session["AcDtl"];

            ////Chellappa - 20250422
            //if (AcDtl != null && AcDtl.appList != null && AcDtl.NoOfApplicants > 0)
            //{
            //    string sFirstName = txtFirstNm.Text.Trim();
            //    string sMidNm = txtMiddleNm.Text.Trim();
            //    string sSurName = txtSurNm.Text.Trim();
            //    string sDOB = CalDOB.SelDate;
            //    string sCurPcode = txtCurPostCode.Text.Trim();

            //    if (sFirstName != "" && sSurName != "" && sDOB != "" && sCurPcode != "")
            //    {
            //        if (IsExistingPendingCust())
            //        {
            //            lblAlert.CssClass = "text-danger font-weight-bold font-16";
            //            lblAlert.Text = "Applicant is an existing customer of UBI(UK) Ltd. Kindly Contact Bank for Assistance.";
            //            obj.StoreEvent("88888", "", "", "", "Applicant is an existing pending customer of UBI(UK) Ltd. Kindly Contact Bank for Assistance.", Convert.ToString(Session["RefNo"]));
            //            return;
            //        }

            //        if (IsExistingcustomer())
            //        {
            //            lblAlert.CssClass = "text-danger font-weight-bold font-16";
            //            lblAlert.Text = "Applicant is an existing customer of UBI(UK) Ltd. Kindly Contact Bank for Assistance.";
            //            obj.StoreEvent("88888", "", "", "", "Applicant is an existing customer of UBI(UK) Ltd. Kindly Contact Bank for Assistance.", Convert.ToString(Session["RefNo"]));
            //            return;
            //        }
            //    }                
            //}
            ////End

            lstUsr = GetUserList();

            if ((btnSaveAsDraft.CausesValidation == true) && (Page.IsValid == false))
            {
                return;
            }

            int seq = 0;

            if (lstUsr != null && lstUsr.Count > 0)
            {
                if ((lstUsr.Count == 4))
                {
                    if (btnSave.Text.ToUpper() == "Add Applicant".ToUpper())
                    {
                        lblAlert.Text = "There can be a maximum of 4 Applicants per application.";
                        return;
                    }
                }
            }

            if (IsDuplicate("SE"))
            {
                //lblAlert.Text = "The same applicant available in the list, kindly check the applicants and continue.";
                lblAlert.Text = "The Same applicant details are available in the applicant list, kindly go to step-1 check the applicants and continue.";
                return;
            }

            string sFirstNm = txtFirstNm.Text.Trim();
            string sSurNm = txtSurNm.Text.Trim();
            if (lstUsr != null && lstUsr.Count > 1)
            {
                if (sFirstNm == string.Empty)
                {
                    lblAlert.Text = "First name cannot be blank";
                    txtFirstNm.Focus();
                    return;
                }
            }
            else
            {
                //2024 commented for 2023 enhancements
                //if (sFirstNm == string.Empty)
                //{
                //    lblAlert.Text = "First name cannot be blank";
                //    txtFirstNm.Focus();
                //    return;
                //}

                //if (sSurNm == string.Empty)
                //{
                //    lblAlert.Text = " Surname cannot be blank";
                //    txtSurNm.Focus();
                //    return;
                //}
                //End

                if (!string.IsNullOrEmpty(txtEmailAddr.Text.Trim()))
                {
                    string pattern = null;
                    pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

                    if (!Regex.IsMatch(txtEmailAddr.Text.Trim(), pattern))
                    {
                        lblAlert.Text = "Email address is invalid, please provide valid email address !";
                        txtEmailAddr.Focus();
                        return;
                    }
                }
                else
                {
                    lblAlert.Text = "Please provide email address to retrieve your saved application !";
                    txtEmailAddr.Focus();
                    return;
                }
            }

            //2024 - Email address verification
            if (Convert.ToString(ViewState["Sequence"]) == "1")
            {
                if (chkEmailVerSts(txtEmailAddr.Text.Trim()) == false)
                {
                    lblAlert.Text = "Please verify your email address before saving the application for future retrieval.";
                    txtEmailAddr.Focus();
                    return;
                }
            }
            //End

            SaveValues();

            if (sType == "NA")
            {
                oAcDtl = (AccountDtl)Session["AcDtl"];

                if (Session["AcOpenRefNo"] != null && Convert.ToString(Session["AcOpenRefNo"]) == oAcDtl.ReferenceNo)
                {
                    //ModalPopupExtender2.Show();
                    //ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-block";
                    //PnlPwdReg.CssClass = "d-block zindex_1";
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
                            //PnlPwdReg.CssClass = "d-block zindex_1";
                            //lblRefNoVal.Text = oAcDtl.ReferenceNo;

                            SaveAndSendEmail();
                        }
                        else
                        {
                            lblAlert.Text = "Unable to process your request now, kindly contact branch!";
                            obj.StoreEvent("88888", "", "", "", "Failed to save the information.", Convert.ToString(Session["RefNo"]));
                        }
                    }
                    else
                    {
                        AccountDtl AcD = GetAcDtl();
                        lblAlert.Text = "Unable to process your request now, kindly contact branch!";
                        obj.StoreEvent("88888", "", "", "", "Information already exist for the Reference No: " + AcD.ReferenceNo + " Kindly retrieve application and update your changes. Please contact the nearest branch if you have any query!", Convert.ToString(Session["RefNo"]));
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
                        //PnlPwdReg.CssClass = "d-block zindex_1";
                        //lblRefNoVal.Text = AcD.ReferenceNo;

                        SaveAndSendEmail();
                    }
                    else
                    {
                        lblAlert.Text = "Unable to process your request now, kindly contact branch!";
                        obj.StoreEvent("88888", "", "", "", "Failed to update the information for the Reference No: " + AcD.ReferenceNo + ". Please contact the nearest branch!", Convert.ToString(Session["RefNo"]));
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

    private bool chkEmailVerSts(string SEmailVal)
    {
        bool bStatus = false;
        string sQuery = string.Empty;
        string sValues = string.Empty;
        string sAcId = string.Empty;
        DataTable dtAcDtl = null;
        try
        {
            dtAcDtl = obj.ExecuteData("SELECT EMVERIFIED FROM OTPFOREMAIL WHERE EMOTPEMAIL='" + SEmailVal + "' AND OTPSTATUS='Y' AND EMOTPREFNO='" + Session["RefNo"] + "'");
            if ((dtAcDtl != null) && (dtAcDtl.Rows.Count > 0))
            {
                if (dtAcDtl.Rows[0]["EMVERIFIED"].ToString().ToUpper() == "Y")
                {
                    bStatus = true;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bStatus;
    }

    private string CreateRefNo(string pType)
    {
        Random random = new Random();
        string sRefNo = string.Empty;
        string sJulianDt = string.Empty;
        string sRandomNm = string.Empty;

        string sSequence = string.Empty;

        try
        {
            sJulianDt = obj.JulianDate(System.DateTime.Now);
            sRandomNm = "A";
            sSequence = GetSequence(pType).ToString("00000");

            sRefNo = sJulianDt + sRandomNm + sSequence;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return sRefNo;
    }

    private int GetSequence(string pType)
    {
        int sSequence = 0;
        try
        {
            DataTable dTable = obj.ExecuteData("SELECT [DATE],[SEQNO],DATEPART(yyyy,[DATE]) AS [Year] FROM SEQPF WHERE TYPE='" + pType + "'");

            if (dTable != null && dTable.Rows.Count > 0)
            {
                int iRunningSeqNo = 0;
                string sCurDt = DateTime.Now.ToString("yyyyMMdd");
                string sTblDt = obj.NullToSpace(dTable.Rows[0]["DATE"]);

                string sCurYear = DateTime.Now.ToString("yyyy");
                string sTblYear = obj.NullToSpace(dTable.Rows[0]["Year"]);

                string sTblSeq = obj.NullToSpace(dTable.Rows[0]["SEQNO"]);

                if (sCurYear == sTblYear)
                {
                    iRunningSeqNo = Convert.ToInt32(sTblSeq);
                }
                else
                {
                    obj.ExecuteCommand("UPDATE SEQPF SET [DATE]='" + sCurDt + "',[SEQNO]='1' WHERE TYPE='" + pType + "'");
                    iRunningSeqNo = 1;
                }

                obj.ExecuteCommand("UPDATE SEQPF SET [SEQNO]='" + Convert.ToString(iRunningSeqNo + 1) + "' WHERE TYPE='" + pType + "'");

                sSequence = iRunningSeqNo;
            }
            else
            {
                throw new Exception("SEQPF table empty");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return sSequence;
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
            throw ex;
        }
        return bStatus;
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

    private bool SaveAcDtl(AccountDtl AcD)
    {
        bool bStatus = false;
        string sQuery = string.Empty;
        string sValues = string.Empty;
        string sAcId = string.Empty;
        string sAppSts = "D";
        string sAcStatus = "KYP";

        sCreatedBy = AcD.ReferenceNo;

        DataTable dtAcDtl = null;

        try
        {

            sQuery = " INSERT INTO ACDTLPF (" +
                     " ACOPENREFNO,REFNO,ISJNTAC,[STATUS]," +
                     " INVAMOUNT,INVPERIOD,INVRATINT," +
                     " OTHBANKNAME,OTHBANKSC,OTHBANKACNO,REPAYINS," +
                     " [CREDATE],[CRETIME],[CREATEDBY],APPSTS,NOOFJNTAPLNT)";

            sValues = " SELECT '" + AcD.ReferenceNo + "','" + AcD.RegUniqueId + "','" + AcD.IsJntAc + "','" + sAcStatus + "'," +
                      "'" + obj.NullToZero(AcD.InvAmount) + "','" + AcD.InvPeriod + "','" + obj.NullToZero(AcD.InvRateOfInt) + "'," +
                      "'" + AcD.OthBankName + "','" + AcD.OthBankSC + "','" + AcD.OthBankAcNo + "','" + AcD.RepayIns + "'," +
                      "'" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("HHmmss") + "','" + sCreatedBy + "','" + sAppSts + "','" + AcD.NoOfApplicants + "'";
            obj.ExecuteCommand(sQuery + sValues);

            dtAcDtl = obj.ExecuteData("SELECT ACID,ACOPENREFNO,REFNO FROM ACDTLPF WHERE ACOPENREFNO='" + AcD.ReferenceNo + "'");
            if ((dtAcDtl != null) && (dtAcDtl.Rows.Count > 0))
            {
                if (dtAcDtl.Rows[0]["ACOPENREFNO"].ToString().ToUpper() == AcD.ReferenceNo.ToUpper())
                {
                    sAcId = dtAcDtl.Rows[0]["ACID"].ToString();

                    if (Session["AcDtl"] != null)
                    {
                        oAcDtl = (AccountDtl)Session["AcDtl"];

                        oAcDtl.AcId = sAcId;
                        Session["AcDtl"] = oAcDtl;
                    }

                    bStatus = true;
                    if (SaveApplicant(AcD, sAcId))
                    {
                        bStatus = true;
                    }
                    else
                    {
                        bStatus = false;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bStatus;
    }

    private bool SaveApplicant(AccountDtl AcDtl, string AcId)
    {
        bool bStatus = false;
        string sQuery = string.Empty;
        DataTable dtApDtl = null;
        List<Applicant> lstAplcnt = null;
        StringBuilder aplQry = new StringBuilder("");

        try
        {
            lstAplcnt = AcDtl.appList;

            if (lstAplcnt != null && lstAplcnt.Count > 0)
            {
                aplQry.Append("INSERT INTO APDTLPF (");
                aplQry.Append("ACID,ACOPENREFNO,REFNO,ISPRIMARY,SEQUENCE,TITLE,");
                aplQry.Append("FIRSTNM,MIDNM,SURNM,GENDER,IDENTDTLS,");
                aplQry.Append("ISUKPSPRT,PSPORTISDT,PSPORTEXDT,PSPORTISCNTRY,IDENTITYNO,CITIZENSHIP,");
                aplQry.Append("MARITALSTS,MARITALSTSOTH,DOB,HOMETELNO,MOBILENO,");
                aplQry.Append("EMAILADDR,USEPRIMADDR,CURDRNO,CURADDR1,CURADDR2,CURADDR3,CURCOUNTY,");
                aplQry.Append("CURPCODE,CURCNTRY,RESIDINGSINCE,MAILINGADDR,PREDRNO,PREADDR1,");
                aplQry.Append("PREADDR2,PREADDR3,PRECOUNTY,PREPCODE,PRECNTRY)");
                //aplQry.Append("BANDTEXT,AUTHID,AUTHDATETIME,WSPROFILEID,WSPROFILENAME,[STATUS])");

                int i = 0;
                foreach (Applicant ApD in lstAplcnt)
                {
                    i += 1;

                    aplQry.Append(" SELECT '" + AcId + "','" + AcDtl.ReferenceNo + "','" + AcDtl.RegUniqueId + "','" + ApD.IsPrimary + "','" + Convert.ToString(i) + "','" + ApD.Title + "',");
                    aplQry.Append("'" + ApD.FirstNm + "','" + ApD.MidNm + "','" + ApD.SurNm + "','" + ApD.Gender + "','" + ApD.IdenDtls + "',");
                    aplQry.Append("'" + ApD.IsUkPsPrt + "','" + obj.DTOC(ApD.PsPrtIssDt) + "','" + obj.DTOC(ApD.PsPrtExpDt) + "','" + ApD.PsPrtIsCntry + "','" + ApD.IdenNo + "','" + ApD.Citizenship + "',");
                    aplQry.Append("'" + ApD.MaritalSts + "','" + ApD.MaritalStsOth + "','" + obj.DTOC(ApD.DOB) + "','" + ApD.HomeTelNo + "','" + ApD.MobileNo + "',");
                    aplQry.Append("'" + ApD.EmailAddr + "','" + ApD.UsePrimaryAddr + "','" + ApD.CurDoorNo + "','" + ApD.CurAddr1 + "','" + ApD.CurAddr2 + "','" + ApD.CurAddr3 + "','" + ApD.CurCounty + "',");
                    aplQry.Append("'" + ApD.CurPcode + "','" + ApD.CurCntry + "','" + obj.DTOC(ApD.ResidingSince) + "','" + ApD.MailingAddr + "','" + ApD.PreDoorNo + "','" + ApD.PreAddr1 + "',");
                    aplQry.Append("'" + ApD.PreAddr2 + "','" + ApD.PreAddr3 + "','" + ApD.PreCounty + "','" + ApD.PrePcode + "','" + ApD.PreCntry + "' UNION ALL ");


                    if (i == 1)
                    {
                        if (AcDtl.IsJntAc.ToUpper() == "No".ToUpper())
                            break;
                    }

                }

                if (aplQry.Length > 0)
                {
                    sQuery = aplQry.Remove(aplQry.Length - 10, 10).ToString();
                    obj.ExecuteCommand(sQuery);

                    dtApDtl = obj.ExecuteData("SELECT COUNT(ACID) as CNT FROM APDTLPF WHERE ACID='" + AcId + "'");
                    if (AcDtl.IsJntAc.ToUpper() == "No".ToUpper())
                    {
                        if ((dtApDtl != null) && (dtApDtl.Rows.Count > 0) && (Convert.ToInt32(dtApDtl.Rows[0]["CNT"]) == 1))
                        {
                            bStatus = true;
                        }
                    }
                    else
                    {
                        if ((dtApDtl != null) && (dtApDtl.Rows.Count > 0) && (Convert.ToInt32(dtApDtl.Rows[0]["CNT"]) == lstAplcnt.Count))
                        {
                            bStatus = true;
                        }
                    }
                }

                if ((sCreatedBy.Length > 0))
                {
                    //obj.StoreEvent("99999", "", "", "", "The A/C opening application " + AcDtl.ReferenceNo + " has been successfully saved by " + sCreatedBy + " and pending to be completed.", sCreatedBy);
                    obj.StoreEvent("65103", AcDtl.ReferenceNo, sCreatedBy, "", "", sCreatedBy);
                }

                if ((sModifiedBy.Length > 0))
                {
                    if (AcDtl.AppStatus.ToUpper() == "S")
                    {
                        //obj.StoreEvent("99999", "", "", "", "The A/C opening application " + AcDtl.ReferenceNo + " has been successfully modified by : " + sModifiedBy, sModifiedBy);
                        obj.StoreEvent("65102", AcDtl.ReferenceNo, sModifiedBy, "", "", sModifiedBy);
                    }
                    else
                    {
                        //obj.StoreEvent("99999", "", "", "", "The A/C opening application " + AcDtl.ReferenceNo + " has been successfully saved by " + sModifiedBy + " and pending to be completed.", sModifiedBy);
                        obj.StoreEvent("65103", AcDtl.ReferenceNo, sModifiedBy, "", "", sModifiedBy);
                    }
                }
            }
            else
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

    private bool UpdateAcDtl(AccountDtl AcD)
    {
        bool bStatus = false;
        string sQuery = string.Empty;
        string sAcId = string.Empty;
        string sAcStatus = string.Empty;
        string sType = Convert.ToString(Session["Type"]);

        sModifiedBy = AcD.ReferenceNo;

        try
        {

            //if (sType == "EA")
            //    sAcStatus = GetAcStatus(AcD);
            //else
            //    sAcStatus = "KYP";


            sQuery = " UPDATE ACDTLPF SET" +
                     " ISJNTAC='" + AcD.IsJntAc + "',[STATUS]='" + AcD.Status + "'," +
                     " INVAMOUNT='" + obj.NullToZero(AcD.InvAmount) + "',INVPERIOD='" + AcD.InvPeriod + "'," +
                     " INVRATINT='" + obj.NullToZero(AcD.InvRateOfInt) + "',OTHBANKNAME='" + AcD.OthBankName + "'," +
                     " OTHBANKSC='" + AcD.OthBankSC + "',OTHBANKACNO='" + AcD.OthBankAcNo + "',REPAYINS='" + AcD.RepayIns + "'," +
                     " [MODDATE]='" + DateTime.Now.ToString("yyyyMMdd") + "',[MODTIME]='" + DateTime.Now.ToString("HHmmss") + "',[MODIFIEDBY]='" + sModifiedBy + "',[NOOFJNTAPLNT]='" + AcD.NoOfApplicants + "'" +
                     " WHERE ACID='" + AcD.AcId + "' AND ACOPENREFNO='" + AcD.ReferenceNo + "'";

            obj.ExecuteCommand(sQuery);
            bStatus = true;

            sQuery = "DELETE FROM APDTLPF WHERE ACID='" + AcD.AcId + "' AND ACOPENREFNO='" + AcD.ReferenceNo + "'";
            obj.ExecuteCommand(sQuery);

            if (SaveApplicant(AcD, AcD.AcId))
            {
                bStatus = true;
            }
            else
            {
                bStatus = false;
            }
        }
        catch (Exception ex)
        {
            bStatus = false;
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
            throw ex;
        }
        return sStatus;
    }

    protected void lblExit_Click(object sender, EventArgs e)
    {
        try
        {
            obj.StoreEvent("66310", Convert.ToString(Session["RefNo"]), "", "", "", Convert.ToString(Session["RefNo"]));
            ClearSessionAndCache();
            Session.Abandon();
        }

        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void lstCur_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void lstPre_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnBackToStep1_Click(object sender, EventArgs e)
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

    protected void btnApplicant1_Click(object sender, EventArgs e)
    {
        try
        {
            ddlTittle.SelectedIndex = 1;
            txtFirstNm.Text = "RAJNEESH"; // "PAUL JEFFERY";
            txtMiddleNm.Text = "";
            txtSurNm.Text = "ROHILLA"; //"LILLICO";
            rblGender.SelectedValue = "MALE";
            CalDOB.SelDate = "11/06/1984"; //"13/05/1972";
            txtPlaceOfBirth.Text = "Nazibabad"; //"Trichy";
            txtMomName.Text = "MomName"; //"Maheswari";
            ddlCtznShp.SelectedIndex = 2;
            //ddlMaritalSts.SelectedIndex = 2;
            txtMobileNo.Text = "07885546854";
            txtEmailAddr.Text = "chellappa.b@macroglobal.co.uk";
            rblIdentity.SelectedIndex = 1;
            rblIdentity_SelectedIndexChanged(sender, e);
            rblPassport.SelectedValue = "IT";
            rblPassport_SelectedIndexChanged(sender, e);

            txtIntPassportL1.Text = "P<";
            txtIntPassportL2.Text = "IND";
            txtIntPassportL3.Text = "ROHILLA<<RAJNEESH<<<<<<<<<<<<<<<<<<<<<<";

            txtIntPassport1.Text = "Z3652058<";
            txtIntPassport2.Text = "0";
            txtIntPassport3.Text = "IND";
            txtIntPassport4.Text = "8406114";
            txtIntPassport5.Text = "M";
            txtIntPassport6.Text = "2611022";
            txtIntPassport7.Text = "<<<<<<<<<<<<<<";
            txtIntPassport8.Text = "<";
            txtIntPassport9.Text = "2";
            calDOI.SelDate = "03/11/2016";
            calDOE.SelDate = "02/11/2026";

            ddlPsPrtIsCntry.SelectedIndex = 1;
            ddlEmpType.SelectedIndex = 1;

            txtCurPostCode.Text = "HA14FR";
            txtCurDrNo.Text = "19";
            txtCurAddr2.Text = "PINNER ROAD";
            //txtCurAddr3.Text = "HARROW";
            ddlCurCity.Text = "HARROW";
            ddlCurCountry.SelectedIndex = 1;
            txtRsdnSnc.Text = "17/12/2015";

            rblUSPerson.SelectedValue = "No";

            rblPayTax.SelectedValue = "No";
            rblUSCitizen.SelectedValue = "No";
            rblGreenCard.SelectedValue = "No";
            rblRealEst.SelectedValue = "No";
            rblassets.SelectedValue = "No";
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnApplicant2_Click(object sender, EventArgs e)
    {
        try
        {
            ddlTittle.SelectedIndex = 1;
            txtFirstNm.Text = "SELVA";
            txtSurNm.Text = "VV";
            rblGender.SelectedValue = "MALE";
            CalDOB.SelDate = "10/08/1974";
            txtPlaceOfBirth.Text = "WATFORD";
            txtMomName.Text = "BALI";
            ddlCtznShp.SelectedIndex = 2;
            //ddlMaritalSts.SelectedIndex = 2;
            txtMobileNo.Text = "07885546854";
            txtEmailAddr.Text = "balamurali.r@macroglobal.co.uk";

            rblIdentity.SelectedIndex = 0;
            rblIdentity_SelectedIndexChanged(sender, e);

            rbldltype.SelectedIndex = 0;

            txtDRLNo1.Text = "VAID9";
            txtDRLNo2.Text = "708104";
            txtDRLNo3.Text = "A99KT";
            calDVLCDOE.SelDate = "09/08/2044";
            txtDVLCPcode.Text = "WD24 7RE";

            ddlEmpType.SelectedIndex = 1;

            txtCurPostCode.Text = "WD24 7RE";
            txtCurDrNo.Text = "6";
            txtCurAddr2.Text = "Northfield Gardens";
            //txtCurAddr3.Text = "WATFORD";
            ddlCurCity.Text = "WATFORD";
            ddlCurCountry.SelectedIndex = 1;
            txtRsdnSnc.Text = "01/06/2015";

            rblUSPerson.SelectedValue = "No";

            rblPayTax.SelectedValue = "No";
            rblUSCitizen.SelectedValue = "No";
            rblGreenCard.SelectedValue = "No";
            rblRealEst.SelectedValue = "No";
            rblassets.SelectedValue = "No";

        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnApplicant3_Click(object sender, EventArgs e)
    {
        try
        {
            ddlTittle.SelectedIndex = 1;
            txtFirstNm.Text = "Chellappa";
            txtSurNm.Text = "CB";
            rblGender.SelectedValue = "Male";
            CalDOB.SelDate = "10/02/1990";
            ddlCtznShp.SelectedIndex = 2;
            //ddlMaritalSts.SelectedIndex = 2;
            txtEmailAddr.Text = "balamurali.r@macroglobal.co.uk";
            txtDRLNo1.Text = "33333";
            txtDRLNo2.Text = "333333";
            txtDRLNo3.Text = "33333";
            calDOI.SelDate = "10/01/2012";
            calDOE.SelDate = "10/01/2022";
            //rblJntApAddSts.SelectedValue = "Yes";
            //txtCurPostCode.Text = "IG119XW";
            //txtCurDrNo.Text = "10 NEW STREET";
            //txtCurAddr3.Text = "LONDON";
            //ddlCurCountry.SelectedIndex = 1;
            //txtRsdnSnc.Text = "12/12/2011";
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnApplicant4_Click(object sender, EventArgs e)
    {
        try
        {
            ddlTittle.SelectedIndex = 1;
            txtFirstNm.Text = "Kirubha";
            txtSurNm.Text = "S";
            rblGender.SelectedValue = "Male";
            CalDOB.SelDate = "10/02/1992";
            ddlCtznShp.SelectedIndex = 2;
            //ddlMaritalSts.SelectedIndex = 2;
            txtEmailAddr.Text = "balamurali.r@macroglobal.co.uk";
            txtDRLNo1.Text = "44444";
            txtDRLNo2.Text = "444444";
            txtDRLNo3.Text = "44444";
            calDOI.SelDate = "10/01/2014";
            calDOE.SelDate = "10/01/2026";
            //rblJntApAddSts.SelectedValue = "Yes";
            //txtCurPostCode.Text = "IG119XW";
            //txtCurDrNo.Text = "10 NEW STREET";
            //txtCurAddr3.Text = "LONDON";
            //ddlCurCountry.SelectedIndex = 1;
            //txtRsdnSnc.Text = "12/12/2011";
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnSubmitEmail_Click(object sender, EventArgs e)
    {
        try
        {
            obj.StoreEvent("66305", Convert.ToString(Session["RefNo"]), "", "", "", Convert.ToString(Session["RefNo"]));

            //SaveAndSendEmail();

            AccountDtl AcDtl = GetAcDtl();
            if (AcDtl != null && AcDtl.appList != null && AcDtl.appList.Count > 0)
            {
                Cache.Remove("Cnfrmtn" + Convert.ToString(Session["loginid"]));
                Response.Redirect("Confirmation.aspx?R=" + obj.UrlEncrypt64("Ref=" + AcDtl.ReferenceNo + " & AplnSts=D"), false);
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
        finally
        {
            ModalPopupExtender2.Hide();
            ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-none";
            PnlPwdReg.CssClass = "d-none zindex_1";
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
                PnlPwdReg.CssClass = "d-block zindex_1";
                lblRefNoVal.Text = AcDtl.ReferenceNo.Trim();

                //Commented 2023 enhancement remaining
                //Cache.Remove("Cnfrmtn" + Convert.ToString(Session["loginid"]));
                //Response.Redirect("Confirmation.aspx?R=" + obj.UrlEncrypt64("Ref=" + AcDtl.ReferenceNo + " & AplnSts=D"), false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //Commented 2023 enhancement remaining
            //ModalPopupExtender2.Hide();
            //ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-none";
            //PnlPwdReg.CssClass = "d-none zindex_1";
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
    //            PnlPwdReg.CssClass = "d-block zindex_1";
    //        }
    //        else
    //        {
    //            bStatus = true;
    //            lblPopupAlert.Text = string.Empty;
    //            ModalPopupExtender2.Hide();
    //            ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-none";
    //            PnlPwdReg.CssClass = "d-none zindex_1";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
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



    //                    bStatus = false;
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

    //            Cache.Remove("Cnfrmtn" + Convert.ToString(Session["loginid"]));
    //            Response.Redirect("Confirmation.aspx?R=" + obj.UrlEncrypt64("Ref=" + AcDtl.ReferenceNo + " & AplnSts=D"), false);


    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        txtEmailId.Text = string.Empty;
    //        txtPassword.Text = string.Empty;
    //        lblPopupAlert.Text = string.Empty;
    //        ModalPopupExtender2.Hide();
    //        ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-none";
    //        PnlPwdReg.CssClass = "d-none zindex_1";
    //    }

    //}
    //End

    protected void ddlEmpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlEmpType.SelectedValue.ToUpper() == "Others".ToUpper())
            {
                pnlEmpOth.Visible = true;
            }
            else
            {
                pnlEmpOth.Visible = false;
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }
    protected void rblUSPerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblUSPerson.SelectedValue.ToUpper() == "Yes".ToUpper())
            {
                pnlUSperson.Visible = true;
            }
            else
            {
                pnlUSperson.Visible = false;
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    oAcDtl = (AccountDtl)Session["AcDtl"];

        //    if (oAcDtl != null)
        //    {
        //        if (oAcDtl.appList.Count > 1)
        //        {
        //            MPEDataLose.Hide();
        //            ClearApplicant();
        //            Cache.Remove("AcDtl" + Convert.ToString(Session["RefNo"]));
        //            Response.Redirect("AccountDetails.aspx", false);
        //        }

        //        MPEDataLose.Hide();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
        //    throw ex;
        //}
    }

    private void ClearApplicant()
    {
        try
        {
            int iSequence = 0;
            iSequence = Convert.ToInt32(ViewState["Sequence"]);

            oAcDtl = (AccountDtl)Session["AcDtl"];

            List<Applicant> lstAppl = new List<Applicant>();
            List<Applicant> lstTemp = new List<Applicant>();
            List<Applicant> lstNew = new List<Applicant>();
            lstAppl = oAcDtl.appList;

            Applicant Usr = (from d in lstAppl where d.Sequence == iSequence select d).First();
            lstAppl.Remove(Usr);

            lstTemp = lstAppl.OrderBy(x => x.Sequence).ToList();

            lstAppl = new List<Applicant>();

            int i = 1;
            foreach (Applicant oApplicant in lstTemp)
            {
                oApplicant.Sequence = i;
                lstNew.Add(oApplicant);
                i += 1;
            }

            //Applicant applicant = new Applicant();
            //applicant.Sequence = iSequence;
            //lstNew.Add(applicant);

            oAcDtl.appList = lstNew;
            Session["AcDtl"] = oAcDtl;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnCncl_Click(object sender, EventArgs e)
    {
        try
        {
            MPEDataLose.Hide();
            MPEDataLose.BackgroundCssClass = "sctableBackground d-none";
            PnlMsgBox.CssClass = "d-none zindex_1";
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnOkDL_Click(object sender, EventArgs e)
    {
        try
        {
            oAcDtl = (AccountDtl)Session["AcDtl"];

            if (oAcDtl != null)
            {
                if (oAcDtl.appList.Count > 1)
                {
                    MPEDataLose.Hide();
                    MPEDataLose.BackgroundCssClass = "sctableBackground d-none";
                    PnlMsgBox.CssClass = "d-none zindex_1";
                    ClearApplicant();
                    Cache.Remove("AcDtl" + Convert.ToString(Session["RefNo"]));
                    Response.Redirect("AccountDetails.aspx", false);

                }

                MPEDataLose.Hide();
                MPEDataLose.BackgroundCssClass = "sctableBackground d-none";
                PnlMsgBox.CssClass = "d-none zindex_1";
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private bool IsValidCity()
    {
        bool bCStatus = false;

        DataTable dtCN = new DataTable();

        try
        {
            //dtCN = obj.ExecuteData("SELECT CITYNAME FROM CITYMST WHERE CITYNAME='" + obj.replaceSplChr(txtCurAddr3.Text.Trim()) + "'");

            dtCN = obj.ExecuteData("SELECT CITYNAME FROM CITYMST WHERE CITYNAME='" + obj.replaceSplChr(ddlCurCity.SelectedValue.Trim()) + "'");

            if (dtCN != null && dtCN.Rows.Count > 0)
                bCStatus = true;
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
        }
        return bCStatus;
    }


    protected void ddlTittle_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string[] _GenderMList = { "MR", "SIR" };
            string[] _GenderFList = { "MS", "MISS", "MRS" };

            if (_GenderMList.Contains(ddlTittle.SelectedValue.ToUpper()))
            {
                rblGender.SelectedValue = "MALE";
            }
            else if (_GenderFList.Contains(ddlTittle.SelectedValue.ToUpper()))
            {
                rblGender.SelectedValue = "FEMALE";
            }
            else
            {
                rblGender.SelectedIndex = -1;
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void ddlSOF_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSOF.Text.ToUpper() == "Others".ToUpper())
            {
                pnlSOFOth.Visible = true;
            }
            else
            {
                pnlSOFOth.Visible = false;
                txtSOFOth.Text = "";
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    #region OTP
    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        try
        {
            string sCustEmail = string.Empty;
            sCustEmail = txtEmailAddr.Text.Trim();
            Session["OTPCustEmail"] = sCustEmail;
            otpResendAttempt.Value = "0";

            obj.StoreEvent("99999", "", "", "", "Initiated to send OTP to customer process", Convert.ToString(Session["RefNo"]));
            //Send OTP to customer  --2025 ....
            if (sCustEmail == string.Empty)
            {
                lblAlert.Text = "Please enter Email Address";
                txtEmailAddr.Focus();
                return;
            }
            else if (SendOTPTOCUST(sCustEmail) == true)
            {
                txtOTP.Text = "";
                lblOTPAlert.Text = "";
                lblOTPMsg.Text = "";
                ModalPopupEmailOTP.Show();
                ModalPopupEmailOTP.BackgroundCssClass = "sctableBackground d-block";
                PnlEmailOTP.CssClass = "d-block zindex_1";
                lblOTPMsg.CssClass = "d-block text-success";
                lblOTPMsg.Text = "One Time Password (OTP) has been sent to your email address '" + sCustEmail + "'";

                //Timer 60 seconds
                int secondsLeft = Convert.ToInt32(obj.RetPolValue("OTP", "OTPCount"));
                hdnSecondsLeft.Value = obj.RetPolValue("OTP", "OTPCount");
                ScriptManager.RegisterStartupScript(this, GetType(), "startTimer", @"startTimer('" + secondsLeft + "');", true);
                //End
            }
            obj.StoreEvent("99999", "", "", "", "Successfully sent OTP to customer", Convert.ToString(Session["RefNo"]));

        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private bool checkEmailBlock(string sCustEMBlockTIME)
    {
        bool flag = false;
        int EmailBlkTimeDif = -1;
        try
        {
            if (!string.IsNullOrEmpty(sCustEMBlockTIME))
            {
                DateTime GNTIME = DateTime.ParseExact(sCustEMBlockTIME, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                DateTime CRTIME = DateTime.ParseExact(DateTime.Now.ToString("yyyyMMddHHmm"), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                EmailBlkTimeDif = (int)(CRTIME - GNTIME).TotalMinutes;
            }
            string sOTPValidMin = obj.RetPolValue("OTP", "AutoUnblock");
            int OTPValTime = Convert.ToInt32(sOTPValidMin);
            if (EmailBlkTimeDif > OTPValTime)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
        return flag;
    }

    protected void btnOTPVerify_Click(object sender, EventArgs e)
    {
        //otpAttempt.Value = (Convert.ToInt32(otpAttempt.Value) + 1).ToString(); // Convert.ToInt32(otpAttempt.Value) + 1;
        string isResenBtnEnable = string.Empty;
        int sOTPAttmtCount = Convert.ToInt32(obj.RetPolValue("OTP", "OTPAttmptCount"));

        sCustOTP = sCustEMOTPREFNO = sCustEMOTPEMAIL = sCustEMOTPGNDATE = sCustEMOTPGNTIME = sCustOTPATTEMPT = sCustOTPRESENDCOUNT = sCustOTPSTATUS = "";

        GetOTPVal();

        if ((txtOTP.Text.Equals("")))
        {
            isResenBtnEnable = hiddenresend.Value;
            if (isResenBtnEnable == "true")
            {
                hdnSecondsLeft.Value = "0";
                ScriptManager.RegisterStartupScript(this, GetType(), "startTimer", @"startTimer('0');", true);
            }

            lblOTPMsg.Text = "";
            lblOTPMsg.CssClass = "d-none";

            lblOTPAlert.CssClass = "d-block text-danger";
            lblOTPAlert.Text = "Please enter a valid OTP";
            txtOTP.Focus();

            #region test
            //if (btnResendOTP.CssClass.Contains("cs") == true)
            //{
            //    //btnResendOTP.CssClass = btnResendOTP.CssClass.Replace("disabled-button", "").Trim();
            //    //btnResendOTP.CssClass = btnResendOTP.CssClass.Replace("aspNetDisabled", "").Trim();
            //    btnResendOTP.CssClass = "btn btn-link h-auto pt-0";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "startTimer", @"startTimer('0');", true);
            //}

            //if (btnResendOTP.Enabled)
            //{
            //     ScriptManager.RegisterStartupScript(this, GetType(), "startTimer", @"startTimer('0');", true);
            //}

            //bool isButtonDisabled = btnResendOTP.Attributes["disabled"] == "disabled";
            //if (isButtonDisabled == false)
            //{
            //    btnResendOTP.CssClass = btnResendOTP.CssClass.Replace("disabled-button", "").Trim();
            //    btnResendOTP.CssClass="btn btn-link";                 
            //}

            //Button btnResendOTP = FindControl("btnResendOTP") as Button;
            //if (btnResendOTP != null)
            //{
            //    if (btnResendOTP.Attributes["disabled"] == "disabled")
            //    {
            //        btnResendOTP.CssClass = "btn btn-link";
            //    }
            //    else
            //    {
            //        btnResendOTP.CssClass = btnResendOTP.CssClass.Replace("disabled-button", "").Trim();
            //        btnResendOTP.CssClass = "btn btn-link";
            //    }
            //}

            ////Timer 60 seconds
            //hdnSecondsLeft.Value = obj.RetPolValue("OTP", "OTPCount");
            //int secondsLeft = Convert.ToInt32(obj.RetPolValue("OTP", "OTPCount"));
            //ScriptManager.RegisterStartupScript(this, GetType(), "startTimer", @"startTimer('" + secondsLeft + "');", true);
            #endregion
        }
        else if (sCustOTP != txtOTP.Text)
        {
            int sOTPAttmptCntVal = 0;
            sOTPAttmptCntVal = Convert.ToInt32(sCustOTPATTEMPT) + 1;

            //Session["OTPCount"] = Convert.ToInt64(Session["OTPCount"]) + 1;

            isResenBtnEnable = hiddenresend.Value;
            if (isResenBtnEnable == "true")
            {
                hdnSecondsLeft.Value = "0";
                ScriptManager.RegisterStartupScript(this, GetType(), "startTimer", @"startTimer('0');", true);
            }

            if (sOTPAttmptCntVal == (sOTPAttmtCount - 1))
            {
                lblOTPMsg.Text = "";
                lblOTPMsg.CssClass = "d-none";

                lblOTPAlert.CssClass = "d-block text-danger";
                lblOTPAlert.Text = "You have entered an Invalid OTP. Your next invalid attempt will redirect you to the homepage.";
                txtOTP.Focus();
            }
            else if (sOTPAttmptCntVal == sOTPAttmtCount)
            {
                lblOTPMsg.Text = "";
                lblOTPMsg.CssClass = "d-none";

                lblOTPAlert.CssClass = "d-none";
                lblOTPAlert.Text = "";

                ModalPopupEmailOTP.Hide();
                ModalPopupEmailOTP.BackgroundCssClass = "d-none";
                PnlEmailOTP.CssClass = "d-none";

                //block
                //BlockEmailAdd(txtEmailAddr.Text.Trim());
                DelDataFrmOTPTbl(txtEmailAddr.Text.Trim());
                UpdateEmailVStsEmpty();

                PopupEmailBlock.Show();
                PopupEmailBlock.BackgroundCssClass = "sctableBackground d-block";
                PnlEmailBlock.CssClass = "d-block zindex_1";
                lblBlockTxt.Text = "You have exceeded the maximum attempts for OTP verification. Please try again after some time.";
            }
            else if (sOTPAttmptCntVal < sOTPAttmtCount)
            {
                lblOTPMsg.Text = "";
                lblOTPMsg.CssClass = "d-none";

                lblOTPAlert.CssClass = "d-block text-danger";
                lblOTPAlert.Text = "You have entered an Invalid OTP.";
                txtOTP.Focus();
            }

            //Update OTP attempt count
            if (txtEmailAddr.Text.Trim() != "")
            {
                obj.ExecuteCommand("UPDATE OTPFOREMAIL SET OTPATTEMPT='" + sOTPAttmptCntVal + "' WHERE EMOTPREFNO='" + Convert.ToString(Session["RefNo"]) + "' AND EMOTPEMAIL='" + Session["OTPCustEmail"].ToString() + "' AND OTPSTATUS='Y'");
            }
            //End

            #region test
            ////Timer 60 seconds
            //hdnSecondsLeft.Value = obj.RetPolValue("OTP", "OTPCount");
            //int secondsLeft = Convert.ToInt32(obj.RetPolValue("OTP", "OTPCount"));
            //ScriptManager.RegisterStartupScript(this, GetType(), "startTimer", @"startTimer('" + secondsLeft + "');", true);

            //if (btnResendOTP.CssClass.Contains("h-auto pt-0") == false)
            //{
            //    //btnResendOTP.CssClass = btnResendOTP.CssClass.Replace("disabled-button", "").Trim();
            //    //btnResendOTP.CssClass = btnResendOTP.CssClass.Replace("aspNetDisabled", "").Trim();
            //    //btnResendOTP.CssClass = "btn btn-link";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "startTimer", @"startTimer('0');", true);
            //}
            #endregion
        }
        else if (checkOTPExp(sCustEMOTPGNTIME, sCustOTP, "E", Convert.ToString(Session["RefNo"])) == true)
        {
            isResenBtnEnable = hiddenresend.Value;
            if (isResenBtnEnable == "true")
            {
                hdnSecondsLeft.Value = "0";
                ScriptManager.RegisterStartupScript(this, GetType(), "startTimer", @"startTimer('0');", true);
            }

            lblOTPMsg.Text = "";
            lblOTPMsg.CssClass = "d-none";

            lblOTPAlert.CssClass = "d-block text-danger";
            lblOTPAlert.Text = "You have entered an Invalid OTP.";
            txtOTP.Focus();
        }
        else
        {
            UpdateEmailVerSts();
            ModalPopupEmailOTP.Hide();
            ModalPopupEmailOTP.BackgroundCssClass = "d-none";
            PnlEmailOTP.CssClass = "d-none";
            imgEmailVerfied.CssClass = "";
            btnVerify.CssClass = "d-none";
            txtEmailAddr.Enabled = false;
            btnUpdateEmail.CssClass = "btn btn-link d-block h-auto";
            lblAlert.Text = "Your email address has been successfully verified.";
            lblAlert.CssClass = "text-success font-weight-bold font-16";
        }
    }

    private void UpdateEmailVStsEmpty()
    {
        string fUpdate = string.Empty;
        try
        {
            obj.StoreEvent("99999", "", "", "", "Initiated to Update the email verification status as empty", Convert.ToString(Session["RefNo"]));
            if (Session["RefNo"] != null && Session["RefNo"].ToString().Length > 0)
            {
                fUpdate = "UPDATE TDAPPIND SET TDEMVERIFIEDSTS='' WHERE TEMPREF='" + Convert.ToString(Session["RefNo"]) + "'";
                obj.ExecuteCommand(fUpdate);

                obj.StoreEvent("99999", "", "", "", "Customer closed the OTP popup " + Convert.ToString(Session["RefNo"]), Convert.ToString(Session["RefNo"]));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #region Block
    private void BlockEmailAdd(string sEmailAddVal)
    {
        string fUpdate = string.Empty;
        try
        {
            obj.StoreEvent("99999", "", "", "", "Initiated to block the email address verification process", Convert.ToString(Session["RefNo"]));
            fUpdate = "UPDATE OTPFOREMAIL SET EMISBLOCKED='Y',EMBLOCKDATETIME='" + DateTime.Now.ToString("yyyyMMddHHmm") + "' WHERE EMOTPEMAIL='" + sEmailAddVal + "' AND OTPSTATUS='Y'";
            obj.ExecuteCommand(fUpdate);
            obj.StoreEvent("99999", "", "", "", "blocked the email address verification process", Convert.ToString(Session["RefNo"]));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnBlockOk_Click(object sender, EventArgs e)
    {
        try
        {
            PopupEmailBlock.Hide();
            PopupEmailBlock.BackgroundCssClass = "d-none";
            PnlEmailBlock.CssClass = "d-none";
            ClearSessionAndCache();
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }
    #endregion


    #region Update
    protected void btnUpdateEmail_Click(object sender, EventArgs e)
    {
        try
        {
            ModalUpdEmailConf.Show();
            ModalUpdEmailConf.BackgroundCssClass = "sctableBackground d-block";
            PnlUpdEmailConf.CssClass = "d-block zindex_1";
            Session["EMUpdate"] = "Y";
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnECUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            ModalUpdEmailConf.Hide();
            ModalUpdEmailConf.BackgroundCssClass = "d-none";
            PnlUpdEmailConf.CssClass = "d-none";
            imgEmailVerfied.CssClass = "d-none";

            txtEmailAddr.Enabled = true;
            btnVerify.CssClass = "button d-block";
            btnUpdateEmail.CssClass = "d-none";
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnECCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ModalUpdEmailConf.Hide();
            ModalUpdEmailConf.BackgroundCssClass = "d-none";
            PnlUpdEmailConf.CssClass = "d-none";
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }
    #endregion

    protected void btnResendOTP_Click(object sender, EventArgs e)
    {
        try
        {
            //otpAttempt.Value = "1";
            otpResendAttempt.Value = Convert.ToString(Convert.ToInt32(otpResendAttempt.Value) + 1);
            int sOTPResendAttCnt = Convert.ToInt32(obj.RetPolValue("OTP", "OTPResendAttCount"));

            //Get OTP Attempt Count
            DataTable dtOTPResendCnt = null;
            string fGetOTPResendCnt = string.Empty;
            string sOTPResendCnt = string.Empty;
            int sOTPResendCntVal = 0;
            fGetOTPResendCnt = "SELECT * FROM OTPFOREMAIL WHERE EMOTPREFNO='" + Convert.ToString(Session["RefNo"]) + "' AND EMOTPEMAIL='" + txtEmailAddr.Text.Trim() + "' AND OTPSTATUS='Y'";
            dtOTPResendCnt = obj.ExecuteData(fGetOTPResendCnt);
            if (dtOTPResendCnt != null && dtOTPResendCnt.Rows.Count > 0)
            {
                sOTPResendCnt = obj.NullToSpace(dtOTPResendCnt.Rows[0]["OTPRESENDCOUNT"]);
                sOTPResendCntVal = Convert.ToInt32(sOTPResendCnt) + 1;
            }
            //End            

            if ((sOTPResendCntVal < sOTPResendAttCnt) || (sOTPResendCntVal <= sOTPResendAttCnt))
            {
                Session["OTPRESEND"] = "Y";
                if (SendOTPTOCUST(Session["OTPCustEmail"].ToString()) == true)
                {
                    ModalPopupEmailOTP.Show();
                    ModalPopupEmailOTP.BackgroundCssClass = "sctableBackground d-block";
                    PnlEmailOTP.CssClass = "d-block zindex_1";

                    if (sOTPResendCntVal == sOTPResendAttCnt)
                    {
                        lblOTPMsg.Text = "";
                        lblOTPMsg.CssClass = "d-none";
                        lblOTPAlert.CssClass = "d-block text-danger";
                        lblOTPAlert.Text = "By clicking the RESEND button again, you will be redirected to the homepage. Please refrain from multiple attempts to resend.";
                    }
                    else
                    {
                        lblOTPAlert.Text = "";
                        lblOTPAlert.CssClass = "d-none";
                        lblOTPMsg.CssClass = "d-block bodytext text-success";
                        lblOTPMsg.Text = "One Time Password (OTP) has been sent to your email address '" + Session["OTPCustEmail"] + "'";
                    }

                    //Timer 60 seconds
                    int secondsLeft = Convert.ToInt32(obj.RetPolValue("OTP", "OTPCount"));
                    hdnSecondsLeft.Value = obj.RetPolValue("OTP", "OTPCount");
                    ScriptManager.RegisterStartupScript(this, GetType(), "startTimer", @"startTimer('" + secondsLeft + "');", true);
                    //End
                }
            }
            else if (sOTPResendCntVal > sOTPResendAttCnt)
            {
                //BlockEmailAdd(txtEmailAddr.Text.Trim());

                DelDataFrmOTPTbl(txtEmailAddr.Text.Trim());
                UpdateEmailVStsEmpty();

                ModalPopupEmailOTP.Hide();
                ModalPopupEmailOTP.BackgroundCssClass = "d-none";
                PnlEmailOTP.CssClass = "d-none";
                lblOTPMsg.Text = "";
                lblOTPMsg.CssClass = "d-none";
                lblOTPAlert.Text = "";
                lblOTPAlert.CssClass = "d-none";
                timer.Attributes["class"] = "d-none";

                PopupEmailBlock.Show();
                PopupEmailBlock.BackgroundCssClass = "sctableBackground d-block";
                PnlEmailBlock.CssClass = "d-block zindex_1";
                lblBlockTxt.Text = "You have exceeded the maximum limit for OTP resend requests. Please try again after some time.";
            }

            //Update OTP attempt count
            if (txtEmailAddr.Text.Trim() != "")
            {
                obj.ExecuteCommand("UPDATE OTPFOREMAIL SET OTPRESENDCOUNT='" + sOTPResendCntVal + "' WHERE EMOTPREFNO='" + Convert.ToString(Session["RefNo"]) + "' AND EMOTPEMAIL='" + txtEmailAddr.Text.Trim() + "' AND OTPSTATUS='Y'");
            }
            //End
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private void DelDataFrmOTPTbl(string sEmailAddVal)
    {
        string fDelData = string.Empty;
        try
        {
            if (sEmailAddVal != null && sEmailAddVal != "")
            {
                obj.StoreEvent("99999", "", "", "", "Initiated to delete the email address from OTP table", Convert.ToString(Session["RefNo"]));
                fDelData = "DELETE FROM OTPFOREMAIL WHERE EMOTPREFNO='" + Convert.ToString(Session["RefNo"]) + "' AND EMOTPEMAIL='" + sEmailAddVal + "' AND OTPSTATUS='Y'";
                obj.ExecuteCommand(fDelData);
                obj.StoreEvent("99999", "", "", "", "successfully deleted the email address from OTP table for email " + sEmailAddVal, Convert.ToString(Session["RefNo"]));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool SendOTPTOCUST(string sCustEmailVal)
    {
        bool SendOTPSts = false;
        try
        {
            string Content = string.Empty, subject = string.Empty, sLogo = string.Empty, MsgBody = string.Empty, sHeader = string.Empty, sFooter = string.Empty, sRefNo = string.Empty, sOTPVal = string.Empty;
            string pName = obj.RetPolValue("UBI", "PRODUCTNAME");

            string sOTPVALIDMIN = obj.RetPolValue("OTP", "OTPValTim");

            sOTPVal = GenerateRandomOTP();

            sRefNo = Convert.ToString(Session["RefNo"]);

            if (Convert.ToString(Session["OTPRESEND"]) == "Y")
            {
                UpdateEmailOTP(sOTPVal, sRefNo, sCustEmailVal, 0, 0);
            }
            else if (Convert.ToString(Session["EMUpdate"]) == "Y")
            {
                UpdateOTPByTempRefNo(sOTPVal, sRefNo, sCustEmailVal, 0, 0);
            }
            else if (Convert.ToString(Session["EmailUnblock"]) == "Y")
            {
                UpdateBlockEmailOTP(sOTPVal, sCustEmailVal);
            }
            else
            {
                SetEmailOTP(sOTPVal, sRefNo, sCustEmailVal, 0, 0);
            }

            Session["OTPRESEND"] = "";
            Session["EMUpdate"] = "";

            Content = obj.GetContent("OTP");
            Content = Content.Replace("(OTPVAL)", sOTPVal);
            Content = Content.Replace("(OTPVALIDMIN)", sOTPVALIDMIN);
            Content = Content.Replace("(PRODUCTNAME)", pName);

            subject = "UBI(UK) - OTP for Email Address Verification";

            sLogo = obj.RetPolValue("MAIL", "LOGO");
            sHeader = obj.RetPolValue("MAIL", "HEADER");
            sFooter = obj.RetPolValue("MAIL", "FOOTER");
            sHeader = sHeader.Replace("UBIUKLOGO", sLogo);
            MsgBody = sHeader + Content + sFooter;

            //SendOTPSts = true;  test purpose

            if (obj.SendEmailMessage(sCustEmailVal, subject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer))
            {
                SendOTPSts = true;
                obj.StoreEvent("99999", "", "", "", "OTP for Email Address Verification sent successfully to Customer", Convert.ToString(Session["RefNo"]));
                System.Threading.Thread.Sleep(5000);
            }
            else
            {
                obj.StoreEvent("99999", "", "", "", "OTP for Email Address Verification failed to send the Customer", Convert.ToString(Session["RefNo"]));
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("99999", "", "", "", "OTP for Email Address process has been failed", Convert.ToString(Session["RefNo"]));
            throw ex;
        }
        return SendOTPSts;
    }

    private void UpdateBlockEmailOTP(string sOTPVal, string sCustEmailVal)
    {
        string fUpdate = string.Empty;
        try
        {
            obj.StoreEvent("99999", "", "", "", "Initiated Update OTP for Email Address Verification process", Convert.ToString(Session["RefNo"]));
            fUpdate = "UPDATE OTPFOREMAIL SET OTP='" + sOTPVal + "',OTPATTEMPT=0,OTPRESENDCOUNT=0,EMOTPGNDATE='" + DateTime.Now.ToString("yyyyMMdd") + "',EMOTPGNTIME='" + DateTime.Now.ToString("yyyyMMddHHmm") + "',EMISBLOCKED='N' WHERE EMOTPEMAIL='" + sCustEmailVal + "' AND OTPSTATUS='Y'";
            obj.ExecuteCommand(fUpdate);
            obj.StoreEvent("99999", "", "", "", "Completed Update OTP for Email Address Verification process", Convert.ToString(Session["RefNo"]));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SetEmailOTP(string sOTP, string sOTPRefNo, string sOTPCustEmail, int OTPAtt, int OTPResendCnt)
    {
        try
        {
            obj.StoreEvent("99999", "", "", "", "Initiated OTP for Email Address Verification process", Convert.ToString(Session["RefNo"]));
            string fSelect = string.Empty;
            fSelect = "INSERT INTO OTPFOREMAIL VALUES('" + sOTP + "','" + sOTPRefNo + "', '" + sOTPCustEmail + "', '" + DateTime.Now.ToString("yyyyMMdd") + "', '" + DateTime.Now.ToString("yyyyMMddHHmm") + "',0,0,'Y','','','','','','NC')";
            obj.ExecuteCommand(fSelect);
            obj.StoreEvent("99999", "", "", "", "Completed OTP for Email Address Verification process", Convert.ToString(Session["RefNo"]));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void UpdateEmailOTP(string sOTP, string sOTPRefNo, string sOTPCustEmail, int OTPAtt, int OTPResendCnt)
    {
        string fUpdate = string.Empty;
        try
        {
            obj.StoreEvent("99999", "", "", "", "Initiated Update OTP for Email Address Verification process", Convert.ToString(Session["RefNo"]));
            fUpdate = "UPDATE OTPFOREMAIL SET OTP='" + sOTP + "',OTPATTEMPT=0,OTPRESENDCOUNT=0,EMOTPGNDATE='" + DateTime.Now.ToString("yyyyMMdd") + "',EMOTPGNTIME='" + DateTime.Now.ToString("yyyyMMddHHmm") + "',EMOTPAPP='NC' WHERE EMOTPREFNO='" + sOTPRefNo + "' AND EMOTPEMAIL='" + sOTPCustEmail + "' AND OTPSTATUS='Y'";
            obj.ExecuteCommand(fUpdate);
            obj.StoreEvent("99999", "", "", "", "Completed Update OTP for Email Address Verification process", Convert.ToString(Session["RefNo"]));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void UpdateOTPByTempRefNo(string sOTP, string sOTPRefNo, string sOTPCustEmail, int OTPAtt, int OTPResendCnt)
    {
        string fUpdate = string.Empty;
        try
        {
            obj.StoreEvent("99999", "", "", "", "Initiated Update OTP for temp reference number Verification process", Convert.ToString(Session["RefNo"]));
            fUpdate = "UPDATE OTPFOREMAIL SET OTP='" + sOTP + "',OTPATTEMPT=0,OTPRESENDCOUNT=0,EMOTPEMAIL='" + sOTPCustEmail + "',EMVERIFIED='',EMOTPVERIFIEDTIME='',EMOTPGNDATE='" + DateTime.Now.ToString("yyyyMMdd") + "',EMOTPGNTIME='" + DateTime.Now.ToString("yyyyMMddHHmm") + "',EMOTPAPP='NC' WHERE EMOTPREFNO='" + sOTPRefNo + "' AND OTPSTATUS='Y'";
            obj.ExecuteCommand(fUpdate);
            obj.StoreEvent("99999", "", "", "", "Completed Update OTP for temp reference number Verification process", Convert.ToString(Session["RefNo"]));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static string GenerateRandomOTP()
    {
        Random generator = new Random();
        String r = generator.Next(0, 1000000).ToString("D6");
        if (r.Distinct().Count() == 1)
        {
            r = GenerateRandomOTP();
        }
        return r;
    }

    private void UpdateEmailVerSts()
    {
        string fUpdate = string.Empty;
        try
        {
            obj.StoreEvent("99999", "", "", "", "Initiated to Update the email verification status", Convert.ToString(Session["RefNo"]));
            fUpdate = "UPDATE OTPFOREMAIL SET EMVERIFIED='Y',EMOTPVERIFIEDTIME='" + DateTime.Now.ToString("yyyyMMddHHmm") + "' WHERE EMOTPREFNO='" + Convert.ToString(Session["RefNo"]) + "' AND EMOTPEMAIL='" + Session["OTPCustEmail"].ToString() + "' AND OTPSTATUS='Y'";
            obj.ExecuteCommand(fUpdate);
            Session["EMVerifiedSts"] = "Y";
            obj.StoreEvent("99999", "", "", "", "Successfully updated the email verification status", Convert.ToString(Session["RefNo"]));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool checkOTPExp(string OTPGNTIME, string OTP, string OTPFOR, string sTemRefNo)
    {
        bool flag = true;
        int OTPTimeDif = -1;
        try
        {
            if (!string.IsNullOrEmpty(OTPGNTIME))
            {
                DateTime GNTIME = DateTime.ParseExact(OTPGNTIME, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                DateTime CRTIME = DateTime.ParseExact(DateTime.Now.ToString("yyyyMMddHHmm"), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                OTPTimeDif = (int)(CRTIME - GNTIME).TotalMinutes;
            }
            string sOTPValidMin = obj.RetPolValue("OTP", "OTPValTim");
            int OTPValTime = Convert.ToInt32(sOTPValidMin);
            if (string.IsNullOrEmpty(OTP) || OTPTimeDif < 0 || OTPTimeDif > OTPValTime)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
        }
        catch (Exception ex)
        {
            // Handle exception if necessary
        }
        return flag;
    }

    private void GetOTPVal()
    {
        try
        {
            DataRow dr;
            string fSelect = "SELECT * FROM OTPFOREMAIL WHERE EMOTPREFNO='" + Convert.ToString(Session["RefNo"]) + "' AND EMOTPEMAIL='" + Session["OTPCustEmail"].ToString() + "' AND OTPSTATUS='Y'";
            DataTable dt = obj.ExecuteData(fSelect);
            if (dt != null && dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];

                sCustOTP = obj.NullToSpace(dr["OTP"]);
                sCustEMOTPREFNO = obj.NullToSpace(dr["EMOTPREFNO"]);
                sCustEMOTPEMAIL = obj.NullToSpace(dr["EMOTPEMAIL"]);
                sCustEMOTPGNDATE = obj.NullToSpace(dr["EMOTPGNDATE"]);
                sCustEMOTPGNTIME = obj.NullToSpace(dr["EMOTPGNTIME"]);
                sCustOTPATTEMPT = obj.NullToSpace(dr["OTPATTEMPT"]);
                sCustOTPRESENDCOUNT = obj.NullToSpace(dr["OTPRESENDCOUNT"]);
                sCustOTPSTATUS = obj.NullToSpace(dr["OTPSTATUS"]);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lnkClose_Click(object sender, EventArgs e)
    {
        try
        {
            obj.StoreEvent("99999", "", "", "", "Initiated to close the OTP popup", Convert.ToString(Session["RefNo"]));

            DelDataFrmOTPTbl(txtEmailAddr.Text.Trim());
            UpdateEmailVStsEmpty();

            lblOTPMsg.Text = "";
            lblOTPMsg.CssClass = "d-none";

            lblOTPAlert.CssClass = "d-none";
            lblOTPAlert.Text = "";

            ModalPopupEmailOTP.Hide();
            ModalPopupEmailOTP.BackgroundCssClass = "d-none";
            PnlEmailOTP.CssClass = "d-none";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    //OCR Integration
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUploader.HasFile)
            {
                if (FileUploader.PostedFiles.Count > 1)
                {
                    lblUploadError.Text = "Only one file is permitted per upload. Please remove the additional file(s) and try again.";
                    return;
                }

                string fileName = Path.GetFileName(FileUploader.FileName);
                string extension = Path.GetExtension(fileName).ToLower();

                // Validate file type
                if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".pdf")
                {
                    lblUploadError.Text = "File format not supported. Please upload a valid file.";
                    return;
                }

                string contentType = FileUploader.PostedFile.ContentType.ToLower();
                if (!(
                    contentType == "image/jpeg" ||
                    contentType == "image/png" ||
                    contentType == "application/pdf"
                ))
                {
                    lblUploadError.Text = "File format not supported. Please upload a valid file.";
                    return;
                }

                // Validate file size (2 MB max)
                if (FileUploader.PostedFile.ContentLength > 2 * 1024 * 1024)
                {
                    lblUploadError.Text = "File size cannot be greater than 2 MB.";
                    return;
                }


                // Convert uploaded file to Base64 directly from InputStream
                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    FileUploader.PostedFile.InputStream.CopyTo(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }
                string imageBase64 = Convert.ToBase64String(fileBytes);
                string sNewGuid = Guid.NewGuid().ToString();

                // Build JSON payload
                string jsonPayload = string.Format(@"
                    {{
                        ""processParam"": {{
                            ""scenario"": ""Ocr"",
                            ""pdfPagesLimit"": 2,
                            ""dateFormat"" : ""dd/MM/yyyy""
                        }},
                        ""List"": [
                            {{ ""ImageData"": {{ ""image"": ""{0}"" }} }}
                        ]
                    }}", imageBase64);

                try
                {
                    // Create HttpWebRequest
                    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://20.68.211.53:9091/api/process"); //From Local QA
                    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:9091/api/process"); //in QA
                    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8090/api/process");  //UAT UBI

                    string sRegulaAPIEndPoint = obj.RetPolValue("REGULA", "APIENDPOINT");
                    if (string.IsNullOrWhiteSpace(sRegulaAPIEndPoint))
                    {
                        obj.StoreEvent("88888", "", "", "", "REGULA API endpoint is not configured.", Convert.ToString(Session["RefNo"]));
                    }
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sRegulaAPIEndPoint);

                    request.Headers.Add("X-RequestID", sNewGuid);
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.UserAgent = "UBI-Regula-POC/1.0";

                    // Write JSON to request body
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(jsonPayload);
                        streamWriter.Flush();
                    }

                    // Get response
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var streamReader = new StreamReader(response.GetResponseStream()))
                        {
                            string jsonResponse = streamReader.ReadToEnd();

                            //string jsonResponse = "";

                            // Deserialize JSON
                            dynamic result = JsonConvert.DeserializeObject(jsonResponse);

                            Dictionary<string, string> ocrFields = new Dictionary<string, string>();

                            // Document metadata
                            int? documentTypeId = null;
                            string CountryCode = null;
                            int? faceCount = null;

                            if (result != null && result.ContainerList != null && result.ContainerList.List != null)
                            {
                                foreach (var container in result.ContainerList.List)
                                {
                                    if (container != null)
                                    {
                                        // Face count
                                        if (container.FaceDetection != null)
                                        {
                                            faceCount = container.FaceDetection.Count;
                                        }

                                        // Candidate FDS info
                                        if (container.OneCandidate != null && container.OneCandidate.FDSIDList != null)
                                        {
                                            var fds = container.OneCandidate.FDSIDList;

                                            documentTypeId = fds.dType;
                                            CountryCode = fds.ICAOCode;
                                        }

                                        // OCR fields
                                        if (container.Text != null && container.Text.fieldList != null)
                                        {
                                            foreach (var field in container.Text.fieldList)
                                            {
                                                if (field != null)
                                                {
                                                    string name = field.fieldName;
                                                    string value = field.value;

                                                    if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(value) && !ocrFields.ContainsKey(name))
                                                    {
                                                        ocrFields.Add(name, value);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (((ocrFields.Count) > 0) && ((!string.IsNullOrEmpty(CountryCode)) && (CountryCode.Length == 3)) && ((documentTypeId == 11) || ((documentTypeId == 49) && (CountryCode.ToUpper() == "GBR"))))
                            {

                                txtFirstNm.Text = string.Empty;
                                txtMiddleNm.Text = string.Empty;
                                txtSurNm.Text = string.Empty;
                                rblGender.ClearSelection();
                                txtPlaceOfBirth.Text = string.Empty;
                                CalDOB.SelDate = "00/00/0";


                                //First Name
                                if (ocrFields.ContainsKey("Given Names") && ocrFields["Given Names"].Trim().Length > 0)
                                {
                                    string sGivenNames = ocrFields["Given Names"].Trim().Length > 30 ? ocrFields["Given Names"].Trim().Substring(0, 30) : ocrFields["Given Names"].Trim();

                                    if (ValidateOCRFields_IsAlpha(sGivenNames))
                                    {
                                        txtFirstNm.Text = sGivenNames;
                                    }
                                }

                                //Middle Name
                                if (ocrFields.ContainsKey("Middle Name") && ocrFields["Middle Name"].Trim().Length > 0)
                                {
                                    string sMiddleNames = ocrFields["Middle Name"].Trim().Length > 20 ? ocrFields["Middle Name"].Trim().Substring(0, 20) : ocrFields["Middle Name"].Trim();

                                    if (ValidateOCRFields_IsAlpha(sMiddleNames))
                                    {
                                        txtMiddleNm.Text = sMiddleNames;
                                    }
                                }

                                //Surname
                                if (ocrFields.ContainsKey("Surname") && ocrFields["Surname"].Trim().Length > 0)
                                {
                                    string sSurNames = ocrFields["Surname"].Trim().Length > 20 ? ocrFields["Surname"].Trim().Substring(0, 20) : ocrFields["Surname"].Trim();

                                    if (ValidateOCRFields_IsAlpha(sSurNames))
                                    {
                                        txtSurNm.Text = sSurNames;
                                    }
                                }

                                //Gender
                                if (ocrFields.ContainsKey("Sex") && ocrFields["Sex"].Trim().Length > 0)
                                {
                                    if (ocrFields["Sex"].ToUpper() == "M")
                                    {
                                        rblGender.SelectedIndex = 0;
                                    }
                                    else if (ocrFields["Sex"].ToUpper() == "F")
                                    {
                                        rblGender.SelectedIndex = 1;
                                    }
                                }

                                //Place of Birth
                                if (ocrFields.ContainsKey("Place of Birth") && ocrFields["Place of Birth"].Trim().Length > 0)
                                {
                                    string sPOB = ocrFields["Place of Birth"].Trim().Length > 30 ? ocrFields["Place of Birth"].Trim().Substring(0, 30) : ocrFields["Place of Birth"].Trim();

                                    if (ValidateOCRFields_IsAlpha(sPOB))
                                    {
                                        txtPlaceOfBirth.Text = sPOB;
                                    }
                                }

                                //Date of Birth
                                if (ocrFields.ContainsKey("Date of Birth") && ocrFields["Date of Birth"].Trim().Length > 0)
                                {
                                    string sDL_DOB = ocrFields["Date of Birth"];

                                    if (IsDate(sDL_DOB))
                                    {
                                        if ((CalAge(sDL_DOB) >= 18) && (CalAge(sDL_DOB) <= 108))
                                        {
                                            try
                                            {
                                                CalDOB.SelDate = sDL_DOB;
                                            }
                                            catch (Exception) { CalDOB.SelDate = "00/00/0"; }
                                        }
                                    }
                                }

                                //Passport
                                if (documentTypeId == 11)
                                {
                                    calDOI.SelDate = "00/00/0";
                                    calDOE.SelDate = "00/00/0";

                                    rblIdentity.SelectedIndex = 1;
                                    rblIdentity_SelectedIndexChanged(sender, e);

                                    if (CountryCode.ToUpper() == "GBR")
                                    {
                                        txtUKPassportL1.Text = string.Empty;
                                        txtUKPassportL2.Text = string.Empty;
                                        txtUKPassportL3.Text = string.Empty;
                                        txtUKPassport1.Text = string.Empty;
                                        txtUKPassport2.Text = string.Empty;
                                        txtUKPassport3.Text = string.Empty;
                                        txtUKPassport4.Text = string.Empty;
                                        txtUKPassport5.Text = string.Empty;
                                        txtUKPassport6.Text = string.Empty;
                                        txtUKPassport7.Text = string.Empty;

                                        // UK - Passport
                                        rblPassport.SelectedIndex = 0;
                                        rblPassport_SelectedIndexChanged(sender, e);

                                        // Split MRZ lines
                                        string mrz = ocrFields.ContainsKey("MRZ Strings") ? ocrFields["MRZ Strings"].Trim() : "";
                                        string[] lines = mrz.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
                                        string line1;
                                        string line2;
                                        line1 = lines.Length > 0 ? lines[0] : string.Empty;
                                        line2 = lines.Length > 1 ? lines[1] : string.Empty;

                                        if ((ValidateOCRFields_IsAlphaNumeric(line1)) && (ValidateOCRFields_IsAlphaNumeric(line1)))
                                        {
                                            UKPassportMrzResult(line1, line2);
                                        }

                                    }
                                    else
                                    {
                                        ddlPsPrtIsCntry.SelectedValue = "-1";

                                        txtIntPassportL1.Text = string.Empty;
                                        txtIntPassportL2.Text = string.Empty;
                                        txtIntPassportL3.Text = string.Empty;
                                        txtIntPassport1.Text = string.Empty;
                                        txtIntPassport2.Text = string.Empty;
                                        txtIntPassport3.Text = string.Empty;
                                        txtIntPassport4.Text = string.Empty;
                                        txtIntPassport5.Text = string.Empty;
                                        txtIntPassport6.Text = string.Empty;
                                        txtIntPassport7.Text = string.Empty;
                                        txtIntPassport8.Text = string.Empty;
                                        txtIntPassport9.Text = string.Empty;

                                        // NON UK - Passport
                                        rblPassport.SelectedIndex = 1;
                                        rblPassport_SelectedIndexChanged(sender, e);

                                        // Split MRZ lines
                                        string mrz = ocrFields.ContainsKey("MRZ Strings") ? ocrFields["MRZ Strings"].Trim() : "";
                                        string[] lines = mrz.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
                                        string line1;
                                        string line2;
                                        line1 = lines.Length > 0 ? lines[0] : string.Empty;
                                        line2 = lines.Length > 1 ? lines[1] : string.Empty;

                                        if ((ValidateOCRFields_IsAlphaNumeric(line1)) && (ValidateOCRFields_IsAlphaNumeric(line1)))
                                        {
                                            NONUKPassportMrzResult(line1, line2);
                                        }

                                        //Country of Issue
                                        string issuingState = ocrFields.ContainsKey("Issuing State Name") ? ocrFields["Issuing State Name"].Trim().ToUpper() : "";

                                        if (!string.IsNullOrEmpty(issuingState) &&
                                            ddlPsPrtIsCntry.Items.FindByValue(issuingState) != null)
                                        {
                                            ddlPsPrtIsCntry.SelectedValue = issuingState;
                                        }

                                    }

                                    //Date of Issue
                                    if (ocrFields.ContainsKey("Date of Issue") && ocrFields["Date of Issue"].Trim().Length > 0)
                                    {
                                        string sDL_DOI = ocrFields["Date of Issue"];
                                        if (IsDate(sDL_DOI))
                                        {
                                            DateTime dt;
                                            dt = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
                                            if (DateTime.Parse(sDL_DOI) <= dt)
                                            {
                                                try
                                                {
                                                    calDOI.SelDate = sDL_DOI;
                                                }
                                                catch (Exception) { calDOI.SelDate = "00/00/0"; }
                                            }
                                            else
                                            { calDOI.SelDate = "00/00/0"; }

                                        }
                                    }

                                    //Date of Expiry
                                    if (ocrFields.ContainsKey("Date of Expiry") && ocrFields["Date of Expiry"].Trim().Length > 0)
                                    {
                                        string sDL_DOE = ocrFields["Date of Expiry"];
                                        if (IsDate(sDL_DOE))
                                        {
                                            DateTime dt;
                                            dt = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
                                            if (DateTime.Parse(sDL_DOE) >= dt)
                                            {
                                                try
                                                {
                                                    calDOE.SelDate = sDL_DOE;
                                                }
                                                catch (Exception) { calDOE.SelDate = "00/00/0"; }
                                            }
                                            else
                                            { calDOE.SelDate = "00/00/0"; }

                                        }
                                    }

                                }
                                else if (documentTypeId == 49)
                                {
                                    txtDRLNo1.Text = string.Empty;
                                    txtDRLNo2.Text = string.Empty;
                                    txtDRLNo3.Text = string.Empty;
                                    txtDVLCPcode.Text = string.Empty;
                                    calDVLCDOE.SelDate = "00/00/0";

                                    rblIdentity.SelectedIndex = 0;
                                    rblIdentity_SelectedIndexChanged(sender, e);

                                    //Only UK - Driving License with Photo type is allowed
                                    if (CountryCode.ToUpper() == "GBR")
                                    {
                                        //Check for Photo type
                                        if (faceCount > 0)
                                        {
                                            rbldltype.SelectedIndex = 0;

                                            //Document Number
                                            string sDocNumber = ocrFields.ContainsKey("Document Number") ? ocrFields["Document Number"] : "";

                                            if (ValidateOCRFields_IsAlphaNumeric(sDocNumber))
                                            {
                                                UKDLResult(sDocNumber);
                                            }

                                            //Post Code
                                            if (ocrFields.ContainsKey("Address Postal Code") && ocrFields["Address Postal Code"].Trim().Length > 0)
                                            {
                                                string sDVLPcode = ocrFields["Address Postal Code"].Trim().Length > 10 ? ocrFields["Address Postal Code"].Trim().Substring(0, 10) : ocrFields["Address Postal Code"].Trim();

                                                if (ValidateOCRFields_IsAlphaNumeric(sDVLPcode))
                                                {
                                                    txtDVLCPcode.Text = sDVLPcode;
                                                }
                                            }

                                            //Date of Expiry
                                            if (ocrFields.ContainsKey("Date of Expiry") && ocrFields["Date of Expiry"].Trim().Length > 0)
                                            {
                                                string sDL_DOE = ocrFields["Date of Expiry"];
                                                if (IsDate(sDL_DOE))
                                                {
                                                    DateTime dt;
                                                    dt = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
                                                    if (DateTime.Parse(sDL_DOE) >= dt)
                                                    {
                                                        try
                                                        {
                                                            calDVLCDOE.SelDate = sDL_DOE;
                                                        }
                                                        catch (Exception) { calDVLCDOE.SelDate = "00/00/0"; }
                                                    }
                                                    else
                                                    { calDVLCDOE.SelDate = "00/00/0"; }

                                                }
                                            }
                                        }
                                    }
                                }

                                lblUploadInfo.Text = "The uploaded identity document has been processed successfully. Please review the auto-filled details below and amend if necessary.";
                            }

                            else
                            {
                                lblUploadError.Text = "The uploaded document type is not supported. Please try again with any of the supported identity documents.";
                                obj.StoreEvent("88888", "", "", "", obj.replaceSplChr("Regula Upload Error: Identity document not supported, RequestID :" + sNewGuid), Convert.ToString(Session["RefNo"]));
                            }

                        }
                    }
                }

                catch (WebException ex)
                {
                    if (ex.Response != null)
                    {
                        using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                        {
                            string errorResponse = reader.ReadToEnd();
                            dynamic err = JsonConvert.DeserializeObject(errorResponse);
                            //Console.WriteLine($"Error Code: {err.code}");
                            //Console.WriteLine($"Message: {err.msg}");
                            //Console.WriteLine($"Server Time: {err.metadata.serverTime}");
                            lblUploadError.Text = "An unexpected error occurred while processing your document. Please try again or enter your details manually if the issue persists.";
                            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr("Regula API Error Response: " + err.msg + ", RequestID :" + sNewGuid), Convert.ToString(Session["RefNo"]));
                        }
                    }
                    else
                    {
                        lblUploadError.Text = "An unexpected error occurred while processing your document. Please try again or enter your details manually if the issue persists.";
                        obj.StoreEvent("88888", "", "", "", obj.replaceSplChr("Regula API Error: " + ex.Message + ", RequestID :" + sNewGuid), Convert.ToString(Session["RefNo"]));
                    }
                }

                catch (Exception ex)
                {
                    lblUploadError.Text = "An unexpected error occurred while processing your document. Please try again or enter your details manually if the issue persists.";
                    obj.StoreEvent("88888", "", "", "", obj.replaceSplChr("Regula API Error: " + ex.Message + ", RequestID :" + sNewGuid), Convert.ToString(Session["RefNo"]));
                }
            }
            else
            {
                lblUploadError.Text = "Please upload your identity document to proceed.";
                return;
            }

        }
        catch (Exception ex)
        {
            lblUploadError.Text = "An unexpected error occurred while processing your document. Please try again or enter your details manually if the issue persists.";
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
        }
    }

    public bool ValidateOCRFields_IsAlpha(string input)
    {
        bool isValid = false;

        try
        {
            if (input != null)
            {
                input = input.Replace(" ", "").Trim();
                isValid = Regex.IsMatch(input, @"^[A-Za-z./-]+$");
            }
        }
        catch (Exception ex) { }

        return isValid;
    }

    public bool ValidateOCRFields_IsAlphaNumeric(string input)
    {
        bool isValid = false;

        try
        {
            if (input != null)
            {
                input = input.Replace(" ", "").Trim();
                isValid = Regex.IsMatch(input, @"^[A-Za-z0-9./\-_<]+$");
            }
        }
        catch (Exception ex) { }

        return isValid;
    }

    public void UKDLResult(string lnumber)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(lnumber))
            {
                lnumber = lnumber.Trim();
                if (lnumber.Length >= 16)
                {
                    txtDRLNo1.Text = lnumber.Substring(0, 5);
                    txtDRLNo2.Text = lnumber.Substring(5, 6);
                    txtDRLNo3.Text = lnumber.Substring(11, 5);
                }
            }
        }
        catch (Exception ex) { }
    }

    public void UKPassportMrzResult(string line1, string line2)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(line1) || !string.IsNullOrWhiteSpace(line2))
            {
                line1 = line1.Trim();
                line2 = line2.Trim();
                if (line1.Length >= 44)
                {
                    txtUKPassportL1.Text = line1.Substring(0, 2);
                    txtUKPassportL2.Text = line1.Substring(2, 3);
                    txtUKPassportL3.Text = line1.Substring(5, 39);
                }

                if (line2.Length >= 44)
                {
                    int index = 0;

                    txtUKPassport1.Text = line2.Substring(index, 10); index += 10;
                    txtUKPassport2.Text = line2.Substring(index, 3); index += 3;
                    txtUKPassport3.Text = line2.Substring(index, 7); index += 7;
                    txtUKPassport4.Text = line2.Substring(index, 1); index += 1;
                    txtUKPassport5.Text = line2.Substring(index, 7); index += 7;
                    txtUKPassport6.Text = line2.Substring(index, 14); index += 14;
                    txtUKPassport7.Text = line2.Substring(index, 2); index += 2;
                }
            }
        }
        catch (Exception ex) { }
    }

    public void NONUKPassportMrzResult(string line1, string line2)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(line1) || !string.IsNullOrWhiteSpace(line2))
            {
                line1 = line1.Trim();
                line2 = line2.Trim();
                if (line1.Length >= 44)
                {
                    txtIntPassportL1.Text = line1.Substring(0, 2);
                    txtIntPassportL2.Text = line1.Substring(2, 3);
                    txtIntPassportL3.Text = line1.Substring(5, 39);
                }

                if (line2.Length >= 44)
                {
                    int index = 0;

                    txtIntPassport1.Text = line2.Substring(index, 9); index += 9;
                    txtIntPassport2.Text = line2.Substring(index, 1); index += 1;
                    txtIntPassport3.Text = line2.Substring(index, 3); index += 3;
                    txtIntPassport4.Text = line2.Substring(index, 7); index += 7;
                    txtIntPassport5.Text = line2.Substring(index, 1); index += 1;
                    txtIntPassport6.Text = line2.Substring(index, 7); index += 7;
                    txtIntPassport7.Text = line2.Substring(index, 14); index += 14;
                    txtIntPassport8.Text = line2.Substring(index, 1); index += 1;
                    txtIntPassport9.Text = line2.Substring(index, 1); index += 1;
                }
            }

        }
        catch (Exception ex) { }
    }

    //End
}