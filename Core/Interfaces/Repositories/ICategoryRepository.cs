using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ICategoryRepository: IRepositoryBase<Category, int>
{
    Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
}