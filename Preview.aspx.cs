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
using CallML;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Text;
using System.Net;
using System.Xml.Linq;

public partial class Preview : System.Web.UI.Page
{

    CultureInfo provider = new CultureInfo("en-GB");
    Methods obj = new Methods();
    AccountDtl oAcDtl = null;
    Applicant oAplcnt = null;
    string str = String.Empty;
    bool check = false;
    bool RDexception = false;
    string sCreatedBy = string.Empty;
    string sModifiedBy = string.Empty;

    string ECUSEMAIL, EFNAME, EMIDNAME, ESNAME, EAMOUNT, ERATE, ETENURE, EPTIT;
    string EJ1CUSEMAIL, EJ1FNAME, EJ1MIDNAME, EJ1SNAME, EJ1TIT;
    string EJ2CUSEMAIL, EJ2FNAME, EJ2MIDNAME, EJ2SNAME, EJ2TIT;
    string EJ3CUSEMAIL, EJ3FNAME, EJ3MIDNAME, EJ3SNAME, EJ3TIT;

    int iCurAdd;
    bool SANCTIONSWARNING, SDNWARNING, PEPWARNING, ADDRESSWARNING, ADDRESSLINKSWARNING, GONEAWAYWARNING;
    bool DVLAWARNING, DECEASEDWARNING, PASSPORTWARNING, SEARCHWARNING, IDCARDWARNING, FRAUDULENTPASSPORTWARNING;
    string IDPCheck, IDPScore;
    string SREFNO, SUNIQUEID, sKYCWarning, sKYCComment;

    //Call Validate API
    private string m_xmlIn = "";
    private string m_xmlOut = "";
    string IdentityResult = string.Empty;
    string IdentityScore = string.Empty;
    string STITLE = string.Empty;
    string SFNAME = string.Empty;
    string SMNAME = string.Empty;
    string SSURNAME = string.Empty;
    string SDOB = string.Empty;
    string SABODENO = string.Empty;
    string SSTREET1 = string.Empty;
    string SPOSTTOWN = string.Empty;
    string SPOSTCODE = string.Empty;
    string MATCHLEVEL = string.Empty;
    string APPVERIFIED = string.Empty;
    string PPL1 = string.Empty;
    string PPL2 = string.Empty;
    string PPFNMatch = string.Empty;
    string PPMNMatch = string.Empty;
    string PPSNMatch = string.Empty;
    string PPISCountry = string.Empty;
    string PPNationality = string.Empty;
    string PPCHKDIGIT1 = string.Empty;
    string PPCHKDIGIT2 = string.Empty;
    string PPCHKDIGIT3 = string.Empty;
    string PPCHKDIGIT4 = string.Empty;
    string PPCHKDIGIT5 = string.Empty;
    string PPDoBDay = string.Empty;
    string PPDoBMonth = string.Empty;
    string PPDoBYear = string.Empty;
    string PPExpiry = string.Empty;
    string DLIDNO = string.Empty;
    string DLSNMATCH = string.Empty;
    string DLINIMATCH = string.Empty;
    string DLONMATCH = string.Empty;
    string DLDoBDay = string.Empty;
    string DLDoBMonth = string.Empty;
    string DLDoBYear = string.Empty;
    string DVLWarning = string.Empty;
    string PPWarning = string.Empty;
    //

