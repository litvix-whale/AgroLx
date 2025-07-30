using Core.Entities;
using Core.DTOs;
namespace Core.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> SearchProductAsync(ProductSearchRequest request);
}