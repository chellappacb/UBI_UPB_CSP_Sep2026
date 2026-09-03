using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SessionTimeout : System.Web.UI.Page
{
    Methods obj = new Methods();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            try
            {
                obj.StoreEvent("88888", "", "", "", "Page is redirected to SessionTimeout", "");
            }
            catch (Exception) { }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "onLoad", "DisplaySessionTimeout()", true);
        }
    }
}