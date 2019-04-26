using System.Data;

namespace APPPInCSharp_Payroll.UnitTests
{
    public abstract class LoadOperationTest
    {
        public DataRow ShuntRow(string columns, params object[] values)
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