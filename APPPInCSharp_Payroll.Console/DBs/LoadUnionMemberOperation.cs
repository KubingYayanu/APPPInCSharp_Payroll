using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.Console
{
    public class LoadUnionMemberOperation : LoadOperation
    {
        public LoadUnionMemberOperation(int memberId, SqlConnection connection)
            : base(connection)
        {
            this.memberId = memberId;
        }

        private readonly int memberId;

        internal Employee Employee
        {
            get { return employee; }
            set { employee = value; }
        }

        public override SqlCommand Command
        {
            get
            {
                string sql = @"select e.EmpId,
                                    e.Name,
                                    e.Address,
                                    e.PaymentClassificationType,
                                    e.PaymentMethodType,
                                    e.ScheduleType,
                                    ea.AffiliationId,
                                    a.Dues
                                from Employee e
                                left join EmployeeAffiliation ea on e.EmpId = ea.EmpId
                                left join Affiliation a on ea.AffiliationId = a.Id
                                where ea.AffiliationId = @Id;";
                var command = SqlCommandUtil.CreateCommand(sql, "Id", memberId);
                return command;
            }
        }

        public void Execute()
        {
            var row = LoadDataRowFromCommand(Command);
            if (row == null)
                return;

            var empId = int.Parse(row["EmpId"].ToString());
            var operation = new LoadEmployeeOperation(empId, connection);
            operation.Execute();

            var unionMember = operation.Employee;
            var dues = double.Parse(row["Dues"].ToString());
            var unionAffiliation = new UnionAffiliation(memberId, dues);
            unionMember.Affiliation = unionAffiliation;

            employee = unionMember;
        }
    }
}