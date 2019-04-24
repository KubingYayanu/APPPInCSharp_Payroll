using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace APPPInCSharp_Payroll.Console
{
    public class PayrollDatabase
    {
        private static Hashtable employees = new Hashtable();
        private static Hashtable members = new Hashtable();

        #region Employee

        public IList<int> GetAllEmployeeIds()
        {
            return employees.Keys.Cast<int>().ToList();
        }

        public void Addemployee(Employee employee)
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
            members[memberId] = null;
        }

        #endregion UnionMember
    }
}