using Microsoft.EntityFrameworkCore;
using ServiceHub.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Infrastructure.Data.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(ServiceHubDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
