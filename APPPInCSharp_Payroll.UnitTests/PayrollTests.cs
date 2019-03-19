using APPPInCSharp_Payroll.Console;
using NUnit.Framework;

namespace APPPInCSharp_Payroll.UnitTests
{
    [TestFixture]
    public class PayrollTests
    {
        [Test]
        public void TestAddSalariedEmployee()
        {
            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Kubing", "Home", 1000.00);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.That("Kubing", Is.EqualTo(e.Name));

            PaymentClassification pc = e.Classification;
            Assert.That(pc is SalariedClassification, Is.True);

            SalariedClassification sc = pc as SalariedClassification;
            Assert.AreEqual(1000.00, sc.Salary, .001);

            PaymentSchedule ps = e.Schedule;
            Assert.That(ps is MonthlySchedule, Is.True);

            PaymentMethod pm = e.Method;
            Assert.That(pm is HoldMethod, Is.True);
        }

        [Test]
        public void TestAddHourlyEmployee()
        {
            int empId = 1;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Kubing", "Home", 50.00);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.That("Kubing", Is.EqualTo(e.Name));

            PaymentClassification pc = e.Classification;
            Assert.That(pc is HourlyClassification, Is.True);

            HourlyClassification sc = pc as HourlyClassification;
            Assert.AreEqual(50.00, sc.HourlyRate, .001);

            PaymentSchedule ps = e.Schedule;
            Assert.That(ps is WeeklySchedule, Is.True);

            PaymentMethod pm = e.Method;
            Assert.That(pm is HoldMethod, Is.True);
        }

        [Test]
        public void TestCommissionedEmployee()
        {
            int empId = 1;
            AddCommissionEmployee t = new AddCommissionEmployee(empId, "Kubing", "Home", 500.00, 80.00);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.That("Kubing", Is.EqualTo(e.Name));

            PaymentClassification pc = e.Classification;
            Assert.That(pc is CommissionedClassification, Is.True);

            CommissionedClassification sc = pc as CommissionedClassification;
            Assert.AreEqual(500.00, sc.Salary, .001);
            Assert.AreEqual(80.00, sc.CommissionRate, .001);

            PaymentSchedule ps = e.Schedule;
            Assert.That(ps is BiweeklySchedule, Is.True);

            PaymentMethod pm = e.Method;
            Assert.That(pm is HoldMethod, Is.True);
        }

        [Test]
        public void TestDeleteEmployee()
        {
            int empId = 4;
            AddCommissionEmployee t = new AddCommissionEmployee(empId, "Kubing", "Home", 2500, 3.2);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.That(e, Is.Not.Null);

            DeleteEmployeeTransaction dt = new DeleteEmployeeTransaction(empId);
            dt.Execute();

            Employee e2 = PayrollDatabase.GetEmployee(empId);
            Assert.That(e2, Is.Null);
        }
    }
}