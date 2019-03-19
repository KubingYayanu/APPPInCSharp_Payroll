using System;

namespace APPPInCSharp_Payroll.Console
{
    public class ServiceCharge
    {
        public DateTime Date { get; }

        public double Amount { get; }

        public ServiceCharge(DateTime date, double amount)
        {
            Date = date;
            Amount = amount;
        }
    }
}