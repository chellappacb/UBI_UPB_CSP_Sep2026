using System;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class RetrieveExistApp : System.Web.UI.Page
{

    Methods obj = new Methods();
    AccountDtl oAcDtl = null;
    Applicant oAplcnt = null;
    CultureInfo provider = new CultureInfo("en-GB");
    ValServer val = new ValServer();

    string str = String.Empty;
    bool check = false;
    bool RDexception = false;
    string sRegUniqueId = string.Empty;

    protected void Page_Init(object sender, System.EventArgs e)
    {

    }

    public void ValidatePage()
    {
        try
        {
            string sCatchNm = "RetieveDtl" + Session["RefNo"];

            if ((Request.UserAgent.IndexOf("AppleWebKit") > 0))
            {
                Request.Browser.Adapters.Clear();
            }

            if (ViewState["RetieveDtl"] == null)
            {
                ViewState["RetieveDtl"] = System.Guid.NewGuid();
                str = ViewState["RetieveDtl"].ToString().Replace("-", "");
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
            throw ex;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //validate_controls();

            if (IsPostBack == false)
            {
                if (Request.QueryString["q"] != null)
                {
                    string[] arrString = obj.GetQString(Request.QueryString["q"]);
                    if (arrString != null && arrString.Count() > 0 && !string.IsNullOrEmpty(arrString[0]))
                    {
                        txtReferNo.Text = arrString[0];
                    }
                }
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", ex.ToString(), "");
        }
    }

    protected void btnRetrieveApp_Click(object sender, EventArgs e)
    {
        string sQuery = string.Empty;
        string sQry = string.Empty;
        string sValues = string.Empty;
        string sAcId = string.Empty;
        string sRefNo = string.Empty;
        string sDOB = string.Empty;
        DataTable dtAcDtl = null;
        DataTable dtDtl = null;
        string fSelBlockedData = string.Empty;
        DataTable dtBlockedData = null;

        try
        {
            if (Page.IsValid == false)
                return;

            if (IsValid())
            {
                if (txtCaptcha.Text == string.Empty)
                {
                    lblmsg.Text = "Security Captcha cannot be blank.";
                    txtCaptcha.Text = "";
                    txtCaptcha.Focus();
                    return;
                }

                if (!IsRefNoValid())
                {
                    lblmsg.Text = "The provided application details do not match our records. Please verify and try again.";
                    txtReferNo.Focus();
                    return;
                }

                if (IsRefNoStsACO())
                {
                    lblmsg.Text = "Please log in through the Existing Customer Portal to proceed, as the bond associated with this reference number has been opened.";
                    txtReferNo.Focus();
                    return;
                }

                string sRefNoVal = txtReferNo.Text.Trim();
                CaptchaStatus status = GetCaptchaStatus(sRefNoVal);
                if (status.IsBlocked == "B")
                {
                    if (!checkCaptchaBlock(status.BlockDateTime))
                    {
                        lblmsg.Text = "Your account has been temporarily blocked. Please wait for some time and then try again.";
                        lblmsg.CssClass = "text-danger";
                        return;
                    }
                    else
                    {
                        ResetCaptcha(sRefNoVal);
                        obj.StoreEvent("99999", "", "", "", "NC Retrieve application successfully captcha Unblocked for Refno " + sRefNoVal, sRefNoVal);
                    }
                }

                if (!IsValidCaptcha())
                {
                    int maxAttempts = Convert.ToInt32(obj.RetPolValue("CAPTCHA", "MAXATTEMPTS"));

                    int sCaptchaAtmpt = status.Attempts + 1;

                    if (sCaptchaAtmpt == maxAttempts)
                    {
                        obj.StoreEvent("99999", "", "", "", "NC Retrieve application Captcha Blocked process initiated", sRefNoVal);

                        BlockCaptcha(sRefNoVal, sCaptchaAtmpt);
                        UpdCaptchaAttpt(sRefNoVal, sCaptchaAtmpt);

                        lblmsg.CssClass = "text-danger";
                        lblmsg.Text = "Your access has been locked due to multiple invalid attempts. Please allow a few moments before trying again.";

                        //txtReferNo.Enabled = false;
                        //txtCaptcha.Enabled = false;
                        //btnRetrieveApp.Enabled = false;
                        obj.StoreEvent("99999", "", "", "", "NC Retrieve application Captcha Blocked process completed", sRefNoVal);
                    }
                    else if (sCaptchaAtmpt == maxAttempts - 1)
                    {
                        lblmsg.CssClass = "text-danger";
                        lblmsg.Text = "The Captcha you entered is incorrect. Your access will be locked after one more invalid attempt. Please enter a valid Captcha.";
                        txtCaptcha.Text = "";
                        txtCaptcha.Focus();

                        UpdCaptchaAttpt(sRefNoVal, sCaptchaAtmpt);
                        obj.StoreEvent("99999", "", "", "", "NC Retrieve application Captcha Second Invalid attempt", sRefNoVal);
                    }
                    else if (sCaptchaAtmpt < maxAttempts)
                    {
                        lblmsg.CssClass = "text-danger";
                        lblmsg.Text = "Captcha is incorrect";
                        txtCaptcha.Text = "";
                        txtCaptcha.Focus();
                        UpdCaptchaAttpt(sRefNoVal, sCaptchaAtmpt);
                        obj.StoreEvent("99999", "", "", "", "NC Retrieve application invalid captcha attempt", sRefNoVal);
                    }

                    return;
                }                

                string sCusBlkDateTime = GetRefNoBlockDate();
                if (sCusBlkDateTime != "")
                {
                    if (!checkEmailBlock(sCusBlkDateTime))
                    {
                        lblmsg.CssClass = "text-center p-2 lblbox error";
                        lblmsg.Text = "Your account has been temporarily blocked. Please wait for some time and then try again.";
                        txtReferNo.Text = "";
                        txtCaptcha.Text = "";
                        return;
                    }
                    else if (checkEmailBlock(sCusBlkDateTime))
                    {
                        obj.ExecuteCommand("UPDATE ACOPCUST SET CUSAAC='Y', CUSBLOCKDATETIME='', LOGINATTEMPT=0 WHERE TRREF = '" + obj.rplsSnglQots(txtReferNo.Text.Trim()) + "'");
                        obj.StoreEvent("99999", "", "", "", "Successfully retrieval application has been Unblocked for reference number " + Convert.ToString(obj.rplsSnglQots(txtReferNo.Text.Trim())), Convert.ToString(obj.rplsSnglQots(txtReferNo.Text.Trim())));
                    }
                }

                //OTP
                string sRefnoVal = txtReferNo.Text.Trim();
                string sEmail = string.Empty, sMobile = string.Empty;
                string sGetRetBondDet = "SELECT TDREFNO,TDFAEMAIL,TDFAMOBNO FROM TDAPPIND WHERE TDREFNO = @TDREFNO";
                DataTable dt = obj.ExecuteDataWthParam(sGetRetBondDet,
                    new SqlParameter("@TDREFNO", txtReferNo.Text.Trim())
                );
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    sEmail = obj.NullToSpace(dr["TDFAEMAIL"]);
                    sMobile = obj.NullToSpace(dr["TDFAMOBNO"]);
                }

                otpResendAttempt.Value = "0";
                if (obj.SendOTPTOCUST(sRefnoVal, sEmail, sMobile, "N", 0, "RetExApp") == true)
                {  
                    txtOTP.Text = "";
                    lblOTPAlert.Text = "";
                    lblOTPMsg.Text = "";
                    ModalPopupEmailOTP.Show();
                    ModalPopupEmailOTP.BackgroundCssClass = "sctableBackground d-block";
                    PnlEmailOTP.CssClass = "d-block zindex_1";
                    lblOTPMsg.CssClass = "d-block text-success";

                    string sMaskedEmail = obj.MaskEmail(sEmail);
                    string sMaskedMobile = obj.MaskMobile(sMobile);

                    lblOTPMsg.Text = "One Time Password (OTP) has been sent to your verified email address ("
                                     + sMaskedEmail + ")";

                    //Timer 60 seconds
                    int secondsLeft = Convert.ToInt32(obj.RetPolValue("OTP", "OTPCount"));
                    hdnSecondsLeft.Value = obj.RetPolValue("OTP", "OTPCount");
                    ScriptManager.RegisterStartupScript(this, GetType(), "startTimer", @"startTimer('" + secondsLeft + "');", true);
                    //End
                }
                //End OTP
               
                //Reset captcha attempt count
                ResetCaptcha(txtReferNo.Text.Trim());
                //End
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), txtReferNo.Text.Trim());
            throw ex;
        }
    }

    #region 'Captcha Block'
    public class CaptchaStatus
    {
        public int Attempts { get; set; }
        public string IsBlocked { get; set; }
        public string BlockDateTime { get; set; }
    }

    private CaptchaStatus GetCaptchaStatus(string sRefNo)
    {
        CaptchaStatus status = new CaptchaStatus();

        string sQryCapBlk = @"SELECT * FROM CaptchaBlockDet WHERE REFNO=@REFNO AND CAPTCHABLKMODULE='NCRETAPP'";
        DataTable dtCapBlk = obj.ExecuteDataWthParam(sQryCapBlk,
            new SqlParameter("@REFNO", sRefNo)
        );

        if (dtCapBlk != null && dtCapBlk.Rows.Count > 0)
        {
            DataRow drCapBlk = dtCapBlk.Rows[0];
            status.Attempts = Convert.ToInt32(obj.NullToSpace(drCapBlk["CAPTCHAATTEMPT"]));
            status.IsBlocked = obj.NullToSpace(drCapBlk["ISBLOCKED"]);
            status.BlockDateTime = obj.NullToSpace(drCapBlk["BLOCKDATETIME"]);
        }
        else
        {
            obj.ExecuteCommandWthParam(@"INSERT INTO CaptchaBlockDet(REFNO, CAPTCHAATTEMPT, ISBLOCKED, BLOCKDATETIME, CAPTCHABLKMODULE)
                                    VALUES (@REFNO, 0, 'N', @BLOCKDATETIME, 'NCRETAPP')",
                new SqlParameter("@REFNO", sRefNo),
                new SqlParameter("@BLOCKDATETIME", DateTime.Now.ToString("yyyyMMddHHmm"))
            );
        }

        return status;
    }

    private bool checkCaptchaBlock(string sCustACBlockTIME)
    {
        bool flag = false;
        int CustBlkTimeDif = -1;
        try
        {
            if (!string.IsNullOrEmpty(sCustACBlockTIME))
            {
                DateTime GNTIME = DateTime.ParseExact(sCustACBlockTIME, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                DateTime CRTIME = DateTime.ParseExact(DateTime.Now.ToString("yyyyMMddHHmm"), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                CustBlkTimeDif = (int)(CRTIME - GNTIME).TotalMinutes;
            }
            string sOTPValidMin = obj.RetPolValue("CAPTCHA", "AutoUnblock");
            int OTPValTime = Convert.ToInt32(sOTPValidMin);
            if (CustBlkTimeDif > OTPValTime)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), txtReferNo.Text.Trim());
            throw ex;
        }
        return flag;
    }

    private void ResetCaptcha(string sRefNo)
    {
        try
        {
            obj.ExecuteCommandWthParam(@"UPDATE CaptchaBlockDet SET CAPTCHAATTEMPT=0, ISBLOCKED='N', BLOCKDATETIME='' WHERE REFNO=@REFNO AND CAPTCHABLKMODULE='NCRETAPP'",
                new SqlParameter("@REFNO", sRefNo)
            );
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), sRefNo);
            throw ex;
        }
    }


    private void BlockCaptcha(string sRefNo, int attempt)
    {
        try
        {
            obj.ExecuteCommandWthParam(@"UPDATE CaptchaBlockDet SET ISBLOCKED='B',CAPTCHAATTEMPT=@CAPTCHAATTEMPT, BLOCKDATETIME=@BLOCKDATETIME WHERE REFNO=@REFNO AND CAPTCHABLKMODULE='NCRETAPP'",
                new SqlParameter("@CAPTCHAATTEMPT", attempt),
                new SqlParameter("@BLOCKDATETIME", DateTime.Now.ToString("yyyyMMddHHmm")),
                new SqlParameter("@REFNO", sRefNo)
            );
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), sRefNo);
            throw ex;
        }
    }

    private void UpdCaptchaAttpt(string sRefNo, int sCaptchaAtmpt)
    {
        try
        {
            if (sRefNo != "")
            {
                obj.ExecuteCommandWthParam(@"UPDATE CaptchaBlockDet SET CAPTCHAATTEMPT=@CAPTCHAATTEMPT WHERE REFNO=@REFNO AND CAPTCHABLKMODULE='NCRETAPP'",
                    new SqlParameter("@CAPTCHAATTEMPT", sCaptchaAtmpt),
                    new SqlParameter("@REFNO", sRefNo)
                );
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(ViewState["sCusID"]));
            throw ex;
        }
    }
    #endregion

    private void SuccRedirect()
    {
        try
        {

            string sQry = " SELECT TDSTATUS FROM TDAPPIND WHERE TDREFNO='" + obj.rplsSnglQots(txtReferNo.Text.Trim()).ToUpper() + "'";
            DataTable dtDtl = obj.ExecuteData(sQry);

            if (dtDtl.Rows.Count > 0)
            {
                string sStatus = obj.NullToSpace(dtDtl.Rows[0]["TDSTATUS"]);
                if (sStatus == "APN")
                {
                    Session["Type"] = "RA";
                    Session["AcDtl"] = LoadAccountDetail();

                    obj.StoreEvent("66314", txtReferNo.Text.Trim(), "", "", "", Convert.ToString(Session["RefNo"]));

                    oAcDtl = (AccountDtl)Session["AcDtl"];

                    int iSeq = 0;
                    bool bIsPartiallyFilled = false;

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
                        Response.Redirect("ApplicantDetails.aspx?A=" + obj.UrlEncrypt64("Seq=" + iSeq.ToString() + "&ActnCd=E&cba=01"), false);

                    }
                    else
                    {
                        Cache.Remove("AcDtl" + Convert.ToString(Session["RefNo"]));
                        Response.Redirect("AccountDetails.aspx", false);

                    }
                }
                //New enhancement 2023
                else if (sStatus == "KYP" || sStatus == "KYS" || sStatus == "FRP" || sStatus == "FRR" || sStatus == "CQR")
                {
                    Response.Redirect("InvestmentDetails.aspx?A=" + obj.UrlEncrypt64("RefNo=" + txtReferNo.Text.Trim()), false);
                }
                else if (sStatus == "CNC")
                {
                    lblmsg.Text = "Sorry, your application has been declined. The login credentials you used are no longer valid!";
                    txtCaptcha.Text = "";
                }
                else
                {
                    lblmsg.Text = "Your account has been opened. To view your bond information, kindly sign in to the Secure ODT portal using the credentials sent to your registered email address. Thanks!";
                    txtCaptcha.Text = "";
                }
                //                
            }

        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), txtReferNo.Text.Trim());
            throw ex;
        }
    }


    #region "OTP"
    //OTP
    protected void btnOTPVerify_Click(object sender, EventArgs e)
    {
        try
        {
            string isResenBtnEnable = string.Empty;

            string sRefNo = string.Empty;
            sRefNo = txtReferNo.Text.Trim();

            int sOTPAttmtCount = Convert.ToInt32(obj.RetPolValue("OTP", "OTPAttmptCount"));

            OTPInfo sOTPVal = obj.GetOTPVal(sRefNo, "RetExApp");

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
                lblOTPAlert.Text = "OTP cannot be blank";
                txtOTP.Focus();
            }
            else if (sOTPVal.OTP != txtOTP.Text)
            {
                int sOTPAttmptCntVal = 0;
                sOTPAttmptCntVal = sOTPVal.OTPATTEMPT + 1;

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
                    lblOTPAlert.Text = "You have entered an Invalid OTP. Your access will be locked on your next invalid attempt.";
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

                    //Lock OTP details
                    if (sOTPVal.CUSTID != "")
                    {
                        obj.LockOTPInHisTbl(sOTPVal.CUSTID, sOTPVal.OTPMODULE, "VERIFY");
                        obj.BlockCustAccOTP(sOTPVal.CUSTID, sOTPVal.OTPMODULE, "VERIFY");
                    }
                    //End

                    PopupEmailBlock.Show();
                    PopupEmailBlock.BackgroundCssClass = "sctableBackground d-block";
                    PnlEmailBlock.CssClass = "d-block zindex_1";
                    lblBlockTxt.Text = "You have exceeded the maximum attempts for OTP verification. Hence, your access has been temporarily locked.";
                    txtReferNo.Text = "";
                    txtCaptcha.Text = "";
                }
                else if (sOTPAttmptCntVal < sOTPAttmtCount)
                {
                    lblOTPMsg.Text = "";
                    lblOTPMsg.CssClass = "d-none";

                    lblOTPAlert.CssClass = "d-block text-danger";
                    lblOTPAlert.Text = "Invalid OTP";
                    txtOTP.Focus();
                }

                //Update OTP attempt count
                if (sOTPVal.CUSTID != "")
                {
                    obj.UpdOTPAttCnt(sOTPAttmptCntVal, sOTPVal.CUSTID, sOTPVal.OTPMODULE);
                }
                //End                
            }
            else if (obj.checkOTPExp(sOTPVal.OTPGNTIME, sOTPVal.OTP, sOTPVal.OTPMODULE, sOTPVal.CUSTID) == true)
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
                lblOTPAlert.Text = "Your OTP has expired. Please click 'RESEND' to receive a new one.";
                txtOTP.Focus();
            }
            else
            {
                obj.UpdateOTPVerSuccSts(sOTPVal.CUSTID, sOTPVal.OTPMODULE);
                ModalPopupEmailOTP.Hide();
                ModalPopupEmailOTP.BackgroundCssClass = "d-none";
                PnlEmailOTP.CssClass = "d-none";

                SuccRedirect();
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), txtReferNo.Text.Trim());
            throw ex;
        }
    }

    protected void btnResendOTP_Click(object sender, EventArgs e)
    {
        try
        {
            //otpAttempt.Value = "1";
            otpResendAttempt.Value = Convert.ToString(Convert.ToInt32(otpResendAttempt.Value) + 1);
            int sOTPResendAttCnt = Convert.ToInt32(obj.RetPolValue("OTP", "OTPResendAttCount"));

            string sRefno = string.Empty;
            sRefno = txtReferNo.Text.Trim();

            int sOTPResendCntVal = 0;
            OTPInfo sOTPVal = obj.GetOTPVal(sRefno, "RetExApp");
            sOTPResendCntVal = sOTPVal.OTPRESENDCOUNT + 1;

            if ((sOTPResendCntVal < sOTPResendAttCnt) || (sOTPResendCntVal <= sOTPResendAttCnt))
            {
                if (obj.SendOTPTOCUST(sRefno, sOTPVal.OTPEMAIL, sOTPVal.OTPMOBILE, "Y", sOTPResendCntVal, "RetExApp") == true)
                {
                    ModalPopupEmailOTP.Show();
                    ModalPopupEmailOTP.BackgroundCssClass = "sctableBackground d-block";
                    PnlEmailOTP.CssClass = "d-block zindex_1";

                    if (sOTPResendCntVal == sOTPResendAttCnt)
                    {
                        lblOTPMsg.Text = "";
                        lblOTPMsg.CssClass = "d-none";
                        lblOTPAlert.CssClass = "d-block text-danger";
                        lblOTPAlert.Text = "Your access will be locked upon clicking the \"Resend\" button again";
                    }
                    else
                    {
                        lblOTPAlert.Text = "";
                        lblOTPAlert.CssClass = "d-none";
                        lblOTPMsg.CssClass = "d-block bodytext text-success";

                        string sMaskedEmail = obj.MaskEmail(sOTPVal.OTPEMAIL);
                        string sMaskedMobile = obj.MaskMobile(sOTPVal.OTPMOBILE);

                        lblOTPMsg.Text = "One Time Password (OTP) has been sent to your verified email address ("
                                         + sMaskedEmail + ")";
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
                //Lock OTP details
                if (sRefno != "")
                {
                    obj.LockOTPInHisTbl(sOTPVal.CUSTID, sOTPVal.OTPMODULE, "RESEND");
                    obj.BlockCustAccOTP(sOTPVal.CUSTID, sOTPVal.OTPMODULE, "RESEND");
                }
                //End

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
            if (sOTPVal.CUSTID != "")
            {
                obj.UpdOTPResendCnt(sOTPResendCntVal, sOTPVal.CUSTID, sOTPVal.OTPMODULE);
            }
            //End              
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), txtReferNo.Text.Trim());
            throw ex;
        }
    }

    protected void lnkClose_Click(object sender, EventArgs e)
    {
        try
        {
            string sRefNo = txtReferNo.Text.Trim();
            OTPInfo sOTPVal = obj.GetOTPVal(sRefNo, "RetExApp");
            obj.LockOTPInHisTbl(sOTPVal.CUSTID, sOTPVal.OTPMODULE, "CLOSE");

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
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), txtReferNo.Text.Trim());
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
            Response.Redirect("Index.aspx", false);
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), txtReferNo.Text.Trim());
            throw ex;
        }
    }
    //End
    #endregion

    private bool IsValid()
    {
        try
        {
            string sRefNo = txtReferNo.Text.Trim();

            if (sRefNo == "")
            {
                lblmsg.Text = "Reference number cannot be blank";
                lblmsg.Visible = true;
                return false;
            }

            lblmsg.Text = "";
            return true;
        }
        catch
        {
            return false;
        }
    }

    private Boolean IsValidCaptcha()
    {
        bool bStatus = false;
        try
        {
            Int32 sessKey;
            Int32 inputKey = txtCaptcha.Text.GetHashCode();
            Int32.TryParse(Session["key"].ToString(), out sessKey);
            if (!(sessKey.Equals(inputKey)))
            {
                return false;
            }
            else
            {
                lblmsg.Text = string.Empty;
                bStatus = true;
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(ViewState["sCusID"]));
            throw ex;
        }

        return bStatus;
    }

    private bool IsRefNoValid()
    {
        bool bPwdValid = false;
        try
        {
            string fSelect = "SELECT TRREF FROM ACOPCUST WHERE TRREF = @TDREFNO";
            DataTable dt = obj.ExecuteDataWthParam(fSelect,
                new SqlParameter("@TDREFNO", txtReferNo.Text.Trim())
            );
            if (dt != null && dt.Rows.Count > 0)
            {
                bPwdValid = true;
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), txtReferNo.Text.Trim());
            throw ex;
        }
        return bPwdValid;
    }

    private bool IsRefNoStsACO()
    {
        bool bRefNoValid = false;
        try
        {
            string fSelect = "SELECT A.TDREFNO FROM TDAPPIND A INNER JOIN ACCINPF B ON A.TDREFNO=B.RID WHERE A.TDREFNO = @TDREFNO";
            DataTable dt = obj.ExecuteDataWthParam(fSelect,
                new SqlParameter("@TDREFNO", txtReferNo.Text.Trim())
            );
            if (dt != null && dt.Rows.Count > 0)
            {
                bRefNoValid = true;
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), txtReferNo.Text.Trim());
            throw ex;
        }
        return bRefNoValid;
    }

    private string GetRefNoBlockDate()
    {
        try
        {
            string query = "SELECT CUSBLOCKDATETIME  FROM ACOPCUST WHERE TRREF = @TDREFNO AND CUSAAC = @CUSAAC";
            DataTable dt = obj.ExecuteDataWthParam(query,
            new SqlParameter("@TDREFNO", txtReferNo.Text.Trim()),
            new SqlParameter("@CUSAAC", "B")
            );
            if (dt != null && dt.Rows.Count > 0)
            {
                return obj.NullToSpace(dt.Rows[0]["CUSBLOCKDATETIME"]);
            }

            return "";
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), txtReferNo.Text.Trim());
            throw ex;
        }
    }

    private bool checkEmailBlock(string sCustACBlockTIME)
    {
        bool flag = false;
        int CustBlkTimeDif = -1;
        try
        {
            if (!string.IsNullOrEmpty(sCustACBlockTIME))
            {
                DateTime GNTIME = DateTime.ParseExact(sCustACBlockTIME, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                DateTime CRTIME = DateTime.ParseExact(DateTime.Now.ToString("yyyyMMddHHmm"), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                CustBlkTimeDif = (int)(CRTIME - GNTIME).TotalMinutes;
            }
            string sOTPValidMin = obj.RetPolValue("OTP", "AutoUnblock");
            int OTPValTime = Convert.ToInt32(sOTPValidMin);
            if (CustBlkTimeDif > OTPValTime)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), txtReferNo.Text.Trim());
            throw ex;
        }
        return flag;
    }

    private void UpdLogAttpt(string sRefNo, int LogAttpt)
    {
        try
        {
            if (sRefNo != "")
            {
                obj.ExecuteCommand("UPDATE ACOPCUST SET LOGINATTEMPT='" + LogAttpt + "' WHERE TRREF='" + obj.rplsSnglQots(sRefNo) + "'");
                obj.StoreEvent("99999", "", "", "", "Update invalid login attempt count", Convert.ToString(obj.rplsSnglQots(txtReferNo.Text.Trim())));
            }
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
            }

            //if (obj.IsExistingcustomer(oApln.FirstNm + oApln.MidNm + oApln.SurNm, oApln.DOB))
            //{
            //    bIsFilled = false;
            //    return bIsFilled;
            //}

            bIsFilled = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return bIsFilled;
    }

    private AccountDtl LoadAccountDetail()
    {
        AccountDtl oAcDtl = null;
        string sQry = string.Empty;
        DataTable dtAcDtl = null;
        try
        {

            sQry = " SELECT TEMPREF,TDREFNO,TDISJOINT,TDTOTAPP,TDINVAMT,TDINVPERIOD,TDINVRATINS,TDREPAYINS,TDEDOTH," +
                   " TDAIOTHRBNKNA AS OTHBANKNM,TDOTHSRCODE AS OTHBANKSC, TDOTHACNO AS OTHBANKACNO," +
                   " TDISPRIMARY,TDSEQUENCE,TDFATITLE,TDFANAME,TDMINAME,TDSURNAME,TDGNDR,TDFADOB,TDMOTMDNME,TDFACITIZEN,TDFAMARITAL," +
                   " TDFAMAROTH,TDFARESINO,TDFAMOBNO,TDFAEMAIL,TDPOB,TDNATINSNO," +
                   " TDFACURADD1 CADDR1,TDFACURADD2 CADDR2,TDFACURADD3 CADDR3,TDFAPCODE CPCODE,TDFACRY CCOUNTY,TDCURCOUNTRY AS CCNTRY,TDFARESINCE," +
                   " TDFAPRADD1 PADDR1,TDFAPRADD2 PADDR2,TDFAPRADD3 PADDR3,TDFAPRPCODE PPCODE,TDFAPRCRY PCOUNTY,TDPRECOUNTRY PCNTRY," +
                   " TDIDDTLS AS IDDTLS, TDFAPASSNO AS PASSPORTNO, TDDOI AS PPDOI,TDDOE AS PPDOE,TDPOI AS PPIC,TDFAPANAME,PPTYPE,DLTYPE," +
                   " TDFADVLANO DLNO,TDDVLDOE,TDDVLPCODE,TDEMPDET," +
                   " TDJOINTADD1 SAMEADDR, TDSTATUS,Renewal,TDAAC,PRVPRESENT,ISUKPERSON AS [ISUSPERSON],PRIJRSDCTN,PRITIN,ADJRSDCTN1,ADTIN1,ADJRSDCTN2,ADTIN2,RESNNAPTIN,PAYTAX,USCITIZEN,GREENCARD,REALESTATE,NOTAX,SOFDET,SOFOTH,TDEMVERIFIEDSTS" +
                   " FROM TDAPPIND WHERE TDREFNO = '" + txtReferNo.Text.Trim().ToUpper() + "' OR TDTRNID ='" + txtReferNo.Text.Trim().ToUpper() + "' ORDER BY ID ASC";

            dtAcDtl = obj.ExecuteData(sQry);

            if (dtAcDtl != null && dtAcDtl.Rows.Count > 0)
            {
                oAcDtl = new AccountDtl();

                //Session["RefNo"] = CreateRefNo("ACOPEN");
                Session["RefNo"] = obj.NullToSpace(dtAcDtl.Rows[0]["TEMPREF"]);
                Session["AcOpenRefNo"] = obj.NullToSpace(dtAcDtl.Rows[0]["TDREFNO"]);

                oAcDtl.RegUniqueId = Convert.ToString(Session["RefNo"]);

                oAcDtl.ReferenceNo = obj.NullToSpace(dtAcDtl.Rows[0]["TDREFNO"]);
                oAcDtl.IsJntAc = obj.NullToSpace(dtAcDtl.Rows[0]["TDISJOINT"]);

                oAcDtl.NoOfApplicants = Convert.ToInt32(obj.NullToZero(dtAcDtl.Rows[0]["TDTOTAPP"]));

                //Added for not load joint panel issue while retrieve app - chellappa 20210128
                //if ((oAcDtl.IsJntAc.ToUpper() == "yes".ToUpper()) && (oAcDtl.NoOfApplicants == 1))
                //{
                //    oAcDtl.IsJntAc = "";
                //    oAcDtl.IsJntAc = "No";
                //}
                //

                oAcDtl.InvAmount = obj.NullToSpace(dtAcDtl.Rows[0]["TDINVAMT"]);
                oAcDtl.InvPeriod = obj.NullToSpace(dtAcDtl.Rows[0]["TDINVPERIOD"]);

                //oAcDtl.InvRateOfInt = obj.NullToSpace(dtAcDtl.Rows[0]["TDINVRATINS"]); commented by ROI issue 20240918
                //Added for ROI issue  2025....
                string sInvPeriod = obj.NullToSpace(dtAcDtl.Rows[0]["TDINVPERIOD"]);
                if (sInvPeriod != null)
                {
                    string fROIsel = string.Empty;
                    fROIsel = "select RateInt from RATINTPF where prodname='" + sInvPeriod + "'";
                    DataTable dtROI = obj.ExecuteData(fROIsel);
                    if (dtROI.Rows.Count > 0)
                    {
                        oAcDtl.InvRateOfInt = obj.NullToZero(dtROI.Rows[0]["RateInt"]);
                    }
                }
                else
                {
                    oAcDtl.InvRateOfInt = obj.NullToSpace(dtAcDtl.Rows[0]["TDINVRATINS"]);
                }
                //End

                oAcDtl.RepayIns = obj.NullToSpace(dtAcDtl.Rows[0]["TDREPAYINS"]);
                oAcDtl.OthBankName = obj.NullToSpace(dtAcDtl.Rows[0]["OTHBANKNM"]);
                oAcDtl.OthBankSC = obj.NullToSpace(dtAcDtl.Rows[0]["OTHBANKSC"]);
                oAcDtl.OthBankAcNo = obj.NullToSpace(dtAcDtl.Rows[0]["OTHBANKACNO"]);


                oAcDtl.appList = new List<Applicant>();

                foreach (DataRow dr in dtAcDtl.Rows)
                {
                    Applicant oAplcnt = new Applicant();

                    oAplcnt.ReferenceNo = obj.NullToSpace(dr["TDREFNO"]);

                    //Primary account or not
                    oAplcnt.IsPrimary = obj.NullToSpace(dr["TDISPRIMARY"]);

                    oAplcnt.Sequence = Convert.ToInt32(obj.NullToZero(dr["TDSEQUENCE"]));

                    //2024 Email verification status
                    oAplcnt.EMVerSts = obj.NullToSpace(dr["TDEMVERIFIEDSTS"]);
                    //End

                    //Personal Details        
                    oAplcnt.Title = obj.NullToSpace(dr["TDFATITLE"]);
                    oAplcnt.FirstNm = obj.NullToSpace(dr["TDFANAME"]);
                    oAplcnt.MidNm = obj.NullToSpace(dr["TDMINAME"]);
                    oAplcnt.SurNm = obj.NullToSpace(dr["TDSURNAME"]);
                    oAplcnt.Gender = obj.NullToSpace(dr["TDGNDR"]);
                    oAplcnt.DOB = obj.CTOD(obj.NullToSpace(dr["TDFADOB"]));
                    oAplcnt.Citizenship = obj.NullToSpace(dr["TDFACITIZEN"]);
                    oAplcnt.MaritalSts = obj.NullToSpace(dr["TDFAMARITAL"]);

                    //if (oAplcnt.MaritalSts.ToUpper() == "OTHERS")
                    //{
                    //    oAplcnt.MaritalStsOth = obj.NullToSpace(dr["MARITALSTSOTH"]);
                    //}

                    oAplcnt.MothersMaidenNm = obj.NullToSpace(dr["TDMOTMDNME"]);
                    oAplcnt.PlaceOfBirth = obj.NullToSpace(dr["TDPOB"]);
                    oAplcnt.NINO = obj.NullToSpace(dr["TDNATINSNO"]);

                    //Contact Details        
                    oAplcnt.HomeTelNo = obj.NullToSpace(dr["TDFARESINO"]);
                    oAplcnt.MobileNo = obj.NullToSpace(dr["TDFAMOBNO"]);
                    oAplcnt.EmailAddr = obj.NullToSpace(dr["TDFAEMAIL"]);

                    //Identification Details
                    oAplcnt.IdenDtls = obj.NullToSpace(dr["IDDTLS"]);

                    //if (oAplcnt.IdenDtls.ToUpper() == "Passport".ToUpper())
                    //{
                    //    oAplcnt.IdenNo = obj.NullToSpace(dr["PASSPORTNO"]);
                    //    //oAplcnt.IsUkPsPrt = obj.NullToSpace(dr["ISUKPSPRT"]);
                    //    oAplcnt.PsPrtName = obj.NullToSpace(dr["TDFAPANAME"]);
                    //    oAplcnt.PsPrtIsCntry = obj.NullToSpace(dr["PPIC"]);
                    //    oAplcnt.PsPrtIssDt = obj.CTOD(obj.NullToSpace(dr["PPDOI"]));
                    //    oAplcnt.PsPrtExpDt = obj.CTOD(obj.NullToSpace(dr["PPDOE"]));
                    //}
                    //else
                    //{
                    //    oAplcnt.IdenNo = obj.NullToSpace(dr["DLNO"]);
                    //    oAplcnt.DrLceExpDt = obj.CTOD(obj.NullToSpace(dr["TDDVLDOE"]));
                    //    //oAplcnt.DrLcePCode = obj.CTOD(obj.NullToSpace(dr["TDDVLPCODE"]));
                    //    oAplcnt.DrLcePCode = obj.NullToSpace(dr["TDDVLPCODE"]);
                    //}

                    if (oAplcnt.IdenDtls.ToUpper() == "Passport".ToUpper())
                    {
                        oAplcnt.IdenNo = obj.NullToSpace(dr["PASSPORTNO"]);
                        oAplcnt.IsUkPsPrt = obj.NullToSpace(dr["PPTYPE"]);
                        //oAplcnt.IsUkPsPrt = obj.NullToSpace(dr["ISUKPSPRT"]);
                        oAplcnt.PsPrtName = obj.NullToSpace(dr["TDFAPANAME"]);
                        oAplcnt.PsPrtIsCntry = obj.NullToSpace(dr["PPIC"]);
                        oAplcnt.PsPrtIssDt = obj.CTOD(obj.NullToSpace(dr["PPDOI"]));
                        oAplcnt.PsPrtExpDt = obj.CTOD(obj.NullToSpace(dr["PPDOE"]));
                    }
                    else
                    {
                        oAplcnt.DrLceType = obj.NullToSpace(dr["DLTYPE"]);
                        oAplcnt.IdenNo = obj.NullToSpace(dr["DLNO"]);
                        oAplcnt.DrLceExpDt = obj.CTOD(obj.NullToSpace(dr["TDDVLDOE"]));
                        oAplcnt.DrLcePCode = obj.NullToSpace(dr["TDDVLPCODE"]);
                    }

                    //Employee Details
                    oAplcnt.EmpType = obj.NullToSpace(dr["TDEMPDET"]);

                    if (oAplcnt.EmpType == "OTHERS")
                    {
                        oAplcnt.EmptypOth = obj.NullToSpace(dr["TDEDOTH"]);
                    }

                    //FATCA
                    oAplcnt.IsUSperson = obj.NullToSpace(dr["ISUSPERSON"]);
                    if (oAplcnt.IsUSperson == "Y")
                    {
                        oAplcnt.IsUSperson = "Yes";
                        oAplcnt.PriJrsdctn = obj.NullToSpace(dr["PRIJRSDCTN"]);
                        oAplcnt.PriTIN = obj.NullToSpace(dr["PRITIN"]);

                        oAplcnt.AdJrsdctn1 = obj.NullToSpace(dr["ADJRSDCTN1"]);
                        oAplcnt.AdTIN1 = obj.NullToSpace(dr["ADTIN1"]);

                        oAplcnt.AdJrsdctn2 = obj.NullToSpace(dr["ADJRSDCTN2"]);
                        oAplcnt.AdTIN2 = obj.NullToSpace(dr["ADTIN2"]);

                        oAplcnt.ResnNAPTIN = obj.NullToSpace(dr["RESNNAPTIN"]);
                    }
                    else if (oAplcnt.IsUSperson == "N")                 //modified chellappa
                    {
                        oAplcnt.IsUSperson = "No";
                    }
                    else
                        oAplcnt.IsUSperson = "";


                    //FATCA DETAILS -chella-20171005
                    oAplcnt.PayTax = obj.NullToSpace(dr["PAYTAX"]);
                    if (oAplcnt.PayTax == "Y")
                        oAplcnt.PayTax = "Yes";
                    else if (oAplcnt.PayTax == "N")
                        oAplcnt.PayTax = "No";
                    else
                        oAplcnt.PayTax = "";

                    oAplcnt.USCitizen = obj.NullToSpace(dr["USCITIZEN"]);
                    if (oAplcnt.USCitizen == "Y")
                        oAplcnt.USCitizen = "Yes";
                    else if (oAplcnt.USCitizen == "N")
                        oAplcnt.USCitizen = "No";
                    else
                        oAplcnt.USCitizen = "";

                    oAplcnt.GreenCard = obj.NullToSpace(dr["GREENCARD"]);
                    if (oAplcnt.GreenCard == "Y")
                        oAplcnt.GreenCard = "Yes";
                    else if (oAplcnt.GreenCard == "N")
                        oAplcnt.GreenCard = "No";
                    else
                        oAplcnt.GreenCard = "";

                    oAplcnt.RealEst = obj.NullToSpace(dr["REALESTATE"]);
                    if (oAplcnt.RealEst == "Y")
                        oAplcnt.RealEst = "Yes";
                    else if (oAplcnt.RealEst == "N")
                        oAplcnt.RealEst = "No";
                    else
                        oAplcnt.RealEst = "";

                    oAplcnt.assets = obj.NullToSpace(dr["NOTAX"]);
                    if (oAplcnt.assets == "Y")
                        oAplcnt.assets = "Yes";
                    else if (oAplcnt.assets == "N")
                        oAplcnt.assets = "No";
                    else
                        oAplcnt.assets = "";

                    //SOF - chella
                    oAplcnt.sof = obj.NullToSpace(dr["SOFDET"]);
                    oAplcnt.sofOth = obj.NullToSpace(dr["SOFOTH"]);

                    //USEPRIMARY 
                    oAplcnt.UsePrimaryAddr = obj.NullToSpace(dr["SAMEADDR"]);

                    if (oAplcnt.UsePrimaryAddr == "Y")
                        oAplcnt.UsePrimaryAddr = "Yes";
                    else
                        oAplcnt.UsePrimaryAddr = "No";

                    //Current Residential Details
                    string[] sCurAddr = obj.NullToSpace(dr["CADDR1"]).Split(';');
                    if (sCurAddr != null && sCurAddr.Length == 1)
                    {
                        oAplcnt.CurDoorNo = sCurAddr[0];
                    }
                    else if (sCurAddr != null && sCurAddr.Length == 2)
                    {
                        oAplcnt.CurDoorNo = sCurAddr[0];
                        oAplcnt.CurAddr1 = sCurAddr[1];
                    }
                    else
                    {
                        oAplcnt.CurDoorNo = string.Empty;
                        oAplcnt.CurAddr1 = string.Empty;
                    }


                    oAplcnt.CurAddr2 = obj.NullToSpace(dr["CADDR2"]);
                    oAplcnt.CurAddr3 = obj.NullToSpace(dr["CADDR3"]);
                    oAplcnt.CurCounty = obj.NullToSpace(dr["CCOUNTY"]);
                    oAplcnt.CurPcode = obj.NullToSpace(dr["CPCODE"]);
                    oAplcnt.CurCntry = obj.NullToSpace(dr["CCNTRY"]);
                    oAplcnt.ResidingSince = obj.CTOD(obj.NullToSpace(dr["TDFARESINCE"]));

                    DateTime newDt;
                    if (DateTime.TryParse(oAplcnt.ResidingSince, out newDt))
                    {
                        double dDiff = (Convert.ToDateTime(newDt, provider) - Convert.ToDateTime(DateTime.Today.AddYears(-1), provider)).TotalDays;
                        if (dDiff > 0)
                        {
                            //Current Residential Details
                            string[] sPreAddr = obj.NullToSpace(dr["PADDR1"]).Split(';');
                            if (sPreAddr != null && sPreAddr.Length == 1)
                            {
                                oAplcnt.PreDoorNo = sPreAddr[0];
                            }
                            else if (sPreAddr != null && sPreAddr.Length == 2)
                            {
                                oAplcnt.PreDoorNo = sPreAddr[0];
                                oAplcnt.PreAddr1 = sPreAddr[1];
                            }
                            else
                            {
                                oAplcnt.PreDoorNo = string.Empty;
                                oAplcnt.PreAddr1 = string.Empty;
                            }

                            oAplcnt.PreAddr2 = obj.NullToSpace(dr["PADDR2"]);
                            oAplcnt.PreAddr3 = obj.NullToSpace(dr["PADDR3"]);
                            oAplcnt.PreCounty = obj.NullToSpace(dr["PCOUNTY"]);
                            oAplcnt.PrePcode = obj.NullToSpace(dr["PPCODE"]);
                            oAplcnt.PreCntry = obj.NullToSpace(dr["PCNTRY"]);
                        }
                    }

                    oAcDtl.appList.Add(oAplcnt);
                }

            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

        return oAcDtl;
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
            //sJulianDt = obj.JulianDate(System.DateTime.Now);
            sSequence = GetSequence(pType).ToString("000000");

            sJulianDt = string.Format("{0:yy}{1:D3}", DateTime.Now, DateTime.Now.DayOfYear);

            if (pType == "ACOPEN")
                sRandomNm = "A";
            else if (pType == "RMTNCE")
                sRandomNm = "R";

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
            DataTable dTable = obj.ExecuteData("SELECT [DATE],[SEQ],DATEPART(yyyy,[DATE]) AS [Year] FROM SEQPF WHERE TYPE='" + pType + "'");

            if (dTable != null && dTable.Rows.Count > 0)
            {
                int iRunningSeqNo = 0;
                string sCurDt = DateTime.Now.ToString("yyyyMMdd");
                string sTblDt = obj.NullToSpace(dTable.Rows[0]["DATE"]);

                string sCurYear = DateTime.Now.ToString("yyyy");
                string sTblYear = obj.NullToSpace(dTable.Rows[0]["Year"]);

                string sTblSeq = obj.NullToSpace(dTable.Rows[0]["SEQ"]);

                if (sCurDt == sTblDt)
                {
                    iRunningSeqNo = Convert.ToInt32(sTblSeq);
                }
                else
                {
                    obj.ExecuteCommand("UPDATE SEQPF SET [DATE]='" + sCurDt + "',[SEQ]='1' WHERE TYPE='" + pType + "'");
                    iRunningSeqNo = 1;
                }

                obj.ExecuteCommand("UPDATE SEQPF SET [SEQ]='" + Convert.ToString(iRunningSeqNo + 1) + "' WHERE TYPE='" + pType + "'");

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
            dtAcDtl = obj.ExecuteData("SELECT REFNO FROM ACDTLPF WHERE REFNO='" + sRegUniqueId + "'");
            if ((dtAcDtl != null) && (dtAcDtl.Rows.Count > 0))
            {
                if (dtAcDtl.Rows[0]["REFNO"].ToString().ToUpper() == sRegUniqueId.ToUpper())
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

    private AccountDtl LoadValues(string sAcId)
    {
        string sQuery = string.Empty;
        DataTable dt = null;
        string sFields = string.Empty;

        Session["AcDtl"] = string.Empty;
        AccountDtl AcDtl = null;

        try
        {
            sQuery = "SELECT A.*,B.* FROM ACDTLPF A LEFT JOIN APDTLPF B ON A.ACID = B.ACID WHERE A.ACID='" + sAcId + "'";
            dt = obj.ExecuteData(sQuery);

            if (dt.Rows.Count > 0)
            {
                AcDtl = new AccountDtl();

                foreach (DataRow dr in dt.Rows)
                {
                    Session["RefNo"] = obj.NullToSpace(dr["REFNO"]);
                    Session["AcOpenRefNo"] = obj.NullToSpace(dr["ACOPENREFNO"]);

                    AcDtl.AcId = sAcId;
                    AcDtl.RegUniqueId = obj.NullToSpace(dr["REFNO"]);
                    AcDtl.ReferenceNo = obj.NullToSpace(dr["ACOPENREFNO"]);
                    AcDtl.NoOfApplicants = Convert.ToInt32(obj.NullToZero(dr["NOOFJNTAPLNT"]));
                    if (obj.NullToSpace(dr["ISJNTAC"]).ToUpper() == "Yes".ToUpper())
                        AcDtl.IsJntAc = "Yes";
                    else
                        AcDtl.IsJntAc = "No";

                    AcDtl.InvAmount = obj.NullToZero(dr["INVAMOUNT"]);
                    AcDtl.InvPeriod = obj.NullToSpace(dr["INVPERIOD"]);
                    AcDtl.InvRateOfInt = obj.NullToZero(dr["INVRATINT"]);
                    AcDtl.OthBankName = obj.NullToSpace(dr["OTHBANKNAME"]);
                    AcDtl.OthBankSC = obj.NullToSpace(dr["OTHBANKSC"]);
                    AcDtl.OthBankAcNo = obj.NullToSpace(dr["OTHBANKACNO"]);
                    AcDtl.RepayIns = obj.NullToSpace(dr["REPAYINS"]);

                    AcDtl.appList = GetApplicants(dt);

                    break;

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return AcDtl;
    }

    private List<Applicant> GetApplicants(DataTable dt)
    {
        try
        {
            List<Applicant> lstApplicant = new List<Applicant>();

            foreach (DataRow dr in dt.Rows)
            {
                oAplcnt = new Applicant();
                oAplcnt.RegUniqueId = obj.NullToSpace(dr["REFNO"]);
                oAplcnt.ReferenceNo = obj.NullToSpace(dr["ACOPENREFNO"]);
                oAplcnt.Sequence = Convert.ToInt32(obj.NullToZero(dr["SEQUENCE"]));

                //Primary account or not
                oAplcnt.IsPrimary = obj.NullToSpace(dr["IsPrimary"]);

                //USEPRIMARY 
                oAplcnt.UsePrimaryAddr = obj.NullToSpace(dr["USEPRIMADDR"]);

                //Personal Details        
                oAplcnt.Title = obj.NullToSpace(dr["TITLE"]);
                oAplcnt.FirstNm = obj.NullToSpace(dr["FIRSTNM"]);
                oAplcnt.MidNm = obj.NullToSpace(dr["MIDNM"]);
                oAplcnt.SurNm = obj.NullToSpace(dr["SURNM"]);
                oAplcnt.Gender = obj.NullToSpace(dr["GENDER"]);
                oAplcnt.DOB = obj.CTOD(obj.NullToSpace(dr["DOB"]));
                oAplcnt.Citizenship = obj.NullToSpace(dr["CITIZENSHIP"]);
                oAplcnt.MaritalSts = obj.NullToSpace(dr["MARITALSTS"]);

                if (oAplcnt.MaritalSts.ToUpper() == "OTHERS")
                {
                    oAplcnt.MaritalStsOth = obj.NullToSpace(dr["MARITALSTSOTH"]);
                }
                //Contact Details        
                oAplcnt.HomeTelNo = obj.NullToSpace(dr["HOMETELNO"]);
                oAplcnt.MobileNo = obj.NullToSpace(dr["MOBILENO"]);
                oAplcnt.EmailAddr = obj.NullToSpace(dr["EMAILADDR"]);

                //Identification Details
                oAplcnt.IdenDtls = obj.NullToSpace(dr["IDENTDTLS"]);

                if (oAplcnt.IdenDtls.ToUpper() == "Passport".ToUpper())
                {
                    oAplcnt.IsUkPsPrt = obj.NullToSpace(dr["ISUKPSPRT"]);
                    oAplcnt.PsPrtIsCntry = obj.NullToSpace(dr["PSPORTISCNTRY"]);
                }
                oAplcnt.PsPrtIssDt = obj.CTOD(obj.NullToSpace(dr["PSPORTISDT"]));
                oAplcnt.PsPrtExpDt = obj.CTOD(obj.NullToSpace(dr["PSPORTEXDT"]));
                oAplcnt.IdenNo = obj.NullToSpace(dr["IDENTITYNO"]);

                //Current Address
                oAplcnt.CurDoorNo = obj.NullToSpace(dr["CURDRNO"]);
                oAplcnt.CurAddr1 = obj.NullToSpace(dr["CURADDR1"]);
                oAplcnt.CurAddr2 = obj.NullToSpace(dr["CURADDR2"]);
                oAplcnt.CurAddr3 = obj.NullToSpace(dr["CURADDR3"]);
                oAplcnt.CurCounty = obj.NullToSpace(dr["CURCOUNTY"]);
                oAplcnt.CurPcode = obj.NullToSpace(dr["CURPCODE"]);
                oAplcnt.CurCntry = obj.NullToSpace(dr["CURCNTRY"]);
                oAplcnt.ResidingSince = obj.CTOD(obj.NullToSpace(dr["RESIDINGSINCE"]));

                oAplcnt.MailingAddr = obj.NullToSpace(dr["MAILINGADDR"]);

                DateTime newDt;
                if (DateTime.TryParse(oAplcnt.ResidingSince, out newDt))
                {
                    double dDiff = (Convert.ToDateTime(newDt, provider) - Convert.ToDateTime(DateTime.Today.AddYears(-1), provider)).TotalDays;
                    if (dDiff > 0)
                    {
                        oAplcnt.PreDoorNo = obj.NullToSpace(dr["PREDRNO"]);
                        oAplcnt.PreAddr1 = obj.NullToSpace(dr["PREADDR1"]);
                        oAplcnt.PreAddr2 = obj.NullToSpace(dr["PREADDR2"]);
                        oAplcnt.PreAddr3 = obj.NullToSpace(dr["PREADDR3"]);
                        oAplcnt.PreCounty = obj.NullToSpace(dr["PRECOUNTY"]);
                        oAplcnt.PrePcode = obj.NullToSpace(dr["PREPCODE"]);
                        oAplcnt.PreCntry = obj.NullToSpace(dr["PRECNTRY"]);
                    }
                }

                lstApplicant.Add(oAplcnt);
            }

            return lstApplicant;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ClrCache()
    {
        try
        {
            if (Session["RefNo"] != null && Session["RefNo"].ToString().Length > 0)
            {
                foreach (string itm in Methods.strAryPages)
                {
                    Cache.Remove(itm + Session["RefNo"]);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}
