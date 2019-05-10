using APPPInCSharp_Payroll.Core;

namespace APPPInCSharp_Payroll.WinForm
{
    public class MockPayrollPresenter : PayrollPresenter
    {
        public MockPayrollPresenter(PayrollDatabase database, ViewLoader viewLoader)
            : base(database, viewLoader)
        {
        }

        public bool addEmployeeActionInvoked;
        public bool runTransactionCalled;

        public override void AddEmployeeActionInvoked()
        {
            addEmployeeActionInvoked = true;
        }

        public override void RunTransactions()
        {
            runTransactionCalled = true;
        }
    }
}