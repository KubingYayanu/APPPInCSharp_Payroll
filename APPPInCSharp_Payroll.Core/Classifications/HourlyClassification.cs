using System;
using System.Collections;

namespace APPPInCSharp_Payroll.Core
{
    public class HourlyClassification : PaymentClassification
    {
        public HourlyClassification(double hourlyRate)
        {
            HourlyRate = hourlyRate;
        }

        public double HourlyRate { get; }

        public Hashtable TimeCards { get; set; } = new Hashtable();

        public double CalculatePay(Paycheck paycheck)
        {
            double totalPay = 0.0;
            foreach (TimeCard timeCard in TimeCards.Values)
            {
                bool isInPayPeriod = DateUtil.IsInPayPeriod(timeCard.Date, paycheck.PayPeriodStart, paycheck.PayPeriodEnd);
                if (isInPayPeriod)
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