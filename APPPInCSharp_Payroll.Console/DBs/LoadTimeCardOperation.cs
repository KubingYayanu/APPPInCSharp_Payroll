using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.Console
{
    public class LoadTimeCardOperation : LoadOperation
    {
        public LoadTimeCardOperation(Employee employee, SqlConnection connection) :
            base(connection)
        {
            this.employee = employee;
        }

        private Hashtable timeCards = new Hashtable();

        public Hashtable TimeCards => timeCards;

        public void Execute()
        {
            Prepare();
            var table = LoadDataFromCommand(Command);
            CreateTimeCards(table);
        }

        public void Prepare()
        {
            tableName = "TimeCard";
        }

        public void CreateTimeCards(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                double hours = double.Parse(row["Hours"].ToString());
                DateTime date = DateTime.Parse(row["Date"].ToString());
                timeCards[date] = new TimeCard(date, hours);
            }
        }
    }
}