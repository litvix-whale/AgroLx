using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ICartItemRepository :IRepositoryBase<CartItem, int>
{
    Task<int> GetTotalQuantityInCartAsync(Guid cartId);
}