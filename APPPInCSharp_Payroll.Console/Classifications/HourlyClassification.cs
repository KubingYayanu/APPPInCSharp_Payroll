using System;
using System.Collections;

namespace APPPInCSharp_Payroll.Console
{
    public class HourlyClassification : PaymentClassification
    {
        private Hashtable timeCards = new Hashtable();

        public double HourlyRate { get; }

        public HourlyClassification(double hourlyRate)
        {
            HourlyRate = hourlyRate;
        }

        public TimeCard GetTimeCard(DateTime date) => timeCards[date] as TimeCard;

        public void AddTimeCard(TimeCard tc)
        {
            timeCards[tc.Date] = tc;
        }

        public double CalculatePay(Paycheck paycheck)
        {
            throw new NotImplementedException();
        }
    }
}