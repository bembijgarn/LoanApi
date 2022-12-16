using FinalProject.DATA;
using FinalProject.Domain;
using FinalProject.Interfaces;
using FinalProject.Models;
using FinalProject.PasswordHashing;
using System.Linq;

namespace FinalProject.Services
{
    public class LoginRegistrationService : ILoginRegistrationInterface
    {
        private readonly UserContext _context;
        public LoginRegistrationService(UserContext context)
        {
            _context = context;

        }
        public User LoginUser(LoginModel login)
        {
            var user = _context.User.Where(x => x.UserName == login.UserName).FirstOrDefault();
            if (user is null || new HashPassword().HashPass(login.Password) != user.Password)
            {
                return null;
            }
            return user;
        }
        public User RegistrationUser(RegistrationModel user)
        {
            var checkusernameAndEmail = _context.User.Where(x => x.Email == user.Email || x.UserName == user.UserName).FirstOrDefault();
            if (checkusernameAndEmail is null)
            {
                User Dbuser = new User();
                Dbuser.FirstName = user.FirstName;
                Dbuser.LastName = user.LastName;
                Dbuser.UserName = user.UserName;
                Dbuser.Password = new HashPassword().HashPass(user.Password);
                Dbuser.Email = user.Email;
                Dbuser.Age = user.Age;
                Dbuser.Role = Role.User;
                Dbuser.SalaryPerMonth = user.SalaryPerMonth;

                _context.Add(Dbuser);
                _context.SaveChanges();
                return Dbuser;
            }
            return null;          
        }
        public User ChangePassword(ChangePasswordModel changepassword, string currentuseremail)
        {
            var Dbuser = _context.User.Where(x => x.Email == currentuseremail).SingleOrDefault();
            if (Dbuser != null)
            {
                Dbuser.Password = new HashPassword().HashPass(changepassword.NewPassword);
                _context.Update(Dbuser);
                _context.SaveChanges();
                return Dbuser;
            }
            return null;
        }
    }
}
