using ServiceHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Domain.Interfaces.Repositories;

public interface IServiceCategoryRepository : IBaseRepository<ServiceCategory>
{
    Task<ICollection<ServiceCategory>> GetAllById(ICollection<Guid> categories,CancellationToken cancellationToken);
}
