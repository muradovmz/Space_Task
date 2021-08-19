using Application.Core;
using Application.Services;
using Application.Users.DTOs;
using Application.Users.Validators;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users
{
    public class Register
    {
        public class Command : IRequest<Result<Unit>>
        {
            public RegisterDto Registration { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Registration).SetValidator(new RegisterValidator());
            }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly UserManager<AppUser> _userManager;
            public Handler(UserManager<AppUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _userManager.Users.AnyAsync(x => x.Email == request.Registration.Email, cancellationToken: cancellationToken))
                {
                    return Result<Unit>.Failure("Email taken");
                }
                if (await _userManager.Users.AnyAsync(x => x.UserName == request.Registration.Username, cancellationToken: cancellationToken))
                {
                    return Result<Unit>.Failure("Username taken");
                }

                var user = new AppUser
                {
                    PrivateNumber = request.Registration.PrivateNumber,
                    Email = request.Registration.Email,
                    UserName = request.Registration.Username
                };

                var result = await _userManager.CreateAsync(user, request.Registration.Password);

                if (result.Succeeded)
                {
                    return Result<Unit>.Success(Unit.Value);
                }

                return Result<Unit>.Failure("Problem registering user");
            }
        }
    }
}