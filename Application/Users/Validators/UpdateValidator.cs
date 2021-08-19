using Application.Users.DTOs;
using FluentValidation;

namespace Application.Users.Validators
{
    public class UpdateValidator : AbstractValidator<UpdateDto>
    {
        public UpdateValidator()
        {
            RuleFor(x => x.MonthSalary).GreaterThan(0).When(x => x.IsEmployed == true);
        }
    }
}