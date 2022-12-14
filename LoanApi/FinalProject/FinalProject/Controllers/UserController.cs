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
        [AllowAnonymous]
        [HttpPost("Registration")]
        public IActionResult Registration([FromBody] User user)
        {
           
            _service.Registration(user);
            return Ok(user);
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] User user)
        {
            Serilog.Log.Information("usercontroller log");
            try
            {
                var Dbuser = _service.Login(user);
                if (Dbuser == null)
                    return BadRequest("Username or Password is incorrect");
                string token = new GenerateTokenclass(_appSettings).GenerateToken(Dbuser);
                return Ok(new
                {
                    Username = Dbuser.UserName,
                    Password = Dbuser.Password,
                    Role = Dbuser.Role,
                    Token = token
                });
            }catch(Exception ex)
            {
                Serilog.Log.Error("error asdolasd", ex);
            }
            return Ok();
        }    
        [Authorize(Roles =Role.User)]
        [HttpPost("AddLoan/{mail}")]
        public IActionResult AddLoan([FromBody] Loan loan,User user, string mail)
        {
                var usersalary = Convert.ToDouble(User.Claims.Where(x => x.Type == ClaimTypes.Country).FirstOrDefault().Value);//claimtypes.country ==  authorized user.salary;
                var UserEmail = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;
                if (UserEmail == null) return NotFound("Invalid User");
                if (UserEmail != mail)
                {
                    return Forbid("message");
                }                   
            var Dbuser = _service.AddLoan(loan,user,mail);
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
        [Authorize(Roles = Role.User)]
        [HttpPut("UpdateLoan")]
        public IActionResult UpdateLoan([FromBody]Loan loan,int currentid)
        {
            currentid = Convert.ToInt32(User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value);
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
    }    
}

    

