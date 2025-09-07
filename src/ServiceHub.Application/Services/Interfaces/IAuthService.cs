using ServiceHub.Application.DTOS;
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
    Task<ApplicationUser?> Login(string email, string password);

    /// <summary>
    /// Metado responsavel por registrar um novo usuario de identidade
    /// </summary>
    Task<ResultRegisterIdentityDTO> Register(string email,string phone,string password);
    Task<bool> ConfirmEmail(ApplicationUser user);
    Task ResetPassword();

}
