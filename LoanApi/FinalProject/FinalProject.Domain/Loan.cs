using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain
{
    public class Loan
    {
        public int Id { get; set; }
        public string LoanType { get; set; }    
        public double Amount { get; set; }
        public string Currency { get;set; }
        public int LoanPeriodmonthly { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public string LoanCondition { get; set; }
        public User User { get; set; }
       
    }
}
