namespace APPPInCSharp_Payroll.Console
{
    public class DeleteEmployeeTransaction : Transaction
    {
        private readonly int empid;

        public DeleteEmployeeTransaction(int empid)
        {
            this.empid = empid;
        }

        public void Execute()
        {
            PayrollDatabase.DeleteEmployee(empid);
        }
    }
}