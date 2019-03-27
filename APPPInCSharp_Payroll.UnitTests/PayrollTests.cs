﻿using APPPInCSharp_Payroll.Console;
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


            int memberId = 86;
            PayrollDatabase.AddUnionMember(memberId, e);

            UnionAffiliation ua = new UnionAffiliation(memberId, 99.52);
            e.Affiliation = ua;

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

        [Test]
        public void TestChangeSalariedTransaction()
        {
            int empId = 4;
            AddCommissionEmployee t = new AddCommissionEmployee(empId, "Kubing", "Home", 2500, 3.2);
            t.Execute();

            ChangeSalariedTransaction cst = new ChangeSalariedTransaction(empId, 1000.00);
            cst.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.IsNotNull(pc);
            Assert.IsTrue(pc is SalariedClassification);

            SalariedClassification sc = pc as SalariedClassification;
            Assert.AreEqual(1000.00, sc.Salary, 0.001);

            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is MonthlySchedule);
        }

        [Test]
        public void TestChangeCommissionTransaction()
        {
            int empId = 5;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Kubing", "Home", 15.25);
            t.Execute();

            ChangeCommissionedTransaction cct = new ChangeCommissionedTransaction(empId, 2000.00, 3.8);
            cct.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.IsNotNull(pc);
            Assert.IsTrue(pc is CommissionedClassification);

            CommissionedClassification cc = pc as CommissionedClassification;
            Assert.AreEqual(2000.00, cc.Salary, 0.001);
            Assert.AreEqual(3.8, cc.CommissionRate, 0.001);

            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is BiweeklySchedule);
        }

        [Test]
        public void TestChangeDirectTransaction()
        {
            int empId = 2;
            AddCommissionEmployee t = new AddCommissionEmployee(empId, "Kubing", "Home", 2000.00, 3.1);
            t.Execute();

            ChangeDirectTransaction cdt = new ChangeDirectTransaction(empId);
            cdt.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentMethod pm = e.Method;
            Assert.IsNotNull(pm);
            Assert.IsTrue(pm is DirectMethod);
        }

        [Test]
        public void TestChangeMailTransaction()
        {
            int empId = 3;
            AddCommissionEmployee t = new AddCommissionEmployee(empId, "Kubing", "Home", 2000.00, 3.1);
            t.Execute();

            ChangeMailTransaction cmt = new ChangeMailTransaction(empId);
            cmt.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentMethod pm = e.Method;
            Assert.IsNotNull(pm);
            Assert.IsTrue(pm is MailMethod);
        }

        [Test]
        public void TestChangeHoldTransaction()
        {
            int empId = 4;
            AddCommissionEmployee t = new AddCommissionEmployee(empId, "Kubing", "Home", 2000.00, 3.1);
            t.Execute();

            ChangeHoldTransaction cmt = new ChangeHoldTransaction(empId);
            cmt.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentMethod pm = e.Method;
            Assert.IsNotNull(pm);
            Assert.IsTrue(pm is HoldMethod);
        }

        [Test]
        public void TestChangeMemberTransaction()
        {
            int empId = 9;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Kubing", "Home", 25.32);
            t.Execute();

            int memberId = 7783;
            ChangeMemberTransaction cmt = new ChangeMemberTransaction(empId, memberId, 99.42);
            cmt.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Affiliation affiliation = e.Affiliation;
            Assert.IsNotNull(e);
            Assert.IsNotNull(affiliation);
            Assert.IsTrue(affiliation is UnionAffiliation);

            UnionAffiliation ua = e.Affiliation as UnionAffiliation;
            Assert.AreEqual(99.42, ua.Dues, 0.001);

            Employee member = PayrollDatabase.GetUnionMember(memberId);
            Assert.IsNotNull(member);
            Assert.AreEqual(e, member);
        }

        [Test]
        public void TestChangeUnaffiliatedTransaction()
        {
            int empId = 10;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Kubing", "Home", 25.32);
            t.Execute();

            int memberId = 7783;
            ChangeMemberTransaction cmt = new ChangeMemberTransaction(empId, memberId, 99.42);
            cmt.Execute();

            ChangeUnaffiliatedTransaction cut = new ChangeUnaffiliatedTransaction(empId);
            cut.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Affiliation affiliation = e.Affiliation;
            Assert.IsNotNull(e);
            Assert.IsNotNull(affiliation);
            Assert.IsTrue(affiliation is NoAffiliation);

            Employee member = PayrollDatabase.GetUnionMember(memberId);
            Assert.IsNull(member);
        }

        [Test]
        public void TestPaySingleSalariedEmployee()
        {
            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Kubing", "Home", 1000.00);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 30);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayDate);
            Assert.AreEqual(1000.00, pc.GrossPay, 0.001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(0.0, pc.Deductions, 0.001);
            Assert.AreEqual(1000.00, pc.NetPay, 0.001);
        }

        [Test]
        public void TestPaySingleSalariedEmployeeOnWrongDate()
        {
            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Kubing", "Home", 1000.00);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 29);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNull(pc);
        }

        [Test]
        public void TestPayingSingleHourlyEmployeeNoTimeCards()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Kubing", "Home", 15.25);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 9);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidateHourlyPaycheck(pt, empId, payDate, 0.0);
        }

        private void ValidateHourlyPaycheck(PaydayTransaction pt, int empId, DateTime payDate, double pay)
        {
            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayDate);
            Assert.AreEqual(pay, pc.GrossPay, 0.01);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(0.0, pc.Deductions, 0.01);
            Assert.AreEqual(pay, pc.NetPay, 0.01);
        }

        [Test]
        public void TestPaySingleHourlyEmployeeOneTimeCard()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Kubing", "Home", 15.25);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 9); //Friday

            TimeCardTransaction tct = new TimeCardTransaction(payDate, 2.0, empId);
            tct.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateHourlyPaycheck(pt, empId, payDate, 30.5);
        }

        [Test]
        public void TestPaySingleHourlyEmployeeOvertimeOneTimeCard()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Kubing", "Home", 15.25);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 9); //Friday

            TimeCardTransaction tct = new TimeCardTransaction(payDate, 9.0, empId);
            tct.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidateHourlyPaycheck(pt, empId, payDate, (8 + 1.5) * 15.25);
        }

        [Test]
        public void TestPaySingleHourlyEmployeeOnWrongDate()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Kubing", "Home", 15.25);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 8); //Thursday

            TimeCardTransaction tct = new TimeCardTransaction(payDate, 9.0, empId);
            tct.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNull(pc);
        }

        [Test]
        public void TestPaySingleHourlyEmployeeTwoCards()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Kubing", "Home", 15.25);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 9); //Friday

            TimeCardTransaction tct = new TimeCardTransaction(payDate, 2.0, empId);
            tct.Execute();

            TimeCardTransaction tct2 = new TimeCardTransaction(payDate.AddDays(-1), 5.0, empId);
            tct2.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidateHourlyPaycheck(pt, empId, payDate, 7 * 15.25);
        }

        [Test]
        public void TestPaySingleHourlyEmployeeWithTimeCardsSpanningTwoPayPeriods()
        {
            int empId = 3;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Kubing", "Home", 15.25);
            t.Execute();

            DateTime payDate = new DateTime(2001, 11, 9); //Friday
            DateTime dateInPreviousPayPeriod = new DateTime(2001, 11, 2);

            TimeCardTransaction tct = new TimeCardTransaction(payDate, 2.0, empId);
            tct.Execute();

            TimeCardTransaction tct2 = new TimeCardTransaction(dateInPreviousPayPeriod, 5.0, empId);
            tct2.Execute();

            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidateHourlyPaycheck(pt, empId, payDate, 2 * 15.25);
        }
    }
}