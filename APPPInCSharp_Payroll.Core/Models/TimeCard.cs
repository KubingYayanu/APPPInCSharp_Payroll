using System;

namespace APPPInCSharp_Payroll.Core
{
    public class TimeCard
    {
        public TimeCard()
        {
        }

        public TimeCard(DateTime date, double hours)
        {
            Date = date;
            Hours = hours;
        }

        public DateTime Date { get; }

        public double Hours { get; }
    }
}