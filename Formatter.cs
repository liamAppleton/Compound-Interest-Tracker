using Spectre.Console;

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

        public static Colours ColourRandomiser()
        {
            Random rnd = new Random();
            int randomColour = rnd.Next(0, 148);
            Colours colour = (Colours)randomColour;
            return colour;

        }
    }

    public enum Colours
    {
        Black = 0,
        Maroon = 1,
        Green = 2,
        Olive = 3,
        Navy = 4,
        Purple = 5,
        Teal = 6,
        Silver = 7,
        Grey = 8,
        Red = 9,
        Lime = 10,
        Yellow = 11,
        Blue = 12,
        Fuchsia = 13,
        Aqua = 14,
        White = 15,
        Grey0 = 16,
        Navyblue = 17,
        Darkblue = 18,
        Blue3 = 19,
        Blue3_1 = 20,
        Blue1 = 21,
        Darkgreen = 22,
        Deepskyblue4 = 23,
        Deepskyblue4_1 = 24,
        Deepskyblue4_2 = 25,
        Dodgerblue3 = 26,
        Dodgerblue2 = 27,
        Green4 = 28,
        Springgreen4 = 29,
        Turquoise4 = 30,
        Deepskyblue3 = 31,
        Deepskyblue3_1 = 32,
        Dodgerblue1 = 33,
        Green3 = 34,
        Springgreen3 = 35,
        Darkcyan = 36,
        Lightseagreen = 37,
        Deepskyblue2 = 38,
        Deepskyblue1 = 39,
        Green3_1 = 40,
        Springgreen3_1 = 41,
        Springgreen2 = 42,
        Cyan3 = 43,
        Darkturquoise = 44,
        Turquoise2 = 45,
        Green1 = 46,
        Springgreen2_1 = 47,
        Springgreen1 = 48,
        Mediumspringgreen = 49,
        Cyan2 = 50,
        Cyan1 = 51,
        Darkred = 52,
        Deeppink4 = 53,
        Purple4 = 54,
        Purple4_1 = 55,
        Purple3 = 56,
        Blueviolet = 57,
        Orange4 = 58,
        Grey37 = 59,
        Mediumpurple4 = 60,
        Slateblue3 = 61,
        Slateblue3_1 = 62,
        Royalblue1 = 63,
        Chartreuse4 = 64,
        Darkseagreen4 = 65,
        Paleturquoise4 = 66,
        Steelblue = 67,
        Steelblue3 = 68,
        Cornflowerblue = 69,
        Chartreuse3 = 70,
        Darkseagreen4_1 = 71,
        Cadetblue = 72,
        Cadetblue_1 = 73,
        Skyblue3 = 74,
        Steelblue1 = 75,
        Chartreuse3_1 = 76,
        Palegreen3 = 77,
        Seagreen3 = 78,
        Aquamarine3 = 79,
        Mediumturquoise = 80,
        Steelblue1_1 = 81,
        Chartreuse2 = 82,
        Seagreen2 = 83,
        Seagreen1 = 84,
        Seagreen1_1 = 85,
        Aquamarine1 = 86,
        Darkslategray2 = 87,
        Darkred_1 = 88,
        Deeppink4_1 = 89,
        Darkmagenta = 90,
        Darkmagenta_1 = 91,
        Darkviolet = 92,
        Purple_1 = 93,
        Orange4_1 = 94,
        Lightpink4 = 95,
        Plum4 = 96,
        Mediumpurple3 = 97,
        Mediumpurple3_1 = 98,
        Slateblue1 = 99,
        Yellow4 = 100,
        Wheat4 = 101,
        Grey53 = 102,
        Lightslategrey = 103,
        Mediumpurple = 104,
        Lightslateblue = 105,
        Yellow4_1 = 106,
        Darkolivegreen3 = 107,
        Darkseagreen = 108,
        Lightskyblue3 = 109,
        Lightskyblue3_1 = 110,
        Skyblue2 = 111,
        Chartreuse2_1 = 112,
        Darkolivegreen3_1 = 113,
        Palegreen3_1 = 114,
        Darkseagreen3 = 115,
        Darkslategray3 = 116,
        Skyblue1 = 117,
        Chartreuse1 = 118,
        Lightgreen = 119,
        Lightgreen_1 = 120,
        Palegreen1 = 121,
        Aquamarine1_1 = 122,
        Darkslategray1 = 123,
        Red3 = 124,
        Deeppink4_2 = 125,
        Mediumvioletred = 126,
        Magenta3 = 127,
        Darkviolet_1 = 128,
        Purple_2 = 129,
        Darkorange3 = 130,
        Indianred = 131,
        Hotpink3 = 132,
        Mediumorchid3 = 133,
        Mediumorchid = 134,
        Mediumpurple2 = 135,
        Darkgoldenrod = 136,
        Lightsalmon3 = 137,
        Rosybrown = 138,
        Grey63 = 139,
        Mediumpurple2_1 = 140,
        Mediumpurple1 = 141,
        Gold3 = 142,
        Darkkhaki = 143,
        Navajowhite3 = 144,
        Grey69 = 145,
        Lightsteelblue3 = 146,
        Lightsteelblue = 147,
        Yellow3 = 148
    }
}