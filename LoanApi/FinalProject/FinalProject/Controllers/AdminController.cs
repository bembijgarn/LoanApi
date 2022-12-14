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
using System.Reflection.Metadata.Ecma335;
using Azure.Core;

namespace FinalProject.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    public class AdminController : Controller
    {
        private readonly Iadminservice _adminservice;
        private readonly AppSettings _appSettings;
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger, Iadminservice adminservice, IOptions<AppSettings> appsettings)
        {
            _logger = logger;
            _adminservice = adminservice;
            _appSettings = appsettings.Value;
        }              
        [Authorize(Roles = Role.Accountant)]
        [HttpGet("GETAlluser")]
        public IActionResult GetUsers()
        {
           
                var users = _adminservice.GettAll();
                return Ok(users);           
            
        }
        [Authorize(Roles = Role.Accountant)]
        [HttpGet("Loaninfo/{mail}")]
        public IActionResult Getloaninfo(string mail)
        {
            var userloan = _adminservice.ChekLoan(mail);
            if (userloan == null)
            {
                return NotFound("user not found");
            }
            return Ok(userloan);
        }
        [Authorize(Roles = Role.Accountant)]
        [HttpPut("Updateuserloan/{id}")]
        public IActionResult UpdateUserLoan([FromBody] Loan loan, int id)
        {
            _adminservice.UpdateUserLoan(loan, id);
            return Ok("updated");
        }
        
        [Authorize(Roles = Role.Accountant)]
        [HttpPut("BlockOrUnblockUserByID/{id}/{Block}")]
        public IActionResult BlockOrUnblockUserById([FromBody] User user, int id, string Block)
        {
            if (Block.ToUpper() == "BLOCK")
            {
                _adminservice.BlockOrUnblockUserbyId(user, id, Block);
                return Ok("User Blocked");

            }
            if (Block.ToUpper() == "UNBLOCK")
            {
                _adminservice.BlockOrUnblockUserbyId(user, id, Block);
                return Ok("User Unblocked");
            }
            return BadRequest("Invalid  Admin Command");
        }       
        [Authorize(Roles = Role.Accountant)]
        [HttpPut("AcceptLoanByuserId/{id}")]
        public IActionResult AcceptUserloanByid([FromBody] Loan loan, int id)
        {
            var Dbuser = _adminservice.AccepLoanbyUserId(loan, id);
            if (Dbuser == null)
            {
                return NotFound("User Not Found");
            }
            return Ok("Accepted");
        }
        [Authorize(Roles = Role.Accountant)]
        [HttpDelete("as")]
        public IActionResult Removeuser(int id)
        {
            _adminservice.Removeuser(id);
            return Ok("deleted");
        }
        [Authorize(Roles = Role.Accountant)]
        [HttpDelete("RemoveloanbyUserid/{id}")]
        public IActionResult RemoveLoanByUserId(int id)
        {
            _adminservice.RemoveLoanbyUserId(id);
            return Ok("Loan Removed");
        }
    }
}
