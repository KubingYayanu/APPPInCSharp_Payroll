using System;
using System.Collections;

namespace APPPInCSharp_Payroll.Console
{
    public class PaydayTransaction : Transaction
    {
        public PaydayTransaction(DateTime payDate)
        {
            this.payDate = payDate;
        }

        private readonly DateTime payDate;

        private readonly Hashtable paychecks = new Hashtable();

        public void Execute()
        {
            var empIds = PayrollDatabase.GetAllEmployeeIds();
            foreach (var empId in empIds)
            {
                Employee employee = PayrollDatabase.GetEmployee(empId);
                if (employee.IsPayDate(payDate))
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