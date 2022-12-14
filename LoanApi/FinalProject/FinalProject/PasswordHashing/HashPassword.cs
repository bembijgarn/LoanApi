using System;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace FinalProject.PasswordHashing
{
    public class HashPassword
    {
        public  string HashPass(string password)
        {
            SHA256 hash = SHA256.Create();
            var passwordbyte = Encoding.Default.GetBytes(password); 
            var hashedpassword = hash.ComputeHash(passwordbyte);
            return Convert.ToHexString(hashedpassword);

        }
    }
}
