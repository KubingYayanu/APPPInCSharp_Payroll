using System;
using System.Collections;

namespace APPPInCSharp_Payroll.Console
{
    public class UnionAffiliation : Affiliation
    {
        public UnionAffiliation(int memberId, double dues)
        {
            MemberId = memberId;
            Dues = dues;
        }

        public Hashtable ServiceCharges { get; set; } = new Hashtable();

        public int MemberId { get; }

        public double Dues { get; }

        public double CalculateDeductions(Paycheck paycheck)
        {
            double totalDues = 0;
            totalDues = CalculateDues(paycheck) + CalculateServiceCharges(paycheck);
            return totalDues;
        }

        private double CalculateServiceCharges(Paycheck paycheck)
        {
            double totalServiceCharges = 0.0;
            foreach (ServiceCharge charge in ServiceCharges.Values)
            {
                bool isInPayPeriod = DateUtil.IsInPayPeriod(charge.Date, paycheck.PayPeriodStart, paycheck.PayPeriodEnd);
                if (isInPayPeriod)
                {
                    totalServiceCharges += charge.Amount;
                }
            }
            return totalServiceCharges;
        }

        private double CalculateDues(Paycheck paycheck)
        {
            double totalDues = 0.0;
            int fridays = NumberOfFridaysInPayPeriod(paycheck.PayPeriodStart, paycheck.PayPeriodEnd);
            totalDues = Dues * fridays;
            return totalDues;
        }

        private int NumberOfFridaysInPayPeriod(DateTime payPeriodStart, DateTime payPeriodEnd)
        {
            int fridays = 0;
            for (DateTime day = payPeriodStart; day <= payPeriodEnd; day = day.AddDays(1))
            {
                if (day.DayOfWeek == DayOfWeek.Friday)
                {
                    fridays++;
                }
            }
            return fridays;
        }
    }
}