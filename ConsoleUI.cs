using Spectre.Console;

namespace CompoundInterestTracker
{
    class ConsoleUI
    {
        static double initialAmount;

        static double annualInterestRate;

        static string? userInputFrequency;

        static string frequencyPeriod = string.Empty;

        static int period;

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
                frequencyPeriod = Util.CompoundFrequencyFormatter(userInputFrequency);
                frequency = (CompoundFrequency)Enum.Parse(typeof(CompoundFrequency), userInputFrequency);
            }
            else
            {
                frequencyPeriod = "Annually";
                frequency = CompoundFrequency.Annually;
            }
        }

        private static void getUserPeriod()
        {
            period = AnsiConsole.Prompt(new TextPrompt<int>($"Enter the number of {frequencyPeriod.ToLower()}s to project: ")
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
            while (i <= period)
            {
                InvestmentPlan investmentPlan = new InvestmentPlan(initialAmount, annualInterestRate, i, frequency);
                ProjectionCalculator projectionCalculator = new ProjectionCalculator(investmentPlan);

                compoundInterest = projectionCalculator.CalculateCompoundInterest();
                string periodDisplay = $"{frequencyPeriod} {i} → Gained: £{compoundInterest - initialAmount:F2}";
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

            AnsiConsole.MarkupLine($"\n[bold blue]=== Investment Summary ===[/]\n\nInitial Amount: {initialAmount}\nAnnual Interest Rate: {annualInterestRate}\nCompounded: {frequency}\nPeriod: {period}\n\nProjected Final Amount: [bold green]£{compoundInterest}[/]\n");
        }
    }
}