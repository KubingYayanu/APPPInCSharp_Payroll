using APPPInCSharp_Payroll.Console;
using NUnit.Framework;
using System;

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

        [Test]
        public void TestTimeCardTransaction()
        {
            int empId = 5;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Kubing", "Home", 15.25);
            t.Execute();

            TimeCardTransaction tct = new TimeCardTransaction(new DateTime(2005, 7, 31), 8.0, empId);
            tct.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is HourlyClassification);

            HourlyClassification hc = pc as HourlyClassification;
            TimeCard tc = hc.GetTimeCard(new DateTime(2005, 7, 31));
            Assert.IsNotNull(tc);
            Assert.AreEqual(8.0, tc.Hours);
        }

        [Test]
        public void TestSalesReceiptTransaction()
        {
            int empId = 6;
            AddCommissionEmployee t = new AddCommissionEmployee(empId, "Kubing", "Home", 2000, 3.0);
            t.Execute();

            SalesReceiptTransaction srt = new SalesReceiptTransaction(new DateTime(2017, 3, 19), 4, empId);
            srt.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is CommissionedClassification);

            CommissionedClassification hc = pc as CommissionedClassification;
            SalesReceipt sr = hc.GetSalesReceipt(new DateTime(2017, 3, 19));
            Assert.IsNotNull(sr);
            Assert.AreEqual(4, sr.Amount);
        }

        [Test]
        public void TestAddServiceCharge()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Kubing", "Home", 15.25);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            UnionAffiliation ua = new UnionAffiliation();
            e.Affiliation = ua;

            int memberId = 86;
            PayrollDatabase.AddUnionMember(memberId, e);

            ServiceChargeTransaction sct = new ServiceChargeTransaction(memberId, new DateTime(2005, 8, 8), 12.95);
            sct.Execute();

            ServiceCharge sc = ua.GetServiceCharge(new DateTime(2005, 8, 8));
            Assert.IsNotNull(sc);
            Assert.AreEqual(12.95, sc.Amount, 0.001);
        }

        [Test]
        public void TestChangeNameTransaction()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Kubing", "Home", 15.25);
            t.Execute();

            ChangeNameTransaction cnt = new ChangeNameTransaction(empId, "Yuling");
            cnt.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);

            Assert.IsNotNull(e);
            Assert.AreEqual("Yuling", e.Name);
        }

        [Test]
        public void TestChangeAddressTransaction()
        {
            int empId = 3;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Kubing", "Home", 15.25);
            t.Execute();

            ChangeAddressTransaction cat = new ChangeAddressTransaction(empId, "Company");
            cat.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);

            Assert.IsNotNull(e);
            Assert.AreEqual("Company", e.Address);
        }

        [Test]
        public void TestChangeHourlyTransaction()
        {
            int empId = 3;
            AddCommissionEmployee t = new AddCommissionEmployee(empId, "Kubing", "Home", 2500, 3.2);
            t.Execute();

            ChangeHourlyTransaction cht = new ChangeHourlyTransaction(empId, 27.52);
            cht.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.IsNotNull(pc);
            Assert.IsTrue(pc is HourlyClassification);

            HourlyClassification hc = pc as HourlyClassification;
            Assert.AreEqual(27.52, hc.HourlyRate, 0.001);

            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is WeeklySchedule);
        }
    }
}