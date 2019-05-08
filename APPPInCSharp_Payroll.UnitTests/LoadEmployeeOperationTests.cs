using APPPInCSharp_Payroll.Core;
using NUnit.Framework;
using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.UnitTests
{
    [TestFixture]
    public class LoadEmployeeOperationTests
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
            SqlCommand command = operation.Command;
            Assert.AreEqual("select * from Employee where EmpId = @EmpId", command.CommandText);
            Assert.AreEqual(123, command.Parameters["@EmpId"].Value);
        }

        [Test]
        public void LoadEmployeeData()
        {
            var row = DataRowUtil.ShuntRow("Name,Address", "Kubing", "10 Rue de Roi");
            operation.CreateEmployee(row);

            Assert.IsNotNull(operation.Employee);
            Assert.AreEqual("Kubing", operation.Employee.Name);
            Assert.AreEqual("10 Rue de Roi", operation.Employee.Address);
        }

        [Test]
        public void LoadingSchedules()
        {
            var row = DataRowUtil.ShuntRow("ScheduleType", "weekly");
            operation.AddSchedule(row);
            Assert.IsTrue(employee.Schedule is WeeklySchedule);

            row = DataRowUtil.ShuntRow("ScheduleType", "biweekly");
            operation.AddSchedule(row);
            Assert.IsTrue(employee.Schedule is BiweeklySchedule);

            row = DataRowUtil.ShuntRow("ScheduleType", "monthly");
            operation.AddSchedule(row);
            Assert.IsTrue(employee.Schedule is MonthlySchedule);
        }

        [Test]
        public void LoadingHoldMethod()
        {
            var row = DataRowUtil.ShuntRow("PaymentMethodType", "hold");
            operation.AddPaymentMethod(row);

            Assert.IsTrue(employee.Method is HoldMethod);
        }
    }
}