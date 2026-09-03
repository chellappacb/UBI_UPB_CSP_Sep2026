using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SessionList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        for (int i = 0; i <= Session.Count - 1; i++)
        {
            Response.Write("<p>" + Session.Keys[i].ToString() + " - " + Session[i].ToString() + "</p>");
        }
    }
}