using System;

namespace APPPInCSharp_Payroll.Console
{
    public class ChangeHourlyTransaction : ChangeClassificationTransaction
    {
        private readonly double hourlyRate;

        public ChangeHourlyTransaction(int empId, double hourlyRate, PayrollDatabase database)
            : base(empId, database)
        {
            this.hourlyRate = hourlyRate;
        }

        protected override PaymentClassification Classification => new HourlyClassification(hourlyRate);

        protected override PaymentSchedule Schedule => new WeeklySchedule();
    }
}