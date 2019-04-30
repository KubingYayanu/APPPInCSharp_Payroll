using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.Console
{
    public class SqlPayrollDatabase : PayrollDatabase
    {
        public SqlPayrollDatabase()
        {
            //Docker
            string connectionString = @"Initial Catalog=Payroll;Data Source=10.0.75.1;user id=dev;password=sa;";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        private readonly SqlConnection connection;

        #region Employee

        public void AddEmployee(Employee employee)
        {
            var operation = new SaveEmployeeOperation(employee, connection);
            operation.Execute();
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
            var operation = new LoadEmployeeOperation(id, connection);
            operation.Execute();
            var loadedEmployee = operation.Employee;

            return loadedEmployee;
        }

        #endregion Employee

        #region UnionMember

        public Employee GetUnionMember(int memberId)
        {
            throw new NotImplementedException();
        }

        public void AddUnionMember(int memberId, Employee employee)
        {
            var operation = new SaveUnionMemberOperaion(memberId, employee, connection);
            operation.Execute();
        }

        public void RemoveUnionMember(int memberId)
        {
            throw new NotImplementedException();
        }

        #endregion UnionMember

        public void AddTimeCard(int empId, TimeCard tc)
        {
            string sql = @"insert into TimeCard (EmpId, Date, Hours) values (@EmpId, @Date, @Hours)";
            var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EmpId", empId);
            command.Parameters.AddWithValue("@Date", tc.Date);
            command.Parameters.AddWithValue("@Hours", tc.Hours);
            command.ExecuteNonQuery();
        }

        public IList<TimeCard> GetTimeCards(int empId)
        {
            string sql = @"select * from TimeCard where EmpId = @EmpId";
            var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EmpId", empId);
            var adapter = new SqlDataAdapter(command);
            var dataset = new DataSet();
            adapter.Fill(dataset);
            var table = dataset.Tables["table"];
            var timeCards = table.ToList<TimeCard>();

            return timeCards;
        }

        public void AddSalesReceipt(int empId, SalesReceipt sr)
        {
            string sql = @"insert into SalesReceipt (EmpId, Date, Amount) values (@EmpId, @Date, @Amount)";
            var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EmpId", empId);
            command.Parameters.AddWithValue("@Date", sr.Date);
            command.Parameters.AddWithValue("@Amount", sr.Amount);
            command.ExecuteNonQuery();
        }

        public IList<SalesReceipt> GetSalesReceipts(int empId)
        {
            string sql = @"select * from SalesReceipt where EmpId = @EmpId";
            var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EmpId", empId);
            var adapter = new SqlDataAdapter(command);
            var dataset = new DataSet();
            adapter.Fill(dataset);
            var table = dataset.Tables["table"];
            var salesReceipts = table.ToList<SalesReceipt>();

            return salesReceipts;
        }
    }
}