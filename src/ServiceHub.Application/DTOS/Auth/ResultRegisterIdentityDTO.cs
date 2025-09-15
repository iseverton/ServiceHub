using ServiceHub.Domain.Entities.Identity;

namespace ServiceHub.Application.DTOS.Auth;

/// <summary>
///  DTO que encapsula o resultado do registro de um usuario de identidade
/// </summary>
public class ResultRegisterIdentityDTO
{
    public ApplicationUser? ApplicationUser { get; set; }
    public List<string>? Errors { get; set; }
    public bool Succeeded { get; set; }
}
