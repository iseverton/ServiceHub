using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<RegisterUserCommandHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;
    private readonly IEmailService _emailService;
    public RegisterUserCommandHandler(ILogger<RegisterUserCommandHandler> logger, 
        IUserRepository userRepository, IAuthService authService, IEmailService emailService)
    {
        _logger = logger;
        _authService = authService;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<ApiResponse<Guid?>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {

        // criar o usuario de identidade
        _logger.LogInformation("Starting ApplicationUser registration for email: {Email}", request.Email);

        var resultRegisterIdentity = await _authService.
            Register(request.Email, request.PhoneNumber, request.Password);

        if (!resultRegisterIdentity.Succeeded) 
        {
            _logger.LogWarning("Failed to register ApplicationUser for email: {Email}. Errors: {Errors}",
                request.Email, string.Join(", ", resultRegisterIdentity.Errors));
           
            return ApiResponseHelpers.
                BadRequest<Guid?>(string.Join(", ", resultRegisterIdentity.Errors));
        } 

        // criar o usuario do dominio
        User user = new User(resultRegisterIdentity.ApplicationUser, request.Name);
        await _userRepository.AddAsync(user, cancellationToken);

        var emailConfirmationResult = await _authService.SendEmailConfirmationAsync(resultRegisterIdentity.ApplicationUser);
        if (!emailConfirmationResult)
        {
            _logger.LogWarning("Failed to send email confirmation for user with ID: {UserId}", user.Id);
            return ApiResponseHelpers.
                BadRequest<Guid?>("Failed to send email confirmation. Please try again later.");
        }

        _logger.LogInformation("Successfully registered User with ID: {UserId}", user.Id);

        return ApiResponseHelpers.Created<Guid?>(user.Id);
    }
   
}


