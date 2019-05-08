using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.Core
{
    public abstract class SaveOperation
    {
        public SaveOperation(SqlConnection connection)
        {
            this.connection = connection;
        }

        protected readonly SqlConnection connection;

        protected void ExecuteTransaction(SqlCommand command, SqlTransaction transaction)
        {
            if (command != null)
            {
                command.Connection = connection;
                command.Transaction = transaction;
                command.ExecuteNonQuery();
            }
        }
    }
}