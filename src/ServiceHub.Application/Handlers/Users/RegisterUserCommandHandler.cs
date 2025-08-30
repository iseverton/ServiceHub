using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ServiceHub.Application.Commands.Users;
using ServiceHub.Application.Services.Interfaces;
using ServiceHub.Domain.Entities;
using ServiceHub.Domain.Entities.Identity;
using ServiceHub.Domain.Interfaces.Repositories;
using ServiceHub.Domain.ValueObjects;
using ServiceHub.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServiceHub.Shared.Utils.ApiResponseHelpers;

namespace ServiceHub.Application.Handlers.Users;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ApiResponse<Guid?>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;
    public RegisterUserCommandHandler(IUserRepository userRepository,IAuthService authService)
    {
        _authService = authService;
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<Guid?>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
       
        // criar o usuario de identidade
        var resultRegisterIdentity = await _authService.
            Register(request.Email, request.PhoneNumber, request.Password);
        
        if (!resultRegisterIdentity.Succeeded) return ApiResponseHelpers.
                BadRequest<Guid?>(string.Join(", ", resultRegisterIdentity.Errors));

        // criar o usuario do dominio
        User user = new User(resultRegisterIdentity.ApplicationUser, request.Name);
        await _userRepository.AddAsync(user, cancellationToken);

        return ApiResponseHelpers.Created<Guid?>(user.Id);
    }

}
