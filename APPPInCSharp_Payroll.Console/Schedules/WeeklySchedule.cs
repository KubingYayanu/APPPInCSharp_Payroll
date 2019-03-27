using System;

namespace APPPInCSharp_Payroll.Console
{
    public class WeeklySchedule : PaymentSchedule
    {
        public DateTime GetPayPeriodStartDate(DateTime payDate)
        {
            return payDate.AddDays(-6);
        }

        public bool IsPayDate(DateTime payDate) => DayOfWeek.Friday == payDate.DayOfWeek;
    }
}