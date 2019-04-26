using APPPInCSharp_Payroll.Console;
using NUnit.Framework;
using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.UnitTests
{
    [TestFixture]
    public class LoadEmployeeOperationTests : LoadOperationTest
    {
        private LoadEmployeeOperation operation;
        private Employee employee;

        [SetUp]
        public void SetUp()
        {
            employee = new Employee(123, "Kubing", "10 Rue de Roi");
            operation = new LoadEmployeeOperation(123, null);

            operation.Employee = employee;
        }

        [Test]
        public void LoadingEmployeeDataCommand()
        {
            operation = new LoadEmployeeOperation(123, null);
            SqlCommand command = operation.LoadEmployeeCommand;
            Assert.AreEqual("select * from Employee where EmpId = @EmpId", command.CommandText);
            Assert.AreEqual(123, command.Parameters["@EmpId"].Value);
        }

        [Test]
        public void LoadEmployeeData()
        {
            var row = ShuntRow("Name,Address", "Kubing", "10 Rue de Roi");
            operation.CreateEmployee(row);

            Assert.IsNotNull(operation.Employee);
            Assert.AreEqual("Kubing", operation.Employee.Name);
            Assert.AreEqual("10 Rue de Roi", operation.Employee.Address);
        }

        [Test]
        public void LoadingSchedules()
        {
            var row = ShuntRow("ScheduleType", "weekly");
            operation.AddSchedule(row);
            Assert.IsTrue(employee.Schedule is WeeklySchedule);

            row = ShuntRow("ScheduleType", "biweekly");
            operation.AddSchedule(row);
            Assert.IsTrue(employee.Schedule is BiweeklySchedule);

            row = ShuntRow("ScheduleType", "monthly");
            operation.AddSchedule(row);
            Assert.IsTrue(employee.Schedule is MonthlySchedule);
        }

        [Test]
        public void LoadingHoldMethod()
        {
            var row = ShuntRow("PaymentMethodType", "hold");
            operation.AddPaymentMethod(row);

            Assert.IsTrue(employee.Method is HoldMethod);
        }
    }
}