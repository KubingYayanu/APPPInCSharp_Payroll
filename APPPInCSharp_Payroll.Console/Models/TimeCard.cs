using System;

namespace APPPInCSharp_Payroll.Console
{
    public class TimeCard
    {
        public DateTime Date { get; }

        public double Hours { get; }

        public TimeCard(DateTime date, double hours)
        {
            Date = date;
            Hours = hours;
        }
    }
}