namespace APPPInCSharp_Payroll.Console
{
    public class ChangeHoldTransaction : ChangeMethodTransaction
    {
        public ChangeHoldTransaction(int empId)
            : base(empId)
        {
        }

        protected override PaymentMethod Method => new HoldMethod();
    }
}