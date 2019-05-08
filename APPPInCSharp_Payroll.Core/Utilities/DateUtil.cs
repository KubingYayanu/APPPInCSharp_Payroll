using System;

namespace APPPInCSharp_Payroll.Core
{
    public class DateUtil
    {
        public static bool IsInPayPeriod(DateTime theDate, DateTime startDate, DateTime endDate)
        {
            return (theDate >= startDate) && (theDate <= endDate);
        }
    }
}