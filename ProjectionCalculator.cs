namespace CompoundInterestTracker
{
    class ProjectionCalculator
    {
        private InvestmentPlan InvestmentPlan
        { get; }

        public ProjectionCalculator(InvestmentPlan investmentPlan)
        {
            InvestmentPlan = investmentPlan;
        }

        public double CalculateCompoundInterest()
        {
            double p = InvestmentPlan.InitialAmount;
            double r = InvestmentPlan.AnnualInterestRate;
            double n = (double)InvestmentPlan.Frequency;
            double t = InvestmentPlan.Years;

            return p * Math.Pow(1 + r / n, n * t);
        }
    }
}