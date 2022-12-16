using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using FinalProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinalProject.HELPERS;
using FinalProject.Interfaces;
using Microsoft.Extensions.Options;
using FinalProject.Domain;
using FinalProject.TokenGenerator;
using FinalProject.Models;
using System;
using FinalProject.Validations;
using System.Security.Claims;

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
        public IActionResult Login(LoginModel user)
        {
            Serilog.Log.Information("Called LoginregistrationController");
            try
            {
                var Validate = new LoginValidation().Validate(user);
                if (!Validate.IsValid)
                {
                    return BadRequest(Validate.Errors[0].ErrorMessage);
                }
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
            }catch(Exception ex)
            {
                Serilog.Log.Error("Error RegistrationLoginController {0}", ex);
            }
            return null;
        }

        [AllowAnonymous]
        [HttpPost("Registration")]
        public IActionResult  Registration(RegistrationModel user)
        {           
            Serilog.Log.Information("Called LoginRegistrationController");
            try
            {
                var Validate = new RegistrationValidation().Validate(user);
                if (!Validate.IsValid)
                {
                    return BadRequest(Validate.Errors[0].ErrorMessage);
                }
                var Dbuser = _loginservice.RegistrationUser(user);
                if (Dbuser == null)
                {
                    return BadRequest("Username Or Email Already Exist");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Error LoginRegistrationController {0}", ex);               
            }
            return null;
        }
        [Authorize(Roles = Role.User)]
        [HttpPut("ChangePassword")]
        public IActionResult ChangePassowrd(ChangePasswordModel Changepassword)
        {
            Serilog.Log.Information("Called LoginRegistrationController");
            try
            {
                var Validate = new ChangePasswordValidation().Validate(Changepassword);
                string Email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;
                if (!Validate.IsValid)
                {
                    return BadRequest(Validate.Errors[0].ErrorMessage);
                }
                if (Email == null) return BadRequest("Email Not Found");               
                var Dbuser = _loginservice.ChangePassword(Changepassword,Email);
                if (Dbuser != null)
                {
                    return Ok( new
                    {
                        Password = Changepassword.NewPassword
                    });
                }
                return BadRequest("Please Contact Support");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Error LoginRegistrationController {0}", ex);
            }
            return null;
        }
    }
}
