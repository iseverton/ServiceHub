using ServiceHub.Domain.Entities.Identity;

namespace ServiceHub.Application.DTOS.Auth;

public class LoginResultDTO
{
    public bool Succeeded { get; set; }
    public ApplicationUser? applicationUser { get; set; }
    public List<string>? Errors { get; set; }

}
