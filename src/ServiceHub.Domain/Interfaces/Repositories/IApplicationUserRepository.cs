using ServiceHub.Domain.Entities;
using ServiceHub.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Domain.Interfaces.Repositories;

public interface IApplicationUserRepository
{
    Task<ApplicationUser?> FindByPhoneAsync(string phoneNumber, CancellationToken cancellationToken);
   
}
