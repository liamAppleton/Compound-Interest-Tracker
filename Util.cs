using Spectre.Console;

namespace CompoundInterestTracker
{
    public static class Util
    {
        private static Color[] colours;

        static Util()
        {
            colours = new Color[] {
                Color.Black,
                Color.Red,
                Color.Green,
                Color.Olive,
                Color.Navy,
                Color.Purple,
                Color.Teal,
                Color.Silver,
                Color.Blue,
                Color.Orange1,
                Color.Gold1,
                Color.Pink1,
                Color.Cyan1,
                Color.Magenta1,
                Color.RosyBrown,
                Color.LightCoral,
                Color.Turquoise2,
                Color.DarkGreen,
                Color.DarkOrange,
                Color.DarkViolet
};
        }

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

        public static Color ColourRandomiser()
        {
            Random rnd = new Random();
            int randomColour = rnd.Next(0, 19);
            return colours[randomColour];
        }
    }
}