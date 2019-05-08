using System;

namespace APPPInCSharp_Payroll.Core
{
    public interface PaymentSchedule
    {
        bool IsPayDate(DateTime payDate);

        DateTime GetPayPeriodStartDate(DateTime payDate);
    }
}