using System;

namespace APPPInCSharp_Payroll.Console
{
    public class Paycheck
    {
        public Paycheck(DateTime payDate)
        {
            PayDate = payDate;
        }

        public DateTime PayDate { get; set; }

        public double GrossPay { get; set; }

        public double Deductions { get; set; }

        public double NetPay { get; set; }

        public string GetField(string field) => "Hold";
    }
}