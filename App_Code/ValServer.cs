using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.CSharp;
using AjaxControlToolkit;
using System.Web.UI;
using System.Web.UI.WebControls;


public class ValServer
{
    public ValServer()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public PlaceHolder PlaceHolder = new PlaceHolder();

    public string VDate = "";

    public void RequiredVal(Control txt, string EMsg, string ValGroup = "")
    {
        RequiredFieldValidator Req = new RequiredFieldValidator();
        Req.ID = "Req_" + txt.ClientID;
        Req.ControlToValidate = txt.ClientID;
        Req.ErrorMessage = EMsg;
        Req.Display = ValidatorDisplay.None;
        Req.SetFocusOnError = true;

        if ((txt) is DropDownList)
        {
            Req.InitialValue = "-1";
        }

        if (!string.IsNullOrEmpty(ValGroup))
        {
            Req.ValidationGroup = ValGroup;
        }

        PlaceHolder.Controls.Add(Req);

        ValidatorCalloutExtender valc = new ValidatorCalloutExtender();
        valc.ID = "ValcReq_" + txt.ID;
        valc.TargetControlID = Req.ID;
        PlaceHolder.Controls.Add(valc);
        valc.CssClass = "CustomValidatorCalloutStyle";
    }


    public void RangeVal(TextBox txt, string ErMsg, ValidationDataType RangeType, string MinVal, string MaxVal, string ValGroup = "")
    {
        RangeValidator Rang = new RangeValidator();
        Rang.ID = "Range_" + txt.ID;
        Rang.ControlToValidate = txt.ID;
        Rang.Type = RangeType;

        Rang.MinimumValue = MinVal;
        Rang.MaximumValue = MaxVal;

        Rang.ErrorMessage = ErMsg;
        Rang.Display = ValidatorDisplay.None;
        Rang.SetFocusOnError = true;

        if (!string.IsNullOrEmpty(ValGroup))
        {
            Rang.ValidationGroup = ValGroup;
        }

        PlaceHolder.Controls.Add(Rang);

        ValidatorCalloutExtender valc = new ValidatorCalloutExtender();
        valc.ID = "ValcRange_" + txt.ID;
        valc.TargetControlID = Rang.ID;
        PlaceHolder.Controls.Add(valc);
        valc.CssClass = "CustomValidatorCalloutStyle";
    }


    public void CompareTxtbox(TextBox txt1, TextBox txt2, string EMsg, ValidationCompareOperator strOperator, ValidationDataType Type, string ValGroup = "")
    {
        CompareValidator Comp = new CompareValidator();
        Comp.ID = "Compt_" + txt1.ID;
        Comp.ControlToValidate = txt2.ID;
        Comp.ControlToCompare = txt1.ID;
        Comp.Type = Type;
        Comp.Operator = strOperator;
        Comp.ErrorMessage = EMsg;
        Comp.Display = ValidatorDisplay.None;
        Comp.SetFocusOnError = true;

        if (!string.IsNullOrEmpty(ValGroup))
        {
            Comp.ValidationGroup = ValGroup;
        }

        PlaceHolder.Controls.Add(Comp);
     
    }


    public void CompareVal(TextBox txt, string EMsg, string ValueToCompare, ValidationDataType strType, ValidationCompareOperator StrOperator, string ValGroup = "")
    {
        CompareValidator Comp = new CompareValidator();
        Comp.ID = "Com_" + txt.ID;
        Comp.ControlToValidate = txt.ID;
        //Comp.ControlToCompare = txt.ID
        Comp.ValueToCompare = ValueToCompare;
        Comp.Type = strType;
        Comp.Operator = StrOperator;
        Comp.ErrorMessage = EMsg;
        Comp.Display = ValidatorDisplay.None;
        Comp.SetFocusOnError = true;

        if (!string.IsNullOrEmpty(ValGroup))
        {
            Comp.ValidationGroup = ValGroup;
        }

        PlaceHolder.Controls.Add(Comp);
      
    }

