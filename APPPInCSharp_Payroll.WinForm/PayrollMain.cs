using APPPInCSharp_Payroll.Core;
using System;
using System.Windows.Forms;

namespace APPPInCSharp_Payroll.WinForm
{
    public class PayrollMain
    {
        [STAThread]
        public static void Main(string[] args)
        {
            PayrollDatabase database = new InMemoryPayrollDatabase();
            WindowViewLoader viewLoader = new WindowViewLoader(database);

            viewLoader.LoadPayrollView();
            Application.Run(viewLoader.LastLoadedView);
        }
    }
}