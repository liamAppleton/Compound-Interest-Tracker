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

            var userInputFrequency = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Enter compounding frequency:")
                .AddChoices("Annually", "Semiannually", "Quarterly", "Monthly", "Daily")
);

            string frequencyPeriod = Formatter.CompoundFrequencyFormatter(userInputFrequency);
            CompoundFrequency frequency = (CompoundFrequency)Enum.Parse(typeof(CompoundFrequency), userInputFrequency);

            Console.WriteLine($"Calculating interest gained for each {frequencyPeriod.ToLower()}");

            var interestOverTime = new List<(string periodDisplay, double amount, Color colour)>();
            int i = 1;
            while (i <= years)
            {
                InvestmentPlan investmentPlan = new InvestmentPlan(initialAmount, annualInterestRate, i, frequency);
                ProjectionCalculator projectionCalculator = new ProjectionCalculator(investmentPlan);

                double compoundInterest = projectionCalculator.CalculateCompoundInterest();
                string periodDisplay = $"{frequencyPeriod} {i} (£{compoundInterest - initialAmount:F2})";
                Color colour = Formatter.ColourRandomiser();

                interestOverTime.Add((periodDisplay, Math.Round(compoundInterest - initialAmount, 2), colour));
                i++;
            }

            AnsiConsole.Write(new BarChart()
                .Width(60)
                .HideValues()
                .Label("Interest Over Time")
                .CenterLabel()
                .AddItems(interestOverTime, (item) => new BarChartItem(
                    item.periodDisplay, item.amount, item.colour
                )));

            Console.WriteLine("Program finished...");
        }
    }
}