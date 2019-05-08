using APPPInCSharp_Payroll.Core;
using System.Collections.Generic;

namespace APPPInCSharp_Payroll.WinForm
{
    public class TransactionContainer
    {
        public TransactionContainer(AddAction action)
        {
            addAction = action;
        }

        private IList<Transaction> transactions = new List<Transaction>();
        private AddAction addAction;

        public delegate void AddAction();

        public IList<Transaction> Transactions => transactions;

        public void Add(Transaction transaction)
        {
            transactions.Add(transaction);
            addAction?.Invoke();
        }

        public void Clear()
        {
            transactions.Clear();
        }
    }
}