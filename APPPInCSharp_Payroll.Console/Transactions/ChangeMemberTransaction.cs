namespace APPPInCSharp_Payroll.Console
{
    public class ChangeMemberTransaction : ChangeAffiliationTransaction
    {
        public ChangeMemberTransaction(int empId, int memberId, double dues, PayrollDatabase database)
            : base(empId, database)
        {
            this.memberId = memberId;
            this.dues = dues;
        }

        private readonly int memberId;
        private readonly double dues;

        protected override Affiliation Affiliation => new UnionAffiliation(memberId, dues);

        protected override void RecordMembership(Employee e)
        {
            database.AddUnionMember(memberId, e);
        }
    }
}