using System;

namespace APPPInCSharp_Payroll.Console
{
    public interface PaymentSchedule
    {
        bool IsPayDate(DateTime payDate);

        DateTime GetPayPeriodStartDate(DateTime payDate);
    }
}