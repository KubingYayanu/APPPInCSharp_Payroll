namespace APPPInCSharp_Payroll.Core
{
    public class ChangeMailTransaction : ChangeMethodTransaction
    {
        public ChangeMailTransaction(int empId, PayrollDatabase database)
            : base(empId, database)
        {
        }

        protected override PaymentMethod Method => new MailMethod();
    }
}