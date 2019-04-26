using System.Data;
using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.Console
{
    public abstract class LoadOperation
    {
        public LoadOperation(SqlConnection connection)
        {
            this.connection = connection;
        }

        protected SqlConnection connection;

        protected DataRow LoadDataFromCommand(SqlCommand command)
        {
            command.Connection = connection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            var table = dataset.Tables["table"];
            var row = table.Rows[0];

            return row;
        }
    }
}