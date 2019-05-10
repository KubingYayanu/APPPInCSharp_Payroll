using APPPInCSharp_Payroll.Core;
using APPPInCSharp_Payroll.WinForm;
using NUnit.Framework;
using System;

namespace APPPInCSharp_Payroll.UnitTests
{
    [TestFixture]
    public class PayrollPresenterTests
    {
        private MockPayrollView view;
        private PayrollPresenter presenter;
        private PayrollDatabase database;
        private MockViewLoader viewLoader;

        [SetUp]
        public void SetUp()
        {
            view = new MockPayrollView();
            database = new InMemoryPayrollDatabase();
            viewLoader = new MockViewLoader();
            presenter = new PayrollPresenter(database, viewLoader);
            presenter.View = view;
        }

        [Test]
        public void Creation()
        {
            Assert.AreSame(view, presenter.View);
            Assert.AreSame(database, presenter.Database);
            Assert.IsNotNull(presenter.TransactionContainer);
        }

        [Test]
        public void AddAction()
        {
            TransactionContainer container = presenter.TransactionContainer;
            Transaction transaction = new MockTransaction(database);

            container.Add(transaction);

            string expected = transaction.ToString() + Environment.NewLine;

            Assert.AreEqual(expected, view.transactionsText);
        }

        [Test]
        public void AddEmployeeAction()
        {
            presenter.AddEmployeeActionInvoked();

            Assert.IsTrue(viewLoader.addEmployeeViewWasLoaded);
        }

        [Test]
        public void RunTransactions()
        {
            MockTransaction transaction = new MockTransaction(database);
            presenter.TransactionContainer.Add(transaction);
            Employee employee = new Employee(123, "Kubing", "123 Baker St.");
            database.AddEmployee(employee);

            presenter.RunTransactions();

            Assert.IsTrue(transaction.wasExecuted);
            Assert.AreEqual("", view.transactionsText);

            string expectedEmployeeTest = employee.ToString() + Environment.NewLine;
            Assert.AreEqual(expectedEmployeeTest, view.employeesText);
        }
    }
}