using System.Collections;

namespace APPPInCSharp_Payroll.Console
{
    public class CommissionedClassification : PaymentClassification
    {
        public CommissionedClassification(double salary, double commissionRate)
        {
            Salary = salary;
            CommissionRate = commissionRate;
        }

        public double Salary { get; }

        public double CommissionRate { get; }

        public Hashtable SalesReceipts { get; set; } = new Hashtable();

        public double CalculatePay(Paycheck paycheck)
        {
            double totalPay = Salary;
            foreach (SalesReceipt receipt in SalesReceipts.Values)
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