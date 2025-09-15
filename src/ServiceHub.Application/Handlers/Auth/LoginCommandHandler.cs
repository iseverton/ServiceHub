using MediatR;
using ServiceHub.Application.Commands.Auth;
using ServiceHub.Application.Services.Interfaces;
using ServiceHub.Domain.Common;

namespace ServiceHub.Application.Handlers.Auth;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<string>>
{
    private readonly IAuthService _authService;
    private readonly IJwtTokenService _jwtTokenService;
    public LoginCommandHandler(IAuthService authService, IJwtTokenService jwtTokenService)
    {
        _authService = authService;
        _jwtTokenService = jwtTokenService;
    }
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request.Email, request.Password);
        if(!result.Succeeded) return Result<string>.Fail(string.Join(", ", result.Errors));

        var token = await _jwtTokenService.GenerateToken(result.applicationUser);
        return Result<string>.Success(token);
    }
}
