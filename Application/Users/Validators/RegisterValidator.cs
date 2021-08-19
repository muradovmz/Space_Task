using Application.Users.DTOs;
using FluentValidation;

namespace Application.Users.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.PrivateNumber).NotNull().NotEmpty();
            RuleFor(x=>x.Username).NotNull().NotEmpty();
            RuleFor(x => x.Password).Matches("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$").WithMessage("Password must be complex");
        }
    }
}