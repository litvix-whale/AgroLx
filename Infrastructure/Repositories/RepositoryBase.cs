using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories;

public abstract class RepositoryBase<TEntity, TId>(AppDbContext context) : IRepositoryBase<TEntity, TId> 
    where TEntity : class
    where TId : struct
{
    protected readonly AppDbContext _context = context;
    protected readonly DbSet<TEntity> _dbSet= context.Set<TEntity>();

    public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<TEntity?> GetByIdAsync(TId id) => await _dbSet.FindAsync(id);
    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TId id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
