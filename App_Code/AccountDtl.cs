using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Resident
/// </summary>
public class AccountDtl : Applicant
{
    System.Globalization.CultureInfo provider = new System.Globalization.CultureInfo("en-GB");
    Methods obj = new Methods();

    public AccountDtl()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private string _ReferenceNo;
    private string _RegUniqueId;
    private string _IsJntAc;
    private int _NoOfApplicants;
    private string _IsJntSameAdd;

    private string _InvAmount;
    private string _InvPeriod;
    private string _InvRateOfInt;
    private string _OthBankName;
    private string _OthBankSC;
    private string _OthBankAcNo;
    private string _RepayIns;

    private string _Status;
    private string _AppStatus;
    private string _CreatedDt;
    private string _CreatedTime;
    private string _CreatedBy;
    private string _ModifiedDt;
    private string _ModifiedTime;
    private string _ModifiedBy;

    private string _Mrktngcont;
    private string _FSCSCont;
    private string _CheckTnC;

    public string Mrktngcont 
    {
        get
        {
            if (_Mrktngcont == null)
            {
                _Mrktngcont = string.Empty;
            }
            return _Mrktngcont;
        }
        set
        {
            _Mrktngcont = value;
        }
    }

    public string FSCSCont 
    {
        get
        {
            if (_FSCSCont == null)
            {
                _FSCSCont = string.Empty;
            }
            return _FSCSCont;
        }
        set
        {
            _FSCSCont = value;
        }
    }

    public string CheckTnC
    {
        get
        {
            if (_CheckTnC == null)
            {
                _CheckTnC = string.Empty;
            }
            return _CheckTnC;
        }
        set
        {
            _CheckTnC = value;
        }
    }

    public string ReferenceNo
    {
        get
        {
            if (_ReferenceNo == null)
            {
                _ReferenceNo = string.Empty;
            }
            return _ReferenceNo;
        }
        set
        {
            _ReferenceNo = value;
        }
    }

    public string RegUniqueId
    {
        get
        {
            if (_RegUniqueId == null)
            {
                _RegUniqueId = string.Empty;
            }
            return _RegUniqueId;
        }
        set
        {
            _RegUniqueId = value;
        }
    }

    public string IsJntAc
    {
        get
        {
            if ((_IsJntAc == null) || (_IsJntAc.Trim().Length == 0))
            {
                _IsJntAc = "No";
            }
            return _IsJntAc;
        }
        set
        {
            _IsJntAc = value;
        }
    }

    public int NoOfApplicants
    {
        get
        {
            if (_NoOfApplicants == null)
            {
                _NoOfApplicants = 0;
            }
            return _NoOfApplicants;
        }
        set
        {
            _NoOfApplicants = value;
        }
    }

    public string IsJntSameAdd
    {
        get
        {
            if (_IsJntSameAdd == null)
            {
                _IsJntSameAdd = string.Empty;
            }
            return _IsJntSameAdd;
        }

        set
        {
            _IsJntSameAdd = value;
        }
    }

    public string InvAmount
    {
        get
        {
            if (_InvAmount == null)
            {
                _InvAmount = string.Empty;
            }
            return _InvAmount;
        }
        set
        {
            _InvAmount = value;
        }
    }

    public string InvPeriod
    {
        get
        {
            if (_InvPeriod == null)
            {
                _InvPeriod = string.Empty;
            }
            return _InvPeriod;
        }
        set
        {
            _InvPeriod = value;
        }
    }

    public string InvRateOfInt
    {
        get
        {
            if (_InvRateOfInt == null)
            {
                _InvRateOfInt = string.Empty;
            }
            return _InvRateOfInt;
        }
        set
        {
            _InvRateOfInt = value;
        }
    }

    public string OthBankName
    {
        get
        {
            if (_OthBankName == null)
            {
                _OthBankName = string.Empty;
            }
            return _OthBankName;
        }
        set
        {
            _OthBankName = value;
        }
    }

    public string OthBankSC
    {
        get
        {
            if (_OthBankSC == null)
            {
                _OthBankSC = string.Empty;
            }
            return _OthBankSC;
        }
        set
        {
            _OthBankSC = value;
        }
    }

