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
    }
}
