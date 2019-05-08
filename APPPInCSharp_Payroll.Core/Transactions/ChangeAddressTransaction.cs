namespace APPPInCSharp_Payroll.Core
{
    public class ChangeAddressTransaction : ChangeEmployeeTransaction
    {
        private readonly string address;

        public ChangeAddressTransaction(int empId, string address, PayrollDatabase database)
            : base(empId, database)
        {
            this.address = address;
        }

        protected override void Change(Employee e)
        {
            e.Address = address;
        }
    }
}