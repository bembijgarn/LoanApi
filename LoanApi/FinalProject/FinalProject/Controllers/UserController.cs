using FinalProject.Domain;
using FinalProject.HELPERS;
using FinalProject.Interfaces;
using FinalProject.TokenGenerator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.EntityFrameworkCore;
using FinalProject.PasswordHashing;
using System.Net;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using FinalProject.Services;
using FinalProject.Models;
using FinalProject.Validations;
using FinalProject.DomainpropertyHelpers;

namespace FinalProject.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private Iuserservice _service;
        private readonly AppSettings _appSettings;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, Iuserservice userservice, IOptions<AppSettings> appsettings)
        {
            _logger = logger;
            _service = userservice;
            _appSettings = appsettings.Value;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddLoan/{mail}")]
        public IActionResult AddLoan(ForUserUpdateLoanModel loan,  string mail)
        {            
            Serilog.Log.Information("Called UserController");
            try
            {
                var Validate = new ForUserUpdateLoanValidation().Validate(loan);
                if (!Validate.IsValid)
                {
                    return BadRequest(Validate.Errors[0].ErrorMessage);
                }
                var usersalary = Convert.ToDouble(User.Claims.Where(x => x.Type == ClaimTypes.Country).FirstOrDefault().Value);//claimtypes.country ==  authorized user.salary;
                var UserEmail = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;
                if (UserEmail == null) return NotFound("Invalid User");
                if (UserEmail != mail)
                {
                    return BadRequest("Invalid Email Adress");
                }
                var Dbuser = _service.AddLoan(loan, mail);
                if (Dbuser == null)
                {
                    return BadRequest("You have already Active LOAN");
                }
                var checksalary = _service.CheckUserSalary(loan, usersalary);
                if (checksalary == false)
                {
                    return BadRequest("You Cant request this Amount of loan");
                }
                return Ok("Loan Requested" + new
                {
                    LoanType = loan.Currency,
                    Currecy = loan.Currency,
                    Amount = loan.Amount,
                    LoanPeriod = loan.LoanPeriodmonthly + " Month",
                });
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Error UserController {0}", ex);
            }
            return null;
        }

        [Authorize(Roles = Role.User)]
        [HttpPut("UpdateLoan")]
        public IActionResult UpdateLoan(ForUserUpdateLoanModel loan)
        {            
            Serilog.Log.Information("Called UserController");
            try
            {
                var Validate = new ForUserUpdateLoanValidation().Validate(loan);
                if (!Validate.IsValid)
                {
                    return BadRequest(Validate.Errors[0].ErrorMessage);
                }
                int currentid = Convert.ToInt32(User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value);
                var Userloan = _service.UpdateLoan(loan, currentid);
                Userloan.UserId = currentid;
                if (currentid != Userloan.UserId)
                {
                    return BadRequest("You havenot access");
                }
                if (Userloan != null)
                {
                    return Ok(new
                    {
                        Loantype = Userloan.LoanType,
                        Amount = Userloan.Amount,
                        Currecy = Userloan.Currency,
                        Loanperiodmonthly = Userloan.LoanPeriodmonthly + " Month",
                        Status = Userloan.Status,
                        Loancondition = Userloan.LoanCondition
                    });
                }
                return BadRequest("User not found");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Error UserController {0}", ex);
            }
            return null;
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("CheckLoanInfo")]
        public IActionResult CheckYourLoanInfo()
        {            
            Serilog.Log.Information("Called UserController");
            try
            {
                int currentuserid = Convert.ToInt32(User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value);
                var Userloan = _service.CheckLoan(currentuserid);
                if (Userloan != null)
                {
                    return Ok(new
                    {
                        Loantype = Userloan.LoanType,
                        Amount = Userloan.Amount,
                        Currecy = Userloan.Currency,
                        Loanperiodmonthly = Userloan.LoanPeriodmonthly + " Month",
                        Status = Userloan.Status,
                        Loancondition = Userloan.LoanCondition
                    });
                }
                return BadRequest("You Have Not Loan");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Error UserController {0}", ex);
            }
            return null;
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteLoan")]
        public IActionResult DeleteLoan()
        {            
            Serilog.Log.Information("Called UserController");
            try
            {
                int currentuserid = Convert.ToInt32(User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value);
                var userloan = _service.RemoveLoan(currentuserid);
                if (userloan != null)
                {
                    return Ok("Your Loan Is Deleted");
                }
                return BadRequest("You Can't Remove Loan,Please Contact Support");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Error UserController {0}", ex);
            }
            return null;            
        }
    }    
}

    

