namespace APPPInCSharp_Payroll.Console
{
    public class AddSalariedEmployee : AddEmployeeTransaction
    {
        private readonly double salary;

        public AddSalariedEmployee(int empid, string name, string address, double salary)
            : base(empid, name, address)
        {
            this.salary = salary;
        }

        protected override PaymentClassification MakeClassification() => new SalariedClassification(salary);

        protected override PaymentSchedule MakeSchedule() => new MonthlySchedule();
    }
}