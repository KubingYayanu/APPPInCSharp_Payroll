using APPPInCSharp_Payroll.Core;
using System;
using System.Text;

namespace APPPInCSharp_Payroll.WinForm
{
    public class PayrollPresenter
    {
        public PayrollPresenter(
            PayrollDatabase database,
            ViewLoader viewLoader)
        {
            this.database = database;
            this.viewLoader = viewLoader;
            TransactionContainer.AddAction addAction = new TransactionContainer.AddAction(TransactionAdded);
            transactionContainer = new TransactionContainer(addAction);
        }

        private PayrollView view;
        private readonly PayrollDatabase database;
        private readonly ViewLoader viewLoader;
        private TransactionContainer transactionContainer;

        public PayrollView View
        {
            get { return view; }
            set { view = value; }
        }

        public PayrollDatabase Database => database;

        public TransactionContainer TransactionContainer => transactionContainer;

        public void TransactionAdded()
        {
            UpdateTransactionsTextBox();
        }

        private void UpdateTransactionsTextBox()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var transaction in transactionContainer.Transactions)
            {
                sb.Append(transaction.ToString());
                sb.Append(Environment.NewLine);
            }
            view.TransactionsText = sb.ToString();
        }

        public virtual void AddEmployeeActionInvoked()
        {
            viewLoader.LoadAddEmployeeView(transactionContainer);
        }

        public virtual void RunTransactions()
        {
            foreach (var transaction in transactionContainer.Transactions)
            {
                transaction.Execute();
            }

            transactionContainer.Clear();
            UpdateTransactionsTextBox();
            UpdateEmployeesTextBox();
        }

        private void UpdateEmployeesTextBox()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var employee in database.GetAllEmployees())
            {
                sb.Append(employee.ToString());
                sb.Append(Environment.NewLine);
            }
            view.EmployeesText = sb.ToString();
        }
    }
}