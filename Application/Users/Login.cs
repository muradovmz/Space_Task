using Application.Core;
using Application.Services;
using Application.Users.DTOs;
using Application.Users.Validators;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users
{
    public class Login
    {
        public class Command : IRequest<Result<string>>
        {
            public LoginDto Login { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Login).SetValidator(new LoginValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly TokenService _tokenService;
            public Handler(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, TokenService tokenService)
            {
                _tokenService = tokenService;
                _signInManager = signInManager;
                _userManager = userManager;
            }
            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Login.Email);

                if (user == null) return Result<string>.Unauthorized();

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Login.Password, false);

                if (result.Succeeded)
                {
                    return Result<string>.Success(_tokenService.CreateToken(user));
                }

                return Result<string>.Unauthorized();
            }
        }
    }
}