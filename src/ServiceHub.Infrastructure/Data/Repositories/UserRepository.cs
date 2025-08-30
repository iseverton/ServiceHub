using Microsoft.EntityFrameworkCore;
using ServiceHub.Domain.Entities;
using ServiceHub.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Infrastructure.Data.Repositories;

public class UserRepository : BaseRepository<User>,IUserRepository
{
    public UserRepository(ServiceHubDbContext context) : base(context)
    {
    }

    
}
