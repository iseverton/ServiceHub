using Microsoft.EntityFrameworkCore;
using ServiceHub.Domain.Entities;
using ServiceHub.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Infrastructure.Data.Repositories;

public class ServiceCategoryRepository : BaseRepository<ServiceCategory>, IServiceCategoryRepository
{
    public ServiceCategoryRepository(ServiceHubDbContext context) : base(context)
    {
    }

    public async Task<ICollection<ServiceCategory>> GetAllById(ICollection<Guid> categories, CancellationToken cancellationToken)
    {
       return await _dbSet.Where(c => categories.Contains(c.Id)).ToListAsync(cancellationToken);
    }
}
