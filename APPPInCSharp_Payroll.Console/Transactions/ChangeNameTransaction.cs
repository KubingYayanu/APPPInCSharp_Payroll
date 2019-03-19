namespace APPPInCSharp_Payroll.Console
{
    public class ChangeNameTransaction : ChangeEmployeeTransaction
    {
        private readonly string newName;

        public ChangeNameTransaction(int empId, string name)
            : base(empId)
        {
            newName = name;
        }

        protected override void Change(Employee e)
        {
            e.Name = newName;
        }
    }
}