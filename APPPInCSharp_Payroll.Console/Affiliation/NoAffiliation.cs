using System;

namespace APPPInCSharp_Payroll.Console
{
    public class NoAffiliation : Affiliation
    {
        public double CalculateDeductions(Paycheck paycheck) => 0.0;
    }
}