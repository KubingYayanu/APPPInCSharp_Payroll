namespace APPPInCSharp_Payroll.Core
{
    public class ChangeHoldTransaction : ChangeMethodTransaction
    {
        public ChangeHoldTransaction(int empId, PayrollDatabase database)
            : base(empId, database)
        {
        }

        protected override PaymentMethod Method => new HoldMethod();
    }
}