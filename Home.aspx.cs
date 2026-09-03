using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json;

public partial class Home : System.Web.UI.Page
{
    Methods obj = new Methods();
    string sCusName = string.Empty;
    string fSelect = string.Empty;
    DataTable dt = null;

    public string SegsJson = "[]";

    protected void Page_Init(object sender, System.EventArgs e)
    {

    }

    [System.Web.Services.WebMethod]
    public static void SetCookieClose()
    {
        HttpCookie cookie = new HttpCookie("cookiesclose", "YES");
        cookie.Expires = DateTime.Now.AddMinutes(20);
        cookie.HttpOnly = true;
        cookie.Secure = true;
        cookie.SameSite = SameSiteMode.Strict;

        HttpContext.Current.Response.Cookies.Add(cookie);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["cookiesclose"] != null)
            {
                string cookiesclose = Request.Cookies["cookiesclose"].Value;
                if (!string.IsNullOrEmpty(cookiesclose) & cookiesclose == "YES")
                {
                    cookie_panel.Visible = false;
                }
                else
                {
                    cookie_panel.Visible = true;
                }
            }


            LoadWheelData();  //Added for Dynamic ROI integration

            //LoadBannerImg();   //commented for Dynamic ROI integration

            //Load deposit amount range dynamically.
            var depositInfo = obj.GetDepositAmounts();
            spMinAmt.InnerText = depositInfo.MinAmount.ToString("N0");
            spMaxAmt.InnerText = depositInfo.MaxAmount.ToString("N0");
            //End.

