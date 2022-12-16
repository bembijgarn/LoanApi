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
using System.Security.Cryptography.X509Certificates;
using FinalProject.Models;

namespace FinalProject.Services
{
    public class UserService : Iuserservice
    {
        private readonly UserContext _context;
        public UserService(UserContext context)
        {
            _context = context;
            
        }      
        public Loan AddLoan(ForUserUpdateLoanModel loan,string mail)
        {
            var User = _context.User.Where(x => x.Email == mail  &&  x.IsBlocked == BlockedOrNot.Unblocked).FirstOrDefault();
            if (User == null)
            {
                return null;
            }
            var userloan = _context.Loan.Where(x => x.UserId == User.Id && x.LoanCondition != Loancondition.Active
            || User.IsBlocked != BlockedOrNot.Blocked).FirstOrDefault();

            if (userloan == null)
            {              
                return null;
            }
            User.loan.Add(new Loan
            {
                LoanType = loan.LoanType,
                Amount = loan.Amount,
                Currency = loan.Currency,
                LoanPeriodmonthly = loan.LoanPeriodmonthly,
                LoanCondition = Loancondition.Active,
                Status = LoanStatus.Requested,
            });
            _context.User.Attach(User);
            _context.SaveChanges();            
            return userloan;           
        }
        public Loan UpdateLoan(ForUserUpdateLoanModel loan,int currentid)
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
                return Dbuser;
            }
            return null;
        }
       public Loan CheckLoan(int currenUsertid)
        {
            var Dbloan = _context.Loan.FirstOrDefault(x => x.UserId == currenUsertid && x.LoanCondition == Loancondition.Active);
            if (Dbloan != null)
            {
                return Dbloan;
            }
            return null;
        }
        public Loan RemoveLoan(int currenusertid)
        {
            var Dbloan = _context.Loan.FirstOrDefault(x => x.UserId == currenusertid && x.Status != LoanStatus.Accpeted);
            if (Dbloan != null)
            {
                _context.Loan.Remove(Dbloan);
                _context.SaveChanges();
                return Dbloan;
            }
            return null;
        }
        
        #region checkusersalarymethod
        public bool CheckUserSalary(ForUserUpdateLoanModel loan, double salary)
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
