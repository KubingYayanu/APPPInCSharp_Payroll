﻿using APPPInCSharp_Payroll.WinForm;
using NUnit.Framework;

namespace APPPInCSharp_Payroll.UnitTests
{
    [TestFixture]
    public class AddEmployeeWindowTests
    {
        private AddEmployeeWindow window;
        private AddEmployeePresenter presenter;
        private TransactionContainer transactionContainer;

        [SetUp]
        public void SetUp()
        {
            window = new AddEmployeeWindow();
            transactionContainer = new TransactionContainer(null);
            presenter = new AddEmployeePresenter(window, transactionContainer, null);

            window.Presenter = presenter;
            window.Show();
        }

        [Test]
        public void StartingState()
        {
            Assert.AreSame(presenter, window.Presenter);
            Assert.IsFalse(window.submitButton.Enabled);
            Assert.IsFalse(window.hourlyRateTextBox.Enabled);
            Assert.IsFalse(window.salaryTextBox.Enabled);
            Assert.IsFalse(window.commissionSalaryTextBox.Enabled);
            Assert.IsFalse(window.commissionTextBox.Enabled);
        }

        [Test]
        public void PresenterValuesAreSet()
        {
            window.empIdTextBox.Text = "123";
            Assert.AreEqual(123, presenter.EmpId);

            window.nameTextBox.Text = "Kubing";
            Assert.AreEqual("Kubing", presenter.Name);

            window.addressTextBox.Text = "321 SomeWhere";
            Assert.AreEqual("321 SomeWhere", presenter.Address);

            window.hourlyRateTextBox.Text = "123.45";
            Assert.AreEqual(123.45, presenter.HourlyRate, .01);

            window.salaryTextBox.Text = "1234";
            Assert.AreEqual(1234, presenter.Salary, .01);

            window.commissionSalaryTextBox.Text = "123";
            Assert.AreEqual(123, presenter.CommissionSalary, .01);

            window.commissionTextBox.Text = "12.3";
            Assert.AreEqual(12.3, presenter.Commission, .01);

            window.hourlyRadioButton.PerformClick();
            Assert.IsTrue(presenter.IsHourly);

            window.salaryRadioButton.PerformClick();
            Assert.IsTrue(presenter.IsSalary);
            Assert.IsFalse(presenter.IsHourly);

            window.commissionRadioButton.PerformClick();
            Assert.IsTrue(presenter.IsCommission);
            Assert.IsFalse(presenter.IsSalary);
        }

        [Test]
        public void EnablingHourlyFields()
        {
            window.hourlyRadioButton.Checked = true;
            Assert.IsTrue(window.hourlyRateTextBox.Enabled);

            window.hourlyRadioButton.Checked = false;
            Assert.IsFalse(window.hourlyRateTextBox.Enabled);
        }

        [Test]
        public void EnablingSalaryFields()
        {
            window.salaryRadioButton.Checked = true;
            Assert.IsTrue(window.salaryTextBox.Enabled);

            window.salaryRadioButton.Checked = false;
            Assert.IsFalse(window.salaryTextBox.Enabled);
        }

        [Test]
        public void EnablingCommissionFields()
        {
            window.commissionRadioButton.Checked = true;
            Assert.IsTrue(window.commissionSalaryTextBox.Enabled);
            Assert.IsTrue(window.commissionTextBox.Enabled);

            window.commissionRadioButton.Checked = false;
            Assert.IsFalse(window.commissionSalaryTextBox.Enabled);
            Assert.IsFalse(window.commissionTextBox.Enabled);
        }

        [Test]
        public void EnablingAddEmployeeButton()
        {
            Assert.IsFalse(window.submitButton.Enabled);

            window.SubmitEnabled = true;
            Assert.IsTrue(window.submitButton.Enabled);

            window.SubmitEnabled = false;
            Assert.IsFalse(window.submitButton.Enabled);
        }

        [Test]
        public void AddEmployee()
        {
            window.empIdTextBox.Text = "123";
            window.nameTextBox.Text = "Kubing";
            window.addressTextBox.Text = "321 SomeWhere";
            window.hourlyRadioButton.Checked = true;
            window.hourlyRateTextBox.Text = "123.45";

            window.submitButton.PerformClick();
            Assert.IsFalse(window.Visible);
            Assert.AreEqual(1, transactionContainer.Transactions.Count);
        }
    }
}