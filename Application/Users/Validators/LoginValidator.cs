using Application.Users.DTOs;
using FluentValidation;

namespace Application.Users.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x=>x.Email).EmailAddress();
            RuleFor(x=>x.Password).NotEmpty().NotNull();
        }
    }
}