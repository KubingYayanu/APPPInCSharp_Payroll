using System;
using System.Collections;

namespace APPPInCSharp_Payroll.Console
{
    public class CommissionedClassification : PaymentClassification
    {
        private Hashtable salesReceipts = new Hashtable();

        public double Salary { get; }

        public double CommissionRate { get; }

        public CommissionedClassification(double salary, double commissionRate)
        {
            Salary = salary;
            CommissionRate = commissionRate;
        }

        public SalesReceipt GetSalesReceipt(DateTime date) => salesReceipts[date] as SalesReceipt;

        public void AddSalesReceipt(SalesReceipt sr)
        {
            salesReceipts[sr.Date] = sr;
        }

        public double CalculatePay(Paycheck paycheck)
        {
            double totalPay = Salary;
            foreach (SalesReceipt receipt in salesReceipts.Values)
            {
                bool isInPayPeriod = DateUtil.IsInPayPeriod(receipt.Date, paycheck.PayPeriodStart, paycheck.PayPeriodEnd);
                if (isInPayPeriod)
                {
                    totalPay += CalculatePayForSalesReceipt(receipt);
                }
            }
            return totalPay;
        }

        private double CalculatePayForSalesReceipt(SalesReceipt receipt) => receipt.Amount * CommissionRate;
    }
}