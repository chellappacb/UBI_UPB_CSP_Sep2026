using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Globalization;

/// <summary>
/// Summary description for NumberToText
/// </summary>
public class NumberToText
{
    public NumberToText()
    {
    }

    //private static string[] _ones =
    //    {
    //            "zero",
    //            "one",
    //            "two",
    //            "three",
    //            "four",
    //            "five",
    //            "six",
    //            "seven",
    //            "eight",
    //            "nine"
    //     };



    //private string[] _teens =
    //    {
    //    "ten",
    //    "eleven",
    //    "twelve",
    //    "thirteen",
    //    "fourteen",
    //    "fifteen",
    //    "sixteen",
    //    "seventeen",
    //    "eighteen",
    //    "nineteen"
    //    };




    //private string[] _tens =
    //    {
    //    "",
    //    "ten",
    //    "twenty",
    //    "thirty",
    //    "forty",
    //    "fifty",
    //    "sixty",
    //    "seventy",
    //    "eighty",
    //    "ninety"
    //    };

    //// US Nnumbering`:

    //private string[] _thousands =
    //{
    //"",
    //"thousand",
    //"million",
    //"billion",
    //"trillion",
    //"quadrillion"
    //};

    //    public string Convert(decimal value)
    //{
    //    string digits, temp;
    //    bool showThousands = false;
    //    bool allZeros = true;
    //    StringBuilder builder = new StringBuilder();
    //    // Convert integer portion of value to string
    //    digits = ((long)value).ToString();
    //    // Traverse characters in reverse order
    //    for (int i = digits.Length - 1; i >= 0; i--)
    //    {
    //        int ndigit = (int)(digits[i] - '0');
    //        int column = (digits.Length - (i + 1));

    //        // Determine if ones, tens, or hundreds column
    //        switch (column % 3)
    //        {
    //            case 0:        // Ones position
    //                showThousands = true;
    //                if (i == 0)
    //                {
    //                    // First digit in number (last in loop)
    //                    temp = String.Format("{0} ", _ones[ndigit]);
    //                }
    //                else if (digits[i - 1] == '1')
    //                {
    //                    // This digit is part of "teen" value
    //                    temp = String.Format("{0} ", _teens[ndigit]);
    //                    // Skip tens position
    //                    i--;
    //                }
    //                else if (ndigit != 0)
    //                {
    //                    // Any non-zero digit
    //                    temp = String.Format("{0} ", _ones[ndigit]);
    //                }
    //                else
    //                {
    //                    // This digit is zero. If digit in tens and hundreds
    //                    // column are also zero, don't show "thousands"
    //                    temp = String.Empty;
    //                    // Test for non-zero digit in this grouping
    //                    if (digits[i - 1] != '0' || (i > 1 && digits[i - 2] != '0'))
    //                        showThousands = true;
    //                    else
    //                        showThousands = false;
    //                }

    //                // Show "thousands" if non-zero in grouping
    //                if (showThousands)
    //                {
    //                    if (column > 0)
    //                    {
    //                        temp = String.Format("{0}{1}{2}",
    //                        temp,
    //                        _thousands[column / 3],
    //                            //allZeros ? " " : ", ");
    //                        allZeros ? " " : " ");
    //                    }
    //                    // Indicate non-zero digit encountered
    //                    allZeros = false;
    //                }
    //                builder.Insert(0, temp);
    //                break;

    //            case 1:        // Tens column
    //                if (ndigit > 0)
    //                {
    //                    temp = String.Format("{0}{1}",
    //                    _tens[ndigit],
    //                    (digits[i + 1] != '0') ? "-" : " ");
    //                    builder.Insert(0, temp);
    //                }
    //                break;

    //            case 2:        // Hundreds column
    //                if (ndigit > 0)
    //                {
    //                    temp = String.Format("{0} hundred ", _ones[ndigit]);
    //                    builder.Insert(0, temp);
    //                }
    //                break;
    //        }
    //    }
    //    builder.AppendFormat(" DOLLARS and {0:00} / 100", (value - (long)value) * 100);//Replace Dollars with paisa if you are using indian currencry

    //    // Capitalize first letter
    //    return String.Format("{0}{1}",
    //    Char.ToUpper(builder[0]),
    //    builder.ToString(1, builder.Length - 1));
    //}

    public string AmountinWords(decimal num)
    {
        string returnValue;
        string strNum;
        string strNumDec;
        string strWord;
        strNum = num.ToString(CultureInfo.InvariantCulture);


        if (strNum.IndexOf(".", StringComparison.Ordinal) + 1 != 0)
        {
            strNumDec = strNum.Substring(strNum.IndexOf(".", StringComparison.Ordinal) + 2 - 1);

            if (strNumDec.Length == 1)
            {
                strNumDec = strNumDec + "0";
            }
            if (strNumDec.Length > 2)
            {
                strNumDec = strNumDec.Substring(0, 2);
            }


            strNum = strNum.Substring(0, strNum.IndexOf(".", StringComparison.Ordinal) + 0);
            //strWord = ((double.Parse(strNum) == 1) ? "" : "") + NumToWord((decimal)(double.Parse(strNum))) + ((double.Parse(strNumDec) > 0) ? (" and Paise" + CWord3((decimal)(double.Parse(strNumDec)))) : "");
            strWord = ((double.Parse(strNum) == 1) ? "" : "") + NumToWord((decimal)(double.Parse(strNum))) + " Rupees" + ((double.Parse(strNumDec) > 0) ? (" and " + CWord3((decimal)(double.Parse(strNumDec))) + " Paise") : "");
        }
        else
        {
            strWord = ((double.Parse(strNum) == 1) ? "" : "") + NumToWord((decimal)(double.Parse(strNum))) + " Rupees";
        }

        if ((strWord == string.Empty) || (strWord == null) || (strWord == " ") || (strWord.Trim() == "Rupees"))
            returnValue = "";
        else
            returnValue = strWord + " Only";
        return returnValue;
    }

