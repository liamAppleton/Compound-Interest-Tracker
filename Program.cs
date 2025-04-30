namespace CompoundInterestTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter initial investment amount: £");
            string initialAmount = Console.ReadLine();

            Console.Write("Enter annual interest rate (in %): ");
            string annualInterestRate = Console.ReadLine();

            Console.Write("Enter number of years: ");
            string years = Console.ReadLine();

            Console.Write("Enter compounding frequency (Annually, Semiannually, Quarterly, Monthly, Daily): ");
            string frequency = Console.ReadLine();
        }
    }
}

