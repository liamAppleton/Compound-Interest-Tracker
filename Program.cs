namespace CompoundInterestTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter initial investment amount: £");
            double initialAmount = double.Parse(Console.ReadLine());

            Console.Write("Enter annual interest rate (in %): ");
            double annualInterestRate = double.Parse(Console.ReadLine());

            Console.Write("Enter number of years: ");
            int years = int.Parse(Console.ReadLine());

            Console.Write("Enter compounding frequency (Annually, Semiannually, Quarterly, Monthly, Daily): ");
            string userInputFrequency = Console.ReadLine();
            string frequencyPeriod = Formatter.CompoundFrequencyFormatter(userInputFrequency);
            CompoundFrequency frequency = (CompoundFrequency)Enum.Parse(typeof(CompoundFrequency), userInputFrequency);

            Console.WriteLine($"Calculating interest gained for each {frequencyPeriod.ToLower()}");

            int i = 1;
            while (i <= years)
            {
                InvestmentPlan investmentPlan = new InvestmentPlan(initialAmount, annualInterestRate, i, frequency);

                ProjectionCalculator projectionCalculator = new ProjectionCalculator(investmentPlan);
                double compoundInterest = projectionCalculator.CalculateCompoundInterest();
                Console.WriteLine($"{frequencyPeriod} {i}: £{compoundInterest:F2}");

                i++;
            }

            Console.WriteLine("Program finished...");

        }
    }
}

