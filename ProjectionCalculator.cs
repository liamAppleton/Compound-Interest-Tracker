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
            double r = InvestmentPlan.AnnualInterestRate / 100;
            double n = (double)InvestmentPlan.Frequency;
            double t = InvestmentPlan.Period;

            double amount = p * Math.Pow(1 + (r / n), n * t);
            return Math.Round(amount, 2);
        }
    }
}