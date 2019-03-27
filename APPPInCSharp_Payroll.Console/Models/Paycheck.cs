using System;

namespace APPPInCSharp_Payroll.Console
{
    public class Paycheck
    {
        public Paycheck(DateTime payPeriodStart, DateTime payPeriodEnd)
        {
            PayPeriodStart = payPeriodStart;
            PayPeriodEnd = payPeriodEnd;
        }

        public DateTime PayPeriodStart { get; set; }

        public DateTime PayPeriodEnd { get; set; }

        public double GrossPay { get; set; }

        public double Deductions { get; set; }

        public double NetPay { get; set; }

        public string GetField(string field) => "Hold";
    }
}