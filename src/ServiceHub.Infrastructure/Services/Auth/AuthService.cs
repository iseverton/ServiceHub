using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
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
using System.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceHub.Infrastructure.Services.Auth;

public class AuthService : IAuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IEmailService _emailService;
    public AuthService(ILogger<AuthService> logger, UserManager<ApplicationUser> userManager, 
        IApplicationUserRepository applicationUserRepository, IEmailService emailService)
    {
        _logger = logger;
        _userManager = userManager;
        _applicationUserRepository = applicationUserRepository;
        _emailService = emailService;
    }

    public async Task<bool> ConfirmEmail(ApplicationUser user)
    {
        var ConfirmEmailtoken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        if (ConfirmEmailtoken == null)
        {
            _logger.LogWarning("Failed to generate email confirmation token for user with ID: {UserId}", user.Id);
            return false;
        }

        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(ConfirmEmailtoken));
        var url = $"http://localhost:7224/Auth/emailconfirmation?token={encodedToken}&email={user.Email}";
        await _emailService.SendEmailAsync(user.Email,"Confirmação de email",url);
        return true;
    }

    public async Task<ApplicationUser?> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return null;

        var result = await _userManager.CheckPasswordAsync(user, password);
        if (!result) return null;

        return user; 
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
        if (!result.Succeeded)
        {
            _logger.LogWarning("Failed to register ApplicationUser for email: {Email}. Errors: {Errors}",
                email, string.Join(", ", result.Errors.Select(e => e.Description)));
        }

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
