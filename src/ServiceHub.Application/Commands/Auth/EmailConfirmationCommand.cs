using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServiceHub.Shared.Utils.ApiResponseHelpers;

namespace ServiceHub.Application.Commands.Auth;

public class EmailConfirmationCommand : IRequest<ApiResponse>
{
    public string Token { get; }
    public string UserId { get; }
    public EmailConfirmationCommand(string token, string userId)
    {
        Token = token;
        UserId = userId;
    }
}
