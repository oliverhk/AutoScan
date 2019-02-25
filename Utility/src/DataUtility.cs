 
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Drawing;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;

namespace Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class DataUtility
    {
        public static string ACTION_ADD = "ADD";
        public static string ACTION_DElETE = "DELETE";
        public static string ACTION_MODIFY = "MODIFY";
        public static string ACTION_SEARCH = "SEARCH";
        public static string ACTION_COPY = "COPY";
        public static string ACTION_APPROVE = "APPROVE";


        public static string ACTION_LOGIN = "LOGIN";
        public static string ACTION_LOGOUT = "LOGOUT";
        public static string ACTION_TURNON = "TURNON";
        public static string ACTION_TURNOFF = "TURNOFF";
        public static string LanguageType = "EN";


        //Regular Expression		
        //Checking Email
        public const string REGEXP_IS_VALID_EMAIL = @"^\w+((-\w+)|(\.\w+))*\@\w+((\.|-)\w+)*\.\w+$";
        //匹配由数字、26个英文字母或者下划线组成的字符串
        public const string REGEXP_IS_VALID_ID = @"^[\w+$]";
        //匹配由数字、26个英文字母
        public const string REGEXP_IS_VALID_ID1 = @"^[A-Za-z0-9]+$";
        //匹配由数字、26个英文字母
        public const string REGEXP_IS_VALID_CODE = @"[%　％ ]";
        //匹配数字
        public const string REGEXP_IS_VALID_ALFNUM = @"^[0-9]+$";
        //Checking NetAdress											
        public const string REGEXP_IS_VALID_URL_HTTP = @"^http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
        public const string REGEXP_IS_VALID_URL = @"^([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
        //Checking Post Code											
        public const string REGEXP_IS_VALID_ZIP = @"\d{6}";
        //Checking Identifier
        public const string REGEXP_IS_VALID_SSN = @"\d{18}|\d{15}";
        //Checking Interger
        public const string REGEXP_IS_VALID_POSITIVE_INT = @"^\d{1,}$";
        //Checking Zero Interger
        public const string REGEXP_IS_VALID_ZERO_INT = @"^[1-9]{1}\d{0,}$";
        //Checking Decimal

        public const String REGEXP_IS_VALID_DECIMAL = @"^-?(0|\d+)(\.\d+)?$";
        //Checking Date
        //public const String REGEXP_IS_VALID_DATE = @"^(?:(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(\/|-|\.)(?:0?2\1(?:29))$)|(?:(?:1[6-9]|[2-9]\d)?\d{2})(\/|-|\.)(?:(?:(?:0?[13578]|1[02])\2(?:31))|(?:(?:0?[1,3-9]|1[0-2])\2(29|30))|(?:(?:0?[1-9])|(?:1[0-2]))\2(?:0?[1-9]|1\d|2[0-8]))$";
        public const String REGEXP_IS_VALID_DATE = @"^(([0]?[1-9])?([1-2][0-9])?([3][0])?([3][1])?)/(([0]?[1-9])?([1]([0-2]))?)/[1-9][0-9]{3}$";
        //Checking Phone
        public const String REGEXP_IS_VALID_PHONE = @"^((\(\d{3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,8}$";

        //Checking Mobile
        public const String REGEXP_IS_VALID_MOBILE = @"^((\(\d{3}\))|(\d{3}\-))?13\d{9}$";

        //Checking UnSafe
        public const String REGEXP_IS_VALID_UNSAFE = @"^(([A-Z]*|[a-z]*|\d*|[-_\~!@#\$%\^&\*\.\(\)\[\]\{\}<>\?\\\/\'\""""]*)|.{0,5})$|\s";

        //checking ip address [Added by XT 20070614]
        public const string REGEXP_IS_VALID_IP_Address = @"(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])";

        //input type 
        public enum InputType
        {
            Double = 0,
            Number,
            PositiveNumber,
        }

        static DataUtility()
        {

        }

        public static string GetString(SqlDataReader reader, int index)
        {
            return (reader.IsDBNull(index) ? "" : reader.GetString(index));
        }

        public static bool IsEmpty(string val)
        {
            if (val == null)
                return true;
            val = val.Trim();
            if (val.Length == 0)
                return true;
            return false;
        }
        /// <summary>
        /// 匹配数字public const string REGEXP_IS_VALID_ALFNUM = @"^[0-9]+$";
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool IsValidALFNUM(string ID)
        {
            bool isValid = (new Regex(REGEXP_IS_VALID_ALFNUM)).IsMatch(ID.Trim());
            return isValid;
        }

        /// <summary>
        /// 匹配整型public const string REGEXP_IS_VALID_INT  = @"^\d{1,}$"; 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool IsValidPositiveInterger(string ID)
        {
            bool isValid = (new Regex(REGEXP_IS_VALID_POSITIVE_INT)).IsMatch(ID.Trim());
            return isValid;
        }

        /// <summary>
        /// 匹配非零整型public const string REGEXP_IS_VALID_ZERO_INT= @"^[1-9]{1}\d{0,}$";
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool IsValidNoneZeroInterger(string ID)
        {
            bool isValid = (new Regex(REGEXP_IS_VALID_ZERO_INT)).IsMatch(ID.Trim());
            return isValid;
        }

        /// <summary>
        /// 匹配由数字、26个英文字母或者下划线组成的字符串public const string REGEXP_IS_VALID_ID = @"^[\w+$]"; 
        /// </summary>
        /// <param name="emailAddress">Email地址</param>
        /// <returns></returns>
        public static bool IsValidID(string ID)
        {
            bool isValid = (new Regex(REGEXP_IS_VALID_ID)).IsMatch(ID.Trim());
            return isValid;
        }

        /// <summary>
        ///匹配由数字、26个英文字母public const string REGEXP_IS_VALID_CODE = @"[%　％ ]"; 
        /// </summary>
        /// <param name="emailAddress">Email地址</param>
        /// <returns></returns>
        public static bool IsValidCode(string code)
        {
            bool isValid = (new Regex(REGEXP_IS_VALID_CODE)).IsMatch(code.Trim());
            return !isValid;
        }

        /// <summary>
        /// 校验输入的Email地址合法性public const string REGEXP_IS_VALID_EMAIL = @"^\w+((-\w+)|(\.\w+))*\@\w+((\.|-)\w+)*\.\w+$";
        /// </summary>
        /// <param name="emailAddress">Email地址</param>
        /// <returns></returns>
        public static bool IsValidEmail(string emailAddress)
        {
            bool isValid = (new Regex(REGEXP_IS_VALID_EMAIL)).IsMatch(emailAddress.Trim());
            return isValid;
        }

        /// <summary>
        /// 校验输入的邮政编码合法性public const string REGEXP_IS_VALID_ZIP  = @"\d{6}"; 
        /// </summary>
        /// <param name="zipCode">邮政编码</param>
        /// <returns></returns>
        public static bool IsValidZip(string zipCode)
        {
            bool isValid = (new Regex(REGEXP_IS_VALID_ZIP)).IsMatch(zipCode.Trim());
            return isValid;
        }

        /// <summary>
        /// 校验输入的身份证编码合法性public const string REGEXP_IS_VALID_SSN  = @"\d{18}|\d{15}"; 
        /// </summary>
        /// <param name="identity">身份证编码</param>
        /// <returns></returns>
        public static bool IsValidIdentity(string identity)
        {
            bool isValid = (new Regex(REGEXP_IS_VALID_SSN)).IsMatch(identity.Trim());
            return isValid;
        }

        /// <summary>
        /// 校验输入的网址合法性public const string REGEXP_IS_VALID_URL_HTTP  = @"^http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"; 
        /// </summary>
        /// <param name="url">网址</param>
        /// <returns></returns>
        public static bool IsValidUrl(string url)
        {
            bool isValid = ((new Regex(REGEXP_IS_VALID_URL_HTTP)).IsMatch(url.Trim()) || (new Regex(REGEXP_IS_VALID_URL)).IsMatch(url.Trim()));
            return isValid;
        }

        /// <summary>
        /// 校验输入的日期合法性public const String REGEXP_IS_VALID_DATE = @"^[1-9][0-9]{3}-(([0]?[1-9])?([1]([0-2]))?)-(([0]?[1-9])?([1-2][0-9])?([3][0])?([3][1])?)$";
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static bool IsValidDate(string date)
        {
            bool isValid = (new Regex(REGEXP_IS_VALID_DATE)).IsMatch(date.Trim());
            if (isValid)
            {
                try
                {
                    System.IFormatProvider dateFormat = new System.Globalization.CultureInfo("fr-FR", true);
                    DateTime dateTime = new DateTime();

                    if (!IsEmpty(date))
                    {
                        dateTime = DateTime.Parse(date, dateFormat);
                    }
                }
                catch
                {
                    isValid = false;
                }

            }
            return isValid;
        }

        /// <summary>
        /// 校验输入的Decimal的合法性public const String REGEXP_IS_VALID_DECIMAL = @"^-?(0|\d+)(\.\d+)?$";    
        /// </summary>
        /// <param name="number">数值</param>
        /// <returns></returns>
        public static bool IsValidNumber(string number)
        {
            bool isValid = (new Regex(REGEXP_IS_VALID_DECIMAL)).IsMatch(number.Trim());
            return isValid;
        }
        /// <summary>
        /// 校验输入的电话号码合法性public const String REGEXP_IS_VALID_PHONE = @"^((\(\d{3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,8}$";
        /// </summary>
        /// <param name="number">电话号码</param>
        /// <returns></returns>
        public static bool IsValidPhone(string phone)
        {
            bool isValid = (new Regex(REGEXP_IS_VALID_PHONE)).IsMatch(phone.Trim());
            return isValid;
        }
        /// <summary>
        /// 校验输入的手机号码合法性public const String REGEXP_IS_VALID_MOBILE = @"^((\(\d{3}\))|(\d{3}\-))?13\d{9}$";
        /// </summary>
        /// <param name="number">手机号码</param>
        /// <returns></returns>
        public static bool IsValidMobile(string mobile)
        {
            bool isValid = (new Regex(REGEXP_IS_VALID_MOBILE)).IsMatch(mobile.Trim());
            return isValid;
        }
        /// <summary>
        /// 校验输入的密码安全性合法性 public const String REGEXP_IS_VALID_UNSAFE = @"^(([A-Z]*|[a-z]*|\d*|[-_\~!@#\$%\^&\*\.\(\)\[\]\{\}<>\?\\\/\'\""""]*)|.{0,5})$|\s";
        /// </summary>
        /// <param name="number">密码</param>
        /// <returns></returns>
        public static bool IsValidUnsafe(string unsafes)
        {
            bool isValid = (new Regex(REGEXP_IS_VALID_UNSAFE)).IsMatch(unsafes.Trim());
            return isValid;
        }
        /// <summary>
        /// 校验输入的为Decimal类型public const String REGEXP_IS_VALID_DECIMAL = @"^-?(0|\d+)(\.\d+)?$";
        /// </summary>
        /// <param name="Decimal"></param>
        /// <returns></returns>
        public static bool IsValidDecimal(string Decimal)
        {
            bool isValid = (new Regex(REGEXP_IS_VALID_DECIMAL)).IsMatch(Decimal.Trim());
            return isValid;
        }

        /// <summary>
        /// REGEXP_IS_VALID_IP_Address=@"/(\d+)\.(\d+)\.(\d+)\.(\d+)/g";
        /// </summary>
        /// <param name="Decimal"></param>
        /// <returns></returns>
        public static bool IsValidIpAddress(string ipAddress)
        {
            bool isValid = (new Regex(REGEXP_IS_VALID_IP_Address)).IsMatch(ipAddress.Trim());
            return isValid;
        }

        public static int ParseInt(string val, int rep)
        {
            int intval = rep;
            try
            {
                if (!IsEmpty(val))
                {
                    //解决代小数点的数据转整形出错的问题。
                    Double tempValue = System.Double.Parse(val.Trim());
                    System.Math.Round(tempValue, 0).ToString();
                    intval = Int32.Parse(System.Math.Round(tempValue, 0).ToString().Trim());
                }
            }
            catch
            {
                intval = rep;
            }
            return intval;
        }

        public static int ParseInt(string val)
        {
            int intval = -1;
            try
            {
                if (!IsEmpty(val))
                {
                    //解决代小数点的数据转整形出错的问题。
                    Double tempValue = System.Double.Parse(val.Trim());
                    System.Math.Round(tempValue, 0).ToString();
                    intval = Int32.Parse(System.Math.Round(tempValue, 0).ToString().Trim());
                }
            }
            catch
            {
                intval = -1;
            }
            return intval;
        }
        public static double ParseDouble(string val, double rep)
        {
            Double intval = rep;
            try
            {
                if (!IsEmpty(val))
                {
                    intval = System.Double.Parse(val.Trim());
                }
            }
            catch
            {
                intval = rep;
            }
            return intval;
        }
        public static double ParseDouble(string val)
        {
            Double intval = -1;
            try
            {
                if (!IsEmpty(val))
                {
                    intval = System.Double.Parse(val.Trim());
                }
            }
            catch
            {
                intval = -1;
            }
            return intval;
        }
        public static float ParseFloat(string val, float rep)
        {
            float intval = rep;
            try
            {
                if (!IsEmpty(val))
                {
                    intval = float.Parse(val.Trim());
                }
            }
            catch
            {
                intval = rep;
            }
            return intval;
        }
        public static float ParseFloat(string val)
        {
            float intval = -1;
            try
            {
                if (!IsEmpty(val))
                {
                    intval = float.Parse(val.Trim());
                }
            }
            catch
            {
                intval = -1;
            }
            return intval;
        }
        public static decimal ParseDecimal(string val, decimal rep)
        {
            Decimal intval = rep;
            try
            {
                if (!IsEmpty(val))
                {
                    intval = System.Decimal.Parse(val.Trim());
                }
            }
            catch
            {
                intval = rep;
            }
            return intval;
        }
        public static decimal ParseDecimal(string val)
        {
            Decimal intval = -1;
            try
            {
                if (!IsEmpty(val))
                {
                    intval = System.Decimal.Parse(val.Trim());
                }
            }
            catch
            {
                intval = -1;
            }
            return intval;
        }
        public static DateTime ParseDateTime(string val, DateTime rep)
        {
            DateTime intval = rep;
            try
            {
                if (!IsEmpty(val))
                {
                    intval = System.DateTime.Parse(val.Trim());
                }
            }
            catch
            {
                intval = rep;
            }
            return intval;
        }
        public static DateTime ParseDateTime(string val)
        {
            DateTime intval = DateTime.Now;
            try
            {
                if (!IsEmpty(val))
                {
                    intval = System.DateTime.Parse(val.Trim());
                }
            }
            catch
            {
                intval = DateTime.Now;
            }
            return intval;
        }
        public static Boolean ParseBoolean(object val)
        {
            Boolean initVaule = false;
            try
            {
                initVaule = Convert.ToBoolean(val);
            }
            catch
            {
                initVaule = false;
            }
            return initVaule;
        }


        public static bool ControlKeyPress(string textValue, System.Windows.Forms.KeyPressEventArgs e, string type)
        {
            if (type.Equals("Double"))
            {
                if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                {
                    if (Char.IsPunctuation(e.KeyChar) && e.KeyChar.ToString().Equals("."))
                    {
                        if (textValue.IndexOf(".") > -1)
                        {
                            e.Handled = true;
                        }
                        else
                        {
                            e.Handled = false;
                        }
                    }
                    else if (Char.IsPunctuation(e.KeyChar) && e.KeyChar.ToString().Equals("-"))
                    {
                        if (textValue.IndexOf("-") > -1)
                        {
                            e.Handled = true;
                        }
                        else
                        {
                            e.Handled = false;
                        }
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    e.Handled = false;
                }
            }
            else if (type.Equals("Number"))
            {
                if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }

            if (e.Handled)
            {
                return true;
            }
            return false;
        }
        public static bool ControlKeyPress(string textValue, System.Windows.Forms.KeyPressEventArgs e, InputType type)
        {
            switch (type)
            {
                case InputType.Double:
                    if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                    {
                        if (Char.IsPunctuation(e.KeyChar) && e.KeyChar.ToString().Equals("."))
                        {
                            if (textValue.IndexOf(".") > -1)
                            {
                                e.Handled = true;
                            }
                            else
                            {
                                e.Handled = false;
                            }
                        }
                        else if (Char.IsPunctuation(e.KeyChar) && e.KeyChar.ToString().Equals("-"))
                        {
                            if (textValue.IndexOf("-") > -1)
                            {
                                e.Handled = true;
                            }
                            else
                            {
                                e.Handled = false;
                            }
                        }
                        else
                        {
                            e.Handled = true;
                        }
                    }
                    break;
                case InputType.Number:
                    if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                case InputType.PositiveNumber:
                    if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                    {
                        if (Char.IsPunctuation(e.KeyChar) && e.KeyChar.ToString().Equals("."))
                        {
                            if (textValue.IndexOf(".") > -1)
                            {
                                e.Handled = true;
                            }
                            else
                            {
                                e.Handled = false;
                            }
                        }
                        else if (Char.IsPunctuation(e.KeyChar) && e.KeyChar.ToString().Equals("-"))
                        {
                            e.Handled = true;
                        }
                        else
                        {
                            e.Handled = true;
                        }
                    }
                    break;
            }

            return e.Handled;
        }

        public static Rectangle Convert2Rectangle(RectangleF org)
        {
            return new Rectangle((int)Math.Round(org.X), (int)Math.Round(org.Y), (int)Math.Round(org.Width), (int)Math.Round(org.Height));
        }

    }
}
