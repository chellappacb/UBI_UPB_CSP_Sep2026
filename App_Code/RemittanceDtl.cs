using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RemittanceDtl
/// </summary>
public class RemittanceDtl
{
	public RemittanceDtl()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Private varaiable declaration

    private string _Remid;
    private string _RemRefNo;
    private string _ReferenceNo;
    private string _RemitterID;
    private string _Benid;
    private string _BenTitle;
    private string _BenFirstNm;
    private string _BenMidNm;
    private string _BenSurNm;
    private string _BenStreet;
    private string _BenCity;

    //private string _BenAddr1;
    //private string _BenAddr2;
    //private string _BenAddr3;
    //private string _BenAddr4;

    private string _BenPCode;
    private string _BenTelNo;
    private string _BenMobNo;
    private string _BenDOB;
    private string _BenEmail;
    private string _BenIsHvPNBac;
    private string _BenIsCbsBr;
    private string _BenCbsAcNo;
    private string _BenNonCbsBrNm;
    private string _BenNonCbsAcNo;
    private string _BenOthBnkNm;
    private string _BenOthBnkBrnNm;
    private string _BenOthBnkIfciCd;
    private string _BenOthBnkAcNo;
    private string _BenOthBnkAddr1;
    private string _BenOthBnkAddr2;
    private string _BenOthBnkAddr3;
    private string _BenOthBnkPCode;
    private string _RemCur;
    private decimal _RemAmount;
    private decimal _IssuanceCrg;
    private decimal _PostageCrg;
    private decimal _CashHanCrg;
    private decimal _TotAmount;
    private decimal _ExcngRate;
    private decimal _BenAmount;
    private string _AmtInWords;
    private string _RemPurpose;
    private string _RemPurOthr;
    private string _RemTitle;
    private string _RemFirstNm;
    private string _RemMidNm;
    private string _RemSurNm;

    private string _RemStreet;
    private string _RemAdDrNo;
    private string _RemAddr1;
    private string _RemAddr2;
    private string _RemAddr3;
    
    private string _RemCity;
    private string _RemPCode;
    private string _RemCountry;
    private string _RemTelNo;
    private string _RemMobNo;
    private string _RemDOB;
    private string _RemEmail;
    private string _RemAcNo;
    private string _RemPayType;
    private string _RemPayOther;
    private string _SrcOfFund;
    private string _SrcOfFundOthr;
    private decimal _PayCheques;
    private decimal _PayCash;
    private decimal _PayOth;
    private decimal _PayTot;
    private string _SendSMS;
    private string _CreDate;
    private string _CreTime;
    private string _CreBy;
    private string _ModDate;
    private string _ModTime;
    private string _ModBy;
    private string _RemSts;
    private string _Status;
    
    private string _AuthProvider;
    private string _AuthReqRef;
    private string _AuthID;
    private string _AuthKey;
    private string _AuthScore;
    private string _AuthResult;
    private string _AuthDate;
    private string _AuthTime;
    private string _ReAuthProvider;
    private string _ReAuthReqRef;
    private string _ReAuthID;
    private string _ReAuthKey;
    private string _ReAuthScore;
    private string _ReAuthResult;
    private string _ReAuthDate;
    private string _ReAuthTime;
    private string _IsIdDocRcvd;
    private string _IdDocRcvdDate;
    private string _IdDocRcvdTime;
    private string _KYCstatus;

    private string _ProofOfDocNm;
    private string _ApproveRemarks;
    private string _DocSubmitDate;
    private string _DocSubmitTime;
    private string _DocVerifiedBy;
      
    #endregion

    #region Public varaiable declaration

    public string Remid;
  
