using System;

namespace APPPInCSharp_Payroll.Console
{
    public class WeeklySchedule : PaymentSchedule
    {
        public bool IsPayDate(DateTime payDate) => DayOfWeek.Friday == payDate.DayOfWeek;
    }
}