    private static string NumToWord(decimal num)
    {
        string returnValue;
        string strNum;
        string strWord;
        strNum = num.ToString(CultureInfo.InvariantCulture);


        if (strNum.Length <= 3)
        {
            strWord = CWord3((decimal)(double.Parse(strNum)));
        }
        else
        {
            strWord = CWordG3((decimal)(double.Parse(strNum.Substring(0, strNum.Length - 3)))) + " " + CWord3((decimal)(double.Parse(strNum.Substring(strNum.Length - 2 - 1))));
        }
        returnValue = strWord;
        return returnValue;
    }

    private static string CWordG3(decimal num)
    {
        string returnValue;
        string strNum;
        string strWord;
        strWord = "";
        string readNum;
        strNum = num.ToString(CultureInfo.InvariantCulture);
        if (strNum.Length % 2 != 0)
        {
            readNum = Convert.ToString(double.Parse(strNum.Substring(0, 1)));
            if (readNum != "0")
            {
                strWord = RetWord(decimal.Parse(readNum));
                readNum = Convert.ToString(double.Parse("1" + StrReplicate("0", strNum.Length - 1) + "000"));
                strWord = strWord + " " + RetWord(decimal.Parse(readNum));
            }
            strNum = strNum.Substring(1);
        }
        while (!Convert.ToBoolean(strNum.Length == 0))
        {
            readNum = Convert.ToString(double.Parse(strNum.Substring(0, 2)));
            if (readNum != "0")
            {
                strWord = strWord + " " + CWord3(decimal.Parse(readNum));
                readNum = Convert.ToString(double.Parse("1" + StrReplicate("0", strNum.Length - 2) + "000"));
                strWord = strWord + " " + RetWord(decimal.Parse(readNum));
            }
            strNum = strNum.Substring(2);
        }
        returnValue = strWord;
        return returnValue;
    }

    private static string CWord3(decimal num)
    {
        string returnValue;
        string strNum = "";
        string strWord = "";
        if (num < 0)
        {
            num = num * -1;
        }
        strNum = num.ToString(CultureInfo.InvariantCulture);


        if (strNum.Length == 3)
        {
            string readNum = Convert.ToString(double.Parse(strNum.Substring(0, 1)));
            strWord = RetWord(decimal.Parse(readNum)) + " Hundred";
            strNum = strNum.Substring(1, strNum.Length - 1);
        }


        if (strNum.Length <= 2)
        {
            if (double.Parse(strNum) >= 0 && double.Parse(strNum) <= 20)
            {
                strWord = strWord + " " + RetWord((decimal)(double.Parse(strNum)));
            }
            else
            {
                strWord = strWord + " " + RetWord((decimal)(Convert.ToDouble(strNum.Substring(0, 1) + "0"))) + " " + RetWord((decimal)(double.Parse(strNum.Substring(1, 1))));
            }
        }


        strNum = num.ToString(CultureInfo.InvariantCulture);
        returnValue = strWord;
        return returnValue;
    }

    private static string RetWord(decimal num)
    {
        string returnValue;

        returnValue = "";
        var arrWordList = new object[,] { { 0, "" }, { 1, "One" }, { 2, "Two" }, { 3, "Three" }, { 4, "Four" }, { 5, "Five" }, { 6, "Six" }, { 7, "Seven" }, { 8, "Eight" }, { 9, "Nine" }, { 10, "Ten" }, { 11, "Eleven" }, { 12, "Twelve" }, { 13, "Thirteen" }, { 14, "Fourteen" }, { 15, "Fifteen" }, { 16, "Sixteen" }, { 17, "Seventeen" }, { 18, "Eighteen" }, { 19, "Nineteen" }, { 20, "Twenty" }, { 30, "Thirty" }, { 40, "Forty" }, { 50, "Fifty" }, { 60, "Sixty" }, { 70, "Seventy" }, { 80, "Eighty" }, { 90, "Ninety" }, { 100, "Hundred" }, { 1000, "Thousand" }, { 100000, "Lakh" }, { 10000000, "Crore" } };


        int i;
        for (i = 0; i <= (arrWordList.Length - 1); i++)
        {
            if (num == Convert.ToDecimal(arrWordList[i, 0]))
            {
                returnValue = (string)(arrWordList[i, 1]);
                break;
            }
        }
        return returnValue;
    }

    private static string StrReplicate(string str, int intD)
    {
        string returnValue;

        int i;
        returnValue = "";
        for (i = 1; i <= intD; i++)
        {
            returnValue = returnValue + str;
        }
        return returnValue;
    }
}