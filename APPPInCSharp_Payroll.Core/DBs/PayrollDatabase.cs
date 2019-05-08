using System.Collections.Generic;

namespace APPPInCSharp_Payroll.Core
{
    public interface PayrollDatabase
    {
        IList<int> GetAllEmployeeIds();

        void AddEmployee(Employee employee);

        Employee GetEmployee(int id);

        void DeleteEmployee(int id);

        void AddUnionMember(int memberId, Employee e);

        Employee GetUnionMember(int id);

        void RemoveUnionMember(int memberId);

        void AddTimeCard(int empId, TimeCard tc);

        IList<TimeCard> GetTimeCards(int empId);

        void AddSalesReceipt(int empId, SalesReceipt sr);

        IList<SalesReceipt> GetSalesReceipts(int empId);

        void AddServiceCharge(int memberId, ServiceCharge sc);

        IList<ServiceCharge> GetServiceCharges(int memberId);
    }
}