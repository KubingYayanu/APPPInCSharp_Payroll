using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.Console
{
    public class SqlPayrollDatabase : PayrollDatabase
    {
        private readonly SqlConnection connection;

        public SqlPayrollDatabase()
        {
            //Docker
            string connectionString = @"Initial Catalog=Payroll;Data Source=10.0.75.1;user id=dev;password=sa;";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void AddEmployee(Employee employee)
        {
            var operation = new SaveEmployeeOperation(employee, connection);
            operation.Execute();
        }

        public void AddUnionMember(int id, Employee e)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public IList<int> GetAllEmployeeIds()
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public Employee GetUnionMember(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveUnionMember(int memberId)
        {
            throw new NotImplementedException();
        }
    }
}