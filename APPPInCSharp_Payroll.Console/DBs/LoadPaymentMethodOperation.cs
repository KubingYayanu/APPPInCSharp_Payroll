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

        private readonly string methodCode;
        private PaymentMethod method;

        public PaymentMethod Method => method;

        public void Execute()
        {
            Prepare();
            var row = LoadData(tableName, Command);
            InvokeCreateor(row);
        }

        public void Prepare()
        {
            if (methodCode.Equals("hold"))
            {
                instanceCreator = new InstanceCreator(CreateHoldMethod);
            }
            else if (methodCode.Equals("directdeposit"))
            {
                tableName = "DirectDepositAccount";
                instanceCreator = new InstanceCreator(CreateDirectDepositMethod);
            }
            else if (methodCode.Equals("mail"))
            {
                tableName = "PaycheckAddress";
                instanceCreator = new InstanceCreator(CreateMailMethod);
            }
        }

        private DataRow LoadData(string tableName, SqlCommand Command)
        {
            if (tableName != null)
            {
                return LoadDataRowFromCommand(Command);
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