﻿namespace APPPInCSharp_Payroll.Core
{
    public class MailMethod : PaymentMethod
    {
        public MailMethod()
        {
        }

        public MailMethod(string address)
        {
            Address = address;
        }

        public string Address { get; set; }

        public void Pay(Paycheck paycheck)
        {
        }
    }
}