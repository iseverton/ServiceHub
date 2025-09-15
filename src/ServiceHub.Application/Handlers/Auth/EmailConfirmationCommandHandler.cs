using MediatR;
using Microsoft.Extensions.Logging;
using ServiceHub.Application.Commands.Auth;
using ServiceHub.Application.Services.Interfaces;
using ServiceHub.Domain.Common;

namespace ServiceHub.Application.Handlers.Auth;

public class EmailConfirmationCommandHandler : IRequestHandler<EmailConfirmationCommand, Result<NoContent>>
{
    private readonly ILogger<EmailConfirmationCommandHandler> _logger;
    private readonly IAuthService _authService;
    public EmailConfirmationCommandHandler(ILogger<EmailConfirmationCommandHandler> logger, IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }
    public async Task<Result<NoContent>> Handle(EmailConfirmationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling EmailConfirmationCommand for UserId: {UserId}", request.UserId);
        var result = await _authService.ConfirmEmailAsync(request.UserId, request.Token);
        if (!result)
        {
            _logger.LogWarning("Email confirmation failed for UserId: {UserId}", request.UserId);
            return Result<NoContent>.Fail("Email confirmation failed. Invalid token or user ID.");
        }
        
        _logger.LogInformation("Email confirmed successfully for UserId: {UserId}", request.UserId);
        return Result<NoContent>.Success();
    }
}
