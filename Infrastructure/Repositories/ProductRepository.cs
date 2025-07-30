using Core.Interfaces.Repositories;
using Core.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ProductRepository(AppDbContext context) : RepositoryBase<Product, int>(context), IProductRepository
{
    
}