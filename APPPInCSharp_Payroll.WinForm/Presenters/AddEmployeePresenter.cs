using APPPInCSharp_Payroll.Core;

namespace APPPInCSharp_Payroll.WinForm
{
    public class AddEmployeePresenter
    {
        public AddEmployeePresenter(
            AddEmployeeView view,
            TransactionContainer container,
            PayrollDatabase database)
        {
            this.view = view;
            transactionContainer = container;
            this.database = database;
        }

        private TransactionContainer transactionContainer;
        private AddEmployeeView view;
        private PayrollDatabase database;

        private int empId;
        private string name;
        private string address;
        private bool isHourly;
        private double hourlyRate;
        private bool isSalary;
        private double salary;
        private bool isCommission;
        private double commissionSalary;
        private double commission;

        public int EmpId
        {
            get { return empId; }
            set
            {
                empId = value;
                UpdateView();
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                UpdateView();
            }
        }

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                UpdateView();
            }
        }

        public bool IsHourly
        {
            get { return isHourly; }
            set
            {
                isHourly = value;
                UpdateView();
            }
        }

        public double HourlyRate
        {
            get { return hourlyRate; }
            set
            {
                hourlyRate = value;
                UpdateView();
            }
        }

        public bool IsSalary
        {
            get { return IsSalary; }
            set
            {
                isSalary = value;
                UpdateView();
            }
        }

        public double Salary
        {
            get { return salary; }
            set
            {
                salary = value;
                UpdateView();
            }
        }

        public bool IsCommission
        {
            get { return isCommission; }
            set
            {
                isCommission = value;
                UpdateView();
            }
        }

        public double CommissionSalary
        {
            get { return commissionSalary; }
            set
            {
                commissionSalary = value;
                UpdateView();
            }
        }

        public double Commission
        {
            get { return commission; }
            set
            {
                commission = value;
                UpdateView();
            }
        }

        public bool AllInfomationIsCollected()
        {
            bool result = true;
            result &= empId > 0;
            result &= !string.IsNullOrWhiteSpace(name);
            result &= !string.IsNullOrWhiteSpace(address);
            result &= isHourly || isSalary || isCommission;

            if (isHourly)
            {
                result &= hourlyRate > 0;
            }
            else if (isSalary)
            {
                result &= salary > 0;
            }
            else if (isCommission)
            {
                result &= commission > 0;
                result &= commissionSalary > 0;
            }

            return result;
        }

        private void UpdateView()
        {
            if (AllInfomationIsCollected())
            {
                view.SubmitEnabled = true;
            }
            else
            {
                view.SubmitEnabled = false;
            }
        }

        public TransactionContainer TransactionContainer => transactionContainer;

        public Transaction CreateTransaction()
        {
            if (isHourly)
            {
                return new AddHourlyEmployee(empId, name, address, hourlyRate, database);
            }
            else if (isSalary)
            {
                return new AddSalariedEmployee(empId, name, address, salary, database);
            }
            else
            {
                return new AddCommissionEmployee(empId, name, address, commissionSalary, commission, database);
            }
        }

        public virtual void AddEmployee()
        {
            transactionContainer.Add(CreateTransaction());
        }
    }
}