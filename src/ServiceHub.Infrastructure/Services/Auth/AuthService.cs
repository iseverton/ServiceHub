using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using ServiceHub.Application.DTOS.Auth;
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
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IEmailService _emailService;
    public AuthService(ILogger<AuthService> logger, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,IApplicationUserRepository applicationUserRepository, IEmailService emailService)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _applicationUserRepository = applicationUserRepository;
        _emailService = emailService;
    }

    public async Task<bool> SendEmailConfirmationAsync(ApplicationUser user)
    {
        var ConfirmEmailtoken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        if (ConfirmEmailtoken == null)
        {
            _logger.LogWarning("Failed to generate email confirmation token for user with ID: {UserId}", user.Id);
            return false;
        }

        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(ConfirmEmailtoken));
        var url = $"http://localhost:7224/Auth/emailconfirmation?token={encodedToken}&id={user.Id}";
        await _emailService.SendEmailAsync(user.Email, "Confirmação de email", url);
        return true;
    }

    public async Task<LoginResultDTO> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user == null) return new LoginResultDTO
        {
            Succeeded = false,
            applicationUser = null,
            Errors = new List<string> { "Invalid email or password" }
        };


        var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);

        if (result.IsLockedOut)
        {
            _logger.LogWarning("User account locked out for email: {Email}", email);
            return new LoginResultDTO
            {
                Succeeded = false,
                applicationUser = null,
                Errors = new List<string> { "User account is locked out" }
            };
        }


        if (!result.Succeeded)
        {
           return new LoginResultDTO
            {
                Succeeded = false,
                applicationUser = null,
                Errors = new List<string> { "Invalid email or password" }
            };
        }
        
        if (!result.IsNotAllowed)
        {
            _logger.LogWarning("Login attempt failed: email not confirmed for user with email: {Email}", email);
            return new LoginResultDTO
            {
                Succeeded = false,
                applicationUser = null,
                Errors = new List<string> { "Email not confirmed" }
            };
        }

        return new LoginResultDTO
        {
            Succeeded = result.Succeeded,
            applicationUser = user
        };

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

    public async Task<bool> ConfirmEmailAsync(string userId, string token)
    {
        var existingUser = await _userManager.FindByIdAsync(userId);
        if (existingUser == null) return false;

        var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

        var result = await _userManager.ConfirmEmailAsync(existingUser, decodedToken);
        if (!result.Succeeded)
        {
            _logger.LogWarning("Email confirmation failed for user with ID: {UserId}. Errors: {Errors}",
              userId, string.Join(", ", result.Errors.Select(e => e.Description)));
            return false;
        }

        return true;
    }
}
