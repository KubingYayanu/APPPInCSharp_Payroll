using System;

namespace APPPInCSharp_Payroll.Console
{
    public class SalesReceipt
    {
        public SalesReceipt()
        {
        }

        public SalesReceipt(DateTime date, int amount)
        {
            Date = date;
            Amount = amount;
        }

        public DateTime Date { get; }

        public int Amount { get; }
    }
}