    public string OthBankAcNo
    {
        get
        {
            if (_OthBankAcNo == null)
            {
                _OthBankAcNo = string.Empty;
            }
            return _OthBankAcNo;
        }
        set
        {
            _OthBankAcNo = value;
        }
    }

    public string RepayIns
    {
        get
        {
            if (_RepayIns == null)
            {
                _RepayIns = string.Empty;
            }
            return _RepayIns;
        }
        set
        {
            _RepayIns = value;
        }
    }

    public string Status
    {
        get
        {
            if ((_Status == null) || (_Status.Trim().Length == 0))
            {
                _Status = "KYP";
            }
            return _Status;
        }
        set
        {
            _Status = value;
        }
    }

    public string AppStatus
    {
        get
        {
            if ((_AppStatus == null) || (_AppStatus.Trim().Length == 0))
            {
                _AppStatus = string.Empty;
            }
            return _AppStatus;
        }
        set
        {
            _AppStatus = value;
        }
    }

    public string CreatedDt
    {
        get
        {
            if ((_CreatedDt == null) || (_CreatedDt.Trim().Length == 0))
            {
                _CreatedDt = string.Empty;
            }
            return _CreatedDt;
        }
        set
        {
            _CreatedDt = value;
        }
    }

    public string CreatedTime
    {
        get
        {
            if ((_CreatedTime == null) || (_CreatedTime.Trim().Length == 0))
            {
                _CreatedTime = string.Empty;
            }
            return _CreatedTime;
        }
        set
        {
            _CreatedTime = value;
        }
    }

    public string CreatedBy
    {
        get
        {
            if ((_CreatedBy == null) || (_CreatedBy.Trim().Length == 0))
            {
                _CreatedBy = string.Empty;
            }
            return _CreatedBy;
        }
        set
        {
            _CreatedBy = value;
        }
    }

    public string ModifiedDt
    {
        get
        {
            if ((_ModifiedDt == null) || (_ModifiedDt.Trim().Length == 0))
            {
                _ModifiedDt = string.Empty;
            }
            return _ModifiedDt;
        }
        set
        {
            _ModifiedDt = value;
        }
    }

    public string ModifiedTime
    {
        get
        {
            if ((_ModifiedTime == null) || (_ModifiedTime.Trim().Length == 0))
            {
                _ModifiedTime = string.Empty;
            }
            return _ModifiedTime;
        }
        set
        {
            _ModifiedTime = value;
        }
    }

    public string ModifiedBy
    {
        get
        {
            if ((_ModifiedBy == null) || (_ModifiedBy.Trim().Length == 0))
            {
                _ModifiedBy = string.Empty;
            }
            return _ModifiedBy;
        }
        set
        {
            _ModifiedBy = value;
        }
    }

    public List<Applicant> appList { get; set; }

