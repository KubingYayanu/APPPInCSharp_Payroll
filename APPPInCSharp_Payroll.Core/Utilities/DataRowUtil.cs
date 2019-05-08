using System.Data;

namespace APPPInCSharp_Payroll.Core
{
    public static class DataRowUtil
    {
        public static DataRow ShuntRow(string columns, params object[] values)
        {
            var table = new DataTable();
            foreach (var columnName in columns.Split(','))
            {
                table.Columns.Add(columnName);
            }
            return table.Rows.Add(values);
        }
    }
}