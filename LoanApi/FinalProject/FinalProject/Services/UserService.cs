using FinalProject.DATA;
using FinalProject.Domain;
using FinalProject.Interfaces;
using FinalProject.PasswordHashing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class UserService : Iuserservice
    {
        private readonly UserContext _context;
        public UserService(UserContext context)
        {
            _context = context;
            
        }
        public User Registration(User userr)
        {
            userr.Role = Role.User;
            userr.Password = new HashPassword().HashPass(userr.Password);
            userr.IsBlocked = BlockedOrNot.Unblocked;
            userr.loan = new List<Loan>() { };
                      
            _context.Add(userr);
            _context.SaveChanges();

            return userr;          
        }
        public User Login(User user)
        {            
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                return null;
            var DbUSer = _context.User.SingleOrDefault(x => x.UserName == user.UserName);
            if (DbUSer == null)
                return null;
            if (new HashPassword().HashPass(user.Password) != DbUSer.Password)
                return null;            
            return DbUSer;
        }       
        public Loan AddLoan(Loan loan,User user,string mail)
        {            
            var Dbuser = _context.User.Where(x => x.Email == mail).SingleOrDefault();
            if (_context.Loan.Any(x => x.UserId == Dbuser.Id && x.LoanCondition == Loancondition.Active 
            || Dbuser.IsBlocked == BlockedOrNot.Blocked))
            {
                Dbuser = null;
                return null;
            }
             var checksalary = CheckUserSalary(loan, Convert.ToInt32(Dbuser.SalaryPerMonth));
            if (checksalary)
            {
                Dbuser.loan.Add(new Loan
                {
                    LoanType = loan.LoanType,
                    Amount = loan.Amount,
                    Currency = loan.Currency,
                    LoanPeriodmonthly = loan.LoanPeriodmonthly,
                    LoanCondition = Loancondition.Active,
                    Status = LoanStatus.Requested,
                });
                _context.User.Attach(Dbuser);
                _context.SaveChanges();
            }

            Dbuser.loan.Add(new Loan
            {
                LoanType = loan.LoanType,
                Amount = loan.Amount,
                Currency = loan.Currency,
                LoanPeriodmonthly = loan.LoanPeriodmonthly,
                LoanCondition = Loancondition.Active,
                Status = LoanStatus.Requested,
            });           
            return loan;            
        }
        public Loan UpdateLoan(Loan loan,int currentid)
        {
            var Dbuser = _context.Loan.FirstOrDefault(x => x.UserId == currentid && x.Status != LoanStatus.Accpeted
            && x.LoanCondition == Loancondition.Active);
            if (Dbuser != null)
            {
                Dbuser.LoanType = loan.LoanType;
                Dbuser.Amount = loan.Amount;
                Dbuser.Currency = loan.Currency;
                Dbuser.LoanPeriodmonthly = loan.LoanPeriodmonthly;
                Dbuser.LoanCondition = Loancondition.Active;
                _context.Loan.Update(Dbuser);
                _context.SaveChanges();
            }
            return loan;
        }
        #region checkusersalarymethod
        public bool CheckUserSalary(Loan loan, double salary)
        {
            bool check = true;
            if ((loan.Amount / loan.LoanPeriodmonthly) > (salary / 2))
            {
                check = false;
                return check;
            }
            return check;
        }
        #endregion



    }
}
