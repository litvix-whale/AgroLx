using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ICartRepository: IRepository<Cart, Guid>
{
    Task<Cart?> GetByUserIdAsync(Guid userId);
    Task<Cart?> ClearByIdAsync(Guid cartId);
    Task<Cart?> GetWithItemsAsync(Guid cartId);
    Task<Cart?> GetByUserWithItemsAsync(Guid userId);
    Task<decimal> GetTotalPriceByIdAsync(Guid cartId);
    Task<Cart?> AddProductAsync(Guid cartId, int productId, int quantity);
    Task<Cart?> RemoveProductAsync(Guid cartId, int productId);
    Task<Cart?> UpdateProductQuantityInCartAsync(Guid cartId, int productId, int quantity);
}