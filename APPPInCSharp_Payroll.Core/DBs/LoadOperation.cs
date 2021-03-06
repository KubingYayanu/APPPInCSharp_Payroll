﻿using System.Data;
using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.Core
{
    public abstract class LoadOperation
    {
        public LoadOperation(SqlConnection connection)
        {
            this.connection = connection;
        }

        protected delegate void InstanceCreator(DataRow row);

        protected Employee employee;
        protected SqlConnection connection;
        protected InstanceCreator instanceCreator;
        protected string tableName;

        public virtual SqlCommand Command
        {
            get
            {
                string sql = $@"select * from {tableName} where EmpId = @EmpId";
                var command = new SqlCommand(sql);
                command.Parameters.AddWithValue("@EmpId", employee.EmpId);
                return command;
            }
        }

        public void InvokeCreateor(DataRow row)
        {
            instanceCreator(row);
        }

        protected DataRow LoadDataRowFromCommand(SqlCommand command)
        {
            command.Connection = connection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            var table = dataset.Tables["table"];
            var row = table.Rows.Count == 0 ? null : table.Rows[0];

            return row;
        }

        protected DataTable LoadDataFromCommand(SqlCommand command)
        {
            command.Connection = connection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            var table = dataset.Tables["table"];

            return table;
        }
    }
}