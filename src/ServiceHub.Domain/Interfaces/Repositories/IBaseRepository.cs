using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);

}
