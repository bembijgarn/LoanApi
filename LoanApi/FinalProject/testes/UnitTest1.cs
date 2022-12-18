using FinalProject.DATA;
using FinalProject.Domain;
using FinalProject.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using Xunit;

namespace testes
{
    
    public class UnitTest1
    {
        private readonly UserContext _context;
        
        public UnitTest1(UserContext context)
        {
            _context = context;
        }
        [Fact]
        public void TestMethod1()
        {
            var Logzor = new Adminservice(_context);
            var test = Logzor.GettAll();
            
        }
    }
}
