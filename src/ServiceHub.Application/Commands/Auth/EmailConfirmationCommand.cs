using MediatR;
using ServiceHub.Domain.Common;

namespace ServiceHub.Application.Commands.Auth;

public class EmailConfirmationCommand : IRequest<Result<NoContent>>
{
    public string Token { get; }
    public string UserId { get; }
    public EmailConfirmationCommand(string token, string userId)
    {
        Token = token;
        UserId = userId;
    }
}
