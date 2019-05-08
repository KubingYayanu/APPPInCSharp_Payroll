namespace APPPInCSharp_Payroll.Core
{
    public class ChangeDirectTransaction : ChangeMethodTransaction
    {
        public ChangeDirectTransaction(int empId, PayrollDatabase database)
            : base(empId, database)
        {
        }

        protected override PaymentMethod Method => new DirectDepositMethod();
    }
}