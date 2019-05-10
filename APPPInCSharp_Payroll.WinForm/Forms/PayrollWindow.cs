using System.Windows.Forms;

namespace APPPInCSharp_Payroll.WinForm
{
    public partial class PayrollWindow : Form, PayrollView
    {
        private PayrollPresenter presenter;

        public PayrollWindow()
        {
            InitializeComponent();
        }

        public string TransactionsText
        {
            set
            {
                transactionsTextBox.Text = value;
            }
        }

        public string EmployeesText
        {
            set
            {
                employeesTextBox.Text = value;
            }
        }

        public PayrollPresenter Presenter
        {
            get { return presenter; }
            set { presenter = value; }
        }

        private void addEmployeeMenuItem_Click(object sender, System.EventArgs e)
        {
            presenter.AddEmployeeActionInvoked();
        }

        private void runButton_Click(object sender, System.EventArgs e)
        {
            presenter.RunTransactions();
        }
    }
}