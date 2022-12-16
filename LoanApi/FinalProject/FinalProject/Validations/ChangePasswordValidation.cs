using FinalProject.Models;
using FluentValidation;

namespace FinalProject.Validations
{
    public class ChangePasswordValidation : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordValidation()
        {
            RuleFor(x => x.NewPassword).MinimumLength(6).NotEmpty();
        }
    }
}
