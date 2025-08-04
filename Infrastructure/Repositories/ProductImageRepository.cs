using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductImageRepository(AppDbContext context) : RepositoryBase<ProductImage, int>(context), IProductImageRepository
{
    public async Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId) =>
        await _dbSet
            .Where(pi => pi.ProductId == productId)
            .OrderBy(pi => pi.UiPriority)
            .ToListAsync();

    public async Task DeleteByProductIdAsync(int productId)
    {
        var images = await _dbSet.Where(pi => pi.ProductId == productId).ToListAsync();
        if (images.Any())
        {
            _dbSet.RemoveRange(images);
            await _context.SaveChangesAsync();
        }
    }
}