using System;

namespace APPPInCSharp_Payroll.Core
{
    public abstract class ChangeEmployeeTransaction : Transaction
    {
        private readonly int empId;

        public ChangeEmployeeTransaction(int empId, PayrollDatabase database)
            : base(database)
        {
            this.empId = empId;
        }

        public override void Execute()
        {
            Employee e = database.GetEmployee(empId);

            if (e != null)
            {
                Change(e);
            }
            else
            {
                throw new InvalidOperationException("No such employee");
            }
        }

        protected abstract void Change(Employee e);
    }
}