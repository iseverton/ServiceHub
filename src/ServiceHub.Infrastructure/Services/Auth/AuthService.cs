using Azure.Core;
using Microsoft.AspNetCore.Identity;
using ServiceHub.Application.DTOS;
using ServiceHub.Application.Services.Interfaces;
using ServiceHub.Domain.Entities.Identity;
using ServiceHub.Domain.Interfaces.Repositories;
using ServiceHub.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceHub.Infrastructure.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationUserRepository _applicationUserRepository;
    public AuthService(UserManager<ApplicationUser> userManager,IApplicationUserRepository applicationUserRepository)
    {
        _userManager = userManager;
        _applicationUserRepository = applicationUserRepository;
    }

    public Task<string> Login()
    {
        throw new NotImplementedException();
    }

    public async Task<ResultRegisterIdentityDTO> Register(string email, string phone, string password)
    {

        // verificacoes de campos unicos
        var existingEmail = await _userManager.FindByEmailAsync(email);
        if (existingEmail != null) return new ResultRegisterIdentityDTO
        {
            ApplicationUser = null,
            Errors = new List<string> { "Email already in use" },
            Succeeded = false
        };

        var existingPhone = await _applicationUserRepository.
            FindByPhoneAsync(phone, CancellationToken.None);
        if (existingPhone != null) return new ResultRegisterIdentityDTO
        {
            ApplicationUser = null,
            Errors = new List<string> { "Phone number already in use" },
            Succeeded = false
        };

        // criar o usuario de identidade
        ApplicationUser applicationUser = new ApplicationUser(email, phone);
        var result = await _userManager.CreateAsync(applicationUser, password);
        return new ResultRegisterIdentityDTO
        {
            ApplicationUser = result.Succeeded ? applicationUser : null,
            Errors = !result.Succeeded ? result.Errors.Select(e => e.Description).ToList() : null,
            Succeeded = result.Succeeded
        };
    }

    public Task ResetPassword()
    {
        throw new NotImplementedException();
    }
}
