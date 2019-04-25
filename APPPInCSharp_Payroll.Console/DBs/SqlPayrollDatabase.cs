using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.Console
{
    public class SqlPayrollDatabase : PayrollDatabase
    {
        private readonly SqlConnection connection;
        private SqlCommand insertPaymentMethodCommand;
        private string methodCode;

        public SqlPayrollDatabase()
        {
            //Docker
            string connectionString = @"Initial Catalog=Payroll;Data Source=10.0.75.1;user id=dev;password=sa;";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void AddEmployee(Employee employee)
        {
            PrepareToSavePaymentMethod(employee);

            string sql = @"insert into Employee (EmpId, Name, Address, ScheduleType,
                                PaymentMethodType, PaymentClassificationType)
                            values (@EmpId, @Name, @Address, @ScheduleType,
                                @PaymentMethodType, @PaymentClassificationType)";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@EmpId", employee.EmpId);
            command.Parameters.AddWithValue("@Name", employee.Name);
            command.Parameters.AddWithValue("@Address", employee.Address);
            command.Parameters.AddWithValue("@ScheduleType", ScheduleCode(employee.Schedule));
            command.Parameters.AddWithValue("@PaymentMethodType", methodCode);
            command.Parameters.AddWithValue("@PaymentClassificationType", employee.Classification.GetType().Name);

            command.ExecuteNonQuery();

            if (insertPaymentMethodCommand != null)
            {
                insertPaymentMethodCommand.ExecuteNonQuery();
            }
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
                string sql = @"insert into DirectDepositAccount (Bank, Account, EmpId)
                                values (@Bank, @Account, @EmpId)";
                insertPaymentMethodCommand = new SqlCommand(sql, connection);
                insertPaymentMethodCommand.Parameters.AddWithValue("@Bank", ddMethod.Bank);
                insertPaymentMethodCommand.Parameters.AddWithValue("@Account", ddMethod.Account);
                insertPaymentMethodCommand.Parameters.AddWithValue("@EmpId", employee.EmpId);
            }
            else if (method is MailMethod)
            {
                methodCode = "mail";
                var mailMethod = method as MailMethod;
                string sql = @"insert into PaycheckAddress (Address, EmpId)
                                values (@Address, @EmpId)";
                insertPaymentMethodCommand = new SqlCommand(sql, connection);
                insertPaymentMethodCommand.Parameters.AddWithValue("@Address", mailMethod.Address);
                insertPaymentMethodCommand.Parameters.AddWithValue("@EmpId", employee.EmpId);
            }
            else
            {
                methodCode = "unknown";
            }
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

        public void AddUnionMember(int id, Employee e)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public IList<int> GetAllEmployeeIds()
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public Employee GetUnionMember(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveUnionMember(int memberId)
        {
            throw new NotImplementedException();
        }
    }
}