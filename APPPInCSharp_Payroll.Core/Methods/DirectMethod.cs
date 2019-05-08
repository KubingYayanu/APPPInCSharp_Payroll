namespace APPPInCSharp_Payroll.Core
{
    public class DirectDepositMethod : PaymentMethod
    {
        public DirectDepositMethod()
        {
        }

        public DirectDepositMethod(string bank, string account)
        {
            Bank = bank;
            Account = account;
        }

        public string Bank { get; set; }

        public string Account { get; set; }

        public void Pay(Paycheck paycheck)
        {
        }
    }
}