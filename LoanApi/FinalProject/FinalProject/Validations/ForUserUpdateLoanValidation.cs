using FinalProject.DomainpropertyHelpers;
using FinalProject.Models;
using FluentValidation;

namespace FinalProject.Validations
{
    public class ForUserUpdateLoanValidation : AbstractValidator<ForUserUpdateLoanModel>
    {
        public ForUserUpdateLoanValidation()
        {
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.Currency).NotEmpty();
            RuleFor(x => x.LoanPeriodmonthly).NotEmpty();
            RuleFor(x => x.LoanType == LoanType.Slow || x.LoanType == LoanType.Fast || x.LoanType == LoanType.Standart)
                .NotEmpty().WithMessage("Select (\"FAST\",\"SLOW\",\"STANDART\") in UpperCase !!!");
        }
    }
}
