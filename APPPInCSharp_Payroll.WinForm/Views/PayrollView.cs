namespace APPPInCSharp_Payroll.WinForm
{
    public interface PayrollView
    {
        string TransactionsText { set; }
        string EmployeesText { set; }
        PayrollPresenter Presenter { set; }
    }
}