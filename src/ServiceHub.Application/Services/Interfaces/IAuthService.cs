using ServiceHub.Application.DTOS.Auth;
using ServiceHub.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Application.Services.Interfaces;

/// <summary> 
///     Interface que define os metodos de autenticacao e registro de usuarios
/// </summary>
public interface IAuthService 
{
    Task<LoginResultDTO> LoginAsync(string email, string password);

    /// <summary>
    /// Metado responsavel por registrar um novo usuario de identidade
    /// </summary>
    Task<ResultRegisterIdentityDTO> Register(string email,string phone,string password);
    Task<bool> SendEmailConfirmationAsync(ApplicationUser user);
    Task<bool> ConfirmEmailAsync(string userId, string token);
    Task ResetPassword();

}