    //public void CompareDate(TextBox txt, string EMsg, string ValueToCompare, ValidationDataType strType, ValidationCompareOperator StrOperator, string ValGroup = "")
    //{
    //    if (Information.IsDate(txt.Text) == true & Information.IsDate(ValueToCompare) == true) {
    //        CompareValidator Comp = new CompareValidator();
    //        Comp.ID = "ComDat_" + txt.ID;
    //        Comp.ControlToValidate = txt.ID;
    //        //Comp.ControlToCompare = txt.ID
    //        Comp.ValueToCompare = ValueToCompare;
    //        Comp.Type = strType;
    //        Comp.Operator = StrOperator;
    //        Comp.ErrorMessage = EMsg;
    //        Comp.Display = ValidatorDisplay.None;
    //        Comp.SetFocusOnError = true;

    //        if (!string.IsNullOrEmpty(ValGroup)) {
    //            Comp.ValidationGroup = ValGroup;
    //        }

    //        PlaceHolder.Controls.Add(Comp);
    //    }
    //}

    //public void DateVal(string strDate, string EMsg, Control setfocus, string ValGroup = "")
    //{
    //    VDate = strDate;
    //    CustomValidator ValD = new CustomValidator();
    //    ValD.ID = "ValD_" + setfocus.ID;
    //    ValD.ControlToValidate = setfocus.ID;
    //    ValD.ErrorMessage = EMsg;
    //    ValD.Display = ValidatorDisplay.None;
    //    ValD.SetFocusOnError = true;
    //    ValD.ServerValidate += Date_ServerValidate;

    //    if (!string.IsNullOrEmpty(ValGroup)) {
    //        ValD.ValidationGroup = ValGroup;
    //    }

    //    PlaceHolder.Controls.Add(ValD);

    //    ValidatorCalloutExtender valc = new ValidatorCalloutExtender();
    //    valc.ID = "ValcD_" + setfocus.ID;
    //    valc.TargetControlID = ValD.ID;
    //    PlaceHolder.Controls.Add(valc);

    //}

    public void AmtZero(TextBox txt, string EMsg, Validate strtype, string ValGroup = "")
    {
        RegularExpressionValidator Regu = new RegularExpressionValidator();
        Regu.ID = "Cust_" + txt.ID;
        Regu.ControlToValidate = txt.ID;
        Regu.ErrorMessage = EMsg;
        Regu.Display = ValidatorDisplay.None;
        Regu.SetFocusOnError = true;

        string RExp = "";

        if (strtype == Validate.Amount)
        {
            RExp = "^(-)?\\d+(\\.\\d*)?$";
        }

        Regu.ValidationExpression = RExp;

        if (!string.IsNullOrEmpty(ValGroup))
        {
            Regu.ValidationGroup = ValGroup;
        }
    }
    public void CustomVal(TextBox txt, string EMsg, Validate strtype, string ValGroup = "")
    {
        RegularExpressionValidator Regu = new RegularExpressionValidator();
        Regu.ID = "Cust_" + txt.ID;
        Regu.ControlToValidate = txt.ID;
        Regu.ErrorMessage = EMsg;
        Regu.Display = ValidatorDisplay.None;
        Regu.SetFocusOnError = true;

        string RExp = "";

        if (strtype == Validate.Password)
        {
            //RExp = "^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[£@#$%^&+=]).*$"
            //RExp = "^.*(?=.{6,})(?=.*\d)(?=.*[a-zA-Z])(?=.*[£@#$%^&+=]).*$"
            //RExp = "^.*(?=.{6,})(?=.*\d)(?=.*[a-zA-Z]).*$"
            RExp = "^.*(?=.{6,})(?=.*\\d)(?=.*[a-zA-Z])(?=.*[@#$%^£&+=]).*$";

        }
        else if (strtype == Validate.Numeric)
        {
            RExp = "^\\d*$";

        }
        else if (strtype == Validate.Alpha)
        {
            RExp = "^[a-zA-Z-/. ]+$";

        }
        else if (strtype == Validate.AlphaNum)
        {
            RExp = "^[0-9a-zA-Z-_/<. ]+$";

        }
        else if (strtype == Validate.AlphaNumSpec)
        {
            RExp = "^[0-9a-zA-Z.£@#$%^&+=()*-_/: ]+$";

        }
        else if (strtype == Validate.VDate)
        {
            RExp = "^(((0[1-9]|[12]\\d|3[01])\\/(0[13578]|1[02])\\/((19|[2-9]\\d)\\d{2}))|((0[1-9]|[12]\\d|30)\\/(0[13456789]|1[012])\\/((19|[2-9]\\d)\\d{2}))|((0[1-9]|1\\d|2[0-8])\\/02\\/((19|[2-9]\\d)\\d{2}))|(29\\/02\\/((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$";

        }
        else if (strtype == Validate.Email)
        {
            RExp = "^(([\\w-]+\\.)+[\\w-]+|([a-zA-Z]{1}|[\\w-]{2,}))@" + "((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?" + "[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\." + "([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?" + "[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" + "([a-zA-Z]+[\\w-]+\\.)+[a-zA-Z]{2,4})$";

        }
        else if (strtype == Validate.Amount)
        {
            RExp = "^(-)?\\d+(\\.\\d*)?$";
        }
        else if (strtype == Validate.Passport)
        {
            //RExp = "^[0-9a-zA-Z>]+$";
            RExp = "^[a-zA-Z0-9\\<]*$";
        }

        Regu.ValidationExpression = RExp;

        if (!string.IsNullOrEmpty(ValGroup))
        {
            Regu.ValidationGroup = ValGroup;
        }

        PlaceHolder.Controls.Add(Regu);

        ValidatorCalloutExtender valc = new ValidatorCalloutExtender();
        valc.ID = "ValcRegu_" + txt.ID;
        valc.TargetControlID = Regu.ID;
        PlaceHolder.Controls.Add(valc);
        valc.CssClass = "CustomValidatorCalloutStyle";
    }


