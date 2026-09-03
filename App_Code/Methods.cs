using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.VisualBasic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Xml;
using System.Xml.XPath;
using EmailRepository;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Methods
{
    DataSet ds = new DataSet();
    SqlDataAdapter da;
    string fselect;
    DataTable dt;
    DataRow dr;

    int i;

    public string UserId;

    string strconnection;
    public SqlConnection sqlcon;

    public SqlCommand strcmd;
    public SqlDataReader rdr;

    public string HTML_hdr = "<table width='700px' cellspacing='0' cellpadding='0' align='center'>" + // bgcolor='#FFFFCC'
            "<tr align='left' style='height: 20px;'>" +
            "<td width='2%'>&nbsp;</td>" +
            "<td colspan='3'>HEADERNAME</td>" +
            "<td width='2%'>&nbsp;</td>" +
            "</tr>" +
            "<tr align='left' style='height: 20px;'>" +
            "<td width='2%'>&nbsp;</td>" +
            "<td colspan='3'>*********************************************************************************************************************</td>" +
            "<td width='2%'>&nbsp;</td>" +
            "</tr>";

    public string HTML_ftr = "<tr align='left' style='height: 20px;'>" +
        "<td width='2%'>&nbsp;</td>" +
        "<td colspan='3'>*********************************************************************************************************************</td>" +
        "<td width='2%'>&nbsp;</td>" +
        "</tr></table>";

    public string HTML_subhdr = "<tr align='left'>" +
        "<td width='2%'>&nbsp;</td>" +
        "<td colspan='3'>SUBHDR :<br>************************************************************************************<br></td>" +
        "<td width='2%'>&nbsp;</td>" +
        "</tr>";

    public string HTML_Boldsubhdr = "<tr align='left'>" +
        "<td width='2%'>&nbsp;</td>" +
        "<td colspan='3' style='font-weight: bold;' >SUBHDR :<br>************************************************************************************<br></td>" +
        "<td width='2%'>&nbsp;</td>" +
        "</tr>";

    public string HTML_RowSpace = "<tr align='left' style='height: 20px;'><td colspan='5'>&nbsp;</td></tr>";

    public string HTML_body(string Cell_Name, string Cell_Val)
    {

        string str_HTML = string.Empty;

        str_HTML = "<tr align='left' style='height: 20px;'>" +
                    "<td width='2%'>&nbsp;</td>" +
                    "<td width='20%'>" + Cell_Name + "</td>" +
                    "<td width='2%'>:</td>" +
                    "<td width='74%'>" + Cell_Val + "</td>" +
                    "<td width='2%'>&nbsp;</td>" +
                    "</tr>";
        return str_HTML;
    }

    public string HTML_BoldBody(string Cell_Name, string Cell_Val)
    {

        string str_HTML = string.Empty;

        str_HTML = "<tr align='left' style='height: 20px;'>" +
                    "<td width='2%'>&nbsp;</td>" +
                    "<td width='20%' style='font-weight: bold;'>" + Cell_Name + "</td>" +
                    "<td width='2%' style='font-weight: bold;'>:</td>" +
                    "<td width='74%' style='font-weight: bold;'>" + Cell_Val + "</td>" +
                    "<td width='2%'>&nbsp;</td>" +
                    "</tr>";
        return str_HTML;
    }


    //public string sUBIEmailHeader = "<html>" +
    //                                " <head> <title>Welcome to UBI</title> </head>" +
    //                                " <body style='margin: 0; padding: 0; line-height: 1.5em; font-family: Verdana; font-size: 14px;color: #ffffff;'>" +
    //                                "    <table align='center' width='60%'  style='background-color: #ffffff; border: 1px solid #cccccc;'>" +
    //                                "        <tr> " +
    //                                "            <td width='50%' align='left' style='padding-left:25px;' ><img src='UBIUKLOGO' alt='UBI Logo' style='height:100px;' /></td>" +
    //                                "            <td width='50%' align='right' style='color:#00579c;  padding-right: 25px;'><h2 >UNION FIXED SAVER</h2></td>" +
    //                                "        </tr>" +
    //                                "    </table>" +
    //                                "<table align='center' width='60%' style='background-color: #ffffff; border: 1px solid #cccccc;;padding: 5px 5px 5px 5px;'>" +
    //                                "    <tr>" +
    //                                "        <td style='color: #ffffff;line-height: 2; background-color: #00579c; padding-left: 10px; border-radius: 5px;'>";

    // public string sUBIEmailFooter = "        </td>" +
    //                                 "    </tr>" +
    //                                 "</table>" +
    //                                 "</body>" +
    //                                 "</html>";



    public string sUBIEmailHeader = " <html>" +
                                    " <head> <title>Welcome to UBI</title><style type='text/css'>a{color:#fff;}</style> </head>" +
                                    " <body style='margin: 0; padding: 0; line-height: 1.5em; font-family: Verdana; font-size: 14px;color: #ffffff;'>" +
                                    " <table style='width:760;border:1px solid #E3E3E3;background-color:#fff;border-collapse:collapse;' align='center'>" +
                                    "    <tr>" +
                                    "         <td style='border-bottom:1px solid #E3E3E3;'>" +
                                    "             <table align='center' width='760'  style='background-color: #ffffff;'>" +
                                    "                 <tr> " +
                                    "                     <td width='50%' align='left' style='padding-left:25px;'>" +
                                    "                         <img src='UBIUKLOGO' alt='UBI Logo' style='height:100px;' />" +
                                    "                     </td>" +
                                    "                     <td width='50%' align='right' style='color:#00579c;  padding-right: 25px;'>" +
                                    "                         <h2 >_PRODNAME</h2>" +
                                    "                     </td>" +
                                    "                 </tr>" +
                                    "             </table>" +
                                    "         </td>" +
                                    "     </tr>" +
                                    "    <tr>" +
                                    "         <td style='color:#fff;padding:5px 5px 5px 5px;' align='left'>" +
                                    "             <table style='border-collapse:collapse;width:100%;line-height:1.5em;background-color:#00579c'>" +
                                    "                 <tr>" +
                                    "                     <td align='left' style='color:#fff;padding:5px 5px 5px 5px;'> ";
    public string sUBIEmailFooter = "                     </td>" +
                                    "                 </tr>" +
                                    "             </table>" +
                                    "         </td>" +
                                    "     </tr>" +
                                    " </table>" +
                                    " </body>" +
                                    " </html>";

    //PCode
    public static string[] strRFlatNo;

    public static string[] strNRFlatNo;
    public static string strDpStreet;

    public static string StrStreet;
    public static string[] StrRBuildgName;

    public static string[] StrNRBuildgName;

    public static string[] StrOrgName;

    public static string[] strAryPages = { "AcDtlLst", "RemDtlLst", "AcDtl", "ApDtl", "OthDtl", "GenAgrmnt", "PrvwDtl", "RemSRDtl", "RemPayDtl", "RemPrvwDtl", "Home", "SenderLst", "SenderDtl", "BenDtlLst", "BenDtl", "ViewAuthDtls" };
    public static string[] sServiceTypeCodes = { "Comment", "Match", "MissMatch", "Warning" };

    public static string[] strAryPagesA = { "ParameterMain", "ExRate", "ExRateHistory", "AcOpnDtl", "RemitDtl", "UsrMaintenance", "changepass", "EmailRep", "EvntVwr", "MdlCtrl" };

    public static string StrTown;
    public static string StrCounty;
    public static string ErrCode;

    public static string PCode;
    //for 192.com return
    public static string PrimDetail;
    public static string Jnt1Detail;
    public static string Jnt2Detail;
    public static string Jnt3Detail;

    public static string Jnt4Detail;
    //for Enc/Dec
    private byte[] key = {

    };
    private byte[] IV = {
        0x12,
        0x34,
        0x56,
        0x78,
        0x90,
        0xab,
        0xcd,
        0xef
    };

    public string[] GetQString(string qstr)
    {
        try
        {
            Int32 i = 0;
            string x = Decrypt64(qstr);
            string[] qs = x.Split('&');
            string[] qv = new string[qs.Length];
            for (i = 0; i <= qs.Length - 1; i++)
            {
                string[] y = qs[i].Split('=');
                qv[i] = y[1].Trim();
            }
            return qv;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    private string sEncryptionKey = "!#$a54?3";
    public SqlConnection Connection()
    {
        strconnection = ConfigurationManager.ConnectionStrings["ConstrNC"].ToString();
        sqlcon = new SqlConnection(strconnection);
        return sqlcon;
    }

    public void OpenConnection()
    {
        if (sqlcon.State == ConnectionState.Closed)
        {
            sqlcon.ConnectionString = strconnection;
            {
                sqlcon.Open();
            }
        }
    }

    public void CloseConnection()
    {
        if (sqlcon.State == ConnectionState.Open)
        {
            sqlcon.Close();
            sqlcon.Dispose();
        }
    }

    public DataTable ExecuteData(string fetch)
    {
        try
        {
            Connection();
            OpenConnection();
            strcmd = new SqlCommand(fetch, sqlcon);
            da = new SqlDataAdapter(strcmd);
            ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        finally
        {
            CloseConnection();
        }

        return dt;
    }

    public void ExecuteCommand(string command)
    {
        try
        {
            Connection();
            OpenConnection();
            strcmd = new SqlCommand(command, sqlcon);
            strcmd.CommandType = CommandType.Text;
            strcmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        finally
        {
            CloseConnection();
        }
    }

    public string RemoveSPChars(string input)
    {
        Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        //Return r.Replace(input, [String].Empty)

        return r.Replace(input, String.Empty).Replace(" ", "");
    }

    public string EncryPass(string sA)
    {
        string sEncryPass = "";

        for (int nIntI = 0; nIntI <= sA.Length - 1; nIntI++)
        {
            int i = Convert.ToInt32(Convert.ToChar(sA.Substring(nIntI, 1)));
            char c = Convert.ToChar(i + 5);
            sEncryPass = sEncryPass + c;
        }

        return sEncryPass;
    }

    //public string DecryPass(string sA)
    //{
    //    string sDecryPass = "";

    //    for (int nIntI = 1; nIntI <= sA.Length; nIntI++)
    //    {
    //        int i = Convert.ToInt32(System.Convert.ToChar(sA.Substring(nIntI, 1)));
    //        char c = Convert.ToChar(i - 5);
    //        sDecryPass = sDecryPass + c;
    //    }

    //    return sDecryPass;
    //}

    public string DecryPass(string sA)
    {
        string sDecryPass = "";

        for (int nIntI = 0; nIntI <= sA.Length - 1; nIntI++)
        {
            int i = Convert.ToInt32(System.Convert.ToChar(sA.Substring(nIntI, 1)));
            char c = Convert.ToChar(i - 5);
            sDecryPass = sDecryPass + c;
        }

        return sDecryPass;
    }

    public void LoadDdlRdr(DropDownList ddl, string fetch)
    {
        try
        {
            Connection();
            OpenConnection();
            strcmd = new SqlCommand(fetch, sqlcon);
            rdr = strcmd.ExecuteReader();
            ddl.Items.Clear();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    if (!string.IsNullOrEmpty(rdr[0].ToString().Trim()))
                        ddl.Items.Add(rdr[0].ToString().Trim());
                }
            }
        }
        catch (Exception ex)
        {
            //'MsgBox(ex.Message)
        }
        finally
        {
            CloseConnection();
        }

    }

    public void LoadComboDetail(ref System.Web.UI.WebControls.DropDownList ddl, ref string SqlStr)
    {
        //data bind using reader
        Connection();
        OpenConnection();
        SqlConnection strcon = new SqlConnection(strconnection);
        SqlCommand strCmd = new SqlCommand(SqlStr, strcon);
        SqlDataReader rdr = default(SqlDataReader);
        strCmd.Connection.Open();
        rdr = strCmd.ExecuteReader();
        rdr.Read();
        ddl.Items.Clear();
        if (rdr.HasRows == true)
        {
            if (rdr.FieldCount == 1)
            {
                do
                {
                    ddl.Items.Add(rdr.GetString(0));
                } while (rdr.Read());

            }
            else if (rdr.FieldCount == 2)
            {
                do
                {
                    ddl.Items.Add(new ListItem(rdr.GetString(0), rdr.GetString(1)));
                } while (rdr.Read());
            }
        }
        rdr.Close();
    }

    public string rplsSnglQots(object pObject)
    {
        string strval = Convert.ToString(pObject);
        strval = strval.Replace("'", "");
        return strval;
    }

    public string replaceSplChr(string strval)
    {
        strval = strval.Replace("'", "");
        strval = strval.Replace(",", "");
        return strval;
    }

    public string NullToSpace(object txt)
    {
        string functionReturnValue = null;

        if (txt == DBNull.Value)
        {
            functionReturnValue = "";
        }
        else if (txt == null || string.IsNullOrWhiteSpace(txt.ToString()))
        {
            functionReturnValue = "";
        }
        else
        {
            functionReturnValue = Convert.ToString(txt).Trim();
        }
        return functionReturnValue;
    }

    public string NullToZero(object txt)
    {
        string functionReturnValue = null;
        if (txt == null || string.IsNullOrEmpty(txt.ToString()) || txt == " ")
        {
            functionReturnValue = "0";
        }
        else
        {
            functionReturnValue = Convert.ToString(txt).Trim();
        }
        return functionReturnValue;
    }

    public bool CheckImage(object txt)
    {
        bool functionReturnValue = false;
        if (txt == DBNull.Value)
        {
            functionReturnValue = false;
        }
        else
        {
            functionReturnValue = true;
        }
        return functionReturnValue;
    }

    public string DTOC(string sText)
    {
        if (sText.Length > 9)
        {
            return sText.Substring(6, 4) + sText.Substring(3, 2) + sText.Substring(0, 2);
        }
        else
        {
            return sText;
        }
    }


    public string CTOD(string sText)
    {
        if (sText.Length > 7)
        {
            sText = sText.Substring(6, 2) + "/" + sText.Substring(4, 2) + "/" + sText.Substring(0, 4);
            return sText;
        }
        else
        {
            //string sToday = DateTime.Now.ToString("dd/MM/yyyy");
            //return sToday;
            return string.Empty;
        }
    }

    public string CTODate(string sText)
    {
        if (sText.Length > 7)
        {
            sText = sText.Substring(6, 2) + "-" + sText.Substring(4, 2) + "-" + sText.Substring(0, 4);
            return sText;
        }
        else
        {
            //string sToday = DateTime.Now.ToString("dd/MM/yyyy");
            //return sToday;
            return string.Empty;
        }
    }

    public string CTOTime(string sText)
    {
        if (sText.Length > 5)
        {
            sText = sText.Substring(0, 2) + ":" + sText.Substring(2, 2) + ":" + sText.Substring(4, 2);
            return sText;
        }
        else
        {
            //string sToday = DateTime.Now.ToString("dd/MM/yyyy");
            //return sToday;
            return string.Empty;
        }
    }

    public void chkSession(System.Web.UI.Page pg)
    {
        //And pg.Session["RETRIEVEID"] = ""
        if (string.IsNullOrEmpty(pg.Session["AcOpn"].ToString()))
        {
            pg.Session.Abandon();
            pg.Session.RemoveAll();
            pg.Session.Clear();
            //FormsAuthentication.SignOut();
            pg.Response.Redirect("SessionTimeout.aspx");
        }
    }

    public string RetPolValue(string pmod, string ptype)
    {

        string strval = null;
        fselect = "select POLDET from POLMTPF where pmodule='" + pmod.Trim() + "'" + " and ptype ='" + ptype.Trim() + "'";
        dt = ExecuteData(fselect);
        if (dt.Rows.Count > 0)
        {
            dr = dt.Rows[0];
            strval = NullToSpace(dr["POLDET"]);
        }
        else
        {
            strval = "";
        }
        return strval;

    }

    public void UpdatePolValue(string pmod, string ptype, string pdet)
    {
        fselect = "Update POLMTPF set POLDET='" + pdet + "' from POLMTPF where pmodule='" + pmod.Trim() + "'" + " and ptype ='" + ptype.Trim() + "'";
        ExecuteCommand(fselect);
    }

    public string CheckListItemString(string Cbl, string sCheckStr)
    {
        fselect = "select * from USRMODAS where USRCODE='" + Cbl + "' and MODCODE='" + sCheckStr + "'";
        dt = ExecuteData(fselect);
        if (dt.Rows.Count > 0)
        {
            dr = dt.Rows[0];
            return dr["MODCODE"].ToString();
        }
        else
        {
            return "";
        }
    }

    public void GetEncryptKey()
    {
        //----Get URL Encryption Key------
        if (System.Web.HttpContext.Current.Session["Urlencrypt"] == null)
        {
            System.Web.HttpContext.Current.Session["Urlencrypt"] = System.Guid.NewGuid();
        }
        //----------------------------
    }


    public string UrlEncrypt64(string stringToEncrypt)
    {
        string sReturnString = string.Empty;
        try
        {
            GetEncryptKey();
            sEncryptionKey = System.Web.HttpContext.Current.Session["Urlencrypt"].ToString();
            key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //Return Replace(Convert.ToBase64String(ms.ToArray()), "/", "Maj@1")
            sReturnString = Convert.ToBase64String(ms.ToArray()).Replace("/", sEncryptionKey.Substring(0, 10).Trim());

        }
        catch (Exception e)
        {
            return e.Message;
        }
        return sReturnString;
    }

    public string[] UrlGetQString(string qstr)
    {
        try
        {
            Int32 i = 0;
            string x = UrlDecrypt64(qstr);
            string[] qs = x.Split('&');
            string[] qv = new string[qs.Length];
            for (i = 0; i <= qs.Length - 1; i++)
            {
                string[] y = qs[i].Split('=');
                qv[i] = y[1].Trim();
            }
            return qv;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string UrlDecrypt64(string stringToDecrypt)
    {
        GetEncryptKey();
        sEncryptionKey = System.Web.HttpContext.Current.Session["Urlencrypt"].ToString();
        //stringToDecrypt = Replace(stringToDecrypt, "Maj@1", "/")
        stringToDecrypt = stringToDecrypt.Replace(sEncryptionKey.Substring(0, 10).Trim(), "/");

        stringToDecrypt = stringToDecrypt.Replace(sEncryptionKey.Substring(0, 10).Trim(), "/");
        byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
        stringToDecrypt = stringToDecrypt.Replace(" ", "+");
        try
        {
            key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));// Strings.Left(sEncryptionKey, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(stringToDecrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception e)
        {
            throw e;
            //Return e.Message
        }
    }
    //}

    //-----------------------------------------------------------------------------------------------
    //64 bit Encryption

    public string Decrypt64(string stringToDecrypt)
    {
        stringToDecrypt = stringToDecrypt.Replace("Maj@1", "/");
        byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
        stringToDecrypt = stringToDecrypt.Replace(" ", "+");
        try
        {
            key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(stringToDecrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception e)
        {
            throw e;
            //Return e.Message
        }
    }


    public string Encrypt64(string stringToEncrypt)
    {
        string sReturnString = string.Empty;
        try
        {
            key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            sReturnString = Convert.ToBase64String(ms.ToArray()).Replace("/", "Maj@1");
        }
        catch (Exception e)
        {
            throw e;
        }

        return sReturnString;
    }

    public void SendEmail(string MsgFrom, string MsgTo, string MsgSubject, string MsgBody)
    {
        try
        {
            string MailFrom = null;
            string MailFrmPW = null;
            string SMTP = null;
            MailFrom = RetPolValue("MAIL", "CONTACT");
            MailFrmPW = RetPolValue("MAIL", "CONTACTPASS");
            SMTP = RetPolValue("NET", "BACKSMTP");

            MailMessage msg = new MailMessage(MsgFrom, MsgTo, MsgSubject, MsgBody);
            msg.IsBodyHtml = true;
            SmtpClient mailClient = new SmtpClient(SMTP, 25);
            NetworkCredential NetCrd = new NetworkCredential(MailFrom, MailFrmPW);
            mailClient.UseDefaultCredentials = false;
            mailClient.Credentials = NetCrd;
            mailClient.Send(msg);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }



    public void SendEmailFrmPNB(string MsgTo, string MsgSubject, string Content)
    {
        string MailFrom = null;
        string MailFrmPW = null;
        string SMTP = null;
        string Logo = null;
        string MsgBody = null;
        string MsgFrom = null;

        try
        {
            MsgFrom = "Punjab National Bank <" + RetPolValue("MAIL", "CONTACT") + ">";

            MailFrom = RetPolValue("MAIL", "CONTACT");
            MailFrmPW = RetPolValue("MAIL", "CONTACTPASS");
            SMTP = RetPolValue("NET", "BACKSMTP");

            Logo = RetPolValue("MAIL", "LOGO");

            MsgBody = "<html>" + "<head><style type='text/css'>.bodytext{font-family: Trebuchet MS, Segoe UI, Verdana,  Arial, sans-serif,  Helvetica;font-size: 12px;color: #000000; padding: 1px 1px 1px 2px;} .bodytextbold{font-family: Trebuchet MS, Segoe UI, Verdana,  Arial, sans-serif,  Helvetica;font-size: 12px;font-weight: bold;color: #000000;\tpadding: 1px 1px 1px 2px;}</style> </head>" + "<body>" + "<table width='760' cellspacing='5' cellpadding='5' border='2' bordercolor='#9F2925' bgcolor='#F9F8F0' align='center'>" + "<tr>" + "<td bgcolor='#9F2925' height='80' align='left'>" + "<img src=" + Logo + " alt='Punjab National Bank'>" + "</td>" + "</tr>" + "<tr>" + "<td align='left' style='font-family:Arial; font-size:12px; color:#000000;'> " + Content + "</td>" + "</tr>" + "</table>" + "</body>" + "</html>";

            MailMessage msg = new MailMessage(MsgFrom, MsgTo, MsgSubject, MsgBody);
            msg.IsBodyHtml = true;
            SmtpClient mailClient = new SmtpClient(SMTP, 25);
            NetworkCredential NetCrd = new NetworkCredential(MailFrom, MailFrmPW);

            mailClient.UseDefaultCredentials = false;
            mailClient.Credentials = NetCrd;

            mailClient.Send(msg);

            byte[] @by = { 0 };
            // StoreEmail(MsgFrom, MsgTo, MsgSubject, MsgBody, @by, "1");

        }
        catch (Exception ex)
        {
            byte[] @by = { 0 };
            //StoreEmail(MsgFrom, MsgTo, MsgSubject, MsgBody, @by, "0");
            throw ex;
        }

    }

    //--------------
    public string GetAcType()
    {
        string functionReturnValue = null;
        fselect = "select ACTYP from ACTYPPF where ACDESC='Individual Deposit Accounts'";
        dt = ExecuteData(fselect);
        if (dt.Rows.Count > 0)
        {
            functionReturnValue = dt.Rows[0][0].ToString();
        }
        return functionReturnValue;
    }

    public string GetContent(string RowName)
    {

        fselect = "select rowcontent from emailmessage where rowname ='" + RowName + "'";
        dt = ExecuteData(fselect);
        if (dt.Rows.Count > 0)
        {
            return NullToSpace(dt.Rows[0][0]);
        }
        else
        {
            return "";
        }
    }

    public void GSendEmail(string MsgFrom, string MsgTo, string MsgSubject, string MsgBody)
    {

        try
        {
            MailMessage msg = new MailMessage(MsgFrom, MsgTo, MsgSubject, MsgBody);
            SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587);

            NetworkCredential NetCrd = new NetworkCredential("gjsoft@gmail.com", "");
            mailClient.UseDefaultCredentials = false;
            mailClient.Credentials = NetCrd;
            mailClient.EnableSsl = true;
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            mailClient.Send(msg);

        }
        catch (Exception ex)
        {
            //Interaction.MsgBox(ex.Message);
        }
    }

    public enum Customer
    {
        NewCust,
        ExistingCust
    }

    public bool ChkEmailExists(string Email)
    {

        fselect = "select CUSPWD,CUFNAME from ACOPCUST where CUSEMAIL='" + Email.Trim() + "'";
        dt = ExecuteData(fselect);

        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return true;
            //Modified by saro on 06/09/2010 
        }

    }

    public string JulianDate(System.DateTime vDate)
    {
        //string jDate = string.Empty;
        //JulianCalendar myCal = new JulianCalendar();
        //jDate = vDate.Year.ToString().Substring(2, 2) + Convert.ToString(myCal.GetDayOfYear(vDate));
        //return jDate;
        return string.Format("{0:D3}", vDate.DayOfYear);
    }

    public void StoreEvent(string sCode, string Xfind, string Yfind, string Zfind, string sMessage, string sUser)
    {
        string Description = null;
        fselect = "select description from auditmtpf where code='" + sCode + "'";
        dt = ExecuteData(fselect);

        if (dt.Rows.Count > 0)
        {
            if ((sCode != "99999") && (sCode != "88888"))
            {

                dr = dt.Rows[0];
                Description = dr[0].ToString();
                if (Xfind.Trim().Length > 0)
                    Description = Description.Replace("XXX", Xfind);
                if (Yfind.Trim().Length > 0)
                    Description = Description.Replace("YYY", Yfind);
                if (Zfind.Trim().Length > 0)
                    Description = Description.Replace("ZZZ", Zfind);
            }
            else
            {
                Description = sMessage;
                if (Xfind.Trim().Length > 0)
                    Description = Description.Replace("XXX", Xfind);
                if (Yfind.Trim().Length > 0)
                    Description = Description.Replace("YYY", Yfind);
            }

            try
            {
                string sStrIns = "Insert InTo AUDITREPF(Date,Time,Category,Description,[User])Values ('" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("HHmmss") + "','" + sCode.Trim() + "','" + Description + "','" + sUser + "')";
                ExecuteCommand(sStrIns);

            }
            catch (Exception ex)
            {
                WriteLog("Exception on (StoreEvent) routine : " + ex.Message);
            }

        }
    }

    public void WriteLog(String msg_cnt)
    {
        try
        {
            String logPath;
            string folderName = "log\\";
            logPath = HttpContext.Current.Server.MapPath("~/" + folderName);
            dir_exist(logPath);
            logPath = logPath + DateTime.Now.ToString("yyyyMMdd") + ".txt";

            // Append line to the file.
            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine(DateTime.Now.ToString("dd/MM/yyyy") + "    " + DateTime.Now.ToString("HH:mm:ss") + "    " + msg_cnt.Trim());
                writer.Close();
                writer.Dispose();
            }
        }
        catch (Exception ex)
        {
            //throw ex;
        }

    }

    private void dir_exist(String s_dir)
    {
        try
        {
            if (File.Exists(s_dir) == false)
            {
                System.IO.Directory.CreateDirectory(s_dir);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public double FormatBaseCurVal(string recCcy, string payCcy, string amtval)
    {
        double FinTrnAmt = 0;
        double ExRate = 0;
        int DPlace = 0;
        string SEI = "";
        double indiexRate = 0;
        double Valamount = 0;
        //--------------Arrive Maximum amount----------------
        //Get currency Rate 

        if (recCcy == payCcy)
        {
            ExRate = 1;
            return Convert.ToDouble(amtval);
        }
        else
        {
            if (recCcy == "GBP")
            {
                fselect = "select * from currency where c8ccy='" + payCcy + "'";
                dt = ExecuteData(fselect);
                if (dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];
                    ExRate = Convert.ToDouble(dr["C8SPT"]);
                    DPlace = Convert.ToInt32(dr["C8CED"]);
                    SEI = dr["C8SEI"].ToString().Trim();

                    // C8CED     C8SPT	    C8SEI
                    // 2	        1.00000	    N

                    //Check Multiply/Divide 
                    if (SEI == "Y")
                    {
                        FinTrnAmt = Math.Round(Convert.ToDouble(amtval) / ExRate, DPlace);
                    }
                    else if (SEI == "N")
                    {
                        FinTrnAmt = Math.Round(Convert.ToDouble(amtval) * ExRate, DPlace);
                    }
                    Valamount = FinTrnAmt;
                    //ExRate &
                }

                //If from currency is not 'GBP' then...........
            }
            else
            {

                //-------------------First Step-------------------
                //----First step take the "from" currency and move the value to USD
                fselect = "select * from currency where c8ccy='" + recCcy + "'";
                dt = ExecuteData(fselect);
                if (dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];
                    ExRate = Convert.ToDouble(dr["C8SPT"]);
                    indiexRate = ExRate;
                    DPlace = Convert.ToInt32(dr["C8CED"]);
                    SEI = dr["C8SEI"].ToString().Trim();

                    //Check Multiply/Divide 
                    if (SEI == "Y")
                    {
                        FinTrnAmt = Math.Round(Convert.ToDouble(amtval) * ExRate, DPlace);
                    }
                    else if (SEI == "N")
                    {
                        FinTrnAmt = Math.Round(Convert.ToDouble(amtval) / ExRate, DPlace);
                    }
                    Valamount = FinTrnAmt;
                }
                // End of dt.rows.count loop..........


                //-------------------Second Step-------------------
                //--------Second step take the "to" currency amount and calculate against GBP to arrive 
                //the final amount for the "TO" currency
                if (payCcy != "GBP")
                {
                    fselect = "select * from currency where c8ccy='" + payCcy + "'";
                    dt = ExecuteData(fselect);
                    if (dt.Rows.Count > 0)
                    {

                        dr = dt.Rows[0];
                        ExRate = Convert.ToDouble(dr["C8SPT"]);
                        DPlace = Convert.ToInt32(dr["C8CED"]);
                        SEI = dr["C8SEI"].ToString().Trim();
                        //Check Multiply/Divide 
                        if (SEI == "Y")
                        {
                            FinTrnAmt = Math.Round((Convert.ToDouble(FinTrnAmt) / ExRate), DPlace);
                            indiexRate = Math.Round((Convert.ToDouble(indiexRate) / ExRate), DPlace);
                        }
                        else if (SEI == "N")
                        {
                            FinTrnAmt = Math.Round((Convert.ToDouble(FinTrnAmt) * ExRate), DPlace);
                            indiexRate = Math.Round((Convert.ToDouble(indiexRate) * ExRate), DPlace);
                        }
                        Valamount = FinTrnAmt;
                    }
                    // End of dt.rows.count loop..........
                }

                Valamount = FinTrnAmt;
                //& " Ex.Rate:" & indiexRate

            }
            //-----From currency 'GBP' Validation Loop
        }
        //--------Same currency validation Loop
        return Valamount;
    }

    public enum ModCode
    {
        Maintenance = 100,
        ParameterMaintenance = 101,
        ExchangeRateMaintenance = 102,
        ExchangeRateHistory = 103,
        AccountOpen = 200,
        AccOpenDetails = 201,
        Remittance = 300,
        RemittanceDetails = 301,
        ApplicationControl = 400,
        UserMaintenance = 401,
        PasswordChange = 402,
        ModuleControl = 403,
        EventViewer = 404,
        EmailRepository = 405,
    }

    public string Gend_Type(string PGender)
    {
        switch (PGender.ToString().ToUpper())
        {
            case "MALE":
                PGender = "M";
                break;
            case "FEMALE":
                PGender = "F";
                break;
            case "M":
                PGender = "MALE";
                break;
            case "F":
                PGender = "FEMALE";
                break;
        }

        return PGender;

    }

    public static string FirstCharToUpper(string input)
    {
        return input.First().ToString().ToUpper() + input.Substring(1);
    }

    public void LoadDDL(DropDownList ddl, string query)
    {
        try
        {

            ddl.Items.Clear();
            DataTable dtDDL = new DataTable();
            dtDDL = ExecuteData(query);
            ddl.DataSource = dtDDL;
            ddl.DataTextField = dtDDL.Columns[0].ToString();
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("-- Select --", "-1"));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string GetRateofInterest(Double amt, string prodname)
    {
        string sRateOfInt = string.Empty;

        try
        {
            fselect = "select RateInt from RATINTPF where prodname='" + prodname + "'";
            DataTable dt1 = ExecuteData(fselect);

            if (dt.Rows.Count > 0)
                sRateOfInt = NullToZero(dt.Rows[0]["RateInt"]);
            else
                sRateOfInt = "0";

        }
        catch (Exception ex)
        {
            throw ex;
        }

        return sRateOfInt;
    }

    public string SearchRefNo()
    {
        int callmlSeqno = 0;
        string callmlRefno = null;

        callmlSeqno = Convert.ToInt32(NullToZero(RetPolValue("CALLML", "SEQNO")));

        if (callmlSeqno > 9999)
            callmlSeqno = 1;
        else
            callmlSeqno += 1;

        callmlRefno = DateTime.Now.ToString("yyyyMMdd") + string.Format("{0:000000}", callmlSeqno); // string.Format("000000", callmlSeqno);

        UpdatePolValue("CALLML", "SEQNO", callmlSeqno.ToString());
        return callmlRefno;
    }


    public string RetCallMLValue(string pid)
    {
        string strval = null;
        string squery = null;
        DataTable dtResult = new DataTable();

        squery = "SELECT PVALUE FROM CALLMLLOOKUP where PID='" + pid + "'";
        dtResult = ExecuteData(squery);

        if (dtResult.Rows.Count > 0)
        {
            strval = NullToSpace(dtResult.Rows[0]["PVALUE"]);
        }
        else
        {
            strval = "";
        }

        return strval;

    }

    public void UpdateBusDate_RSetSeqNo()
    {
        try
        {
            System.DateTime BusDate = default(System.DateTime);
            string sBusQry = null;
            DataTable sBusdt = new DataTable();

            sBusQry = "select POLDET from POLMTPF where PMODULE='BUS' AND PTYPE='DATE'";
            sBusdt = ExecuteData(sBusQry);

            if (sBusdt.Rows.Count > 0)
            {
                BusDate = Convert.ToDateTime(CTOD(sBusdt.Rows[0][0].ToString()));

                if (BusDate != DateTime.Now.Date)
                {
                    //CallMl Serach Reference Sequence Number
                    fselect = "UPDATE POLMTPF SET POLDET='0' WHERE PMODULE='CALLML' AND PTYPE='SEQNO'";
                    ExecuteCommand(fselect);

                    //Update BusDate---------------------------------------------
                    fselect = "update POLMTPF set POLDET='" + DateTime.Now.ToString("yyyyMMdd") + "' where PMODULE='BUS' AND PTYPE='DATE'";
                    ExecuteCommand(fselect);

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string CreateAcOpnRefNo(Customer type)
    {

        try
        {
            string Prefix = "";
            double SeqNo = 0;
            string RefNo = "";

            switch (type)
            {

                case Customer.ExistingCust:
                    Prefix = RetPolValue("ACOPN", "ECPREFIX");
                    SeqNo = Convert.ToInt32(NullToZero(RetPolValue("ACOPN", "ECSEQNO")));
                    break;

                case Customer.NewCust:
                    Prefix = RetPolValue("ACOPN", "NCPREFIX");
                    SeqNo = Convert.ToInt32(NullToZero(RetPolValue("ACOPN", "NCSEQNO")));
                    break;
            }

            if (SeqNo >= 999999)
            {
                SeqNo = 0;
                Prefix = Prefix.Substring(0, 2) + Convert.ToInt32(Prefix.Substring(2, 1)) + 1;
            }
            else
            {
                SeqNo += 1;
            }


            RefNo = Prefix + JulianDate(DateTime.Now) + string.Format("{0:000000}", SeqNo);

            UpdateAcOpnRefNo(RefNo);

            return RefNo;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void UpdateAcOpnRefNo(string TRREF)
    {
        try
        {
            if (TRREF.Length > 3)
            {
                string Prefix = TRREF.Substring(0, 3);
                string SeqNo = TRREF.Substring(TRREF.Length - 6);


                switch (TRREF.Substring(0, 2))
                {
                    case "EC":
                        {
                            UpdatePolValue("ACOPN", "ECPREFIX", Prefix);
                            UpdatePolValue("ACOPN", "ECSEQNO", SeqNo);
                            break;
                        }
                    case "NC":
                        {
                            UpdatePolValue("ACOPN", "NCPREFIX", Prefix);
                            UpdatePolValue("ACOPN", "NCSEQNO", SeqNo);
                            break;
                        }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public bool SendEmailMessage(string sMsgTo, string sMsgSubject, string sMsgBody, string sAttachmentPath, string sPDFBody, string sPDFFileName, string sPDFPwd, bool bIsExptnMsg, string sEmailOptions, string sRefNo, AlertEmail.MailType sMailType)
    {

        bool bStatus = false;

        try
        {
            string EmailSub = "UBI(UK) - UPB";
            string sPolModType = string.Empty;
            string sHdrBgColor = string.Empty;
            string sHdrFntColor = string.Empty;

            //2024
            string sSMTPHost = string.Empty;
            string sUserID = string.Empty;
            string sPWD = string.Empty;
            sSMTPHost = RetPolValue("ERMAIL", "SMTP");        //|jgrfnq3zsntsgfsptknsinfzp3ht3zp
            sUserID = RetPolValue("ERMAIL", "MAILFROM");      //donotreply@unionbankofindiauk.co.uk
            sPWD = RetPolValue("ERMAIL", "PASSWORD");         //X/=yj<9)zy;
            //End

            sPolModType = ConfigurationManager.AppSettings[sEmailOptions];

            if ((bIsExptnMsg == true))
            {
                sHdrBgColor = ConfigurationManager.AppSettings["ExptnHdrBgClr"];
                sHdrFntColor = ConfigurationManager.AppSettings["ExptnHdrFntClr"];
            }
            else
            {
                sHdrBgColor = ConfigurationManager.AppSettings["HdrBgClr"];
                sHdrFntColor = ConfigurationManager.AppSettings["HdrFntClr"];
            }

            if (string.IsNullOrEmpty(sMsgSubject))
            {
                sMsgSubject = EmailSub;
            }

            AlertEmail oMail = new AlertEmail();

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;


            if (oMail.SendEmail(ConfigurationManager.AppSettings["ConstrNC"], ConfigurationManager.AppSettings["ApplnNm"], ConfigurationManager.AppSettings["EmailTO"],
            sSMTPHost, sUserID, sPWD,
            sMsgSubject, sMsgBody, sPDFFileName, sPDFPwd, sPDFBody, sMsgTo, "", sHdrBgColor, sHdrFntColor, sPolModType, sMailType))
            {
                bStatus = true;
            }

        }
        catch (Exception ex)
        {
        }
        return bStatus;

    }


    public void SendEmailFromUBI(string sMsgTo, string sMsgSubject, string sMsgContent)
    {
        byte[] byteValue = { };

        string sSMTP, sApplNm, sEmailMsgFrom, sUserName, sPassword, sDomain, MsgBody, sLogo;
        bool bSendLocal, bDefaultCrdntl, bSSLEnable;
        int iPortNo;
        sSMTP = sApplNm = sEmailMsgFrom = sUserName = sPassword = sDomain = MsgBody = sLogo = string.Empty;
        bSendLocal = bDefaultCrdntl = bSSLEnable = false;
        iPortNo = 25;

        try
        {
            bSendLocal = RetPolValue("SNDMAIL", "THROULOCAL") == "Y" ? true : false;
            if (bSendLocal == false)
            {
                sSMTP = RetPolValue("UBIMAIL", "SMTP");
                sApplNm = RetPolValue("UBIMAIL", "APPNAME");
                sEmailMsgFrom = RetPolValue("UBIMAIL", "MAILFROM");
                sUserName = RetPolValue("UBIMAIL", "USERNAME");
                sPassword = RetPolValue("UBIMAIL", "PASSWORD");
                sDomain = RetPolValue("UBIMAIL", "DOMAIN");
                bDefaultCrdntl = RetPolValue("UBIMAIL", "USEDFLTCRE") == "Y" ? true : false;
                bSSLEnable = RetPolValue("UBIMAIL", "SSL") == "Y" ? true : false;
                iPortNo = Convert.ToInt32(NullToZero(RetPolValue("UBIMAIL", "PORT"))) == 0 ? 25 : Convert.ToInt32(NullToZero(RetPolValue("UBIMAIL", "PORT")));
            }
            else
            {
                sSMTP = "mail.macroinfotech.co.uk";
                sApplNm = "UBIUK - ODT";
                sEmailMsgFrom = "fpay@macroinfotech.co.uk";
                sUserName = "fpay@macroinfotech.co.uk";
                sPassword = "Apex1234";
                sDomain = "";
                bDefaultCrdntl = false;
                bSSLEnable = false;
                iPortNo = 25;
            }

            sLogo = RetPolValue("MAIL", "LOGO");

            sUBIEmailHeader = sUBIEmailHeader.Replace("UBIUKLOGO", "https://www.unionbankofindiauk.co.uk/Portals/0/logo.png");

            string sProdName = String.Empty;
            sProdName = RetPolValue("UBI", "PRODUCTNAME");
            sUBIEmailHeader = sUBIEmailHeader.Replace("_PRODNAME", sProdName);

            MsgBody = sUBIEmailHeader + sMsgContent + sUBIEmailFooter;

            MailMessage msg = new MailMessage(sApplNm + "<" + sEmailMsgFrom + ">", sMsgTo, sMsgSubject, MsgBody);
            msg.IsBodyHtml = true;

            NetworkCredential NetCrd = new NetworkCredential();
            NetCrd.UserName = sUserName;
            NetCrd.Password = sPassword;
            NetCrd.Domain = sDomain;

            SmtpClient mailClient = new SmtpClient();
            mailClient.Host = sSMTP;
            mailClient.Port = iPortNo;
            mailClient.UseDefaultCredentials = false;
            mailClient.EnableSsl = bSSLEnable;
            mailClient.Credentials = NetCrd;

            mailClient.Send(msg);

            StoreEmail(sEmailMsgFrom, sMsgTo, sMsgSubject, MsgBody, byteValue, "1");
        }
        catch (Exception ex)
        {
            //  throw ex;
            StoreEmail(sEmailMsgFrom, sMsgTo, sMsgSubject, MsgBody, byteValue, "0");
        }

    }





    public bool StoreEmail(string MsgFrom, string MsgTo, string MsgSubject, string MsgBody, byte[] MsgAttach, string MsgStatus)
    {
        bool bStatus = false;
        try
        {
            MsgBody = replaceSplChr(MsgBody);
            string strQuery = "INSERT INTO [EMCONTENTPF]([EMDATE],[EMTIME],[EMFROM],[EMTO],[EMSUB],[EMATTA],[EMBODY],[EMSTATUS]) VALUES(@EMDATE, @EMTIME, @EMFROM, @EMTO, @EMSUB, @EMATTA, @EMBODY, @EMSTATUS)";

            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.Add("@EMDATE", SqlDbType.NVarChar).Value = DateTime.Now.ToString("yyyyMMdd");
            cmd.Parameters.Add("@EMTIME", SqlDbType.NVarChar).Value = DateTime.Now.ToString("HH:mm:ss");
            cmd.Parameters.Add("@EMFROM", SqlDbType.NVarChar).Value = MsgFrom;
            cmd.Parameters.Add("@EMTO", SqlDbType.NVarChar).Value = MsgTo;
            cmd.Parameters.Add("@EMSUB", SqlDbType.NVarChar).Value = MsgSubject;
            cmd.Parameters.Add("@EMATTA", SqlDbType.Image).Value = MsgAttach;
            cmd.Parameters.Add("@EMBODY", SqlDbType.NText).Value = MsgBody;
            cmd.Parameters.Add("@EMSTATUS", SqlDbType.NVarChar).Value = MsgStatus;

            SqlConnection oSqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConstrNC"].ToString());

            cmd.CommandType = CommandType.Text;
            cmd.Connection = oSqlCon;

            try
            {
                oSqlCon.Open();
                cmd.ExecuteNonQuery();
                bStatus = true;
            }
            catch { }
            finally
            {
                oSqlCon.Close();
                oSqlCon.Dispose();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bStatus;
    }
    public AccountDtl GetAcDtl()
    {
        AccountDtl AcDtl = null;

        try
        {
            if (System.Web.HttpContext.Current.Session["AcDtl"] != null)
            {
                AcDtl = (AccountDtl)System.Web.HttpContext.Current.Session["AcDtl"];
            }
        }
        catch (Exception ex)
        {
            StoreEvent("88888", "", "", "", replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(System.Web.HttpContext.Current.Session["RefNo"]));
            throw ex;
        }
        return AcDtl;
    }

    //public bool SaveApplicant(string sAplnSts)
    //{
    //    AccountDtl AcDtl = GetAcDtl();

    //    bool bStatus = false;
    //    string sQuery = string.Empty;

    //    DataTable dtApDtl = null;
    //    List<Applicant> lstAplcnt = null;
    //    StringBuilder aplQry = new StringBuilder("");

    //    try
    //    {
    //        lstAplcnt = AcDtl.appList;

    //        if (lstAplcnt != null && lstAplcnt.Count > 0)
    //        {
    //            string sQryField, sQryValue, sCommonValues;
    //            string sRefNo, sIsJntAc, sInvAmount, sInvPeriod, sInvRateOfInt, sOthBankName, sOthBankSC, sOthBankAcNo, sRepayIns;
    //            double dInvAmt;

    //            sQryField = sQryValue = sCommonValues = string.Empty;
    //            sRefNo = sIsJntAc = sInvAmount = sInvPeriod = sInvRateOfInt = sOthBankName = sOthBankSC = sOthBankAcNo = sRepayIns = string.Empty;

    //            sRefNo = AcDtl.ReferenceNo;
    //            sIsJntAc = AcDtl.IsJntAc;

    //            sInvAmount = AcDtl.InvAmount;
    //            sInvPeriod = AcDtl.InvPeriod;
    //            sInvRateOfInt = AcDtl.InvRateOfInt;
    //            sOthBankName = AcDtl.OthBankName;
    //            sOthBankSC = AcDtl.OthBankSC;
    //            sOthBankAcNo = AcDtl.OthBankAcNo;
    //            sRepayIns = AcDtl.RepayIns;

    //            aplQry.Append("INSERT INTO TDAPPIND (TEMPREF,TDTRNID, TDREFNO,");
    //            aplQry.Append("TDACTYPE, TDINVAMT, TDINVPERIOD, TDINVRATINS, TDAIOTHRBNKNA, TDOTHSRCODE, TDOTHACNO, TDREPAYINS,TDISJOINT,MRKTNGCONT,");
    //            aplQry.Append("TDISPRIMARY, TDSEQUENCE,TDFATITLE, TDFANAME, TDMINAME, TDSURNAME, TDGNDR, TDFADOB,");
    //            aplQry.Append("TDFACITIZEN,TDFAMARITAL, TDFARESINO, TDFAMOBNO, TDFAEMAIL, TDPOB, TDMOTMDNME,");
    //            aplQry.Append("TDFACURADD1, TDFACURADD2, TDFACURADD3, TDFAPCODE, TDFACRY,TDCURCOUNTRY,");
    //            aplQry.Append("TDIDDTLS,PPTYPE,TDFAPANAME,DLTYPE,ISUKPERSON,PRIJRSDCTN,PRITIN,ADJRSDCTN1,ADTIN1,ADJRSDCTN2,ADTIN2,RESNNAPTIN,TDEMPDET,TDEDOTH,TDFAPASSNO, TDDOI, TDDOE, TDPOI, TDFADVLANO, TDDVLDOE, TDDVLPCODE,TDNATINSNO,");
    //            aplQry.Append("TDTOTAPP, TDSTATUS, TDDATE, TDTIME,Renewal,TDJOINTADD1,TDAUTHENID,UniqueSyndGLobalID,");
    //            aplQry.Append("TDAAC,SREFNO,TDFARESINCE,PRVPRESENT,TDFAPRADD1,TDFAPRADD2,TDFAPRADD3,TDFAPRPCODE,TDFAPRCRY,TDPRECOUNTRY)");

    //            if (sInvAmount != string.Empty && Double.TryParse(sInvAmount, out dInvAmt))
    //            {
    //                sCommonValues = "'INDIVIDUAL DEPOSIT ACCOUNTS','" + rplsSnglQots(sInvAmount) + "','" + rplsSnglQots(sInvPeriod) + "','" + rplsSnglQots(sInvRateOfInt) + "'," +
    //                                "'" + rplsSnglQots(sOthBankName) + "','" + rplsSnglQots(sOthBankSC) + "','" + rplsSnglQots(sOthBankAcNo) + "','" + rplsSnglQots(sRepayIns) + "','" + rplsSnglQots(AcDtl.IsJntAc) + "','" + rplsSnglQots(AcDtl.Mrktngcont) + "'";
    //            }
    //            else
    //            {
    //                sCommonValues = "'INDIVIDUAL DEPOSIT ACCOUNTS',NULL,NULL,NULL," +
    //                                "'" + rplsSnglQots(sOthBankName).ToUpper() + "','" + rplsSnglQots(sOthBankSC).ToUpper() + "','" + rplsSnglQots(sOthBankAcNo).ToUpper() + "','" + rplsSnglQots(sRepayIns).ToUpper() + "','" + rplsSnglQots(AcDtl.IsJntAc).ToUpper() + "','" + rplsSnglQots(AcDtl.Mrktngcont) + "'";
    //            }

    //            int iApCount = 0;
    //            foreach (Applicant ApD in lstAplcnt)
    //            {
    //                iApCount += 1;

    //                string sPPNm, sPPNo, sPPIsDt, sPPExDt, sPPIC, sDvlcNo, sDvlcExDt, sDvlcPcode, sTDTOTAPP, sTDSTATUS, sTDDATE, sTDTIME, sRenewal, sIsSameAddr;
    //                string sTDAUTHENID, sUniqueSyndGLobalID, sTDAAC, sSREFNO, sTDFARESINCE, sTDFAPRADD1, sTDFAPRADD2, sTDFAPRADD3, sTDFAPRPCODE, sTDFAPRCRY;
    //                string sIDDtls, sPPTYPE, sPRVPRESENT, sTDFAPANAME, sDLTYPE, EmpType, EmptypOth;
    //                string IsUSperson;

    //                string sGlobalId = Convert.ToString(System.Web.HttpContext.Current.Session["GLOBANAID"]);

    //                sPPNm = sPPNo = sPPIsDt = sPPExDt = sPPIC = sDvlcNo = sDvlcExDt = sDvlcPcode = string.Empty;
    //                sTDTOTAPP = sTDSTATUS = sTDDATE = sTDTIME = sRenewal = sIsSameAddr = sTDAAC = string.Empty;
    //                sTDAUTHENID = sUniqueSyndGLobalID = sTDAAC = sSREFNO = sTDFARESINCE = sTDFAPRADD1 = sTDFAPRADD2 = sTDFAPRADD3 = sTDFAPRPCODE = sTDFAPRCRY = string.Empty;
    //                sIDDtls = sPPTYPE = sPRVPRESENT = sTDFAPANAME = sDLTYPE = EmpType = EmptypOth = IsUSperson = string.Empty;

    //                sTDTOTAPP = AcDtl.appList.Count().ToString();
    //                //sTDSTATUS = "APN";
    //                sTDDATE = DateTime.Now.ToString("yyyyMMdd");
    //                sTDTIME = DateTime.Now.ToString("HHmmss");
    //                sRenewal = "U";


    //                if (sAplnSts == "D")
    //                    sTDSTATUS = "APN";
    //                else
    //                    sTDSTATUS = "KYP";


    //                sTDAAC = "A";
    //                sSREFNO = SearchRefNo();

    //                if (ApD.UsePrimaryAddr.ToUpper() == "YES")
    //                    sIsSameAddr = "Y";
    //                else
    //                    sIsSameAddr = "N";

    //                if (ApD.PreAddr1.Length > 0 || ApD.PrePcode.Length > 0)
    //                    sPRVPRESENT = "Y";
    //                else
    //                    sPRVPRESENT = "N";


    //                if (ApD.IdenDtls.ToUpper() == "Driving Licence".ToUpper())
    //                {
    //                    sIDDtls = "Driving Licence".ToUpper();
    //                    sDLTYPE = ApD.DrLceType;
    //                    sDvlcNo = ApD.IdenNo;
    //                    sDvlcExDt = DTOC(ApD.DrLceExpDt);
    //                    sDvlcPcode = ApD.DrLcePCode;
    //                }
    //                else if (ApD.IdenDtls.ToUpper() == "Passport".ToUpper())
    //                {
    //                    sIDDtls = "Passport".ToUpper();
    //                    if (ApD.IsUkPsPrt.ToUpper() == "UK")
    //                        sPPTYPE = "UK";
    //                    else
    //                        sPPTYPE = "IT";

    //                    sPPIC = ApD.PsPrtIsCntry;
    //                    sPPNm = ApD.PsPrtName;
    //                    sPPNo = ApD.IdenNo;
    //                    sPPIsDt = DTOC(ApD.PsPrtIssDt);
    //                    sPPExDt = DTOC(ApD.PsPrtExpDt);
    //                }
    //                else
    //                {
    //                    //sIDDtls = "Passport".ToUpper(); 
    //                    sIDDtls = "";                     //modified chellappa
    //                    sDLTYPE = string.Empty;
    //                    sDvlcNo = string.Empty;
    //                    sDvlcExDt = string.Empty;
    //                    sDvlcPcode = string.Empty;

    //                    sPPTYPE = string.Empty;
    //                    sPPIC = string.Empty;
    //                    sPPNm = string.Empty;
    //                    sPPNo = string.Empty;
    //                    sPPIsDt = string.Empty;
    //                    sPPExDt = string.Empty;

    //                }

    //                /* FATCA */
    //                if (ApD.IsUSperson.ToUpper() == "YES")
    //                    IsUSperson = "Y";
    //                else if (ApD.IsUSperson.ToUpper() == "NO")
    //                    IsUSperson = "N";
    //                else
    //                    IsUSperson = "";
    //                /* FATCA */


    //                if (iApCount == 1)
    //                    aplQry.Append("SELECT '" + AcDtl.RegUniqueId.ToUpper() + "','','" + sRefNo.ToUpper() + "'," + sCommonValues + ",'YES','" + iApCount.ToString() + "',");
    //                else
    //                    aplQry.Append("SELECT '" + AcDtl.RegUniqueId.ToUpper() + "','" + sRefNo.ToUpper() + "',''," + sCommonValues + ",'NO','" + iApCount.ToString() + "',");

    //                aplQry.Append("'" + rplsSnglQots(ApD.Title).ToUpper() + "','" + rplsSnglQots(ApD.FirstNm).ToUpper() + "','" + rplsSnglQots(ApD.MidNm).ToUpper() + "','" + rplsSnglQots(ApD.SurNm).ToUpper() + "','" + rplsSnglQots(ApD.Gender).ToUpper() + "','" + rplsSnglQots(DTOC(ApD.DOB)).ToUpper() + "',");
    //                aplQry.Append("'" + rplsSnglQots(ApD.Citizenship).ToUpper() + "','" + rplsSnglQots(ApD.MaritalSts).ToUpper() + "','" + rplsSnglQots(ApD.HomeTelNo).ToUpper() + "','" + rplsSnglQots(ApD.MobileNo).ToUpper() + "','" + rplsSnglQots(ApD.EmailAddr) + "','" + rplsSnglQots(ApD.PlaceOfBirth).ToUpper() + "','" + rplsSnglQots(ApD.MothersMaidenNm).ToUpper() + "',");
    //                aplQry.Append("'" + rplsSnglQots(ApD.CurDoorNo).ToUpper() + ";" + rplsSnglQots(ApD.CurAddr1).ToUpper() + "','" + rplsSnglQots(ApD.CurAddr2).ToUpper() + "','" + rplsSnglQots(ApD.CurAddr3).ToUpper() + "','" + rplsSnglQots(ApD.CurPcode).ToUpper() + "','" + rplsSnglQots(ApD.CurCounty).ToUpper() + "','" + rplsSnglQots(ApD.CurCntry).ToUpper() + "',");
    //                aplQry.Append("'" + rplsSnglQots(sIDDtls).ToUpper() + "','" + rplsSnglQots(sPPTYPE).ToUpper() + "','" + rplsSnglQots(sPPNm).ToUpper() + "','" + rplsSnglQots(sDLTYPE).ToUpper() + "','" + rplsSnglQots(IsUSperson).ToUpper() + "','" + rplsSnglQots(ApD.PriJrsdctn).ToUpper() + "','" + rplsSnglQots(ApD.PriTIN).ToUpper() + "','" + rplsSnglQots(ApD.AdJrsdctn1).ToUpper() + "','" + rplsSnglQots(ApD.AdTIN1).ToUpper() + "','" + rplsSnglQots(ApD.AdJrsdctn2).ToUpper() + "','" + rplsSnglQots(ApD.AdTIN2).ToUpper() + "','" + rplsSnglQots(ApD.ResnNAPTIN).ToUpper() + "','" + rplsSnglQots(ApD.EmpType).ToUpper() + "','" + rplsSnglQots(ApD.EmptypOth).ToUpper() + "','" + rplsSnglQots(sPPNo).ToUpper() + "','" + rplsSnglQots(sPPIsDt).ToUpper() + "','" + rplsSnglQots(sPPExDt).ToUpper() + "','" + rplsSnglQots(sPPIC).ToUpper() + "',");
    //                aplQry.Append("'" + rplsSnglQots(sDvlcNo).ToUpper() + "','" + rplsSnglQots(sDvlcExDt).ToUpper() + "','" + rplsSnglQots(sDvlcPcode).ToUpper() + "','" + rplsSnglQots(ApD.NINO).ToUpper() + "',");
    //                aplQry.Append("'" + rplsSnglQots(sTDTOTAPP).ToUpper() + "','" + rplsSnglQots(sTDSTATUS).ToUpper() + "','" + rplsSnglQots(sTDDATE).ToUpper() + "','" + rplsSnglQots(sTDTIME).ToUpper() + "','" + rplsSnglQots(sRenewal).ToUpper() + "','" + rplsSnglQots(sIsSameAddr).ToUpper() + "',");

    //                //aplQry.Append("");

    //                aplQry.Append("'" + "AuthId" + "','" + sGlobalId.ToUpper() + "','" + rplsSnglQots(sTDAAC).ToUpper() + "','" + rplsSnglQots(sSREFNO).ToUpper() + "','" + rplsSnglQots(DTOC(ApD.ResidingSince)).ToUpper() + "',");
    //                aplQry.Append("'" + rplsSnglQots(sPRVPRESENT).ToUpper() + "','" + rplsSnglQots(ApD.PreDoorNo).ToUpper() + ";" + rplsSnglQots(ApD.PreAddr1).ToUpper() + "','" + rplsSnglQots(ApD.PreAddr2).ToUpper() + "','" + rplsSnglQots(ApD.PreAddr3).ToUpper() + "','" + rplsSnglQots(ApD.PrePcode).ToUpper() + "','" + rplsSnglQots(ApD.PreCounty).ToUpper() + "','" + rplsSnglQots(ApD.PreCntry).ToUpper() + "' UNION ALL ");
    //                //aplQry.Append("'" + rplsSnglQots(sPRVPRESENT).ToUpper() + "','" + rplsSnglQots(ApD.PreDoorNo).ToUpper() + "','" + rplsSnglQots(ApD.PreAddr1).ToUpper() + "','" + rplsSnglQots(ApD.PreAddr3).ToUpper() + "','" + rplsSnglQots(ApD.PrePcode).ToUpper() + "','" + rplsSnglQots(ApD.PreCounty).ToUpper() + "','" + rplsSnglQots(ApD.PreCntry).ToUpper() + "' UNION ALL ");

    //                if ((iApCount == 1) && (AcDtl.IsJntAc.ToUpper() == "No".ToUpper()))
    //                    break;

    //            }

    //            if (iApCount > 0)
    //            {
    //                sQuery = aplQry.Remove(aplQry.Length - 10, 10).ToString();

    //                ExecuteCommand(sQuery);

    //                Applicant oAplcnt = lstAplcnt[0];

    //                dtApDtl = ExecuteData("SELECT COUNT(ID) as CNT FROM TDAPPIND WHERE TDTRNID ='" + sRefNo + "' OR TDREFNO='" + sRefNo + "'");

    //                if (AcDtl.IsJntAc.ToUpper() == "No".ToUpper())
    //                {
    //                    if ((dtApDtl != null) && (dtApDtl.Rows.Count > 0) && (Convert.ToInt32(dtApDtl.Rows[0]["CNT"]) == 1))
    //                    {
    //                        if (Convert.ToString(System.Web.HttpContext.Current.Session["Type"]).ToUpper() == "NA")
    //                        {
    //                            string sQry = " INSERT INTO ACOPCUST (TRREF, CUFNAME, CULNAME, CUDOB, CUSEMAIL, CUSPWD, CUCTADD1, CUCTADD2, CUCTADD3, CUCTADD4, CUCOUNTRY, CUTEL, CUMOB,CUMODATE,CUMOTIME)" +
    //                              " SELECT '" + rplsSnglQots(AcDtl.ReferenceNo) + "','" + rplsSnglQots(oAplcnt.FirstNm) + "','" + rplsSnglQots(oAplcnt.SurNm) + "','" + rplsSnglQots(DTOC(oAplcnt.DOB)) + "','" + rplsSnglQots(oAplcnt.EmailAddr) + "','" + "" + "','" + rplsSnglQots(oAplcnt.CurDoorNo) + ";" + rplsSnglQots(oAplcnt.CurAddr1) + "','" + rplsSnglQots(oAplcnt.CurAddr2) + "','" + rplsSnglQots(oAplcnt.CurAddr3) + "','" + rplsSnglQots(oAplcnt.CurPcode) + "','" + rplsSnglQots(oAplcnt.CurCntry) + "','" + rplsSnglQots(oAplcnt.HomeTelNo) + "','" + rplsSnglQots(oAplcnt.MobileNo) + "','" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("HHmmss") + "'";

    //                            ExecuteCommand(sQry);
    //                        }

    //                        bStatus = true;
    //                    }
    //                }
    //                else
    //                {
    //                    if ((dtApDtl != null) && (dtApDtl.Rows.Count > 0) && (Convert.ToInt32(dtApDtl.Rows[0]["CNT"]) == lstAplcnt.Count))
    //                    {
    //                        if (Convert.ToString(System.Web.HttpContext.Current.Session["Type"]).ToUpper() == "NA")
    //                        {
    //                            string sQry = " INSERT INTO ACOPCUST (TRREF, CUFNAME, CULNAME, CUDOB, CUSEMAIL, CUSPWD, CUCTADD1, CUCTADD2, CUCTADD3, CUCTADD4, CUCOUNTRY, CUTEL, CUMOB,CUMODATE,CUMOTIME)" +
    //                              " SELECT '" + rplsSnglQots(AcDtl.ReferenceNo) + "','" + rplsSnglQots(oAplcnt.FirstNm) + "','" + rplsSnglQots(oAplcnt.SurNm) + "','" + rplsSnglQots(DTOC(oAplcnt.DOB)) + "','" + rplsSnglQots(oAplcnt.EmailAddr) + "','" + "" + "','" + rplsSnglQots(oAplcnt.CurDoorNo) + ";" + rplsSnglQots(oAplcnt.CurAddr1) + "','" + rplsSnglQots(oAplcnt.CurAddr2) + "','" + rplsSnglQots(oAplcnt.CurAddr3) + "','" + rplsSnglQots(oAplcnt.CurPcode) + "','" + rplsSnglQots(oAplcnt.CurCntry) + "','" + rplsSnglQots(oAplcnt.HomeTelNo) + "','" + rplsSnglQots(oAplcnt.MobileNo) + "','" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("HHmmss") + "'";

    //                            ExecuteCommand(sQry);
    //                        }

    //                        bStatus = true;
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            bStatus = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        StoreEvent("99999", "", "", "", replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(System.Web.HttpContext.Current.Session["RefNo"]));
    //        throw ex;
    //    }
    //    return bStatus;
    //}

    public bool SaveApplicant(string sAplnSts)
    {
        AccountDtl AcDtl = GetAcDtl();

        bool bStatus = false;
        string sQuery = string.Empty;

        DataTable dtApDtl = null;
        List<Applicant> lstAplcnt = null;
        StringBuilder aplQry = new StringBuilder("");

        try
        {
            lstAplcnt = AcDtl.appList;

            if (lstAplcnt != null && lstAplcnt.Count > 0)
            {
                string sQryField, sQryValue, sCommonValues;
                string sRefNo, sIsJntAc, sInvAmount, sInvPeriod, sInvRateOfInt, sOthBankName, sOthBankSC, sOthBankAcNo, sRepayIns;
                double dInvAmt;
                double dInvPrd;
                double dInvROI;

                sQryField = sQryValue = sCommonValues = string.Empty;
                sRefNo = sIsJntAc = sInvAmount = sInvPeriod = sInvRateOfInt = sOthBankName = sOthBankSC = sOthBankAcNo = sRepayIns = string.Empty;

                sRefNo = AcDtl.ReferenceNo;
                sIsJntAc = AcDtl.IsJntAc;

                sInvAmount = AcDtl.InvAmount;
                sInvPeriod = AcDtl.InvPeriod;
                sInvRateOfInt = AcDtl.InvRateOfInt;
                sOthBankName = AcDtl.OthBankName;
                sOthBankSC = AcDtl.OthBankSC;
                sOthBankAcNo = AcDtl.OthBankAcNo;
                sRepayIns = AcDtl.RepayIns;

                aplQry.Append("INSERT INTO TDAPPIND (TEMPREF,TDTRNID, TDREFNO,");
                aplQry.Append("TDACTYPE, TDINVAMT, TDINVPERIOD, TDINVRATINS, TDAIOTHRBNKNA, TDOTHSRCODE, TDOTHACNO, TDREPAYINS,TDISJOINT,MRKTNGCONT,FSCSCONT,FTDAWARECONT,");
                aplQry.Append("TDISPRIMARY, TDSEQUENCE,TDFATITLE, TDFANAME, TDMINAME, TDSURNAME, TDGNDR, TDFADOB,");
                aplQry.Append("TDFACITIZEN,TDFAMARITAL, TDFARESINO, TDFAMOBNO, TDFAEMAIL, TDPOB, TDMOTMDNME,");
                aplQry.Append("TDFACURADD1, TDFACURADD2, TDFACURADD3, TDFAPCODE, TDFACRY,TDCURCOUNTRY,");
                aplQry.Append("TDIDDTLS,PPTYPE,TDFAPANAME,DLTYPE,ISUKPERSON,PRIJRSDCTN,PRITIN,ADJRSDCTN1,ADTIN1,ADJRSDCTN2,ADTIN2,RESNNAPTIN,TDEMPDET,TDEDOTH,TDFAPASSNO, TDDOI, TDDOE, TDPOI, TDFADVLANO, TDDVLDOE, TDDVLPCODE,TDNATINSNO,");
                aplQry.Append("TDTOTAPP, TDSTATUS, TDDATE, TDTIME,Renewal,TDJOINTADD1,TDAUTHENID,UniqueSyndGLobalID,");
                aplQry.Append("TDAAC,SREFNO,TDFARESINCE,PRVPRESENT,TDFAPRADD1,TDFAPRADD2,TDFAPRADD3,TDFAPRPCODE,TDFAPRCRY,TDPRECOUNTRY,PAYTAX,USCITIZEN,GREENCARD,REALESTATE,NOTAX,SOFDET,SOFOTH,TDEMVERIFIEDSTS)");

                //if (sInvAmount != string.Empty && Double.TryParse(sInvAmount, out dInvAmt))
                //{
                //    sCommonValues = "'INDIVIDUAL DEPOSIT ACCOUNTS','" + rplsSnglQots(sInvAmount) + "','" + rplsSnglQots(sInvPeriod) + "','" + rplsSnglQots(sInvRateOfInt) + "'," +
                //                    "'" + rplsSnglQots(sOthBankName) + "','" + rplsSnglQots(sOthBankSC) + "','" + rplsSnglQots(sOthBankAcNo) + "','" + rplsSnglQots(sRepayIns) + "','" + rplsSnglQots(AcDtl.IsJntAc) + "','" + rplsSnglQots(AcDtl.Mrktngcont) + "','" + rplsSnglQots(AcDtl.FSCSCont) + "'";
                //}
                //else
                //{
                //    sCommonValues = "'INDIVIDUAL DEPOSIT ACCOUNTS',NULL,NULL,NULL," +
                //                    "'" + rplsSnglQots(sOthBankName).ToUpper() + "','" + rplsSnglQots(sOthBankSC).ToUpper() + "','" + rplsSnglQots(sOthBankAcNo).ToUpper() + "','" + rplsSnglQots(sRepayIns).ToUpper() + "','" + rplsSnglQots(AcDtl.IsJntAc).ToUpper() + "','" + rplsSnglQots(AcDtl.Mrktngcont) + "','" + rplsSnglQots(AcDtl.FSCSCont) + "'";
                //}
                sCommonValues = "'INDIVIDUAL DEPOSIT ACCOUNTS'";

                if (sInvAmount != string.Empty && Double.TryParse(sInvAmount, out dInvAmt))
                {
                    sCommonValues += ",'" + rplsSnglQots(sInvAmount) + "'";
                }
                else
                {
                    sCommonValues += ",NULL";
                }
                if (sInvPeriod != string.Empty)
                {
                    sCommonValues += ",'" + rplsSnglQots(sInvPeriod) + "'";
                }
                else
                {
                    sCommonValues += ",NULL";
                }
                if (sInvRateOfInt != string.Empty && Double.TryParse(sInvRateOfInt, out dInvROI))
                {
                    sCommonValues += ",'" + rplsSnglQots(dInvROI) + "'";
                }
                else
                {
                    sCommonValues += ",NULL";
                }

                sCommonValues += ",'" + rplsSnglQots(sOthBankName) + "','" + rplsSnglQots(sOthBankSC) + "','" + rplsSnglQots(sOthBankAcNo) + "','" + rplsSnglQots(sRepayIns) + "','" + rplsSnglQots(AcDtl.IsJntAc) + "','" + rplsSnglQots(AcDtl.Mrktngcont) + "','" + rplsSnglQots(AcDtl.FSCSCont) + "','" + rplsSnglQots(AcDtl.CheckTnC) + "'";

                int iApCount = 0;
                foreach (Applicant ApD in lstAplcnt)
                {
                    iApCount += 1;

                    string sPPNm, sPPNo, sPPIsDt, sPPExDt, sPPIC, sDvlcNo, sDvlcExDt, sDvlcPcode, sTDTOTAPP, sTDSTATUS, sTDDATE, sTDTIME, sRenewal, sIsSameAddr;
                    string sTDAUTHENID, sUniqueSyndGLobalID, sTDAAC, sSREFNO, sTDFARESINCE, sTDFAPRADD1, sTDFAPRADD2, sTDFAPRADD3, sTDFAPRPCODE, sTDFAPRCRY;
                    string sIDDtls, sPPTYPE, sPRVPRESENT, sTDFAPANAME, sDLTYPE, EmpType, EmptypOth;
                    string IsUSperson;
                    string PayTax, USCitizen, GreenCard, RealEst, assets, sTDEMVERIFIEDSTS;

                    string sGlobalId = Convert.ToString(System.Web.HttpContext.Current.Session["GLOBANAID"]);

                    sPPNm = sPPNo = sPPIsDt = sPPExDt = sPPIC = sDvlcNo = sDvlcExDt = sDvlcPcode = string.Empty;
                    sTDTOTAPP = sTDSTATUS = sTDDATE = sTDTIME = sRenewal = sIsSameAddr = sTDAAC = string.Empty;
                    sTDAUTHENID = sUniqueSyndGLobalID = sTDAAC = sSREFNO = sTDFARESINCE = sTDFAPRADD1 = sTDFAPRADD2 = sTDFAPRADD3 = sTDFAPRPCODE = sTDFAPRCRY = string.Empty;
                    sIDDtls = sPPTYPE = sPRVPRESENT = sTDFAPANAME = sDLTYPE = EmpType = EmptypOth = IsUSperson = PayTax = USCitizen = GreenCard = RealEst = assets = sTDEMVERIFIEDSTS = string.Empty;


                    sTDTOTAPP = AcDtl.appList.Count().ToString();
                    //sTDSTATUS = "APN";
                    sTDDATE = DateTime.Now.ToString("yyyyMMdd");
                    sTDTIME = DateTime.Now.ToString("HHmmss");
                    sRenewal = "U";


                    if (sAplnSts == "D")
                        sTDSTATUS = "APN";
                    else
                        sTDSTATUS = "KYP";


                    sTDAAC = "A";
                    sSREFNO = SearchRefNo();

                    if (ApD.UsePrimaryAddr.ToUpper() == "YES")
                        sIsSameAddr = "Y";
                    else
                        sIsSameAddr = "N";

                    if (ApD.PreAddr1.Length > 0 || ApD.PrePcode.Length > 0)
                        sPRVPRESENT = "Y";
                    else
                        sPRVPRESENT = "N";


                    if (ApD.IdenDtls.ToUpper() == "Driving Licence".ToUpper())
                    {
                        sIDDtls = "Driving Licence".ToUpper();
                        sDLTYPE = ApD.DrLceType;
                        sDvlcNo = ApD.IdenNo;
                        sDvlcExDt = DTOC(ApD.DrLceExpDt);
                        sDvlcPcode = ApD.DrLcePCode;
                    }
                    else if (ApD.IdenDtls.ToUpper() == "Passport".ToUpper())
                    {
                        sIDDtls = "Passport".ToUpper();
                        if (ApD.IsUkPsPrt.ToUpper() == "UK")
                            sPPTYPE = "UK";
                        else
                            sPPTYPE = "IT";

                        sPPIC = ApD.PsPrtIsCntry;
                        sPPNm = ApD.PsPrtName;
                        sPPNo = ApD.IdenNo;
                        sPPIsDt = DTOC(ApD.PsPrtIssDt);
                        sPPExDt = DTOC(ApD.PsPrtExpDt);
                    }
                    else
                    {
                        //sIDDtls = "Passport".ToUpper(); 
                        sIDDtls = "";                     //modified chellappa
                        sDLTYPE = string.Empty;
                        sDvlcNo = string.Empty;
                        sDvlcExDt = string.Empty;
                        sDvlcPcode = string.Empty;

                        sPPTYPE = string.Empty;
                        sPPIC = string.Empty;
                        sPPNm = string.Empty;
                        sPPNo = string.Empty;
                        sPPIsDt = string.Empty;
                        sPPExDt = string.Empty;

                    }

                    /* FATCA */
                    if (ApD.IsUSperson.ToUpper() == "YES")
                        IsUSperson = "Y";
                    else if (ApD.IsUSperson.ToUpper() == "NO")
                        IsUSperson = "N";
                    else
                        IsUSperson = "";
                    /* FATCA */

                    /*FATCA-Chella-20171005*/
                    if (ApD.PayTax.ToUpper() == "YES")
                        PayTax = "Y";
                    else if (ApD.PayTax.ToUpper() == "NO")
                        PayTax = "N";
                    else
                        PayTax = "";

                    if (ApD.USCitizen.ToUpper() == "YES")
                        USCitizen = "Y";
                    else if (ApD.USCitizen.ToUpper() == "NO")
                        USCitizen = "N";
                    else
                        USCitizen = "";

                    if (ApD.GreenCard.ToUpper() == "YES")
                        GreenCard = "Y";
                    else if (ApD.GreenCard.ToUpper() == "NO")
                        GreenCard = "N";
                    else
                        GreenCard = "";

                    if (ApD.RealEst.ToUpper() == "YES")
                        RealEst = "Y";
                    else if (ApD.RealEst.ToUpper() == "NO")
                        RealEst = "N";
                    else
                        RealEst = "";

                    if (ApD.assets.ToUpper() == "YES")
                        assets = "Y";
                    else if (ApD.assets.ToUpper() == "NO")
                        assets = "N";
                    else
                        assets = "";
                    /*FATCA-Chella-20171005*/

                    //chellappa - 20190125
                    string SOFType = string.Empty;
                    string SOFOth = string.Empty;
                    SOFType = rplsSnglQots(ApD.sof).ToUpper();
                    SOFOth = rplsSnglQots(ApD.sofOth).ToUpper();

                    if (ApD.EMVerSts.ToUpper() == "Y")
                    {
                        sTDEMVERIFIEDSTS = "Y";
                    }
                    else
                    {
                        sTDEMVERIFIEDSTS = "N";
                    }

                    if (iApCount == 1)
                        aplQry.Append("SELECT '" + AcDtl.RegUniqueId.ToUpper() + "','','" + sRefNo.ToUpper() + "'," + sCommonValues + ",'YES','" + iApCount.ToString() + "',");
                    else
                        aplQry.Append("SELECT '" + AcDtl.RegUniqueId.ToUpper() + "','" + sRefNo.ToUpper() + "',''," + sCommonValues + ",'NO','" + iApCount.ToString() + "',");

                    aplQry.Append("'" + rplsSnglQots(ApD.Title).ToUpper() + "','" + rplsSnglQots(ApD.FirstNm).ToUpper() + "','" + rplsSnglQots(ApD.MidNm).ToUpper() + "','" + rplsSnglQots(ApD.SurNm).ToUpper() + "','" + rplsSnglQots(ApD.Gender).ToUpper() + "','" + rplsSnglQots(DTOC(ApD.DOB)).ToUpper() + "',");
                    aplQry.Append("'" + rplsSnglQots(ApD.Citizenship).ToUpper() + "','" + rplsSnglQots(ApD.MaritalSts).ToUpper() + "','" + rplsSnglQots(ApD.HomeTelNo).ToUpper() + "','" + rplsSnglQots(ApD.MobileNo).ToUpper() + "','" + rplsSnglQots(ApD.EmailAddr) + "','" + rplsSnglQots(ApD.PlaceOfBirth).ToUpper() + "','" + rplsSnglQots(ApD.MothersMaidenNm).ToUpper() + "',");
                    aplQry.Append("'" + rplsSnglQots(ApD.CurDoorNo).ToUpper() + ";" + rplsSnglQots(ApD.CurAddr1).ToUpper() + "','" + rplsSnglQots(ApD.CurAddr2).ToUpper() + "','" + rplsSnglQots(ApD.CurAddr3).ToUpper() + "','" + rplsSnglQots(ApD.CurPcode).ToUpper() + "','" + rplsSnglQots(ApD.CurCounty).ToUpper() + "','" + rplsSnglQots(ApD.CurCntry).ToUpper() + "',");
                    aplQry.Append("'" + rplsSnglQots(sIDDtls).ToUpper() + "','" + rplsSnglQots(sPPTYPE).ToUpper() + "','" + rplsSnglQots(sPPNm).ToUpper() + "','" + rplsSnglQots(sDLTYPE).ToUpper() + "','" + rplsSnglQots(IsUSperson).ToUpper() + "','" + rplsSnglQots(ApD.PriJrsdctn).ToUpper() + "','" + rplsSnglQots(ApD.PriTIN).ToUpper() + "','" + rplsSnglQots(ApD.AdJrsdctn1).ToUpper() + "','" + rplsSnglQots(ApD.AdTIN1).ToUpper() + "','" + rplsSnglQots(ApD.AdJrsdctn2).ToUpper() + "','" + rplsSnglQots(ApD.AdTIN2).ToUpper() + "','" + rplsSnglQots(ApD.ResnNAPTIN).ToUpper() + "','" + rplsSnglQots(ApD.EmpType).ToUpper() + "','" + rplsSnglQots(ApD.EmptypOth).ToUpper() + "','" + rplsSnglQots(sPPNo).ToUpper() + "','" + rplsSnglQots(sPPIsDt).ToUpper() + "','" + rplsSnglQots(sPPExDt).ToUpper() + "','" + rplsSnglQots(sPPIC).ToUpper() + "',");
                    aplQry.Append("'" + rplsSnglQots(sDvlcNo).ToUpper() + "','" + rplsSnglQots(sDvlcExDt).ToUpper() + "','" + rplsSnglQots(sDvlcPcode).ToUpper() + "','" + rplsSnglQots(ApD.NINO).ToUpper() + "',");
                    aplQry.Append("'" + rplsSnglQots(sTDTOTAPP).ToUpper() + "','" + rplsSnglQots(sTDSTATUS).ToUpper() + "','" + rplsSnglQots(sTDDATE).ToUpper() + "','" + rplsSnglQots(sTDTIME).ToUpper() + "','" + rplsSnglQots(sRenewal).ToUpper() + "','" + rplsSnglQots(sIsSameAddr).ToUpper() + "',");

                    //aplQry.Append("");

                    aplQry.Append("'" + "AuthId" + "','" + sGlobalId.ToUpper() + "','" + rplsSnglQots(sTDAAC).ToUpper() + "','" + rplsSnglQots(sSREFNO).ToUpper() + "','" + rplsSnglQots(DTOC(ApD.ResidingSince)).ToUpper() + "',");
                    aplQry.Append("'" + rplsSnglQots(sPRVPRESENT).ToUpper() + "','" + rplsSnglQots(ApD.PreDoorNo).ToUpper() + ";" + rplsSnglQots(ApD.PreAddr1).ToUpper() + "','" + rplsSnglQots(ApD.PreAddr2).ToUpper() + "','" + rplsSnglQots(ApD.PreAddr3).ToUpper() + "','" + rplsSnglQots(ApD.PrePcode).ToUpper() + "','" + rplsSnglQots(ApD.PreCounty).ToUpper() + "','" + rplsSnglQots(ApD.PreCntry).ToUpper() + "','" + rplsSnglQots(PayTax).ToUpper() + "','" + rplsSnglQots(USCitizen).ToUpper() + "','" + rplsSnglQots(GreenCard).ToUpper() + "','" + rplsSnglQots(RealEst).ToUpper() + "','" + rplsSnglQots(assets).ToUpper() + "','" + SOFType + "','" + SOFOth + "','" + sTDEMVERIFIEDSTS + "' UNION ALL ");
                    //aplQry.Append("'" + rplsSnglQots(sPRVPRESENT).ToUpper() + "','" + rplsSnglQots(ApD.PreDoorNo).ToUpper() + "','" + rplsSnglQots(ApD.PreAddr1).ToUpper() + "','" + rplsSnglQots(ApD.PreAddr3).ToUpper() + "','" + rplsSnglQots(ApD.PrePcode).ToUpper() + "','" + rplsSnglQots(ApD.PreCounty).ToUpper() + "','" + rplsSnglQots(ApD.PreCntry).ToUpper() + "' UNION ALL ");


                    StoreEvent("99999", "", "", "", "Applicant Details inserted into TDAPPIND Successfully", Convert.ToString(System.Web.HttpContext.Current.Session["RefNo"]));

                    if ((iApCount == 1) && (AcDtl.IsJntAc.ToUpper() == "No".ToUpper()))
                        break;

                }

                if (iApCount > 0)
                {
                    sQuery = aplQry.Remove(aplQry.Length - 10, 10).ToString();

                    ExecuteCommand(sQuery);

                    Applicant oAplcnt = lstAplcnt[0];

                    dtApDtl = ExecuteData("SELECT COUNT(ID) as CNT FROM TDAPPIND WHERE TDTRNID ='" + sRefNo + "' OR TDREFNO='" + sRefNo + "'");

                    if (AcDtl.IsJntAc.ToUpper() == "No".ToUpper())
                    {
                        if ((dtApDtl != null) && (dtApDtl.Rows.Count > 0) && (Convert.ToInt32(dtApDtl.Rows[0]["CNT"]) == 1))
                        {
                            if (Convert.ToString(System.Web.HttpContext.Current.Session["Type"]).ToUpper() == "NA")
                            {
                                string sQry = " INSERT INTO ACOPCUST (TRREF, CUFNAME, CULNAME, CUDOB, CUSEMAIL, CUSPWD, CUCTADD1, CUCTADD2, CUCTADD3, CUCTADD4, CUCOUNTRY, CUTEL, CUMOB,CUMODATE,CUMOTIME)" +
                                  " SELECT '" + rplsSnglQots(AcDtl.ReferenceNo) + "','" + rplsSnglQots(oAplcnt.FirstNm) + "','" + rplsSnglQots(oAplcnt.SurNm) + "','" + rplsSnglQots(DTOC(oAplcnt.DOB)) + "','" + rplsSnglQots(oAplcnt.EmailAddr) + "','" + "" + "','" + rplsSnglQots(oAplcnt.CurDoorNo) + ";" + rplsSnglQots(oAplcnt.CurAddr1) + "','" + rplsSnglQots(oAplcnt.CurAddr2) + "','" + rplsSnglQots(oAplcnt.CurAddr3) + "','" + rplsSnglQots(oAplcnt.CurPcode) + "','" + rplsSnglQots(oAplcnt.CurCntry) + "','" + rplsSnglQots(oAplcnt.HomeTelNo) + "','" + rplsSnglQots(oAplcnt.MobileNo) + "','" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("HHmmss") + "'";

                                ExecuteCommand(sQry);
                            }

                            bStatus = true;
                        }
                    }
                    else
                    {
                        if ((dtApDtl != null) && (dtApDtl.Rows.Count > 0) && (Convert.ToInt32(dtApDtl.Rows[0]["CNT"]) == lstAplcnt.Count))
                        {
                            if (Convert.ToString(System.Web.HttpContext.Current.Session["Type"]).ToUpper() == "NA")
                            {
                                string sQry = " INSERT INTO ACOPCUST (TRREF, CUFNAME, CULNAME, CUDOB, CUSEMAIL, CUSPWD, CUCTADD1, CUCTADD2, CUCTADD3, CUCTADD4, CUCOUNTRY, CUTEL, CUMOB,CUMODATE,CUMOTIME)" +
                                  " SELECT '" + rplsSnglQots(AcDtl.ReferenceNo) + "','" + rplsSnglQots(oAplcnt.FirstNm) + "','" + rplsSnglQots(oAplcnt.SurNm) + "','" + rplsSnglQots(DTOC(oAplcnt.DOB)) + "','" + rplsSnglQots(oAplcnt.EmailAddr) + "','" + "" + "','" + rplsSnglQots(oAplcnt.CurDoorNo) + ";" + rplsSnglQots(oAplcnt.CurAddr1) + "','" + rplsSnglQots(oAplcnt.CurAddr2) + "','" + rplsSnglQots(oAplcnt.CurAddr3) + "','" + rplsSnglQots(oAplcnt.CurPcode) + "','" + rplsSnglQots(oAplcnt.CurCntry) + "','" + rplsSnglQots(oAplcnt.HomeTelNo) + "','" + rplsSnglQots(oAplcnt.MobileNo) + "','" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("HHmmss") + "'";

                                ExecuteCommand(sQry);
                            }

                            bStatus = true;
                        }
                    }
                }
            }
            else
            {
                bStatus = false;
            }
        }
        catch (Exception ex)
        {
            StoreEvent("88888", "", "", "", replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(System.Web.HttpContext.Current.Session["RefNo"]));
            throw ex;
        }
        return bStatus;
    }

    public bool IsDate(string sdate) //20130826
    {

        try
        {

            DateTime dt;
            bool isDate = true;

            try
            {
                if (sdate.Length == 0)
                {
                    isDate = false;
                }
                else if ((sdate.Length > 0) && (sdate.Length == 10))
                {
                    dt = DateTime.Parse(sdate);
                }
                else
                {
                    isDate = false;
                }
            }
            catch
            {
                isDate = false;
            }

            return isDate;
        }

        catch (Exception ex)
        {
            StoreEvent("88888", "", "", "", replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(System.Web.HttpContext.Current.Session["RefNo"]));
            throw ex;
        }
    }

    //public bool IsExistingcustomer(string sName, string sDOB)
    //{
    //    bool bStatus = true;
    //    DataTable dtAcDtl = null;
    //    try
    //    {
    //        string sQuery = " SELECT CUSTNAME FROM CUSACSTS WHERE 1=1 " +
    //                        " AND  " +
    //                        " UPPER(LTRIM(RTRIM(REPLACE(ISNULL(CUSTNAME,''),' ','')))) = UPPER(REPLACE('" + replaceSplChr(sName) + "',' ','')) " +
    //                        " AND  " +
    //                        " CASE ISNULL(CUSTDOB,'') WHEN '' THEN '' ELSE SUBSTRING(CUSTDOB,7,4) + SUBSTRING(CUSTDOB,4,2) + SUBSTRING(CUSTDOB,1,2) END  = '" + DTOC(sDOB) + "'";

    //        dtAcDtl = ExecuteData(sQuery);
    //        if ((dtAcDtl != null) && (dtAcDtl.Rows.Count == 0))
    //            bStatus = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        StoreEvent("88888", "", "", "", replaceSplChr(ex.Message + ex.ToString()), Convert.ToString(System.Web.HttpContext.Current.Session["RefNo"]));
    //        bStatus = true;
    //        throw ex;
    //    }

    //    return bStatus;
    //}

    //20190125-chellappa
    public byte[] StreamToBytes(Stream stream_val)
    {
        stream_val.Position = 0;

        try
        {
            using (var memoryStream_val = new MemoryStream())
            {
                stream_val.CopyTo(memoryStream_val);
                return memoryStream_val.ToArray();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public string BytesToString(byte[] InputVal)
    {
        try
        {
            string strResult = null;
            strResult = string.Empty;

            strResult = Convert.ToBase64String(InputVal, 0, InputVal.Length - 1);

            return (strResult);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //Hashing Algorithm 2024
    public string CreateSalt(int saltSize)
    {
        var buff = new byte[saltSize];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(buff);
        }
        return Convert.ToBase64String(buff);
    }
    public string EncryptPassword(string pPassword, string pSalt)
    {
        var saltAndPwd = String.Concat(pPassword, pSalt);
        var hashedPwd = GetSwcSha1(saltAndPwd);
        return hashedPwd;
    }
    static string GetSwcSha1(string value)
    {
        var algorithm = SHA1.Create();
        var data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
        var sh1 = new StringBuilder();

        foreach (byte t in data)
        {
            sh1.Append(t.ToString("x2").ToUpperInvariant());
        }
        return sh1.ToString();
    }
    //End    

    //2023 Enhancements Remaining
    public int ReturnInteger(string query, params SqlParameter[] parameters)
    {
        try
        {
            SqlConnection con = Connection();
            OpenConnection();

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                object obj = cmd.ExecuteScalar();

                return (obj != null && obj != DBNull.Value)
                       ? Convert.ToInt32(obj)
                       : 0;
            }
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        finally
        {
            CloseConnection();
        }
    }

    public string ReturnString(string query, params SqlParameter[] parameters)
    {
        try
        {
            SqlConnection con = Connection();
            OpenConnection();

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                object obj = cmd.ExecuteScalar();

                return (obj != null && obj != DBNull.Value)
                       ? obj.ToString()
                       : string.Empty;
            }
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        finally
        {
            CloseConnection();
        }
    }

    public int ExecuteCommandWthParam(string query, params SqlParameter[] parameters)
    {
        try
        {
            Connection();
            OpenConnection();

            using (SqlCommand cmd = new SqlCommand(query, sqlcon))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        finally
        {
            CloseConnection();
        }
    }

    public DataTable ExecuteDataWthParam(string query, params SqlParameter[] parameters)
    {
        DataTable dt = new DataTable();

        try
        {
            Connection();
            OpenConnection();

            using (SqlCommand cmd = new SqlCommand(query, sqlcon))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        finally
        {
            CloseConnection();
        }

        return dt;
    }

    //Tenure process
    public class DepositAmountInfo
    {
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
    }

    public DepositAmountInfo GetDepositAmounts()
    {
        DepositAmountInfo info = new DepositAmountInfo();

        string fSelect = @"SELECT PTYPE, POLDET 
                       FROM POLMTPF 
                       WHERE PMODULE = 'DEPOSIT'
                         AND PTYPE IN ('MINAMOUNT','MAXAMOUNT')";

        DataTable dt = ExecuteData(fSelect);

        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row["PTYPE"].ToString() == "MINAMOUNT")
                    info.MinAmount = Convert.ToDecimal(row["POLDET"]);

                if (row["PTYPE"].ToString() == "MAXAMOUNT")
                    info.MaxAmount = Convert.ToDecimal(row["POLDET"]);
            }
        }

        return info;
    }
    //End

    public bool SendOTPTOCUST(string sCUSID, string sCustEmailVal, string sCustMobileNo, string sIsOTPResend, int OTPResendCnt, string sModule)
    {
        bool SendOTPSts = false;

        try
        {
            string sOTPVal = string.Empty, SendOTPEmailSts = "N", SendOTPMobileSts = "N";
            sOTPVal = GenerateRandomOTP();

            if (!string.IsNullOrEmpty(sCustEmailVal) )
            {
                if (sIsOTPResend == "Y")
                {
                    UpdateEmailOTP(sCUSID, sOTPVal, sCustEmailVal, sCustMobileNo, 0, OTPResendCnt, sModule);
                }
                else
                {
                    SetOTP(sCUSID, sOTPVal, sCustEmailVal, sCustMobileNo, 0, 0, sModule);
                }

                if (SendOTPToCusEmail(sCUSID, sOTPVal, sCustEmailVal, sModule) == true)
                {
                    SendOTPEmailSts = "Y";
                    StoreEvent("99999", "", "", "", sModule + " NC OTP - successfully sent the OTP to customer email", sCUSID);
                }

                //if (SendOTPToCusMobile(sCUSID, sOTPVal, sCustMobileNo, sModule) == true)
                //{
                //    SendOTPMobileSts = "Y";
                //    StoreEvent("99999", "", "", "", sModule + " NC OTP - successfully sent the OTP to customer email or mobile", sCUSID);
                //}

                if (SendOTPEmailSts == "Y")
                {
                    SendOTPSts = true;
                }
            }
        }
        catch (Exception ex)
        {
            StoreEvent("99999", "", "", "", sModule + " NC OTP - failed to send the OTP to customer email or mobile", sCUSID);
            throw ex;
        }
        return SendOTPSts;
    }

    public string GenerateRandomOTP()
    {
        Random generator = new Random();
        String r = generator.Next(0, 1000000).ToString("D6");
        if (r.Distinct().Count() == 1)
        {
            r = GenerateRandomOTP();
        }
        return r;
    }

    public void SetOTP(string sCUSID, string sOTP, string sOTPEmail, string sOTPMobileNo, int OTPAtt, int OTPResendCnt, string sModule)
    {
        try
        {
            StoreEvent("99999", "", "", "", sModule + " NC OTP - Process initiated to set the OTP", sCUSID);
            string fSelect = string.Empty;

            fSelect = "INSERT INTO OTP_History (OTP,CUSTID,OTPEMAIL,OTPMOBILE,OTPGNDATE,OTPGNTIME,OTPATTEMPT,OTPRESENDCOUNT,OTPSTATUS,EMISBLOCKED,EMBLOCKDATETIME,OTPVERIFIEDTIME,OTPMODULE) " +
                           "VALUES (@OTP, @CUSID, @OTPEMAIL, @OTPMOBILE, @OTPGNDATE, @OTPGNTIME, @OTPATTEMPT,@OTPRESENDCOUNT,@OTPSTATUS,@EMISBLOCKED,@EMBLOCKDATETIME,@OTPVERIFIEDTIME,@OTPMODULE)";
            ExecuteCommandWthParam(fSelect,
                new SqlParameter("@OTP", sOTP),
                new SqlParameter("@CUSID", sCUSID),
                new SqlParameter("@OTPEMAIL", sOTPEmail),
                new SqlParameter("@OTPMOBILE", sOTPMobileNo),
                new SqlParameter("@OTPGNDATE", DateTime.Now.ToString("yyyyMMdd")),
                new SqlParameter("@OTPGNTIME", DateTime.Now.ToString("yyyyMMddHHmm")),
                new SqlParameter("@OTPATTEMPT", OTPAtt),
                new SqlParameter("@OTPRESENDCOUNT", OTPResendCnt),
                new SqlParameter("@OTPSTATUS", "Y"),
                new SqlParameter("@EMISBLOCKED", ""),
                new SqlParameter("@EMBLOCKDATETIME", ""),
                new SqlParameter("@OTPVERIFIEDTIME", ""),
                new SqlParameter("@OTPMODULE", sModule)
            );
            StoreEvent("99999", "", "", "", sModule + " NC OTP - Process completed to set the OTP", sCUSID);
        }
        catch (Exception ex)
        {
            StoreEvent("99999", "", "", "", sModule + " NC OTP - Process failed to set the OTP", sCUSID);
            throw ex;
        }
    }

    private void UpdateEmailOTP(string sCUSID, string sOTPVal, string sCustEmailVal, string sCustMobileNo, int sOTPAtt, int sOTPResendCnt, string sModule)
    {
        string fUpdate = string.Empty;
        try
        {
            if (!string.IsNullOrEmpty(sCUSID))
            {
                StoreEvent("99999", "", "", "", sModule + " NC OTP – Resend flow initiated to update OTP details in the history table.", sCUSID);

                string fUpdQry = "UPDATE OTP_History SET OTP=@OTP,OTPGNDATE=@OTPGNDATE,OTPGNTIME=@OTPGNTIME,OTPATTEMPT=@OTPATTEMPT,OTPRESENDCOUNT=@OTPRESENDCOUNT " +
                                 "WHERE CUSTID=@CUSID AND OTPSTATUS=@OTPSTATUS AND OTPMODULE=@OTPMODULE ";
                ExecuteCommandWthParam(fUpdQry,
                    new SqlParameter("@OTP", sOTPVal),
                    new SqlParameter("@CUSID", sCUSID),
                    new SqlParameter("@OTPGNDATE", DateTime.Now.ToString("yyyyMMdd")),
                    new SqlParameter("@OTPGNTIME", DateTime.Now.ToString("yyyyMMddHHmm")),
                    new SqlParameter("@OTPATTEMPT", sOTPAtt),
                    new SqlParameter("@OTPRESENDCOUNT", sOTPResendCnt),
                    new SqlParameter("@OTPSTATUS", "Y"),
                    new SqlParameter("@OTPMODULE", sModule)
                    );

                StoreEvent("99999", "", "", "", sModule + " NC OTP – Resend flow successfully completed to update OTP details in the history table.", sCUSID);
            }
        }
        catch (Exception ex)
        {
            StoreEvent("99999", "", "", "", sModule + " NC OTP – Resend flow failed to update OTP details in the history table.", sCUSID);
            throw ex;
        }
    }

    public bool SendOTPToCusEmail(string sCUSID, string sOTPVal, string sCustEmailVal, string sModule)
    {
        bool sEmailOTPSts = false;

        try
        {
            string Content = string.Empty, subject = string.Empty, sLogo = string.Empty, MsgBody = string.Empty, sHeader = string.Empty, sFooter = string.Empty;

            string pName = RetPolValue("UBI", "PRODUCTNAME");
            string sOTPVALIDMIN = RetPolValue("OTP", "OTPValTim");

            Content = GetContent("Email OTP - Application Retrieval");
            Content = Content.Replace("(OTPVAL)", sOTPVal);
            Content = Content.Replace("(OTPVALIDMIN)", sOTPVALIDMIN);
            Content = Content.Replace("(PRODUCTNAME)", pName);

            subject = "UBI(UK) - OTP for Application Retrieval";

            sLogo = RetPolValue("MAIL", "LOGO");
            sHeader = RetPolValue("MAIL", "HEADER");
            sFooter = RetPolValue("MAIL", "FOOTER");
            sHeader = sHeader.Replace("UBIUKLOGO", sLogo);
            MsgBody = sHeader + Content + sFooter;

            //sEmailOTPSts = true;  //test purpose

            if (SendEmailMessage(sCustEmailVal, subject, MsgBody, "", "", "", "", false, "", "", EmailRepository.AlertEmail.MailType.Customer))
            {
                sEmailOTPSts = true;
                StoreEvent("99999", "", "", "", sModule + " NC OTP - OTP sent successfully to the customer Email.", sCUSID);
                System.Threading.Thread.Sleep(5000);
            }
            else
            {
                StoreEvent("99999", "", "", "", sModule + " NC OTP - Failed to send the OTP to the customer Email.", sCUSID);
            }
        }
        catch (Exception ex)
        {
            StoreEvent("99999", "", "", "", sModule + " NC OTP - Error to send the OTP to the customer Email.", sCUSID);
        }
        return sEmailOTPSts;
    }

    public bool SendOTPToCusMobile(string sCUSID, string sOTPVal, string sCustMobileNo, string sModule)
    {
        bool sMobileOTPSts = true;  //testing purpose

        ////Need to uncomment while QA launch
        //bool sMobileOTPSts = false;

        //string status = "Failed";

        //KaleyraSmsService smsService = new KaleyraSmsService();
        //KaleyraResponse resonse = smsService.SendOtp(sCustMobileNo, sOTPVal);

        //try
        //{
        //    if (resonse.Success)
        //    {
        //        status = "Success";
        //        sMobileOTPSts = true;
        //    }
        //    else
        //    {
        //        status = "Failed";
        //        sMobileOTPSts = false;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    sMobileOTPSts = false;
        //}

        //InsertSMSLog(sCUSID, sCustMobileNo, sOTPVal, resonse.MessageId, resonse.CreatedDateTime, resonse.Recipient, resonse.RawJson, status, sModule);

        return sMobileOTPSts;
    }

    public void InsertSMSLog(string custId, string mobile, string otp, string msgId, string created, string recipient, string fullJson, string status, string sModule)
    {
        try
        {
            StoreEvent("99999", "", "", "", sModule + " NC OTP – Initiated the process to insert the OTP SMS log.", custId);

            string query = @"INSERT INTO SMS_OTP_Log(CUSTID, MOBILE, OTP, MSGID, CreatedDateTime,RECIPIENT, FULLJSON, OTPSTATUS,CREATEDON,OTPModule) VALUES
                     (@CUSTID, @MOBILE, @OTP, @MSGID, @CreatedDateTime, @RECIPIENT, @FULLJSON, @OTPSTATUS, @CREATEDON, @OTPModule)";

            ExecuteCommandWthParam(query,
                new SqlParameter("@CUSTID", custId),
                new SqlParameter("@MOBILE", mobile),
                new SqlParameter("@OTP", otp),
                new SqlParameter("@MSGID", (object)msgId ?? DBNull.Value),
                new SqlParameter("@CreatedDateTime", (object)created ?? DBNull.Value),
                new SqlParameter("@RECIPIENT", (object)recipient ?? DBNull.Value),
                new SqlParameter("@FULLJSON", fullJson),
                new SqlParameter("@OTPSTATUS", status),
                new SqlParameter("@CREATEDON", DateTime.Now.ToString("yyyyMMddHHmm")),
                new SqlParameter("@OTPModule", sModule)
            );
        }
        catch (Exception ex)
        {
            StoreEvent("99999", "", "", "", sModule + " NC OTP - Error occurred while inserting the OTP SMS log.", HttpContext.Current.Session["loginid"].ToString());
        }
    }

    public string MaskEmail(string email)
    {
        try
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
                return email;

            string[] parts = email.Split('@');
            string username = parts[0];
            string domain = parts[1];

            if (username.Length <= 3)
            {
                string maskedMiddle1 = new string('#', username.Length);
                return maskedMiddle1 + "@" + domain;
            }

            string first = username.Substring(0, 1);
            string last2 = username.Substring(username.Length - 2, 2);

            string maskedMiddle = new string('#', username.Length - 3);

            return first + maskedMiddle + last2 + "@" + domain;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string MaskMobile(string mobile)
    {
        if (string.IsNullOrEmpty(mobile) || mobile.Length < 3)
            return mobile;

        string first = mobile.Substring(0, 1);
        string last2 = mobile.Substring(mobile.Length - 2, 2);

        string maskedMiddle = new string('#', mobile.Length - 3);

        return first + maskedMiddle + last2;
    }

    public OTPInfo GetOTPVal(string sCUSID, string OTPModule)
    {
        OTPInfo sOTPVal = new OTPInfo();

        try
        {
            DataRow dr;
            string fSelect = "SELECT * FROM OTP_History WHERE CUSTID='" + sCUSID + "' AND OTPSTATUS='Y' AND OTPMODULE='" + OTPModule + "'";
            DataTable dt = ExecuteData(fSelect);
            if (dt != null && dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];

                sOTPVal.OTP = NullToSpace(dr["OTP"]);
                sOTPVal.CUSTID = NullToSpace(dr["CUSTID"]);
                sOTPVal.OTPEMAIL = NullToSpace(dr["OTPEMAIL"]);
                sOTPVal.OTPMOBILE = NullToSpace(dr["OTPMOBILE"]);
                sOTPVal.OTPGNDATE = NullToSpace(dr["OTPGNDATE"]);
                sOTPVal.OTPGNTIME = NullToSpace(dr["OTPGNTIME"]);
                sOTPVal.OTPATTEMPT = Convert.ToInt32(NullToSpace(dr["OTPATTEMPT"]));
                sOTPVal.OTPRESENDCOUNT = Convert.ToInt32(NullToSpace(dr["OTPRESENDCOUNT"]));
                sOTPVal.OTPSTATUS = NullToSpace(dr["OTPSTATUS"]);
                sOTPVal.EMISBLOCKED = NullToSpace(dr["EMISBLOCKED"]);
                sOTPVal.EMBLOCKDATETIME = NullToSpace(dr["EMBLOCKDATETIME"]);
                sOTPVal.OTPVERIFIEDTIME = NullToSpace(dr["OTPVERIFIEDTIME"]);
                sOTPVal.OTPMODULE = NullToSpace(dr["OTPMODULE"]);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return sOTPVal;
    }

    public void LockOTPInHisTbl(string sCUSID, string OTPModule, string sAction)
    {
        try
        {
            if (!string.IsNullOrEmpty(sCUSID))
            {
                StoreEvent("99999", "", "", "", OTPModule + " NC OTP – initiated the process to lock the OTP details in the history table - " + sAction, sCUSID);

                string fUpdQry = "UPDATE OTP_History SET OTPSTATUS=@OTPLKSTATUS WHERE CUSTID=@CUSID AND OTPSTATUS=@OTPSTATUS AND OTPMODULE=@OTPMODULE ";
                ExecuteCommandWthParam(fUpdQry,
                    new SqlParameter("@CUSID", sCUSID),
                    new SqlParameter("@OTPLKSTATUS", "L"),
                    new SqlParameter("@OTPSTATUS", "Y"),
                    new SqlParameter("@OTPMODULE", OTPModule)
                    );

                StoreEvent("99999", "", "", "", OTPModule + " NC OTP – completed the process to lock the OTP details in the history table - " + sAction, sCUSID);
            }
        }
        catch (Exception ex)
        {
            StoreEvent("99999", "", "", "", OTPModule + " NC OTP – failed the process to lock the OTP details in the history table - " + sAction, sCUSID);
            throw ex;
        }
    }

    public void BlockCustAccOTP(string sRefno, string OTPModule, string sAction)
    {
        try
        {
            if (!string.IsNullOrEmpty(sRefno))
            {
                StoreEvent("99999", "", "", "", OTPModule + " NC OTP – initiated the process to block the customer account - " + sAction, sRefno);

                string fUpdQry = "UPDATE ACOPCUST SET CUSAAC=@CUSAAC,CUSBLOCKDATETIME=@CUSBLOCKDATETIME WHERE TRREF=@CUSUID";
                ExecuteCommandWthParam(fUpdQry,
                    new SqlParameter("@CUSUID", sRefno),
                    new SqlParameter("@CUSAAC", "B"),
                    new SqlParameter("@CUSBLOCKDATETIME", DateTime.Now.ToString("yyyyMMddHHmm")));  

                StoreEvent("99999", "", "", "", OTPModule + " NC OTP – successfully completed the process to block the customer account - " + sAction, sRefno);
            }
        }
        catch (Exception ex)
        {
            StoreEvent("99999", "", "", "", OTPModule + " NC OTP – failed to block the customer account - " + sAction, sRefno);
            throw ex;
        }
    }

    public void UpdOTPAttCnt(int OTPAttCnt, string sCUSID, string OTPModule)
    {
        try
        {
            if (!string.IsNullOrEmpty(sCUSID))
            {
                StoreEvent("99999", "", "", "", OTPModule + " NC OTP – Initiated the OTP count update process.", sCUSID);

                string fUpdQry = "UPDATE OTP_History SET OTPATTEMPT=@OTPATTEMPT WHERE CUSTID=@CUSID AND OTPSTATUS=@OTPSTATUS AND OTPMODULE=@OTPMODULE";
                ExecuteCommandWthParam(fUpdQry,
                    new SqlParameter("@OTPATTEMPT", OTPAttCnt),
                    new SqlParameter("@CUSID", sCUSID),
                    new SqlParameter("@OTPSTATUS", "Y"),
                    new SqlParameter("@OTPMODULE", OTPModule)
                    );

                StoreEvent("99999", "", "", "", OTPModule + " NC OTP – Successfully completed the OTP count update process.", sCUSID);
            }
        }
        catch (Exception ex)
        {
            StoreEvent("99999", "", "", "", OTPModule + " NC OTP – Failed to update the OTP count.", sCUSID);
            throw ex;
        }
    }

    public bool checkOTPExp(string OTPGNTIME, string OTP, string OTPModule, string sCUSTID)
    {
        bool flag = true;
        int OTPTimeDif = -1;
        try
        {
            if (!string.IsNullOrEmpty(OTPGNTIME))
            {
                DateTime GNTIME = DateTime.ParseExact(OTPGNTIME, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                DateTime CRTIME = DateTime.ParseExact(DateTime.Now.ToString("yyyyMMddHHmm"), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                OTPTimeDif = (int)(CRTIME - GNTIME).TotalMinutes;
            }
            string sOTPValidMin = RetPolValue("OTP", "OTPValTim");
            int OTPValTime = Convert.ToInt32(sOTPValidMin);
            if (string.IsNullOrEmpty(OTP) || OTPTimeDif < 0 || OTPTimeDif > OTPValTime)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
        }
        catch (Exception ex)
        {
            StoreEvent("99999", "", "", "", OTPModule + " NC OTP – process failed to check the OTP expiry.", sCUSTID);
        }
        return flag;
    }

    public void UpdateOTPVerSuccSts(string sCUSID, string OTPModule)
    {
        try
        {
            if (!string.IsNullOrEmpty(sCUSID))
            {
                StoreEvent("99999", "", "", "", OTPModule + " NC OTP – Initiated to update the OTP success status.", sCUSID);

                string fUpdQry = "UPDATE OTP_History SET OTPSTATUS=@OTPSUCCSTS,OTPVERIFIEDTIME=@OTPVERIFIEDTIME WHERE CUSTID=@CUSID AND OTPSTATUS=@OTPSTATUS AND OTPMODULE=@OTPMODULE";
                ExecuteCommandWthParam(fUpdQry,
                    new SqlParameter("@OTPSUCCSTS", "S"),
                    new SqlParameter("@OTPVERIFIEDTIME", DateTime.Now.ToString("yyyyMMddHHmm")),
                    new SqlParameter("@CUSID", sCUSID),
                    new SqlParameter("@OTPSTATUS", "Y"),
                    new SqlParameter("@OTPMODULE", OTPModule)
                    );

                StoreEvent("99999", "", "", "", OTPModule + " NC OTP – completed to update the OTP success status.", sCUSID);
            }
        }
        catch (Exception ex)
        {
            StoreEvent("99999", "", "", "", OTPModule + " NC OTP – Failed to update the OTP success status.", sCUSID);
            throw ex;
        }
    }

    public void UpdOTPResendCnt(int sOTPResendCntVal, string sCUSID, string OTPModule)
    {
        try
        {
            if (!string.IsNullOrEmpty(sCUSID))
            {
                StoreEvent("99999", "", "", "", OTPModule + " NC OTP – Initiated the OTP resend count update process.", sCUSID);

                string fUpdQry = "UPDATE OTP_History SET OTPRESENDCOUNT=@OTPRESENDCOUNT WHERE CUSTID=@CUSID AND OTPSTATUS=@OTPSTATUS AND OTPMODULE=@OTPMODULE";
                ExecuteCommandWthParam(fUpdQry,
                    new SqlParameter("@OTPRESENDCOUNT", sOTPResendCntVal),
                    new SqlParameter("@CUSID", sCUSID),
                    new SqlParameter("@OTPSTATUS", "Y"),
                    new SqlParameter("@OTPMODULE", OTPModule)
                    );

                StoreEvent("99999", "", "", "", OTPModule + " NC OTP – Successfully completed the OTP resend count update process.", sCUSID);
            }
        }
        catch (Exception ex)
        {
            StoreEvent("99999", "", "", "", OTPModule + " NC OTP – Failed to update the OTP resend count.", sCUSID);
            throw ex;
        }
    }

    //End
}

