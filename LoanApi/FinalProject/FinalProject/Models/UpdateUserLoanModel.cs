namespace FinalProject.Models
{
    public class UpdateUserLoanModel
    {
        public string LoanType { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public int LoanPeriodmonthly { get; set; }
        public string Status { get; set; }

        public string LoanCondition { get; set; }
    }
}
