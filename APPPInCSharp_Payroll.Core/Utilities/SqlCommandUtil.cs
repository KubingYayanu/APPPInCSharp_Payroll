using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace APPPInCSharp_Payroll.Core
{
    public class SqlCommandUtil
    {
        public static SqlCommand CreateCommand<T>(string sql, T parameter) where T : new()
        {
            var command = new SqlCommand(sql);
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            foreach (var property in properties)
            {
                command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(parameter, null));
            }

            return command;
        }

        public static SqlCommand CreateCommand(string sql, string parameters, params object[] values)
        {
            var command = new SqlCommand(sql);
            var array = parameters.Split(',');
            for (int i = 0; i < array.Length; i++)
            {
                command.Parameters.AddWithValue($"@{array[i]}", values[i]);
            }

            return command;
        }
    }
}