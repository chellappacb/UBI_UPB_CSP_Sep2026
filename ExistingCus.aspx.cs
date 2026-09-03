using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExistingCus : System.Web.UI.Page
{
    Methods obj = new Methods();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string sProdName = string.Empty;
                sProdName = obj.RetPolValue("UBI", "PRODUCTNAME");
                
                string sContent = "Union Bank of India (UK) existing customer can open " + sProdName + " using existing customer <br />security portal.";
                sContent += " Once you are logged into the existing customer Security portal, please use the option <br />&#34;" + sProdName + "&#34; Request";

                lblContent.Text = sContent;
            }
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), "");
        }
    }
}