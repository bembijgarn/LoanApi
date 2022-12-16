using FinalProject.Domain;
using System.Collections.Generic;
using System;
using FinalProject.Models;

namespace FinalProject.Interfaces
{
    public interface ILoginRegistrationInterface
    {
        User LoginUser(LoginModel user);
        User RegistrationUser(RegistrationModel user);
        User ChangePassword(ChangePasswordModel changepassword, string currentuseremail);


    }
}
