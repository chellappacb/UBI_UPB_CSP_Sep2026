using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GenError : System.Web.UI.Page
{
    Methods obj = new Methods();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                obj.StoreEvent("88888", "", "", "", "Page is redirected to Generror", "");
            }
            catch (Exception){}

            ClearSessionAndCache();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onLoad", "DisplaySessionTimeout()", true);
        }

    }

    private void ClearSessionAndCache()
    {
        try
        {
            if (Session["usercode"] != null && Session["usercode"].ToString().Length > 0)
            {
                foreach (string itm in Methods.strAryPages)
                {
                    Cache.Remove(itm + Session["usercode"]);
                }

                Session.Abandon();
                Session.Clear();

                //Response.Redirect("Login.aspx");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

}