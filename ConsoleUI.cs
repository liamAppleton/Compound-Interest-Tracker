using Spectre.Console;

namespace CompoundInterestTracker
{
    class ConsoleUI
    {
        public static async Task Run()
        {
            var initialAmount = AnsiConsole.Prompt(new TextPrompt<double>("Enter initial investment amount: £")
                .Validate((amount) => amount < 1 ?
                ValidationResult.Error("[red]Amount must be greater than 0[/]") :
                ValidationResult.Success()
                ));

            var annualInterestRate = AnsiConsole.Prompt(new TextPrompt<double>("Enter annual interest rate (in %): ")
                .Validate((amount) => amount < 1 ?
                ValidationResult.Error("[red]Annual interest must be greater than 0[/]") :
                ValidationResult.Success()
                ));

            var years = AnsiConsole.Prompt(new TextPrompt<int>("Enter number of years: ")
                .Validate((amount) => amount < 1 ?
                ValidationResult.Error("[red]Years must be greater than 0[/]") :
                ValidationResult.Success()
                ));

            var userInputFrequency = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Enter compounding frequency:")
                .AddChoices("Annually", "Semiannually", "Quarterly", "Monthly", "Daily")
);

            string frequencyPeriod = Util.CompoundFrequencyFormatter(userInputFrequency);
            CompoundFrequency frequency = (CompoundFrequency)Enum.Parse(typeof(CompoundFrequency), userInputFrequency);

            AnsiConsole.Markup($"\nCalculating interest gained for each {frequencyPeriod.ToLower()}...");

            var interestOverTime = new List<(string periodDisplay, double amount, Color colour)>();
            int i = 1;
            while (i <= years)
            {
                InvestmentPlan investmentPlan = new InvestmentPlan(initialAmount, annualInterestRate, i, frequency);
                ProjectionCalculator projectionCalculator = new ProjectionCalculator(investmentPlan);

                double compoundInterest = projectionCalculator.CalculateCompoundInterest();
                string periodDisplay = $"{frequencyPeriod} {i} (£{compoundInterest - initialAmount:F2})";
                Color colour = Util.ColourRandomiser();

                interestOverTime.Add((periodDisplay, Math.Round(compoundInterest - initialAmount, 2), colour));
                i++;
            }

            await AnsiConsole.Progress()
                .StartAsync(async ctx =>
                {
                    var task1 = ctx.AddTask("[green]Calculating compound interest[/]");

                    while (!ctx.IsFinished)
                    {
                        await Task.Delay(15);
                        task1.Increment(1.5);
                    }
                });

            AnsiConsole.Write(new BarChart()
                .Width(60)
                .HideValues()
                .Label("Interest Over Time")
                .CenterLabel()
                .AddItems(interestOverTime, (item) => new BarChartItem(
                    item.periodDisplay, item.amount, item.colour
                )));

            AnsiConsole.MarkupLine("\n[green]Program finished...[/]");
        }
    }
}