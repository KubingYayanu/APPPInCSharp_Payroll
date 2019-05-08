namespace APPPInCSharp_Payroll.Core
{
    public abstract class Transaction
    {
        public Transaction(PayrollDatabase database)
        {
            this.database = database;
        }

        protected readonly PayrollDatabase database;

        public abstract void Execute();
    }
}