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

                double compoundInterest = projectionCalculator.CalculateCompoundInterest();
                string periodDisplay = $"{frequencyPeriod} {i} (£{compoundInterest - initialAmount:F2})";
                Color colour = Util.ColourRandomiser();

                interestOverTime.Add((periodDisplay, Math.Round(compoundInterest - initialAmount, 2), colour));
                i++;
            }

        }

        public static async Task Run()
        {
            getUserInput();

            AnsiConsole.Markup($"\nCalculating interest gained for each {frequencyPeriod.ToLower()}...");

            setInterestOverTime();

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

            if (interestOverTime != null)
            {
                AnsiConsole.Write(new BarChart()
                .Width(60)
                .HideValues()
                .Label("Interest Over Time\n")
                .CenterLabel()
                .AddItems(interestOverTime, (item) => new BarChartItem(
                    item.periodDisplay, item.amount, item.colour
                )));
            }


            AnsiConsole.MarkupLine("\n[green]Program finished![/]");
        }
    }
}