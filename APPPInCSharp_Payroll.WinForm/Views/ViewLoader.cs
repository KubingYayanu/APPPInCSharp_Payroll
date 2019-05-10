namespace APPPInCSharp_Payroll.WinForm
{
    public interface ViewLoader
    {
        void LoadPayrollView();

        void LoadAddEmployeeView(TransactionContainer transactionContainer);
    }
}