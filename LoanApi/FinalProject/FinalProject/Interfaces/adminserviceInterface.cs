using FinalProject.Domain;
using FinalProject.Models;
using System.Collections.Generic;

namespace FinalProject.Interfaces
{
    public interface Iadminservice
    {
        User Removeuser(int id);
        IEnumerable<User> GettAll();
        Loan RemoveLoanbyUserId(int id);
        Loan ChekLoan(string mail);
        Loan UpdateUserLoan(UpdateUserLoanModel userloan, int id);
        User BlockOrUnblockUserbyId(int id, string Block);
        Loan AccepLoanbyUserId(int Id);
    }
}
