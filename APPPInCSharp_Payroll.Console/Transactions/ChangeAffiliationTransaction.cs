namespace APPPInCSharp_Payroll.Console
{
    public abstract class ChangeAffiliationTransaction : ChangeEmployeeTransaction
    {
        public ChangeAffiliationTransaction(int empId, PayrollDatabase database)
            : base(empId, database)
        {
        }

        protected override void Change(Employee e)
        {
            RecordMembership(e);
            e.Affiliation = Affiliation;
        }

        protected abstract Affiliation Affiliation { get; }

        protected abstract void RecordMembership(Employee e);
    }
}