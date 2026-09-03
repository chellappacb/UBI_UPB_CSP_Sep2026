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

public partial class AccountDetails : System.Web.UI.Page
{

    Methods obj = new Methods();
    ValServer val = new ValServer();

    AccountDtl oAcDtl = null;
    CultureInfo provider = new CultureInfo("en-GB");
    string str = String.Empty;
    bool check = false;
    string sCreatedBy = string.Empty;
    string sModifiedBy = string.Empty;

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

            //validate_controls();
        }
        catch (Exception ex)
        {
            //obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            //throw ex;
        }
    }

    //private void validate_controls()
    //{
    //    try
    //    {
    //        AccountDtl AcDtl = null;
    //        AcDtl = (AccountDtl)Session["AcDtl"];

    //        val.PlaceHolder = ErrorPH;
    //        val.RequiredVal(txtEmailId, "Please enter Email ID", "RegEmail");
    //        val.CustomVal(txtEmailId, "Invalid Email ID", ValServer.Validate.Email, "RegEmail");
    //        val.RequiredVal(txtPassword, "Please enter Password", "RegEmail");

    //    }
    //    catch (Exception ex)
    //    {
    //        obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
    //        throw ex;
    //    }

    //}

    public void ValidatePage()
    {
        try
        {
            if ((Session["RefNo"] == null) || (Convert.ToString(Session["RefNo"]) == string.Empty))
            {
                Response.Redirect("SessionTimeout.aspx", false);

            }
            else
            {
                string sCatchNm = "AcDtl" + Session["RefNo"];

                if ((Request.UserAgent.IndexOf("AppleWebKit") > 0))
                {
                    Request.Browser.Adapters.Clear();
                }

                if (ViewState["AcDtl"] == null)
                {
                    ViewState["AcDtl"] = System.Guid.NewGuid();
                    str = ViewState["AcDtl"].ToString().Replace("-", "");
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
            if (Page.IsPostBack == false)
            {
                if (Session["AcDtl"] != null)
                {
                    LoadAplicantDtl();
                    NextButtonVisibility();
                }
                else
                {
                    Response.Redirect("SessionTimeout.aspx", false);
                }
            }

            string sIDDocUpld = obj.RetPolValue("WEB", "IDDOCUPLOAD");
            if (sIDDocUpld == "Y")
            {
                pnlIDDocUploadNote.Visible = true;
            }
            else
            {
                pnlIDDocUploadNote.Visible = false;
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private void NextButtonVisibility()
    {
        try
        {
            oAcDtl = (AccountDtl)Session["AcDtl"];

            if (oAcDtl.appList != null && oAcDtl.appList.Count == oAcDtl.NoOfApplicants && oAcDtl.appList[0].FirstNm.Length > 0)
                btnNext.Visible = true;
            else
                btnNext.Visible = false;

            if (oAcDtl.appList != null && oAcDtl.appList.Count > 0 && oAcDtl.appList[0].FirstNm.Length > 0 && IsDate(obj.NullToSpace(oAcDtl.appList[0].DOB)))
            {
                btnSaveAsDraft.CausesValidation = true;
                btnSaveAsDraft.Visible = true;
            }
            else
            {
                btnSaveAsDraft.CausesValidation = true;
                btnSaveAsDraft.Visible = false;
            }

            // This has to be incorporate in next button click
            //int iSeq = 0;
            //bool bIsPartiallyFilled = false;

            //foreach (Applicant oApln in oAcDtl.appList)
            //{
            //    if (IsFilled(oApln) == false)
            //    {
            //        bIsPartiallyFilled = true;
            //        iSeq = oApln.Sequence;
            //        break;
            //    }
            //}

            //if (bIsPartiallyFilled)
            //{
            //    Cache.Remove("ApDtl" + Convert.ToString(Session["RefNo"]));
            //    Response.Redirect("ApplicantDetails.aspx?A=" + obj.UrlEncrypt64("Seq=" + iSeq.ToString() + "&ActnCd=E&cba=01"), false);
            //}
            //else
            //{
            //    Cache.Remove("AcDtl" + Convert.ToString(Session["RefNo"]));
            //    Response.Redirect("AccountDetails.aspx", false);
            //}

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool IsFilled(Applicant oApln)
    {
        bool bIsFilled = false;

        try
        {
            if (oApln.Title == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            if (oApln.FirstNm == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            if (oApln.SurNm == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            if (oApln.SurNm.Length < 2)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            if (oApln.Gender == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            if (oApln.MaritalSts == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            if (oApln.DOB == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            if (oApln.PlaceOfBirth == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            if (oApln.MothersMaidenNm == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            if (oApln.Citizenship == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            if (oApln.MobileNo == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            //if (oApln.MobileNo.Length != 10)
            //{
            //    bIsFilled = false;
            //    return bIsFilled;
            //}
            if (oApln.EmailAddr == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            else
            {
                string pattern = pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

                if (!Regex.IsMatch(oApln.EmailAddr, pattern))
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
            }
            if (oApln.IdenDtls == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            if (oApln.IdenDtls.ToUpper() == "DRIVING LICENSE")
            {
                if (oApln.DrLceType == string.Empty)
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if (oApln.DrLceIdenNo == string.Empty)
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if (oApln.DrLceIdenNo.Length != 16)
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if (oApln.DrLceExpDt == string.Empty)
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if (!obj.IsDate(oApln.DrLceExpDt))
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if (oApln.DrLcePCode == string.Empty)
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
            }
            if (oApln.IdenDtls.ToUpper() == "PASSPORT")
            {
                //if (oApln.IsUkPsPrt == string.Empty)
                //{
                //    bIsFilled = false;
                //    return bIsFilled;
                //}
                if (oApln.IdenNo == string.Empty)
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if (oApln.IdenNo.Length != 44)
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if (oApln.PsPrtIssDt == string.Empty)
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if (!obj.IsDate(oApln.PsPrtIssDt))
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if (oApln.PsPrtExpDt == string.Empty)
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if (!obj.IsDate(oApln.PsPrtExpDt))
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if (oApln.PsPrtIsCntry == string.Empty)
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if (oApln.PsPrtName == string.Empty)
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if (oApln.PsPrtName.Length != 44)
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
            }


            if (oApln.sof == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            if (oApln.sof.ToUpper() == "others".ToUpper() && oApln.sofOth == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }

            if (oApln.EmpType == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            if (oApln.EmpType.ToUpper() == "Others".ToUpper() && oApln.EmptypOth == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            if (oApln.CurDoorNo == string.Empty && oApln.CurAddr1 == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            if (oApln.CurAddr3 == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            if (oApln.ResidingSince == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            if (!obj.IsDate(oApln.ResidingSince))
            {
                bIsFilled = false;
                return bIsFilled;
            }
            else
            {
                DateTime newDt;
                if (DateTime.TryParse(oApln.ResidingSince, out newDt))
                {
                    double dDiff = (Convert.ToDateTime(newDt, provider) - Convert.ToDateTime(DateTime.Today.AddYears(-1), provider)).TotalDays;
                    if (dDiff > 0)
                    {
                        if (oApln.PreDoorNo == string.Empty && oApln.PreAddr1 == string.Empty)
                        {
                            bIsFilled = false;
                            return bIsFilled;
                        }
                        if (oApln.PreAddr3 == string.Empty)
                        {
                            bIsFilled = false;
                            return bIsFilled;
                        }
                    }
                }
            }
            if (oApln.IsUSperson == string.Empty)
            {
                bIsFilled = false;
                return bIsFilled;
            }
            if (oApln.IsUSperson.ToUpper() == "Yes".ToUpper())
            {
                if (oApln.PriJrsdctn == string.Empty)
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if (oApln.PriTIN == string.Empty)
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if ((oApln.AdJrsdctn1 == string.Empty))
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if ((oApln.AdJrsdctn1 == string.Empty) && (oApln.AdTIN1 != string.Empty))
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if ((oApln.AdJrsdctn2 == string.Empty) && (oApln.AdTIN2 != string.Empty))
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if ((oApln.AdJrsdctn1 != string.Empty) && (oApln.AdTIN1 == string.Empty) && (oApln.ResnNAPTIN == string.Empty))
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                if ((oApln.AdJrsdctn2 != string.Empty) && (oApln.AdTIN2 == string.Empty) && (oApln.ResnNAPTIN == string.Empty))
                {
                    bIsFilled = false;
                    return bIsFilled;
                }
                //if (oApln.PriJrsdctn == string.Empty && oApln.PriTIN == string.Empty && oApln.ResnNAPTIN == string.Empty)
                //{
                //    bIsFilled = false;
                //    return bIsFilled;
                //}
                //if (oApln.PriJrsdctn != string.Empty && oApln.PriTIN == string.Empty)
                //{
                //    bIsFilled = false;
                //    return bIsFilled;
                //}
                //if (oApln.PriJrsdctn == string.Empty && oApln.PriTIN != string.Empty)
                //{
                //    bIsFilled = false;
                //    return bIsFilled;
                //}
                //if (oApln.AdJrsdctn1 != string.Empty && oApln.AdTIN1 == string.Empty)
                //{
                //    bIsFilled = false;
                //    return bIsFilled;
                //}
                //if (oApln.AdJrsdctn1 == string.Empty && oApln.AdTIN1 != string.Empty)
                //{
                //    bIsFilled = false;
                //    return bIsFilled;
                //}
                //if (oApln.AdJrsdctn2 != string.Empty && oApln.AdTIN2 == string.Empty)
                //{
                //    bIsFilled = false;
                //    return bIsFilled;
                //}
                //if (oApln.AdJrsdctn2 == string.Empty && oApln.AdTIN2 != string.Empty)
                //{
                //    bIsFilled = false;
                //    return bIsFilled;
                //}
            }
            bIsFilled = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return bIsFilled;
    }

    private void LoadAplicantDtl()
    {
        try
        {
            oAcDtl = (AccountDtl)Session["AcDtl"];

            if (oAcDtl.IsJntAc.ToUpper() == "Yes".ToUpper())
            {
                rblIsJntAc.SelectedValue = oAcDtl.IsJntAc.ToUpper();

                pnlJointCount.Visible = true;

                if (Convert.ToInt32(obj.NullToZero(oAcDtl.NoOfApplicants)) > 1)
                    rblIsJntCount.SelectedValue = Convert.ToString(oAcDtl.NoOfApplicants);
                else if (Convert.ToInt32(obj.NullToZero(oAcDtl.NoOfApplicants)) == 1)
                    rblIsJntCount.SelectedValue = Convert.ToString(2);
                else { }

                ShowJointPanels(oAcDtl.NoOfApplicants);
            }
            else
            {
                rblIsJntAc.SelectedValue = "NO";
                ShowJointPanelsLoad(1);
                btnPrimaryAdd.Visible = true;
                btnPrimaryEdit.Visible = false;
                btnPrimaryDelete.Visible = false;
            }

            if (oAcDtl.appList != null && oAcDtl.appList.Count > 0)
            {
                foreach (Applicant AppDtl in oAcDtl.appList)
                    ButtonVisibility(AppDtl.Sequence, AppDtl.FirstNm);


                int iLoadedApplicant = oAcDtl.appList.Count();
                int iAppToBeLoad = Convert.ToInt32(oAcDtl.NoOfApplicants) - iLoadedApplicant;
                if (iAppToBeLoad != 0)
                {
                    iAppToBeLoad = iLoadedApplicant + 1;
                    for (; iAppToBeLoad <= Convert.ToInt32(oAcDtl.NoOfApplicants); iAppToBeLoad++)
                        ButtonVisibility(iAppToBeLoad, "");
                }
            }
            else
            {
                for (int i = 1; i <= Convert.ToInt32(oAcDtl.NoOfApplicants); i++)
                    ButtonVisibility(i, "");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ShowJointPanelsLoad(int iNoOfApplicant)
    {
        try
        {

            switch (iNoOfApplicant)
            {
                case 1:
                    {
                        pnlPrimary.Visible = true;
                        pnlJoint1.Visible = false;
                        pnlJoint2.Visible = false;
                        pnlJoint3.Visible = false;
                        break;
                    }
                case 2:
                    {
                        pnlPrimary.Visible = true;
                        pnlJoint1.Visible = true;
                        pnlJoint2.Visible = false;
                        pnlJoint3.Visible = false;
                        break;
                    }
                case 3:
                    {
                        pnlPrimary.Visible = true;
                        pnlJoint1.Visible = true;
                        pnlJoint2.Visible = true;
                        pnlJoint3.Visible = false;
                        break;
                    }
                case 4:
                    {
                        pnlPrimary.Visible = true;
                        pnlJoint1.Visible = true;
                        pnlJoint2.Visible = true;
                        pnlJoint3.Visible = true;
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

    private void ButtonVisibility(int pSeq, string pApplName)
    {
        try
        {
            switch (pSeq)
            {
                case 1:
                    {
                        if (pApplName != string.Empty)
                        {
                            lblPrimaryName.Text = pApplName;
                            btnPrimaryAdd.Visible = false;
                            btnPrimaryEdit.Visible = true;
                            btnPrimaryDelete.Visible = true;
                        }
                        else
                        {
                            btnPrimaryAdd.Visible = true;
                            btnPrimaryEdit.Visible = false;
                            btnPrimaryDelete.Visible = false;

                            btnJointAdd1.Visible = false;
                            btnJointModify1.Visible = false;
                            btnJointDelete1.Visible = false;

                            btnJointAdd2.Visible = false;
                            btnJointModify2.Visible = false;
                            btnJointDelete2.Visible = false;

                            btnJointAdd3.Visible = false;
                            btnJointModify3.Visible = false;
                            btnJointDelete3.Visible = false;
                        }
                        break;
                    }
                case 2:
                    {
                        if (pApplName != string.Empty)
                        {
                            lbljoint1.Text = pApplName;
                            btnJointAdd1.Visible = false;
                            btnJointModify1.Visible = true;
                            btnJointDelete1.Visible = true;
                        }
                        else
                        {
                            if (lblPrimaryName.Text != "Yet to be added")
                            {
                                btnJointAdd1.Visible = true;
                                btnJointModify1.Visible = false;
                                btnJointDelete1.Visible = false;

                                btnJointAdd2.Visible = false;
                                btnJointModify2.Visible = false;
                                btnJointDelete2.Visible = false;

                                btnJointAdd3.Visible = false;
                                btnJointModify3.Visible = false;
                                btnJointDelete3.Visible = false;
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        if (pApplName != string.Empty)
                        {
                            lbljoint2.Text = pApplName;
                            btnJointAdd2.Visible = false;
                            btnJointModify2.Visible = true;
                            btnJointDelete2.Visible = true;
                        }
                        else
                        {
                            if (lbljoint1.Text != "Yet to be added")
                            {
                                btnJointAdd2.Visible = true;
                                btnJointModify2.Visible = false;
                                btnJointDelete2.Visible = false;

                                btnJointAdd3.Visible = false;
                                btnJointModify3.Visible = false;
                                btnJointDelete3.Visible = false;
                            }
                        }
                        break;
                    }
                case 4:
                    {
                        if (pApplName != string.Empty)
                        {
                            lbljoint3.Text = pApplName;
                            btnJointAdd3.Visible = false;
                            btnJointModify3.Visible = true;
                            btnJointDelete3.Visible = true;
                        }
                        else
                        {
                            if (lbljoint2.Text != "Yet to be added")
                            {
                                btnJointAdd3.Visible = true;
                                btnJointModify3.Visible = false;
                                btnJointDelete3.Visible = false;
                            }
                        }
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

    protected void rblIsJntAc_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblIsJntAc.SelectedValue.ToUpper() == "Yes".ToUpper())
            {
                obj.StoreEvent("66302", Convert.ToString(Session["RefNo"]), "Joint", "", "", Convert.ToString(Session["RefNo"]));
            }
            else if (rblIsJntAc.SelectedValue.ToUpper() == "No".ToUpper())
            {
                obj.StoreEvent("66302", Convert.ToString(Session["RefNo"]), "Individual", "", "", Convert.ToString(Session["RefNo"]));
            }

            lblAlert.Text = string.Empty;

            oAcDtl = (AccountDtl)Session["AcDtl"];

            if (rblIsJntAc.SelectedValue.ToUpper() == "Yes".ToUpper())
            {
                if (rblIsJntCount.SelectedValue == "")
                    rblIsJntCount.SelectedValue = "2";

                rblIsJntCount_SelectedIndexChanged(sender, e);

            }
            else if (rblIsJntAc.SelectedValue.ToUpper() == "No".ToUpper())
            {
                if ((oAcDtl != null) && (oAcDtl.appList != null) && (oAcDtl.appList.Count > 0) && (oAcDtl.appList.Count == 1))
                {
                    UpdateSessionObject();

                    pnlJointCount.Visible = false;
                    rblIsJntCount.ClearSelection();
                    ShowJointPanels(1);
                    ButtonVisibility(1, oAcDtl.appList[0].FirstNm);

                    lbljoint1.Text = "Yet to be added";
                    lbljoint2.Text = "Yet to be added";
                    lbljoint3.Text = "Yet to be added";
                }
                else if ((oAcDtl != null) && (oAcDtl.appList != null) && (oAcDtl.appList.Count >= 0) && (oAcDtl.appList.Count != 1))
                {
                    //UpdateSessionObject();

                    pnlPrimary.Visible = true;
                    lblDataLostMsg.Text = "Currently you have " + oAcDtl.appList.Count() + " applicants. Applicant(s) details will be lost except the first applicant. Do you want to continue ?";

                    ModalPopupExtender1.Show();
                    ModalPopupExtender1.BackgroundCssClass = "sctableBackground d-block";
                    PnlMsgBox.CssClass = "d-block zindex_1";

                    pnlJointCount.Visible = false;
                    rblIsJntCount.ClearSelection();
                }
                else
                {
                    UpdateSessionObject();

                    pnlJointCount.Visible = false;
                    pnlPrimary.Visible = true;
                    ButtonVisibility(1, "");
                    pnlJoint1.Visible = false;
                    pnlJoint2.Visible = false;
                    pnlJoint3.Visible = false;
                    rblIsJntCount.ClearSelection();

                }

            }
            NextButtonVisibility();
        }

        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void rblIsJntCount_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            oAcDtl = (AccountDtl)Session["AcDtl"];

            obj.StoreEvent("66303", Convert.ToString(Session["RefNo"]), Convert.ToString(Convert.ToInt32(rblIsJntCount.SelectedValue) - 1), "", "", Convert.ToString(Session["RefNo"]));

            if ((oAcDtl != null) && (oAcDtl.appList != null) && (oAcDtl.appList.Count > Convert.ToInt32(rblIsJntCount.SelectedValue)))
            {
                lblDataLostMsg.Text = "Currently you have " + oAcDtl.appList.Count() + " applicants. The last added applicant(s) details will be lost. Do you want to continue ?";
                ModalPopupExtender1.Show();
                ModalPopupExtender1.BackgroundCssClass = "sctableBackground d-block";
                PnlMsgBox.CssClass = "d-block zindex_1";
            }
            else
            {

                pnlJointCount.Visible = true;

                UpdateSessionObject();

                if (rblIsJntCount.SelectedValue != null)
                    ShowJointPanels(Convert.ToInt32(rblIsJntCount.SelectedValue));

                oAcDtl = (AccountDtl)Session["AcDtl"];

                if (oAcDtl != null && oAcDtl.appList != null && oAcDtl.appList.Count() > 0)
                {
                    int iLoadedApplicant = oAcDtl.appList.Count();
                    int iAppToBeLoad = Convert.ToInt32(oAcDtl.NoOfApplicants) - iLoadedApplicant;

                    foreach (Applicant AppDtl in oAcDtl.appList)
                        ButtonVisibility(AppDtl.Sequence, AppDtl.FirstNm);

                    if (iAppToBeLoad != 0)
                    {
                        iAppToBeLoad = iLoadedApplicant + 1;
                        for (; iAppToBeLoad <= Convert.ToInt32(oAcDtl.NoOfApplicants); iAppToBeLoad++)
                            ButtonVisibility(iAppToBeLoad, "");
                    }
                }
                else
                {
                    for (int i = 1; i < Convert.ToInt32(oAcDtl.NoOfApplicants); i++)
                        ButtonVisibility(i, "");
                }
            }
            NextButtonVisibility();
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private void UpdateSessionObject()
    {
        try
        {
            oAcDtl = (AccountDtl)Session["AcDtl"];

            if (rblIsJntAc.SelectedValue.ToUpper() == "Yes".ToUpper())
            {
                oAcDtl.IsJntAc = "Yes";
                oAcDtl.NoOfApplicants = Convert.ToInt32(rblIsJntCount.SelectedValue);

                if ((oAcDtl.appList != null) && (oAcDtl.appList.Count > Convert.ToInt32(rblIsJntCount.SelectedValue)))
                {
                    List<Applicant> lstAppl = new List<Applicant>();

                    for (int iIndex = 1; iIndex <= Convert.ToInt32(rblIsJntCount.SelectedValue); iIndex++)
                        lstAppl.Add(oAcDtl.appList[iIndex - 1]);

                    oAcDtl.appList = lstAppl;
                }

                Session["AcDtl"] = oAcDtl;
            }
            else
            {
                oAcDtl.IsJntAc = "No";
                oAcDtl.NoOfApplicants = 1;

                if (oAcDtl.appList != null && oAcDtl.appList.Count > 0)
                {
                    List<Applicant> lstAppl = new List<Applicant>();
                    lstAppl.Add(oAcDtl.appList[0]);
                    oAcDtl.appList = lstAppl;
                }

                lbljoint1.Text = "Yet to be added";
                lbljoint2.Text = "Yet to be added";
                lbljoint3.Text = "Yet to be added";

                Session["AcDtl"] = oAcDtl;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ShowJointPanels(int count)
    {
        try
        {
            switch (count)
            {
                case 1:
                    {
                        pnlPrimary.Visible = true;

                        pnlJoint1.Visible = false;
                        pnlJoint2.Visible = false;
                        pnlJoint3.Visible = false;

                        lbljoint1.Text = "Yet to be added";
                        lbljoint2.Text = "Yet to be added";
                        lbljoint3.Text = "Yet to be added";


                        break;
                    }
                case 2:
                    {
                        pnlPrimary.Visible = true;
                        pnlJoint1.Visible = true;
                        pnlJoint2.Visible = false;
                        pnlJoint3.Visible = false;


                        lbljoint2.Text = "Yet to be added";
                        lbljoint3.Text = "Yet to be added";
                        break;
                    }
                case 3:
                    {
                        pnlPrimary.Visible = true;
                        pnlJoint1.Visible = true;
                        pnlJoint2.Visible = true;
                        pnlJoint3.Visible = false;

                        lbljoint3.Text = "Yet to be added";
                        break;
                    }
                case 4:
                    {
                        pnlPrimary.Visible = true;
                        pnlJoint1.Visible = true;
                        pnlJoint2.Visible = true;
                        pnlJoint3.Visible = true;
                        break;
                    }
                default:
                    {
                        pnlPrimary.Visible = false;
                        pnlJoint1.Visible = false;
                        pnlJoint2.Visible = false;
                        pnlJoint3.Visible = false;
                        break;
                    }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            int iSeq = 0;
            bool bIsPartiallyFilled = false;


            btnNext.Enabled = false;

            if (SaveValues())
            {
                oAcDtl = (AccountDtl)Session["AcDtl"];

                //2024
                if (Convert.ToString(Session["Type"]) == "RA" && oAcDtl.appList[0].Sequence.ToString() == "1")
                {
                    if (chkEmailVerSts(oAcDtl.appList[0].EmailAddr.ToString()) == false)
                    {
                        lblAlert.CssClass = "alert font-15 text-danger";
                        lblAlert.Text = "Please verify your email address before proceeding to the next step.";
                        return;
                    }
                }
                else if (Convert.ToString(Session["PrimaryDel"]) == "Yes" && oAcDtl.appList[0].Sequence.ToString() == "1")
                {
                    if (chkEmailVerSts(oAcDtl.appList[0].EmailAddr.ToString()) == false)
                    {
                        lblAlert.CssClass = "alert font-15 text-danger";
                        lblAlert.Text = "Please verify your email address before proceeding to the next step.";
                        return;
                    }
                }
                //End

                foreach (Applicant oApln in oAcDtl.appList)
                {
                    if (IsFilled(oApln) == false)
                    {
                        bIsPartiallyFilled = true;
                        iSeq = oApln.Sequence;
                        break;
                    }
                }

                if (bIsPartiallyFilled)
                {
                    Cache.Remove("ApDtl" + Convert.ToString(Session["RefNo"]));
                    Response.Redirect("ApplicantDetails.aspx?A=" + obj.UrlEncrypt64("Seq=" + iSeq.ToString() + "&ActnCd=E"), false);

                }
                else
                {
                    Cache.Remove("OthDtl" + Convert.ToString(Session["RefNo"]));
                    Response.Redirect("BondDetails.aspx", false);

                }
            }

            //if (SaveValues())
            //{
            //    Cache.Remove("OthDtl" + Convert.ToString(Session["RefNo"]));
            //    Response.Redirect("BondDetails.aspx", false);
            //}

        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
        finally
        {
            btnNext.Enabled = true;
        }

    }

    //2024
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
    //End

    private bool SaveValues()
    {
        bool bStatus = false;
        try
        {
            oAcDtl = (AccountDtl)Session["AcDtl"];

            if (rblIsJntAc.SelectedValue.ToUpper() == "Yes".ToUpper())
            {
                oAcDtl.IsJntAc = "Yes";
                oAcDtl.NoOfApplicants = Convert.ToInt32(rblIsJntCount.SelectedValue);
            }
            else
            {
                oAcDtl.IsJntAc = "No";
                oAcDtl.NoOfApplicants = 1;
            }

            Session["AcDtl"] = oAcDtl;
            bStatus = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bStatus;
    }

    protected void btnPrimaryAdd_Click(object sender, EventArgs e)
    {

        try
        {

            oAcDtl = (AccountDtl)Session["AcDtl"];

            if (rblIsJntAc.SelectedValue.ToUpper() == "Yes".ToUpper())
            {
                oAcDtl.IsJntAc = "Yes";
                oAcDtl.NoOfApplicants = Convert.ToInt32(rblIsJntCount.SelectedValue);
            }
            else
            {
                oAcDtl.IsJntAc = "No";
                oAcDtl.NoOfApplicants = 1;
            }

            List<Applicant> lstApplicant = new List<Applicant>();
            lstApplicant.Add(AddApplicant(1));

            oAcDtl.appList = lstApplicant;
            Session["AcDtl"] = oAcDtl;
            Session["IsJointAc"] = "Y";

            Cache.Remove("ApDtl" + Convert.ToString(Session["RefNo"]));
            Response.Redirect("ApplicantDetails.aspx?A=" + obj.UrlEncrypt64("Seq=1&ActnCd=A"), false);

        }

        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private Applicant AddApplicant(int sequence)
    {
        Applicant applicant = new Applicant();
        try
        {
            applicant.Sequence = sequence;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return applicant;
    }

    protected void btnJointAdd1_Click(object sender, EventArgs e)
    {
        try
        {
            oAcDtl = (AccountDtl)Session["AcDtl"];

            if (rblIsJntAc.SelectedValue.ToUpper() == "Yes".ToUpper())
                oAcDtl.IsJntAc = "Yes";
            else
                oAcDtl.IsJntAc = "No";

            oAcDtl.NoOfApplicants = Convert.ToInt32(rblIsJntCount.SelectedValue);

            oAcDtl.appList.Add(AddApplicant(2));
            Session["AcDtl"] = oAcDtl;

            Cache.Remove("ApDtl" + Convert.ToString(Session["RefNo"]));
            Response.Redirect("ApplicantDetails.aspx?A=" + obj.UrlEncrypt64("Seq=2&ActnCd=A"), false);


        }

        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnJointAdd2_Click(object sender, EventArgs e)
    {
        try
        {
            oAcDtl = (AccountDtl)Session["AcDtl"];

            if (rblIsJntAc.SelectedValue.ToUpper() == "Yes".ToUpper())
                oAcDtl.IsJntAc = "Yes";
            else
                oAcDtl.IsJntAc = "No";

            oAcDtl.NoOfApplicants = Convert.ToInt32(rblIsJntCount.SelectedValue);

            oAcDtl.appList.Add(AddApplicant(3));
            Session["AcDtl"] = oAcDtl;

            Cache.Remove("ApDtl" + Convert.ToString(Session["RefNo"]));
            Response.Redirect("ApplicantDetails.aspx?A=" + obj.UrlEncrypt64("Seq=3&ActnCd=A"), false);

        }

        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnJointAdd3_Click(object sender, EventArgs e)
    {
        try
        {
            AccountDtl AcDtl = null;
            AcDtl = (AccountDtl)Session["AcDtl"];

            if (rblIsJntAc.SelectedValue.ToUpper() == "Yes".ToUpper())
                AcDtl.IsJntAc = "Yes";
            else
                AcDtl.IsJntAc = "No";

            AcDtl.NoOfApplicants = Convert.ToInt32(rblIsJntCount.SelectedValue);

            AcDtl.appList.Add(AddApplicant(4));
            Session["AcDtl"] = AcDtl;

            Cache.Remove("ApDtl" + Convert.ToString(Session["RefNo"]));
            Response.Redirect("ApplicantDetails.aspx?A=" + obj.UrlEncrypt64("Seq=4&ActnCd=A"), false);

        }

        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }

    }

    protected void btnPrimaryEdit_Click(object sender, EventArgs e)
    {
        try
        {
            Cache.Remove("ApDtl" + Convert.ToString(Session["RefNo"]));
            Response.Redirect("ApplicantDetails.aspx?A=" + obj.UrlEncrypt64("Seq=1&ActnCd=E"), false);

        }

        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnJointModify1_Click(object sender, EventArgs e)
    {
        try
        {
            Cache.Remove("ApDtl" + Convert.ToString(Session["RefNo"]));
            Response.Redirect("ApplicantDetails.aspx?A=" + obj.UrlEncrypt64("Seq=2&ActnCd=E"), false);

        }

        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnJointModify2_Click(object sender, EventArgs e)
    {
        try
        {
            Cache.Remove("ApDtl" + Convert.ToString(Session["RefNo"]));
            Response.Redirect("ApplicantDetails.aspx?A=" + obj.UrlEncrypt64("Seq=3&ActnCd=E"), false);

        }

        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnJointModify3_Click(object sender, EventArgs e)
    {
        try
        {
            Cache.Remove("ApDtl" + Convert.ToString(Session["RefNo"]));
            Response.Redirect("ApplicantDetails.aspx?A=" + obj.UrlEncrypt64("Seq=4&ActnCd=E"), false);

        }

        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnPrimaryDelete_Click(object sender, EventArgs e)
    {
        try
        {
            obj.StoreEvent("66304", Convert.ToString(Session["RefNo"]), lblPrimaryName.Text.Trim(), "", "", Convert.ToString(Session["RefNo"]));

            DeleteApplicant(1);
            LoadAplicantDtl();
            NextButtonVisibility();

            Session["PrimaryDel"] = "Yes";  //2024          
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnJointDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            obj.StoreEvent("66304", Convert.ToString(Session["RefNo"]), lbljoint1.Text.Trim(), "", "", Convert.ToString(Session["RefNo"]));

            DeleteApplicant(2);
            LoadAplicantDtl();
            NextButtonVisibility();
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnJointDelete2_Click(object sender, EventArgs e)
    {
        try
        {
            obj.StoreEvent("66304", Convert.ToString(Session["RefNo"]), lbljoint2.Text.Trim(), "", "", Convert.ToString(Session["RefNo"]));

            DeleteApplicant(3);
            LoadAplicantDtl();
            NextButtonVisibility();
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnJointDelete3_Click(object sender, EventArgs e)
    {
        try
        {
            obj.StoreEvent("66304", Convert.ToString(Session["RefNo"]), lbljoint3.Text.Trim(), "", "", Convert.ToString(Session["RefNo"]));

            DeleteApplicant(4);
            LoadAplicantDtl();
            NextButtonVisibility();
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private void DeleteApplicant(int iSequence)
    {
        try
        {
            oAcDtl = (AccountDtl)Session["AcDtl"];

            //2024
            string sSeqNo = oAcDtl.appList[0].Sequence.ToString();
            string sEmail = oAcDtl.appList[0].EmailAddr.ToString();
            string sTempRefNo = Session["RefNo"].ToString();
            DelDataFrmOTPTbl(sSeqNo, sEmail, sTempRefNo);
            //End

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

            lblPrimaryName.Text = "Yet to be added";
            lbljoint1.Text = "Yet to be added";
            lbljoint2.Text = "Yet to be added";
            lbljoint3.Text = "Yet to be added";

            oAcDtl.appList = lstNew;
            Session["AcDtl"] = oAcDtl;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //2024
    private void DelDataFrmOTPTbl(string sSeqNo, string sEmail, string sTempRefNo)
    {
        string sDelData = string.Empty;
        try
        {
            if (sSeqNo != "" && sEmail != "" && sTempRefNo != "")
            {
                obj.StoreEvent("99999", "", "", "", "Initiated to delete the primary email address from OTP table", Convert.ToString(Session["RefNo"]));
                sDelData = "DELETE FROM OTPFOREMAIL WHERE EMOTPREFNO='" + sTempRefNo + "' AND EMOTPEMAIL='" + sEmail + "'";
                obj.ExecuteCommand(sDelData);
                obj.StoreEvent("99999", "", "", "", "successfully deleted the primary email address from OTP table for email " + sEmail, Convert.ToString(Session["RefNo"]));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //End

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
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
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
            throw ex;
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            oAcDtl = (AccountDtl)Session["AcDtl"];

            if (oAcDtl != null)
            {
                if (rblIsJntAc.SelectedValue.ToUpper() == "No".ToUpper())
                {

                    if (oAcDtl.appList != null && oAcDtl.appList.Count > 0)
                    {

                        obj.StoreEvent("66306", Convert.ToString(Session["RefNo"]), "", "", "", Convert.ToString(Session["RefNo"]));

                        Applicant oAppDtl = oAcDtl.appList[0];
                        List<Applicant> oApList = new List<Applicant>();
                        oApList.Add(oAppDtl);
                        oAcDtl.appList = oApList;
                        oAcDtl.NoOfApplicants = 1;

                        pnlJointCount.Visible = false;
                        pnlPrimary.Visible = true;

                        pnlJoint1.Visible = false;
                        pnlJoint2.Visible = false;
                        pnlJoint3.Visible = false;

                        lbljoint1.Text = "Yet to be added";
                        lbljoint2.Text = "Yet to be added";
                        lbljoint3.Text = "Yet to be added";

                        Session["AcDtl"] = oAcDtl;
                    }

                    ModalPopupExtender1.Hide();
                    ModalPopupExtender1.BackgroundCssClass = "sctableBackground d-none";
                    PnlMsgBox.CssClass = "d-none zindex_1";
                }
                else
                {
                    UpdateSessionObject();

                    if (rblIsJntCount.SelectedValue != null)
                        ShowJointPanels(Convert.ToInt32(rblIsJntCount.SelectedValue));

                    oAcDtl = (AccountDtl)Session["AcDtl"];

                    if (oAcDtl != null && oAcDtl.appList != null && oAcDtl.appList.Count() > 0)
                    {
                        int iLoadedApplicant = oAcDtl.appList.Count();
                        int iAppToBeLoad = Convert.ToInt32(oAcDtl.NoOfApplicants) - iLoadedApplicant;

                        foreach (Applicant AppDtl in oAcDtl.appList)
                            ButtonVisibility(AppDtl.Sequence, AppDtl.FirstNm);

                        if (iAppToBeLoad != 0)
                        {
                            iAppToBeLoad = iLoadedApplicant + 1;
                            for (; iAppToBeLoad <= Convert.ToInt32(oAcDtl.NoOfApplicants); iAppToBeLoad++)
                                ButtonVisibility(iAppToBeLoad, "");
                        }
                    }
                    else
                    {
                        for (int i = 1; i < Convert.ToInt32(oAcDtl.NoOfApplicants); i++)
                            ButtonVisibility(i, "");
                    }

                    ModalPopupExtender1.Hide();
                    ModalPopupExtender1.BackgroundCssClass = "sctableBackground d-none";
                    PnlMsgBox.CssClass = "d-none zindex_1";
                }
            }
            NextButtonVisibility();
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupExtender1.Hide();
            ModalPopupExtender1.BackgroundCssClass = "sctableBackground d-none";
            PnlMsgBox.CssClass = "d-none zindex_1";
            LoadAplicantDtl();
            NextButtonVisibility();
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {
        try
        {
            if ((btnSaveAsDraft.CausesValidation == true) && (Page.IsValid == false))
            {
                return;
            }
            else
            {
                string sType = Convert.ToString(Session["Type"]);

                btnSaveAsDraft.Enabled = false;

                if (sType == "NA")
                {
                    oAcDtl = (AccountDtl)Session["AcDtl"];

                    //2024
                    if (Convert.ToString(Session["PrimaryDel"]) == "Yes" && oAcDtl.appList[0].Sequence.ToString() == "1")
                    {
                        if (chkEmailVerSts(oAcDtl.appList[0].EmailAddr.ToString()) == false)
                        {
                            lblAlert.CssClass = "alert font-15 text-danger";
                            lblAlert.Text = "Please verify your email address before proceeding to the next step.";
                            return;
                        }
                    }
                    //End

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

                    //2024
                    if (AcD.appList[0].Sequence.ToString() == "1")
                    {
                        if (chkEmailVerSts(AcD.appList[0].EmailAddr.ToString()) == false)
                        {
                            lblAlert.CssClass = "alert font-15 text-danger";
                            lblAlert.Text = "Please verify your email address before proceeding to the next step.";
                            return;
                        }
                    }
                    //End

                    obj.StoreEvent("99999", "", "", "", "Going to delete RA values From TDAPPIND", Convert.ToString(Session["RefNo"]));
                    //20181115-chellappa
                    if ((AcD.ReferenceNo != null) && (AcD.ReferenceNo != ""))
                    {
                        obj.ExecuteCommand("DELETE FROM TDAPPIND WHERE TDREFNO='" + AcD.ReferenceNo + "' OR TDTRNID = '" + AcD.ReferenceNo + "'");

                        obj.StoreEvent("99999", "", "", "", "Deleted RA values From TDAPPIND", Convert.ToString(Session["RefNo"]));

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
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
        finally
        {
            btnSaveAsDraft.Enabled = true;
        }
    }

    private bool IsDate(string sdate) //20130826
    {
        try
        {
            DateTime dt;
            bool isDate = false;

            try
            {
                if ((sdate.Length > 0) && (sdate.Length == 10))
                    isDate = DateTime.TryParse(sdate, out dt);
                else
                    isDate = false;
            }
            catch
            {
                isDate = false;
            }

            return isDate;
        }
        catch (Exception ex)
        {
            throw ex;
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
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
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

                //20230208 chellappa - link text color change
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

    //                    obj.StoreEvent("99999", "", "", "", "Customer Password and Email updated in acopcust", Convert.ToString(Session["RefNo"]));

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
    //    try
    //    {
    //        AccountDtl AcDtl = GetAcDtl();
    //        if (AcDtl != null && AcDtl.appList != null && AcDtl.appList.Count > 0)
    //        {
    //            Applicant oApDtl = AcDtl.appList[0];

    //            string Content, subject;

    //            string pName = obj.RetPolValue("UBI", "PRODUCTNAME");

    //            string url = obj.RetPolValue("ACOPN", "RETAPPURL") + "?q=" + obj.Encrypt64("RefNo=" + AcDtl.ReferenceNo);

    //            //20230208 chellappa - link text color change
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

    //            string sLogo = string.Empty;
    //            string MsgBody = string.Empty;
    //            string sHeader = string.Empty;
    //            string sFooter = string.Empty;
    //            sLogo = obj.RetPolValue("MAIL", "LOGO");
    //            sHeader = obj.RetPolValue("MAIL", "HEADER");
    //            sFooter = obj.RetPolValue("MAIL", "FOOTER");
    //            sHeader = sHeader.Replace("UBIUKLOGO", sLogo);
    //            MsgBody = sHeader + Content + sFooter;

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

    protected void lnkClose_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupExtender2.Hide();
            ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-none";
            PnlPwdReg.CssClass = "d-none zindex_1";
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }
}