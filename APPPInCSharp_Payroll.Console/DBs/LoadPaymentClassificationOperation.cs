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

        private readonly string classificationCode;
        private PaymentClassification classification;

        public PaymentClassification Classification => classification;

        public void Execute()
        {
            Prepare();
            var row = LoadDataFromCommand(Command);
            InvokeCreateor(row);
        }

        public void Prepare()
        {
            if (classificationCode.Equals("salaried"))
            {
                tableName = "SalariedClassification";
                instanceCreator = new InstanceCreator(CreateSalariedClassification);
            }
            else if (classificationCode.Equals("commissioned"))
            {
                tableName = "CommissionedClassification";
                instanceCreator = new InstanceCreator(CreateCommissionedClassification);
            }
            else if (classificationCode.Equals("hourly"))
            {
                tableName = "HourlyClassification";
                instanceCreator = new InstanceCreator(CreateHourlyClassification);
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