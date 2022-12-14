using FinalProject.Domain;
using System.Collections.Generic;

namespace FinalProject.Interfaces
{
    public interface Iadminservice
    {
        User Removeuser(int id);
        IEnumerable<User> GettAll();
        Loan RemoveLoanbyUserId(int id);
        Loan ChekLoan(string mail);
        Loan UpdateUserLoan(Loan loan, int id);
        User BlockOrUnblockUserbyId(User user, int id, string Block);
        Loan AccepLoanbyUserId(Loan loan,int Id);
    }
}
