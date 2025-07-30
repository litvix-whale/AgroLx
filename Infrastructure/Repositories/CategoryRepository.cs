using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CategoryRepository(AppDbContext context) : RepositoryBase<Category, int>(context), ICategoryRepository
{
    
}