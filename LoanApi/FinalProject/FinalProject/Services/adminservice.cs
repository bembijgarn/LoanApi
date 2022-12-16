using FinalProject.DATA;
using FinalProject.Domain;
using FinalProject.Interfaces;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.Services
{
    public class Adminservice : Iadminservice
    {
        private readonly UserContext _context;
        public Adminservice(UserContext context)
        {
            _context = context;
        }
        public  User Removeuser(int id)
        {                   
            var usertoremove = _context.User.Find(id);
            if (usertoremove == null)
            {
                return null;
            }
            _context.Remove(usertoremove);
            _context.SaveChanges();
            return usertoremove;
        
        }
        public IEnumerable<User> GettAll()
        {
            return _context.User;
        }
        public Loan RemoveLoanbyUserId(int id)
        {
            var loantoremove = _context.Loan.Where(x => x.UserId == id).FirstOrDefault();
            _context.Remove(loantoremove);
            _context.SaveChanges();
            return loantoremove; ;
        }
        public Loan ChekLoan(string mail)
        {
            var user = _context.User.Where(x => x.Email == mail).SingleOrDefault();
            var userwithloan = _context.Loan.Where(x => x.UserId == user.Id).FirstOrDefault(); ;
            return userwithloan;
        }
        public Loan UpdateUserLoan(UpdateUserLoanModel loan ,int id)
        {
            var Dbuser = _context.Loan.FirstOrDefault(x => x.UserId == id);
            if (Dbuser != null)
            {
                Dbuser.LoanType = loan.LoanType;
                Dbuser.Amount = loan.Amount;
                Dbuser.Currency = loan.Currency;
                Dbuser.LoanPeriodmonthly = loan.LoanPeriodmonthly;
                Dbuser.LoanCondition = Loancondition.Active;
                _context.Loan.Update(Dbuser);
                _context.SaveChanges();
                return Dbuser;
            }
            return null;
        }
        public User BlockOrUnblockUserbyId(int id,string Block)
        {
            var Dbuser = _context.User.Find(id);
            if (Block.ToUpper() == "BLOCK")
            {                
                Dbuser.IsBlocked = BlockedOrNot.Blocked;
            }
            if (Block.ToUpper() == "UNBLOCK")
            {
                Dbuser.IsBlocked = BlockedOrNot.Unblocked;
            }
            _context.Update(Dbuser);
            _context.SaveChanges();
            return Dbuser;
        }
        public Loan AccepLoanbyUserId(int Id)
        {
            var Dbuser = _context.Loan.FirstOrDefault(x => x.Status == LoanStatus.Requested && x.LoanCondition == Loancondition.Active && x.UserId == Id);
            if (Dbuser != null)
            {
                Dbuser.Status = "Accepted";
                _context.Loan.Update(Dbuser);
                _context.SaveChanges();
            }
           
            return Dbuser;
        }
    }
}
