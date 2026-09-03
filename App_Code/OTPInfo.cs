using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OTPInfo
/// </summary>
public class OTPInfo
{
    public string OTP { get; set; }
    public string CUSTID { get; set; }
    public string OTPEMAIL { get; set; }
    public string OTPMOBILE { get; set; }
    public string OTPGNDATE { get; set; }
    public string OTPGNTIME { get; set; }
    public int OTPATTEMPT { get; set; }
    public int OTPRESENDCOUNT { get; set; }
    public string OTPSTATUS { get; set; }
    public string EMISBLOCKED { get; set; }
    public string EMBLOCKDATETIME { get; set; }
    public string OTPVERIFIEDTIME { get; set; }
    public string OTPMODULE { get; set; }
}