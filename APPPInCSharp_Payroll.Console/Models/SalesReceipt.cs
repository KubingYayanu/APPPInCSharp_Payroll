using System;

namespace APPPInCSharp_Payroll.Console
{
    public class SalesReceipt
    {
        public DateTime Date { get; }

        public int Amount { get; }

        public SalesReceipt(DateTime date, int amount)
        {
            Date = date;
            Amount = amount;
        }
    }
}