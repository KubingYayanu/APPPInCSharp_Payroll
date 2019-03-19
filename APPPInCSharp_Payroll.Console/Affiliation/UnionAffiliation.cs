using System;
using System.Collections;

namespace APPPInCSharp_Payroll.Console
{
    public class UnionAffiliation : Affiliation
    {
        private Hashtable serviceCharges = new Hashtable();

        public void AddServiceCharge(ServiceCharge sc)
        {
            serviceCharges[sc.Date] = sc;
        }

        public ServiceCharge GetServiceCharge(DateTime date)
        {
            return serviceCharges[date] as ServiceCharge;
        }
    }
}