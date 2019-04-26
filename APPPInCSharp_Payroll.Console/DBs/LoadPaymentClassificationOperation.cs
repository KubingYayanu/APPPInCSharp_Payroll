using System.Data;
using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.Console
{
    public class LoadPaymentClassificationOperation : LoadOperation
    {
        public LoadPaymentClassificationOperation(Employee employee, string classificationCode, SqlConnection connection)
            : base(connection)
        {
            this.employee = employee;
            this.classificationCode = classificationCode;
        }

        private readonly Employee employee;
        private readonly string classificationCode;
        private PaymentClassification classification;

        private delegate void PaymentClassificationCreator(DataRow row);

        private PaymentClassificationCreator paymentMethodCreator;
        private string tableName;

        public PaymentClassification Classification => classification;

        public SqlCommand Command
        {
            get
            {
                string sql = $@"select * from {tableName} where EmpId = @EmpId";
                var command = new SqlCommand(sql);
                command.Parameters.AddWithValue("@EmpId", employee.EmpId);
                return command;
            }
        }

        public void Execute()
        {
            Prepare();
            var row = LoadDataFromCommand(Command);
            CreatePaymentClassification(row);
        }

        public void CreatePaymentClassification(DataRow row)
        {
            paymentMethodCreator(row);
        }

        public void Prepare()
        {
            if (classificationCode.Equals("salaried"))
            {
                tableName = "SalariedClassification";
                paymentMethodCreator = new PaymentClassificationCreator(CreateSalariedClassification);
            }
            else if (classificationCode.Equals("commissioned"))
            {
                tableName = "CommissionedClassification";
                paymentMethodCreator = new PaymentClassificationCreator(CreateCommissionedClassification);
            }
            else if (classificationCode.Equals("hourly"))
            {
                tableName = "HourlyClassification";
                paymentMethodCreator = new PaymentClassificationCreator(CreateHourlyClassification);
            }
        }

        private void CreateSalariedClassification(DataRow row)
        {
            var salary = double.Parse(row["Salary"].ToString());
            classification = new SalariedClassification(salary);
        }

        private void CreateCommissionedClassification(DataRow row)
        {
            var salary = double.Parse(row["Salary"].ToString());
            var commissionRate = double.Parse(row["Commission"].ToString());
            classification = new CommissionedClassification(salary, commissionRate);
        }

        private void CreateHourlyClassification(DataRow row)
        {
            var hourlyRate = double.Parse(row["HourlyRate"].ToString());
            classification = new HourlyClassification(hourlyRate);
        }
    }
}