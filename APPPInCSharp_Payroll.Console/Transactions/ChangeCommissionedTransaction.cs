namespace APPPInCSharp_Payroll.Console
{
    public class ChangeCommissionedTransaction : ChangeClassificationTransaction
    {
        private readonly double salary;
        private readonly double commissionRate;

        public ChangeCommissionedTransaction(int empId, double salary, double commissionRate)
            : base(empId)
        {
            this.salary = salary;
            this.commissionRate = commissionRate;
        }

        protected override PaymentClassification Classification => new CommissionedClassification(salary, commissionRate);

        protected override PaymentSchedule Schedule => new BiweeklySchedule();
    }
}