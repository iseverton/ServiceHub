using ServiceHub.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Application.DTOS;

/// <summary>
///  DTO que encapsula o resultado do registro de um usuario de identidade
/// </summary>
public class ResultRegisterIdentityDTO
{
    public ApplicationUser? ApplicationUser { get; set; }
    public List<string>? Errors { get; set; }
    public bool Succeeded { get; set; }
}
