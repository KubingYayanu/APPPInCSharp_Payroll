using APPPInCSharp_Payroll.Core;
using NUnit.Framework;
using System;

namespace APPPInCSharp_Payroll.UnitTests
{
    [TestFixture]
    public class UtilitiesTests
    {
        #region DataRowUtil

        private class ObjectClass
        {
            public int Int { get; set; }

            public double Double { get; set; }

            public float Float { get; set; }

            public decimal Decimal { get; set; }

            public string String { get; set; }

            public char Char { get; set; }

            public bool Bool { get; set; }

            public DateTime DateTime { get; set; }
        }

        private class NullableObjectClass
        {
            public int? Int { get; set; }

            public double? Double { get; set; }

            public float? Float { get; set; }

            public decimal? Decimal { get; set; }

            public string String { get; set; }

            public char? Char { get; set; }

            public bool? Bool { get; set; }

            public DateTime? DateTime { get; set; }
        }

        [Test]
        public void DataTableToObjectList()
        {
            var row =
                DataRowUtil.ShuntRow("Int,Double,Float,Decimal,String,Char,Bool,DateTime"
                    , 1, 2.1d, 3.2f, 4.5m, "str", 'c', true, new DateTime(1995, 5, 3));

            var table = row.Table;
            var list = table.ToList<ObjectClass>();
            Assert.IsTrue(list.Count == 1);

            var obj = list[0];
            Assert.AreEqual(1, obj.Int);
            Assert.AreEqual(2.1d, obj.Double, .01);
            Assert.AreEqual(3.2f, obj.Float);
            Assert.AreEqual(4.5m, obj.Decimal);
            Assert.AreEqual("str", obj.String);
            Assert.AreEqual('c', obj.Char);
            Assert.IsTrue(obj.Bool);
            Assert.AreEqual(new DateTime(1995, 5, 3), obj.DateTime);
        }

        [Test]
        public void DataTableToNullableObjectList()
        {
            var row =
                DataRowUtil.ShuntRow("Int,Double,Float,Decimal,String,Char,Bool,DateTime"
                    , null, null, null, null, null, null, null, null);

            var table = row.Table;
            var list = table.ToList<NullableObjectClass>();
            Assert.IsTrue(list.Count == 1);

            var obj = list[0];
            Assert.IsNull(obj.Int);
            Assert.IsNull(obj.Double);
            Assert.IsNull(obj.Float);
            Assert.IsNull(obj.Decimal);
            Assert.IsNull(obj.String);
            Assert.IsNull(obj.Char);
            Assert.IsNull(obj.Bool);
            Assert.IsNull(obj.DateTime);
        }

        #endregion DataRowUtil

        #region SqlCommandUtil

        private class ParameterClass
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public double Amount { get; set; }

            public DateTime Date { get; set; }
        }

        [Test]
        public void CreateSqlCommandWithClass()
        {
            string sql = "select * from Test";
            var date = new DateTime(1995, 5, 4);
            var parameter = new ParameterClass
            {
                Id = 1,
                Amount = 2.2d,
                Name = "Kubing",
                Date = date
            };
            var command = SqlCommandUtil.CreateCommand(sql, parameter);

            Assert.AreEqual(sql, command.CommandText);
            Assert.AreEqual(1, int.Parse(command.Parameters["@Id"].Value.ToString()));
            Assert.AreEqual(2.2d, double.Parse(command.Parameters["@Amount"].Value.ToString()), .01);
            Assert.AreEqual("Kubing", command.Parameters["@Name"].Value);
            Assert.AreEqual(date, DateTime.Parse(command.Parameters["@Date"].Value.ToString()));
        }

        [Test]
        public void CreateSqlCommandWithParams()
        {
            string sql = "select * from Test";
            var date = new DateTime(1995, 5, 4);
            var param = new { Id = 1, Amount = 2.2d, Name = "Kubing", Date = date };
            var command = SqlCommandUtil.CreateCommand(sql, "Id,Amount,Name,Date", 1, 2.2d, "Kubing", date);

            Assert.AreEqual(sql, command.CommandText);
            Assert.AreEqual(1, int.Parse(command.Parameters["@Id"].Value.ToString()));
            Assert.AreEqual(2.2d, double.Parse(command.Parameters["@Amount"].Value.ToString()), .01);
            Assert.AreEqual("Kubing", command.Parameters["@Name"].Value);
            Assert.AreEqual(date, DateTime.Parse(command.Parameters["@Date"].Value.ToString()));
        }

        #endregion SqlCommandUtil
    }
}