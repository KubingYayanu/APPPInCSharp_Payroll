using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.Core
{
    public class LoadSalesReceiptOperation : LoadOperation
    {
        public LoadSalesReceiptOperation(Employee employee, SqlConnection connection)
            : base(connection)
        {
            this.employee = employee;
        }

        private Hashtable salesReceipts = new Hashtable();

        public Hashtable SalesReceipts => salesReceipts;

        public void Execute()
        {
            Prepare();
            var table = LoadDataFromCommand(Command);
            CreateSalesReceipts(table);
        }

        public void Prepare()
        {
            tableName = "SalesReceipt";
        }

        public void CreateSalesReceipts(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                int amount = int.Parse(row["Amount"].ToString());
                DateTime date = DateTime.Parse(row["Date"].ToString());
                salesReceipts[date] = new SalesReceipt(date, amount);
            }
        }
    }
}