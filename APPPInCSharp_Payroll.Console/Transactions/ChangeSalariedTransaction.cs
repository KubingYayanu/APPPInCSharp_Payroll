namespace APPPInCSharp_Payroll.Console
{
    public class ChangeSalariedTransaction : ChangeClassificationTransaction
    {
        private readonly double salary;

        public ChangeSalariedTransaction(int empId, double salary)
            : base(empId)
        {
            this.salary = salary;
        }

        protected override PaymentClassification Classification => new SalariedClassification(salary);

        protected override PaymentSchedule Schedule => new MonthlySchedule();
    }
}