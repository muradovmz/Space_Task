using Application.Core;
using Application.Users.DTOs;
using Application.Users.Validators;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users
{
    public class Update
    {
        public class Command : IRequest<Result<Unit>>
        {
            public UpdateDto Update { get; set; }
            public AppUser User { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Update).SetValidator(new UpdateValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IMapper _mapper;
            public Handler(UserManager<AppUser> userManager,IMapper mapper)
            {
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                _mapper.Map(request.Update, request.User);
                var result = await _userManager.UpdateAsync(request.User);

                if (result.Succeeded)
                {
                    return Result<Unit>.Success(Unit.Value);
                }

                return Result<Unit>.Failure("Problem updating user");
            }
        }
    }
}
