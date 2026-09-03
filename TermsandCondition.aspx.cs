using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TermsandCondition : System.Web.UI.Page
{
    Methods obj = new Methods();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            BindTenureList();

            //Load deposit amount range dynamically.
            var depositInfo = obj.GetDepositAmounts();
            spMinAmt.InnerText = depositInfo.MinAmount.ToString("N0");
            spMaxAmt.InnerText = depositInfo.MaxAmount.ToString("N0");
            //End.
        }
        catch (Exception ex)
        {
            obj.StoreEvent("88888", "", "", "", "Exception on Analytics Method :: " + obj.replaceSplChr(ex.Message + ex.ToString()), "");
        }
    }

    private void BindTenureList()
    {
        string sSql = @"SELECT ProdName FROM RATINTPF ORDER BY
                        CASE
                            WHEN ProdName LIKE '%Year%' THEN
                                CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS INT) * 12
                            ELSE
                                CAST(LEFT(ProdName, PATINDEX('%[^0-9]%', ProdName + 'X') - 1) AS INT)
                        END";

        DataTable dt = obj.ExecuteData(sSql);
        StringBuilder sb = new StringBuilder();

        foreach (DataRow dr in dt.Rows)
        {
            string tenure = dr["ProdName"].ToString();
            int months = 0;

            if (tenure.Contains("Year"))
            {
                int years = Convert.ToInt32(tenure.Replace("Years", "").Replace("Year", "").Trim());
                months = years * 12;
            }
            else
            {
                months = Convert.ToInt32(tenure.Replace("Months", "").Replace("Month", "").Trim());
            }

            //sb.AppendFormat("* {0} month{1}<br/>", months, months > 1 ? "s" : "");
            sb.AppendFormat("<span class='star-align'>*</span>{0} month{1}<br/>", months, months > 1 ? "s" : "");
        }

        lblTD.InnerHtml = sb.ToString();
    }
}