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
            double totalPay = 0.0;
            foreach (TimeCard timeCard in timeCards.Values)
            {
                if (IsInPayPeriod(timeCard, paycheck.PayDate))
                {
                    totalPay += CalculatePayForTimeCard(timeCard);
                }
            }
            return totalPay;
        }

        private bool IsInPayPeriod(TimeCard card, DateTime payPeriod)
        {
            DateTime payPeriodEndDate = payPeriod;
            DateTime payPeriodStartDate = payPeriod.AddDays(-5);

            return card.Date <= payPeriodEndDate && card.Date >= payPeriodStartDate;
        }

        private double CalculatePayForTimeCard(TimeCard card)
        {
            double overtimeHours = Math.Max(0.0, card.Hours - 8);
            double normalHours = card.Hours - overtimeHours;

            return HourlyRate * normalHours + HourlyRate * 1.5 * overtimeHours;
        }
    }
}