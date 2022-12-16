using FinalProject.Domain;
using FluentValidation;

namespace FinalProject.Validations
{
    public class LoginValidation : AbstractValidator<LoginModel>
    {
        public LoginValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().Length(6, 20);
            RuleFor(x => x.Password).NotEmpty().Length(6, 18);
        }
    }
}
