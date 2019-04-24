namespace APPPInCSharp_Payroll.Console
{
    public class ChangeUnaffiliatedTransaction : ChangeAffiliationTransaction
    {
        public ChangeUnaffiliatedTransaction(int empId, PayrollDatabase database)
            : base(empId, database)
        {
        }

        protected override Affiliation Affiliation => new NoAffiliation();

        protected override void RecordMembership(Employee e)
        {
            Affiliation affiliation = e.Affiliation;
            if (affiliation is UnionAffiliation)
            {
                UnionAffiliation ua = affiliation as UnionAffiliation;
                int memberId = ua.MemberId;
                database.RemoveUnionMember(memberId);
            }
        }
    }
}