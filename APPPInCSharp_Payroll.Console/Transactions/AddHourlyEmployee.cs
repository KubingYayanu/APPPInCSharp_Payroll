namespace APPPInCSharp_Payroll.Console
{
    public class AddHourlyEmployee : AddEmployeeTransaction
    {
        private double hourlyRate;

        public AddHourlyEmployee(int empid, string name, string address, double hourlyRate, PayrollDatabase database)
            : base(empid, name, address, database)
        {
            this.hourlyRate = hourlyRate;
        }

        protected override PaymentClassification MakeClassification() => new HourlyClassification(hourlyRate);

        protected override PaymentSchedule MakeSchedule() => new WeeklySchedule();
    }
}