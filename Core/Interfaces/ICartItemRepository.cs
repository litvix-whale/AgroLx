using Core.Entities;

namespace Core.Interfaces;

public interface ICartItemRepository :IRepository<CartItem, int>
{
    Task<int> GetTotalQuantityInCartAsync(Guid cartId);
}