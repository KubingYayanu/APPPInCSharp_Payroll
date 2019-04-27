using System;
using System.Collections.Generic;

namespace APPPInCSharp_Payroll.Console
{
    public interface PayrollDatabase
    {
        IList<int> GetAllEmployeeIds();

        void AddEmployee(Employee employee);

        Employee GetEmployee(int id);

        void DeleteEmployee(int id);

        void AddUnionMember(int id, Employee e);

        Employee GetUnionMember(int id);

        void RemoveUnionMember(int memberId);

        void AddTimeCard(int empId, TimeCard tc);

        IList<TimeCard> GetTimeCards(int empId);
    }
}