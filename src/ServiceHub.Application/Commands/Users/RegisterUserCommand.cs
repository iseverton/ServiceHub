using MediatR;
using ServiceHub.Domain.Common;

namespace ServiceHub.Application.Commands.Users;

public class RegisterUserCommand : IRequest<Result<Guid?>>
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public RegisterUserCommand(string name, string phoneNumber, string email, string password, string confirmPassword)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        ConfirmPassword = confirmPassword;
    }
}
