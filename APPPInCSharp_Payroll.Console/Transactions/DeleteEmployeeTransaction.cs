namespace APPPInCSharp_Payroll.Console
{
    public class DeleteEmployeeTransaction : Transaction
    {
        private readonly int empid;

        public DeleteEmployeeTransaction(int empid, PayrollDatabase database)
            : base(database)
        {
            this.empid = empid;
        }

        public override void Execute()
        {
            PayrollDatabase.Instance.DeleteEmployee(empid);
        }
    }
}