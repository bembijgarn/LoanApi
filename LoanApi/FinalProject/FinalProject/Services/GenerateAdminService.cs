using FinalProject.DATA;
using FinalProject.Domain;
using FinalProject.Interfaces;
using FinalProject.Models;
using FinalProject.PasswordHashing;

namespace FinalProject.Services
{
    public class GenerateAdminService : IGenerateAdminInterface
    {

        private readonly UserContext _context;
        public GenerateAdminService(UserContext context)
        {
            _context = context;

        }
       public User GenerateAdmin()
        {
            User Dbuser = new User();
            Dbuser.FirstName = "Admin";
            Dbuser.LastName = "Admin";
            Dbuser.UserName = "admin000";
            Dbuser.Password = "admin007";
            Dbuser.Email = "admin00@gmail.com";
            Dbuser.Age = 777;
            Dbuser.Role = Role.Accountant;
            Dbuser.SalaryPerMonth = 10000;

            _context.Add(Dbuser);
            _context.SaveChanges();
            return Dbuser;
        }
    }
}
