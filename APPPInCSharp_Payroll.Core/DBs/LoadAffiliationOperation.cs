using System.Data.SqlClient;

namespace APPPInCSharp_Payroll.Core
{
    public class LoadAffiliationOperation : LoadOperation
    {
        public LoadAffiliationOperation(int empId, SqlConnection connection)
            : base(connection)
        {
            this.empId = empId;
        }

        private readonly int empId;
        private Affiliation affiliation;

        internal Affiliation Affiliation
        {
            get { return affiliation; }
            set { affiliation = value; }
        }

        public override SqlCommand Command
        {
            get
            {
                string sql = @"select *
                                from Affiliation a
                                left join EmployeeAffiliation ea on a.Id = ea.AffiliationId
                                where EmpId = @EmpId";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@EmpId", empId);
                return command;
            }
        }

        public void Execute()
        {
            var row = LoadDataRowFromCommand(Command);

            if (row == null)
            {
                affiliation = new NoAffiliation();
            }
            else
            {
                var affiliationId = int.Parse(row["Id"].ToString());
                var dues = double.Parse(row["Dues"].ToString());
                affiliation = new UnionAffiliation(affiliationId, dues);
            }
        }
    }
}