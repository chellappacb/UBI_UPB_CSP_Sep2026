using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class GridviewExport
{
	public GridviewExport()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void Export(string fileName, GridView gv)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        //HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=x.xls")
        //.EnableViewState = False

        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        //Dim sdouble As String = """"
        //Dim style1 As String = "<style> .text { mso-number-format:" & sdouble & "\0022\0022\#\,\#\#0\.00" & sdouble & "; } </style>"
        string style1 = "<style>.text{mso-number-format:\\@;}</style>";

        //Create a form to contain the grid
        Table table = new Table();
        table.GridLines = gv.GridLines;
        System.Web.UI.WebControls.DataGrid g = new System.Web.UI.WebControls.DataGrid();

        //  add the header row to the table
        if ((((gv.HeaderRow) != null)))
        {
            GridviewExport.PrepareControlForExport(gv.HeaderRow);
            table.Rows.Add(gv.HeaderRow);
        }
        //  add each of the data rows to the table
        foreach (GridViewRow row in gv.Rows)
        {
            GridviewExport.PrepareControlForExport(row);
            table.Rows.Add(row);
        }

        foreach (GridViewRow i in gv.Rows)
        {
            foreach (TableCell tc in i.Cells)
            {
                tc.CssClass = "text";
            }
        }

        //  add the footer row to the table
        if ((gv.FooterRow) != null)
        {
            GridviewExport.PrepareControlForExport(gv.FooterRow);
            table.Rows.Add(gv.FooterRow);
        }

        //  render the table into the htmlwriter
        table.RenderControl(htw);
        //  render the htmlwriter into the response
        HttpContext.Current.Response.Write(style1);
        HttpContext.Current.Response.Write(sw.ToString());
        //HttpContext.Current.Response.End();
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.SuppressContent = true;
        HttpContext.Current.ApplicationInstance.CompleteRequest();
        //HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
    
    private static void PrepareControlForExport(Control control)
    {
        int i = 0;
        while ((i < control.Controls.Count))
        {
            Control current = control.Controls[i];
            if ((current is LinkButton))
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl(((LinkButton)current).Text));
            }
            else if ((current is ImageButton))
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl(((ImageButton)current).AlternateText));
            }
            else if ((current is HyperLink))
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl(((HyperLink)current).Text));
            }
            else if ((current is DropDownList))
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl(((DropDownList)current).SelectedItem.Text));
            }
            else if ((current is CheckBox))
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl(((CheckBox)current).Checked.ToString()));
                //TODO: Warning!!!, inline IF is not supported ?
            }
            if (current.HasControls())
            {
                GridviewExport.PrepareControlForExport(current);
            }
            i = (i + 1);
        }
    }
}