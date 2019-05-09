namespace APPPInCSharp_Payroll.Core
{
    public class MockTransaction : Transaction
    {
        public MockTransaction(PayrollDatabase database)
            : base(database)
        {
        }

        public bool wasExecuted;

        public override void Execute()
        {
            wasExecuted = true;
        }
    }
}