    public List<AccountDtl> GetAccountDtlList(DateTime sFromDate, DateTime sToDate, string sRefNo)
    {
        string sQuery = string.Empty;
        string sFields = string.Empty;
        DataTable dtGrid = null;

        List<AccountDtl> lstAcDtl = null;

        try
        {
            sFields = "ACID,REFNO,RESIDENTSTS,BRANCH,CASE [ACTYPE] WHEN 'Other' THEN ACTYPEOTH Else ACTYPE END AS [ACTYPE],CURRENCY,ISJNTAC";

            sFields = sFields + ", '" + sFromDate + "' FromDt, '" + sToDate + "' ToDt, STATUS";

            sQuery = " SELECT " + sFields + " FROM ACDTLPF WHERE CREDATE between '" + obj.DTOC(sFromDate.ToString()) + "' and '" + obj.DTOC(sToDate.ToString()) + "' ";

            if (sRefNo.Length > 0)
            {
                sQuery += " and refno like '%" + sRefNo + "%'";
            }

            dtGrid = obj.ExecuteData(sQuery + " ORDER BY ACID DESC");

            if (dtGrid != null && dtGrid.Rows.Count > 0)
            {
                lstAcDtl = new List<AccountDtl>();

                //foreach (DataRow dr in dtGrid.Rows)
                //{
                //    AccountDtl AcDtl = new AccountDtl();
                //    AcDtl.AcId = obj.NullToSpace(dr["ACID"]);

                //    AcDtl.RegUniqueId = obj.NullToSpace(dr["REFNO"]);
                //    //20141013
                //    AcDtl.ReferenceNo = obj.NullToSpace(dr["REFNO"]);
                //    //
                //    AcDtl.ResidentSts = obj.NullToSpace(dr["RESIDENTSTS"]);
                //    AcDtl.Branch = obj.NullToSpace(dr["BRANCH"]);
                //    AcDtl.Currency = obj.NullToSpace(dr["CURRENCY"]);

                //    AcDtl.AcType = obj.NullToSpace(dr["ACTYPE"]);
                //    if (obj.NullToSpace(dr["ACTYPE"]).ToUpper() == "Other".ToUpper())
                //    {
                //        AcDtl.AcTypeOth = obj.NullToSpace(dr["ACTYPEOTH"]);
                //    }

                //    AcDtl.Purpose = obj.NullToSpace(dr["PURPOSE"]);
                //    if (obj.NullToSpace(dr["PURPOSE"]).ToUpper() == "Other".ToUpper())
                //    {
                //        AcDtl.PurposeOth = obj.NullToSpace(dr["PURPOSEOTH"]);
                //    }

                //    AcDtl.HowToKnow = obj.NullToSpace(dr["KNOWPNBFROM"]);
                //    //if (AcDtl.HowToKnow == "PNBIL Customer" | AcDtl.HowToKnow == "Radio" | AcDtl.HowToKnow == "Contact Of Staff" | AcDtl.HowToKnow == "Other")
                //    if (AcDtl.HowToKnow == "PNBIL Customer" | AcDtl.HowToKnow == "Contact Of Staff" | AcDtl.HowToKnow == "Other")
                //    {
                //        AcDtl.HowToKnowOth = obj.NullToSpace(dr["KNOWPNBFROMOTH"]);
                //    }

                //    AcDtl.ChkBook = obj.NullToSpace(dr["CHKBOOK"]);
                //    AcDtl.StmtFreq = obj.NullToSpace(dr["STMTFREQ"]);

                //    if (obj.NullToSpace(dr["OTHBNKAC"]).ToUpper() == "Yes".ToUpper())
                //    {
                //        AcDtl.OthBnkAc = "Yes";
                //        AcDtl.OthBnkNm1 = obj.NullToSpace(dr["OTHBNKNM1"]);
                //        AcDtl.OthBnkNm2 = obj.NullToSpace(dr["OTHBNKNM2"]);
                //    }
                //    else
                //    {
                //        AcDtl.OthBnkAc = "No";
                //        AcDtl.OthBnkNm1 = string.Empty;
                //        AcDtl.OthBnkNm2 = string.Empty;
                //    }

                //    AcDtl.RsdntlProp = obj.NullToSpace(dr["RSDNTLPROP"]);

                //    if (obj.NullToSpace(dr["RSDNTLPROP"]).ToUpper() == "Other".ToUpper())
                //    {
                //        AcDtl.RsdntlPropOth = obj.NullToSpace(dr["RSDNTLPROPOTH"]);
                //    }
                //    else
                //    {
                //        AcDtl.RsdntlPropOth = string.Empty;
                //    }

                //    if (obj.NullToSpace(dr["ISANYOPSNGLY"]).ToUpper() == "Yes".ToUpper())
                //        AcDtl.IsAnyOpSngly = "Yes";
                //    else
                //        AcDtl.IsAnyOpSngly = "No";

                //    if (obj.NullToSpace(dr["ISANYOPSNGLY"]).ToUpper() == "Yes".ToUpper())
                //        AcDtl.IsMrkPur = "Yes";
                //    else
                //        AcDtl.IsMrkPur = "No";
                //}
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return lstAcDtl;
    }

    public AccountDtl GetAccountDtl(string sAcId)
    {
        string sQuery = string.Empty;
        string sFields = string.Empty;
        DataTable dt = null;

        AccountDtl AcDtl = null;

        try
        {
            sQuery = "SELECT * FROM ACDTLPF WHERE ACID='" + sAcId + "'";
            dt = obj.ExecuteData(sQuery);

            if (dt != null && dt.Rows.Count > 0)
            {
                //DataRow dr = dt.Rows[0];

                //AcDtl = new AccountDtl();

                //AcDtl.AcId = obj.NullToSpace(dr["ACID"]);
                //AcDtl.RegUniqueId = obj.NullToSpace(dr["REFNO"]);
                ////20141013
                //AcDtl.ReferenceNo = obj.NullToSpace(dr["REFNO"]);
                ////
                //AcDtl.Branch = obj.NullToSpace(dr["BRANCH"]);
                //AcDtl.Currency = obj.NullToSpace(dr["CURRENCY"]);
                //AcDtl.AcType = obj.NullToSpace(dr["ACTYPE"]);
                //AcDtl.AcTypeOth = obj.NullToSpace(dr["ACTYPEOTH"]);
                //AcDtl.Purpose = obj.NullToSpace(dr["PURPOSE"]);
                //AcDtl.PurposeOth = obj.NullToSpace(dr["PURPOSEOTH"]);
                //AcDtl.ResidentSts = obj.NullToSpace(dr["RESIDENTSTS"]);
                //AcDtl.ResidentSts = obj.NullToSpace(dr["OTHBNKAC"]);
                //AcDtl.OthBnkNm1 = obj.NullToSpace(dr["OTHBNKNM1"]);
                //AcDtl.OthBnkNm2 = obj.NullToSpace(dr["OTHBNKNM2"]);
                //AcDtl.HowToKnow = obj.NullToSpace(dr["KNOWPNBFROM"]);
                //AcDtl.HowToKnowOth = obj.NullToSpace(dr["KNOWPNBFROMOTH"]);
                //AcDtl.ChkBook = obj.NullToSpace(dr["CHKBOOK"]);
                //AcDtl.StmtFreq = obj.NullToSpace(dr["STMTFREQ"]);
                //AcDtl.RsdntlProp = obj.NullToSpace(dr["RSDNTLPROP"]);
                //AcDtl.RsdntlPropOth = obj.NullToSpace(dr["RSDNTLPROPOTH"]);
                //AcDtl.IsJntAc = obj.NullToSpace(dr["ISJNTAC"]);
                //AcDtl.IsAnyOpSngly = obj.NullToSpace(dr["ISANYOPSNGLY"]);
                //AcDtl.IsMrkPur = obj.NullToSpace(dr["ISMRKPURPOSE"]);
                //AcDtl.IsNotOrdRes = obj.NullToSpace(dr["ISNOTORDNRYRES"]);
                //AcDtl.CreatedDt = obj.NullToSpace(dr["CREDATE"]);
                //AcDtl.CreatedTime = obj.NullToSpace(dr["CRETIME"]);
                //AcDtl.CreatedBy = obj.NullToSpace(dr["CREATEDBY"]);
                //AcDtl.ModifiedDt = obj.NullToSpace(dr["MODDATE"]);
                //AcDtl.ModifiedTime = obj.NullToSpace(dr["MODTIME"]);
                //AcDtl.ModifiedBy = obj.NullToSpace(dr["MODIFIEDBY"]);
                //AcDtl.Status = obj.NullToSpace(dr["STATUS"]);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return AcDtl;
    }

    public List<Applicant> GetApplicants(string sAcId)
    {
        string sQuery = string.Empty;
        string sFields = string.Empty;
        DataTable dt = null;

        Applicant oAplcnt = null;
        List<Applicant> lstApplicant = null;
        try
        {
            sQuery = "SELECT * FROM APDTLPF WHERE ACID='" + sAcId + "' ORDER BY SEQUENCE ASC";
            dt = obj.ExecuteData(sQuery);

            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    lstApplicant = new List<Applicant>();

            //    foreach (DataRow dr in dt.Rows)
            //    {

            //        oAplcnt = new Applicant();

            //        oAplcnt = new Applicant();
            //        oAplcnt.RegUniqueId = obj.NullToSpace(dr["REFNO"]);
            //        //20141013
            //        oAplcnt.ReferenceNo = obj.NullToSpace(dr["REFNO"]);
            //        //
            //        oAplcnt.Sequence = Convert.ToInt32(obj.NullToSpace(dr["SEQUENCE"]));

            //        //Personal Details        
            //        oAplcnt.Title = obj.NullToSpace(dr["TITLE"]);
            //        oAplcnt.FirstNm = obj.NullToSpace(dr["FIRSTNM"]);
            //        oAplcnt.MidNm = obj.NullToSpace(dr["MIDNM"]);
            //        oAplcnt.SurNm = obj.NullToSpace(dr["SURNM"]);
            //        oAplcnt.Gender = obj.NullToSpace(dr["GENDER"]);
            //        oAplcnt.DOB = obj.NullToSpace(dr["DOB"]);
            //        oAplcnt.Citizenship = obj.NullToSpace(dr["CITIZENSHIP"]);
            //        oAplcnt.MaritalSts = obj.NullToSpace(dr["MARITALSTS"]);

            //        if (oAplcnt.MaritalSts.ToUpper() == "Other")
            //        {
            //            oAplcnt.MaritalStsOth = obj.NullToSpace(dr["MARITALSTSOTH"]);
            //        }
            //        //Contact Details        
            //        oAplcnt.HomeTelNo = obj.NullToSpace(dr["HOMETELNO"]);
            //        oAplcnt.MobileNo = obj.NullToSpace(dr["MOBILENO"]);
            //        oAplcnt.EmailAddr = obj.NullToSpace(dr["EMAILADDR"]);

            //        //Requirement Details        
            //        oAplcnt.ChkBk = string.Empty;
            //        oAplcnt.DbtCrd = obj.NullToSpace(dr["DBTCRD"]);
            //        if (oAplcnt.DbtCrd.ToUpper() == "Yes".ToUpper())
            //        {
            //            oAplcnt.AcsCd = obj.NullToSpace(dr["ACSCD"]);
            //            oAplcnt.DsplyNm = obj.NullToSpace(dr["DSPLYNM"]);
            //            oAplcnt.MemWord = obj.NullToSpace(dr["MEMWORD"]);
            //        }
            //        oAplcnt.NetBnkAcs = obj.NullToSpace(dr["NETBNKACS"]);
            //        oAplcnt.StmtFrq = string.Empty;

            //        //Employment details
            //        oAplcnt.EmpDtls = obj.NullToSpace(dr["EMPDTLS"]);

            //        if (oAplcnt.EmpDtls.ToUpper() == "Others".ToUpper())
            //        {
            //            oAplcnt.EmpDtlsOth = obj.NullToSpace(dr["EMPDTLSOTH"]);
            //        }

            //        oAplcnt.NatOfEmpBus = obj.NullToSpace(dr["NATOFEMPBUS"]);
            //        oAplcnt.NmOfEmpBus = obj.NullToSpace(dr["NMOFEMPBUS"]);
            //        oAplcnt.EmpAddr1 = obj.NullToSpace(dr["EMPADDR1"]);
            //        oAplcnt.EmpAddr2 = obj.NullToSpace(dr["EMPADDR2"]);
            //        oAplcnt.EmpAddr3 = obj.NullToSpace(dr["EMPADDR3"]);
            //        oAplcnt.EmpPstCd = obj.NullToSpace(dr["EMPPSTCD"]);
            //        oAplcnt.EmpTelNo = obj.NullToSpace(dr["EMPTELNO"]);
            //        oAplcnt.EmpSince = obj.NullToSpace(dr["EMPSINCE"]);
            //        oAplcnt.EmpAnlIncm = obj.NullToSpace(dr["EMPANLINCM"]);
            //        oAplcnt.EmpDpndts = obj.NullToSpace(dr["EMPDPNDTS"]);

            //        //Current Address
            //        oAplcnt.CurAddr1 = obj.NullToSpace(dr["CURADDR1"]);
            //        oAplcnt.CurAddr2 = obj.NullToSpace(dr["CURADDR2"]);
            //        oAplcnt.CurAddr3 = obj.NullToSpace(dr["CURADDR3"]);
            //        oAplcnt.CurPcode = obj.NullToSpace(dr["CURPCODE"]);
            //        oAplcnt.CurCntry = obj.NullToSpace(dr["CURCNTRY"]);
            //        oAplcnt.ResidingSince = obj.NullToSpace(dr["RESIDINGSINCE"]);

            //        oAplcnt.MailingAddr = obj.NullToSpace(dr["MAILINGADDR"]);

            //        DateTime newDt;
            //        if (DateTime.TryParse(oAplcnt.ResidingSince, out newDt))
            //        {
            //            double dDiff = (Convert.ToDateTime(newDt, provider) - Convert.ToDateTime(DateTime.Today.AddYears(-3), provider)).TotalDays;
            //            if (dDiff > 0)
            //            {
            //                oAplcnt.PreAddr1 = obj.NullToSpace(dr["PREADDR1"]);
            //                oAplcnt.PreAddr2 = obj.NullToSpace(dr["PREADDR2"]);
            //                oAplcnt.PreAddr3 = obj.NullToSpace(dr["PREADDR3"]);
            //                oAplcnt.PrePcode = obj.NullToSpace(dr["PREPCODE"]);
            //                oAplcnt.PreCntry = obj.NullToSpace(dr["PRECNTRY"]);
            //            }
            //        }

            //        if (oAplcnt.MailingAddr.ToUpper() == "Differ".ToUpper())
            //        {
            //            oAplcnt.MailAddr1 = obj.NullToSpace(dr["MAILADDR1"]);
            //            oAplcnt.MailAddr2 = obj.NullToSpace(dr["MAILADDR2"]);
            //            oAplcnt.MailAddr3 = obj.NullToSpace(dr["MAILADDR3"]);
            //            oAplcnt.MailPcode = obj.NullToSpace(dr["MAILPCODE"]);
            //            oAplcnt.MailCntry = obj.NullToSpace(dr["MAILCNTRY"]);
            //        }

            //        lstApplicant.Add(oAplcnt);
            //    }
            //}
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return lstApplicant;
    }

    public bool SaveAccount(AccountDtl objAcDtl)
    {
        bool bStatus = false;
        string sQuery = string.Empty;
        string sValues = string.Empty;
        string sAcId = string.Empty;

        string sAcStatus = string.Empty;
        DataTable dtAcDtl = null;

        try
        {
         //   sQuery = " INSERT INTO objAcDtlTLPF (" +
         //" REFNO,BRANCH,CURRENCY,ACTYPE,ACTYPEOTH," +
         //" PURPOSE,PURPOSEOTH,RESIDENTSTS,OTHBNKAC,OTHBNKNM1," +
         //" OTHBNKNM2,KNOWPNBFROM,KNOWPNBFROMOTH,CHKBOOK,STMTFREQ," +
         //" RSDNTLPROP,RSDNTLPROPOTH,ISJNTAC,ISANYOPSNGLY,ISMRKPURPOSE," +
         //" [STATUS],[CREDATE],[CRETIME],[CREATEDBY])";

         //   sValues = " SELECT '" + objAcDtl.RegUniqueId + "','" + objAcDtl.Branch + "','" + objAcDtl.Currency + "','" + objAcDtl.AcType + "','" + objAcDtl.AcTypeOth + "',";
         //   sValues += "'" + objAcDtl.Purpose + "','" + objAcDtl.PurposeOth + "','" + objAcDtl.ResidentSts + "','" + objAcDtl.OthBnkAc + "','" + objAcDtl.OthBnkNm1 + "',";
         //   sValues += "'" + objAcDtl.OthBnkNm2 + "','" + objAcDtl.HowToKnow + "','" + objAcDtl.HowToKnowOth + "','" + objAcDtl.ChkBook + "','" + objAcDtl.StmtFreq + "',";
         //   sValues += "'" + objAcDtl.RsdntlProp + "','" + objAcDtl.RsdntlPropOth + "','" + objAcDtl.IsJntAc + "','" + objAcDtl.IsAnyOpSngly + "','" + objAcDtl.IsMrkPur + "',";
         //   sValues += "'" + objAcDtl.Status + "','" + objAcDtl.CreatedDt + "','" + CreatedTime + "','" + CreatedBy + "'";
         //   obj.ExecuteCommand(sQuery + sValues);

         //   dtAcDtl = obj.ExecuteData("SELECT ACID,REFNO FROM ACDTLPF WHERE REFNO='" + objAcDtl.RegUniqueId + "'");
         //   if ((dtAcDtl != null) && (dtAcDtl.Rows.Count > 0))
         //   {
         //       if (dtAcDtl.Rows[0]["REFNO"].ToString().ToUpper() == objAcDtl.RegUniqueId.ToUpper())
         //       {
         //           sAcId = dtAcDtl.Rows[0]["ACID"].ToString();
         //           bStatus = true;
         //           if (SaveApplicant(objAcDtl, sAcId))
         //           {
         //               bStatus = true;
         //           }
         //           else
         //           {
         //               bStatus = false;
         //           }

         //       }
         //   }

        }
        catch (Exception ex)
        {
            throw ex;
        }

        return bStatus;
    }

    public bool UpdateAcDtl(AccountDtl AcD)
    {
        bool bStatus = false;
        string sQuery = string.Empty;
        string sAcId = string.Empty;

        try
        {
            //sQuery = " UPDATE ACDTLPF SET" +
            //         " BRANCH='" + AcD.Branch + "',CURRENCY='" + AcD.Currency + "',ACTYPE='" + AcD.AcType + "',ACTYPEOTH='" + AcD.AcTypeOth + "'," +
            //         " PURPOSE='" + AcD.Purpose + "',PURPOSEOTH='" + AcD.PurposeOth + "',RESIDENTSTS='" + AcD.ResidentSts + "',OTHBNKAC='" + AcD.OthBnkAc + "',OTHBNKNM1='" + AcD.OthBnkNm1 + "'," +
            //         " OTHBNKNM2='" + AcD.OthBnkNm2 + "',KNOWPNBFROM='" + AcD.HowToKnow + "',KNOWPNBFROMOTH='" + AcD.HowToKnowOth + "',CHKBOOK='" + AcD.ChkBook + "',STMTFREQ='" + AcD.StmtFreq + "'," +
            //         " RSDNTLPROP='" + AcD.RsdntlProp + "',RSDNTLPROPOTH='" + AcD.RsdntlPropOth + "',ISJNTAC='" + AcD.IsJntAc + "',ISANYOPSNGLY='" + AcD.IsAnyOpSngly + "',ISMRKPURPOSE='" + AcD.IsMrkPur + "'," +
            //         " [STATUS]='',[MODDATE]='" + AcD.ModifiedDt + "',[MODTIME]='" + AcD.ModifiedTime + "',[MODIFIEDBY]='" + AcD.ModifiedBy + "'" +
            //         " WHERE ACID='" + AcD.AcId + "' AND REFNO='" + AcD.RegUniqueId + "'";


            //obj.ExecuteCommand(sQuery);

            //sQuery = "DELETE FROM APDTLPF WHERE ACID='" + AcD.AcId + "' AND REFNO='" + AcD.RegUniqueId + "'";
            //obj.ExecuteCommand(sQuery);

            //if (SaveApplicant(AcD, AcD.AcId))
            //{
            //    bStatus = true;
            //}
            //else
            //{
            //    bStatus = false;
            //}

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bStatus;
    }

    public bool SaveApplicant(AccountDtl AcDtl, string AcId)
    {
        bool bStatus = false;
        string sQuery = string.Empty;
        DataTable dtApDtl = null;
        List<Applicant> lstAplcnt = null;
        StringBuilder aplQry = new StringBuilder("");

        try
        {
            //lstAplcnt = AcDtl.appList;

            //if (lstAplcnt != null && lstAplcnt.Count > 0)
            //{
            //    aplQry.Append("INSERT INTO APDTLPF (");
            //    aplQry.Append("ACID,REFNO,ISPRIMARY,SEQUENCE,TITLE,");
            //    aplQry.Append("FIRSTNM,MIDNM,SURNM,GENDER,IDENTDTLS,");
            //    aplQry.Append("ISUKPSPRT,PSPORTISDT,PSPORTEXDT,PSPORTISCNTRY,IDENTITYNO,CITIZENSHIP,");
            //    aplQry.Append("MARITALSTS,MARITALSTSOTH,DOB,HOMETELNO,MOBILENO,");
            //    aplQry.Append("EMAILADDR,CURADDR1,CURADDR2,CURADDR3,CURPCODE,");
            //    aplQry.Append("CURCNTRY,RESIDINGSINCE,MAILINGADDR,PREADDR1,PREADDR2,");
            //    aplQry.Append("PREADDR3,PREPCODE,PRECNTRY,MAILADDR1,MAILADDR2,");
            //    aplQry.Append("MAILADDR3,MAILPCODE,MAILCNTRY,CHKBK,DBTCRD,");
            //    aplQry.Append("ACSCD,DSPLYNM,MEMWORD,NETBNKACS,STMTFRQ,");
            //    aplQry.Append("EMPDTLS,EMPDTLSOTH,NATOFEMPBUS,NMOFEMPBUS,EMPADDR1,");
            //    aplQry.Append("EMPADDR2,EMPADDR3,EMPPSTCD,EMPTELNO,EMPSINCE,");
            //    aplQry.Append("EMPANLINCM,EMPDPNDTS,SCORE,[STATUS])");

            //    int i = 0;
            //    foreach (Applicant ApD in lstAplcnt)
            //    {
            //        i += 1;
            //        if (i == 1)
            //            aplQry.Append(" SELECT '" + AcId + "','" + AcDtl.RegUniqueId + "','1','" + Convert.ToString(i) + "','" + ApD.Title + "',");
            //        else
            //            aplQry.Append(" SELECT '" + AcId + "','" + AcDtl.RegUniqueId + "','0','" + Convert.ToString(i) + "','" + ApD.Title + "',");

            //        aplQry.Append("'" + ApD.FirstNm + "','" + ApD.MidNm + "','" + ApD.SurNm + "','" + ApD.Gender + "','" + ApD.IdenDtls + "',");
            //        aplQry.Append("'" + ApD.IsUkPsPrt + "','" + ApD.PsPrtIssDt + "','" + ApD.PsPrtExpDt + "','" + ApD.PsPrtIsCntry + "','" + ApD.IdenNo + "','" + ApD.Citizenship + "',");
            //        aplQry.Append("'" + ApD.MaritalSts + "','" + ApD.MaritalStsOth + "','" + ApD.DOB + "','" + ApD.HomeTelNo + "','" + ApD.MobileNo + "',");
            //        aplQry.Append("'" + ApD.EmailAddr + "','" + ApD.CurAddr1 + "','" + ApD.CurAddr2 + "','" + ApD.CurAddr3 + "','" + ApD.CurPcode + "',");
            //        aplQry.Append("'" + ApD.CurCntry + "','" + ApD.ResidingSince + "','" + ApD.MailingAddr + "','" + ApD.PreAddr1 + "','" + ApD.PreAddr2 + "',");
            //        aplQry.Append("'" + ApD.PreAddr3 + "','" + ApD.PrePcode + "','" + ApD.PreCntry + "','" + ApD.MailAddr1 + "','" + ApD.MailAddr2 + "',");
            //        aplQry.Append("'" + ApD.MailAddr3 + "','" + ApD.MailPcode + "','" + ApD.MailCntry + "','" + ApD.ChkBk + "','" + ApD.DbtCrd + "',");
            //        aplQry.Append("'" + ApD.AcsCd + "','" + ApD.DsplyNm + "','" + ApD.MemWord + "','" + ApD.NetBnkAcs + "','" + ApD.StmtFrq + "',");
            //        aplQry.Append("'" + ApD.EmpDtls + "','" + ApD.EmpDtlsOth + "','" + ApD.NatOfEmpBus + "','" + ApD.NmOfEmpBus + "','" + ApD.EmpAddr1 + "',");
            //        aplQry.Append("'" + ApD.EmpAddr2 + "','" + ApD.EmpAddr3 + "','" + ApD.EmpPstCd + "','" + ApD.EmpTelNo + "','" + ApD.EmpSince + "',");
            //        aplQry.Append("'" + ApD.EmpAnlIncm + "','" + ApD.Score + "','" + ApD.EmpDpndts + "','' UNION ALL ");
            //    }

            //    if (aplQry.Length > 0)
            //    {
            //        sQuery = aplQry.Remove(aplQry.Length - 10, 10).ToString();
            //        obj.ExecuteCommand(sQuery);

            //        dtApDtl = obj.ExecuteData("SELECT COUNT(ACID) as CNT FROM APDTLPF WHERE ACID='" + AcId + "'");

            //        if ((dtApDtl != null) && (dtApDtl.Rows.Count > 0) && (Convert.ToInt32(dtApDtl.Rows[0]["CNT"]) == lstAplcnt.Count))
            //        {
            //            bStatus = true;
            //        }
            //    }

            //}

            //else
            //{

            //    bStatus = true;
            //}

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bStatus;
    }

}
