using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.Utilities
{
   public static class DateTimeUtility
    {
        public static DateTime ToMiladiDate(this DateTime dt)
        {
            var pc = new PersianCalendar();
            return pc.ToDateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, 0);
        }

        public static DateTime ToPersianDate(this DateTime dt)
        {
            var pc = new PersianCalendar();
            var year = pc.GetYear(dt);
            var month = pc.GetMonth(dt);
            var day = pc.GetDayOfMonth(dt);
            var hour = pc.GetHour(dt);
            var min = pc.GetMinute(dt);

            return new DateTime(year, month, day, hour, min, 0);


            //return persianDateTime.ToString("yyyy/MM/dd HH:mm");
        }
        public static bool IsUrl(this string str)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            //return Regex.IsMatch(str, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?");
        }

        public static bool IsMobile(this string str)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str, @"^(((\+|00)98)|0)?9[123]\d{8}$");
        }

        public static bool IsTimeSpan12(this string str)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str, @"^(1[012]|[1-9]):([0-5]?[0-9]) (AM|am|PM|pm)$");
        }

        public static bool IsTimeSpan12P(this string str)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str, @"^(1[012]|[1-9]):([0-5]?[0-9]) (ق ظ|ق.ظ|ب ظ|ب.ظ)$");
        }

        public static bool IsTimeSpan24Hhm(this string str)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str, @"^([01][0-9]|2[0-3]):([0-5]?[0-9])$");
        }

        public static bool IsTimeSpan24Hm(this string str)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str, @"^(2[0-3]|[01]?\d):([0-5]?[0-9])$");
        }

        public static bool IsPersianDateTime(this string str)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str, @"^(13\d{2}|[1-9]\d)/(1[012]|0?[1-9])/([12]\d|3[01]|0?[1-9]) ([01][0-9]|2[0-3]):([0-5]?[0-9])$");
        }

        public static bool IsTime(this string str)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str, @"^([01][0-9]|2[0-3]):([0-5]?[0-9])$");
        }

        public static bool IsPersianDate(this string str)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str, @"^(13\d{2}|[1-9]\d)/(1[012]|0?[1-9])/([12]\d|3[01]|0?[1-9])$");
        }

        public static PersianDateTime ToPersianDateTime(this DateTime dt)
        {
            return new PersianDateTime(dt);
        }

        public static PersianDateTime ToPersianDateTime(this string text)
        {
            return new PersianDateTime(Convert.ToDateTime(text));
        }

        public static string ToStringDateTime12(this PersianDateTime dt)
        {
            return dt.ToString("yyyy/MM/dd hh:mm tt");
        }

        public static string ToStringDateTime24(this PersianDateTime dt)
        {
            return dt.ToString("yyyy/MM/dd HH:mm");
        }

        public static string ToStringShamsiDate(this PersianDateTime dt)
        {
            string tt = dt.Hour < 12 ? "قبل از ظهر" : "بعد از ظهر";
            return dt.ToString("d MMMM yyyy در h:mm " + tt);
        }

        public static string ToStringDate(this PersianDateTime dt)
        {
            return dt.ToString("yyyy/MM/dd");
        }

        public static string ToStringDateTime12P(this PersianDateTime dt)
        {
            //return dt.ToString("yyyy/MM/dd hh:mm tt").Replace("AM", "ق.ظ").Replace("PM", "ب.ظ");
            string hh = dt.ToString("HH");
            string tt = Convert.ToInt32(hh) < 12 ? "ق.ظ" : "ب.ظ";
            return dt.ToString("yyyy/M/d h:mm " + tt);
        }

        public static string ToStringShamsiDate(this DateTime dt)
        {
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
            int intYear = pc.GetYear(dt);
            int intMonth = pc.GetMonth(dt);
            int intDayOfMonth = pc.GetDayOfMonth(dt);
            DayOfWeek enDayOfWeek = pc.GetDayOfWeek(dt);
            string strMonthName = "";
            string strDayName = "";
            switch (intMonth)
            {
                case 1:
                    strMonthName = "فروردین";
                    break;
                case 2:
                    strMonthName = "اردیبهشت";
                    break;
                case 3:
                    strMonthName = "خرداد";
                    break;
                case 4:
                    strMonthName = "تیر";
                    break;
                case 5:
                    strMonthName = "مرداد";
                    break;
                case 6:
                    strMonthName = "شهریور";
                    break;
                case 7:
                    strMonthName = "مهر";
                    break;
                case 8:
                    strMonthName = "آبان";
                    break;
                case 9:
                    strMonthName = "آذر";
                    break;
                case 10:
                    strMonthName = "دی";
                    break;
                case 11:
                    strMonthName = "بهمن";
                    break;
                case 12:
                    strMonthName = "اسفند";
                    break;
                default:
                    strMonthName = "";
                    break;
            }

            //switch (enDayOfWeek)
            //{
            //    case DayOfWeek.Friday:
            //        strDayName = "جمعه";
            //        break;
            //    case DayOfWeek.Monday:
            //        strDayName = "دوشنبه";
            //        break;
            //    case DayOfWeek.Saturday:
            //        strDayName = "شنبه";
            //        break;
            //    case DayOfWeek.Sunday:
            //        strDayName = "یکشنبه";
            //        break;
            //    case DayOfWeek.Thursday:
            //        strDayName = "پنجشنبه";
            //        break;
            //    case DayOfWeek.Tuesday:
            //        strDayName = "سه شنبه";
            //        break;
            //    case DayOfWeek.Wednesday:
            //        strDayName = "چهارشنبه";
            //        break;
            //    default:
            //        strDayName = "";
            //        break;
            //}

            return (string.Format("{0} {1} {2} {3}", strDayName, intDayOfMonth, strMonthName, intYear));
        }

        public static string ToText(this int digit)
        {
            string txt = digit.ToString();
            int length = txt.Length;

            string[] a1 = new string[10] { "-", "یک", "دو", "سه", "چهار", "پنح", "شش", "هفت", "هشت", "نه" };
            string[] a2 = new string[10] { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
            string[] a3 = new string[10] { "-", "ده", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
            string[] a4 = new string[10] { "-", "یک صد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفصد", "هشصد", "نهصد" };

            string result = "";
            bool isDahegan = false;

            for (int i = 0; i < length; i++)
            {
                string character = txt[i].ToString();
                switch (length - i)
                {
                    case 7://میلیون
                        if (character != "0")
                        {
                            result += a1[Convert.ToInt32(character)] + " میلیون و ";
                        }
                        else
                        {
                            result = result.TrimEnd('و', ' ');
                        }
                        break;
                    case 6://صدهزار
                        if (character != "0")
                        {
                            result += a4[Convert.ToInt32(character)] + " و ";
                        }
                        else
                        {
                            result = result.TrimEnd('و', ' ');
                        }
                        break;
                    case 5://ده هزار
                        if (character == "1")
                        {
                            isDahegan = true;
                        }
                        else if (character != "0")
                        {
                            result += a3[Convert.ToInt32(character)] + " و ";
                        }
                        break;
                    case 4://هزار
                        if (isDahegan == true)
                        {
                            result += a2[Convert.ToInt32(character)] + " هزار و ";
                            isDahegan = false;
                        }
                        else
                        {
                            if (character != "0")
                            {
                                result += a1[Convert.ToInt32(character)] + " هزار و ";
                            }
                            else
                            {
                                result = result.TrimEnd('و', ' ');
                            }
                        }
                        break;
                    case 3://صد
                        if (character != "0")
                        {
                            result += a4[Convert.ToInt32(character)] + " و ";
                        }
                        break;
                    case 2://ده
                        if (character == "1")
                        {
                            isDahegan = true;
                        }
                        else if (character != "0")
                        {
                            result += a3[Convert.ToInt32(character)] + " و ";
                        }
                        break;
                    case 1://یک
                        if (isDahegan == true)
                        {
                            result += a2[Convert.ToInt32(character)];
                            isDahegan = false;
                        }
                        else
                        {
                            if (character != "0") result += a1[Convert.ToInt32(character)];
                            else result = result.TrimEnd('و', ' ');
                        }
                        break;
                }
            }
            return result;
        }
    }
}
