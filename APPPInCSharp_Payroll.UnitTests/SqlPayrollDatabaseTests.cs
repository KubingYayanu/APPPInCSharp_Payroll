using APPPInCSharp_Payroll.Console;
using NUnit.Framework;
using System.Data;
using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.UnitTests
{
    [TestFixture]
    public class SqlPayrollDatabaseTests
    {
        private PayrollDatabase database;
        private SqlConnection connection;
        private Employee employee;

        [SetUp]
        public void SetUp()
        {
            database = new SqlPayrollDatabase();

            //Docker
            string connectionString = @"Initial Catalog=Payroll;Data Source=10.0.75.1;user id=dev;password=sa;";
            connection = new SqlConnection(connectionString);
            connection.Open();

            ClearPaycheckAddressTable();
            ClearDirectDepositAccountTable();
            ClearEmployeeTable();

            employee = new Employee(123, "Kubing", "123 Baker St.");
            employee.Schedule = new MonthlySchedule();
            employee.Method = new DirectDepositMethod("Bank 1", "123890");
            employee.Classification = new SalariedClassification(1000.00);
        }

        private void ClearEmployeeTable()
        {
            new SqlCommand("delete from Employee", connection).ExecuteNonQuery();
        }

        private void ClearDirectDepositAccountTable()
        {
            new SqlCommand("delete from DirectDepositAccount", connection).ExecuteNonQuery();
        }

        private void ClearPaycheckAddressTable()
        {
            new SqlCommand("delete from PaycheckAddress", connection).ExecuteNonQuery();
        }

        private DataTable LoadTable(string tableName)
        {
            string sql = $@"select * from {tableName}";
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            DataTable table = dataset.Tables["table"];

            return table;
        }

        [TearDown]
        public void TearDown()
        {
            connection.Close();
        }

        [Test]
        public void AddEmployee()
        {
            database.AddEmployee(employee);
            var table = LoadTable("Employee");
            Assert.AreEqual(1, table.Rows.Count);

            DataRow row = table.Rows[0];
            Assert.AreEqual(123, row["EmpId"]);
            Assert.AreEqual("Kubing", row["Name"]);
            Assert.AreEqual("123 Baker St.", row["Address"]);
        }

        [Test]
        public void ScheduleGetsSaved()
        {
            CheckSavedScheduleCode(new MonthlySchedule(), "monthly");
            ClearPaycheckAddressTable();
            ClearDirectDepositAccountTable();
            ClearEmployeeTable();
            CheckSavedScheduleCode(new WeeklySchedule(), "weekly");
            ClearPaycheckAddressTable();
            ClearDirectDepositAccountTable();
            ClearEmployeeTable();
            CheckSavedScheduleCode(new BiweeklySchedule(), "biweekly");
        }

        private void CheckSavedScheduleCode(PaymentSchedule schedule, string expectedCode)
        {
            employee.Schedule = schedule;
            database.AddEmployee(employee);

            var table = LoadTable("Employee");
            var row = table.Rows[0];
            Assert.AreEqual(expectedCode, row["ScheduleType"]);
        }

        [Test]
        public void PaymentMethodGetsSaved()
        {
            CheckSavedPaymentMethodCode(new HoldMethod(), "hold");
            ClearPaycheckAddressTable();
            ClearDirectDepositAccountTable();
            ClearEmployeeTable();
            CheckSavedPaymentMethodCode(new DirectDepositMethod("Bank -1", "0987654321"), "directdeposit");
            ClearPaycheckAddressTable();
            ClearDirectDepositAccountTable();
            ClearEmployeeTable();
            CheckSavedPaymentMethodCode(new MailMethod("111 Maple Ct."), "mail");
        }

        private void CheckSavedPaymentMethodCode(PaymentMethod method, string expectedCode)
        {
            employee.Method = method;
            database.AddEmployee(employee);

            var table = LoadTable("Employee");
            var row = table.Rows[0];
            Assert.AreEqual(expectedCode, row["PaymentMethodType"]);
        }

        [Test]
        public void DirectDepositMethodGetsSaved()
        {
            CheckSavedPaymentMethodCode(new DirectDepositMethod("Bank -1", "0987654321"), "directdeposit");
            var table = LoadTable("DirectDepositAccount");
            Assert.AreEqual(1, table.Rows.Count);

            var row = table.Rows[0];
            Assert.AreEqual("Bank -1", row["Bank"]);
            Assert.AreEqual("0987654321", row["Account"]);
            Assert.AreEqual(123, row["EmpId"]);
        }

        [Test]
        public void MailMethodGetsSaved()
        {
            CheckSavedPaymentMethodCode(new MailMethod("111 Maple Ct."), "mail");
            var table = LoadTable("PaycheckAddress");
            Assert.AreEqual(1, table.Rows.Count);

            var row = table.Rows[0];
            Assert.AreEqual("111 Maple Ct.", row["Address"]);
            Assert.AreEqual(123, row["EmpId"]);
        }
    }
}