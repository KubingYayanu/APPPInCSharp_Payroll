namespace APPPInCSharp_Payroll.WinForm
{
    public class MockViewLoader : ViewLoader
    {
        public bool addEmployeeViewWasLoaded;
        private bool payrollViewWasLoaded;

        public void LoadAddEmployeeView(TransactionContainer transactionContainer)
        {
            addEmployeeViewWasLoaded = true;
        }

        public void LoadPayrollView()
        {
            payrollViewWasLoaded = true;
        }
    }
}