    protected void Page_Init(object sender, System.EventArgs e)
    {
        try
        {
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

    public void ValidatePage()
    {
        try
        {
            string sCatchNm = "PrvwDtl" + Session["RefNo"];

            if ((Request.UserAgent.IndexOf("AppleWebKit") > 0))
            {
                Request.Browser.Adapters.Clear();
            }

            if (ViewState["PrvwDtl"] == null)
            {
                ViewState["PrvwDtl"] = System.Guid.NewGuid();
                str = ViewState["PrvwDtl"].ToString().Replace("-", "");
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
            ValidatePage();

            if (Page.IsPostBack == false)
            {
                LoadAcDetails(GetAcDtl());

                //LoadAccordianPanels();                 
            }
            LoadAccordianPanels();
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private void LoadAccordianPanels()
    {
        string sApplicantDtl = string.Empty;
        AccountDtl AcDtl = null;

        try
        {

            if (Session["AcDtl"] != null)
            {
                AcDtl = (AccountDtl)Session["AcDtl"];
                acrDynamic.SelectedIndex = -1;

                Label lbTitle;
                Label lbContent;
                AjaxControlToolkit.AccordionPane pn;

                int i = 0;     // This is just to use for assigning pane an id



                foreach (Applicant AppDtl in AcDtl.appList)
                {
                    lbTitle = new Label();
                    lbContent = new Label();

                    if (i != 0)
                    {
                        acrDynamic.RequireOpenedPane = false; //no open pane  
                        lbTitle.Text = "Joint a/c -" + i + ": " + AppDtl.FirstNm;
                    }
                    else
                    {
                        lbTitle.Text = "Primary a/c : " + AppDtl.FirstNm;
                    }

                    sApplicantDtl = "<table class='preTable table borderless lh-1' >" +
                                    "<tr><td class='two_preview previewaccordper' style=''>Name</td>" +
                                    "<td class='two_preview pewtxt'>" + AppDtl.FirstNm + " " + AppDtl.MidNm + " " + AppDtl.SurNm + "</td></tr>" +
                                    "<tr><td class='two_preview previewaccord' style=''>DOB</td>" +
                                    "<td class='two_preview pewtxt'>" + AppDtl.DOB + "</td></tr>";

                    if (!string.IsNullOrWhiteSpace(AppDtl.EmailAddr.Trim()))
                        sApplicantDtl += "<tr><td class='two_preview previewaccord'>Email</td>" +
                                         "<td  class='two_preview pewtxt'>" + AppDtl.EmailAddr + "</td></tr>";
                    else
                        sApplicantDtl += "<tr><td>Email</td><td>N/A</td></tr>";

                    sApplicantDtl += "<tr><td class='two_preview previewaccord'>Address</td>" +
                                     "<td  class='two_preview pewtxt'>" + AppDtl.CurDoorNo + "</td></tr>";

                    //"<tr><td></td>" +
                    //"<td style='color:#000000;font-family:Trebuchet MS,Segoe UI,Verdana,Arial,sans-serif,Helvetica;font-size: 13px;font-weight:bold;'>" + AppDtl.CurDoorNo + "</td></tr>";

                    if (!string.IsNullOrWhiteSpace(AppDtl.CurAddr1.Trim()))
                        sApplicantDtl += "<tr><td class='two_preview'></td><td class='two_preview pewtxt'>" + AppDtl.CurAddr1 + "</td></tr>";
                    if (!string.IsNullOrWhiteSpace(AppDtl.CurAddr2.Trim()))
                        sApplicantDtl += "<tr><td class='two_preview'></td><td class='two_preview pewtxt'>" + AppDtl.CurAddr2 + "</td></tr>";
                    if (!string.IsNullOrWhiteSpace(AppDtl.CurCounty.Trim()))
                        sApplicantDtl += "<tr><td class='two_preview'></td><td class='two_preview pewtxt'>" + AppDtl.CurCounty + "</td></tr>";

                    sApplicantDtl += "<tr><td class='two_preview'></td><td class='two_preview pewtxt'>" + AppDtl.CurAddr3 + "</td></tr>" +
                                     "<tr><td class='two_preview'></td><td class='two_preview pewtxt'>" + AppDtl.CurPcode + "</td></tr>";


                    sApplicantDtl += "<tr><td colspan='2' class='previewaccordhead'>Identity Details</td>" +
                                     "</tr><tr><td colspan='2'><hr class='hr'/></td></tr>";

                    //"<td></tr><tr><td>-----------------------</td></td></tr>";


                    //"<td></tr><tr><td><hr align='left' style='border-top: 1px solid gray;'></td></td></tr>";

                    if (AppDtl.IdenDtls.ToUpper() == "Passport".ToUpper())
                    {
                        sApplicantDtl += "<tr><td class='two_preview previewaccord'>Identity Proof </td>" +
                                         "<td class='two_preview pewtxt'>" + AppDtl.IdenDtls + "</td></tr>" +
                                         "<tr><td class='two_preview previewaccord'>Passport No </td>" +
                                         "<td class='two_preview pewtxt'>" + AppDtl.IdenNo + "</td></tr>" +
                                         "<tr><td class='two_preview previewaccord'>Country of Issue </td>" +
                                         "<td class='two_preview pewtxt'>" + AppDtl.PsPrtIsCntry + "</td></tr>" +
                                         "<tr><td class='two_preview previewaccord'>Date of Issue </td>" +
                                         "<td class='two_preview pewtxt'>" + AppDtl.PsPrtIssDt + "</td></tr>" +
                                         "<tr><td class='two_preview previewaccord'>Date of Expiry </td>" +
                                         "<td class='two_preview pewtxt'>" + AppDtl.PsPrtExpDt + "</td></tr>";
                    }
                    else
                    {
                        sApplicantDtl += "<tr><td class='two_preview previewaccord'>Identity Proof </td>" +
                                         "<td class='two_preview pewtxt'>" + AppDtl.IdenDtls + "</td></tr>" +
                                        "<tr><td class='two_preview previewaccord'>Driving Licence No </td>" +
                                        "<td class='two_preview pewtxt'>" + AppDtl.IdenNo + "</td></tr>" +
                                        "<tr><td class='two_preview previewaccord'>Date of Expiry </td>" +
                                        "<td class='two_preview pewtxt'>" + AppDtl.DrLceExpDt + "</td></tr>" +
                                        "<tr><td class='two_preview previewaccord'>Postcode on Licence </td>" +
                                        "<td class='two_preview pewtxt'>" + AppDtl.DrLcePCode + "</td></tr>";

                    }
                    sApplicantDtl += "</table>";

                    lbContent.Text = sApplicantDtl;
                    pn = new AjaxControlToolkit.AccordionPane();
                    pn.ID = "Pane" + i;
                    pn.HeaderContainer.Controls.Add(lbTitle);
                    pn.ContentContainer.Controls.Add(lbContent);
                    acrDynamic.Panes.Add(pn);
                    ++i;
                }

            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
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
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
        return AcDtl;
    }

    public void LoadAcDetails(AccountDtl AcDetail)
    {
        try
        {
            lblInvAmount.Text = AcDetail.InvAmount;
            lblInvPeriod.Text = AcDetail.InvPeriod;
            lblInvRatOfInt.Text = AcDetail.InvRateOfInt;

            lblBankName.Text = AcDetail.OthBankName;
            lblBankSC.Text = AcDetail.OthBankSC;
            lblBankAcNo.Text = AcDetail.OthBankAcNo;

            lblRepayment.Text = AcDetail.RepayIns;

            /* Calculate Maturity Amount */
            if ((AcDetail.InvAmount != string.Empty) && Convert.ToDouble(AcDetail.InvAmount) > 0)
            {
                try
                {
                    double dMatAmt = 0;
                    //int iPeriod = Convert.ToInt32(AcDetail.InvPeriod.Substring(0, 1));
                    double dAmount = Convert.ToDouble(AcDetail.InvAmount);
                    double dROI = Convert.ToDouble(AcDetail.InvRateOfInt);

                    dMatAmt = dAmount;

                    string periodText = AcDetail.InvPeriod.ToLower();
                    double years = 0;
                    if (periodText.Contains("year"))
                    {
                        periodText = periodText.Replace("years", "").Replace("year", "").Trim();
                        years = Convert.ToDouble(periodText);
                    }
                    else if (periodText.Contains("month"))
                    {
                        periodText = periodText.Replace("months", "").Replace("month", "").Trim();
                        double months = Convert.ToDouble(periodText);
                        years = months / 12.0;
                    }

                    dMatAmt = dMatAmt * (1 + ((dROI / 100.0) * years));

                    lblMatAmt.Text = string.Format("{0:###,###.00}", dMatAmt);
                }
                catch (Exception ex)
                {
                    lblMatAmt.Text = "N/A";
                }
            }

            /* Calculate Maturity Amount */

            switch (AcDetail.RepayIns)
            {
                case "SBM":
                    lblRepayment.Text = "Send back money to original account";
                    break;
                case "RDP":
                    lblRepayment.Text = "Renew deposit for same period";
                    break;
                    //case "INS":  'commented on 2023 enhancement
                    //    lblRepayment.Text = "Will instruct through Union Premier Bond portal, 15 days before maturity";
                    //    break;
            }
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

    protected void btnOtrDtl_Click(object sender, EventArgs e)
    {
        try
        {
            Cache.Remove("OthDtl" + Convert.ToString(Session["RefNo"]));
            Response.Redirect("BondDetails.aspx", false);

        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            btnSave.Enabled = false;
            obj.StoreEvent("66305", Convert.ToString(Session["RefNo"]), "", "", "", Convert.ToString(Session["RefNo"]));

            if (Convert.ToString(Session["Type"]) == "NA")
            {
                AccountDtl AcDtl = null;
                if (Session["AcDtl"] != null)
                    AcDtl = (AccountDtl)Session["AcDtl"];

                if (Session["AcOpenRefNo"] != null && Convert.ToString(Session["AcOpenRefNo"]) == AcDtl.ReferenceNo)
                    Session["Type"] = "RA";
            }

            ViewState["SubStatus"] = "D";
            SaveDetails("D");
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

    //Commented 2023 enhancement remaining
    //private bool IsValidData()
    //{
    //    bool bStatus = false;
    //    try
    //    {
    //        if (txtPassword.Text.Trim() == string.Empty)
    //        {
    //            lblPopupAlert.Text = "Password cannot be blank!";
    //            txtPassword.Focus();
    //            ModalPopupExtender2.Show();
    //            ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-block";
    //            PnlPwdReg.CssClass = "w-90 d-block zindex_1";
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
    //        obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
    //        throw ex;
    //    }
    //    return bStatus;
    //}
    //End

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid == true)
            {
                btnSubmit.Enabled = false;
                if (Convert.ToString(Session["Type"]) == "NA")
                {
                    AccountDtl AcDtl = null;
                    if (Session["AcDtl"] != null)
                        AcDtl = (AccountDtl)Session["AcDtl"];

                    if (Session["AcOpenRefNo"] != null && Convert.ToString(Session["AcOpenRefNo"]) == AcDtl.ReferenceNo)
                        Session["Type"] = "RA";
                }

                if (!IsValidValues())
                {
                    obj.StoreEvent("99999", "", "", "", "System could not accept the application, please make sure all the mandatory fields are filled and try again. If the problem continues kindly contact the nearest branch.", Convert.ToString(Session["RefNo"]));
                    return;
                }

                //AuthenticateApplicants();            

                ViewState["SubStatus"] = "S";
                SaveDetails("S");
                //AccountDtl AcD = GetAcDtl();
                //Response.Redirect("Confirmation.aspx?R=" + obj.UrlEncrypt64("Ref=" + AcD.ReferenceNo + " & AplnSts=S"), false);
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
        finally
        {
            btnSubmit.Enabled = true;
        }
    }

    #region
    //20171229 validations added
    //private bool IsValidValues()
    //{
    //    AccountDtl AcDtl = obj.GetAcDtl();
    //    List<Applicant> lstAplcnt = null;

    //    bool SVStatus = false;

    //    try
    //    {
    //        lstAplcnt = AcDtl.appList;

    //        if (lstAplcnt != null && lstAplcnt.Count > 0)
    //        {
    //            string sSVRefNo = AcDtl.ReferenceNo;
    //            string sSVIsJntAc = AcDtl.IsJntAc;
    //            string sSVInvAmount = AcDtl.InvAmount;
    //            string sSVInvPeriod = AcDtl.InvPeriod;
    //            string sSVInvRateOfInt = AcDtl.InvRateOfInt;
    //            string sSVOthBankName = AcDtl.OthBankName;
    //            string sSVOthBankSC = AcDtl.OthBankSC;
    //            string sSVOthBankAcNo = AcDtl.OthBankAcNo;
    //            string sSVRepayIns = AcDtl.RepayIns;

    //            foreach (Applicant Apln in lstAplcnt)
    //            {
    //                /*Personal Details*/
    //                string sSVTitle = Apln.Title;
    //                string sSVFirstNm = Apln.FirstNm;
    //                string sSVSurNm = Apln.SurNm;
    //                string sSVGender = Apln.Gender;
    //                string sSVMaritalsts = Apln.MaritalSts;
    //                string sSVDOB = Apln.DOB;
    //                string sSVPlaceOfBirth = Apln.PlaceOfBirth;
    //                string sSVMothMaidNm = Apln.MothersMaidenNm;
    //                string sSVCitizenship = Apln.Citizenship;

    //                /*Contact Details*/
    //                string sSVMobileNo = Apln.MobileNo;
    //                string sSVEmail = Apln.EmailAddr;

    //                /*Identity Details*/
    //                string sSVIdentity = Apln.IdenDtls;

    //                /*Employment Details*/
    //                string sSVEmpType = Apln.EmpType;
    //                string sSVEmpOth = Apln.EmptypOth;

    //                /*Address*/
    //                string sSVCurPostCode = Apln.CurPcode;
    //                string sSVCurDrNo = Apln.CurDoorNo;
    //                string sSVCurAdd2 = Apln.CurAddr2;
    //                string sSVCurAdd3 = Apln.CurAddr3;
    //                string sSVCurCountry = Apln.CurCntry;
    //                string sSVRsdnSince = Apln.ResidingSince;

    //                /*Tax Liabilities*/
    //                string sSVIsUSperson = Apln.IsUSperson;
    //                string sSVPriJrsdctn = Apln.PriJrsdctn;
    //                string sSVPriTIN = Apln.PriTIN;
    //                string sSVAdJrsdctn1 = Apln.AdJrsdctn1;
    //                string sSVAdTIN1 = Apln.AdTIN1;
    //                string sSVAdJrsdctn2 = Apln.AdJrsdctn2;
    //                string sSVAdTIN2 = Apln.AdTIN2;
    //                string sSVRFNPTin = Apln.ResnNAPTIN;

    //                /*FATCA*/
    //                string sSVPayTax = Apln.PayTax;
    //                string sSVUSCitizen = Apln.USCitizen;
    //                string sSVGreenCard = Apln.GreenCard;
    //                string sSVRealEst = Apln.RealEst;
    //                string sSVAsset = Apln.assets;


    //                # region Personal Details
    //                if (sSVTitle == string.Empty || sSVTitle == "-1")
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVFirstNm == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVFirstNm.Length < 2)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVSurNm == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVSurNm.Length < 2)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVGender == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if ((sSVMaritalsts == string.Empty) || (sSVMaritalsts == "-1"))
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (IsDate(sSVDOB) == true)
    //                {
    //                    if (CalAge(sSVDOB) < 18)
    //                    {
    //                        SVStatus = false;
    //                        return SVStatus;
    //                    }
    //                }
    //                else
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVPlaceOfBirth == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVMothMaidNm == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVCitizenship == string.Empty || sSVCitizenship == "-1")
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                #endregion

    //                # region Contact Details
    //                if (sSVMobileNo == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVEmail == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                else
    //                {
    //                    string pattern = pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

    //                    if (!Regex.IsMatch(sSVEmail, pattern))
    //                    {
    //                        SVStatus = false;
    //                        return SVStatus;
    //                    }
    //                }
    //                #endregion

    //                # region Identity Details
    //                if (sSVIdentity == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                else
    //                {
    //                    if (sSVIdentity.ToUpper() == "Driving Licence".ToUpper())
    //                    {
    //                        string sSVDLTYPE = Apln.DrLceType;
    //                        string sSVDvlcNo = Apln.IdenNo;
    //                        string sSVDvlcExDt = Apln.DrLceExpDt;
    //                        string sSVDvlcPcode = Apln.DrLcePCode;

    //                        if (sSVDLTYPE == string.Empty)
    //                        {
    //                            SVStatus = false;
    //                            return SVStatus;
    //                        }
    //                        if (sSVDvlcNo == string.Empty)
    //                        {
    //                            SVStatus = false;
    //                            return SVStatus;
    //                        }
    //                        if (sSVDvlcExDt == string.Empty)
    //                        {
    //                            SVStatus = false;
    //                            return SVStatus;
    //                        }
    //                        if (sSVDvlcPcode == string.Empty)
    //                        {
    //                            SVStatus = false;
    //                            return SVStatus;
    //                        }
    //                    }
    //                    if (sSVIdentity.ToUpper() == "Passport".ToUpper())
    //                    {
    //                        string sSVPType = Apln.IsUkPsPrt;
    //                        string sPPNm = Apln.PsPrtName;
    //                        string sPPNo = Apln.IdenNo;
    //                        string sPPIC = Apln.PsPrtIsCntry;
    //                        string sPPIsDt = Apln.PsPrtIssDt;
    //                        string sPPExDt = Apln.PsPrtExpDt;

    //                        if (sSVPType == string.Empty)
    //                        {
    //                            SVStatus = false;
    //                            return SVStatus;
    //                        }
    //                        if (sPPNm == string.Empty)
    //                        {
    //                            SVStatus = false;
    //                            return SVStatus;
    //                        }
    //                        if (sPPNo == string.Empty)
    //                        {
    //                            SVStatus = false;
    //                            return SVStatus;
    //                        }
    //                        if (sPPIC == string.Empty)
    //                        {
    //                            SVStatus = false;
    //                            return SVStatus;
    //                        }
    //                        if (sPPIsDt == string.Empty)
    //                        {
    //                            SVStatus = false;
    //                            return SVStatus;
    //                        }
    //                        if (sPPExDt == string.Empty)
    //                        {
    //                            SVStatus = false;
    //                            return SVStatus;
    //                        }
    //                    }
    //                }
    //                #endregion

    //                # region Employment Details
    //                if (sSVEmpType == string.Empty || sSVEmpType == "-1")
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVEmpType.ToUpper() == "others".ToUpper() && sSVEmpOth == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                #endregion

    //                #region Address
    //                if (sSVCurPostCode == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVCurDrNo == string.Empty && sSVCurAdd2 == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVCurAdd3 == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVCurCountry == string.Empty || sSVCurCountry == "-1")
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVRsdnSince == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                #endregion

    //                #region Tax Liablities
    //                if (sSVIsUSperson == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVIsUSperson.ToUpper() == "yes".ToUpper())
    //                {
    //                    if (sSVPriJrsdctn == "-1" && sSVPriTIN == string.Empty && sSVRFNPTin == string.Empty)
    //                    {
    //                        SVStatus = false;
    //                        return SVStatus;
    //                    }
    //                    if (sSVPriJrsdctn != "-1" && sSVPriTIN == string.Empty)
    //                    {
    //                        SVStatus = false;
    //                        return SVStatus;
    //                    }
    //                    if (sSVPriJrsdctn == "-1" && sSVPriTIN != string.Empty)
    //                    {
    //                        SVStatus = false;
    //                        return false;
    //                    }
    //                    if (sSVAdJrsdctn1 != "-1" && sSVAdTIN1 == string.Empty)
    //                    {
    //                        SVStatus = false;
    //                        return SVStatus;
    //                    }
    //                    if (sSVAdJrsdctn1 == "-1" && sSVAdTIN1 != string.Empty)
    //                    {
    //                        SVStatus = false;
    //                        return false;
    //                    }
    //                    if (sSVAdJrsdctn2 != "-1" && sSVAdTIN2 == string.Empty)
    //                    {
    //                        SVStatus = false;
    //                        return SVStatus;
    //                    }
    //                    if (sSVAdJrsdctn2 == "-1" && sSVAdTIN2 != string.Empty)
    //                    {
    //                        SVStatus = false;
    //                        return false;
    //                    }
    //                }
    //                #endregion

    //                #region FATCA
    //                if (sSVPayTax == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVUSCitizen == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVGreenCard == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVRealEst == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                if (sSVAsset == string.Empty)
    //                {
    //                    SVStatus = false;
    //                    return SVStatus;
    //                }
    //                #endregion

    //            }



    //            //if (sSVRefNo == string.Empty)
    //            //{
    //            //    SVStatus = false;
    //            //    return SVStatus;
    //            //}
    //            if (sSVIsJntAc == string.Empty)
    //            {
    //                SVStatus = false;
    //                return SVStatus;
    //            }
    //            if (sSVInvAmount == string.Empty)
    //            {
    //                SVStatus = false;
    //                return SVStatus;
    //            }
    //            if (sSVInvPeriod == string.Empty)
    //            {
    //                SVStatus = false;
    //                return SVStatus;
    //            }
    //            if (sSVInvRateOfInt == string.Empty)
    //            {
    //                SVStatus = false;
    //                return SVStatus;
    //            }
    //            if (sSVOthBankName == string.Empty)
    //            {
    //                SVStatus = false;
    //                return SVStatus;
    //            }
    //            if (sSVOthBankSC == string.Empty)
    //            {
    //                SVStatus = false;
    //                return SVStatus;
    //            }
    //            if (sSVOthBankAcNo == string.Empty)
    //            {
    //                SVStatus = false;
    //                return SVStatus;
    //            }
    //            if (sSVRepayIns == string.Empty)
    //            {
    //                SVStatus = false;
    //                return SVStatus;
    //            }


    //            SVStatus = true;
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    return SVStatus;
    //}

    ////private bool IsValidValues()
    ////{
    ////    AccountDtl AcDtl = obj.GetAcDtl();
    ////    List<Applicant> lstAplcnt = null;

    ////    bool SVStatus = false;

    ////    try
    ////    {
    ////        lstAplcnt = AcDtl.appList;

    ////        if (lstAplcnt != null && lstAplcnt.Count > 0)
    ////        {
    ////            string sSVRefNo = AcDtl.ReferenceNo;
    ////            string sSVIsJntAc = AcDtl.IsJntAc;
    ////            string sSVInvAmount = AcDtl.InvAmount;
    ////            string sSVInvPeriod = AcDtl.InvPeriod;
    ////            string sSVInvRateOfInt = AcDtl.InvRateOfInt;
    ////            string sSVOthBankName = AcDtl.OthBankName;
    ////            string sSVOthBankSC = AcDtl.OthBankSC;
    ////            string sSVOthBankAcNo = AcDtl.OthBankAcNo;
    ////            string sSVRepayIns = AcDtl.RepayIns;

    ////            foreach (Applicant Apln in lstAplcnt)
    ////            {
    ////                /*Personal Details*/
    ////                string sSVTitle = Apln.Title;
    ////                string sSVFirstNm = Apln.FirstNm;
    ////                string sSVSurNm = Apln.SurNm;
    ////                string sSVGender = Apln.Gender;
    ////                string sSVMaritalsts = Apln.MaritalSts;
    ////                string sSVDOB = Apln.DOB;
    ////                string sSVPlaceOfBirth = Apln.PlaceOfBirth;
    ////                string sSVMothMaidNm = Apln.MothersMaidenNm;
    ////                string sSVCitizenship = Apln.Citizenship;

    ////                /*Contact Details*/
    ////                string sSVMobileNo = Apln.MobileNo;
    ////                string sSVEmail = Apln.EmailAddr;

    ////                /*Identity Details*/
    ////                string sSVIdentity = Apln.IdenDtls;

    ////                /*Employment Details*/
    ////                string sSVEmpType = Apln.EmpType;
    ////                string sSVEmpOth = Apln.EmptypOth;

    ////                /*Address*/
    ////                string sSVCurPostCode = Apln.CurPcode;
    ////                string sSVCurDrNo = Apln.CurDoorNo;
    ////                string sSVCurAdd2 = Apln.CurAddr2;
    ////                string sSVCurAdd3 = Apln.CurAddr3;
    ////                string sSVCurCountry = Apln.CurCntry;
    ////                string sSVRsdnSince = Apln.ResidingSince;

    ////                /*Tax Liabilities*/
    ////                string sSVIsUSperson = Apln.IsUSperson;
    ////                string sSVPriJrsdctn = Apln.PriJrsdctn;
    ////                string sSVPriTIN = Apln.PriTIN;
    ////                string sSVAdJrsdctn1 = Apln.AdJrsdctn1;
    ////                string sSVAdTIN1 = Apln.AdTIN1;
    ////                string sSVAdJrsdctn2 = Apln.AdJrsdctn2;
    ////                string sSVAdTIN2 = Apln.AdTIN2;
    ////                string sSVRFNPTin = Apln.ResnNAPTIN;

    ////                /*FATCA*/
    ////                string sSVPayTax = Apln.PayTax;
    ////                string sSVUSCitizen = Apln.USCitizen;
    ////                string sSVGreenCard = Apln.GreenCard;
    ////                string sSVRealEst = Apln.RealEst;
    ////                string sSVAsset = Apln.assets;


    ////                # region Personal Details
    ////                if (sSVTitle == string.Empty || sSVTitle == "-1")
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "SVStatus", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                if (sSVFirstNm == string.Empty)
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVFirstNm", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                if (sSVFirstNm.Length < 2)
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVFirstNm.Length", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                if (sSVSurNm == string.Empty)
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVSurNm", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                if (sSVSurNm.Length < 2)
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVSurNm.Length", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                if (sSVGender == string.Empty)
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVGender", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                if ((sSVMaritalsts == string.Empty) || (sSVMaritalsts == "-1"))
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVMaritalsts", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                if (IsDate(sSVDOB) == true)
    ////                {
    ////                    if (CalAge(sSVDOB) < 18)
    ////                    {
    ////                        SVStatus = false;
    ////                        obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "CalAge(sSVDOB)", Convert.ToString(Session["RefNo"]));
    ////                        return SVStatus;
    ////                    }
    ////                }
    ////                else
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "IsDate(sSVDOB)", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                if (sSVPlaceOfBirth == string.Empty)
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVPlaceOfBirth", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                if (sSVMothMaidNm == string.Empty)
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVMothMaidNm", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                if (sSVCitizenship == string.Empty || sSVCitizenship == "-1")
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVCitizenship", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                #endregion

    ////                # region Contact Details
    ////                if (sSVMobileNo == string.Empty)
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVMobileNo", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                if (sSVEmail == string.Empty)
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "SVStatus", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                else
    ////                {
    ////                    string pattern = pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

    ////                    if (!Regex.IsMatch(sSVEmail, pattern))
    ////                    {
    ////                        SVStatus = false;
    ////                        obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "Regex.IsMatch(sSVEmail, pattern)", Convert.ToString(Session["RefNo"]));
    ////                        return SVStatus;
    ////                    }
    ////                }
    ////                #endregion

    ////                # region Identity Details
    ////                if (sSVIdentity == string.Empty)
    ////                {
    ////                    SVStatus = false;
    ////                    return SVStatus;
    ////                }
    ////                else
    ////                {
    ////                    if (sSVIdentity.ToUpper() == "Driving Licence".ToUpper())
    ////                    {
    ////                        string sSVDLTYPE = Apln.DrLceType;
    ////                        string sSVDvlcNo = Apln.IdenNo;
    ////                        string sSVDvlcExDt = Apln.DrLceExpDt;
    ////                        string sSVDvlcPcode = Apln.DrLcePCode;

    ////                        if (sSVDLTYPE == string.Empty)
    ////                        {
    ////                            SVStatus = false;
    ////                            obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVDLTYPE", Convert.ToString(Session["RefNo"]));
    ////                            return SVStatus;
    ////                        }
    ////                        if (sSVDvlcNo == string.Empty)
    ////                        {
    ////                            SVStatus = false;
    ////                            obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVDvlcNo", Convert.ToString(Session["RefNo"]));
    ////                            return SVStatus;
    ////                        }
    ////                        if (sSVDvlcExDt == string.Empty)
    ////                        {
    ////                            SVStatus = false;
    ////                            obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVDvlcExDt", Convert.ToString(Session["RefNo"]));
    ////                            return SVStatus;
    ////                        }
    ////                        if (sSVDvlcPcode == string.Empty)
    ////                        {
    ////                            SVStatus = false;
    ////                            obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVDvlcPcode", Convert.ToString(Session["RefNo"]));
    ////                            return SVStatus;
    ////                        }
    ////                    }
    ////                    if (sSVIdentity.ToUpper() == "Passport".ToUpper())
    ////                    {
    ////                        string sSVPType = Apln.IsUkPsPrt;
    ////                        string sPPNm = Apln.PsPrtName;
    ////                        string sPPNo = Apln.IdenNo;
    ////                        string sPPIC = Apln.PsPrtIsCntry;
    ////                        string sPPIsDt = Apln.PsPrtIssDt;
    ////                        string sPPExDt = Apln.PsPrtExpDt;

    ////                        if (sSVPType == string.Empty)
    ////                        {
    ////                            SVStatus = false;
    ////                            obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVPType", Convert.ToString(Session["RefNo"]));
    ////                            return SVStatus;
    ////                        }
    ////                        if (sPPNm == string.Empty)
    ////                        {
    ////                            SVStatus = false;
    ////                            obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sPPNm", Convert.ToString(Session["RefNo"]));
    ////                            return SVStatus;
    ////                        }
    ////                        if (sPPNo == string.Empty)
    ////                        {
    ////                            SVStatus = false;
    ////                            obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sPPNo", Convert.ToString(Session["RefNo"]));
    ////                            return SVStatus;
    ////                        }
    ////                        if (sPPIC == string.Empty)
    ////                        {
    ////                            SVStatus = false;
    ////                            obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sPPIC", Convert.ToString(Session["RefNo"]));
    ////                            return SVStatus;
    ////                        }
    ////                        if (sPPIsDt == string.Empty)
    ////                        {
    ////                            SVStatus = false;
    ////                            obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sPPIsDt", Convert.ToString(Session["RefNo"]));
    ////                            return SVStatus;
    ////                        }
    ////                        if (sPPExDt == string.Empty)
    ////                        {
    ////                            SVStatus = false;
    ////                            obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sPPExDt", Convert.ToString(Session["RefNo"]));
    ////                            return SVStatus;
    ////                        }
    ////                    }
    ////                }
    ////                #endregion

    ////                # region Employment Details
    ////                if (sSVEmpType == string.Empty || sSVEmpType == "-1")
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVEmpType", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                if (sSVEmpType.ToUpper() == "others".ToUpper() && sSVEmpOth == string.Empty)
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVEmpType", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                #endregion

    ////                #region Address
    ////                if (sSVCurPostCode == string.Empty)
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVCurPostCode", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                if (sSVCurDrNo == string.Empty && sSVCurAdd2 == string.Empty)
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVCurDrNo or sSVCurAdd2", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                if (sSVCurAdd3 == string.Empty)
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVCurAdd3", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                if (sSVCurCountry == string.Empty || sSVCurCountry == "-1")
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVCurCountry", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                if (sSVRsdnSince == string.Empty)
    ////                {
    ////                    SVStatus = false;
    ////                    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVRsdnSince", Convert.ToString(Session["RefNo"]));
    ////                    return SVStatus;
    ////                }
    ////                #endregion

    ////                #region Tax Liablities
    ////                ////if (sSVIsUSperson == string.Empty)
    ////                ////{
    ////                ////    SVStatus = false;
    ////                ////    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVIsUSperson", Convert.ToString(Session["RefNo"]));
    ////                ////    return SVStatus;
    ////                ////}
    ////                ////if (sSVIsUSperson.ToUpper() == "yes".ToUpper())
    ////                ////{
    ////                ////    if (sSVPriJrsdctn == "-1" && sSVPriTIN == string.Empty && sSVRFNPTin == string.Empty)
    ////                ////    {
    ////                ////        SVStatus = false;
    ////                ////        obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVPriJrsdctn sSVPriTIN sSVRFNPTin", Convert.ToString(Session["RefNo"]));
    ////                ////        return SVStatus;
    ////                ////    }
    ////                ////    if (sSVPriJrsdctn != "-1" && sSVPriTIN == string.Empty)
    ////                ////    {
    ////                ////        SVStatus = false;
    ////                ////        obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVPriJrsdctn sSVPriTIN", Convert.ToString(Session["RefNo"]));
    ////                ////        return SVStatus;
    ////                ////    }
    ////                ////    if (sSVPriJrsdctn == "-1" && sSVPriTIN != string.Empty)
    ////                ////    {
    ////                ////        SVStatus = false;
    ////                ////        obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVPriJrsdctn sSVPriTIN", Convert.ToString(Session["RefNo"]));
    ////                ////        return false;
    ////                ////    }
    ////                ////    if (sSVAdJrsdctn1 != "-1" && sSVAdTIN1 == string.Empty)
    ////                ////    {
    ////                ////        SVStatus = false;
    ////                ////        obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVAdJrsdctn1 sSVAdTIN1", Convert.ToString(Session["RefNo"]));
    ////                ////        return SVStatus;
    ////                ////    }
    ////                ////    if (sSVAdJrsdctn1 == "-1" && sSVAdTIN1 != string.Empty)
    ////                ////    {
    ////                ////        SVStatus = false;
    ////                ////        obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVAdJrsdctn1 sSVAdTIN1", Convert.ToString(Session["RefNo"]));
    ////                ////        return false;
    ////                ////    }
    ////                ////    if (sSVAdJrsdctn2 != "-1" && sSVAdTIN2 == string.Empty)
    ////                ////    {
    ////                ////        SVStatus = false;
    ////                ////        obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVAdJrsdctn2 sSVAdTIN2", Convert.ToString(Session["RefNo"]));
    ////                ////        return SVStatus;
    ////                ////    }
    ////                ////    if (sSVAdJrsdctn2 == "-1" && sSVAdTIN2 != string.Empty)
    ////                ////    {
    ////                ////        SVStatus = false;
    ////                ////        obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVAdJrsdctn2 sSVAdTIN2", Convert.ToString(Session["RefNo"]));
    ////                ////        return false;
    ////                ////    }
    ////                ////}
    ////                #endregion

    ////                #region FATCA
    ////                ////if (sSVPayTax == string.Empty)
    ////                ////{
    ////                ////    SVStatus = false;
    ////                ////    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVPayTax", Convert.ToString(Session["RefNo"]));
    ////                ////    return SVStatus;
    ////                ////}
    ////                ////if (sSVUSCitizen == string.Empty)
    ////                ////{
    ////                ////    SVStatus = false;
    ////                ////    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVUSCitizen", Convert.ToString(Session["RefNo"]));
    ////                ////    return SVStatus;
    ////                ////}
    ////                ////if (sSVGreenCard == string.Empty)
    ////                ////{
    ////                ////    SVStatus = false;
    ////                ////    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVGreenCard", Convert.ToString(Session["RefNo"]));
    ////                ////    return SVStatus;
    ////                ////}
    ////                ////if (sSVRealEst == string.Empty)
    ////                ////{
    ////                ////    SVStatus = false;
    ////                ////    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVRealEst", Convert.ToString(Session["RefNo"]));
    ////                ////    return SVStatus;
    ////                ////}
    ////                ////if (sSVAsset == string.Empty)
    ////                ////{
    ////                ////    SVStatus = false;
    ////                ////    obj.StoreEvent("99999", "", "", "", Apln.FirstNm + "sSVAsset", Convert.ToString(Session["RefNo"]));
    ////                ////    return SVStatus;
    ////                ////}
    ////                #endregion

    ////            }



    ////            //if (sSVRefNo == string.Empty)
    ////            //{
    ////            //    SVStatus = false;
    ////            //    return SVStatus;
    ////            //}
    ////            if (sSVIsJntAc == string.Empty)
    ////            {
    ////                SVStatus = false;
    ////                obj.StoreEvent("99999", "", "", "", "sSVIsJntAc", Convert.ToString(Session["RefNo"]));
    ////                return SVStatus;
    ////            }
    ////            if (sSVInvAmount == string.Empty)
    ////            {
    ////                SVStatus = false;
    ////                obj.StoreEvent("99999", "", "", "", "sSVInvAmount", Convert.ToString(Session["RefNo"]));
    ////                return SVStatus;
    ////            }
    ////            if (sSVInvPeriod == string.Empty)
    ////            {
    ////                SVStatus = false;
    ////                obj.StoreEvent("99999", "", "", "", "sSVInvPeriod", Convert.ToString(Session["RefNo"]));
    ////                return SVStatus;
    ////            }
    ////            if (sSVInvRateOfInt == string.Empty)
    ////            {
    ////                SVStatus = false;
    ////                obj.StoreEvent("99999", "", "", "", "sSVInvRateOfInt", Convert.ToString(Session["RefNo"]));
    ////                return SVStatus;
    ////            }
    ////            if (sSVOthBankName == string.Empty)
    ////            {
    ////                SVStatus = false;
    ////                obj.StoreEvent("99999", "", "", "", "sSVOthBankName", Convert.ToString(Session["RefNo"]));
    ////                return SVStatus;
    ////            }
    ////            if (sSVOthBankSC == string.Empty)
    ////            {
    ////                SVStatus = false;
    ////                obj.StoreEvent("99999", "", "", "", "sSVOthBankSC", Convert.ToString(Session["RefNo"]));
    ////                return SVStatus;
    ////            }
    ////            if (sSVOthBankAcNo == string.Empty)
    ////            {
    ////                SVStatus = false;
    ////                obj.StoreEvent("99999", "", "", "", "sSVOthBankAcNo", Convert.ToString(Session["RefNo"]));
    ////                return SVStatus;
    ////            }
    ////            if (sSVRepayIns == string.Empty)
    ////            {
    ////                SVStatus = false;
    ////                obj.StoreEvent("99999", "", "", "", "sSVRepayIns", Convert.ToString(Session["RefNo"]));
    ////                return SVStatus;
    ////            }


    ////            SVStatus = true;
    ////        }

    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////    return SVStatus;
    ////}
    #endregion

    //Added for national id missing issue(20210121) - chellappa 
    private bool IsValidValues()
    {
        AccountDtl AcDtl = obj.GetAcDtl();
        List<Applicant> lstAplcnt = null;

        bool SVStatus = false;

        try
        {
            lstAplcnt = AcDtl.appList;

            if (lstAplcnt != null && lstAplcnt.Count > 0)
            {
                string sSVRefNo = AcDtl.ReferenceNo;
                string sSVIsJntAc = AcDtl.IsJntAc;
                string sSVInvAmount = AcDtl.InvAmount;
                string sSVInvPeriod = AcDtl.InvPeriod;
                string sSVInvRateOfInt = AcDtl.InvRateOfInt;
                string sSVOthBankName = AcDtl.OthBankName;
                string sSVOthBankSC = AcDtl.OthBankSC;
                string sSVOthBankAcNo = AcDtl.OthBankAcNo;
                string sSVRepayIns = AcDtl.RepayIns;

                foreach (Applicant Apln in lstAplcnt)
                {
                    /*Personal Details*/
                    string sSVTitle = Apln.Title;
                    string sSVFirstNm = Apln.FirstNm;
                    string sSVSurNm = Apln.SurNm;
                    string sSVGender = Apln.Gender;
                    string sSVMaritalsts = Apln.MaritalSts;
                    string sSVDOB = Apln.DOB;
                    string sSVPlaceOfBirth = Apln.PlaceOfBirth;
                    string sSVMothMaidNm = Apln.MothersMaidenNm;
                    string sSVCitizenship = Apln.Citizenship;

                    /*Contact Details*/
                    string sSVMobileNo = Apln.MobileNo;
                    string sSVEmail = Apln.EmailAddr;

                    /*Identity Details*/
                    string sSVIdentity = Apln.IdenDtls;

                    /*Employment Details*/
                    string sSVEmpType = Apln.EmpType;
                    string sSVEmpOth = Apln.EmptypOth;

                    /*Address*/
                    string sSVCurPostCode = Apln.CurPcode;
                    string sSVCurDrNo = Apln.CurDoorNo;
                    string sSVCurAdd2 = Apln.CurAddr2;
                    string sSVCurAdd3 = Apln.CurAddr3;
                    string sSVCurCountry = Apln.CurCntry;
                    string sSVRsdnSince = Apln.ResidingSince;

                    /*Tax Liabilities*/
                    string sSVIsUSperson = Apln.IsUSperson;
                    string sSVPriJrsdctn = Apln.PriJrsdctn;
                    string sSVPriTIN = Apln.PriTIN;
                    string sSVAdJrsdctn1 = Apln.AdJrsdctn1;
                    string sSVAdTIN1 = Apln.AdTIN1;
                    string sSVAdJrsdctn2 = Apln.AdJrsdctn2;
                    string sSVAdTIN2 = Apln.AdTIN2;
                    string sSVRFNPTin = Apln.ResnNAPTIN;

                    /*FATCA*/
                    string sSVPayTax = Apln.PayTax;
                    string sSVUSCitizen = Apln.USCitizen;
                    string sSVGreenCard = Apln.GreenCard;
                    string sSVRealEst = Apln.RealEst;
                    string sSVAsset = Apln.assets;



                    # region Personal Details
                    if (sSVTitle == string.Empty || sSVTitle == "-1")
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Title is missing for the applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please select the Title for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if (sSVFirstNm == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "First name is missing for the applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please enter the First name for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if (sSVFirstNm.Length < 2)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "First name length is not sufficient for the applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "First name should have atleast 2 alpha characters for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if (sSVSurNm == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Surname is missing for the applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please enter the Surname for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if (sSVSurNm.Length < 2)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Surname length is not sufficient for the applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Surname should have atleast 2 alpha characters for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if (sSVGender == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Gender is missing for the applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please select the Gender for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if ((sSVMaritalsts == string.Empty) || (sSVMaritalsts == "-1"))
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Marital status is missing for the applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please select the Marital status for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if (IsDate(sSVDOB) == true)
                    {
                        if (CalAge(sSVDOB) < 18)
                        {
                            SVStatus = false;
                            obj.StoreEvent("99999", "", "", "", "Date of Birth should be above 18 Years for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                            lblEValErrMsg.Text = "The Date of Birth should be above 18 Years for the Applicant " + Apln.FirstNm;
                            return SVStatus;
                        }
                    }
                    else
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Invalid Date of Birth for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Invalid Date of Birth for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if (sSVPlaceOfBirth == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Place Of Birth missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please enter your Place Of Birth for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if (sSVMothMaidNm == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Mothers maiden name for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please enter your Mothers maiden name for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if (sSVCitizenship == string.Empty || sSVCitizenship == "-1")
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Citizenship missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please select your Citizenship for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    #endregion

                    # region Contact Details
                    if (sSVMobileNo == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "mobile number missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please enter mobile number for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if (sSVEmail == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Email Address missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please enter Email Address for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    else
                    {
                        string pattern = pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

                        if (!Regex.IsMatch(sSVEmail, pattern))
                        {
                            SVStatus = false;
                            obj.StoreEvent("99999", "", "", "", "valid Email Address for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                            lblEValErrMsg.Text = "Please enter valid Email Address for the Applicant " + Apln.FirstNm;
                            return SVStatus;
                        }
                    }
                    #endregion

                    # region Identity Details
                    if (sSVIdentity == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Identity Details missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please select the Identity Details for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    else
                    {
                        if (sSVIdentity.ToUpper() == "Driving Licence".ToUpper())
                        {
                            string sSVDLTYPE = Apln.DrLceType;
                            string sSVDvlcNo = Apln.IdenNo;
                            string sSVDvlcExDt = Apln.DrLceExpDt;
                            string sSVDvlcPcode = Apln.DrLcePCode;

                            if (sSVDLTYPE == string.Empty)
                            {
                                SVStatus = false;
                                obj.StoreEvent("99999", "", "", "", "Driving Licence Type missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                                lblEValErrMsg.Text = "Please select the Driving Licence Type for the Applicant " + Apln.FirstNm;
                                return SVStatus;
                            }
                            if (sSVDvlcNo == string.Empty)
                            {
                                SVStatus = false;
                                obj.StoreEvent("99999", "", "", "", "Driving Licence missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                                lblEValErrMsg.Text = "Please enter the Driving Licence for the Applicant " + Apln.FirstNm;
                                return SVStatus;
                            }
                            if (sSVDvlcExDt == string.Empty)
                            {
                                SVStatus = false;
                                obj.StoreEvent("99999", "", "", "", "Driving licence expiry date missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                                lblEValErrMsg.Text = "Please select the Driving licence expiry date for the Applicant " + Apln.FirstNm;
                                return SVStatus;
                            }
                            if (sSVDvlcPcode == string.Empty)
                            {
                                SVStatus = false;
                                obj.StoreEvent("99999", "", "", "", "postcode on Licence missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                                lblEValErrMsg.Text = "Please enter the postcode on Licence for the Applicant " + Apln.FirstNm;
                                return SVStatus;
                            }
                        }
                        if (sSVIdentity.ToUpper() == "Passport".ToUpper())
                        {
                            string sSVPType = Apln.IsUkPsPrt;
                            string sPPNm = Apln.PsPrtName;
                            string sPPNo = Apln.IdenNo;
                            string sPPIC = Apln.PsPrtIsCntry;
                            string sPPIsDt = Apln.PsPrtIssDt;
                            string sPPExDt = Apln.PsPrtExpDt;

                            if (sSVPType == string.Empty)
                            {
                                SVStatus = false;
                                obj.StoreEvent("99999", "", "", "", "passport type missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                                lblEValErrMsg.Text = "Please select the passport type for the Applicant " + Apln.FirstNm;
                                return SVStatus;
                            }
                            if (sPPNm == string.Empty)
                            {
                                SVStatus = false;
                                obj.StoreEvent("99999", "", "", "", "passport Name missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                                lblEValErrMsg.Text = "Please enter the passport Name for the Applicant " + Apln.FirstNm;
                                return SVStatus;
                            }
                            if (sPPNo == string.Empty)
                            {
                                SVStatus = false;
                                obj.StoreEvent("99999", "", "", "", "passport No missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                                lblEValErrMsg.Text = "Please enter the passport No for the Applicant " + Apln.FirstNm;
                                return SVStatus;
                            }
                            if (sPPIC == string.Empty)
                            {
                                SVStatus = false;
                                obj.StoreEvent("99999", "", "", "", "passport issuing country missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                                lblEValErrMsg.Text = "Please select the passport issuing country for the Applicant " + Apln.FirstNm;
                                return SVStatus;
                            }
                            if (sPPIsDt == string.Empty)
                            {
                                SVStatus = false;
                                obj.StoreEvent("99999", "", "", "", "passport issued date missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                                lblEValErrMsg.Text = "Please select the passport issued date for the Applicant " + Apln.FirstNm;
                                return SVStatus;
                            }
                            if (sPPExDt == string.Empty)
                            {
                                SVStatus = false;
                                obj.StoreEvent("99999", "", "", "", "passport expiry date missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                                lblEValErrMsg.Text = "Please select the passport expiry date for the Applicant " + Apln.FirstNm;
                                return SVStatus;
                            }
                        }
                    }
                    #endregion

                    # region Employment Details
                    if (sSVEmpType == string.Empty || sSVEmpType == "-1")
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Occupation details missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please select the Occupation details for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if (sSVEmpType.ToUpper() == "others".ToUpper() && sSVEmpOth == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "occupation details other missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please enter the occupation other details for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    #endregion

                    #region Address
                    if (sSVCurPostCode == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "current address postcode missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please enter the current address postcode for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    //if (sSVCurDrNo == string.Empty)
                    //{
                    //    SVStatus = false;
                    //    obj.StoreEvent("99999", "", "", "", "current address house / flat no missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                    //    lblEValErrMsg.Text = "Please enter the current address house / flat no for the Applicant " + Apln.FirstNm;
                    //    return SVStatus;
                    //}
                    if (sSVCurAdd2 == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "current address street missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please enter the current address street for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if (sSVCurAdd3 == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "current address city missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please select the current address city for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if (sSVCurCountry == string.Empty || sSVCurCountry == "-1")
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Current address Country missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please select the current address country for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if (sSVRsdnSince == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Current address Residence missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please Select Current Address Date of Residence for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    #endregion

                    #region Tax Liablities
                    if (sSVIsUSperson == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Tax information missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please select the Tax information for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }

                    //if (sSVIsUSperson.ToUpper() == "Yes".ToUpper())
                    //{
                    //    if (sSVPriJrsdctn != "-1" && sSVPriTIN == string.Empty)
                    //    {
                    //        SVStatus = false;
                    //        obj.StoreEvent("99999", "", "", "", "primary TIN missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                    //        lblEValErrMsg.Text = "Please enter primary TIN for the Applicant " + Apln.FirstNm;
                    //        return SVStatus;
                    //    }
                    //    if (sSVAdJrsdctn1 == "-1" && sSVAdTIN1 != string.Empty)
                    //    {
                    //        SVStatus = false;
                    //        obj.StoreEvent("99999", "", "", "", "additional Jurisdiction-1 missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                    //        lblEValErrMsg.Text = "Please Select additional Jurisdiction-1 for the Applicant " + Apln.FirstNm;
                    //        return SVStatus;
                    //    }
                    //    if (sSVAdJrsdctn2 == "-1" && sSVAdTIN2 != string.Empty)
                    //    {
                    //        SVStatus = false;
                    //        obj.StoreEvent("99999", "", "", "", "additional Jurisdiction-2 missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                    //        lblEValErrMsg.Text = "Please Select additional Jurisdiction-2 for the Applicant " + Apln.FirstNm;
                    //        return SVStatus;
                    //    }

                    //    if ((sSVPriJrsdctn != "-1" && sSVPriTIN != string.Empty) && ((sSVAdJrsdctn1 == "-1") && (sSVAdTIN1 == string.Empty || sSVRFNPTin == string.Empty)))
                    //    {
                    //        SVStatus = false;
                    //        obj.StoreEvent("99999", "", "", "", "additional Jurisdiction-1 missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                    //        lblEValErrMsg.Text = "Please Select additional Jurisdiction-1 for the Applicant " + Apln.FirstNm;
                    //        return SVStatus;
                    //    }

                    //    if (sSVPriJrsdctn != "-1" && sSVPriTIN != string.Empty)
                    //    {
                    //        if (sSVRFNPTin == string.Empty)
                    //        {
                    //            if ((sSVAdJrsdctn1 != "-1") && (sSVAdTIN1 == string.Empty))
                    //            {
                    //                SVStatus = false;
                    //                obj.StoreEvent("99999", "", "", "", "additional TIN-1 / enter reasons for not being able to provide TIN for additional Jurisdiction missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                    //                lblEValErrMsg.Text = "Please enter additional TIN-1 / enter reasons for not being able to provide TIN for additional Jurisdiction for the Applicant " + Apln.FirstNm;
                    //                return SVStatus;
                    //            }
                    //        }
                    //    }
                    //    if ((sSVAdJrsdctn2 != "-1") && (sSVAdTIN2 == string.Empty && sSVRFNPTin == string.Empty))
                    //    {
                    //        SVStatus = false;
                    //        obj.StoreEvent("99999", "", "", "", "additional TIN-2 / enter reasons for not being able to provide TIN for additional Jurisdiction missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                    //        lblEValErrMsg.Text = "Please enter additional TIN-2 / enter reasons for not being able to provide TIN for additional Jurisdiction for the Applicant " + Apln.FirstNm;
                    //        return SVStatus;
                    //    }
                    //}
                    #endregion

                    #region FATCA
                    if (sSVPayTax == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Pay Tax missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please Select Pay Tax for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if (sSVUSCitizen == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "US citizen missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please Select US citizen for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if (sSVGreenCard == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Green Card missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please Select Green Card for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if (sSVRealEst == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Real Estate in USA missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please Select Real Estate in USA for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    if (sSVAsset == string.Empty)
                    {
                        SVStatus = false;
                        obj.StoreEvent("99999", "", "", "", "Assets held in USA missing for the Applicant " + Apln.FirstNm, Convert.ToString(Session["RefNo"]));
                        lblEValErrMsg.Text = "Please Select Assets held in USA for the Applicant " + Apln.FirstNm;
                        return SVStatus;
                    }
                    #endregion

                }


                if (sSVIsJntAc == string.Empty)
                {
                    SVStatus = false;
                    obj.StoreEvent("99999", "", "", "", "Isjoint missing", Convert.ToString(Session["RefNo"]));
                    return SVStatus;
                }

                if (sSVInvAmount == string.Empty)
                {
                    SVStatus = false;
                    obj.StoreEvent("99999", "", "", "", "Investment Amount missing", Convert.ToString(Session["RefNo"]));
                    lblEValErrMsg.Text = "Please enter the Investment amount";
                    return SVStatus;
                }
                if (sSVInvPeriod == string.Empty)
                {
                    SVStatus = false;
                    obj.StoreEvent("99999", "", "", "", "Investment Period missing", Convert.ToString(Session["RefNo"]));
                    lblEValErrMsg.Text = "Please select the deposit period";
                    return SVStatus;
                }
                if (sSVInvRateOfInt == string.Empty)
                {
                    SVStatus = false;
                    obj.StoreEvent("99999", "", "", "", "Rate of Interest missing", Convert.ToString(Session["RefNo"]));
                    lblEValErrMsg.Text = "Technical error please contact branch";
                    return SVStatus;
                }
                if (sSVOthBankName == string.Empty)
                {
                    SVStatus = false;
                    obj.StoreEvent("99999", "", "", "", "SOF Bank Name missing", Convert.ToString(Session["RefNo"]));
                    lblEValErrMsg.Text = "Please enter the bank name";
                    return SVStatus;
                }
                if (sSVOthBankSC == string.Empty)
                {
                    SVStatus = false;
                    obj.StoreEvent("99999", "", "", "", "SOF Bank Sortcode missing", Convert.ToString(Session["RefNo"]));
                    lblEValErrMsg.Text = "Please enter bank sortcode";
                    return SVStatus;
                }
                if (sSVOthBankAcNo == string.Empty)
                {
                    SVStatus = false;
                    obj.StoreEvent("99999", "", "", "", "SOF Bank Account Number missing", Convert.ToString(Session["RefNo"]));
                    lblEValErrMsg.Text = "Please enter bank account number";
                    return SVStatus;
                }
                if (sSVRepayIns == string.Empty)
                {
                    SVStatus = false;
                    obj.StoreEvent("99999", "", "", "", "Repayment instruction missing", Convert.ToString(Session["RefNo"]));
                    lblEValErrMsg.Text = "Please select the repayment instruction";
                    return SVStatus;
                }


                SVStatus = true;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return SVStatus;
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
    //20171229 validations added

    private void SaveDetails(string sAplctnSts)
    {

        try
        {
            string sType = Convert.ToString(Session["Type"]);
            string sKYCCheck = string.Empty;

            if (sType == "NA")
            {
                AccountDtl AcDtl = null;

                if (Session["AcDtl"] != null)
                {
                    AcDtl = (AccountDtl)Session["AcDtl"];
                }

                if (Session["AcOpenRefNo"] != null && Convert.ToString(Session["AcOpenRefNo"]) == AcDtl.ReferenceNo)
                {
                    string sSubmittedStatus = Convert.ToString(ViewState["SubStatus"]);
                    if (sSubmittedStatus == "D")
                    {
                        SaveAndSendEmail();
                    }
                    else if (sSubmittedStatus == "S")
                    {
                        ModalPopupExtSub.Show();
                        ModalPopupExtSub.BackgroundCssClass = "sctableBackground d-block";
                        PnlSub.CssClass = "w-90 d-block zindex_1";
                        lblSubRefNo.Text = AcDtl.ReferenceNo;
                    }
                }
                else
                {

                    Session["AcOpenRefNo"] = obj.CreateAcOpnRefNo(Methods.Customer.NewCust);

                    AcDtl.ReferenceNo = Session["AcOpenRefNo"].ToString();

                    Session["AcDtl"] = AcDtl;

                    if (!IsAcExist())
                    {
                        //Validate Applicants using Webservices ( GB group )

                        sKYCCheck = obj.RetPolValue("KYC", "CHECK");

                        AccountDtl AcD = GetAcDtl();

                        obj.StoreEvent("99999", "", "", "", "going to insert TDKEY values", Convert.ToString(Session["RefNo"]));

                        obj.ExecuteCommand("INSERT INTO TDKEY (TDREFNO,TDDATE,TDTIME) VALUES('" + AcD.ReferenceNo + "','" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("HHmmss") + "')");

                        obj.StoreEvent("99999", "", "", "", "TDKEY values inserted Successfully", Convert.ToString(Session["RefNo"]));

                        if (obj.SaveApplicant(sAplctnSts))
                        {
                            string sSubmittedStatus = Convert.ToString(ViewState["SubStatus"]);
                            if (sSubmittedStatus == "D")
                            {
                                SaveAndSendEmail();
                            }
                            else if (sSubmittedStatus == "S")
                            {
                                ModalPopupExtSub.Show();
                                ModalPopupExtSub.BackgroundCssClass = "sctableBackground d-block";
                                PnlSub.CssClass = "w-90 d-block zindex_1";
                                lblSubRefNo.Text = AcD.ReferenceNo;
                            }
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
                        obj.StoreEvent("88888", "", "", "", "Information already exist for the Reference No: " + AcD.ReferenceNo + " Kindly retrieve application and update your changes. Please contact the nearest branch if you have any query!", Convert.ToString(Session["RefNo"]));
                    }
                }
            }
            else if (sType == "RA")
            {
                //Validate Applicants using Webservices ( GB group )
                sKYCCheck = obj.RetPolValue("KYC", "CHECK");

                AccountDtl AcD = GetAcDtl();

                obj.StoreEvent("99999", "", "", "", "Going to delete values From TDAPPIND", Convert.ToString(Session["RefNo"]));
                //20181115-chellappa
                if ((AcD.ReferenceNo != null) && (AcD.ReferenceNo != ""))
                {
                    obj.ExecuteCommand("DELETE FROM TDAPPIND WHERE TDREFNO='" + AcD.ReferenceNo + "' OR TDTRNID = '" + AcD.ReferenceNo + "'");

                    obj.StoreEvent("99999", "", "", "", "Deleted values From TDAPPIND", Convert.ToString(Session["RefNo"]));

                    if (obj.SaveApplicant(sAplctnSts))
                    {
                        string sSubmittedStatus = Convert.ToString(ViewState["SubStatus"]);
                        if (sSubmittedStatus == "D")
                        {
                            SaveAndSendEmail();
                        }
                        else if (sSubmittedStatus == "S")
                        {
                            ModalPopupExtSub.Show();
                            ModalPopupExtSub.BackgroundCssClass = "sctableBackground d-block";
                            PnlSub.CssClass = "w-90 d-block zindex_1";
                            lblSubRefNo.Text = AcD.ReferenceNo;
                        }
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
    }

    private void DelDataFrmOTPTbl(string sEmailAddVal)
    {
        string fDelData = string.Empty;
        string fInsHisTbl = string.Empty;
        try
        {
            if (sEmailAddVal != null && sEmailAddVal != "")
            {
                obj.StoreEvent("99999", "", "", "", "Initiated to move the data from OTP table to history table.", Convert.ToString(Session["RefNo"]));
                fInsHisTbl = "SET IDENTITY_INSERT OTPFOREMAILHIST ON " +
                           " INSERT INTO OTPFOREMAILHIST (ID,OTP,EMOTPREFNO,EMOTPEMAIL,EMOTPGNDATE,EMOTPGNTIME,OTPATTEMPT,OTPRESENDCOUNT,OTPSTATUS,EMVERIFIED,EMISBLOCKED,EMBLOCKDATETIME,OTPCUSUID,EMOTPVERIFIEDTIME,EMOTPAPP) " +
                           " SELECT ID,OTP,EMOTPREFNO,EMOTPEMAIL,EMOTPGNDATE,EMOTPGNTIME,OTPATTEMPT,OTPRESENDCOUNT,OTPSTATUS,EMVERIFIED,EMISBLOCKED,EMBLOCKDATETIME,OTPCUSUID,EMOTPVERIFIEDTIME,EMOTPAPP " +
                           " FROM OTPFOREMAIL WHERE  EMOTPREFNO='" + Convert.ToString(Session["RefNo"]) + "' AND OTPSTATUS='Y' " +
                           " SET IDENTITY_INSERT OTPFOREMAILHIST OFF";
                obj.ExecuteCommand(fInsHisTbl);

                obj.StoreEvent("99999", "", "", "", "Initiated to delete the email address from OTP table", Convert.ToString(Session["RefNo"]));
                fDelData = "DELETE FROM OTPFOREMAIL WHERE EMOTPREFNO='" + Convert.ToString(Session["RefNo"]) + "' AND OTPSTATUS='Y'";
                obj.ExecuteCommand(fDelData);
                obj.StoreEvent("99999", "", "", "", "successfully deleted the email address from OTP table for email " + sEmailAddVal, Convert.ToString(Session["RefNo"]));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool SaveApplicant(string sAplnSts)
    {
        AccountDtl AcDtl = GetAcDtl();

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
                string sQryField, sQryValue, sCommonValues;
                string sRefNo, sIsJntAc, sInvAmount, sInvPeriod, sInvRateOfInt, sOthBankName, sOthBankSC, sOthBankAcNo, sRepayIns;
                double dInvAmt;

                sQryField = sQryValue = sCommonValues = string.Empty;
                sRefNo = sIsJntAc = sInvAmount = sInvPeriod = sInvRateOfInt = sOthBankName = sOthBankSC = sOthBankAcNo = sRepayIns = string.Empty;

                sRefNo = AcDtl.ReferenceNo;
                sIsJntAc = AcDtl.IsJntAc;

                sInvAmount = AcDtl.InvAmount;
                sInvPeriod = AcDtl.InvPeriod;
                sInvRateOfInt = AcDtl.InvRateOfInt;
                sOthBankName = AcDtl.OthBankName;
                sOthBankSC = AcDtl.OthBankSC;
                sOthBankAcNo = AcDtl.OthBankAcNo;
                sRepayIns = AcDtl.RepayIns;

                aplQry.Append("INSERT INTO TDAPPIND (TEMPREF,TDTRNID, TDREFNO,");
                aplQry.Append("TDACTYPE, TDINVAMT, TDINVPERIOD, TDINVRATINS, TDAIOTHRBNKNA, TDOTHSRCODE, TDOTHACNO, TDREPAYINS,TDISJOINT,");
                aplQry.Append("TDISPRIMARY, TDSEQUENCE,TDFATITLE, TDFANAME, TDMINAME, TDSURNAME, TDGNDR, TDFADOB,");
                aplQry.Append("TDFACITIZEN,TDFAMARITAL, TDFARESINO, TDFAMOBNO, TDFAEMAIL, TDPOB, TDMOTMDNME,");
                aplQry.Append("TDFACURADD1, TDFACURADD2, TDFACURADD3, TDFAPCODE, TDFACRY,");
                aplQry.Append("TDIDDTLS,PPTYPE,TDFAPANAME,DLTYPE,ISUKPERSON,PRIJRSDCTN,PRITIN,ADJRSDCTN1,ADTIN1,ADJRSDCTN2,ADTIN2,RESNNAPTIN,TDEMPDET,TDEDOTH,TDFAPASSNO, TDDOI, TDDOE, TDPOI, TDFADVLANO, TDDVLDOE, TDDVLPCODE,TDNATINSNO,");

                //aplQry.Append("");

                aplQry.Append("TDTOTAPP, TDSTATUS, TDDATE, TDTIME,Renewal,TDJOINTADD1,TDAUTHENID,UniqueSyndGLobalID,");
                aplQry.Append("TDAAC,SREFNO,TDFARESINCE,PRVPRESENT,TDFAPRADD1,TDFAPRADD2,TDFAPRADD3,TDFAPRPCODE,TDFAPRCRY)");

                if (sInvAmount != string.Empty && Double.TryParse(sInvAmount, out dInvAmt))
                {
                    sCommonValues = "'INDIVIDUAL DEPOSIT ACCOUNTS','" + obj.rplsSnglQots(sInvAmount) + "','" + obj.rplsSnglQots(sInvPeriod) + "','" + obj.rplsSnglQots(sInvRateOfInt) + "'," +
                                    "'" + obj.rplsSnglQots(sOthBankName) + "','" + obj.rplsSnglQots(sOthBankSC) + "','" + obj.rplsSnglQots(sOthBankAcNo) + "','" + obj.rplsSnglQots(sRepayIns) + "','" + obj.rplsSnglQots(AcDtl.IsJntAc) + "'";
                }
                else
                {
                    sCommonValues = "'INDIVIDUAL DEPOSIT ACCOUNTS',NULL,NULL,NULL," +
                                    "'" + obj.rplsSnglQots(sOthBankName).ToUpper() + "','" + obj.rplsSnglQots(sOthBankSC).ToUpper() + "','" + obj.rplsSnglQots(sOthBankAcNo).ToUpper() + "','" + obj.rplsSnglQots(sRepayIns).ToUpper() + "','" + obj.rplsSnglQots(AcDtl.IsJntAc).ToUpper() + "'";
                }
                int iApCount = 0;
                foreach (Applicant ApD in lstAplcnt)
                {
                    iApCount += 1;

                    string sPPNm, sPPNo, sPPIsDt, sPPExDt, sPPIC, sDvlcNo, sDvlcExDt, sDvlcPcode, sTDTOTAPP, sTDSTATUS, sTDDATE, sTDTIME, sRenewal, sIsSameAddr;
                    string sTDAUTHENID, sUniqueSyndGLobalID, sTDAAC, sSREFNO, sTDFARESINCE, sTDFAPRADD1, sTDFAPRADD2, sTDFAPRADD3, sTDFAPRPCODE, sTDFAPRCRY;
                    string sPPTYPE, sPRVPRESENT, sTDFAPANAME, sDLTYPE, EmpType, EmptypOth;
                    string IsUKperson;

                    string sGlobalId = Convert.ToString(Session["GLOBANAID"]);

                    sPPNm = sPPNo = sPPIsDt = sPPExDt = sPPIC = sDvlcNo = sDvlcExDt = sDvlcPcode = string.Empty;
                    sTDTOTAPP = sTDSTATUS = sTDDATE = sTDTIME = sRenewal = sIsSameAddr = sTDAAC = string.Empty;
                    sTDAUTHENID = sUniqueSyndGLobalID = sTDAAC = sSREFNO = sTDFARESINCE = sTDFAPRADD1 = sTDFAPRADD2 = sTDFAPRADD3 = sTDFAPRPCODE = sTDFAPRCRY = string.Empty;
                    sPPTYPE = sPRVPRESENT = sTDFAPANAME = sDLTYPE = EmpType = EmptypOth = IsUKperson = string.Empty;

                    sTDTOTAPP = AcDtl.appList.Count().ToString();

                    sTDDATE = DateTime.Now.ToString("yyyyMMdd");
                    sTDTIME = DateTime.Now.ToString("HHmmss");
                    sRenewal = "U";

                    sTDAAC = "A";
                    sSREFNO = obj.SearchRefNo();

                    if (sAplnSts == "D")
                        sTDSTATUS = "APN";
                    else
                        sTDSTATUS = "KYP";

                    if (ApD.UsePrimaryAddr.ToUpper() == "YES")
                        sIsSameAddr = "Y";
                    else
                        sIsSameAddr = "N";

                    if (ApD.PreAddr1.Length > 0 || ApD.PrePcode.Length > 0)
                        sPRVPRESENT = "Y";
                    else
                        sPRVPRESENT = "N";


                    if (ApD.IdenDtls.ToUpper() == "Driving Licence".ToUpper())
                    {
                        sDLTYPE = ApD.DrLceType;
                        sDvlcNo = ApD.IdenNo;
                        sDvlcExDt = obj.DTOC(ApD.DrLceExpDt);
                        sDvlcPcode = ApD.DrLcePCode;
                    }
                    else
                    {
                        if (ApD.IsUkPsPrt.ToUpper() == "UK")
                            sPPTYPE = "UK";
                        else
                            sPPTYPE = "IT";

                        sPPIC = ApD.PsPrtIsCntry;
                        //sPPIC = obj.DTOC(ApD.PsPrtIsCntry);
                        sPPNm = ApD.PsPrtName;
                        sPPNo = ApD.IdenNo;
                        sPPIsDt = obj.DTOC(ApD.PsPrtIssDt);
                        sPPExDt = obj.DTOC(ApD.PsPrtExpDt);
                    }

                    /* FATCA */
                    if (ApD.IsUSperson.ToUpper() == "YES")
                        IsUKperson = "Y";
                    else
                        IsUKperson = "N";
                    /* FATCA */

                    if (iApCount == 1)
                        aplQry.Append("SELECT '" + AcDtl.RegUniqueId.ToUpper() + "','','" + sRefNo.ToUpper() + "'," + sCommonValues + ",'YES','" + iApCount.ToString() + "',");
                    else
                        aplQry.Append("SELECT '" + AcDtl.RegUniqueId.ToUpper() + "','" + sRefNo.ToUpper() + "',''," + sCommonValues + ",'NO','" + iApCount.ToString() + "',");

                    aplQry.Append("'" + obj.rplsSnglQots(ApD.Title).ToUpper() + "','" + obj.rplsSnglQots(ApD.FirstNm).ToUpper() + "','" + obj.rplsSnglQots(ApD.MidNm).ToUpper() + "','" + obj.rplsSnglQots(ApD.SurNm).ToUpper() + "','" + obj.rplsSnglQots(ApD.Gender).ToUpper() + "','" + obj.rplsSnglQots(obj.DTOC(ApD.DOB)).ToUpper() + "',");
                    aplQry.Append("'" + obj.rplsSnglQots(ApD.Citizenship).ToUpper() + "','" + obj.rplsSnglQots(ApD.MaritalSts).ToUpper() + "','" + obj.rplsSnglQots(ApD.HomeTelNo).ToUpper() + "','" + obj.rplsSnglQots(ApD.MobileNo).ToUpper() + "','" + obj.rplsSnglQots(ApD.EmailAddr) + "','" + obj.rplsSnglQots(ApD.PlaceOfBirth).ToUpper() + "','" + obj.rplsSnglQots(ApD.MothersMaidenNm).ToUpper() + "',");
                    aplQry.Append("'" + obj.rplsSnglQots(ApD.CurDoorNo).ToUpper() + ";" + obj.rplsSnglQots(ApD.CurAddr1).ToUpper() + "','" + obj.rplsSnglQots(ApD.CurAddr2).ToUpper() + "','" + obj.rplsSnglQots(ApD.CurAddr3).ToUpper() + "','" + obj.rplsSnglQots(ApD.CurPcode).ToUpper() + "','" + obj.rplsSnglQots(ApD.CurCounty).ToUpper() + "',");
                    aplQry.Append("'" + obj.rplsSnglQots(ApD.IdenDtls).ToUpper() + "','" + obj.rplsSnglQots(sPPTYPE).ToUpper() + "','" + obj.rplsSnglQots(sPPNm).ToUpper() + "','" + obj.rplsSnglQots(sDLTYPE).ToUpper() + "','" + obj.rplsSnglQots(IsUKperson).ToUpper() + "','" + obj.rplsSnglQots(ApD.PriJrsdctn).ToUpper() + "','" + obj.rplsSnglQots(ApD.PriTIN).ToUpper() + "','" + obj.rplsSnglQots(ApD.AdJrsdctn1).ToUpper() + "','" + obj.rplsSnglQots(ApD.AdTIN1).ToUpper() + "','" + obj.rplsSnglQots(ApD.AdJrsdctn2).ToUpper() + "','" + obj.rplsSnglQots(ApD.AdTIN2).ToUpper() + "','" + obj.rplsSnglQots(ApD.ResnNAPTIN).ToUpper() + "','" + obj.rplsSnglQots(ApD.EmpType).ToUpper() + "','" + obj.rplsSnglQots(ApD.EmptypOth).ToUpper() + "','" + obj.rplsSnglQots(sPPNo).ToUpper() + "','" + obj.rplsSnglQots(sPPIsDt).ToUpper() + "','" + obj.rplsSnglQots(sPPExDt).ToUpper() + "','" + obj.rplsSnglQots(sPPIC).ToUpper() + "',");
                    aplQry.Append("'" + obj.rplsSnglQots(sDvlcNo).ToUpper() + "','" + obj.rplsSnglQots(sDvlcExDt).ToUpper() + "','" + obj.rplsSnglQots(sDvlcPcode).ToUpper() + "','" + obj.rplsSnglQots(ApD.NINO).ToUpper() + "',");
                    aplQry.Append("'" + obj.rplsSnglQots(sTDTOTAPP).ToUpper() + "','" + obj.rplsSnglQots(sTDSTATUS).ToUpper() + "','" + obj.rplsSnglQots(sTDDATE).ToUpper() + "','" + obj.rplsSnglQots(sTDTIME).ToUpper() + "','" + obj.rplsSnglQots(sRenewal).ToUpper() + "','" + obj.rplsSnglQots(sIsSameAddr).ToUpper() + "',");

                    //aplQry.Append("");

                    aplQry.Append("'" + "AuthId" + "','" + sGlobalId.ToUpper() + "','" + obj.rplsSnglQots(sTDAAC).ToUpper() + "','" + obj.rplsSnglQots(sSREFNO).ToUpper() + "','" + obj.rplsSnglQots(obj.DTOC(ApD.ResidingSince)).ToUpper() + "',");
                    aplQry.Append("'" + obj.rplsSnglQots(sPRVPRESENT).ToUpper() + "','" + obj.rplsSnglQots(ApD.PreDoorNo).ToUpper() + "','" + obj.rplsSnglQots(ApD.PreAddr1).ToUpper() + "','" + obj.rplsSnglQots(ApD.PreAddr3).ToUpper() + "','" + obj.rplsSnglQots(ApD.PrePcode).ToUpper() + "','" + obj.rplsSnglQots(ApD.PreCounty).ToUpper() + "' UNION ALL ");

                    if ((iApCount == 1) && (AcDtl.IsJntAc.ToUpper() == "No".ToUpper()))
                        break;

                }

                if (iApCount > 0)
                {
                    sQuery = aplQry.Remove(aplQry.Length - 10, 10).ToString();
                    obj.ExecuteCommand(sQuery);

                    Applicant oAplcnt = lstAplcnt[0];

                    dtApDtl = obj.ExecuteData("SELECT COUNT(ID) as CNT FROM TDAPPIND WHERE TDTRNID ='" + sRefNo + "' OR TDREFNO='" + sRefNo + "'");

                    if (AcDtl.IsJntAc.ToUpper() == "No".ToUpper())
                    {
                        if ((dtApDtl != null) && (dtApDtl.Rows.Count > 0) && (Convert.ToInt32(dtApDtl.Rows[0]["CNT"]) == 1))
                        {
                            if (Convert.ToString(Session["Type"]).ToUpper() == "NA")
                            {
                                string sQry = " INSERT INTO ACOPCUST (TRREF, CUFNAME, CULNAME, CUDOB, CUSEMAIL, CUSPWD, CUCTADD1, CUCTADD2, CUCTADD3, CUCTADD4, CUCOUNTRY, CUTEL, CUMOB,CUMODATE,CUMOTIME)" +
                                  " SELECT '" + obj.rplsSnglQots(AcDtl.ReferenceNo) + "','" + obj.rplsSnglQots(oAplcnt.FirstNm) + "','" + obj.rplsSnglQots(oAplcnt.SurNm) + "','" + obj.rplsSnglQots(obj.DTOC(oAplcnt.DOB)) + "','" + obj.rplsSnglQots(oAplcnt.EmailAddr) + "','" + "" + "','" + obj.rplsSnglQots(oAplcnt.CurDoorNo) + "','" + obj.rplsSnglQots(oAplcnt.CurAddr1) + "','" + obj.rplsSnglQots(oAplcnt.CurAddr2) + "','" + obj.rplsSnglQots(oAplcnt.CurAddr3) + "','" + obj.rplsSnglQots(oAplcnt.CurCounty) + "','" + obj.rplsSnglQots(oAplcnt.HomeTelNo) + "','" + obj.rplsSnglQots(oAplcnt.MobileNo) + "','" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("HHmmss") + "'";

                                obj.ExecuteCommand(sQry);
                            }

                            bStatus = true;
                        }
                    }
                    else
                    {
                        if ((dtApDtl != null) && (dtApDtl.Rows.Count > 0) && (Convert.ToInt32(dtApDtl.Rows[0]["CNT"]) == lstAplcnt.Count))
                        {
                            if (Convert.ToString(Session["Type"]).ToUpper() == "NA")
                            {
                                string sQry = " INSERT INTO ACOPCUST (TRREF, CUFNAME, CULNAME, CUDOB, CUSEMAIL, CUSPWD, CUCTADD1, CUCTADD2, CUCTADD3, CUCTADD4, CUCOUNTRY, CUTEL, CUMOB,CUMODATE,CUMOTIME)" +
                                  " SELECT '" + obj.rplsSnglQots(AcDtl.ReferenceNo) + "','" + obj.rplsSnglQots(oAplcnt.FirstNm) + "','" + obj.rplsSnglQots(oAplcnt.SurNm) + "','" + obj.rplsSnglQots(obj.DTOC(oAplcnt.DOB)) + "','" + obj.rplsSnglQots(oAplcnt.EmailAddr) + "','" + "" + "','" + obj.rplsSnglQots(oAplcnt.CurDoorNo) + "','" + obj.rplsSnglQots(oAplcnt.CurAddr1) + "','" + obj.rplsSnglQots(oAplcnt.CurAddr2) + "','" + obj.rplsSnglQots(oAplcnt.CurAddr3) + "','" + obj.rplsSnglQots(oAplcnt.CurCounty) + "','" + obj.rplsSnglQots(oAplcnt.HomeTelNo) + "','" + obj.rplsSnglQots(oAplcnt.MobileNo) + "','" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("HHmmss") + "'";

                                obj.ExecuteCommand(sQry);
                            }

                            bStatus = true;
                        }
                    }
                }
            }
            else
            {
                bStatus = false;
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
        int iGBScore = Convert.ToInt32(obj.RetPolValue("GBGAML", "PassScore"));
        try
        {
            List<Applicant> Applicants = objAcDtl.appList;
            Applicant oAp = (from appl in Applicants where appl.KYCstatus != "Y" select appl).FirstOrDefault();
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtAcDtl = null;
            AccountDtl AcD = GetAcDtl();
            dtAcDtl = obj.ExecuteData("SELECT ACID,ACOPENREFNO,REFNO FROM ACDTLPF WHERE ACOPENREFNO='" + AcD.ReferenceNo + "'");

            if ((dtAcDtl != null) && (dtAcDtl.Rows.Count > 0))
            {
                string scriptString = string.Empty;
                scriptString = "<script language=javascript>window.open('Loading.aspx?pg=Rpt');</script>";

                if (!ClientScript.IsStartupScriptRegistered("Startup"))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Startup", scriptString);
                }
            }
            else
            {
                lblAlert.Text = "No Detail Found to Print";
                obj.StoreEvent("88888", "", "", "", "No Detail Found to Print", Convert.ToString(Session["RefNo"]));
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
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
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
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
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
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
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
        return bStatus;
    }

    //private bool SaveAcDtl(AccountDtl AcD, string sAplctnSts)
    //{
    //    bool bStatus = false;
    //    string sQuery = string.Empty;
    //    string sValues = string.Empty;
    //    string sAcId = string.Empty;
    //    string sAppSts = "S";
    //    string sAcStatus = string.Empty;
    //    string sKYCCheck = obj.RetPolValue("KYC", "CHECK");
    //    //string sCreatedBy = string.Empty;

    //    sAppSts = sAplctnSts;

    //    sCreatedBy = AcD.ReferenceNo;

    //    DataTable dtAcDtl = null;

    //    try
    //    {
    //        if (sKYCCheck.ToUpper() == "YES")
    //        {
    //            sAcStatus = GetAcStatus(AcD);
    //        }
    //        else
    //        {
    //            sAcStatus = "KYP";
    //        }

    //        sQuery = " INSERT INTO ACDTLPF (" +
    //                " ACOPENREFNO,REFNO,ISJNTAC,[STATUS]," +
    //                " INVAMOUNT,INVPERIOD,INVRATINT,OTHBANKNAME,OTHBANKSC,OTHBANKACNO,REPAYINS," +
    //                " [CREDATE],[CRETIME],[CREATEDBY],APPSTS,NOOFJNTAPLNT)";

    //        sValues = " SELECT '" + AcD.ReferenceNo + "','" + AcD.RegUniqueId + "','" + AcD.IsJntAc + "','" + sAcStatus + "'," +
    //                  "'" + AcD.InvAmount + "','" + AcD.InvPeriod + "','" + AcD.InvRateOfInt + "','" + AcD.OthBankName + "'," +
    //                  "'" + AcD.OthBankSC + "','" + AcD.OthBankAcNo + "','" + AcD.RepayIns + "'," +
    //                  "'" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("HHmmss") + "','" + sCreatedBy + "','" + sAppSts + "','" + AcD.NoOfApplicants + "'";
    //        obj.ExecuteCommand(sQuery + sValues);

    //        dtAcDtl = obj.ExecuteData("SELECT ACID,ACOPENREFNO,REFNO FROM ACDTLPF WHERE ACOPENREFNO='" + AcD.ReferenceNo + "'");
    //        if ((dtAcDtl != null) && (dtAcDtl.Rows.Count > 0))
    //        {
    //            if (dtAcDtl.Rows[0]["ACOPENREFNO"].ToString().ToUpper() == AcD.ReferenceNo.ToUpper())
    //            {
    //                sAcId = dtAcDtl.Rows[0]["ACID"].ToString();
    //                bStatus = true;
    //                if (SaveApplicant(AcD, sAcId))
    //                {
    //                    bStatus = true;
    //                }
    //                else
    //                {
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

    //private bool UpdateAcDtl(AccountDtl AcD, string sAplctnSts)
    //{
    //    bool bStatus = false;
    //    string sQuery = string.Empty;
    //    string sAcId = string.Empty;
    //    string sAcStatus = string.Empty;
    //    string sAppSts = "S";
    //    string sKYCCheck = obj.RetPolValue("KYC", "CHECK");

    //    sAppSts = sAplctnSts;

    //    if ((Convert.ToString(Session["RefNo"]).Length > 0) && (Convert.ToString(Session["RefNo"]) != null))
    //        sModifiedBy = Convert.ToString(Session["RefNo"]);
    //    else
    //        sModifiedBy = AcD.ReferenceNo;

    //    try
    //    {

    //        if (sKYCCheck.ToUpper() == "YES")
    //        {
    //            sAcStatus = GetAcStatus(AcD);
    //        }
    //        else
    //        {
    //            sAcStatus = "KYP";
    //        }

    //        sQuery = " UPDATE ACDTLPF SET" +

    //                 " ISJNTAC='" + AcD.IsJntAc + "',[STATUS]='" + sAcStatus + "',[MODDATE]='" + DateTime.Now.ToString("yyyyMMdd") + "'," +
    //                 " INVAMOUNT='" + AcD.InvAmount + "',INVPERIOD='" + AcD.InvPeriod + "',INVRATINT='" + AcD.InvRateOfInt + "',OTHBANKNAME='" + AcD.OthBankName + "'," +
    //                 " OTHBANKSC='" + AcD.OthBankSC + "',OTHBANKACNO='" + AcD.OthBankAcNo + "',REPAYINS='" + AcD.RepayIns + "'," +
    //                 " [MODTIME]='" + DateTime.Now.ToString("HHmmss") + "',[MODIFIEDBY]='" + sModifiedBy + "', APPSTS='" + sAppSts + "',[NOOFJNTAPLNT]='" + AcD.NoOfApplicants + "'" +
    //                 " WHERE ACID='" + AcD.AcId + "' AND ACOPENREFNO='" + AcD.ReferenceNo + "'";

    //        obj.ExecuteCommand(sQuery);
    //        bStatus = true;

    //        obj.ExecuteCommand("INSERT INTO APDTLPF_HIST SELECT *,'" + DateTime.Now.ToString("yyyyMMddHHmmss") + "' FROM APDTLPF WHERE ACID='" + AcD.AcId + "' AND ACOPENREFNO='" + AcD.ReferenceNo + "'");

    //        sQuery = "DELETE FROM APDTLPF WHERE ACID='" + AcD.AcId + "' AND ACOPENREFNO='" + AcD.ReferenceNo + "'";
    //        obj.ExecuteCommand(sQuery);

    //        if (SaveApplicant(AcD, AcD.AcId))
    //        {
    //            bStatus = true;
    //        }
    //        else
    //        {
    //            bStatus = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
    //        bStatus = false;
    //        throw ex;
    //    }
    //    return bStatus;
    //}

    //private bool SaveApplicant(AccountDtl AcDtl, string AcId)
    //{
    //    bool bStatus = false;
    //    string sQuery = string.Empty;
    //    DataTable dtApDtl = null;
    //    List<Applicant> lstAplcnt = null;
    //    StringBuilder aplQry = new StringBuilder("");

    //    try
    //    {
    //        lstAplcnt = AcDtl.appList;

    //        if (lstAplcnt != null && lstAplcnt.Count > 0)
    //        {
    //            aplQry.Append("INSERT INTO APDTLPF (");
    //            aplQry.Append("ACID,ACOPENREFNO,REFNO,ISPRIMARY,SEQUENCE,TITLE,");
    //            aplQry.Append("FIRSTNM,MIDNM,SURNM,GENDER,IDENTDTLS,");
    //            aplQry.Append("ISUKPSPRT,PSPORTISDT,PSPORTEXDT,PSPORTISCNTRY,IDENTITYNO,CITIZENSHIP,");
    //            aplQry.Append("MARITALSTS,MARITALSTSOTH,DOB,HOMETELNO,MOBILENO,");
    //            aplQry.Append("EMAILADDR,USEPRIMADDR,CURDRNO,CURADDR1,CURADDR2,CURADDR3,CURCOUNTY,");
    //            aplQry.Append("CURPCODE,CURCNTRY,RESIDINGSINCE,MAILINGADDR,PREDRNO,PREADDR1,");
    //            aplQry.Append("PREADDR2,PREADDR3,PRECOUNTY,PREPCODE,PRECNTRY,");
    //            aplQry.Append("AUTHPRVDR,AUTHREQREF,AUTHID,AUTHKEY,AUTHSCORE,AUTHRESULT,");
    //            aplQry.Append("NINO,MPAN,OTHBANKSCODE,OTHBANKACNO,");
    //            aplQry.Append("AUTHDATE,AUTHTIME,KYCSTATUS)");

    //            int i = 0;
    //            foreach (Applicant ApD in lstAplcnt)
    //            {
    //                i += 1;

    //                aplQry.Append(" SELECT '" + AcId + "','" + AcDtl.ReferenceNo + "','" + AcDtl.RegUniqueId + "','" + ApD.IsPrimary + "','" + Convert.ToString(i) + "','" + ApD.Title + "',");

    //                aplQry.Append("'" + ApD.FirstNm + "','" + ApD.MidNm + "','" + ApD.SurNm + "','" + ApD.Gender + "','" + ApD.IdenDtls + "',");
    //                aplQry.Append("'" + ApD.IsUkPsPrt + "','" + obj.DTOC(ApD.PsPrtIssDt) + "','" + obj.DTOC(ApD.PsPrtExpDt) + "','" + ApD.PsPrtIsCntry + "','" + ApD.IdenNo + "','" + ApD.Citizenship + "',");
    //                aplQry.Append("'" + ApD.MaritalSts + "','" + ApD.MaritalStsOth + "','" + obj.DTOC(ApD.DOB) + "','" + ApD.HomeTelNo + "','" + ApD.MobileNo + "',");
    //                aplQry.Append("'" + ApD.EmailAddr + "','" + ApD.UsePrimaryAddr + "','" + ApD.CurDoorNo + "','" + ApD.CurAddr1 + "','" + ApD.CurAddr2 + "','" + ApD.CurAddr3 + "','" + ApD.CurCounty + "',");
    //                aplQry.Append("'" + ApD.CurPcode + "','" + ApD.CurCntry + "','" + obj.DTOC(ApD.ResidingSince) + "','" + ApD.MailingAddr + "','" + ApD.PreDoorNo + "','" + ApD.PreAddr1 + "',");
    //                aplQry.Append("'" + ApD.PreAddr2 + "','" + ApD.PreAddr3 + "','" + ApD.PreCounty + "','" + ApD.PrePcode + "','" + ApD.PreCntry + "',");
    //                aplQry.Append("'" + ApD.AuthProvider + "','" + ApD.AuthReqRef + "','" + ApD.AuthID + "','" + ApD.AuthKey + "','" + ApD.AuthScore + "','" + ApD.AuthResult + "',");
    //                aplQry.Append("'" + ApD.NINO + "','" + ApD.MPAN + "','" + ApD.OthBankSrtCd + "','" + ApD.OthBankAcNo + "',");
    //                aplQry.Append("'" + ApD.AuthDate + "','" + ApD.AuthTime + "','" + ApD.KYCstatus + "' UNION ALL ");

    //                if (i == 1)
    //                {
    //                    if (AcDtl.IsJntAc.ToUpper() == "No".ToUpper())
    //                        break;
    //                }
    //            }

    //            if (aplQry.Length > 0)
    //            {
    //                sQuery = aplQry.Remove(aplQry.Length - 10, 10).ToString();
    //                obj.ExecuteCommand(sQuery);

    //                dtApDtl = obj.ExecuteData("SELECT COUNT(ACID) as CNT FROM APDTLPF WHERE ACID='" + AcId + "'");
    //                if (AcDtl.IsJntAc.ToUpper() == "No".ToUpper())
    //                {
    //                    if ((dtApDtl != null) && (dtApDtl.Rows.Count > 0) && (Convert.ToInt32(dtApDtl.Rows[0]["CNT"]) == 1))
    //                    {
    //                        bStatus = true;
    //                    }
    //                }
    //                else
    //                {
    //                    if ((dtApDtl != null) && (dtApDtl.Rows.Count > 0) && (Convert.ToInt32(dtApDtl.Rows[0]["CNT"]) == lstAplcnt.Count))
    //                    {
    //                        bStatus = true;
    //                    }
    //                }
    //            }

    //            if ((sCreatedBy.Length > 0))
    //            {
    //                //obj.StoreEvent("99999", "", "", "", "The A/C opening application " + AcDtl.ReferenceNo + " has been successfully created by " + sCreatedBy, sCreatedBy);
    //                obj.StoreEvent("65101", AcDtl.ReferenceNo, sCreatedBy, "", "", sCreatedBy);
    //            }

    //            if ((sModifiedBy.Length > 0))
    //            {
    //                if (AcDtl.AppStatus.ToUpper() == "S")
    //                {
    //                    //obj.StoreEvent("99999", "", "", "", "The A/C opening application " + AcDtl.ReferenceNo + " has been successfully modified by : " + sModifiedBy, sModifiedBy);
    //                    obj.StoreEvent("65102", AcDtl.ReferenceNo, sModifiedBy, "", "", sModifiedBy);
    //                }
    //                else
    //                {
    //                    //obj.StoreEvent("99999", "", "", "", "The A/C opening application " + AcDtl.ReferenceNo + " has been successfully completed by " + sModifiedBy, sModifiedBy);
    //                    obj.StoreEvent("65104", AcDtl.ReferenceNo, sModifiedBy, "", "", sModifiedBy);
    //                }
    //            }
    //        }
    //        else
    //        {

    //            bStatus = true;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        obj.StoreEvent("99999", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
    //        throw ex;
    //    }
    //    return bStatus;
    //}

    protected void btnExit_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Index.aspx", false);

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
        try
        {
            string sSubmittedStatus = Convert.ToString(ViewState["SubStatus"]);
            if (sSubmittedStatus == "D")
            {
                //if (SaveAndSendEmail())
                //{
                AccountDtl AcD = GetAcDtl();
                Cache.Remove("Cnfrmtn" + Convert.ToString(Session["loginid"]));
                Response.Redirect("Confirmation.aspx?R=" + obj.UrlEncrypt64("Ref=" + AcD.ReferenceNo + " & AplnSts=D"), false);
                //}

                ModalPopupExtender2.Hide();
                ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-none";
                PnlPwdReg.CssClass = "d-none zindex_1";
            }
            else if (sSubmittedStatus == "S")
            {
                string sQuery, sTDSTATUS;

                //if (IsValidData())
                //{
                //if (Authenticate())  //commented 2023
                if (AuthenticateV2())  //New callML service 2023
                {
                    obj.StoreEvent("99999", "", "", "", "CallML KYS initiated process", Convert.ToString(Session["RefNo"]));

                    sQuery = sTDSTATUS = string.Empty;

                    try
                    {
                        SendKYCSuccessEmail();
                    }
                    catch
                    {

                    }


                    sTDSTATUS = "KYS";

                    AccountDtl AcD = GetAcDtl();

                    //CALLML 
                    sQuery = "update tdappind set TDGENAGG='Y', TDRECOFFER='N', TDSTATUS='" + sTDSTATUS + "', TDTOTAPP='" + Convert.ToString(AcD.appList.Count) + "', TDDATE='" + DateTime.Now.ToString("yyyyMMdd") + "', TDTIME='" + DateTime.Now.ToString("HHmmss") + "' where  TDREFNO = '" + AcD.ReferenceNo + "'";
                    obj.ExecuteCommand(sQuery);

                    //update Joint Customer Status


                    sQuery = "UPDATE TDAPPIND SET TDSTATUS='" + sTDSTATUS + "' WHERE TDTRNID='" + AcD.ReferenceNo + "'";
                    obj.ExecuteCommand(sQuery);

                    obj.StoreEvent("99999", "", "", "", "Updated Status KYS", Convert.ToString(Session["RefNo"]));

                    //Customer Feedback - chellappa 20200928
                    ModalPopupExtender2.Hide();
                    ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-none";
                    PnlPwdReg.CssClass = "d-none zindex_1";

                    ModalPopFeedBck.Show();
                    ModalPopFeedBck.BackgroundCssClass = "sctableBackground d-block";
                    PnlFB.CssClass = "w-90 d-block zindex_1";
                    //Customer Feedback - chellappa 20200928

                    //commented for Customer Feedback process and move to SendFeedBackEmail - chellappa 20200928
                    //Cache.Remove("Cnfrmtn" + Convert.ToString(Session["loginid"]));
                    //Response.Redirect("Confirmation.aspx?R=" + obj.UrlEncrypt64("Ref=" + AcD.ReferenceNo + " & AplnSts=S"), false);
                    //commented for Customer Feedback process and move to SendFeedBackEmail - chellappa 20200928

                }
                else
                {
                    obj.StoreEvent("99999", "", "", "", "CallML KYP initiated process", Convert.ToString(Session["RefNo"]));

                    sQuery = sTDSTATUS = string.Empty;
                    sTDSTATUS = "KYP";

                    try
                    {
                        SendKYCPendingEmail();
                    }
                    catch
                    {

                    }
                    AccountDtl AcD = GetAcDtl();

                    //CALLML 
                    sQuery = "update tdappind set TDGENAGG='Y', TDRECOFFER='N', TDSTATUS='" + sTDSTATUS + "', TDTOTAPP='" + Convert.ToString(AcD.appList.Count) + "', TDDATE='" + DateTime.Now.ToString("yyyyMMdd") + "', TDTIME='" + DateTime.Now.ToString("HHmmss") + "' where  TDREFNO = '" + AcD.ReferenceNo + "'";
                    obj.ExecuteCommand(sQuery);

                    //update Joint Customer Status
                    sQuery = "UPDATE TDAPPIND SET TDSTATUS='" + sTDSTATUS + "' WHERE TDTRNID='" + AcD.ReferenceNo + "'";
                    obj.ExecuteCommand(sQuery);
                    obj.StoreEvent("99999", "", "", "", "Updated Status KYP", Convert.ToString(Session["RefNo"]));

                    //Customer Feedback - chellappa 20200928
                    ModalPopupExtender2.Hide();
                    ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-none";
                    PnlPwdReg.CssClass = "w-90 d-none zindex_1";

                    ModalPopFeedBck.Show();
                    ModalPopFeedBck.BackgroundCssClass = "sctableBackground d-block";
                    PnlFB.CssClass = "w-90 d-block zindex_1";
                    //Customer Feedback - chellappa 20200928

                    //commented for Customer Feedback process and move to SendFeedBackEmail - chellappa 20200928
                    //Cache.Remove("Cnfrmtn" + Convert.ToString(Session["loginid"]));
                    //Response.Redirect("Confirmation.aspx?R=" + obj.UrlEncrypt64("Ref=" + AcD.ReferenceNo + " & AplnSts=S"), false);
                    //commented for Customer Feedback process and move to SendFeedBackEmail - chellappa 20200928

                }
                //}

                //2024
                AccountDtl AcDDel = GetAcDtl();
                DelDataFrmOTPTbl(AcDDel.appList[0].EmailAddr);
                //End
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            Response.Redirect("GenError.aspx", false);

        }
    }

    //Customer Feedback - chellappa 20200928
    protected void btnFB_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsFBValid())
            {
                obj.StoreEvent("99999", "", "", "", "Start the Customer Feedback email process", Convert.ToString(Session["RefNo"]));
                SendFeedBackEmail();
                obj.StoreEvent("99999", "", "", "", "End the Customer Feedback email process", Convert.ToString(Session["RefNo"]));
                lblFBAlert.Text = "";
                ModalPopFeedBck.Hide();
                ModalPopFeedBck.BackgroundCssClass = "sctableBackground d-none";
                PnlFB.CssClass = "w-90 d-none zindex_1";
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private void SendFeedBackEmail()
    {
        try
        {
            string sWebsite = string.Empty;
            string sAppl = string.Empty;
            string sIntRate = string.Empty;
            string sPerf = string.Empty;
            string content = string.Empty;
            string sMsgSubject = string.Empty;
            string sLogo = string.Empty;
            string MsgBody = string.Empty;
            string sHeader = string.Empty;
            string sFooter = string.Empty;
            string FBMsgTo = string.Empty;

            string FBDESC = string.Empty;

            sWebsite = rblWeb.SelectedValue;
            sAppl = rblApp.SelectedValue;
            sIntRate = rblIntRat.SelectedValue;
            sPerf = rblAllPer.SelectedValue;




            AccountDtl sFBAcDtl = GetAcDtl();
            if (sFBAcDtl != null && sFBAcDtl.appList != null && sFBAcDtl.appList.Count > 0)
            {
                Applicant oApDtl = sFBAcDtl.appList[0];


                string pName = obj.RetPolValue("UBI", "PRODUCTNAME");

                sLogo = obj.RetPolValue("MAIL", "LOGO");
                sHeader = obj.RetPolValue("MAIL", "HEADER");
                sFooter = obj.RetPolValue("MAIL", "FOOTER");
                sHeader = sHeader.Replace("UBIUKLOGO", sLogo);

                content = obj.GetContent("Customer Feedback");

                content = content.Replace("(PRODUCTNAME)", pName);
                content = content.Replace("(CUSTNAME)", oApDtl.Title.ToUpper() + " " + oApDtl.FirstNm.ToUpper() + " " + oApDtl.MidNm.ToUpper() + " " + oApDtl.SurNm.ToUpper());
                content = content.Replace("(DATE)", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                content = content.Replace("(REFNO)", sFBAcDtl.ReferenceNo.ToUpper());
                content = content.Replace("(CUSTEMAIL)", oApDtl.EmailAddr);

                sMsgSubject = "UBI(UK) - Customer Feedback Notification";


                if (sWebsite != string.Empty)
                {
                    content = content.Replace("(WEB)", sWebsite);
                }
                else
                {
                    sWebsite = "NO RESPONSE";
                    content = content.Replace("(WEB)", sWebsite);
                }

                if (sAppl != string.Empty)
                {
                    content = content.Replace("(APP)", sAppl);
                }
                else
                {
                    sAppl = "NO RESPONSE";
                    content = content.Replace("(APP)", sAppl);
                }

                if (sIntRate != string.Empty)
                {
                    content = content.Replace("(INTRAT)", sIntRate);
                }
                else
                {
                    sIntRate = "NO RESPONSE";
                    content = content.Replace("(INTRAT)", sIntRate);
                }

                if (sPerf != string.Empty)
                {
                    content = content.Replace("(PER)", sPerf);
                }
                else
                {
                    sPerf = "NO RESPONSE";
                    content = content.Replace("(PER)", sPerf);
                }


                FBMsgTo = obj.RetPolValue("MAIL", "CUSTFEEDBCK");

                MsgBody = sHeader + content + sFooter;


                //Insert FB Rating Value
                string sInsFBVal = string.Empty;
                sInsFBVal = "INSERT INTO FBRATING ([DATE],[TIME],FBREFNO,FBRATUPBWEB,FBRATUPBAPP,FBRATUPBIRATE,FBRATUPBOALL) " +
                            "VALUES('" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("HHmmss") + "','" + sFBAcDtl.ReferenceNo.ToUpper() + "','" + sWebsite + "','" + sAppl + "','" + sIntRate + "','" + sPerf + "')";
                obj.ExecuteCommand(sInsFBVal);
                obj.StoreEvent("88888", "", "", "", "Customer Feedback vales are successfully added", Convert.ToString(Session["RefNo"]));


                try
                {
                    if (obj.SendEmailMessage(FBMsgTo, sMsgSubject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer))
                    {
                        try
                        {
                            obj.StoreEvent("99999", "", "", "", "Customer Feedback email sent successfully", Convert.ToString(Session["RefNo"]));
                        }
                        catch (Exception ex)
                        { }
                    }
                    else
                    {
                        try
                        {
                            obj.StoreEvent("88888", "", "", "", "Customer Feedback email sending failure", Convert.ToString(Session["RefNo"]));
                        }
                        catch (Exception ex)
                        { }
                    }
                }
                catch (Exception exinr)
                {
                    obj.StoreEvent("88888", "", "", "", "Unable to send the saved application email. " + obj.replaceSplChr(exinr.ToString()), Convert.ToString(Session["RefNo"]));
                }
                //obj.SendEmailFromUBI(sMsgTo, sMsgSubject, content);

                Cache.Remove("Cnfrmtn" + Convert.ToString(Session["loginid"]));
                Response.Redirect("Confirmation.aspx?R=" + obj.UrlEncrypt64("Ref=" + sFBAcDtl.ReferenceNo + " & AplnSts=S"), false);
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private Boolean IsFBValid()
    {
        bool bStatus = false;
        try
        {
            string Website = rblWeb.SelectedValue;
            string Appl = rblApp.SelectedValue;
            string IntRate = rblIntRat.SelectedValue;
            string Perf = rblAllPer.SelectedValue;

            if (Website == string.Empty && Appl == string.Empty && IntRate == string.Empty && Perf == string.Empty)
            {
                ModalPopFeedBck.Show();
                ModalPopFeedBck.BackgroundCssClass = "sctableBackground d-block";
                PnlFB.CssClass = "w-90 d-block zindex_1";
                lblFBAlert.Text = "Please select atleast three option to complete the feedback.";
                rblWeb.Focus();
                bStatus = false;
                return bStatus;
            }

            int sCount = 0;

            if (Website != string.Empty)
            {
                sCount = sCount + 1;
            }
            if (Appl != string.Empty)
            {
                sCount = sCount + 1;
            }
            if (IntRate != string.Empty)
            {
                sCount = sCount + 1;
            }
            if (Perf != string.Empty)
            {
                sCount = sCount + 1;
            }

            if (sCount < 3)
            {
                ModalPopFeedBck.Show();
                ModalPopFeedBck.BackgroundCssClass = "sctableBackground d-block";
                PnlFB.CssClass = "w-90 d-block zindex_1";
                lblFBAlert.Text = "Please select atleast three option to complete the feedback.";
                rblWeb.Focus();
                bStatus = false;
                //return bStatus;
            }
            else
            {
                bStatus = true;
                //return bStatus; 
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return bStatus;
    }
    //Customer Feedback - chellappa 20200928

    #region New callML service using API 2023
    //New callML service using API CALL 2023 - Chellappa and Vignesh    
    private void genXmlToString(string sRefNo)
    {
        m_xmlIn = string.Empty;

        XmlDocument xmlDoc = new XmlDocument();
        AccountDtl AcD = GetAcDtl();
        // Create the root element <callvalidate>
        XmlElement callvalidateElement = xmlDoc.CreateElement("callvalidate");

        // Create the <authentication> element and its child elements
        XmlElement authenticationElement = xmlDoc.CreateElement("authentication");
        authenticationElement.AppendChild(CreateElementWithText(xmlDoc, "company", obj.RetPolValue("CALLVALIDATENEW", "COMPANY")));
        authenticationElement.AppendChild(CreateElementWithText(xmlDoc, "username", obj.RetPolValue("CALLVALIDATENEW", "USERNAME")));
        authenticationElement.AppendChild(CreateElementWithText(xmlDoc, "password", obj.RetPolValue("CALLVALIDATENEW", "PASSWORD")));

        // Create the <application> element
        XmlElement applicationElement = CreateElementWithText(xmlDoc, "application", obj.RetPolValue("CALLVALIDATENEW", "APPLICATION"));

        // Create the <ChecksRequired> element and its child elements
        XmlElement checksRequiredElement = xmlDoc.CreateElement("ChecksRequired");
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "BankStandard", "no"));
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "BankEnhanced", "no"));
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "CardLive", "no"));
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "CardEnhanced", "no"));
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "IDEnhanced", "yes"));
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "NCOAAlert", "no"));
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "CallValidate3D", "no"));
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "DeliveryFraud", "no"));
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "CreditScore", "yes"));
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "RealTimeFraudAlerts", "no"));
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "DeviceRisk", "no"));
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "EmailRisk", "no"));
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "EmailID", "no"));
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "WatchlistScreening", "no"));
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "MobileID", "no"));
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "MobileRisk", "no"));
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "MobileSimSwap", "no"));
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "MobileRedirect", "no"));
        checksRequiredElement.AppendChild(CreateElementWithText(xmlDoc, "MobileKYC", "no"));

        // Create the <sessions> element and its child elements
        XmlElement sessionsElement = xmlDoc.CreateElement("sessions");
        XmlElement sessionElement = xmlDoc.CreateElement("session");
        sessionElement.SetAttribute("RID", Convert.ToString(AcD.ReferenceNo));
        XmlElement dataElement = xmlDoc.CreateElement("data");

        DataTable dTGetData = new DataTable();

        string sGetData = "SELECT top 1 * FROM TDAPPIND WHERE SREFNO='" + sRefNo + "' ORDER BY SREFNO";
        dTGetData = obj.ExecuteData(sGetData);

        if (dTGetData != null || dTGetData.Rows.Count > 0)
        {
            foreach (DataRow drGD in dTGetData.Rows)
            {
                // Create the <Personalinformation> element and its child elements
                XmlElement personalInformationElement = xmlDoc.CreateElement("Personalinformation");
                XmlElement individualDetailsElement = xmlDoc.CreateElement("IndividualDetails");
                individualDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Dateofbirth", obj.CTODate(obj.NullToSpace(drGD["TDFADOB"]))));
                individualDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Title", obj.NullToSpace(drGD["TDFATITLE"])));
                individualDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Firstname", obj.NullToSpace(drGD["TDFANAME"])));
                individualDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Othernames", obj.NullToSpace(drGD["TDMINAME"])));
                individualDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Surname", obj.NullToSpace(drGD["TDSURNAME"])));
                individualDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Phonenumber", obj.NullToSpace(drGD["TDFAMOBNO"])));
                individualDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Emailaddress", obj.NullToSpace(drGD["TDFAEMAIL"])));
                individualDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "addrlessthan12months", "false"));
                individualDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Passportline1", obj.NullToSpace(drGD["TDFAPANAME"])));
                individualDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Passportline2", obj.NullToSpace(drGD["TDFAPASSNO"])));
                individualDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Drivinglicensenumber", obj.NullToSpace(drGD["TDFADVLANO"])));
                individualDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "PassportExpiryDate", obj.CTODate(obj.NullToSpace(drGD["TDDOE"]))));
                individualDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "IPAddress", ""));
                personalInformationElement.AppendChild(individualDetailsElement);

                // Create the <AddressDetails> element and its child elements
                XmlElement addressDetailsElement = xmlDoc.CreateElement("AddressDetails");
                addressDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Abodenumber", obj.NullToSpace(drGD["TDFACURADD1"])));
                addressDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Buildingnumber", ""));
                addressDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Buildingname", ""));
                addressDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Address1", obj.NullToSpace(drGD["TDFACURADD2"])));
                addressDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Address2", ""));
                addressDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Address3", ""));
                addressDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Town", obj.NullToSpace(drGD["TDFACURADD3"])));
                addressDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Postcode", obj.NullToSpace(drGD["TDFAPCODE"])));
                addressDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Previousabodenumber", ""));
                addressDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Previousbuildingnumber", ""));
                addressDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Previousbuildingname", ""));
                addressDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Previousaddress1", ""));
                addressDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Previousaddress2", ""));
                addressDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Previousaddress3", ""));
                addressDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Previoustown", ""));
                addressDetailsElement.AppendChild(CreateElementWithText(xmlDoc, "Previouspostcode", ""));
                personalInformationElement.AppendChild(addressDetailsElement);
                dataElement.AppendChild(personalInformationElement);
                sessionElement.AppendChild(dataElement);
                sessionsElement.AppendChild(sessionElement);

                // Build the XML structure
                callvalidateElement.AppendChild(authenticationElement);
                callvalidateElement.AppendChild(applicationElement);
                callvalidateElement.AppendChild(checksRequiredElement);
                callvalidateElement.AppendChild(sessionsElement);

                // Add the root element to the XML document
                xmlDoc.AppendChild(callvalidateElement);

                // Convert the XML document to a string
                m_xmlIn = xmlDoc.OuterXml;
            }
        }
    }

    static XmlElement CreateElementWithText(XmlDocument xmlDoc, string elementName, string text)
    {
        XmlElement element = xmlDoc.CreateElement(elementName);
        element.InnerText = text;
        return element;
    }

    private bool AuthenticateV2()
    {
        bool bValidApplicant = false;
        try
        {
            AccountDtl AcD = GetAcDtl();

            string sGetData = null;
            DataTable dTGetData = new DataTable();
            string sOurRefno = string.Empty;
            //sGetData = "SELECT top 1 * FROM TDAPPIND WHERE TDREFNO='" + AcD.ReferenceNo + "' or TDTRNID='" + AcD.ReferenceNo + "' ORDER BY SREFNO";
            sGetData = "SELECT * FROM TDAPPIND WHERE TDREFNO='" + AcD.ReferenceNo + "' or TDTRNID='" + AcD.ReferenceNo + "' ORDER BY SREFNO";
            dTGetData = obj.ExecuteData(sGetData);

            if (dTGetData == null || dTGetData.Rows.Count == 0)
            {
                bValidApplicant = false;
            }
            else
            {
                bool bEAuthSts = false;
                string sEAuthExcMsg = string.Empty;
                foreach (DataRow drGD in dTGetData.Rows)
                {
                    try
                    {
                        sOurRefno = string.Empty;
                        m_xmlOut = string.Empty;
                        //searchDefinition = objCallML6.Search06b(searchDefinition);
                        genXmlToString(obj.NullToSpace(drGD["SREFNO"]));
                        string url = obj.RetPolValue("CALLVALIDATENEW", "URL");
                        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                        HttpWebRequest oHttp = (HttpWebRequest)WebRequest.Create(url);
                        oHttp.Method = "POST";
                        byte[] postBuffer = System.Text.Encoding.ASCII.GetBytes(m_xmlIn);
                        oHttp.ContentLength = postBuffer.Length;
                        Stream postData = oHttp.GetRequestStream();
                        postData.Write(postBuffer, 0, postBuffer.Length);
                        postData.Close();

                        // Process the Response.
                        HttpWebResponse myResponse = (HttpWebResponse)oHttp.GetResponse();
                        StreamReader loResponseStream = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.ASCII);
                        m_xmlOut = loResponseStream.ReadToEnd();
                        myResponse.Close();
                        loResponseStream.Close();
                        bEAuthSts = true;
                        obj.StoreEvent("99999", "", "", "", "CALLML API is end", Convert.ToString(Session["RefNo"]));
                    }
                    catch (Exception ex)
                    {
                        bEAuthSts = false;
                        obj.StoreEvent("99999", "", "", "", "Exception while consuming the CallML Service", Convert.ToString(Session["RefNo"]));
                    }

                    if (bEAuthSts)
                    {
                        obj.StoreEvent("99999", "", "", "", "Ready to insert CALLML values", Convert.ToString(Session["RefNo"]));

                        ClearCallMLValues();
                        XmlTextReader xmltr = new XmlTextReader(new StringReader(m_xmlOut));

                        while (xmltr.Read())
                        {

                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("passportwarning"))
                            {
                                PPWarning = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("dvlawarning"))
                            {
                                DVLWarning = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("drivinglicence_dobyearmatch"))
                            {
                                DLDoBYear = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("drivinglicence_dobmonthmatch"))
                            {
                                DLDoBMonth = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("drivinglicence_dobdaymatch"))
                            {
                                DLDoBDay = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("drivinglicence_othernamematch"))
                            {
                                DLONMATCH = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("drivinglicence_initialmatch"))
                            {
                                DLINIMATCH = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("drivinglicence_surnamematch"))
                            {
                                DLSNMATCH = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("drivinglicence_idnumber"))
                            {
                                DLIDNO = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("passport_expirydateok"))
                            {
                                PPExpiry = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("passport_dobyearmatch"))
                            {
                                PPDoBYear = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("passport_dobmonthmatch"))
                            {
                                PPDoBMonth = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("passport_dobdaymatch"))
                            {
                                PPDoBDay = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("passport_checkDigit5Match"))
                            {
                                PPCHKDIGIT5 = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("passport_checkDigit4Match"))
                            {
                                PPCHKDIGIT4 = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("passport_checkDigit3Match"))
                            {
                                PPCHKDIGIT3 = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("passport_checkDigit2Match"))
                            {
                                PPCHKDIGIT2 = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("passport_checkDigit1Match"))
                            {
                                PPCHKDIGIT1 = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("passport_nationality"))
                            {
                                PPNationality = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("passport_issuingcountry"))
                            {
                                PPISCountry = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("passport_middlenamematch"))
                            {
                                PPMNMatch = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("passport_forenamematch"))
                            {
                                PPFNMatch = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("passport_surnamematch"))
                            {
                                PPSNMatch = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("passport_machinereadableline2"))
                            {
                                PPL1 = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("passport_machinereadableline1"))
                            {
                                PPL2 = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("Title"))
                            {
                                STITLE = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("Firstname"))
                            {
                                SFNAME = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("Othernames"))
                            {
                                SMNAME = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("Surname"))
                            {
                                SSURNAME = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("Dateofbirth"))
                            {
                                SDOB = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("Abodenumber"))
                            {
                                SABODENO = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("Address1"))
                            {
                                SSTREET1 = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("Town"))
                            {
                                SPOSTTOWN = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("Postcode"))
                            {
                                SPOSTCODE = xmltr.ReadString();
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("IdentityResult"))
                            {
                                IdentityResult = xmltr.ReadString();
                                MATCHLEVEL = "INDIVIDUALREPORT";
                                //if (IdentityResult == "Pass")
                                if (IdentityResult == "Pass" && PPWarning.ToUpper() == "FALSE" && DVLWarning.ToUpper() == "FALSE")
                                {
                                    APPVERIFIED = "YES";
                                }
                                else
                                {
                                    APPVERIFIED = "REFER";
                                }
                            }
                            if (xmltr.NodeType == XmlNodeType.Element && xmltr.Name.Equals("IdentityScore"))
                            {
                                IdentityScore = xmltr.ReadString();
                            }
                        }

                        sOurRefno = obj.NullToSpace(drGD["SREFNO"]);

                        if (sOurRefno == "")
                            sOurRefno = AcD.ReferenceNo + "_" + obj.NullToSpace(drGD["TDSEQUENCE"]); //sOurRefno = obj.NullToSpace(drGD["TDREFNO"]);

                        InsertCallMLValuesV2();
                        obj.ExecuteCommand("UPDATE TDAPPIND SET IDCHECK='" + APPVERIFIED.ToUpper() + "',IDSCORE='" + IdentityScore.ToUpper() + "' WHERE SREFNO='" + sOurRefno + "'");

                        obj.StoreEvent("99999", "", "", "", "Updated CALLML VALUES IN TDAPPIND", Convert.ToString(Session["RefNo"]));
                        //return true;
                    }
                    else
                    {
                        obj.ExecuteCommand("UPDATE TDAPPIND SET IDCHECK='REFER',SUNIQUEID='" + SUNIQUEID + "',KYCCOMMENT='COULD NOT PERFORM E-AUTH : " + sEAuthExcMsg + "' WHERE SREFNO='" + sOurRefno + "'");
                    }

                }

                string sCallMLPass = "SELECT * FROM TDAPPIND WHERE (ISNULL(IDCHECK,'REFER') <> 'YES') AND (TDREFNO='" + AcD.ReferenceNo + "' or TDTRNID='" + AcD.ReferenceNo + "')";
                DataTable dTCallMlPass = obj.ExecuteData(sCallMLPass);


                if (dTCallMlPass != null && dTCallMlPass.Rows.Count == 0)
                    bValidApplicant = true;
                else
                    bValidApplicant = false;
            }
            //return false;
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", "Updated KYC errormsg", Convert.ToString(Session["RefNo"]));
            return false;
        }
        return bValidApplicant;
    }

    public void InsertCallMLValuesV2()
    {
        try
        {
            if (!string.IsNullOrEmpty(m_xmlOut))
            {
                obj.ExecuteCommand("INSERT INTO CallValidateLogs (RawInput,RawOutput,LogDate,LogTime) VALUES('" + m_xmlIn + "','" + m_xmlOut + "','" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("HHmmss") + "')");

                string sInsertcommand = string.Empty;
                AccountDtl AcD = GetAcDtl();
                try
                {
                    sInsertcommand = "INSERT INTO CALLML(SREFNO,SDATE,STIME,TDREFNO,STITLE,SFNAME,SSURNAME,SDOB,SABODENO,SSTREET1,SPOSTTOWN,SPOSTCODE,MATCHLEVEL,APPVERIFIED,IDENTITYSCORE,PPMLINE1,PPMLINE2,PPSNAMEMATCH,PPFNAMEMATCH,PPMNAMEMATCH,PPISSUECOUNTRY,PPNATIONALITY,PPCHKDIGIT1MATCH,PPCHKDIGIT2MATCH,PPCHKDIGIT3MATCH,PPCHKDIGIT4MATCH,PPCHKDIGIT5MATCH,PPDOBDAYMATCH,PPDOBMONTHMATCH,PPDOBYEARMATCH,PPEXPIRYDATEOK,DLIDNO,DLSNAMEMATCH,DLINITMATCH,DLONAMEMATCH,DLDOBDAYMATCH,DLDOBMONTHMATCH,DLDOBYEARMATCH,DVLAWARNING,PASSPORTWARNING) VALUES('";
                    sInsertcommand += SREFNO.Trim() + "','" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("HHmmss") + "','" + AcD.ReferenceNo + "','";
                    sInsertcommand += STITLE.ToUpper() + "','" + SFNAME.ToUpper() + "','" + SSURNAME.ToUpper() + "','" + SDOB.ToUpper() + "','";
                    sInsertcommand += SABODENO.ToUpper() + "','" + SSTREET1.ToUpper() + "','" + SPOSTTOWN.ToUpper() + "','" + SPOSTCODE.ToUpper() + "','" + MATCHLEVEL.ToUpper() + "','" + APPVERIFIED.ToUpper() + "','" + IdentityScore.ToUpper() + "','";
                    sInsertcommand += PPL1.ToUpper() + "','" + PPL2.ToUpper() + "','" + PPSNMatch.ToUpper() + "','" + PPFNMatch.ToUpper() + "','" + PPMNMatch.ToUpper() + "','" + PPISCountry.ToUpper() + "','" + PPNationality.ToUpper() + "','";
                    sInsertcommand += PPCHKDIGIT1.ToUpper() + "','" + PPCHKDIGIT2.ToUpper() + "','" + PPCHKDIGIT3.ToUpper() + "','" + PPCHKDIGIT4.ToUpper() + "','" + PPCHKDIGIT5.ToUpper() + "','" + PPDoBDay.ToUpper() + "','" + PPDoBMonth.ToUpper() + "','" + PPDoBYear.ToUpper() + "','" + PPExpiry.ToUpper() + "','";
                    sInsertcommand += DLIDNO.ToUpper() + "','" + DLSNMATCH.ToUpper() + "','" + DLINIMATCH.ToUpper() + "','" + DLONMATCH.ToUpper() + "','" + DLDoBDay.ToUpper() + "','" + DLDoBMonth.ToUpper() + "','" + DLDoBYear.ToUpper() + "','" + DVLWarning.ToUpper() + "','" + PPWarning.ToUpper() + "')";

                    obj.ExecuteCommand(sInsertcommand.ToUpper());

                    obj.StoreEvent("66312", Convert.ToString(Session["RefNo"]), "", "", "", Convert.ToString(Session["RefNo"]));
                }
                catch (Exception ex)
                {
                    obj.StoreEvent("99999", "", "", "", "Callml Insert statement error " + obj.replaceSplChr(sInsertcommand) + "Exception Message:" + obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
                }
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }
    //End
    #endregion

    #region old callMl service using WSDL
    private bool Authenticate()
    {
        bool bValidApplicant = false;

        try
        {
            //Need to comment the following code while live
            //if (!bValidApplicant) return true;

            CallML6 objCallML6 = new CallML6();
            AccountDtl AcD = GetAcDtl();

            string sGetData = null;
            DataTable dTGetData = new DataTable();

            string sCallMLPass = string.Empty;
            DataTable dTCallMlPass = new DataTable();

            string sOurRefno = string.Empty;
            sGetData = "SELECT * FROM TDAPPIND WHERE TDREFNO='" + AcD.ReferenceNo + "' or TDTRNID='" + AcD.ReferenceNo + "' ORDER BY SREFNO";
            dTGetData = obj.ExecuteData(sGetData);

            if (dTGetData == null || dTGetData.Rows.Count == 0)
            {
                bValidApplicant = false;
            }
            else
            {
                foreach (DataRow drGD in dTGetData.Rows)
                {
                    try
                    {
                        sOurRefno = string.Empty;

                        string sCompName, sUid, sPwd;
                        sCompName = sUid = sPwd = string.Empty;
                        string sAbodeno, sBuildingno, sStreet1, sPosttown, sPostcode, sAddresstype;
                        sAbodeno = sBuildingno = sStreet1 = sPosttown = sPostcode = sAddresstype = string.Empty;
                        string sTitle, sFname, sMiddleName, sSurname, sDob;
                        sTitle = sFname = sMiddleName = sSurname = sDob = string.Empty;
                        string sPassNo, sCtryISsue, sPpdoe;
                        sPassNo = sCtryISsue = sPpdoe = string.Empty;
                        string sDvType, sDvIdvalue;
                        sDvType = sDvIdvalue = string.Empty;
                        string sPpIdtype, sPpIdkey1, sPpIdvalue1, sPpIdkey2, sPpIdvalue2;
                        sPpIdtype = sPpIdkey1 = sPpIdvalue1 = sPpIdkey2 = sPpIdvalue2 = string.Empty;
                        string sSearchPurpose, sMinchecks;
                        sSearchPurpose = sOurRefno = sMinchecks = string.Empty;
                        bool sOther = true;
                        bool bPreviousAdd = false;

                        string stDOB, sBuildNo, sBuildName;
                        stDOB = sBuildNo = sBuildName = string.Empty;
                        string[] add;
                        int j = 0;


                        CallMLParameters6 callmlParams = new CallMLParameters6(); ;
                        mlprimarysearch callmlPrimaySearch = new mlprimarysearch();

                        callcreditheaders callcreditHeaders = new callcreditheaders();
                        callmlsearch6_1 searchDefinition = new callmlsearch6_1(); ;

                        applicant callmlApplicant = new applicant();
                        name callmlName = new name(); ;

                        CallML.address callmlCurAdd = new CallML.address(); //As New callML.address ' Address' is ambiguous, imported from the namespaces or types 'PostCodeSoftware, callML' ' Updated By Rose - 22/09/2015
                        CallML.address callmlPrAdd = new CallML.address(); //As New callML.address ' Address' is ambiguous, imported from the namespaces or types 'PostCodeSoftware, callML' ' Updated By Rose - 22/09/2015

                        telephone callmlTelephone = new telephone(); // As New telephone

                        identity[] callmlIdentity = new identity[1];
                        idparam[] callmlIdparm = new idparam[1];

                        bool bDvla = false;
                        bool bPp = false;

                        stDOB = sBuildNo = sBuildName = string.Empty;

                        stDOB = obj.NullToSpace(drGD["TDFADOB"]);

                        add = obj.NullToSpace(drGD["TDFACURADD1"]).Split(';'); // Split(obj.NullToSpace(drGD("TDFACURADD1")), ";")
                        for (int k = 0; k < add.Length; k++)
                        {
                            if (k == 0)
                                sBuildNo = add[k];
                            else
                                sBuildName = add[k];
                        }

                        sCompName = obj.RetPolValue("CALLML", "CNAME");
                        sUid = obj.RetPolValue("CALLML", "UID");
                        //sPwd = obj.RetPolValue("CALLML", "PWD");
                        sPwd = obj.DecryPass(obj.RetPolValue("CALLML", "PWD"));
                        sAbodeno = "";
                        sBuildingno = sBuildNo;
                        sStreet1 = obj.NullToSpace(drGD["TDFACURADD2"]);
                        sPosttown = obj.NullToSpace(drGD["TDFACURADD3"]).Length > 30 ? obj.NullToSpace(drGD["TDFACURADD3"]).Substring(0, 30) : obj.NullToSpace(drGD["TDFACURADD3"]);
                        sPostcode = obj.NullToSpace(drGD["TDFAPCODE"]);

                        sTitle = obj.NullToSpace(drGD["TDFATITLE"]);
                        sFname = obj.NullToSpace(drGD["TDFANAME"]).Length > 30 ? obj.NullToSpace(drGD["TDFANAME"]).Substring(0, 30) : obj.NullToSpace(drGD["TDFANAME"]);
                        sMiddleName = obj.NullToSpace(drGD["TDMINAME"]).Length > 30 ? obj.NullToSpace(drGD["TDMINAME"]).Substring(0, 30) : obj.NullToSpace(drGD["TDMINAME"]);
                        sSurname = obj.NullToSpace(drGD["TDSURNAME"]).Length > 30 ? obj.NullToSpace(drGD["TDSURNAME"]).Substring(0, 30) : obj.NullToSpace(drGD["TDSURNAME"]);

                        if (stDOB.Length > 7)
                            sDob = stDOB.Substring(0, 4) + "-" + stDOB.Substring(4, 2) + "-" + stDOB.Substring(6, 2);
                        else
                            sDob = DateTime.Now.ToString("yyyy-MM-dd");

                        sDvType = obj.NullToSpace(drGD["DLTYPE"]);
                        sDvIdvalue = obj.NullToSpace(drGD["TDFADVLANO"]);
                        sPassNo = obj.NullToSpace(drGD["TDFAPASSNO"]);

                        if (sPassNo != string.Empty)
                        {
                            bPp = true;

                            sCtryISsue = obj.NullToSpace(drGD["TDPOI"]);

                            sPpIdkey1 = "MACHINEREADABLELINE1";
                            sPpIdkey2 = "MACHINEREADABLELINE2";
                            sPpIdtype = obj.RetCallMLValue("UKPP");

                            sPpIdvalue1 = obj.NullToSpace(drGD["TDFAPANAME"]);
                            sPpIdvalue2 = obj.NullToSpace(drGD["TDFAPASSNO"]);
                            sPpdoe = obj.NullToSpace(drGD["TDDOE"]);
                        }

                        sOurRefno = obj.NullToSpace(drGD["SREFNO"]);

                        if (sOurRefno == "")
                            sOurRefno = AcD.ReferenceNo + "_" + obj.NullToSpace(drGD["TDSEQUENCE"]); //sOurRefno = obj.NullToSpace(drGD["TDREFNO"]);

                        sMinchecks = obj.RetPolValue("CALLML", "MINCHECK");

                        callcreditHeaders.company = sCompName;
                        callcreditHeaders.username = sUid;
                        callcreditHeaders.password = sPwd;

                        objCallML6.callcreditheadersValue = callcreditHeaders;

                        callmlCurAdd.abodeno = "";

                        /* Added by bala as Call credit is not accepting building number more than 12 char */
                        string sBuilding_No = string.Empty;
                        if (sBuildName == "" && sBuildingno.Length > 12)
                            sBuilding_No = sBuildingno.Substring(0, 12);
                        else if (sBuildName != "" && sBuildingno.Length > 12)
                            sBuilding_No = string.Empty;
                        else
                            sBuilding_No = sBuildingno;

                        callmlCurAdd.buildingno = sBuilding_No;
                        callmlCurAdd.buildingname = sBuildName;
                        callmlCurAdd.street1 = sStreet1;
                        callmlCurAdd.street2 = "";
                        callmlCurAdd.sublocality = "";
                        callmlCurAdd.locality = "";
                        callmlCurAdd.posttown = sPosttown;
                        callmlCurAdd.premiseno = "";
                        callmlCurAdd.premisename = "";

                        sPostcode = sPostcode.Replace(" ", "");
                        sPostcode = sPostcode.Length > 4 ? sPostcode.Substring(0, sPostcode.Length - 3) + " " + sPostcode.Substring(sPostcode.Length - 3, 3) : sPostcode;

                        callmlCurAdd.postcode = sPostcode;
                        callmlCurAdd.addresstype = inputaddresstype.@long;

                        if (obj.NullToSpace(drGD["PRVPRESENT"]) == "Y")
                        {
                            string sPrBuildNo, sPrBuildName, sPrPostcode, sPrStreet1;
                            string[] sPRadd = obj.NullToSpace(drGD["TDFAPRADD1"]).Split(';');
                            sPrStreet1 = obj.NullToSpace(drGD["TDFAPRADD2"]);

                            sPrBuildNo = sPrBuildName = sPrPostcode = sPrStreet1 = string.Empty;

                            for (int k = 0; k < add.Length; k++)
                            {
                                if (k == 0)
                                    sPrBuildNo = add[k];
                                else
                                    sPrBuildName = add[k];
                            }

                            callmlPrAdd.abodeno = "";

                            /* Added by bala as Call credit is not accepting building number more than 12 char */
                            string sPreBuilding_No = string.Empty;
                            if (sPrBuildName == "" && sPrBuildNo.Length > 12)
                                sPreBuilding_No = sPrBuildNo.Substring(0, 12);
                            else if (sPrBuildName != "" && sPrBuildNo.Length > 12)
                                sPreBuilding_No = string.Empty;
                            else
                                sPreBuilding_No = sPrBuildNo;

                            callmlPrAdd.buildingno = sPreBuilding_No;

                            callmlPrAdd.buildingname = sPrBuildName;
                            callmlPrAdd.street1 = sPrStreet1;
                            callmlPrAdd.street2 = "";
                            callmlPrAdd.sublocality = "";
                            callmlPrAdd.locality = "";
                            callmlPrAdd.posttown = obj.NullToSpace(drGD["TDFAPRADD3"]).Length > 24 ? obj.NullToSpace(drGD["TDFAPRADD3"]).Substring(0, 25) : obj.NullToSpace(drGD["TDFAPRADD3"]);
                            callmlPrAdd.premiseno = "";
                            callmlPrAdd.premisename = "";

                            sPrPostcode = obj.NullToSpace(drGD["TDFAPRPCODE"]).Replace(" ", "");

                            sPrPostcode = sPrPostcode.Length > 4 ? sPrPostcode.Substring(0, sPrPostcode.Length - 3) + " " + sPrPostcode.Substring(sPrPostcode.Length - 3, 3) : sPrPostcode;
                            callmlPrAdd.postcode = sPrPostcode;
                            callmlPrAdd.addresstype = inputaddresstype.@long;

                            bPreviousAdd = true;
                        }

                        callmlName.title = sTitle;
                        callmlName.forename = sFname;
                        callmlName.othernames = sMiddleName;
                        callmlName.surname = sSurname;

                        callmlApplicant.currentaddress = callmlCurAdd;

                        if (bPreviousAdd)
                            callmlApplicant.previousaddress = callmlPrAdd;

                        callmlApplicant.name = callmlName;
                        callmlApplicant.dateofbirth = Convert.ToDateTime(sDob);

                        if (sDvIdvalue != "")
                        {
                            identity DvlaIden = new identity();
                            idparam DvlaParam = new idparam();

                            bDvla = true;

                            if (sDvType == "P")
                                DvlaIden.idtype = obj.RetCallMLValue("PHOTODL");
                            else
                                DvlaIden.idtype = obj.RetCallMLValue("OLDDL");


                            DvlaParam.idkey = "IDNUMBER";
                            DvlaParam.idvalue = sDvIdvalue;

                            callmlIdparm[0] = DvlaParam;

                            callmlIdentity[0] = DvlaIden;
                            callmlIdentity[0].idparams = callmlIdparm;

                        }

                        if (bPp)
                        {
                            identity ppIden = new identity();
                            idparam ppParam1 = new idparam();
                            idparam ppParam2 = new idparam();
                            idparam ppParam3 = new idparam();

                            idparam[] ppIdparm = new idparam[3];

                            ppIden.idtype = sPpIdtype;

                            ppParam1.idkey = sPpIdkey1;
                            ppParam1.idvalue = sPpIdvalue1;

                            ppParam2.idkey = sPpIdkey2;
                            ppParam2.idvalue = sPpIdvalue2;

                            ppParam3.idkey = "EXPIRYDATE";
                            ppParam3.idvalue = sPpdoe.Substring(6, 2) + sPpdoe.Substring(4, 2) + sPpdoe.Substring(0, 4);

                            ppIdparm[0] = ppParam1;
                            ppIdparm[1] = ppParam2;
                            ppIdparm[2] = ppParam3;

                            if (bDvla)
                            {
                                Array.Resize(ref callmlIdentity, 2);
                                callmlIdentity[1] = ppIden;
                                callmlIdentity[1].idparams = ppIdparm;
                            }
                            else
                            {
                                callmlIdentity[0] = ppIden;
                                callmlIdentity[0].idparams = ppIdparm;
                            }
                        }

                        callmlPrimaySearch.searchpurpose = searchpurpose.ML;
                        callmlPrimaySearch.applicant = callmlApplicant;
                        callmlPrimaySearch.identities = callmlIdentity;

                        callmlPrimaySearch.searchdirectors = sOther;
                        callmlPrimaySearch.minchecks = Convert.ToInt32(sMinchecks);
                        callmlPrimaySearch.usebai = sOther;
                        callmlPrimaySearch.useccj = sOther;
                        callmlPrimaySearch.usehcj = sOther;
                        callmlPrimaySearch.useer = sOther;


                        callmlPrimaySearch.usesettledaccounts = true;
                        callmlPrimaySearch.settledaccountmonths = Convert.ToInt32(obj.RetPolValue("CALLML", "SETAC"));
                        callmlPrimaySearch.settledaccountmonthsSpecified = true;

                        callmlPrimaySearch.useukinvestors = sOther;

                        callmlPrimaySearch.decisionwarning = obj.RetPolValue("CALLML", "DECWARN");

                        callmlPrimaySearch.searchtelephone = false;

                        callmlPrimaySearch.applicant.dateofbirthSpecified = true;

                        if (bPreviousAdd)
                        {
                            callmlPrimaySearch.applicant.addrlessthan12months = true;
                            callmlPrimaySearch.applicant.addrlessthan12monthsSpecified = true;
                        }
                        else
                        {
                            callmlPrimaySearch.applicant.addrlessthan12months = false;
                        }


                        callmlParams.primarysearch = callmlPrimaySearch;
                        callmlParams.yourreference = sOurRefno;

                        obj.StoreEvent("99999", "", "", "", "CALLML Service is start for the Applicant First Name : " + sFname, Convert.ToString(Session["RefNo"]));
                        searchDefinition.parameters = callmlParams;

                        bool bEAuthSts = false;
                        string sEAuthExcMsg = string.Empty;
                        try
                        {
                            searchDefinition = objCallML6.Search06b(searchDefinition);
                            bEAuthSts = true;
                            obj.StoreEvent("99999", "", "", "", "CALLML Service is end", Convert.ToString(Session["RefNo"]));
                        }
                        catch (Exception ex)
                        {
                            bEAuthSts = false;

                            sEAuthExcMsg = obj.replaceSplChr(ex.Message);
                            if (sEAuthExcMsg.Length > 350)
                                sEAuthExcMsg = sEAuthExcMsg.Substring(0, 350);

                            obj.StoreEvent("99999", "", "", "", "Exception while consuming the CallML Service", Convert.ToString(Session["RefNo"]));
                        }

                        if (bEAuthSts)
                        {
                            obj.StoreEvent("99999", "", "", "", "Ready to insert CALLML values", Convert.ToString(Session["RefNo"]));

                            InsertCallMLValues(searchDefinition.parameters, searchDefinition.results);

                            obj.ExecuteCommand("UPDATE TDAPPIND SET IDCHECK='" + IDPCheck.ToUpper() + "',IDSCORE='" + IDPScore.ToUpper() + "',SUNIQUEID='" + SUNIQUEID + "',KYCWARNING='" + sKYCWarning.ToUpper() + "',KYCCOMMENT='" + sKYCComment.ToUpper() + "' WHERE SREFNO='" + SREFNO + "'");

                            obj.StoreEvent("99999", "", "", "", "Updated CALLML VALUES IN TDAPPID", Convert.ToString(Session["RefNo"]));
                        }
                        else
                        {
                            obj.ExecuteCommand("UPDATE TDAPPIND SET IDCHECK='REFER',SUNIQUEID='" + SUNIQUEID + "',KYCCOMMENT='COULD NOT PERFORM E-AUTH : " + sEAuthExcMsg + "' WHERE SREFNO='" + sOurRefno + "'");
                        }

                        ////searchDefinition = objCallML6.Search06b(searchDefinition);
                        ////obj.StoreEvent("99999", "", "", "", "CALLML Service is end", Convert.ToString(Session["RefNo"]));


                        ////obj.StoreEvent("99999", "", "", "", "Ready to insert CALLML values", Convert.ToString(Session["RefNo"]));

                        ////InsertCallMLValues(searchDefinition.parameters, searchDefinition.results);


                        ////obj.ExecuteCommand("UPDATE TDAPPIND SET IDCHECK='" + IDPCheck.ToUpper() + "',IDSCORE='" + IDPScore.ToUpper() + "',SUNIQUEID='" + SUNIQUEID + "',KYCWARNING='" + sKYCWarning.ToUpper() + "',KYCCOMMENT='" + sKYCComment.ToUpper() + "' WHERE SREFNO='" + SREFNO + "'");

                        ////obj.StoreEvent("99999", "", "", "", "Updated CALLML VALUES IN TDAPPID", Convert.ToString(Session["RefNo"]));
                    }
                    catch (Exception ex)
                    {
                        string sErrMsg = string.Empty;
                        sErrMsg = obj.replaceSplChr(ex.Message);
                        // obj.StoreEvent("99999", sErrMsg);
                        if (sErrMsg.Length > 399)
                            sErrMsg = sErrMsg.Substring(0, 400);

                        obj.ExecuteCommand("UPDATE TDAPPIND SET IDCHECK='REFER',SUNIQUEID='" + SUNIQUEID + "',KYCCOMMENT='" + sErrMsg.ToUpper() + "' WHERE SREFNO='" + sOurRefno + "'");

                        obj.StoreEvent("88888", "", "", "", "Updated KYC errormsg", Convert.ToString(Session["RefNo"]));
                    }
                } // end for

                //Verify all applicant pass during KYC Checking

                sCallMLPass = "SELECT * FROM TDAPPIND WHERE (ISNULL(IDCHECK,'REFER') <> 'YES' OR ISNULL(KYCWARNING,'Y') <> 'N') AND (TDREFNO='" + AcD.ReferenceNo + "' or TDTRNID='" + AcD.ReferenceNo + "')";
                dTCallMlPass = obj.ExecuteData(sCallMLPass);

                if (dTCallMlPass != null && dTCallMlPass.Rows.Count == 0)
                    bValidApplicant = true;
                else
                    bValidApplicant = false;

            } // end else
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }

        return bValidApplicant;
        //return true;
    }

    private void ClearCallMLValues()
    {
        IDPCheck = string.Empty;
        IDPScore = string.Empty;
        SREFNO = string.Empty;
        SUNIQUEID = string.Empty;
        sKYCWarning = string.Empty;
        sKYCComment = string.Empty;

        SANCTIONSWARNING = false;
        SDNWARNING = false;
        PEPWARNING = false;
        ADDRESSWARNING = false;
        ADDRESSLINKSWARNING = false;
        GONEAWAYWARNING = false;
        DVLAWARNING = false;
        DECEASEDWARNING = false;
        PASSPORTWARNING = false;
        SEARCHWARNING = false;
        IDCARDWARNING = false;
        FRAUDULENTPASSPORTWARNING = false;
    }

    public void InsertCallMLValues(CallMLParameters6 parametersSubmited, CallMLResults6 resultToDisplay)
    {
        try
        {
            //Clear Global variables
            ClearCallMLValues();

            string STITLE, SFNAME, SMNAME, SSURNAME, SDOB;
            STITLE = SFNAME = SMNAME = SSURNAME = SDOB = string.Empty;
            string SABODENO, SBUILDINGNO, SBUILDINGNAME, SSTREET1, SSTREET2, SSUBLOCALITY, SLOCALITY;
            SABODENO = SBUILDINGNO = SBUILDINGNAME = SSTREET1 = SSTREET2 = SSUBLOCALITY = SLOCALITY = string.Empty;
            string SPOSTTOWN, SPREMISENO, SPREMISENAME, SPOSTCODE, SADDRESSTYPE;
            SPOSTTOWN = SPREMISENO = SPREMISENAME = SPOSTCODE = SADDRESSTYPE = string.Empty;

            string SPRABODENO, SPRBUILDINGNO, SPRBUILDINGNAME, SPRSTREET1, SPRSTREET2, SPRSUBLOCALITY, SPRLOCALITY;
            SPRABODENO = SPRBUILDINGNO = SPRBUILDINGNAME = SPRSTREET1 = SPRSTREET2 = SPRSUBLOCALITY = SPRLOCALITY = string.Empty;
            string SPRPOSTTOWN, SPRPREMISENO, SPRPREMISENAME, SPRPOSTCODE, SPRADDRESSTYPE;
            SPRPOSTTOWN = SPRPREMISENO = SPRPREMISENAME = SPRPOSTCODE = SPRADDRESSTYPE = string.Empty;

            string SDLIDTYPE, SDLIDKEY1, SDLIDVALUE1;
            SDLIDTYPE = SDLIDKEY1 = SDLIDVALUE1 = string.Empty;
            string SPPIDTYPE, SPPIDKEY1, SPPIDVALUE1, SPPIDKEY2, SPPIDVALUE2, SPPIDKEY3, SPPIDVALUE3, SOTHVALUE;
            SPPIDTYPE = SPPIDKEY1 = SPPIDVALUE1 = SPPIDKEY2 = SPPIDVALUE2 = SPPIDKEY3 = SPPIDVALUE3 = SOTHVALUE = string.Empty;
            int SMINCHECKS;


            string SEARCHDATE, SEARCHID, CAST;
            SEARCHDATE = SEARCHID = CAST = string.Empty;
            int NUMPRIMARYCHECKS, NUMCORROBORATIVECHECKS, NUMPRIMARYOTHERIDSCONFIRMED, NUMCORROBORATIVEOTHERIDSCONFIRMED, CONFIRMATORYDOBS, TOTALDOBS;

            string MATCHLEVEL, APPVERIFIED;
            string sInsertcommand;
            string sParameter;

            MATCHLEVEL = APPVERIFIED = sInsertcommand = sParameter = string.Empty;

            int CDRCURADDLEVEL, CDSCURADDLEVEL, CDRPRADDLEVEL, CDSPRADDLEVEL, LVLCDDOB, LVLCDCCJ, LVLCDBAI, LVLCDFTSE;
            string DLIDNO, DLSNAMEMATCH, DLINITMATCH;
            DLIDNO = DLSNAMEMATCH = DLINITMATCH = string.Empty;
            string DLONAMEMATCH, DLDOBDAYMATCH, DLDOBMONTHMATCH, DLDOBYEARMATCH, PPIDNO, PPMLINE1, PPMLINE2, PPISSUECOUNTRY, PPNATIONALITY;
            DLONAMEMATCH = DLDOBDAYMATCH = DLDOBMONTHMATCH = DLDOBYEARMATCH = PPIDNO = PPMLINE1 = PPMLINE2 = PPISSUECOUNTRY = PPNATIONALITY = string.Empty;
            bool PPSNAMEMATCH, PPFNAMEMATCH, PPMNAMEMATCH, PPCHKDIGIT1MATCH, PPCHKDIGIT2MATCH, PPCHKDIGIT3MATCH, PPCHKDIGIT4MATCH, PPCHKDIGIT5MATCH;
            PPSNAMEMATCH = PPFNAMEMATCH = PPMNAMEMATCH = PPCHKDIGIT1MATCH = PPCHKDIGIT2MATCH = PPCHKDIGIT3MATCH = PPCHKDIGIT4MATCH = PPCHKDIGIT5MATCH = false;
            string PPDOBDAYMATCH, PPDOBMONTHMATCH, PPDOBYEARMATCH, PPEXPIRYDATEOK;
            PPDOBDAYMATCH = PPDOBMONTHMATCH = PPDOBYEARMATCH = PPEXPIRYDATEOK = string.Empty;

            bool bDLPresent = true;
            bool bPassportPresent = false;

            string TDREFNO, TDTRNID, sqry;
            TDREFNO = TDTRNID = sqry = string.Empty;
            DataTable dtRes;

            bool bPreviousAdd = false;

            string SIDTYPE = null;

            try
            {
                SREFNO = obj.NullToSpace(parametersSubmited.yourreference).Trim();

                try
                {
                    sqry = "SELECT TDREFNO,TDTRNID FROM TDAPPIND WHERE SREFNO='" + SREFNO + "'";
                    dtRes = obj.ExecuteData(sqry);
                    if (dtRes != null & dtRes.Rows.Count > 0)
                    {
                        TDREFNO = obj.NullToSpace(dtRes.Rows[0]["TDREFNO"]);
                        TDTRNID = obj.NullToSpace(dtRes.Rows[0]["TDTRNID"]);
                    }


                }
                catch (Exception ex)
                {
                }



                if (resultToDisplay.searchdate != null)
                {
                    SEARCHDATE = resultToDisplay.searchdate.Replace("T", " ");
                }

                SEARCHID = obj.NullToSpace(resultToDisplay.searchid);
                CAST = obj.NullToSpace(resultToDisplay.cast);
                NUMPRIMARYCHECKS = resultToDisplay.numprimarychecks;// resultToDisplay.numprimarychecks;
                NUMCORROBORATIVECHECKS = resultToDisplay.numcorroborativechecks;
                NUMPRIMARYOTHERIDSCONFIRMED = resultToDisplay.numprimaryotheridsconfirmed;
                NUMCORROBORATIVEOTHERIDSCONFIRMED = resultToDisplay.numcorroborativeshareidsconfirmed;
                CONFIRMATORYDOBS = resultToDisplay.confirmatorydobs;
                TOTALDOBS = resultToDisplay.totaldobs;

                SANCTIONSWARNING = resultToDisplay.sanctionswarning;
                SDNWARNING = resultToDisplay.sdnwarning;
                PEPWARNING = resultToDisplay.pepwarning;
                ADDRESSWARNING = resultToDisplay.addresswarning;
                ADDRESSLINKSWARNING = resultToDisplay.addresslinkswarning;
                GONEAWAYWARNING = resultToDisplay.goneawaywarning;
                DVLAWARNING = resultToDisplay.dvlawarning;
                DECEASEDWARNING = resultToDisplay.deceasedwarning;
                PASSPORTWARNING = resultToDisplay.passportwarning;
                SEARCHWARNING = resultToDisplay.searchwarning;
                IDCARDWARNING = resultToDisplay.idcardwarning;
                FRAUDULENTPASSPORTWARNING = resultToDisplay.fraudulentpassportwarning;

                MATCHLEVEL = resultToDisplay.matchlevel.ToString();
                APPVERIFIED = obj.NullToSpace(resultToDisplay.appverified);

                if (string.IsNullOrEmpty(APPVERIFIED.Trim()))
                {
                    APPVERIFIED = "REFER";
                }

                SUNIQUEID = SEARCHID;
                IDPCheck = APPVERIFIED;
                IDPScore = MATCHLEVEL;

                if (bKYCWarning() == true)
                {
                    sKYCWarning = "Y";
                }
                else
                {
                    sKYCWarning = "N";
                }

                applicant oApAuthRslt = parametersSubmited.primarysearch.applicant;

                STITLE = obj.NullToSpace(oApAuthRslt.name.title);
                SFNAME = obj.NullToSpace(oApAuthRslt.name.forename);
                SMNAME = obj.NullToSpace(oApAuthRslt.name.othernames);
                SSURNAME = obj.NullToSpace(oApAuthRslt.name.surname);
                SDOB = oApAuthRslt.dateofbirth.ToString();
                SABODENO = obj.NullToSpace(oApAuthRslt.currentaddress.abodeno);

                SABODENO = obj.NullToSpace(oApAuthRslt.currentaddress.abodeno);
                SBUILDINGNO = obj.NullToSpace(oApAuthRslt.currentaddress.buildingno);
                SBUILDINGNAME = obj.NullToSpace(oApAuthRslt.currentaddress.buildingname);
                SSTREET1 = obj.NullToSpace(oApAuthRslt.currentaddress.street1);
                SSTREET2 = obj.NullToSpace(oApAuthRslt.currentaddress.street2);
                SSUBLOCALITY = obj.NullToSpace(oApAuthRslt.currentaddress.sublocality);
                SLOCALITY = obj.NullToSpace(oApAuthRslt.currentaddress.locality);
                SPOSTTOWN = obj.NullToSpace(oApAuthRslt.currentaddress.posttown);
                SPREMISENO = obj.NullToSpace(oApAuthRslt.currentaddress.premiseno);
                SPREMISENAME = obj.NullToSpace(oApAuthRslt.currentaddress.premisename);
                SPOSTCODE = obj.NullToSpace(oApAuthRslt.currentaddress.postcode);
                SADDRESSTYPE = oApAuthRslt.currentaddress.addresstype.ToString();

                if (parametersSubmited.primarysearch.applicant.addrlessthan12months == true)
                {

                    var PreAddr = parametersSubmited.primarysearch.applicant.previousaddress;
                    bPreviousAdd = true;
                    SPRABODENO = obj.NullToSpace(PreAddr.abodeno);
                    SPRBUILDINGNO = obj.NullToSpace(PreAddr.buildingno);
                    SPRBUILDINGNAME = obj.NullToSpace(PreAddr.buildingname);
                    SPRSTREET1 = obj.NullToSpace(PreAddr.street1);
                    SPRSTREET2 = obj.NullToSpace(PreAddr.street2);
                    SPRSUBLOCALITY = obj.NullToSpace(PreAddr.sublocality);
                    SPRLOCALITY = obj.NullToSpace(PreAddr.locality);
                    SPRPOSTTOWN = obj.NullToSpace(PreAddr.posttown);
                    SPRPREMISENO = obj.NullToSpace(PreAddr.premiseno);
                    SPRPREMISENAME = obj.NullToSpace(PreAddr.premisename);
                    SPRPOSTCODE = obj.NullToSpace(PreAddr.postcode);
                    SPRADDRESSTYPE = PreAddr.addresstype.ToString();
                }

                //Identity document1 - Driving License / Passport


                if (parametersSubmited.primarysearch.identities.Count() >= 1)
                {
                    var identity = parametersSubmited.primarysearch.identities[0];

                    SIDTYPE = obj.NullToSpace(identity.idtype);


                    if (SIDTYPE == "6" | SIDTYPE == "30")
                    {
                        SDLIDTYPE = SIDTYPE;
                        switch (identity.idparams.Count())
                        {
                            case 1:
                                SDLIDKEY1 = obj.NullToSpace(identity.idparams[0].idkey);
                                SDLIDVALUE1 = obj.NullToSpace(identity.idparams[0].idvalue);
                                bDLPresent = true;
                                break;
                            default:
                                SDLIDKEY1 = "";
                                SDLIDVALUE1 = "";
                                break;
                        }
                    }
                    else
                    {
                        SPPIDTYPE = SIDTYPE;
                        bPassportPresent = true;
                        switch (identity.idparams.Count())
                        {
                            case 1:
                                SPPIDKEY1 = obj.NullToSpace(identity.idparams[0].idkey);
                                SPPIDVALUE1 = obj.NullToSpace(identity.idparams[0].idvalue);
                                SPPIDKEY2 = "";
                                SPPIDVALUE3 = "";
                                SPPIDKEY3 = "";
                                SPPIDVALUE3 = "";
                                break;
                            case 2:
                                SPPIDKEY1 = obj.NullToSpace(identity.idparams[0].idkey);
                                SPPIDVALUE1 = obj.NullToSpace(identity.idparams[0].idvalue);
                                SPPIDKEY2 = obj.NullToSpace(identity.idparams[1].idkey);
                                SPPIDVALUE3 = obj.NullToSpace(identity.idparams[1].idvalue);
                                SPPIDKEY3 = "";
                                SPPIDVALUE3 = "";
                                break;
                            case 3:
                                SPPIDKEY1 = obj.NullToSpace(identity.idparams[0].idkey);
                                SPPIDVALUE1 = obj.NullToSpace(identity.idparams[0].idvalue);
                                SPPIDKEY2 = obj.NullToSpace(identity.idparams[1].idkey);
                                SPPIDVALUE3 = obj.NullToSpace(identity.idparams[1].idvalue);
                                SPPIDKEY3 = obj.NullToSpace(identity.idparams[2].idkey);
                                SPPIDVALUE3 = obj.NullToSpace(identity.idparams[2].idvalue);
                                break;
                        }
                    }

                    //Identity document2 - Driving License / Passport


                    if (parametersSubmited.primarysearch.identities.Count() == 2)
                    {
                        var identity2 = parametersSubmited.primarysearch.identities[1];

                        SIDTYPE = obj.NullToSpace(identity2.idtype);


                        if (SIDTYPE == "6" | SIDTYPE == "30")
                        {
                            SDLIDTYPE = SIDTYPE;
                            switch (identity2.idparams.Count())
                            {
                                case 1:
                                    SDLIDKEY1 = obj.NullToSpace(identity2.idparams[0].idkey);
                                    SDLIDVALUE1 = obj.NullToSpace(identity2.idparams[0].idvalue);
                                    bDLPresent = true;
                                    break;
                                default:
                                    SDLIDKEY1 = "";
                                    SDLIDVALUE1 = "";
                                    break;
                            }

                        }
                        else
                        {
                            SPPIDTYPE = SIDTYPE;
                            bPassportPresent = true;
                            switch (identity2.idparams.Count())
                            {
                                case 1:
                                    SPPIDKEY1 = obj.NullToSpace(identity2.idparams[0].idkey);
                                    SPPIDVALUE1 = obj.NullToSpace(identity2.idparams[0].idvalue);
                                    SPPIDKEY2 = "";
                                    SPPIDVALUE3 = "";
                                    SPPIDKEY3 = "";
                                    SPPIDVALUE3 = "";
                                    break;
                                case 2:
                                    SPPIDKEY1 = obj.NullToSpace(identity2.idparams[0].idkey);
                                    SPPIDVALUE1 = obj.NullToSpace(identity2.idparams[0].idvalue);
                                    SPPIDKEY2 = obj.NullToSpace(identity2.idparams[1].idkey);
                                    SPPIDVALUE3 = obj.NullToSpace(identity2.idparams[1].idvalue);
                                    SPPIDKEY3 = "";
                                    SPPIDVALUE3 = "";
                                    break;
                                case 3:
                                    SPPIDKEY1 = obj.NullToSpace(identity2.idparams[0].idkey);
                                    SPPIDVALUE1 = obj.NullToSpace(identity2.idparams[0].idvalue);
                                    SPPIDKEY2 = obj.NullToSpace(identity2.idparams[1].idkey);
                                    SPPIDVALUE2 = obj.NullToSpace(identity2.idparams[1].idvalue);
                                    SPPIDKEY3 = obj.NullToSpace(identity2.idparams[2].idkey);
                                    SPPIDVALUE3 = obj.NullToSpace(identity2.idparams[2].idvalue);
                                    break;
                            }
                        }

                    }

                }

                var _with6 = parametersSubmited.primarysearch;
                SMINCHECKS = _with6.minchecks;
                SOTHVALUE = "";


                var _with7 = resultToDisplay.furtherdetails;

                CDRCURADDLEVEL = _with7.levelofconfidenceer.currentaddresslevel;
                CDSCURADDLEVEL = _with7.levelofconfidenceshare.currentaddresslevel;

                if (bPreviousAdd == true)
                {
                    CDRPRADDLEVEL = _with7.levelofconfidenceer.previousaddresslevel;
                    CDSPRADDLEVEL = _with7.levelofconfidenceshare.previousaddresslevel;
                }
                else
                {
                    CDRPRADDLEVEL = 0;
                    CDSPRADDLEVEL = 0;
                }


                LVLCDDOB = _with7.levelofconfidencedob;
                LVLCDCCJ = _with7.levelofconfidenceccj;
                LVLCDBAI = _with7.levelofconfidencebai;
                LVLCDFTSE = _with7.levelofconfidenceftse;


                try
                {

                    if (SDLIDTYPE == obj.RetCallMLValue("OLDDL") | SDLIDTYPE == obj.RetCallMLValue("PHOTODL"))
                    {
                        DLIDNO = _with7.drivinglicence.idnumber;
                        DLSNAMEMATCH = obj.NullToSpace(_with7.drivinglicence.surnamematch);
                        DLINITMATCH = obj.NullToSpace(_with7.drivinglicence.initialmatch);
                        DLONAMEMATCH = obj.NullToSpace(_with7.drivinglicence.othernamematch);
                        DLDOBDAYMATCH = obj.NullToSpace(_with7.drivinglicence.dobdaymatch);
                        DLDOBMONTHMATCH = obj.NullToSpace(_with7.drivinglicence.dobmonthmatch);
                        DLDOBYEARMATCH = obj.NullToSpace(_with7.drivinglicence.dobyearmatch);

                    }


                    if (SPPIDTYPE == obj.RetCallMLValue("UKPP"))
                    {
                        PPIDNO = obj.NullToSpace(_with7.passport.idnumber);
                        PPMLINE1 = obj.NullToSpace(_with7.passport.machinereadableline1);
                        PPMLINE2 = obj.NullToSpace(_with7.passport.machinereadableline2);
                        PPSNAMEMATCH = _with7.passport.surnamematch;
                        PPFNAMEMATCH = _with7.passport.forenamematch;
                        PPMNAMEMATCH = _with7.passport.middlenamematch;
                        PPISSUECOUNTRY = obj.NullToSpace(_with7.passport.issuingcountry);
                        PPNATIONALITY = _with7.passport.nationality;
                        PPCHKDIGIT1MATCH = _with7.passport.checkDigit1Match;
                        PPCHKDIGIT2MATCH = _with7.passport.checkDigit2Match;
                        PPCHKDIGIT3MATCH = _with7.passport.checkDigit3Match;
                        PPCHKDIGIT4MATCH = _with7.passport.checkDigit4Match;
                        PPCHKDIGIT5MATCH = _with7.passport.checkDigit5Match;
                        PPDOBDAYMATCH = obj.NullToSpace(_with7.passport.dobdaymatch);
                        PPDOBMONTHMATCH = obj.NullToSpace(_with7.passport.dobmonthmatch);
                        PPDOBYEARMATCH = obj.NullToSpace(_with7.passport.dobyearmatch);
                        PPEXPIRYDATEOK = obj.NullToSpace(_with7.passport.expirydateok);
                    }

                }
                catch (Exception ex)
                {
                    // obj.StoreEvent("99999", "Match status error" + obj.replaceSplChr(ex.Message));
                }



                sParameter = "SREFNO,SDATE,STIME,TDREFNO,TDTRNID,STITLE,SFNAME,SMNAME,SSURNAME,SDOB,";

                sParameter += "SABODENO,SBUILDINGNO,SBUILDINGNAME,SSTREET1,SSTREET2,SSUBLOCALITY,SLOCALITY,";
                sParameter += "SPOSTTOWN,SPREMISENO,SPREMISENAME,SPOSTCODE,SADDRESSTYPE,";

                sParameter += "SPRABODENO,SPRBUILDINGNO,SPRBUILDINGNAME,SPRSTREET1,SPRSTREET2,SPRSUBLOCALITY,SPRLOCALITY,";
                sParameter += "SPRPOSTTOWN,SPRPREMISENO,SPRPREMISENAME,SPRPOSTCODE,SPRADDRESSTYPE,";

                sParameter += "SDLIDTYPE,SDLIDKEY1,SDLIDVALUE1,";
                sParameter += "SPPIDTYPE,SPPIDKEY1,SPPIDVALUE1,SPPIDKEY2,SPPIDVALUE2,SPPIDKEY3,SPPIDVALUE3,";
                sParameter += "SMINCHECKS, SOTHVALUE, SEARCHDATE, SEARCHID, [CAST],";
                sParameter += "NUMPRIMARYCHECKS,NUMCORROBORATIVECHECKS,NUMPRIMARYOTHERIDSCONFIRMED,";
                sParameter += "NUMCORROBORATIVEOTHERIDSCONFIRMED,CONFIRMATORYDOBS,TOTALDOBS,";
                sParameter += "SANCTIONSWARNING,SDNWARNING,PEPWARNING,ADDRESSWARNING,";
                sParameter += "ADDRESSLINKSWARNING,GONEAWAYWARNING,DVLAWARNING,";
                sParameter += "DECEASEDWARNING,PASSPORTWARNING,SEARCHWARNING,";
                sParameter += "IDCARDWARNING,";
                sParameter += "FRAUDULENTPASSPORTWARNING,MATCHLEVEL,APPVERIFIED,";
                sParameter += "CDRCURADDLEVEL,CDSCURADDLEVEL,LVLCDDOB,LVLCDCCJ,LVLCDBAI,LVLCDFTSE,DLIDNO,DLSNAMEMATCH,DLINITMATCH,";
                sParameter += "DLONAMEMATCH,DLDOBDAYMATCH,DLDOBMONTHMATCH,DLDOBYEARMATCH,PPIDNO,PPMLINE1,PPMLINE2,PPSNAMEMATCH,PPFNAMEMATCH,";
                sParameter += "PPMNAMEMATCH,PPISSUECOUNTRY,PPNATIONALITY,PPCHKDIGIT1MATCH,PPCHKDIGIT2MATCH,PPCHKDIGIT3MATCH,PPCHKDIGIT4MATCH,";
                sParameter += "PPCHKDIGIT5MATCH,PPDOBDAYMATCH,PPDOBMONTHMATCH,PPDOBYEARMATCH,PPEXPIRYDATEOK,";
                sParameter += "CDRPRADDLEVEL,CDSPRADDLEVEL";


                try
                {
                    sInsertcommand = "INSERT INTO CALLML(" + sParameter + ") VALUES('";
                    sInsertcommand += SREFNO.Trim() + "','" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("HHmmss") + "','" + TDREFNO + "','" + TDTRNID + "','";
                    sInsertcommand += STITLE + "','" + SFNAME + "','" + SMNAME + "','" + SSURNAME + "','" + SDOB + "','";

                    sInsertcommand += SABODENO + "','" + SBUILDINGNO + "','" + SBUILDINGNAME + "','" + SSTREET1 + "','" + SSTREET2 + "','" + SSUBLOCALITY + "','" + SLOCALITY + "','";
                    sInsertcommand += SPOSTTOWN + "','" + SPREMISENO + "','" + SPREMISENAME + "','" + SPOSTCODE + "','" + SADDRESSTYPE + "','";

                    sInsertcommand += SPRABODENO + "','" + SPRBUILDINGNO + "','" + SPRBUILDINGNAME + "','" + SPRSTREET1 + "','" + SPRSTREET2 + "','" + SPRSUBLOCALITY + "','" + SPRLOCALITY + "','";
                    sInsertcommand += SPRPOSTTOWN + "','" + SPRPREMISENO + "','" + SPRPREMISENAME + "','" + SPRPOSTCODE + "','" + SPRADDRESSTYPE + "','";

                    sInsertcommand += SDLIDTYPE + "','" + SDLIDKEY1 + "','" + SDLIDVALUE1 + "','";
                    sInsertcommand += SPPIDTYPE + "','" + SPPIDKEY1 + "','" + SPPIDVALUE1 + "','" + SPPIDKEY2 + "','" + SPPIDVALUE2 + "','" + SPPIDKEY3 + "','" + SPPIDVALUE3 + "','" + SMINCHECKS + "','" + SOTHVALUE + "','" + SEARCHDATE + "','" + SEARCHID + "','" + CAST + "','";
                    sInsertcommand += NUMPRIMARYCHECKS + "','" + NUMCORROBORATIVECHECKS + "','" + NUMPRIMARYOTHERIDSCONFIRMED + "','";
                    sInsertcommand += NUMCORROBORATIVEOTHERIDSCONFIRMED + "','" + CONFIRMATORYDOBS + "','" + TOTALDOBS + "','";
                    sInsertcommand += SANCTIONSWARNING + "','" + SDNWARNING + "','" + PEPWARNING + "','" + ADDRESSWARNING + "','";
                    sInsertcommand += ADDRESSLINKSWARNING + "','" + GONEAWAYWARNING + "','" + DVLAWARNING + "','";
                    sInsertcommand += DECEASEDWARNING + "','" + PASSPORTWARNING + "','" + SEARCHWARNING + "','";
                    sInsertcommand += IDCARDWARNING + "','" + "";
                    sInsertcommand += FRAUDULENTPASSPORTWARNING + "','" + MATCHLEVEL + "','" + APPVERIFIED + "','";
                    sInsertcommand += CDRCURADDLEVEL + "','" + CDSCURADDLEVEL + "','" + LVLCDDOB + "','" + LVLCDCCJ + "','" + LVLCDBAI + "','" + LVLCDFTSE + "','" + DLIDNO + "','" + DLSNAMEMATCH + "','" + DLINITMATCH + "','";
                    sInsertcommand += DLONAMEMATCH + "','" + DLDOBDAYMATCH + "','" + DLDOBMONTHMATCH + "','" + DLDOBYEARMATCH + "','" + PPIDNO + "','" + PPMLINE1 + "','" + PPMLINE2 + "','" + PPSNAMEMATCH.ToString() + "','" + PPFNAMEMATCH.ToString() + "','";
                    sInsertcommand += PPMNAMEMATCH.ToString() + "','" + PPISSUECOUNTRY + "','" + PPNATIONALITY + "','" + PPCHKDIGIT1MATCH.ToString() + "','" + PPCHKDIGIT2MATCH.ToString() + "','" + PPCHKDIGIT3MATCH.ToString() + "','" + PPCHKDIGIT4MATCH.ToString() + "','";
                    sInsertcommand += PPCHKDIGIT5MATCH.ToString() + "','" + PPDOBDAYMATCH + "','" + PPDOBMONTHMATCH + "','" + PPDOBYEARMATCH + "','" + PPEXPIRYDATEOK + "','" + CDRPRADDLEVEL + "','" + CDSPRADDLEVEL + "')";

                    obj.ExecuteCommand(sInsertcommand.ToUpper());

                    obj.StoreEvent("66312", Convert.ToString(Session["RefNo"]), "", "", "", Convert.ToString(Session["RefNo"]));

                }
                catch (Exception ex)
                {
                    obj.StoreEvent("99999", "", "", "", "Callml Insert statement error " + obj.replaceSplChr(sInsertcommand) + "Exception Message:" + obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
                }

            }
            catch (Exception ex)
            {
                obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
                throw ex;
            }
        }
        catch
        {

        }
    }

    public bool bKYCWarning()
    {

        bool bStatus = false;

        try
        {
            if (SANCTIONSWARNING)
            {
                sKYCComment += "Sanctions warning,";
                bStatus = true;
            }

            if (SDNWARNING)
            {
                sKYCComment += "SDN warning,";
                bStatus = true;
            }

            if (PEPWARNING)
            {
                sKYCComment += "PEP warning,";
                bStatus = true;
            }

            if (ADDRESSWARNING)
            {
                sKYCComment += "Address warning,";
                bStatus = true;
            }

            if (ADDRESSLINKSWARNING)
            {
                sKYCComment += "Address link warning,";
                bStatus = true;
            }

            if (GONEAWAYWARNING)
            {
                sKYCComment += "Gone away warning,";
                bStatus = true;
            }

            if (DVLAWARNING)
            {
                sKYCComment += "Driving license warning,";
                bStatus = true;
            }

            if (DECEASEDWARNING)
            {
                sKYCComment += "Deceased warning,";
                bStatus = true;
            }

            if (PASSPORTWARNING)
            {
                sKYCComment += "Passport warning,";
                bStatus = true;
            }

            if (SEARCHWARNING)
            {
                sKYCComment += "Search warning,";
                bStatus = true;
            }

            if (IDCARDWARNING)
            {
                sKYCComment += "ID card warning,";
                bStatus = true;
            }

            if (FRAUDULENTPASSPORTWARNING)
            {
                sKYCComment += "Fraudulent passport warning,";
                bStatus = true;
            }

            sKYCComment = sKYCComment.Length > 0 ? sKYCComment.Substring(0, sKYCComment.Length - 1) : string.Empty;

        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }

        return bStatus;

    }
    #endregion

    private void SendKYCSuccessEmail()
    {
        try
        {
            #region Save password
            //Commented 2023 enhancement remaining
            ///* Save Password in the ACOPCUST table */
            //DataTable dtAcOp = new DataTable();
            //string sQuery = string.Empty;

            //sQuery = "SELECT CUSPWD,CUFNAME FROM ACOPCUST WHERE TRREF='" + obj.NullToSpace(Session["AcOpenRefNo"]) + "'";
            //dtAcOp = obj.ExecuteData(sQuery);

            //if (dtAcOp != null && dtAcOp.Rows.Count > 0)
            //{
            //    string tmpEncrypted = "";
            //    string tmpDecrypted = "";

            //    tmpEncrypted = txtPassword.Text.Trim();

            //    byte[] decodedBytes = Convert.FromBase64String(tmpEncrypted);

            //    string decodedText;

            //    decodedText = Convert.ToString(Encoding.UTF8.GetString(decodedBytes));
            //    tmpDecrypted = decodedText;

            //    if (tmpDecrypted != "")
            //    {
            //        string strupd = "Update acopcust set cuspwd ='" + obj.EncryPass(obj.rplsSnglQots(tmpDecrypted)) + "',CUSEMAIL='" + obj.rplsSnglQots(txtEmailId.Text.Trim()) + "' where trref = '" + obj.NullToSpace(Session["AcOpenRefNo"]) + "'";
            //        obj.ExecuteCommand(strupd);
            //    }

            //}
            ///* Save Password in the ACOPCUST table */
            //End
            #endregion

            AccountDtl oAcD = null;

            string sSubject, sContent, sEmailTo, sTitle, sName, sTitle1, sName1, sTitle2, sName2, sTitle3, sName3;

            sSubject = sContent = sEmailTo = sTitle = sName = sTitle1 = sName1 = sTitle2 = sName2 = sTitle3 = sName3 = string.Empty;

            string sLogo = string.Empty;
            string MsgBody = string.Empty;
            string sHeader = string.Empty;
            string sFooter = string.Empty;

            string pName = obj.RetPolValue("UBI", "PRODUCTNAME");

            oAcD = GetAcDtl();

            if (oAcD != null && oAcD.appList != null)
            {
                sSubject = "UBI (UK)-Union Premier Bond Pre-Account Opening stage - KYC Completed";
                sContent = obj.GetContent("KYC Completed");


                foreach (Applicant oApl in oAcD.appList)
                {
                    switch (oApl.Sequence)
                    {
                        case 1:
                            {
                                sTitle = oApl.Title.ToUpper();
                                sName = oApl.FirstNm.ToUpper() + " " + oApl.MidNm.ToUpper() + " " + oApl.SurNm.ToUpper();
                                sEmailTo = oApl.EmailAddr;
                                break;
                            }
                        case 2:
                            {
                                sTitle1 = oApl.Title.ToUpper();
                                sName1 = oApl.FirstNm.ToUpper() + " " + oApl.MidNm.ToUpper() + " " + oApl.SurNm.ToUpper();
                                break;
                            }
                        case 3:
                            {
                                sTitle2 = oApl.Title.ToUpper();
                                sName2 = oApl.FirstNm.ToUpper() + " " + oApl.MidNm.ToUpper() + " " + oApl.SurNm.ToUpper();
                                break;
                            }
                        case 4:
                            {
                                sTitle3 = oApl.Title.ToUpper();
                                sName3 = oApl.FirstNm.ToUpper() + " " + oApl.MidNm.ToUpper() + " " + oApl.SurNm.ToUpper();
                                break;
                            }
                    }

                }

                sContent = sContent.Replace("(TITLE)", sTitle);
                sContent = sContent.Replace("(NAME)", sName);

                sContent = sContent.Replace("(J1TITLE)", sTitle1);
                sContent = sContent.Replace("(J1NAME)", sName1);

                sContent = sContent.Replace("(J2TITLE)", sTitle2);
                sContent = sContent.Replace("(J2NAME)", sName2);

                sContent = sContent.Replace("(J3TITLE)", sTitle3);
                sContent = sContent.Replace("(J3NAME)", sName3);

                sContent = sContent.Replace("(AMOUNT)", oAcD.InvAmount);
                sContent = sContent.Replace("(TENURE)", oAcD.InvPeriod);
                sContent = sContent.Replace("(REFNO)", oAcD.ReferenceNo);

                sContent = sContent.Replace("(UBISORTCODE)", obj.RetPolValue("UBI", "SORTCODE"));
                sContent = sContent.Replace("(UBIPOOLACNO)", obj.RetPolValue("UBI", "POOLACNO"));

                sContent = sContent.Replace("(PRODUCTNAME)", pName);

                sContent = sContent.Replace("(ROI)", oAcD.InvRateOfInt + " %");  //Added for enhancement 2023

                //obj.SendEmailFromUBI(sEmailTo, sSubject, sContent);

                sLogo = obj.RetPolValue("MAIL", "LOGO");
                sHeader = obj.RetPolValue("MAIL", "HEADER");
                sFooter = obj.RetPolValue("MAIL", "FOOTER");
                sHeader = sHeader.Replace("UBIUKLOGO", sLogo);
                MsgBody = sHeader + sContent + sFooter;

                //obj.SendEmailMessage(sEmailTo, sSubject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer);
                //obj.StoreEvent("99999", "", "", "", "KYC Success Email Sent Successfully", Convert.ToString(Session["RefNo"]));

                try
                {
                    if (obj.SendEmailMessage(sEmailTo, sSubject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer))
                    {
                        try
                        {
                            obj.StoreEvent("99999", "", "", "", "KYC Success Email sent successfully", Convert.ToString(Session["RefNo"]));
                            System.Threading.Thread.Sleep(5000);
                            obj.SendEmailMessage(obj.RetPolValue("MAIL", "COPY"), sSubject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer);
                        }
                        catch (Exception ex)
                        { }
                    }
                    else
                    {
                        try
                        {
                            obj.StoreEvent("88888", "", "", "", "KYC Success Email sending failure", Convert.ToString(Session["RefNo"]));
                            System.Threading.Thread.Sleep(5000);
                            obj.SendEmailMessage(obj.RetPolValue("MAIL", "COPY"), sSubject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer);
                        }
                        catch (Exception ex)
                        { }
                    }
                }
                catch (Exception exinr)
                {
                    obj.StoreEvent("88888", "", "", "", "Unable to send the KYC Success Email. " + obj.replaceSplChr(exinr.ToString()), Convert.ToString(Session["RefNo"]));
                }

            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private void SendKYCPendingEmail()
    {
        string sLogo = string.Empty;
        string MsgBody = string.Empty;
        string sHeader = string.Empty;
        string sFooter = string.Empty;
        try
        {

            #region Save password
            //Commented 2023 enhancement remaining
            ///* Save Password in the ACOPCUST table */
            //DataTable dtAcOp = new DataTable();
            //string sQuery = string.Empty;

            //sQuery = "SELECT CUSPWD,CUFNAME FROM ACOPCUST WHERE TRREF='" + obj.NullToSpace(Session["AcOpenRefNo"]) + "'";
            //dtAcOp = obj.ExecuteData(sQuery);

            //if (dtAcOp != null && dtAcOp.Rows.Count > 0)
            //{
            //    string tmpEncrypted = "";
            //    string tmpDecrypted = "";

            //    tmpEncrypted = txtPassword.Text.Trim();

            //    byte[] decodedBytes = Convert.FromBase64String(tmpEncrypted);

            //    string decodedText;

            //    decodedText = Convert.ToString(Encoding.UTF8.GetString(decodedBytes));
            //    tmpDecrypted = decodedText;

            //    if (tmpDecrypted != "")
            //    {
            //        string strupd = "Update acopcust set cuspwd ='" + obj.EncryPass(tmpDecrypted) + "',CUSEMAIL='" + txtEmailId.Text.Trim() + "' where trref = '" + obj.NullToSpace(Session["AcOpenRefNo"]) + "'";
            //        obj.ExecuteCommand(strupd);
            //    }

            //}
            ///* Save Password in the ACOPCUST table */
            //End
            #endregion

            AccountDtl oAcD = null;
            Applicant oApl = null;
            string sSubject, sContent;
            sSubject = sContent = string.Empty;

            oAcD = GetAcDtl();

            if (oAcD != null && oAcD.appList != null)
            {
                oApl = oAcD.appList[0];

                string pName = obj.RetPolValue("UBI", "PRODUCTNAME");

                sSubject = "UBI (UK) - Union Premier Bond application could not be completed - KYC pending";
                sContent = obj.GetContent("KYC pending");
                sContent = sContent.Replace("(TITLE)", oApl.Title.ToUpper());
                sContent = sContent.Replace("(NAME)", oApl.FirstNm.ToUpper() + " " + oApl.MidNm.ToUpper() + " " + oApl.SurNm.ToUpper());
                sContent = sContent.Replace("(REFNO)", oAcD.ReferenceNo);
                sContent = sContent.Replace("(PRODUCTNAME)", pName);

                sLogo = obj.RetPolValue("MAIL", "LOGO");
                sHeader = obj.RetPolValue("MAIL", "HEADER");
                sFooter = obj.RetPolValue("MAIL", "FOOTER");
                sHeader = sHeader.Replace("UBIUKLOGO", sLogo);
                MsgBody = sHeader + sContent + sFooter;

                //obj.SendEmailFromUBI(oApl.EmailAddr, sSubject, sContent);

                //obj.SendEmailMessage(oApl.EmailAddr, sSubject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer);
                //obj.StoreEvent("99999", "", "", "", "KYC Pending Email Sent Successfully", Convert.ToString(Session["RefNo"]));

                try
                {
                    if (obj.SendEmailMessage(oApl.EmailAddr, sSubject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer))
                    {
                        try
                        {
                            obj.StoreEvent("99999", "", "", "", "KYC Pending Email Sent Successfully", Convert.ToString(Session["RefNo"]));
                            System.Threading.Thread.Sleep(5000);
                            obj.SendEmailMessage(obj.RetPolValue("MAIL", "COPY"), sSubject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer);
                        }
                        catch (Exception ex)
                        { }
                    }
                    else
                    {
                        try
                        {
                            obj.StoreEvent("99999", "", "", "", "KYC Pending Email sending failure", Convert.ToString(Session["RefNo"]));
                            System.Threading.Thread.Sleep(5000);
                            obj.SendEmailMessage(obj.RetPolValue("MAIL", "COPY"), sSubject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer);
                        }
                        catch (Exception ex)
                        { }
                    }
                }
                catch (Exception exinr)
                {
                    obj.StoreEvent("88888", "", "", "", "Unable to send the KYC Pending Email. " + obj.replaceSplChr(exinr.ToString()), Convert.ToString(Session["RefNo"]));
                }
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private void eVarRetrieve()
    {
        string sQuery = string.Empty;
        DataTable dtCusInfo = new DataTable();

        ECUSEMAIL = EFNAME = EMIDNAME = ESNAME = EAMOUNT = ERATE = ETENURE = EPTIT = string.Empty;
        EJ1CUSEMAIL = EJ1FNAME = EJ1MIDNAME = EJ1SNAME = "";
        EJ2CUSEMAIL = EJ2FNAME = EJ2MIDNAME = EJ2SNAME = "";
        EJ3CUSEMAIL = EJ3FNAME = EJ3MIDNAME = EJ3SNAME = "";

        sQuery = "SELECT * FROM TDAPPIND WHERE TDTRNID='" + Session["TRANREF"] + "' AND TDREFNO='" + Session["TRANREF"] + "'";
        dtCusInfo = obj.ExecuteData(sQuery);

        if (dtCusInfo.Rows.Count == 0)
            return;
        else
        {
            foreach (DataRow dr in dtCusInfo.Rows)
            {
                int iSequence = Convert.ToInt32(obj.NullToZero(dr["TDSEQUENCE"]));

                if (iSequence == 1)
                {
                    EPTIT = obj.NullToSpace(dr["TDFATITLE"]);
                    ECUSEMAIL = obj.NullToSpace(dr["TDFAEMAIL"]);
                    EFNAME = obj.NullToSpace(dr["TDFANAME"]);
                    ESNAME = obj.NullToSpace(dr["TDSURNAME"]);
                    EMIDNAME = obj.NullToSpace(dr["TDMINAME"]);
                    EAMOUNT = obj.NullToZero(dr["TDINVAMT"]);
                    ETENURE = obj.NullToZero(dr["TDINVPERIOD"]);
                    ERATE = obj.NullToZero(dr["TDINVRATINS"]);
                }
                else if (iSequence == 2)
                {
                    EJ1CUSEMAIL = obj.NullToSpace(dr["TDFAEMAIL"]);
                    EJ1FNAME = obj.NullToSpace(dr["TDFANAME"]);
                    EJ1MIDNAME = obj.NullToSpace(dr["TDMINAME"]);
                    EJ1SNAME = obj.NullToSpace(dr["TDSURNAME"]);
                }
                else if (iSequence == 3)
                {
                    EJ2CUSEMAIL = obj.NullToSpace(dr["TDFAEMAIL"]);
                    EJ2FNAME = obj.NullToSpace(dr["TDFANAME"]);
                    EJ2MIDNAME = obj.NullToSpace(dr["TDMINAME"]);
                    EJ2SNAME = obj.NullToSpace(dr["TDSURNAME"]);
                }
                else if (iSequence == 4)
                {
                    EJ3CUSEMAIL = obj.NullToSpace(dr["TDFAEMAIL"]);
                    EJ3FNAME = obj.NullToSpace(dr["TDFANAME"]);
                    EJ3MIDNAME = obj.NullToSpace(dr["TDMINAME"]);
                    EJ3SNAME = obj.NullToSpace(dr["TDSURNAME"]);
                }
            }
        }
    }

    private bool SaveAndSendEmail()
    {
        bool bStatus = false;

        try
        {
            if (Page.IsValid == true)
            {
                DataTable dtAcOp = new DataTable();
                string sQuery = string.Empty;

                sQuery = "SELECT CUSPWD,CUFNAME FROM ACOPCUST WHERE TRREF='" + obj.NullToSpace(Session["AcOpenRefNo"]) + "'";
                dtAcOp = obj.ExecuteData(sQuery);

                if (dtAcOp != null && dtAcOp.Rows.Count > 0)
                {
                    SendEmail();
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

    private void SendEmail()
    {
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
                Content = Content.Replace("(TITLE)", oApDtl.Title.ToUpper());

                Content = Content.Replace("(CUSTOMERFIRSTNAME)", oApDtl.FirstNm.ToUpper());
                Content = Content.Replace("(CUSTOMERMIDNAME)", oApDtl.MidNm.ToUpper());
                Content = Content.Replace("(CUSTOMERSURNAME)", oApDtl.SurNm.ToUpper());
                Content = Content.Replace("(EMAILID)", oApDtl.EmailAddr);
                Content = Content.Replace("(REFNO)", AcDtl.ReferenceNo.ToUpper());
                Content = Content.Replace("(URL)", url);
                Content = Content.Replace("(PRODUCTNAME)", pName);

                subject = "UBI(UK) - Online Deposit Taking application saved successfully";

                string sLogo = string.Empty;
                string MsgBody = string.Empty;
                string sHeader = string.Empty;
                string sFooter = string.Empty;
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
                PnlPwdReg.CssClass = "w-90 d-block zindex_1";
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
            //PnlPwdReg.CssClass = "w-90 d-none zindex_1";
        }

    }

    //Commented 2023 enhancement remaining
    //private void SendEmail(string sPassword)
    //{
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
    //            Content = Content.Replace("(TITLE)", oApDtl.Title.ToUpper());

    //            Content = Content.Replace("(CUSTOMERFIRSTNAME)", oApDtl.FirstNm.ToUpper());
    //            Content = Content.Replace("(CUSTOMERMIDNAME)", oApDtl.MidNm.ToUpper());
    //            Content = Content.Replace("(CUSTOMERSURNAME)", oApDtl.SurNm.ToUpper());
    //            Content = Content.Replace("(REFNO)", AcDtl.ReferenceNo.ToUpper());
    //            Content = Content.Replace("(EMAILID)", oApDtl.EmailAddr);
    //            Content = Content.Replace("(PASSWORD)", sPassword);
    //            Content = Content.Replace("(URL)", url);
    //            Content = Content.Replace("(PRODUCTNAME)", pName);

    //            subject = "UBI(UK) - Online Deposit Taking application saved successfully";

    //            string sLogo = string.Empty;
    //            string MsgBody = string.Empty;
    //            string sHeader = string.Empty;
    //            string sFooter = string.Empty;
    //            sLogo = obj.RetPolValue("MAIL", "LOGO");
    //            sHeader = obj.RetPolValue("MAIL", "HEADER");
    //            sFooter = obj.RetPolValue("MAIL", "FOOTER");
    //            sHeader = sHeader.Replace("UBIUKLOGO", sLogo);
    //            MsgBody = sHeader + Content + sFooter;

    //            //obj.SendEmailFromUBI(oApDtl.EmailAddr, subject, Content);

    //            ////obj.SendEmailMessage(oApDtl.EmailAddr, subject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer);

    //            ////obj.StoreEvent("99999", "", "", "", "Retrieve exist Application Mail Send Successfully", Convert.ToString(Session["RefNo"]));

    //            //obj.SendEmailMessage(oApDtl.EmailAddr, subject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer);
    //            //obj.StoreEvent("99999", "", "", "", "Saved application email sent successfully", Convert.ToString(Session["RefNo"]));

    //            try
    //            {
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
    //        PnlPwdReg.CssClass = "w-90 d-none zindex_1";
    //    }

    //}
    //End


    //Customer Feedback - chellappa 20200928
    protected void lnkFBClose_Click(object sender, EventArgs e)
    {
        try
        {
            obj.StoreEvent("99999", "", "", "", "Customer skip the Feedback process", Convert.ToString(Session["RefNo"]));

            AccountDtl FBAcDtl = GetAcDtl();
            if (FBAcDtl != null && FBAcDtl.appList != null && FBAcDtl.appList.Count > 0)
            {
                //Applicant oApDtl = FBAcDtl.appList[0];  
                lblFBAlert.Text = string.Empty;
                ModalPopFeedBck.Hide();
                ModalPopFeedBck.BackgroundCssClass = "sctableBackground d-none";
                PnlFB.CssClass = "w-90 d-none zindex_1";

                Cache.Remove("Cnfrmtn" + Convert.ToString(Session["loginid"]));
                Response.Redirect("Confirmation.aspx?R=" + obj.UrlEncrypt64("Ref=" + FBAcDtl.ReferenceNo + " & AplnSts=S"), false);
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            obj.StoreEvent("99999", "", "", "", "Customer skip the Feedback process", Convert.ToString(Session["RefNo"]));

            AccountDtl FBAcDtl = GetAcDtl();
            if (FBAcDtl != null && FBAcDtl.appList != null && FBAcDtl.appList.Count > 0)
            {
                //Applicant oApDtl = FBAcDtl.appList[0];  
                lblFBAlert.Text = string.Empty;
                ModalPopFeedBck.Hide();
                ModalPopFeedBck.BackgroundCssClass = "sctableBackground d-none";
                PnlFB.CssClass = "w-90 d-none zindex_1";

                Cache.Remove("Cnfrmtn" + Convert.ToString(Session["loginid"]));
                Response.Redirect("Confirmation.aspx?R=" + obj.UrlEncrypt64("Ref=" + FBAcDtl.ReferenceNo + " & AplnSts=S"), false);
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }
    //Customer Feedback - chellappa 20200928
}
