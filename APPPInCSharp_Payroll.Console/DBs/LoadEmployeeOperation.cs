using System.Data;
using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.Console
{
    public class LoadEmployeeOperation : LoadOperation
    {
        public LoadEmployeeOperation(int empId, SqlConnection connection)
            : base(connection)
        {
            this.empId = empId;
        }

        private readonly int empId;
        private Employee employee;

        internal Employee Employee
        {
            get { return employee; }
            set { employee = value; }
        }

        public SqlCommand LoadEmployeeCommand
        {
            get
            {
                string sql = @"select * from Employee where EmpId = @EmpId";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@EmpId", empId);
                return command;
            }
        }

        public void Execute()
        {
            var row = LoadDataFromCommand(LoadEmployeeCommand);

            CreateEmployee(row);
            AddSchedule(row);
            AddPaymentMethod(row);
            AddPaymentClassification(row);
        }

        public void CreateEmployee(DataRow row)
        {
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
    }
}