namespace Fortitude.Model.LoansModels
{
    public class LoanDetails
    {
        public double Principal { get; set; }
        public double AnnualInterestRate { get; set; }
        public int Years { get; set; }
    }

    public class LoanApply
    {
        public double Amount { get; set; }
        public int Years { get; set; }
    }
}
