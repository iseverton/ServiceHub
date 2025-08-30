using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceHub.Domain.Entities.Identity;
using ServiceHub.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Infrastructure.Data.Repositories;

public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly ServiceHubDbContext _context;
    public ApplicationUserRepository(ServiceHubDbContext context)
    {
        _context = context;
    }
    public async Task<ApplicationUser?> FindByPhoneAsync(string phoneNumber, CancellationToken cancellationToken)
    {
       return await _context.ApplicationUsers.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber, cancellationToken);
    }
}
