namespace APPPInCSharp_Payroll.Core
{
    public abstract class ChangeMethodTransaction : ChangeEmployeeTransaction
    {
        public ChangeMethodTransaction(int empId, PayrollDatabase database)
            : base(empId, database)
        {
        }

        protected override void Change(Employee e)
        {
            e.Method = Method;
        }

        protected abstract PaymentMethod Method { get; }
    }
}