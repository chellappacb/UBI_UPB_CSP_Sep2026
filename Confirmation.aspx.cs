using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Confirmation : System.Web.UI.Page
{
    Methods obj = new Methods();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string sProdName = string.Empty;
            sProdName = obj.RetPolValue("UBI", "PRODUCTNAME");
             
            string SMessage = string.Empty;
            SMessage = "We have received your " + sProdName + ". We will check the application details and";
            SMessage += "revert back to you shortly.";
            SMessage += "<br />";
            SMessage += "Should you require any further information, please feel free to contact us at 0207";
            SMessage += "332 4250";
            SMessage += "<br />";
            SMessage += "Monday to Friday between 9am to 5pm (excluding bank holidays) or email us at premierbond@unionbankofindiauk.co.uk";
            SMessage += "<br />";
            lblKYCPendingMessage.Text = SMessage;

            SMessage = string.Empty;
            SMessage = "We have received your " + sProdName + ". Email has been sent to the registered email ";
            SMessage += "address and please follow the procedures mentioned in the email for the further details.";
            SMessage += "<br />";
            SMessage += "Should you require any further information, please feel free to contact us at 0207";
            SMessage += "332 4250";
            SMessage += "<br />";
            SMessage += "Monday to Friday between 9am to 5pm (excluding bank holidays) or email us at premierbond@unionbankofindiauk.co.uk";
            SMessage += "<br />";
            lblKYCSuccessMessage.Text = SMessage;

            string[] arrString = obj.UrlGetQString(Request.QueryString["R"]);
            if (arrString != null && arrString.Count() > 0 && !string.IsNullOrEmpty(arrString[0]))
            {
                lblRefNo.Text = arrString[0];
                lblKYCPending.Text = arrString[0];
                lblKYCSuccess.Text = arrString[0];

                if (arrString[1] == "D")
                {
                    pnlSave.Visible = true;
                    pnlKYCPending.Visible = false;
                    pnlKYCSuccess.Visible = false;
                }
                if (arrString[1] == "P")
                {
                    pnlSave.Visible = false;
                    pnlKYCPending.Visible = true;
                    pnlKYCSuccess.Visible = false;
                }
                if (arrString[1] == "S")
                {
                    pnlSave.Visible = false;
                    pnlKYCPending.Visible = false;
                    pnlKYCSuccess.Visible = true;
                }
                ClearSessionAndCache();
            }
            else
            {
                ClearSessionAndCache();
                Response.Redirect("Index.aspx", false);
               
            }
        }
        catch (Exception ex)
        {
            ClearSessionAndCache();
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
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", obj.replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(Session["RefNo"]));
            throw ex;
        }
    }
}