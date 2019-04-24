namespace APPPInCSharp_Payroll.Console
{
    public abstract class AddEmployeeTransaction : Transaction
    {
        private readonly int empid;
        private readonly string name;
        private readonly string address;

        public AddEmployeeTransaction(int empid, string name, string address, PayrollDatabase database)
            : base(database)
        {
            this.empid = empid;
            this.name = name;
            this.address = address;
        }

        protected abstract PaymentClassification MakeClassification();

        protected abstract PaymentSchedule MakeSchedule();

        public override void Execute()
        {
            PaymentClassification pc = MakeClassification();
            PaymentSchedule ps = MakeSchedule();
            PaymentMethod pm = new HoldMethod();
            Affiliation af = new NoAffiliation();

            Employee e = new Employee(empid, name, address);
            e.Classification = pc;
            e.Schedule = ps;
            e.Method = pm;
            e.Affiliation = af;
            PayrollDatabase.Instance.Addemployee(e);
        }
    }
}