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
using FinalProject.Models;
using System.Threading;

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
            Serilog.Log.Information("Called admincontroler");
            try
            {
                var users = _adminservice.GettAll();
                return Ok(users);
            }catch(Exception ex)
            {
                Serilog.Log.Error("Error AdminController {0}",ex);
            }
            return null;           
        }

        [Authorize(Roles = Role.Accountant)]
        [HttpGet("Loaninfo/{mail}")]
        public IActionResult Getloaninfo(string mail)
        {           
            Serilog.Log.Information("Called admincontroler");
            try
            {
                var userloan = _adminservice.ChekLoan(mail);
                if (userloan == null)
                {
                    return NotFound("user has not Loan");
                }
                return Ok(userloan);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Error AdminController {0}", ex);
            }
            return null;
        }
        [Authorize(Roles = Role.Accountant)]
        [HttpPut("Updateuserloan/{id}")]

        public IActionResult UpdateUserLoan(UpdateUserLoanModel loan, int id)
        {           
            Serilog.Log.Information("Called admincontroler");
            try
            {
                var userloan = _adminservice.UpdateUserLoan(loan, id);
                if (userloan == null)
                {
                    return BadRequest("User Loan Not Found");
                }
                return Ok("updated");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Error AdminController {0}", ex);
            }
            return null;
        }

        [Authorize(Roles = Role.Accountant)]
        [HttpPut("BlockOrUnblockUserByID/{id}/{Block}")]
        public IActionResult BlockOrUnblockUserById(int id, string Block)
        {           
            Serilog.Log.Information("Called admincontroler");
            try
            {
                if (Block.ToUpper() == "BLOCK")
                {
                    _adminservice.BlockOrUnblockUserbyId(id, Block);
                    return Ok("User Blocked");

                }
                if (Block.ToUpper() == "UNBLOCK")
                {
                    _adminservice.BlockOrUnblockUserbyId(id, Block);
                    return Ok("User Unblocked");
                }
                return BadRequest("Invalid  Admin Command");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Error AdminController {0}", ex);
            }
            return null;
        }

        [Authorize(Roles = Role.Accountant)]
        [HttpPut("AcceptLoanByuserId/{id}")]
        public IActionResult AcceptUserloanByid(int id)
        {           
            Serilog.Log.Information("Called admincontroler");
            try
            {
                var Dbuser = _adminservice.AccepLoanbyUserId(id);
                if (Dbuser == null)
                {
                    return NotFound("User Not Found");
                }
                return Ok("Accepted");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Error AdminController {0}", ex);
            }
            return null;
        }

        [Authorize(Roles = Role.Accountant)]
        [HttpDelete("RemoveUserById/{id}")]
        public IActionResult RemoveuserById(int id)
        {           
            Serilog.Log.Information("Called admincontroler");
            try
            {
                var deleteuser = _adminservice.Removeuser(id);
                if (deleteuser == null)
                {
                    return BadRequest("User Not Found");
                }
                return Ok("deleted");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Error AdminController {0}", ex);
            }
            return null;
        }

        [Authorize(Roles = Role.Accountant)]
        [HttpDelete("RemoveloanbyUserid/{id}")]
        public IActionResult RemoveLoanByUserId(int id)
        {          
            Serilog.Log.Information("Called admincontroler");
            try
            {
                _adminservice.RemoveLoanbyUserId(id);
                return Ok("Loan Removed");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Error AdminController {0}", ex);
            }
            return null;
        }
    }
}