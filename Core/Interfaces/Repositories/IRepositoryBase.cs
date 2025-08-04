namespace Core.Interfaces.Repositories;

public interface IRepositoryBase<TEntity, TId> 
    where TEntity : class 
    where TId : struct
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TId id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TId id);
}