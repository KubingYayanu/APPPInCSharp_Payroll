namespace APPPInCSharp_Payroll.Console
{
    public class ChangeDirectTransaction : ChangeMethodTransaction
    {
        public ChangeDirectTransaction(int empId)
            : base(empId)
        {
        }

        protected override PaymentMethod Method => new DirectMethod();
    }
}