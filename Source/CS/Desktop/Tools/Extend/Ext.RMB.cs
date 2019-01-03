using System;
using System.Data;
using System.Configuration;

 namespace Tools
{
/// <summary>
/// 将指定的数字转换为人民币大写
/// </summary>
public class RMB
{
    private decimal money = 0;
    /// <summary>
    /// 设置转换的人民币金额
    /// </summary>
    public decimal setMoney
    {
        set { money = value; }
    }
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="m">金额</param>
    public RMB(decimal m)
	{
        money = m;
	}
    /// <summary>
    /// 将数字转换为人民币大写格式
    /// </summary>
    /// <returns></returns>
    public string convertCurrency()
    {
     const decimal MAXIMUM_NUMBER = 99999999999.99M;
     const string CN_ZERO = "零";
     const string CN_ONE = "壹"; 
    const string CN_TWO = "贰"; 
    const string CN_THREE = "叁"; 
    const string CN_FOUR = "肆"; 
    const string CN_FIVE = "伍"; 
    const string CN_SIX = "陆"; 
    const string CN_SEVEN = "柒"; 
    const string CN_EIGHT = "捌"; 
    const string CN_NINE = "玖"; 
    const string CN_TEN = "拾"; 
    const string CN_HUNDRED = "佰"; 
    const string CN_THOUSAND = "仟"; 
    const string CN_TEN_THOUSAND = "万"; 
    const string CN_HUNDRED_MILLION = "亿"; 
    const string CN_SYMBOL = "人民币(大写):"; 
    const string CN_DOLLAR = "元"; 
    const string CN_TEN_CENT = "角"; 
    const string CN_CENT = "分"; 
    const string CN_INTEGER = "整";
    string integral="";// Represent integral part of digit number.
    string dec="";// Represent decimal part of digit number.
    string outputstringacters="";    // The output result.
    string[] parts;
   
    int zeroCount;
    int i, p;
        string d="";
     int quotient, modulus;
     System.Text.RegularExpressions.Regex reg=new System.Text.RegularExpressions.Regex(@"^((\d{1,3}(,\d{3})*(.((\d{3},)*\d{1,3}))?)|(\d+(.\d+)?))$");
     if (reg.IsMatch(money.ToString())==false)
     {    
        return "非法数字格式!"; 
     }
     // Assert the number is not greater than the maximum number. 
    if (money > MAXIMUM_NUMBER) 
    {        
        return "数字太大!"; 
    } 
    // Process the coversion from currency digits to stringacters: 
    // Separate integral and decimal parts before processing coversion: 
    parts = money.ToString().Split('.'); 
    if (parts.Length > 1)
    { 
        integral = parts[0]; 
        dec = parts[1]; 
        // Cut down redundant decimal digits that are after the second. 
        dec = dec.Substring(0, 2); 
    } 
    else { 
        integral = parts[0]; 
        dec = ""; 
    } 
   // Prepare the stringacters corresponding to the digits: 
    string[] digits ={CN_ZERO, CN_ONE, CN_TWO, CN_THREE, CN_FOUR, CN_FIVE, CN_SIX, CN_SEVEN, CN_EIGHT, CN_NINE};
    string[] radices = { "", CN_TEN, CN_HUNDRED, CN_THOUSAND };
    string[] bigRadices = { "", CN_TEN_THOUSAND, CN_HUNDRED_MILLION };
    string[] decimals = { CN_TEN_CENT, CN_CENT }; 
   // Start processing: 
    outputstringacters = ""; 
   // Process integral part if it is larger than 0: 
    if (Convert.ToInt64(integral) > 0) { 
        zeroCount = 0; 
        for (i = 0; i < integral.Length; i++) { 
            p = integral.Length - i - 1; 
            d = integral.Substring(i, 1); 
            quotient = p / 4; 
            modulus = p % 4; 
            if (d == "0") { 
                zeroCount++; 
            } 
            else { 
                if (zeroCount > 0) 
                { 
                    outputstringacters += digits[0]; 
                } 
                zeroCount = 0;
                outputstringacters += digits[Convert.ToInt32(d)] + radices[modulus]; 
            } 
            if (modulus == 0 && zeroCount < 4) { 
                outputstringacters += bigRadices[quotient]; 
            } 
        } 
        outputstringacters += CN_DOLLAR; 
    }
    // Process decimal part if there is: 
    if (dec != "") { 
        for (i = 0; i < dec.Length ; i++) { 
            d = dec.Substring(i, 1); 
            if (d != "0") { 
                outputstringacters += digits[Convert.ToInt32(d)] + decimals[i]; 
            } 
        } 
    } 
  // Confirm and return the final output string: 
    if (outputstringacters == "") { 
        outputstringacters = CN_ZERO + CN_DOLLAR; 
    } 
    if (dec == "") { 
        outputstringacters += CN_INTEGER; 
    } 
    outputstringacters = CN_SYMBOL + outputstringacters; 
    return outputstringacters; 
    }
}
}

