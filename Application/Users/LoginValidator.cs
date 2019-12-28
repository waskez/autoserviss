using AutoServiss.Shared.Users;
using FluentValidation;

namespace AutoServiss.Application.Users
{
    public class LoginValidator : AbstractValidator<LoginQuery>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Nav norādīta E-pasta adrese");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Nav norādīta Parole");
        }
    }
}