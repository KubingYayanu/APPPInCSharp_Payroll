﻿namespace APPPInCSharp_Payroll.Core
{
    public class SalariedClassification : PaymentClassification
    {
        public double Salary { get; }

        public SalariedClassification(double salary)
        {
            Salary = salary;
        }

        public double CalculatePay(Paycheck paycheck) => Salary;
    }
}