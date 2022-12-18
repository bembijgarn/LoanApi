using FinalProject.DATA;
using FinalProject.Domain;
using FinalProject.Interfaces;
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
        private readonly Iadminservice _adminservic;
        private  readonly UserContext _context;
        
        public UnitTest1(Iadminservice service,UserContext context)
        {
            this._adminservic = service;
            this._context = context;

        }


        [Fact]
        public void Getall_Test1()
        {              
            var actual = _adminservic.GettAll();
            Assert.IsType<OkObjectResult>(actual as OkObjectResult);
        }
        [Fact]
        public void RemoveUser_Test()
        {
            int id = 10;
            var actual = _adminservic.Removeuser(id);

            Assert.NotNull(actual);
        }
        [Fact]
        public void checkloan_Test()
        {
            string mail = "aeazkmy@gmail.com";
            var actual = _adminservic.ChekLoan(mail);

            Assert.NotNull(actual);
        }
        [Fact]
        public void Blokcorunblokcuser_Test()
        {
            int id = 43;
            string blockorunblokc = "BLOCK";
            var actual = _adminservic.BlockOrUnblockUserbyId(id,blockorunblokc);
            Assert.NotNull(actual);

            
        }
       
    }
}
