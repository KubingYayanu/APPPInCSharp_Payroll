using System;

namespace APPPInCSharp_Payroll.Console
{
    public class SalesReceiptTransaction : Transaction
    {
        private readonly DateTime date;
        private readonly int amount;
        private readonly int empId;

        public SalesReceiptTransaction(DateTime date, int amount, int empId, PayrollDatabase database)
            : base(database)
        {
            this.date = date;
            this.amount = amount;
            this.empId = empId;
        }

        public override void Execute()
        {
            Employee e = PayrollDatabase.Instance.GetEmployee(empId);

            if (e != null)
            {
                CommissionedClassification cc = e.Classification as CommissionedClassification;
                if (cc != null)
                {
                    cc.AddSalesReceipt(new SalesReceipt(date, amount));
                }
                else
                {
                    throw new InvalidOperationException("Tried to add SalesReceipt to non-commissioned employee");
                }
            }
            else
            {
                throw new InvalidOperationException("No such employee");
            }
        }
    }
}