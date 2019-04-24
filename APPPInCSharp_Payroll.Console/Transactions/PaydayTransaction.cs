using System;
using System.Collections;

namespace APPPInCSharp_Payroll.Console
{
    public class PaydayTransaction : Transaction
    {
        public PaydayTransaction(DateTime payDate, PayrollDatabase database)
            : base(database)
        {
            this.payDate = payDate;
        }

        private readonly DateTime payDate;

        private readonly Hashtable paychecks = new Hashtable();

        public override void Execute()
        {
            var empIds = database.GetAllEmployeeIds();
            foreach (var empId in empIds)
            {
                Employee employee = database.GetEmployee(empId);
                if (employee != null && employee.IsPayDate(payDate))
                {
                    DateTime startDate = employee.GetPayPeriodStartDate(payDate);
                    Paycheck pc = new Paycheck(startDate, payDate);
                    paychecks[empId] = pc;
                    employee.Payday(pc);
                }
            }
        }

        public Paycheck GetPaycheck(int empId) => paychecks[empId] as Paycheck;
    }
}