using System;

namespace APPPInCSharp_Payroll.Core
{
    public class MonthlySchedule : PaymentSchedule
    {
        private bool IsLastDayOfMonth(DateTime date)
        {
            int m1 = date.Month;
            int m2 = date.AddDays(1).Month;
            return m1 != m2;
        }

        public bool IsPayDate(DateTime payDate) => IsLastDayOfMonth(payDate);

        public DateTime GetPayPeriodStartDate(DateTime payDate)
        {
            return payDate.AddDays(1).AddMonths(-1);
        }
    }
}