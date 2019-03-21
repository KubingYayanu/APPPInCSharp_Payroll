﻿using System;
using System.Collections;

namespace APPPInCSharp_Payroll.Console
{
    public class UnionAffiliation : Affiliation
    {
        private Hashtable serviceCharges = new Hashtable();

        public int MemberId { get; }

        public double Dues { get; }

        public UnionAffiliation(int memberId, double dues)
        {
            MemberId = memberId;
            Dues = dues;
        }

        public void AddServiceCharge(ServiceCharge sc)
        {
            serviceCharges[sc.Date] = sc;
        }

        public ServiceCharge GetServiceCharge(DateTime date) => serviceCharges[date] as ServiceCharge;

        public double CalculateDeductions(Paycheck paycheck) => 0.0;
    }
}