namespace APPPInCSharp_Payroll.Core
{
    public class ChangeSalariedTransaction : ChangeClassificationTransaction
    {
        private readonly double salary;

        public ChangeSalariedTransaction(int empId, double salary, PayrollDatabase database)
            : base(empId, database)
        {
            this.salary = salary;
        }

        protected override PaymentClassification Classification => new SalariedClassification(salary);

        protected override PaymentSchedule Schedule => new MonthlySchedule();
    }
}