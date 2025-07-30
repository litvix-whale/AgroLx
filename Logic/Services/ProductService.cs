using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Logic.Services;

public class ProductService(IProductRepository productRepository, IProductImageService imageService, IProductImageRepository productImageRepository) : IProductService
{
    
}