namespace APPPInCSharp_Payroll.Core
{
    public interface PaymentMethod
    {
        void Pay(Paycheck paycheck);
    }
}