using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Loan_API.Controllers;
using FinalProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinalProject.HELPERS;
using FinalProject.Interfaces;
using Microsoft.Extensions.Options;
using FinalProject.Domain;
using FinalProject.TokenGenerator;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    [Authorize]
    [Route("Api/[Controller]")]
    public class LoginRegistrationController : Controller
    {
        private ILoginRegistrationInterface _loginservice;
        private readonly AppSettings _appSettings;

        public LoginRegistrationController( ILoginRegistrationInterface loginservice, IOptions<AppSettings> appsettings)
        {
            
            _loginservice = loginservice;
            _appSettings = appsettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Logzorr(LoginModel user)
        {           
            var Dbuser = _loginservice.LoginUser(user);
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
        }
        [AllowAnonymous]
        [HttpPost("Registration")]
        public IActionResult  Registration(RegistrationModel user)
        {
            var Dbuser = _loginservice.RegistrationUser(user);
            if (Dbuser == null)
            {
                return BadRequest("Username Or Email Already Exist");
            }
            return Ok(user);
        }
    }
}
