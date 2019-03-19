using System.Collections;

namespace APPPInCSharp_Payroll.Console
{
    public class PayrollDatabase
    {
        private static Hashtable employees = new Hashtable();

        public static void AddEmployee(int id, Employee employee)
        {
            employees[id] = employee;
        }

        public static Employee GetEmployee(int id) => employees[id] as Employee;

        public static void DeleteEmployee(int id)
        {
            employees[id] = null;
        }
    }
}