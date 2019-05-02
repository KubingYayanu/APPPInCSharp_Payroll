using System;

namespace APPPInCSharp_Payroll.Console
{
    public class ServiceChargeTransaction : Transaction
    {
        private readonly int memberId;
        private readonly DateTime time;
        private readonly double charge;

        public ServiceChargeTransaction(int memberId, DateTime time, double charge, PayrollDatabase database)
            : base(database)
        {
            this.memberId = memberId;
            this.time = time;
            this.charge = charge;
        }

        public override void Execute()
        {
            Employee e = database.GetUnionMember(memberId);
            if (e != null)
            {
                UnionAffiliation ua = null;
                if (e.Affiliation is UnionAffiliation)
                {
                    ua = e.Affiliation as UnionAffiliation;
                }

                if (ua != null)
                {
                    database.AddServiceCharge(memberId, new ServiceCharge(time, charge));
                }
                else
                {
                    throw new InvalidOperationException("Tried to add service charge to union member without a union affiliation");
                }
            }
            else
            {
                throw new InvalidOperationException("No such union member");
            }
        }
    }
}