using Spectre.Console;

namespace CompoundInterestTracker
{
    class ConsoleUI
    {
        static double initialAmount;

        static double annualInterestRate;

        static string? userInputFrequency;

        static string frequencyAsString = string.Empty;

        static int projectionPeriod;

        static CompoundFrequency frequency;

        static List<(string periodDisplay, double amount, Color colour)>? interestOverTime;

        static double compoundInterest;

        private static void getUserInitialAmount()
        {
            initialAmount = AnsiConsole.Prompt(new TextPrompt<double>("Enter initial investment amount: £")
                .Validate((amount) => amount < 1 ?
                ValidationResult.Error("[red]Amount must be greater than 0[/]") :
                ValidationResult.Success()
                ));
        }

        private static void getUserAnnualInterestRate()
        {
            annualInterestRate = AnsiConsole.Prompt(new TextPrompt<double>("Enter annual interest rate (in %): ")
                .Validate((amount) => amount < 1 ?
                ValidationResult.Error("[red]Annual interest must be greater than 0[/]") :
                ValidationResult.Success()
                ));
        }

        private static void getUserInputFrequency()
        {
            userInputFrequency = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Enter compounding frequency:")
                .AddChoices("Annually", "Semiannually", "Quarterly", "Monthly", "Daily"));
        }

        private static void setFrequencyData()
        {
            if (userInputFrequency != null)
            {
                frequencyAsString = Util.CompoundFrequencyFormatter(userInputFrequency);
                frequency = (CompoundFrequency)Enum.Parse(typeof(CompoundFrequency), userInputFrequency);
            }
            else
            {
                frequencyAsString = "Annually";
                frequency = CompoundFrequency.Annually;
            }
        }

        private static void getUserPeriod()
        {
            projectionPeriod = AnsiConsole.Prompt(new TextPrompt<int>($"Enter the number of {frequencyAsString.ToLower()}s to project: ")
                .Validate((amount) => amount < 1 ?
                ValidationResult.Error("[red]Period must be greater than 0[/]") :
                ValidationResult.Success()
                ));
        }

        private static void getUserInput()
        {
            getUserInitialAmount();
            getUserAnnualInterestRate();
            getUserInputFrequency();
            setFrequencyData();
            getUserPeriod();
        }

        private static void setInterestOverTime()
        {
            interestOverTime = new List<(string periodDisplay, double amount, Color colour)>();
            int i = 1;
            while (i <= projectionPeriod)
            {
                InvestmentPlan investmentPlan = new InvestmentPlan(initialAmount, annualInterestRate, i, frequency);
                ProjectionCalculator projectionCalculator = new ProjectionCalculator(investmentPlan);

                compoundInterest = projectionCalculator.CalculateCompoundInterest();
                string periodDisplay = $"{frequencyAsString} {i} → Gained: £{compoundInterest - initialAmount:F2}";
                Color colour = Util.ColourRandomiser();

                interestOverTime.Add((periodDisplay, Math.Round(compoundInterest - initialAmount, 2), colour));
                i++;
            }

        }

        public static async Task Run()
        {
            AnsiConsole.MarkupLine("\n[bold blue]=== Compound Interest Tracker ===[/]\n");

            getUserInput();

            setInterestOverTime();

            await AnsiConsole.Progress()
                .StartAsync(async ctx =>
                {
                    var task1 = ctx.AddTask("[blue]Calculating compound interest[/]");

                    while (!ctx.IsFinished)
                    {
                        await Task.Delay(15);
                        task1.Increment(1.5);
                    }
                });

            if (interestOverTime != null)
            {
                AnsiConsole.MarkupLine("\n[bold blue]=== Interest Over Time ===[/]\n");

                AnsiConsole.Write(new BarChart()
                .Width(60)
                .HideValues()
                .AddItems(interestOverTime, (item) => new BarChartItem(
                    item.periodDisplay, item.amount, item.colour
                )));
            }

            AnsiConsole.MarkupLine("\n[bold blue]=== Investment Summary ===[/]\n");
            AnsiConsole.MarkupLine($"Initial Investment: £{initialAmount:F2}");
            AnsiConsole.MarkupLine($"Annual Interest Rate: {annualInterestRate}");
            AnsiConsole.MarkupLine($"Compounded: {frequency}");
            AnsiConsole.MarkupLine($"Projection Period Length: {projectionPeriod}");
            AnsiConsole.MarkupLine($"\nProjected Final Amount: [bold green]£{compoundInterest}[/]");
        }
    }
}