using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServiceHub.Shared.Utils.ApiResponseHelpers;

namespace ServiceHub.Application.Commands.Users;

public class RegisterUserCommand : IRequest<ApiResponse<Guid?>>
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
