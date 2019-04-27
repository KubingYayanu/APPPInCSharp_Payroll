using System;
using System.Collections.Generic;
using System.Data;
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
            var operation = new LoadEmployeeOperation(id, connection);
            operation.Execute();
            var loadedEmployee = operation.Employee;

            return loadedEmployee;
        }

        public Employee GetUnionMember(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveUnionMember(int memberId)
        {
            throw new NotImplementedException();
        }

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

            var timeCards = new List<TimeCard> { };
            foreach (DataRow row in table.Rows)
            {
                double hours = double.Parse(row["Hours"].ToString());
                DateTime date = DateTime.Parse(row["Date"].ToString());
                timeCards.Add(new TimeCard(date, hours));
            }

            return timeCards;
        }
    }
}