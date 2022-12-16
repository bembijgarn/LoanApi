using FinalProject.Domain;
using System.Collections.Generic;
using System;
using FinalProject.Models;

namespace FinalProject.Interfaces
{
    public interface Iuserservice
    {      
        Loan AddLoan(ForUserUpdateLoanModel loan,string mail);
        bool CheckUserSalary(ForUserUpdateLoanModel loan, double salary);
        Loan UpdateLoan(ForUserUpdateLoanModel loan,int currentId);





    }
}