    public string RemRefNo
    {
        get
        {
            if (_RemRefNo == null)
            {
                _RemRefNo = string.Empty;
            }
            return _RemRefNo;
        }
        set
        {
            _RemRefNo = value;
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
    public string RemitterID
    {
        get
        {
            if (_RemitterID == null)
            {
                _RemitterID = string.Empty;
            }
            return _RemitterID;
        }
        set
        {
            _RemitterID = value;
        }
    }
    public string Benid;
    public string BenTitle
    {
        get
        {
            if (_BenTitle == null)
            {
                _BenTitle = string.Empty;
            }
            return _BenTitle;
        }
        set
        {
            _BenTitle = value;
        }
    }
    public string BenFirstnm
    {
        get
        {
            if (_BenFirstNm == null)
            {
                _BenFirstNm = string.Empty;
            }
            return _BenFirstNm;
        }
        set
        {
            _BenFirstNm = value;
        }
    }
    public string BenMidNm
    {
        get
        {
            if (_BenMidNm == null)
            {
                _BenMidNm = string.Empty;
            }
            return _BenMidNm;
        }
        set
        {
            _BenMidNm = value;
        }
    }
    public string BenSurNm
    {
        get
        {
            if (_BenSurNm == null)
            {
                _BenSurNm = string.Empty;
            }
            return _BenSurNm;
        }
        set
        {
            _BenSurNm = value;
        }
    }

    public string BenStreet
    {
        get
        {
            if (_BenStreet == null)
            {
                _BenStreet = string.Empty;
            }
            return _BenStreet;
        }
        set
        {
            _BenStreet = value;
        }
    }
    public string BenCity
    {
        get
        {
            if (_BenCity == null)
            {
                _BenCity = string.Empty;
            }
            return _BenCity;
        }
        set
        {
            _BenCity = value;
        }
    }

    //public string BenAddr1
    //{
    //    get
    //    {
    //        if (_BenAddr1 == null)
    //        {
    //            _BenAddr1 = string.Empty;
    //        }
    //        return _BenAddr1;
    //    }
    //    set
    //    {
    //        _BenAddr1 = value;
    //    }
    //}
    //public string BenAddr2
    //{
    //    get
    //    {
    //        if (_BenAddr2 == null)
    //        {
    //            _BenAddr2 = string.Empty;
    //        }
    //        return _BenAddr2;
    //    }
    //    set
    //    {
    //        _BenAddr2 = value;
    //    }
    //}
    //public string BenAddr3
    //{
    //    get
    //    {
    //        if (_BenAddr3 == null)
    //        {
    //            _BenAddr3 = string.Empty;
    //        }
    //        return _BenAddr3;
    //    }
    //    set
    //    {
    //        _BenAddr3 = value;
    //    }
    //}
    //public string BenAddr4
    //{
    //    get
    //    {
    //        if (_BenAddr4 == null)
    //        {
    //            _BenAddr4 = string.Empty;
    //        }
    //        return _BenAddr4;
    //    }
    //    set
    //    {
    //        _BenAddr4 = value;
    //    }
    //}

    public string BenPCode
    {
        get
        {
            if (_BenPCode == null)
            {
                _BenPCode = string.Empty;
            }
            return _BenPCode;
        }
        set
        {
            _BenPCode = value;
        }
    }
    public string BenTelNo
    {
        get
        {
            if (_BenTelNo == null)
            {
                _BenTelNo = string.Empty;
            }
            return _BenTelNo;
        }
        set
        {
            _BenTelNo = value;
        }
    }
    public string BenMobNo
    {
        get
        {
            if (_BenMobNo == null)
            {
                _BenMobNo = string.Empty;
            }
            return _BenMobNo;
        }
        set
        {
            _BenMobNo = value;
        }
    }
    public string BenDOB
    {
        get
        {
            if (_BenDOB == null)
            {
                _BenDOB = string.Empty;
            }
            return _BenDOB;
        }
        set
        {
            _BenDOB = value;
        }
    }
    public string BenEmail
    {
        get
        {
            if (_BenEmail == null)
            {
                _BenEmail = string.Empty;
            }
            return _BenEmail;
        }
        set
        {
            _BenEmail = value;
        }
    }
    public string BenIsHvPNBac
    {
        get
        {
            if (_BenIsHvPNBac == null)
            {
                _BenIsHvPNBac = string.Empty;
            }
            return _BenIsHvPNBac;
        }
        set
        {
            _BenIsHvPNBac = value;
        }
    }
    public string BenIsCbsBr
    {
        get
        {
            if (_BenIsCbsBr == null)
            {
                _BenIsCbsBr = string.Empty;
            }
            return _BenIsCbsBr;
        }
        set
        {
            _BenIsCbsBr = value;
        }
    }
    public string BenCbsAcNo
    {
        get
        {
            if (_BenCbsAcNo == null)
            {
                _BenCbsAcNo = string.Empty;
            }
            return _BenCbsAcNo;
        }
        set
        {
            _BenCbsAcNo = value;
        }
    }
    public string BenNonCbsBrNm
    {
        get
        {
            if (_BenNonCbsBrNm == null)
            {
                _BenNonCbsBrNm = string.Empty;
            }
            return _BenNonCbsBrNm;
        }
        set
        {
            _BenNonCbsBrNm = value;
        }
    }
    public string BenNonCbsAcNo
    {
        get
        {
            if (_BenNonCbsAcNo == null)
            {
                _BenNonCbsAcNo = string.Empty;
            }
            return _BenNonCbsAcNo;
        }
        set
        {
            _BenNonCbsAcNo = value;
        }
    }
    public string BenOthBnkNm
    {
        get
        {
            if (_BenOthBnkNm == null)
            {
                _BenOthBnkNm = string.Empty;
            }
            return _BenOthBnkNm;
        }
        set
        {
            _BenOthBnkNm = value;
        }
    }
    public string BenOthBnkBrnNm
    {
        get
        {
            if (_BenOthBnkBrnNm == null)
            {
                _BenOthBnkBrnNm = string.Empty;
            }
            return _BenOthBnkBrnNm;
        }
        set
        {
            _BenOthBnkBrnNm = value;
        }
    }
    public string BenOthBnkIfciCd
    {
        get
        {
            if (_BenOthBnkIfciCd == null)
            {
                _BenOthBnkIfciCd = string.Empty;
            }
            return _BenOthBnkIfciCd;
        }
        set
        {
            _BenOthBnkIfciCd = value;
        }
    }
    public string BenOthBnkAcNo
    {
        get
        {
            if (_BenOthBnkAcNo == null)
            {
                _BenOthBnkAcNo = string.Empty;
            }
            return _BenOthBnkAcNo;
        }
        set
        {
            _BenOthBnkAcNo = value;
        }
    }
    public string BenOthBnkAddr1
    {
        get
        {
            if (_BenOthBnkAddr1 == null)
            {
                _BenOthBnkAddr1 = string.Empty;
            }
            return _BenOthBnkAddr1;
        }
        set
        {
            _BenOthBnkAddr1 = value;
        }
    }
    public string BenOthBnkAddr2
    {
        get
        {
            if (_BenOthBnkAddr2 == null)
            {
                _BenOthBnkAddr2 = string.Empty;
            }
            return _BenOthBnkAddr2;
        }
        set
        {
            _BenOthBnkAddr2 = value;
        }
    }
    public string BenOthBnkAddr3
    {
        get
        {
            if (_BenOthBnkAddr3 == null)
            {
                _BenOthBnkAddr3 = string.Empty;
            }
            return _BenOthBnkAddr3;
        }
        set
        {
            _BenOthBnkAddr3 = value;
        }
    }
    public string BenOthBnkPCode
    {
        get
        {
            if (_BenOthBnkPCode == null)
            {
                _BenOthBnkPCode = string.Empty;
            }
            return _BenOthBnkPCode;
        }
        set
        {
            _BenOthBnkPCode = value;
        }
    }
    public string RemCur
    {
        get
        {
            if (_RemCur == null)
            {
                _RemCur = string.Empty;
            }
            return _RemCur;
        }
        set
        {
            _RemCur = value;
        }
    }
    public decimal RemAmount
    {
        get
        {
            if (_RemAmount == null)
            {
                _RemAmount = 0;
            }
            return _RemAmount;
        }
        set
        {
            _RemAmount = value;
        }
    }
    public decimal IssuanceCrg
    {
        get
        {
            if (_IssuanceCrg == null)
            {
                _IssuanceCrg = 0;
            }
            return _IssuanceCrg;
        }
        set
        {
            _IssuanceCrg = value;
        }
    }
    public decimal PostageCrg
    {
        get
        {
            if (_PostageCrg == null)
            {
                _PostageCrg = 0;
            }
            return _PostageCrg;
        }
        set
        {
            _PostageCrg = value;
        }
    }
    public decimal CashHanCrg
    {
        get
        {
            if (_CashHanCrg == null)
            {
                _CashHanCrg = 0;
            }
            return _CashHanCrg;
        }
        set
        {
            _CashHanCrg = value;
        }
    }
    public decimal TotAmount
    {
        get
        {
            if (_TotAmount == null)
            {
                _TotAmount = 0;
            }
            return _TotAmount;
        }
        set
        {
            _TotAmount = value;
        }
    }
    public decimal ExcngRate
    {
        get
        {
            if (_ExcngRate == null)
            {
                _ExcngRate = 0;
            }
            return _ExcngRate;
        }
        set
        {
            _ExcngRate = value;
        }
    }
    public decimal BenAmount
    {
        get
        {
            if (_BenAmount == null)
            {
                _BenAmount = 0;
            }
            return _BenAmount;
        }
        set
        {
            _BenAmount = value;
        }
    }
    public string AmtInWords
    {
        get
        {
            if (_AmtInWords == null)
            {
                _AmtInWords = string.Empty;
            }
            return _AmtInWords;
        }
        set
        {
            _AmtInWords = value;
        }
    }
    public string RemPurpose
    {
        get
        {
            if (_RemPurpose == null)
            {
                _RemPurpose = string.Empty;
            }
            return _RemPurpose;
        }
        set
        {
            _RemPurpose = value;
        }
    }
    public string RemPurOthr
    {
        get
        {
            if (_RemPurOthr == null)
            {
                _RemPurOthr = string.Empty;
            }
            return _RemPurOthr;
        }
        set
        {
            _RemPurOthr = value;
        }
    }
    public string RemTitle
    {
        get
        {
            if (_RemTitle == null)
            {
                _RemTitle = string.Empty;
            }
            return _RemTitle;
        }
        set
        {
            _RemTitle = value;
        }
    }
    public string RemFirstNm
    {
        get
        {
            if (_RemFirstNm == null)
            {
                _RemFirstNm = string.Empty;
            }
            return _RemFirstNm;
        }
        set
        {
            _RemFirstNm = value;
        }
    }
    public string RemMidNm
    {
        get
        {
            if (_RemMidNm == null)
            {
                _RemMidNm = string.Empty;
            }
            return _RemMidNm;
        }
        set
        {
            _RemMidNm = value;
        }
    }
    public string RemSurNm
    {
        get
        {
            if (_RemSurNm == null)
            {
                _RemSurNm = string.Empty;
            }
            return _RemSurNm;
        }
        set
        {
            _RemSurNm = value;
        }
    }

    public string RemStreet
    {
        get
        {
            if (_RemStreet == null)
            {
                _RemStreet = string.Empty;
            }
            return _RemStreet;
        }
        set
        {
            _RemStreet = value;
        }
    }

    public string RemAdDrNo
    {
        get
        {
            if (_RemAdDrNo == null)
            {
                _RemAdDrNo = string.Empty;
            }
            return _RemAdDrNo;
        }
        set
        {
            _RemAdDrNo = value;
        }
    }
    public string RemAddr1
    {
        get
        {
            if (_RemAddr1 == null)
            {
                _RemAddr1 = string.Empty;
            }
            return _RemAddr1;
        }
        set
        {
            _RemAddr1 = value;
        }
    }
    public string RemAddr2
    {
        get
        {
            if (_RemAddr2 == null)
            {
                _RemAddr2 = string.Empty;
            }
            return _RemAddr2;
        }
        set
        {
            _RemAddr2 = value;
        }
    }
    public string RemAddr3
    {
        get
        {
            if (_RemAddr3 == null)
            {
                _RemAddr3 = string.Empty;
            }
            return _RemAddr3;
        }
        set
        {
            _RemAddr3 = value;
        }
    }

    public string RemCity
    {
        get
        {
            if (_RemCity == null)
            {
                _RemCity = string.Empty;
            }
            return _RemCity;
        }
        set
        {
            _RemCity = value;
        }
    }
    public string RemPCode
    {
        get
        {
            if (_RemPCode == null)
            {
                _RemPCode = string.Empty;
            }
            return _RemPCode;
        }
        set
        {
            _RemPCode = value;
        }
    }
    public string RemCountry
    {
        get
        {
            if (_RemCountry == null)
            {
                _RemCountry = string.Empty;
            }
            return _RemCountry;
        }
        set
        {
            _RemCountry = value;
        }
    }
    public string RemTelNo
    {
        get
        {
            if (_RemTelNo == null)
            {
                _RemTelNo = string.Empty;
            }
            return _RemTelNo;
        }
        set
        {
            _RemTelNo = value;
        }
    }
    public string RemMobNo
    {
        get
        {
            if (_RemMobNo == null)
            {
                _RemMobNo = string.Empty;
            }
            return _RemMobNo;
        }
        set
        {
            _RemMobNo = value;
        }
    }
    public string RemDOB
    {
        get
        {
            if (_RemDOB == null)
            {
                _RemDOB = string.Empty;
            }
            return _RemDOB;
        }
        set
        {
            _RemDOB = value;
        }
    }
    public string RemEmail
    {
        get
        {
            if (_RemEmail == null)
            {
                _RemEmail = string.Empty;
            }
            return _RemEmail;
        }
        set
        {
            _RemEmail = value;
        }
    }
    public string RemAcNo
    {
        get
        {
            if (_RemAcNo == null)
            {
                _RemAcNo = string.Empty;
            }
            return _RemAcNo;
        }
        set
        {
            _RemAcNo = value;
        }
    }
    public string RemPayType
    {
        get
        {
            if (_RemPayType == null)
            {
                _RemPayType = string.Empty;
            }
            return _RemPayType;
        }
        set
        {
            _RemPayType = value;
        }
    }
    public string RemPayOther
    {
        get
        {
            if (_RemPayOther == null)
            {
                _RemPayOther = string.Empty;
            }
            return _RemPayOther;
        }
        set
        {
            _RemPayOther = value;
        }
    }    
    public string SrcOfFund
    {
        get
        {
            if (_SrcOfFund == null)
            {
                _SrcOfFund = string.Empty;
            }
            return _SrcOfFund;
        }
        set
        {
            _SrcOfFund = value;
        }
    }
    public string SrcOfFundOthr
    {
        get
        {
            if (_SrcOfFundOthr == null)
            {
                _SrcOfFundOthr = string.Empty;
            }
            return _SrcOfFundOthr;
        }
        set
        {
            _SrcOfFundOthr = value;
        }
    }
    public decimal PayCheques
    {
        get
        {
            if (_PayCheques == null)
            {
                _PayCheques = 0;
            }
            return _PayCheques;
        }
        set
        {
            _PayCheques = value;
        }
    }
    public decimal PayCash
    {
        get
        {
            if (_PayCash == null)
            {
                _PayCash = 0;
            }
            return _PayCash;
        }
        set
        {
            _PayCash = value;
        }
    }
    public decimal PayOth
    {
        get
        {
            if (_PayOth == null)
            {
                _PayOth = 0;
            }
            return _PayOth;
        }
        set
        {
            _PayOth = value;
        }
    }
    public decimal PayTot
    {
        get
        {
            if (_PayTot == null)
            {
                _PayTot = 0;
            }
            return _PayTot;
        }
        set
        {
            _PayTot = value;
        }
    }
    public string SendSMS
    {
        get
        {
            if ((_SendSMS == null) || (_SendSMS.Trim().Length == 0))
            {
                _SendSMS = "No";
            }
            return _SendSMS;
        }
        set
        {
            _SendSMS = value;
        }
    }
    public string CreDate
    {
        get
        {
            if (_CreDate == null)
            {
                _CreDate = string.Empty;
            }
            return _CreDate;
        }
        set
        {
            _CreDate = value;
        }
    }
    public string CreTime
    {
        get
        {
            if (_CreTime == null)
            {
                _CreTime = string.Empty;
            }
            return _CreTime;
        }
        set
        {
            _CreTime = value;
        }
    }
    public string CreBy
    {
        get
        {
            if (_CreBy == null)
            {
                _CreBy = string.Empty;
            }
            return _CreBy;
        }
        set
        {
            _CreBy = value;
        }
    }
    public string ModDate
    {
        get
        {
            if (_ModDate == null)
            {
                _ModDate = string.Empty;
            }
            return _ModDate;
        }
        set
        {
            _ModDate = value;
        }
    }
    public string ModTime
    {
        get
        {
            if (_ModTime == null)
            {
                _ModTime = string.Empty;
            }
            return _ModTime;
        }
        set
        {
            _ModTime = value;
        }
    }
    public string ModBy
    {
        get
        {
            if (_ModBy == null)
            {
                _ModBy = string.Empty;
            }
            return _ModBy;
        }
        set
        {
            _ModBy = value;
        }
    }
    public string RemSts
    {
        get
        {
            if (_RemSts == null)
            {
                _RemSts = string.Empty;
            }
            return _RemSts;
        }
        set
        {
            _RemSts = value;
        }
    }
    public string Status
    {
        get
        {
            if (_Status == null)
            {
                _Status = string.Empty;
            }
            return _Status;
        }
        set
        {
            _Status = value;
        }
    }

    public string AuthProvider
    {
        get
        {
            if (_AuthProvider == null)
            {
                _AuthProvider = string.Empty;
            }
            return _AuthProvider;
        }
        set
        {
            _AuthProvider = value;
        }
    }
    public string AuthReqRef
    {
        get
        {
            if (_AuthReqRef == null)
            {
                _AuthReqRef = string.Empty;
            }
            return _AuthReqRef;
        }
        set
        {
            _AuthReqRef = value;
        }
    }
    public string AuthID
    {
        get
        {
            if (_AuthID == null)
            {
                _AuthID = string.Empty;
            }
            return _AuthID;
        }
        set
        {
            _AuthID = value;
        }
    }
    public string AuthKey
    {
        get
        {
            if (_AuthKey == null)
            {
                _AuthKey = string.Empty;
            }
            return _AuthKey;
        }
        set
        {
            _AuthKey = value;
        }
    }
    public string AuthScore
    {
        get
        {
            if (_AuthScore == null)
            {
                _AuthScore = string.Empty;
            }
            return _AuthScore;
        }
        set
        {
            _AuthScore = value;
        }
    }
    public string AuthResult
    {
        get
        {
            if (_AuthResult == null)
            {
                _AuthResult = string.Empty;
            }
            return _AuthResult;
        }
        set
        {
            _AuthResult = value;
        }
    }
    public string AuthDate
    {
        get
        {
            if (_AuthDate == null)
            {
                _AuthDate = string.Empty;
            }
            return _AuthDate;
        }
        set
        {
            _AuthDate = value;
        }
    }
    public string AuthTime
    {
        get
        {
            if (_AuthTime == null)
            {
                _AuthTime = string.Empty;
            }
            return _AuthTime;
        }
        set
        {
            _AuthTime = value;
        }
    }
    public string ReAuthProvider
    {
        get
        {
            if (_ReAuthProvider == null)
            {
                _ReAuthProvider = string.Empty;
            }
            return _ReAuthProvider;
        }
        set
        {
            _ReAuthProvider = value;
        }
    }
    public string ReAuthReqRef
    {
        get
        {
            if (_ReAuthReqRef == null)
            {
                _ReAuthReqRef = string.Empty;
            }
            return _ReAuthReqRef;
        }
        set
        {
            _ReAuthReqRef = value;
        }
    }
    public string ReAuthID
    {
        get
        {
            if (_ReAuthID == null)
            {
                _ReAuthID = string.Empty;
            }
            return _ReAuthID;
        }
        set
        {
            _ReAuthID = value;
        }
    }
    public string ReAuthKey
    {
        get
        {
            if (_ReAuthKey == null)
            {
                _ReAuthKey = string.Empty;
            }
            return _ReAuthKey;
        }
        set
        {
            _ReAuthKey = value;
        }
    }
    public string ReAuthScore
    {
        get
        {
            if (_ReAuthScore == null)
            {
                _ReAuthScore = string.Empty;
            }
            return _ReAuthScore;
        }
        set
        {
            _ReAuthScore = value;
        }
    }
    public string ReAuthResult
    {
        get
        {
            if (_ReAuthResult == null)
            {
                _ReAuthResult = string.Empty;
            }
            return _ReAuthResult;
        }
        set
        {
            _ReAuthResult = value;
        }
    }
    public string ReAuthDate
    {
        get
        {
            if (_ReAuthDate == null)
            {
                _ReAuthDate = string.Empty;
            }
            return _ReAuthDate;
        }
        set
        {
            _ReAuthDate = value;
        }
    }
    public string ReAuthTime
    {
        get
        {
            if (_ReAuthTime == null)
            {
                _ReAuthTime = string.Empty;
            }
            return _ReAuthTime;
        }
        set
        {
            _ReAuthTime = value;
        }
    }
    public string IsIdDocRcvd
    {
        get
        {
            if (_IsIdDocRcvd == null)
            {
                _IsIdDocRcvd = string.Empty;
            }
            return _IsIdDocRcvd;
        }
        set
        {
            _IsIdDocRcvd = value;
        }
    }
    public string IdDocRcvdDate
    {
        get
        {
            if (_IdDocRcvdDate == null)
            {
                _IdDocRcvdDate = string.Empty;
            }
            return _IdDocRcvdDate;
        }
        set
        {
            _IdDocRcvdDate = value;
        }
    }
    public string IdDocRcvdTime
    {
        get
        {
            if (_IdDocRcvdTime == null)
            {
                _IdDocRcvdTime = string.Empty;
            }
            return _IdDocRcvdTime;
        }
        set
        {
            _IdDocRcvdTime = value;
        }
    }
    public string KYCstatus
    {
        get
        {
            if (_KYCstatus == null)
            {
                _KYCstatus = string.Empty;
            }
            return _KYCstatus;
        }
        set
        {
            _KYCstatus = value;
        }
    }

    public string ProofOfDocNm
    {
        get
        {
            if (_ProofOfDocNm == null)
            {
                _ProofOfDocNm = string.Empty;
            }
            return _ProofOfDocNm;
        }
        set
        {
            _ProofOfDocNm = value;
        }
    }

    public string ApproveRemarks
    {
        get
        {
            if (_ApproveRemarks == null)
            {
                _ApproveRemarks = string.Empty;
            }
            return _ApproveRemarks;
        }
        set
        {
            _ApproveRemarks = value;
        }
    }

    public string DocSubmitDate
    {
        get
        {
            if (_DocSubmitDate == null)
            {
                _DocSubmitDate = string.Empty;
            }
            return _DocSubmitDate;
        }
        set
        {
            _DocSubmitDate = value;
        }
    }

    public string DocSubmitTime
    {
        get
        {
            if (_DocSubmitTime == null)
            {
                _DocSubmitTime = string.Empty;
            }
            return _DocSubmitTime;
        }
        set
        {
            _DocSubmitTime = value;
        }
    }

    public string DocVerifiedBy
    {
        get
        {
            if (_DocVerifiedBy == null)
            {
                _DocVerifiedBy = string.Empty;
            }
            return _DocVerifiedBy;
        }
        set
        {
            _DocVerifiedBy = value;
        }
    }


    #endregion

}