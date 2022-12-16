using FinalProject.Models;
using FluentValidation;

namespace FinalProject.Validations
{
    public class RegistrationValidation : AbstractValidator<RegistrationModel>
    {

        public RegistrationValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().Length(0, 50);
            RuleFor(x => x.LastName).NotEmpty().Length(0,50);
            RuleFor(x => x.Age).NotEmpty().GreaterThanOrEqualTo(18);
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.SalaryPerMonth).NotEmpty();           
            RuleFor(x => x.UserName).NotEmpty().Length(6, 20);
            RuleFor(x => x.Password).NotEmpty().Length(6,18);
        }
    }
}
