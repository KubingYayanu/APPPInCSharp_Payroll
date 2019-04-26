using APPPInCSharp_Payroll.Console;
using NUnit.Framework;

namespace APPPInCSharp_Payroll.UnitTests
{
    [TestFixture]
    public class LoadPaymentMethodOperationTests : LoadOperationTest
    {
        private Employee employee;
        private LoadPaymentMethodOperation operation;

        [SetUp]
        public void SetUp()
        {
            employee = new Employee(123, "Kubing", "23 Pine Ct");
        }

        [Test]
        public void LoadHoldMethod()
        {
            operation = new LoadPaymentMethodOperation(employee, "hold", null);
            operation.Execute();
            var method = operation.Method;

            Assert.IsTrue(method is HoldMethod);
        }

        [Test]
        public void LoadDirectDepositMethodCommand()
        {
            operation = new LoadPaymentMethodOperation(employee, "directdeposit", null);
            operation.Prepare();
            var command = operation.Command;

            Assert.AreEqual("select * from DirectDepositAccount where EmpId = @EmpId", command.CommandText);
            Assert.AreEqual(employee.EmpId, command.Parameters["@EmpId"].Value);
        }

        [Test]
        public void CreateDirectDepositMethodFromRow()
        {
            operation = new LoadPaymentMethodOperation(employee, "directdeposit", null);
            operation.Prepare();
            var row = ShuntRow("Bank,Account", "1st Bank", "0123456");
            operation.InvokeCreateor(row);

            var method = operation.Method;
            Assert.IsTrue(method is DirectDepositMethod);

            var ddMethod = method as DirectDepositMethod;
            Assert.AreEqual("1st Bank", ddMethod.Bank);
            Assert.AreEqual("0123456", ddMethod.Account);
        }

        [Test]
        public void LoadMailMethodCommand()
        {
            operation = new LoadPaymentMethodOperation(employee, "mail", null);
            operation.Prepare();
            var command = operation.Command;

            Assert.AreEqual("select * from PaycheckAddress where EmpId = @EmpId", command.CommandText);
            Assert.AreEqual(employee.EmpId, command.Parameters["@EmpId"].Value);
        }

        [Test]
        public void LoadMailMethodFromRow()
        {
            operation = new LoadPaymentMethodOperation(employee, "mail", null);
            operation.Prepare();
            var row = ShuntRow("Address", "23 Pine Ct");
            operation.InvokeCreateor(row);

            var method = operation.Method;
            Assert.IsTrue(method is MailMethod);

            var mailMethod = method as MailMethod;
            Assert.AreEqual("23 Pine Ct", mailMethod.Address);
        }
    }
}