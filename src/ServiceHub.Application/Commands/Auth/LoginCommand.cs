using MediatR;
using ServiceHub.Domain.Common;

namespace ServiceHub.Application.Commands.Auth;

public class LoginCommand : IRequest<Result<string>>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public LoginCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
