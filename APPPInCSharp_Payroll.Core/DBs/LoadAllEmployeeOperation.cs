using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.Core
{
    public class LoadAllEmployeeOperation : LoadOperation
    {
        public LoadAllEmployeeOperation(SqlConnection connection)
            : base(connection)
        {
        }

        private int empId;

        private IList<Employee> employees;

        internal IList<Employee> Employees
        {
            get { return employees; }
            set { employees = value; }
        }

        public override SqlCommand Command
        {
            get
            {
                string sql = @"select * from Employee";
                SqlCommand command = new SqlCommand(sql, connection);
                return command;
            }
        }

        public void Execute()
        {
            var table = LoadDataFromCommand(Command);

            if (table != null && table.Rows.Count != 0)
                return;

            foreach (DataRow row in table.Rows)
            {
                CreateEmployee(row);
                AddSchedule(row);
                AddPaymentMethod(row);
                AddPaymentClassification(row);
                AddAffiliation();
            }
        }

        public void CreateEmployee(DataRow row)
        {
            empId = int.Parse(row["EmpId"].ToString());
            string name = row["Name"].ToString();
            string address = row["Address"].ToString();
            employee = new Employee(empId, name, address);
        }

        public void AddSchedule(DataRow row)
        {
            string scheduleType = row["ScheduleType"].ToString();
            if (scheduleType.Equals("weekly"))
            {
                employee.Schedule = new WeeklySchedule();
            }
            else if (scheduleType.Equals("biweekly"))
            {
                employee.Schedule = new BiweeklySchedule();
            }
            else if (scheduleType.Equals("monthly"))
            {
                employee.Schedule = new MonthlySchedule();
            }
        }

        public void AddPaymentMethod(DataRow row)
        {
            string methodCode = row["PaymentMethodType"].ToString();
            var operation = new LoadPaymentMethodOperation(employee, methodCode, connection);
            operation.Execute();
            employee.Method = operation.Method;
        }

        public void AddPaymentClassification(DataRow row)
        {
            string classificationCode = row["PaymentClassificationType"].ToString();
            var operation = new LoadPaymentClassificationOperation(employee, classificationCode, connection);
            operation.Execute();
            employee.Classification = operation.Classification;
        }

        public void AddAffiliation()
        {
            var operation = new LoadAffiliationOperation(empId, connection);
            operation.Execute();
            employee.Affiliation = operation.Affiliation;
        }
    }
}