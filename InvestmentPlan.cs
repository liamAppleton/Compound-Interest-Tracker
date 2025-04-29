namespace CompoundInterestTracker
{
    class InvestmentPlan
    {
        private double initialAmount;

        private double annualInterestRate;

        private int years;

        public double InitialAmount
        {
            get { return initialAmount; }
            set
            {
                if (value < 0) { initialAmount = 0; }
                else { initialAmount = value; }
            }
        }

        public double AnnualInterestRate
        {
            get { return annualInterestRate; }
            set
            {
                if (value < 0) { annualInterestRate = 0; }
                else { annualInterestRate = value; }
            }

        }

        public int Years
        {
            get { return years; }
            set
            {
                if (value < 1 || value > 50)
                {
                    throw new ArgumentOutOfRangeException("Years must be between 1 and 50 inclusive.");
                }
                else { years = value; }
            }
        }

        public CompoundFrequency Frequency
        { get; set; }

        public InvestmentPlan(double initialAmount, double annualInterestRate, int years, CompoundFrequency frequency)
        {
            InitialAmount = initialAmount;
            AnnualInterestRate = annualInterestRate;
            Years = years;
            Frequency = frequency;
        }
    }

    public enum CompoundFrequency
    {
        Annually = 1,
        Semiannually = 2,
        Quarterly = 4,
        Monthly = 12,
        Daily = 365
    }
}