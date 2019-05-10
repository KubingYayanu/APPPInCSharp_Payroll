using APPPInCSharp_Payroll.Core;
using System.Windows.Forms;

namespace APPPInCSharp_Payroll.WinForm
{
    public class WindowViewLoader : ViewLoader
    {
        public WindowViewLoader(PayrollDatabase database)
        {
            this.database = database;
        }

        private readonly PayrollDatabase database;
        private Form lastLoadedView;

        public Form LastLoadedView => lastLoadedView;

        public void LoadAddEmployeeView(TransactionContainer transactionContainer)
        {
            AddEmployeeWindow view = new AddEmployeeWindow();
            AddEmployeePresenter presenter = new AddEmployeePresenter(view, transactionContainer, database);
            view.Presenter = presenter;

            LoadView(view);
        }

        public void LoadPayrollView()
        {
            PayrollWindow view = new PayrollWindow();
            PayrollPresenter presenter = new PayrollPresenter(database, this);

            view.Presenter = presenter;
            presenter.View = view;

            LoadView(view);
        }

        private void LoadView(Form view)
        {
            view.Show();
            lastLoadedView = view;
        }
    }
}