namespace APPPInCSharp_Payroll.Core
{
    public class ChangeCommissionedTransaction : ChangeClassificationTransaction
    {
        private readonly double salary;
        private readonly double commissionRate;

        public ChangeCommissionedTransaction(int empId, double salary, double commissionRate, PayrollDatabase database)
            : base(empId, database)
        {
            this.salary = salary;
            this.commissionRate = commissionRate;
        }

        protected override PaymentClassification Classification => new CommissionedClassification(salary, commissionRate);

        protected override PaymentSchedule Schedule => new BiweeklySchedule();
    }
}