    public void LengthVal(TextBox txt, string EMsg, string MinVal, string MaxVal, string ValGroup = "")
    {
        RegularExpressionValidator RegLen = new RegularExpressionValidator();
        RegLen.ID = "RegLen_" + txt.ID;
        RegLen.ControlToValidate = txt.ID;
        RegLen.ErrorMessage = EMsg;
        RegLen.Display = ValidatorDisplay.None;
        RegLen.SetFocusOnError = true;
        RegLen.ValidationExpression = "(.|\\n){" + MinVal + "," + MaxVal + "}";

        if (!string.IsNullOrEmpty(ValGroup))
        {
            RegLen.ValidationGroup = ValGroup;
        }

        PlaceHolder.Controls.Add(RegLen);

        ValidatorCalloutExtender valc = new ValidatorCalloutExtender();
        valc.ID = "ValcRegLen_" + txt.ID;
        valc.TargetControlID = RegLen.ID;
        PlaceHolder.Controls.Add(valc);
        valc.CssClass = "CustomValidatorCalloutStyle";
    }

    public enum Validate
    {

        Password,
        Numeric,
        Alpha,
        AlphaNum,
        AlphaNumSpec,
        VDate,
        Email,
        Amount,
        Passport

    }


    //public void Date_ServerValidate(object sender, ServerValidateEventArgs args)
    //{
    //    if (!Regex.Match(VDate, "^(((0[1-9]|[12]\\d|3[01])\\/(0[13578]|1[02])\\/((19|[2-9]\\d)\\d{2}))|((0[1-9]|[12]\\d|30)\\/(0[13456789]|1[012])\\/((19|[2-9]\\d)\\d{2}))|((0[1-9]|1\\d|2[0-8])\\/02\\/((19|[2-9]\\d)\\d{2}))|(29\\/02\\/((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$").Success) {
    //        args.IsValid = false;
    //    } else if (Information.IsDate(VDate) == false) {
    //        args.IsValid = false;
    //    } else {
    //        args.IsValid = true;
    //    }

    //}

}

