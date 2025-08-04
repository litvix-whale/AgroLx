using Core.Interfaces.Repositories;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository(AppDbContext context) : RepositoryBase<Product, int>(context), IProductRepository
{
    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId) =>
        await _dbSet.Where(p => p.Category.Id == categoryId).ToListAsync();
}