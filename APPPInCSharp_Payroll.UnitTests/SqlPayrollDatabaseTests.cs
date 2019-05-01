using APPPInCSharp_Payroll.Console;
using NUnit.Framework;
using System;
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

            ClearAllTable();

            employee = new Employee(123, "Kubing", "123 Baker St.");
            employee.Schedule = new MonthlySchedule();
            employee.Method = new DirectDepositMethod("Bank 1", "123890");
            employee.Classification = new SalariedClassification(1000.00);
        }

        private void ClearAllTable()
        {
            new SqlCommand("delete from EmployeeAffiliation", connection).ExecuteNonQuery();
            new SqlCommand("delete from Affiliation", connection).ExecuteNonQuery();
            new SqlCommand("delete from SalesReceipt", connection).ExecuteNonQuery();
            new SqlCommand("delete from TimeCard", connection).ExecuteNonQuery();
            new SqlCommand("delete from HourlyClassification", connection).ExecuteNonQuery();
            new SqlCommand("delete from CommissionedClassification", connection).ExecuteNonQuery();
            new SqlCommand("delete from SalariedClassification", connection).ExecuteNonQuery();
            new SqlCommand("delete from PaycheckAddress", connection).ExecuteNonQuery();
            new SqlCommand("delete from DirectDepositAccount", connection).ExecuteNonQuery();
            new SqlCommand("delete from Employee", connection).ExecuteNonQuery();
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
        public void SaveIsTransactional()
        {
            var method = new DirectDepositMethod(null, null);
            employee.Method = method;
            try
            {
                database.AddEmployee(employee);
                Assert.Fail("An exception needs to occur for this test to work.");
            }
            catch (Exception)
            {
            }

            var table = LoadTable("Employee");
            Assert.AreEqual(0, table.Rows.Count);
        }

        #region Schedule

        [Test]
        public void ScheduleGetsSaved()
        {
            CheckSavedScheduleCode(new MonthlySchedule(), "monthly");
            ClearAllTable();
            CheckSavedScheduleCode(new WeeklySchedule(), "weekly");
            ClearAllTable();
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

        #endregion Schedule

        #region PaymentMethod

        [Test]
        public void PaymentMethodGetsSaved()
        {
            CheckSavedPaymentMethodCode(new HoldMethod(), "hold");
            ClearAllTable();
            CheckSavedPaymentMethodCode(new DirectDepositMethod("Bank -1", "0987654321"), "directdeposit");
            ClearAllTable();
            CheckSavedPaymentMethodCode(new MailMethod("111 Maple Ct."), "mail");
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

        [Test]
        public void SaveMailMethodThenHoldMethod()
        {
            employee.Method = new MailMethod("123 Baker St.");
            database.AddEmployee(employee);

            Employee employee2 = new Employee(321, "Ed", "456 Elm St.");
            employee2.Method = new HoldMethod();
            database.AddEmployee(employee2);

            var table = LoadTable("PaycheckAddress");
            Assert.AreEqual(1, table.Rows.Count);
        }

        private void CheckSavedPaymentMethodCode(PaymentMethod method, string expectedCode)
        {
            employee.Method = method;
            database.AddEmployee(employee);

            var table = LoadTable("Employee");
            var row = table.Rows[0];
            Assert.AreEqual(expectedCode, row["PaymentMethodType"]);
        }

        #endregion PaymentMethod

        #region PaymentClassification

        [Test]
        public void PaymentClassificationGetsSaved()
        {
            CheckSavedPaymentClassificationCode(new SalariedClassification(1000.00), "salaried");
            ClearAllTable();
            CheckSavedPaymentClassificationCode(new CommissionedClassification(2200.00, 18.00), "commissioned");
            ClearAllTable();
            CheckSavedPaymentClassificationCode(new HourlyClassification(150.00), "hourly");
        }

        [Test]
        public void SalariedClassificationGetsSaved()
        {
            CheckSavedPaymentClassificationCode(new SalariedClassification(1000.00), "salaried");
            var table = LoadTable("SalariedClassification");
            Assert.AreEqual(1, table.Rows.Count);

            var row = table.Rows[0];
            Assert.AreEqual(1000.00, double.Parse(row["Salary"].ToString()), .01);
            Assert.AreEqual(123, row["EmpId"]);
        }

        [Test]
        public void CommissionedClassificationGetsSaved()
        {
            CheckSavedPaymentClassificationCode(new CommissionedClassification(2200.00, 18.00), "commissioned");
            var table = LoadTable("CommissionedClassification");
            Assert.AreEqual(1, table.Rows.Count);

            var row = table.Rows[0];
            Assert.AreEqual(2200.00, double.Parse(row["Salary"].ToString()), .01);
            Assert.AreEqual(18.00, double.Parse(row["Commission"].ToString()), .01);
            Assert.AreEqual(123, row["EmpId"]);
        }

        [Test]
        public void HourlyClassificationGetsSaved()
        {
            CheckSavedPaymentClassificationCode(new HourlyClassification(150.00), "hourly");
            var table = LoadTable("HourlyClassification");
            Assert.AreEqual(1, table.Rows.Count);

            var row = table.Rows[0];
            Assert.AreEqual(150.00, double.Parse(row["HourlyRate"].ToString()), .01);
            Assert.AreEqual(123, row["EmpId"]);
        }

        private void CheckSavedPaymentClassificationCode(PaymentClassification classification, string expectedCode)
        {
            employee.Classification = classification;
            database.AddEmployee(employee);

            var table = LoadTable("Employee");
            var row = table.Rows[0];
            Assert.AreEqual(expectedCode, row["PaymentClassificationType"]);
        }

        #endregion PaymentClassification

        [Test]
        public void LoadSalariedEmployee()
        {
            employee.Schedule = new BiweeklySchedule();
            employee.Method = new DirectDepositMethod("1st Bank", "0123456");
            employee.Classification = new SalariedClassification(5432.10);
            database.AddEmployee(employee);

            Employee loadedEmployee = database.GetEmployee(123);
            Assert.AreEqual(123, loadedEmployee.EmpId);
            Assert.AreEqual(employee.Name, loadedEmployee.Name);
            Assert.AreEqual(employee.Address, loadedEmployee.Address);
            PaymentSchedule schedule = loadedEmployee.Schedule;
            Assert.IsTrue(schedule is BiweeklySchedule);

            PaymentMethod method = loadedEmployee.Method;
            Assert.IsTrue(method is DirectDepositMethod);

            DirectDepositMethod ddMethod = method as DirectDepositMethod;
            Assert.AreEqual("1st Bank", ddMethod.Bank);
            Assert.AreEqual("0123456", ddMethod.Account);

            PaymentClassification classification = loadedEmployee.Classification;
            Assert.IsTrue(classification is SalariedClassification);

            SalariedClassification salariedClassification = classification as SalariedClassification;
            Assert.AreEqual(5432.10, salariedClassification.Salary, .01);
        }

        [Test]
        public void LoadHourlyEmployeeOneTimeCard()
        {
            employee.Schedule = new WeeklySchedule();
            employee.Method = new DirectDepositMethod("1st Bank", "0123456");
            employee.Classification = new HourlyClassification(180.50);
            database.AddEmployee(employee);

            DateTime punchDate = new DateTime(2001, 11, 9); //Friday
            TimeCardTransaction tct = new TimeCardTransaction(punchDate, 9.0, 123, database);
            tct.Execute();

            Employee loadedEmployee = database.GetEmployee(123);
            PaymentClassification classification = loadedEmployee.Classification;
            Assert.IsTrue(classification is HourlyClassification);

            HourlyClassification hourlyClassification = classification as HourlyClassification;
            Assert.AreEqual(180.50, hourlyClassification.HourlyRate, .01);

            var timeCards = hourlyClassification.TimeCards;
            Assert.AreEqual(1, timeCards.Count);

            var tc = timeCards[punchDate] as TimeCard;
            Assert.IsNotNull(tc);
            Assert.AreEqual(9.0, tc.Hours);
        }

        [Test]
        public void LoadHourlyEmployeeTwoTimeCards()
        {
            employee.Schedule = new WeeklySchedule();
            employee.Method = new DirectDepositMethod("1st Bank", "0123456");
            employee.Classification = new HourlyClassification(180.50);
            database.AddEmployee(employee);

            DateTime payDate = new DateTime(2001, 11, 9); //Friday
            DateTime previousPayDate = payDate.AddDays(-7);
            TimeCardTransaction tct1 = new TimeCardTransaction(payDate, 9.0, 123, database);
            tct1.Execute();

            TimeCardTransaction tct2 = new TimeCardTransaction(previousPayDate, 5.0, 123, database);
            tct2.Execute();

            Employee loadedEmployee = database.GetEmployee(123);
            PaymentClassification classification = loadedEmployee.Classification;
            Assert.IsTrue(classification is HourlyClassification);

            HourlyClassification hourlyClassification = classification as HourlyClassification;
            Assert.AreEqual(180.50, hourlyClassification.HourlyRate, .01);

            var timeCards = hourlyClassification.TimeCards;
            Assert.AreEqual(2, timeCards.Count);

            var tc1 = timeCards[payDate] as TimeCard;
            Assert.IsNotNull(tc1);
            Assert.AreEqual(9.0, tc1.Hours);

            var tc2 = timeCards[previousPayDate] as TimeCard;
            Assert.IsNotNull(tc2);
            Assert.AreEqual(5.0, tc2.Hours);
        }

        [Test]
        public void LoadCommissionedClassificationOneSalesReceipt()
        {
            employee.Schedule = new BiweeklySchedule();
            employee.Method = new DirectDepositMethod("1st Bank", "0123456");
            employee.Classification = new CommissionedClassification(500.00, 80.30);
            database.AddEmployee(employee);

            var saleDay = new DateTime(2001, 11, 16);
            SalesReceiptTransaction srt = new SalesReceiptTransaction(saleDay, 3, 123, database);
            srt.Execute();

            Employee loadedEmployee = database.GetEmployee(123);
            PaymentClassification classification = loadedEmployee.Classification;
            Assert.IsTrue(classification is CommissionedClassification);

            CommissionedClassification commissionedClassification = classification as CommissionedClassification;
            Assert.AreEqual(500.00, commissionedClassification.Salary, .01);
            Assert.AreEqual(80.30, commissionedClassification.CommissionRate, .01);

            var salesReceipts = commissionedClassification.SalesReceipts;
            Assert.AreEqual(1, salesReceipts.Count);

            var sr = salesReceipts[saleDay] as SalesReceipt;
            Assert.IsNotNull(sr);
            Assert.AreEqual(3, sr.Amount);
        }

        [Test]
        public void UnionMemberGetsSaved()
        {
            database.AddEmployee(employee);

            int memberId = 7783;
            ChangeMemberTransaction cmt = new ChangeMemberTransaction(employee.EmpId, memberId, 99.42, database);
            cmt.Execute();

            var affiliationTable = LoadTable("Affiliation");
            var affiliationRow = affiliationTable.Rows[0];
            Assert.AreEqual(memberId, affiliationRow["Id"]);
            Assert.AreEqual(99.42, double.Parse(affiliationRow["Dues"].ToString()), .01);

            var employeeAffiliationTable = LoadTable("EmployeeAffiliation");
            var employeeAffiliationRow = employeeAffiliationTable.Rows[0];
            Assert.AreEqual(memberId, employeeAffiliationRow["AffiliationId"]);
            Assert.AreEqual(employee.EmpId, employeeAffiliationRow["EmpId"]);
        }

        [Test]
        public void LoadUnionMember()
        {
            database.AddEmployee(employee);

            int memberId = 7783;
            ChangeMemberTransaction cmt = new ChangeMemberTransaction(employee.EmpId, memberId, 99.42, database);
            cmt.Execute();

            var unionMember = database.GetUnionMember(memberId);
            Assert.AreEqual(employee.EmpId, unionMember.EmpId);
            Assert.AreEqual(employee.Name, unionMember.Name);
            Assert.AreEqual(employee.Address, unionMember.Address);

            var unionAffiliation = unionMember.Affiliation as UnionAffiliation;
            Assert.AreEqual(memberId, unionAffiliation.MemberId);
            Assert.AreEqual(99.42, unionAffiliation.Dues, .01);
        }
    }
}