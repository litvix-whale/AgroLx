using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ProductImageRepository(AppDbContext context) : RepositoryBase<ProductImage, int>(context), IProductImageRepository
{
    
}