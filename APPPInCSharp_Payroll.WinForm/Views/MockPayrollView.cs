namespace APPPInCSharp_Payroll.WinForm
{
    public class MockPayrollView : PayrollView
    {
        public string transactionsText;
        public string employeesText;
        public PayrollPresenter presenter;

        public string EmployeesText
        {
            set
            {
                employeesText = value;
            }
        }

        public PayrollPresenter Presenter
        {
            set
            {
                presenter = value;
            }
        }

        public string TransactionsText
        {
            set
            {
                transactionsText = value;
            }
        }
    }
}