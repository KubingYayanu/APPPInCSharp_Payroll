namespace APPPInCSharp_Payroll.Console
{
    public abstract class ChangeMethodTransaction : ChangeEmployeeTransaction
    {
        public ChangeMethodTransaction(int empId)
            : base(empId)
        {
        }

        protected override void Change(Employee e)
        {
            e.Method = Method;
        }

        protected abstract PaymentMethod Method { get; }
    }
}