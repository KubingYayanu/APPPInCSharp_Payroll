using System;

namespace APPPInCSharp_Payroll.Core
{
    public class NoAffiliation : Affiliation
    {
        public double CalculateDeductions(Paycheck paycheck) => 0.0;
    }
}