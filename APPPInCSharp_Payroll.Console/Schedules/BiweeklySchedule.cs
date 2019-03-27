using System;
using System.Globalization;

namespace APPPInCSharp_Payroll.Console
{
    public class BiweeklySchedule : PaymentSchedule
    {
        public DateTime GetPayPeriodStartDate(DateTime payDate) => payDate.AddDays(-13);

        public bool IsPayDate(DateTime payDate)
        {
            var cal = new GregorianCalendar();
            var weekNum = cal.GetWeekOfYear(payDate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            bool isBiweek = weekNum % 2 == 0;
            bool isFriday = DayOfWeek.Friday == payDate.DayOfWeek;

            return isBiweek && isFriday;
        }
    }
}