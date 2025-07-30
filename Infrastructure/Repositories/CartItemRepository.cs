using Core.Interfaces.Repositories;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories;

public class CartItemRepository(AppDbContext context) : RepositoryBase<CartItem, int>(context), ICartItemRepository
{
    public async Task<int> GetTotalQuantityInCartAsync(Guid cartId)
    {
        return await _context.CartItems
            .Where(ci => ci.CartId == cartId)
            .SumAsync(ci => ci.Quantity);
    }
}