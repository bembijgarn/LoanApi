using FinalProject.HELPERS;
using FinalProject.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection.Metadata.Ecma335;

namespace FinalProject.Controllers
{
    [Route("api/[Controller]")]
    public class GenerateAdminController : Controller
    {
        private IGenerateAdminInterface _adminservice;
        
        public GenerateAdminController(IGenerateAdminInterface adminservice)
        {
            _adminservice = adminservice;
        }

        [HttpPost("GenerateAdmin")]
        public IActionResult GenerateAdmin()
        {          
            Serilog.Log.Information("Called GenerateAdminController");
            try
            {
                var admingenerate = _adminservice.GenerateAdmin();
                return Ok(new
                {
                    Username = admingenerate.UserName,
                    Password = admingenerate.Password,
                });
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Error GenerateAdminController {0}", ex);
            }
            return null;
        }
    }
}
