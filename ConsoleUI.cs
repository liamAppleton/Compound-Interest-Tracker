using Spectre.Console;

namespace CompoundInterestTracker
{
    class ConsoleUI
    {
        public static void Run()
        {
            var initialAmount = AnsiConsole.Prompt(new TextPrompt<double>("Enter initial investment amount: £"));

            var annualInterestRate = AnsiConsole.Prompt(new TextPrompt<double>("Enter annual interest rate (in %): "));

            var years = AnsiConsole.Prompt(new TextPrompt<int>("Enter number of years: "));

            var userInputFrequency = AnsiConsole.Prompt(new TextPrompt<string>("Enter compounding frequency (Annually, Semiannually, Quarterly, Monthly, Daily): "));

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