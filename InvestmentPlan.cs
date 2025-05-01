namespace CompoundInterestTracker
{
    class InvestmentPlan
    {
        private double initialAmount;

        private double annualInterestRate;

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

        public int ProjectionPeriod
        { get; set; }

        public CompoundFrequency Frequency
        { get; set; }

        public InvestmentPlan(double initialAmount, double annualInterestRate, int projectionPeriod, CompoundFrequency frequency)
        {
            InitialAmount = initialAmount;
            AnnualInterestRate = annualInterestRate;
            ProjectionPeriod = projectionPeriod;
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