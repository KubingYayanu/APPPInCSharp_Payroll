namespace APPPInCSharp_Payroll.Console
{
    public class ChangeNameTransaction : ChangeEmployeeTransaction
    {
        private readonly string newName;

        public ChangeNameTransaction(int empId, string name, PayrollDatabase database)
            : base(empId, database)
        {
            newName = name;
        }

        protected override void Change(Employee e)
        {
            e.Name = newName;
        }
    }
}