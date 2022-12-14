using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain
{
    public class User
    {
      /*  public User()
        {
            this.Id = Guid.NewGuid(); 
        }   */  
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }   
        public double SalaryPerMonth { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsBlocked { get; set; }
        public List<Loan> loan { get; set; } = new List<Loan>();
    }
}
