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

        public static IList<int> GetAllEmployeeIds() => employees.Keys.Cast<int>().ToList();

        public static void AddEmployee(int id, Employee employee)
        {
            employees[id] = employee;
        }

        public static Employee GetEmployee(int id) => employees[id] as Employee;

        public static void DeleteEmployee(int id)
        {
            employees[id] = null;
        }

        public static Employee GetUnionMember(int memberId)
        {
            var empId = Convert.ToInt32(members[memberId]);
            return employees[empId] as Employee;
        }

        public static void AddUnionMember(int memberId, Employee e)
        {
            members[memberId] = e.EmpId;
        }

        public static void RemoveUnionMember(int memberId)
        {
            members[memberId] = null;
        }
    }
}