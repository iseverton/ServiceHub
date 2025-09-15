using MediatR;
using ServiceHub.Domain.Common;

namespace ServiceHub.Application.Commands.Providers;

public class RegisterProviderCommand : IRequest<Result<Guid?>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public RegisterProviderCommand(string name, string description, string phoneNumber, string email, string password, string confirmPassword)
    {
        Name = name;
        Description = description;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        ConfirmPassword = confirmPassword;
    }
}
