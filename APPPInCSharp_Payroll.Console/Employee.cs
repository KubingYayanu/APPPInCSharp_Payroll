﻿namespace APPPInCSharp_Payroll.Console
{
    public class Employee
    {
        public int EmpId { get; }

        public string Name { get; set; }

        public string Address { get; }

        public PaymentClassification Classification { get; set; }

        public PaymentSchedule Schedule { get; set; }

        public PaymentMethod Method { get; set; }

        public Affiliation Affiliation { get; set; }

        public Employee(int empid, string name, string address)
        {
            EmpId = empid;
            Name = name;
            Address = address;
        }
    }
}