using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace APPPInCSharp_Payroll.Core
{
    public static class DataTableExtensions
    {
        public static IList<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            IList<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        public static IList<T> ToList<T>(this DataTable table, Dictionary<string, string> mappings) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            IList<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties, mappings);
                result.Add(item);
            }

            return result;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                var column = property.Name;
                MappingItem(property, row, item, column);
            }
            return item;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties, Dictionary<string, string> mappings) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                if (mappings.ContainsKey(property.Name))
                {
                    var column = mappings[property.Name];
                    MappingItem(property, row, item, column);
                }
            }
            return item;
        }

        private static void MappingItem<T>(PropertyInfo property, DataRow row, T item, string column)
        {
            Type type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
            var value = row.IsNull(column) ? null : row[column];
            object safeValue = (value == null) ? null : Convert.ChangeType(value, type);
            property.SetValue(item, safeValue, null);
        }
    }
}