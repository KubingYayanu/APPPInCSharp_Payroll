using System;
using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.Console
{
    public class SaveUnionMemberOperaion : SaveOperation
    {
        public SaveUnionMemberOperaion(int memberId, Employee e, SqlConnection connection)
            : base(connection)
        {
            this.memberId = memberId;
            employee = e;
        }

        private readonly int memberId;
        private readonly Employee employee;
        private SqlCommand insertAffiliationCommand;
        private SqlCommand insertEmployeeAffiliationCommand;

        public void Execute()
        {
            PrepareToSaveAffiliation();
            PrepareToSaveEmployeeAffiliation();

            SqlTransaction transaction = connection.BeginTransaction("Save UnionMember");

            try
            {
                ExecuteTransaction(insertAffiliationCommand, transaction);
                ExecuteTransaction(insertEmployeeAffiliationCommand, transaction);
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }

        private void PrepareToSaveAffiliation()
        {
            string sql = @"insert into Affiliation (Id, Dues) values (@Id, @Dues)";
            var affiliation = employee.Affiliation as UnionAffiliation;
            insertAffiliationCommand = SqlCommandUtil.CreateCommand(sql, "Id,Dues", memberId, affiliation.Dues);
        }

        private void PrepareToSaveEmployeeAffiliation()
        {
            string sql = @"insert into EmployeeAffiliation (EmpId, AffiliationId)
                            values (@EmpId, @AffiliationId)";
            insertEmployeeAffiliationCommand = SqlCommandUtil.CreateCommand(sql, "EmpId,AffiliationId", employee.EmpId, memberId);
        }
    }
}