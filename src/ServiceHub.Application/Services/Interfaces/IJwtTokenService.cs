using ServiceHub.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Application.Services.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(ApplicationUser applicationUser);
}
