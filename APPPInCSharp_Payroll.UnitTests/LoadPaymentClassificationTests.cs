using APPPInCSharp_Payroll.Console;
using NUnit.Framework;

namespace APPPInCSharp_Payroll.UnitTests
{
    [TestFixture]
    public class LoadPaymentClassificationTests
    {
        private Employee employee;
        private LoadPaymentClassificationOperation operation;

        [SetUp]
        public void SetUp()
        {
            employee = new Employee(123, "Kubing", "23 Pine Ct");
        }

        [Test]
        public void LoadSalariedClassificationCommand()
        {
            operation = new LoadPaymentClassificationOperation(employee, "salaried", null);
            operation.Prepare();
            var command = operation.Command;

            Assert.AreEqual("select * from SalariedClassification where EmpId = @EmpId", command.CommandText);
            Assert.AreEqual(employee.EmpId, command.Parameters["@EmpId"].Value);
        }

        [Test]
        public void LoadSalariedClassificationFromRow()
        {
            operation = new LoadPaymentClassificationOperation(employee, "salaried", null);
            operation.Prepare();
            var row = DataRowUtil.ShuntRow("Salary", 1000.00);
            operation.InvokeCreateor(row);

            var classification = operation.Classification;
            Assert.IsTrue(classification is SalariedClassification);

            var salariedClassification = classification as SalariedClassification;
            Assert.AreEqual(1000.00, salariedClassification.Salary, .01);
        }

        [Test]
        public void LoadCommissionedClassificationCommand()
        {
            operation = new LoadPaymentClassificationOperation(employee, "commissioned", null);
            operation.Prepare();
            var command = operation.Command;

            Assert.AreEqual("select * from CommissionedClassification where EmpId = @EmpId", command.CommandText);
            Assert.AreEqual(employee.EmpId, command.Parameters["@EmpId"].Value);
        }

        [Test]
        public void LoadHourlyClassificationCommand()
        {
            operation = new LoadPaymentClassificationOperation(employee, "hourly", null);
            operation.Prepare();
            var command = operation.Command;

            Assert.AreEqual("select * from HourlyClassification where EmpId = @EmpId", command.CommandText);
            Assert.AreEqual(employee.EmpId, command.Parameters["@EmpId"].Value);
        }
    }
}