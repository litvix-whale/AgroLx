using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ICartItemRepository :IRepository<CartItem, int>
{
    Task<int> GetTotalQuantityInCartAsync(Guid cartId);
}