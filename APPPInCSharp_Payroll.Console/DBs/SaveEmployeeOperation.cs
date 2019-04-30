using System;
using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.Console
{
    public class SaveEmployeeOperation : SaveOperation
    {
        public SaveEmployeeOperation(Employee employee, SqlConnection connection)
            : base(connection)
        {
            this.employee = employee;
        }

        private readonly Employee employee;
        private SqlCommand insertEmployeeCommand;
        private SqlCommand insertPaymentMethodCommand;
        private SqlCommand insertPaymentClassificationCommand;
        private string methodCode;
        private string classificationCode;

        public void Execute()
        {
            PrepareToSavePaymentClassification(employee);
            PrepareToSavePaymentMethod(employee);
            PrepareToSaveEmployee(employee);

            SqlTransaction transaction = connection.BeginTransaction("Save Employee");

            try
            {
                ExecuteTransaction(insertEmployeeCommand, transaction);
                ExecuteTransaction(insertPaymentMethodCommand, transaction);
                ExecuteTransaction(insertPaymentClassificationCommand, transaction);
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }

        private void PrepareToSaveEmployee(Employee employee)
        {
            string sql = @"insert into Employee (EmpId, Name, Address, ScheduleType,
                                PaymentMethodType, PaymentClassificationType)
                            values (@EmpId, @Name, @Address, @ScheduleType,
                                @PaymentMethodType, @PaymentClassificationType)";
            insertEmployeeCommand = new SqlCommand(sql);

            insertEmployeeCommand.Parameters.AddWithValue("@EmpId", employee.EmpId);
            insertEmployeeCommand.Parameters.AddWithValue("@Name", employee.Name);
            insertEmployeeCommand.Parameters.AddWithValue("@Address", employee.Address);
            insertEmployeeCommand.Parameters.AddWithValue("@ScheduleType", ScheduleCode(employee.Schedule));
            insertEmployeeCommand.Parameters.AddWithValue("@PaymentMethodType", methodCode);
            insertEmployeeCommand.Parameters.AddWithValue("@PaymentClassificationType", classificationCode);
        }

        private void PrepareToSavePaymentMethod(Employee employee)
        {
            PaymentMethod method = employee.Method;
            if (method is HoldMethod)
            {
                methodCode = "hold";
            }
            else if (method is DirectDepositMethod)
            {
                methodCode = "directdeposit";
                var ddMethod = method as DirectDepositMethod;
                insertPaymentMethodCommand = CreateInsertDirectDepoistCommand(ddMethod, employee);
            }
            else if (method is MailMethod)
            {
                methodCode = "mail";
                var mailMethod = method as MailMethod;
                insertPaymentMethodCommand = CreateInsertMailMethodCommand(mailMethod, employee);
            }
            else
            {
                methodCode = "unknown";
            }
        }

        private SqlCommand CreateInsertDirectDepoistCommand(DirectDepositMethod ddMethod, Employee employee)
        {
            string sql = @"insert into DirectDepositAccount (Bank, Account, EmpId)
                                values (@Bank, @Account, @EmpId)";
            var command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@Bank", ddMethod.Bank);
            command.Parameters.AddWithValue("@Account", ddMethod.Account);
            command.Parameters.AddWithValue("@EmpId", employee.EmpId);
            return command;
        }

        private SqlCommand CreateInsertMailMethodCommand(MailMethod mailMethod, Employee employee)
        {
            string sql = @"insert into PaycheckAddress (Address, EmpId)
                                values (@Address, @EmpId)";
            var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Address", mailMethod.Address);
            command.Parameters.AddWithValue("@EmpId", employee.EmpId);
            return command;
        }

        private void PrepareToSavePaymentClassification(Employee employee)
        {
            PaymentClassification classification = employee.Classification;
            if (classification is SalariedClassification)
            {
                classificationCode = "salaried";
                var salariedClassification = classification as SalariedClassification;
                insertPaymentClassificationCommand =
                    CreateInsertSalariedClassificationCommand(salariedClassification, employee);
            }
            else if (classification is CommissionedClassification)
            {
                classificationCode = "commissioned";
                var commissionedClassification = classification as CommissionedClassification;
                insertPaymentClassificationCommand =
                    CreateInsertCommissionedClassificationCommand(commissionedClassification, employee);
            }
            else if (classification is HourlyClassification)
            {
                classificationCode = "hourly";
                var hourlyClassification = classification as HourlyClassification;
                insertPaymentClassificationCommand =
                    CreateInsertHourlyClassificationCommand(hourlyClassification, employee);
            }
            else
            {
                classificationCode = "unknown";
            }
        }

        private SqlCommand CreateInsertSalariedClassificationCommand(SalariedClassification salariedClassification, Employee employee)
        {
            string sql = @"insert into SalariedClassification (Salary, EmpId)
                            values (@Salary, @EmpId)";
            var command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@Salary", salariedClassification.Salary);
            command.Parameters.AddWithValue("@EmpId", employee.EmpId);
            return command;
        }

        private SqlCommand CreateInsertCommissionedClassificationCommand(CommissionedClassification commissionedClassification, Employee employee)
        {
            string sql = @"insert into CommissionedClassification (Salary, Commission, EmpId)
                            values (@Salary, @Commission, @EmpId)";
            var command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@Salary", commissionedClassification.Salary);
            command.Parameters.AddWithValue("@Commission", commissionedClassification.CommissionRate);
            command.Parameters.AddWithValue("@EmpId", employee.EmpId);
            return command;
        }

        private SqlCommand CreateInsertHourlyClassificationCommand(HourlyClassification hourlyClassification, Employee employee)
        {
            string sql = @"insert into HourlyClassification (HourlyRate, EmpId)
                            values (@HourlyRate, @EmpId)";
            var command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@HourlyRate", hourlyClassification.HourlyRate);
            command.Parameters.AddWithValue("@EmpId", employee.EmpId);
            return command;
        }

        private string ScheduleCode(PaymentSchedule schedule)
        {
            if (schedule is MonthlySchedule)
            {
                return "monthly";
            }
            else if (schedule is WeeklySchedule)
            {
                return "weekly";
            }
            else if (schedule is BiweeklySchedule)
            {
                return "biweekly";
            }
            return "unknown";
        }
    }
}