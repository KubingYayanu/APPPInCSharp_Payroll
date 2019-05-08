using System;
using System.Windows.Forms;

namespace APPPInCSharp_Payroll.WinForm
{
    public partial class AddEmployeeWindow : Form, AddEmployeeView
    {
        private AddEmployeePresenter presenter;

        public AddEmployeeWindow()
        {
            InitializeComponent();
        }

        public AddEmployeePresenter Presenter
        {
            get { return presenter; }
            set { presenter = value; }
        }

        public bool SubmitEnabled
        {
            set
            {
                submitButton.Enabled = value;
            }
        }

        private void hourlyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            hourlyRateTextBox.Enabled = hourlyRadioButton.Checked;
            presenter.IsHourly = hourlyRadioButton.Checked;
        }

        private void salaryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            salaryTextBox.Enabled = salaryRadioButton.Checked;
            presenter.IsSalary = salaryRadioButton.Checked;
        }

        private void commissionRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            commissionSalaryTextBox.Enabled = commissionRadioButton.Checked;
            commissionTextBox.Enabled = commissionRadioButton.Checked;
            presenter.IsCommission = commissionRadioButton.Checked;
        }

        private void empIdTextBox_TextChanged(object sender, EventArgs e)
        {
            presenter.EmpId = AsInt(empIdTextBox.Text);
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            presenter.Name = nameTextBox.Text;
        }

        private void addressTextBox_TextChanged(object sender, EventArgs e)
        {
            presenter.Address = addressTextBox.Text;
        }

        private void hourlyRateTextBox_TextChanged(object sender, EventArgs e)
        {
            presenter.HourlyRate = AsDouble(hourlyRateTextBox.Text);
        }

        private void salaryTextBox_TextChanged(object sender, EventArgs e)
        {
            presenter.Salary = AsDouble(salaryTextBox.Text);
        }

        private void commissionSalaryTextBox_TextChanged(object sender, EventArgs e)
        {
            presenter.CommissionSalary = AsDouble(commissionSalaryTextBox.Text);
        }

        private void commissionTextBox_TextChanged(object sender, EventArgs e)
        {
            presenter.Commission = AsDouble(commissionTextBox.Text);
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            presenter.AddEmployee();
            Close();
        }

        private int AsInt(string text)
        {
            try
            {
                return int.Parse(text);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private double AsDouble(string text)
        {
            try
            {
                return double.Parse(text);
            }
            catch (Exception)
            {
                return 0.0;
            }
        }
    }
}