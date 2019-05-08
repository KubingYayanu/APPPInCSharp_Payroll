namespace APPPInCSharp_Payroll.Core
{
    public class AddSalariedEmployee : AddEmployeeTransaction
    {
        private readonly double salary;

        public AddSalariedEmployee(int empid, string name, string address, double salary, PayrollDatabase database)
            : base(empid, name, address, database)
        {
            this.salary = salary;
        }

        protected override PaymentClassification MakeClassification() => new SalariedClassification(salary);

        protected override PaymentSchedule MakeSchedule() => new MonthlySchedule();
    }
}