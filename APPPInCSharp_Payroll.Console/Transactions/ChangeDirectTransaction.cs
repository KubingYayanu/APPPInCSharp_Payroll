namespace APPPInCSharp_Payroll.Console
{
    public class ChangeDirectTransaction : ChangeMethodTransaction
    {
        public ChangeDirectTransaction(int empId, PayrollDatabase database)
            : base(empId, database)
        {
        }

        protected override PaymentMethod Method => new DirectMethod();
    }
}