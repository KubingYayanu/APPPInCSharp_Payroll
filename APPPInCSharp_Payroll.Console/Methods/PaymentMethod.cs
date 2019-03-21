namespace APPPInCSharp_Payroll.Console
{
    public interface PaymentMethod
    {
        void Pay(Paycheck paycheck);
    }
}