using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace APPPInCSharp_Payroll.Core
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
            string sql = @"delete from Employee where EmpId = @EmpId";
            var command = SqlCommandUtil.CreateCommand(sql, "EmpId", id);
            command.Connection = connection;
            command.ExecuteNonQuery();
        }

        public IList<int> GetAllEmployeeIds()
        {
            string sql = @"select EmpId from Employee";
            var command = new SqlCommand(sql, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            var table = dataset.Tables["table"];
            var list =
                table.AsEnumerable()
                     .Select(x => x.Field<int>("EmpId"))
                     .ToList();

            return list;
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
            var operation = new LoadUnionMemberOperation(memberId, connection);
            operation.Execute();
            var unionMember = operation.Employee;

            return unionMember;
        }

        public void AddUnionMember(int memberId, Employee employee)
        {
            var operation = new SaveUnionMemberOperaion(memberId, employee, connection);
            operation.Execute();
        }

        public void RemoveUnionMember(int memberId)
        {
            string sql = @"delete from Affiliation where Id = @Id";
            var command = SqlCommandUtil.CreateCommand(sql, "Id", memberId);
            command.Connection = connection;
            command.ExecuteNonQuery();
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

        public void AddServiceCharge(int memberId, ServiceCharge sc)
        {
            string sql = @"insert into ServiceCharge (AffiliationId, Date, Amount)
                            values (@AffiliationId, @Date, @Amount)";
            var command =
                SqlCommandUtil.CreateCommand(sql, "AffiliationId,Date,Amount",
                    memberId, sc.Date, sc.Amount);
            command.Connection = connection;
            command.ExecuteNonQuery();
        }

        public IList<ServiceCharge> GetServiceCharges(int memberId)
        {
            string sql = @"select * from ServiceCharge where AffiliationId = @AffiliationId";
            var command = SqlCommandUtil.CreateCommand(sql, "AffiliationId", memberId);
            command.Connection = connection;
            var adapter = new SqlDataAdapter(command);
            var dataset = new DataSet();
            adapter.Fill(dataset);
            var table = dataset.Tables["table"];
            var serviceCharges = table.ToList<ServiceCharge>();

            return serviceCharges;
        }
    }
}