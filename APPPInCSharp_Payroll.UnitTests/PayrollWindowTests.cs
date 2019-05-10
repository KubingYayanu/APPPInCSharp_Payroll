using APPPInCSharp_Payroll.Core;
using APPPInCSharp_Payroll.WinForm;
using NUnit.Framework;

namespace APPPInCSharp_Payroll.UnitTests
{
    [TestFixture]
    public class PayrollWindowTests
    {
        private PayrollDatabase database;
        private ViewLoader viewLoader;
        private PayrollWindow window;
        private MockPayrollPresenter presenter;

        [SetUp]
        public void SetUp()
        {
            database = new InMemoryPayrollDatabase();
            viewLoader = new MockViewLoader();
            window = new PayrollWindow();
            presenter = new MockPayrollPresenter(database, viewLoader);
            window.Presenter = presenter;
            window.Show();
        }

        [TearDown]
        public void TearDown()
        {
            window.Dispose();
        }

        [Test]
        public void TransactionsText()
        {
            window.TransactionsText = "abc 123";
            Assert.AreEqual("abc 123", window.transactionsTextBox.Text);
        }

        [Test]
        public void EmployeesText()
        {
            window.EmployeesText = "some employees";
            Assert.AreEqual("some employees", window.employeesTextBox.Text);
        }

        [Test]
        public void AddEmployeeAction()
        {
            window.addEmployeeMenuItem.PerformClick();
            Assert.IsTrue(presenter.addEmployeeActionInvoked);
        }

        [Test]
        public void RunTransactions()
        {
            window.runButton.PerformClick();
            Assert.IsTrue(presenter.runTransactionCalled);
        }
    }
}