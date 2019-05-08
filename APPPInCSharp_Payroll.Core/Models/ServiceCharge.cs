using System;

namespace APPPInCSharp_Payroll.Core
{
    public class ServiceCharge
    {
        public ServiceCharge()
        {
        }

        public ServiceCharge(DateTime date, double amount)
        {
            Date = date;
            Amount = amount;
        }

        public DateTime Date { get; set; }

        public double Amount { get; set; }
    }
}