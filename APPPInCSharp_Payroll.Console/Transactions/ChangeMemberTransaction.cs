﻿namespace APPPInCSharp_Payroll.Console
{
    public class ChangeMemberTransaction : ChangeAffiliationTransaction
    {
        private readonly int memberId;
        private readonly double dues;

        public ChangeMemberTransaction(int empId, int memberId, double dues, PayrollDatabase database)
            : base(empId, database)
        {
            this.memberId = memberId;
            this.dues = dues;
        }

        protected override Affiliation Affiliation => new UnionAffiliation(memberId, dues);

        protected override void RecordMembership(Employee e)
        {
            PayrollDatabase.Instance.AddUnionMember(memberId, e);
        }
    }
}