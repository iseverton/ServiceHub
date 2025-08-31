using MediatR;
using ServiceHub.Application.Commands.Auth;
using ServiceHub.Application.Services.Interfaces;
using ServiceHub.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServiceHub.Shared.Utils.ApiResponseHelpers;

namespace ServiceHub.Application.Handlers.Auth;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<string>>
{
    private readonly IAuthService _authService;
    private readonly IJwtTokenService _jwtTokenService;
    public LoginCommandHandler(IAuthService authService, IJwtTokenService jwtTokenService)
    {
        _authService = authService;
        _jwtTokenService = jwtTokenService;
    }
    public async Task<ApiResponse<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.Login(request.Email, request.Password);
        if(result == null) return ApiResponseHelpers.Unauthorized<string>("Invalid email or password");

        var token = await _jwtTokenService.GenerateToken(result);
        return ApiResponseHelpers.Ok<string>(token);
    }
}
