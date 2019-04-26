using System.Data;
using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.Console
{
    public class LoadPaymentMethodOperation : LoadOperation
    {
        public LoadPaymentMethodOperation(Employee employee, string methodCode, SqlConnection connection)
            : base(connection)
        {
            this.employee = employee;
            this.methodCode = methodCode;
        }

        private readonly Employee employee;
        private readonly string methodCode;
        private PaymentMethod method;

        private delegate void PaymentMethodCreator(DataRow row);

        private PaymentMethodCreator paymentMethodCreator;
        private string tableName;

        public PaymentMethod Method => method;

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
            var row = LoadData(tableName, Command);
            CreatePaymentMethod(row);
        }

        public void CreatePaymentMethod(DataRow row)
        {
            paymentMethodCreator(row);
        }

        public void Prepare()
        {
            if (methodCode.Equals("hold"))
            {
                paymentMethodCreator = new PaymentMethodCreator(CreateHoldMethod);
            }
            else if (methodCode.Equals("directdeposit"))
            {
                tableName = "DirectDepositAccount";
                paymentMethodCreator = new PaymentMethodCreator(CreateDirectDepositMethod);
            }
            else if (methodCode.Equals("mail"))
            {
                tableName = "PaycheckAddress";
                paymentMethodCreator = new PaymentMethodCreator(CreateMailMethod);
            }
        }

        private DataRow LoadData(string tableName, SqlCommand Command)
        {
            if (tableName != null)
            {
                return LoadDataFromCommand(Command);
            }
            else
            {
                return null;
            }
        }

        private void CreateDirectDepositMethod(DataRow row)
        {
            string bank = row["Bank"].ToString();
            string account = row["Account"].ToString();
            method = new DirectDepositMethod(bank, account);
        }

        private void CreateMailMethod(DataRow row)
        {
            string address = row["Address"].ToString();
            method = new MailMethod(address);
        }

        private void CreateHoldMethod(DataRow row)
        {
            method = new HoldMethod();
        }
    }
}