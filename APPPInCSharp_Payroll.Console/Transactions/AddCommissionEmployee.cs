﻿namespace APPPInCSharp_Payroll.Console
{
    public class AddCommissionEmployee : AddEmployeeTransaction
    {
        private double salary;
        private double commissionRate;

        public AddCommissionEmployee(int empid, string name
            , string address, double salary, double commissionRate)
            : base(empid, name, address)
        {
            this.salary = salary;
            this.commissionRate = commissionRate;
        }

        protected override PaymentClassification MakeClassification() => new CommissionedClassification(salary, commissionRate);

        protected override PaymentSchedule MakeSchedule() => new BiweeklySchedule();
    }
}