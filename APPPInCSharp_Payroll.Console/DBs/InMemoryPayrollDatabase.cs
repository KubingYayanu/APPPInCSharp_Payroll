using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace APPPInCSharp_Payroll.Console
{
    public class InMemoryPayrollDatabase : PayrollDatabase
    {
        private static Hashtable employees = new Hashtable();
        private static Hashtable members = new Hashtable();

        #region Employee

        public IList<int> GetAllEmployeeIds()
        {
            return employees.Keys.Cast<int>().ToList();
        }

        public void AddEmployee(Employee employee)
        {
            employees[employee.EmpId] = employee;
        }

        public Employee GetEmployee(int id)
        {
            return employees[id] as Employee;
        }

        public void DeleteEmployee(int id)
        {
            employees[id] = null;
        }

        #endregion Employee

        #region UnionMember

        public Employee GetUnionMember(int memberId)
        {
            var empId = Convert.ToInt32(members[memberId]);
            return employees[empId] as Employee;
        }

        public void AddUnionMember(int memberId, Employee e)
        {
            members[memberId] = e.EmpId;
        }

        public void RemoveUnionMember(int memberId)
        {
            var empId = Convert.ToInt32(members[memberId]);
            var employee = employees[empId] as Employee;
            employee.Affiliation = new NoAffiliation();
            members[memberId] = null;
        }

        #endregion UnionMember

        public void AddTimeCard(int empId, TimeCard tc)
        {
            var employee = employees[empId] as Employee;
            var hourlyClassification = employee.Classification as HourlyClassification;
            hourlyClassification.TimeCards[tc.Date] = tc;
            employees[empId] = employee;
        }

        public IList<TimeCard> GetTimeCards(int empId)
        {
            var employee = employees[empId] as Employee;
            var hourlyClassification = employee.Classification as HourlyClassification;
            var timeCards = new TimeCard[hourlyClassification.TimeCards.Count];
            hourlyClassification.TimeCards.Values.CopyTo(timeCards, 0);
            return timeCards.ToList();
        }

        public void AddSalesReceipt(int empId, SalesReceipt sr)
        {
            var employee = employees[empId] as Employee;
            var commissionedClassification = employee.Classification as CommissionedClassification;
            commissionedClassification.SalesReceipts[sr.Date] = sr;
            employees[empId] = employee;
        }

        public IList<SalesReceipt> GetSalesReceipts(int empId)
        {
            var employee = employees[empId] as Employee;
            var commissionedClassification = employee.Classification as CommissionedClassification;
            var salesReceipts = new SalesReceipt[commissionedClassification.SalesReceipts.Count];
            commissionedClassification.SalesReceipts.Values.CopyTo(salesReceipts, 0);
            return salesReceipts.ToList();
        }

        public void AddServiceCharge(int memberId, ServiceCharge sc)
        {
            var empId = Convert.ToInt32(members[memberId]);
            var employee = employees[empId] as Employee;
            var affiliation = employee.Affiliation as UnionAffiliation;
            affiliation.ServiceCharges[sc.Date] = sc;
        }

        public IList<ServiceCharge> GetServiceCharges(int memberId)
        {
            var empId = Convert.ToInt32(members[memberId]);
            var employee = employees[empId] as Employee;
            var affiliation = employee.Affiliation as UnionAffiliation;
            var serviceCharges = new ServiceCharge[affiliation.ServiceCharges.Count];
            affiliation.ServiceCharges.Values.CopyTo(serviceCharges, 0);

            return serviceCharges.ToList();
        }
    }
}