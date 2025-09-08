using MediatR;
using Microsoft.Extensions.Logging;
using ServiceHub.Application.Commands.Auth;
using ServiceHub.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServiceHub.Shared.Utils.ApiResponseHelpers;

namespace ServiceHub.Application.Handlers.Auth;

public class EmailConfirmationCommandHandler : IRequestHandler<EmailConfirmationCommand, ApiResponse>
{
    private readonly ILogger<EmailConfirmationCommandHandler> _logger;
    private readonly IAuthService _authService;
    public EmailConfirmationCommandHandler(ILogger<EmailConfirmationCommandHandler> logger, IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }
    public async Task<ApiResponse> Handle(EmailConfirmationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling EmailConfirmationCommand for UserId: {UserId}", request.UserId);
        var result = await _authService.ConfirmEmailAsync(request.UserId, request.Token);
        if (!result)
        {
            _logger.LogWarning("Email confirmation failed for UserId: {UserId}", request.UserId);
            return BadRequest("Email confirmation failed. Invalid token or user ID.");
        }
        
        _logger.LogInformation("Email confirmed successfully for UserId: {UserId}", request.UserId);
        return Ok("Email confirmed successfully.");
    }
}
