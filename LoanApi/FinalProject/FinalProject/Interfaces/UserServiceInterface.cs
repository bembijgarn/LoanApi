using FinalProject.Domain;
using System.Collections.Generic;
using System;

namespace FinalProject.Interfaces
{
    public interface Iuserservice
    {
        User Registration(User user);
        User Login(User user);
        Loan AddLoan(Loan loan,User user,string mail);
        bool CheckUserSalary(Loan loan, double salary);
        Loan UpdateLoan(Loan loan,int currentId);




    }
}