            if (!IsPostBack)
            {
                string sProdName = obj.RetPolValue("UBI", "PRODUCTNAME");
                lblProdName.Text = sProdName;

                string Query = "";
                string ANTRDATE, ANTRTIME, ANGUID, ANSESID, ANTRREF, ANEMILID, ANTRCTYPE, ANREFSITE, ANTRSOURCE, ANKEYWRD, ANCAMPGN, ANVISITS, ANIPDET, ANBROW, ANBROWVER, ANOPRSYS, ANRECSTS, ANMODDATE, ANMODTIME, ANREMINDER, ANREMDATE, ANREMTIME;

                HttpBrowserCapabilities BCaps;
                BCaps = Request.Browser;

                ANTRDATE = DateTime.Now.ToString("yyyyMMdd");
                ANTRTIME = DateTime.Now.ToString("HHmmss");
                ANGUID = Guid.NewGuid().ToString();
                Session["GLOBANAID"] = ANGUID;

                ANSESID = DateTime.Now.ToString("HHmmss");
                ANTRREF = "";
                ANEMILID = "";
                ANTRCTYPE = "";
                ANREFSITE = "MSN";
                ANTRSOURCE = "";
                ANKEYWRD = Request.QueryString["q"];
                ANCAMPGN = "";
                ANVISITS = "";
                ANIPDET = Request.ServerVariables["remote_addr"];
                ANBROW = BCaps.Browser;
                ANBROWVER = BCaps.Version;
                ANOPRSYS = BCaps.Platform;
                ANRECSTS = "PN";
                ANMODDATE = DateTime.Now.ToString("yyyyMMdd");
                ANMODTIME = DateTime.Now.ToString("HHmmss");
                ANREMINDER = "";
                ANREMDATE = DateTime.Now.ToString("yyyyMMdd");
                ANREMTIME = DateTime.Now.ToString("HHmmss");

                Query = "INSERT INTO ANALYTICS (ANTRDATE, ANTRTIME, ANGUID, ANSESID, ANTRREF, ANEMILID, ANTRCTYPE, ANREFSITE, ANTRSOURCE, ANKEYWRD, ANCAMPGN, ANVISITS, ANIPDET, ANBROW, ANBROWVER, ANOPRSYS, ANRECSTS, ANMODDATE, ANMODTIME, ANREMINDER, ANREMDATE, ANREMTIME,URL) values (";
                Query += "'" + ANTRDATE + " ','" + ANTRTIME + "','" + ANGUID + "','" + ANSESID + "','" + ANTRREF + "','" + ANEMILID + "','" + ANTRCTYPE + "','" + ANREFSITE + "','" + ANTRSOURCE + "','" + ANKEYWRD + "','" + ANCAMPGN + "','" + ANVISITS + "','" + ANIPDET + "','" + ANBROW + "','" + ANBROWVER + "','" + ANOPRSYS + "','" + ANRECSTS + "','" + ANMODDATE + "','" + ANMODTIME + "','" + ANREMINDER + "','" + ANREMDATE + "','" + ANREMTIME + "','" + Request.Url.AbsoluteUri.ToString() + "')";

                obj.ExecuteCommand(Query);

            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", "Exception on Analytics Method :: " + obj.replaceSplChr(ex.Message + ex.ToString()), "");
        }
    }

    #region  Commented for Dynamic ROI Integration
    //private void LoadBannerImg()
    //{
    //    try
    //    {
    //        string sTenureWthMonProcess = obj.RetPolValue("HOMEBANNER", "ISACTIVE");

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
    //            dt = obj.ExecuteData(fSelect);
    //            foreach (DataRow dr in dt.Rows)
    //            {
    //                PYear = obj.NullToSpace(dr["ProdName"]);
    //                RInt = obj.NullToSpace(dr["RateInt"]);
    //                switch (PYear)
    //                {
    //                    case "1Year":
    //                        lblfrstyr.Text = RInt + "%";
    //                        break;
    //                    case "2Years":
    //                        lblSecyr.Text = RInt + "%";
    //                        break;
    //                    case "3Years":
    //                        lblThirdyr.Text = RInt + "%";
    //                        break;
    //                    case "4Years":
    //                        lblFothyr.Text = RInt + "%";
    //                        break;
    //                    case "5Years":
    //                        lblFifthyr.Text = RInt + "%";
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

    //private void SetBannerImage()
    //{
    //    Image img = (Image)UpdatePanel1.FindControl("imgBanner");
    //    if (img == null) return;
    //    string imgPath = obj.RetPolValue("HOMEBANNER", "BACKIMAGE");
    //    if (!string.IsNullOrEmpty(imgPath))
    //    {
    //        //img.ImageUrl = ResolveUrl("~" + imgPath);
    //        img.ImageUrl = imgPath;
    //        img.Visible = true;
    //    }
    //}

    //private void LoadInterestRates()
    //{
    //    string qry = @"SELECT CASE WHEN ProdName LIKE '%Year%' THEN CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS VARCHAR(10)) + ' year' +
    //                       CASE WHEN CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS INT) > 1 THEN 's' ELSE '' END
    //                           WHEN ProdName LIKE '%Month%' THEN CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS VARCHAR(10)) + ' Months' END AS TENURE_TEXT,
    //                RateInt AS RATE FROM RATINTPF
    //                ORDER BY
    //                CASE WHEN ProdName LIKE '%Year%' THEN
    //                            CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS INT) * 12
    //                        ELSE CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS INT)
    //                END";
    //    DataTable dt = obj.ExecuteData(qry);
    //    Repeater rpt = UpdatePanel1.FindControl("rptRates") as Repeater;
    //    if (rpt != null)
    //    {
    //        rpt.DataSource = dt;
    //        rpt.DataBind();
    //    }
    //}
    #endregion

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
            throw ex;
        }
    }

    //Added for Dynamic ROI Integration
    private void LoadWheelData()
    {
        try
        {
            List<Segment> lst = new List<Segment>();

            //string fSelect = @"SELECT 
            //                   STUFF(ProdName,PATINDEX('%[^0-9]%', ProdName),0,' ') AS ProdName, 
            //                   RateInt FROM RATINTPF ORDER BY
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