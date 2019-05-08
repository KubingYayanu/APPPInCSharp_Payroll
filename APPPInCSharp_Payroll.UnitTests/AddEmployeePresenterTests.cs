using APPPInCSharp_Payroll.Core;
using APPPInCSharp_Payroll.WinForm;
using NUnit.Framework;

namespace APPPInCSharp_Payroll.UnitTests
{
    [TestFixture]
    public class AddEmployeePresenterTests
    {
        private AddEmployeePresenter presenter;
        private TransactionContainer container;
        private InMemoryPayrollDatabase database;
        private MockAddEmployeeView view;

        [SetUp]
        public void SetUp()
        {
            view = new MockAddEmployeeView();
            container = new TransactionContainer(null);
            database = new InMemoryPayrollDatabase();
            presenter = new AddEmployeePresenter(view, container, database);
        }

        [Test]
        public void Creation()
        {
            Assert.AreSame(container, presenter.TransactionContainer);
        }

        [Test]
        public void AllInfoIsCollected()
        {
            Assert.IsFalse(presenter.AllInfomationIsCollected());

            presenter.EmpId = 1;
            Assert.IsFalse(presenter.AllInfomationIsCollected());

            presenter.Name = "Kubing";
            Assert.IsFalse(presenter.AllInfomationIsCollected());

            presenter.Address = "123 abc";
            Assert.IsFalse(presenter.AllInfomationIsCollected());

            presenter.IsHourly = true;
            Assert.IsFalse(presenter.AllInfomationIsCollected());

            presenter.HourlyRate = 1.23;
            Assert.IsTrue(presenter.AllInfomationIsCollected());

            presenter.IsHourly = false;
            Assert.IsFalse(presenter.AllInfomationIsCollected());

            presenter.IsSalary = true;
            Assert.IsFalse(presenter.AllInfomationIsCollected());

            presenter.Salary = 1234;
            Assert.IsTrue(presenter.AllInfomationIsCollected());

            presenter.IsSalary = false;
            Assert.IsFalse(presenter.AllInfomationIsCollected());

            presenter.IsCommission = true;
            Assert.IsFalse(presenter.AllInfomationIsCollected());

            presenter.CommissionSalary = 123;
            Assert.IsFalse(presenter.AllInfomationIsCollected());

            presenter.Commission = 12;
            Assert.IsTrue(presenter.AllInfomationIsCollected());
        }

        [Test]
        public void ViewGetsUpdate()
        {
            presenter.EmpId = 1;
            CheckSubmitEnabled(false, 1);

            presenter.Name = "Kubing";
            CheckSubmitEnabled(false, 2);

            presenter.Address = "123 abc";
            CheckSubmitEnabled(false, 3);

            presenter.IsHourly = true;
            CheckSubmitEnabled(false, 4);

            presenter.HourlyRate = 1.23;
            CheckSubmitEnabled(true, 5);
        }

        private void CheckSubmitEnabled(bool expected, int count)
        {
            Assert.AreEqual(expected, view.submitEnabled);
            Assert.AreEqual(count, view.submitEnabledCount);
            view.submitEnabled = false;
        }

        [Test]
        public void CreatingTransaction()
        {
            presenter.EmpId = 123;
            presenter.Name = "Kubing";
            presenter.Address = "314 Elm";

            presenter.IsHourly = true;
            presenter.HourlyRate = 10;
            Assert.IsTrue(presenter.CreateTransaction() is AddHourlyEmployee);

            presenter.IsHourly = false;
            presenter.IsSalary = true;
            presenter.Salary = 3000;
            Assert.IsTrue(presenter.CreateTransaction() is AddSalariedEmployee);

            presenter.IsSalary = false;
            presenter.IsCommission = true;
            presenter.CommissionSalary = 1000;
            presenter.Commission = 25;
            Assert.IsTrue(presenter.CreateTransaction() is AddCommissionEmployee);
        }

        [Test]
        public void AddEmployee()
        {
            presenter.EmpId = 123;
            presenter.Name = "Kubing";
            presenter.Address = "314 Elm";
            presenter.IsHourly = true;
            presenter.HourlyRate = 25;

            presenter.AddEmployee();

            Assert.AreEqual(1, container.Transactions.Count);
            Assert.IsTrue(container.Transactions[0] is AddHourlyEmployee);
        }
    }
}