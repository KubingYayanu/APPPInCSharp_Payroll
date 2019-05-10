using APPPInCSharp_Payroll.Core;
using APPPInCSharp_Payroll.WinForm;
using NUnit.Framework;
using System.Collections.Generic;

namespace APPPInCSharp_Payroll.UnitTests
{
    [TestFixture]
    public class TransactionContainerTests
    {
        private PayrollDatabase database;
        private TransactionContainer container;
        private bool addActionCalled;
        private Transaction transaction;

        [SetUp]
        public void SetUp()
        {
            database = new InMemoryPayrollDatabase();
            TransactionContainer.AddAction action = new TransactionContainer.AddAction(SillyAddAction);
            container = new TransactionContainer(action);
            transaction = new MockTransaction(database);
        }

        [Test]
        public void Construction()
        {
            Assert.AreEqual(0, container.Transactions.Count);
        }

        [Test]
        public void AddingTransaction()
        {
            container.Add(transaction);

            IList<Transaction> transactions = container.Transactions;
            Assert.AreEqual(1, transactions.Count);
            Assert.AreSame(transaction, transactions[0]);
        }

        [Test]
        public void AddingTransactionTriggersDelegate()
        {
            container.Add(transaction);
            Assert.IsTrue(addActionCalled);
        }

        private void SillyAddAction()
        {
            addActionCalled = true;
        }
    }
}