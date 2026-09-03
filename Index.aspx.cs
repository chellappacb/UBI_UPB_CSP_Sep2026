using CallML;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    Methods obj = new Methods();
    string sCusName = string.Empty;

    public string SegsJson = "[]";

    protected void Page_Init(object sender, System.EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        /* UPBv2 enhancement Aug-2021 */
        if (IsPostBack == false)
        {
            //new enhancement 2023 popup
            PnlConfirmation.Visible = true;
            PnlNewcust.Visible = true;
            lblPopupAlert.Text = string.Empty;
            LblConfirmMsg.Text = "This is a fixed term bond deposit where premature withdrawal is not permitted.";
            //end

            LoadWheelData();  //Added for Dynamic ROI integration

            //LoadBannerImg();  //commented for Dynamic ROI integration

            string sQry = "SELECT TOP 1 ANNOUNCEMENTTEXT FROM ANNOUNCEMENTPF WHERE [STATUS]='A' AND ENABLESTS = 'Y'  " +
                        " AND CAST(CONVERT(CHAR(8), GETDATE(), 112) + REPLACE(CONVERT(CHAR(8), GETDATE(), 108), ':', '') AS BIGINT) >= CAST(EFECTIVEFROMDT + EFECTIVEFROMTM AS BIGINT)   " +
                        " AND CAST(CONVERT(CHAR(8), GETDATE(), 112) + REPLACE(CONVERT(CHAR(8), GETDATE(), 108), ':', '') AS BIGINT) <= CASE ISNULL(DOESITHASEXPIRY, 'Y')   " +
                        " WHEN 'Y' THEN CAST(EXPIRYDATE + EXPIRYTIME AS BIGINT) ELSE CAST(CONVERT(CHAR(8), GETDATE(), 112) + REPLACE(CONVERT(CHAR(8), GETDATE(), 108), ':', '') AS BIGINT) END   " +
                        " ORDER BY ID DESC ";
            DataTable dt = obj.ExecuteData(sQry);
            if (dt != null && dt.Rows.Count > 0)
            {
                string sMarqueeText = obj.NullToSpace(dt.Rows[0]["ANNOUNCEMENTTEXT"]);
                if (!string.IsNullOrWhiteSpace(sMarqueeText))
                {
                    pnlMarquee.CssClass = "d-block";
                    MarqueeText.InnerText = sMarqueeText;
                }
            }
        }
        /* UPBv2 enhancement Aug-2021 */
    }

    //commented for Dynamic ROI integration
    //private void LoadBannerImg()
    //{
    //    try
    //    {
    //        string sTenureWthMonProcess = obj.RetPolValue("INDEXBANNER", "ISACTIVE");

    //        if (sTenureWthMonProcess == "Y")
    //        {
    //            pnlNewTenure.Visible = true;
    //            pnlOldTenure.Visible = false;
    //            SetBannerImage();
    //        }
    //        else
    //        {
    //            pnlNewTenure.Visible = false;
    //            pnlOldTenure.Visible = true;
    //            //20181010-chellappa
    //            string PYear = string.Empty;
    //            string RInt = string.Empty;
    //            string fSelect = " SELECT ProdName,RateInt FROM RATINTPF ORDER BY" +
    //                             " CASE WHEN ProdName Like '%Month%' THEN 1 WHEN ProdName LIKE '%Year%'  THEN 2 END," +
    //                             " CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS INT)";
    //            DataTable dt = obj.ExecuteData(fSelect);

    //            foreach (DataRow dr in dt.Rows)
    //            {
    //                PYear = obj.NullToSpace(dr["ProdName"]);
    //                RInt = obj.NullToSpace(dr["RateInt"]);
    //                switch (PYear)
    //                {
    //                    case "1Year":
    //                        OneYr.InnerHtml = RInt + "%";
    //                        break;
    //                    case "2Years":
    //                        TwoYrs.InnerHtml = RInt + "%";
    //                        break;
    //                    case "3Years":
    //                        ThreeYrs.InnerHtml = RInt + "%";
    //                        break;
    //                    case "4Years":
    //                        FourYrs.InnerHtml = RInt + "%";
    //                        break;
    //                    case "5Years":
    //                        FiveYrs.InnerHtml = RInt + "%";
    //                        break;
    //                    default:
    //                        break;
    //                }
    //            }
    //            //LoadInterestRates();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //End

    private void SetBannerImage()
    {
        Image img = (Image)UpdatePanel1.FindControl("imgBanner");

        if (img == null) return;

        string imgPath = obj.RetPolValue("INDEXBANNER", "BACKIMAGE");

        if (!string.IsNullOrEmpty(imgPath))
        {
            //img.ImageUrl = ResolveUrl("~/" + imgPath);
            img.ImageUrl = imgPath;
            img.Visible = true;
        }
    }

    protected void lbtnNew_Click(object sender, EventArgs e)
    {
        try
        {
            txtCaptcha.Focus();
            ModalPopupExtender1.Show();
            ModalPopupExtender1.BackgroundCssClass = "sctableBackground d-block";
            PnlConfirmation.CssClass = "d-block zindex_1";

            //Authenticate(); //need to comment this one

            //new enhancement 2023
            PnlConfirmation.Visible = true;
            PnlNewcust.Visible = true;
            lblPopupAlert.Text = string.Empty;
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    private void Authenticate()
    {
        try
        {
            CallML6 objCallML6 = new CallML6();

            CallMLParameters6 callmlParams = new CallMLParameters6(); ;
            mlprimarysearch callmlPrimaySearch = new mlprimarysearch();

            callcreditheaders callcreditHeaders = new callcreditheaders();
            callmlsearch6_1 searchDefinition = new callmlsearch6_1(); ;

            applicant callmlApplicant = new applicant();
            name callmlName = new name(); ;

            CallML.address callmlCurAdd = new CallML.address();

            callcreditHeaders.company = "UBoI CTest";
            callcreditHeaders.username = "api.ctest@UBoI.com";
            callcreditHeaders.password = "C3A6X4Z7@WeD";
            callcreditHeaders.application = "V705-API-TEST";

            objCallML6.callcreditheadersValue = callcreditHeaders;

            callmlCurAdd.abodeno = "";
            callmlCurAdd.buildingno = "1";
            callmlCurAdd.buildingname = "";
            callmlCurAdd.street1 = "TOP GEAR LANE";
            callmlCurAdd.street2 = "";
            callmlCurAdd.sublocality = "";
            callmlCurAdd.locality = "";
            callmlCurAdd.posttown = "TEST TOWN";
            callmlCurAdd.premiseno = "";
            callmlCurAdd.premisename = "";
            callmlCurAdd.postcode = "X9 9LF";
            callmlCurAdd.addresstype = inputaddresstype.@long;

            callmlName.title = "Mr";
            callmlName.forename = "JULIA";
            callmlName.othernames = "";
            callmlName.surname = "AUDI";

            callmlApplicant.currentaddress = callmlCurAdd;

            callmlApplicant.name = callmlName;
            callmlApplicant.dateofbirth = Convert.ToDateTime("10/10/1990");

            identity[] callmlIdentity = new identity[1];
            idparam[] callmlIdparm = new idparam[1];

            identity DvlaIden = new identity();
            idparam DvlaParam = new idparam();

            DvlaIden.idtype = "30";


            DvlaParam.idkey = "IDNUMBER"; //AUDI9101010J99CCTP
            DvlaParam.idvalue = "AUDI9101010J99CCTP";

            callmlIdparm[0] = DvlaParam;

            callmlIdentity[0] = DvlaIden;
            callmlIdentity[0].idparams = callmlIdparm;

            callmlPrimaySearch.searchpurpose = searchpurpose.ML;
            callmlPrimaySearch.applicant = callmlApplicant;


            callmlPrimaySearch.searchdirectors = true; ;
            callmlPrimaySearch.minchecks = Convert.ToInt32(3);
            callmlPrimaySearch.usebai = true;
            callmlPrimaySearch.useccj = true;
            callmlPrimaySearch.usehcj = true;
            callmlPrimaySearch.useer = true;

            callmlPrimaySearch.usesettledaccounts = true;
            callmlPrimaySearch.settledaccountmonths = 12;
            callmlPrimaySearch.settledaccountmonthsSpecified = true;

            callmlPrimaySearch.useukinvestors = true; ;

            callmlPrimaySearch.decisionwarning = "2,3,4,5,7,10,11";

            callmlPrimaySearch.searchtelephone = false;

            callmlPrimaySearch.applicant.dateofbirthSpecified = true;

            callmlPrimaySearch.applicant.addrlessthan12months = false;

            callmlParams.primarysearch = callmlPrimaySearch;
            callmlParams.yourreference = "UBI20170502204005";

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            searchDefinition.parameters = callmlParams;
            object test = objCallML6.Search06b(searchDefinition);

            searchDefinition = objCallML6.Search06b(searchDefinition);
        }
        catch (Exception ex)
        {
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
            //sJulianDt = obj.JulianDate(System.DateTime.Now);
            sSequence = GetSequence(pType).ToString("000000");

            sJulianDt = string.Format("{0:yy}{1:D3}", DateTime.Now, DateTime.Now.DayOfYear);

            if (pType == "ACOPEN")
                sRandomNm = "A";
            else if (pType == "RMTNCE")
                sRandomNm = "R";

            sRefNo = sJulianDt + sRandomNm + sSequence;


            //sCusName = "CUS" + sRefNo;
            sCusName = sRefNo;
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
                throw new Exception("Sequence generate table - SEQPF is in empty");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return sSequence;
    }

    protected void lbtnRetrive_Click(object sender, EventArgs e)
    {
        try
        {
            Session["Type"] = "RA";
            Session["RetieveDtl"] = null;
            Cache.Remove("RetieveDtl" + Convert.ToString(Session["usercode"]));
            Response.Redirect("RetrieveExistApp.aspx", false);
            //Response.End();
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }

    }

    protected void lbtnExistnCust_Click(object sender, EventArgs e)
    {
        try
        {
            Session["Type"] = null;
            Session["RetieveDtl"] = null;
            string sWebURL = obj.RetPolValue("WEB", "APPURL") + "Login.aspx";
            Response.Redirect(sWebURL, false);
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void btnNewApplicant_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValidCaptcha())
            {
                Session["Type"] = "NA";
                Session["RefNo"] = CreateRefNo("ACOPEN");
                Session["usercode"] = sCusName;
                Session["AcDtl"] = null;

                AccountDtl AcDtl = new AccountDtl();

                AcDtl.RegUniqueId = Convert.ToString(Session["RefNo"]);
                AcDtl.ReferenceNo = "";
                AcDtl.NoOfApplicants = 1;

                Session["AcDtl"] = AcDtl;
                Cache.Remove("AcDtl" + Convert.ToString(Session["RefNo"]));

                obj.StoreEvent("66301", Convert.ToString(Session["RefNo"]), "", "", "", Convert.ToString(Session["RefNo"]));

                Response.Redirect("AccountDetails.aspx", false);
                //Response.End();
            }

        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
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
            if (txtCaptcha.Text == string.Empty)
            {
                lblPopupAlert.Text = "Please enter the Captcha text!";
                txtCaptcha.Focus();
                ModalPopupExtender1.Show();
                ModalPopupExtender1.BackgroundCssClass = "sctableBackground d-block";
                PnlConfirmation.CssClass = "d-block zindex_1";
            }
            else if (!(sessKey.Equals(inputKey))) // Validate capcha
            {
                lblPopupAlert.Text = "Invalid Captcha text!";
                txtCaptcha.Text = "";
                txtCaptcha.Focus();
                ModalPopupExtender1.Show();
                ModalPopupExtender1.BackgroundCssClass = "sctableBackground d-block";
                PnlConfirmation.CssClass = "d-block zindex_1";
            }
            else
            {
                lblPopupAlert.Text = string.Empty;
                bStatus = true;
                ModalPopupExtender1.Hide();
                ModalPopupExtender1.BackgroundCssClass = "sctableBackground d-none";
                PnlConfirmation.CssClass = "d-none zindex_1";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return bStatus;
    }

    protected void lnkbtnReset_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupExtender1.Show();
            ModalPopupExtender1.BackgroundCssClass = "sctableBackground d-block";
            PnlConfirmation.CssClass = "d-block zindex_1";
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void lnkClose_Click(object sender, EventArgs e)
    {
        try
        {
            txtCaptcha.Text = "";
            ModalPopupExtender1.Hide();
            ModalPopupExtender1.BackgroundCssClass = "sctableBackground d-none";
            PnlConfirmation.CssClass = "d-none zindex_1";
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupExtender2.Show();
            ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-block";
            pnlfscspopup.CssClass = "w-90 d-block zindex_1";
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    protected void popupclose_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupExtender2.Hide();
            ModalPopupExtender2.BackgroundCssClass = "sctableBackground d-none";
            pnlfscspopup.CssClass = "w-90 d-none zindex_1";
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ":: " + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }

    //New enhancement 2023
    protected void btnshowcaptcha_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
        ModalPopupExtender1.BackgroundCssClass = "sctableBackground d-block";
        PnlConfirmation.CssClass = "d-block zindex_1";
        PnlNewcust.Visible = true;
        lblPopupAlert.Text = string.Empty;
    }
    //End

    //Added for Dynamic ROI Integration
    private void LoadWheelData()
    {
        try
        {
            List<Segment> lst = new List<Segment>();

            //string fSelect = @"SELECT STUFF(ProdName,PATINDEX('%[^0-9]%', ProdName),0,' ') AS ProdName,RateInt FROM RATINTPF ORDER BY
            //                   CASE
            //                       WHEN ProdName LIKE '%Month%' THEN 1
            //                       WHEN ProdName LIKE '%Year%' THEN 2
            //                   END, 
            //                   CAST(LEFT(ProdName,PATINDEX('%[^0-9]%', ProdName + 'X') - 1)  AS INT)";

            string fSelect = @"SELECT STUFF(ProdName, PATINDEX('%[^0-9]%', ProdName), 0, ' ') AS ProdName, RateInt
                                FROM RATINTPF
                                ORDER BY
                                CASE
                                    WHEN ProdName LIKE '%Month%'
                                        THEN CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS INT)

                                    WHEN ProdName LIKE '%Year%'
                                        THEN CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS INT) * 12
                                END";

            DataTable dt = obj.ExecuteData(fSelect);

            string[] colors = { "#e51e1e", "#0b5ea8" };

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Segment seg = new Segment();
                seg.rate = Convert.ToDecimal(dt.Rows[i]["RateInt"]).ToString("0.00") + "%";
                seg.term = dt.Rows[i]["ProdName"].ToString();
                seg.col = colors[i % 2];
                lst.Add(seg);
            }

            SegsJson = JsonConvert.SerializeObject(lst);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public class Segment
    {
        public string rate { get; set; }

        public string term { get; set; }

        public string col { get; set; }
    }
    //End
}