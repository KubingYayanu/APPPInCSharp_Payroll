namespace APPPInCSharp_Payroll.Console
{
    public class HourlyClassification : PaymentClassification
    {
        public double HourlyRate { get; }

        public HourlyClassification(double hourlyRate)
        {
            HourlyRate = hourlyRate;
        }
    }
}