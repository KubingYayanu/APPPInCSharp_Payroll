namespace APPPInCSharp_Payroll.Console
{
    public class ChangeMailTransaction : ChangeMethodTransaction
    {
        public ChangeMailTransaction(int empId)
            : base(empId)
        {
        }

        protected override PaymentMethod Method => new MailMethod();
    }
}