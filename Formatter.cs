namespace CompoundInterestTracker
{
    public static class Formatter
    {
        public static string CompoundFrequencyFormatter(string frequency)
        {
            string output;

            switch (frequency)
            {
                case "Annually":
                    output = "Year";
                    break;
                case "Semiannually":
                    output = "Half-year";
                    break;
                case "Quarterly":
                    output = "Quarter";
                    break;
                case "Monthly":
                    output = "Month";
                    break;
                case "Daily":
                    output = "Day";
                    break;
                default:
                    output = "Period";
                    break;
            }

            return output;
        }
    }
}