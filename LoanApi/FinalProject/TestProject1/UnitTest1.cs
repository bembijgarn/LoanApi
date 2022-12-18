using FinalProject.DATA;
using FinalProject.Domain;
using FinalProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        private readonly UserContext _context;
        
        public UnitTest1(UserContext context)
        {
            this._context = context;
        }


        [Fact]
        public void Test1()
        {              
            var actual = new Adminservice(_context).GettAll();
            Assert.IsType<OkObjectResult>(actual as OkObjectResult);
        }

       
    }
}
