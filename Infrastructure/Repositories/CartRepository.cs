using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CartRepository(AppDbContext context) : RepositoryBase<Cart, Guid>(context), ICartRepository
{
    public async Task<Cart?> GetByUserIdAsync(Guid userId)
    {
        return await _context.Carts
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<Cart?> ClearByIdAsync(Guid cartId)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart is null)
            return null;

        _context.CartItems.RemoveRange(cart.CartItems);
        await _context.SaveChangesAsync();

        return cart;
    }

    public async Task<Cart?> GetWithItemsAsync(Guid cartId)
    {
        return await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.Id == cartId);
    }

    public async Task<Cart?> GetByUserWithItemsAsync(Guid userId)
    {
        return await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<decimal> GetTotalPriceByIdAsync(Guid cartId)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart == null)
            return 0;

        return cart.CartItems.Sum(item => item.Product.Price * item.Quantity);
    }

    public async Task<Cart?> AddProductAsync(Guid cartId, int productId, int quantity)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart == null) return null;

        var existingItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            cart.CartItems.Add(new CartItem
            {
                CartId = cartId,
                ProductId = productId,
                Quantity = quantity
            });
        }

        await _context.SaveChangesAsync();
        return cart;
    }

    public async Task<Cart?> RemoveProductAsync(Guid cartId, int productId)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart == null) return null;

        var item = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);

        if (item != null)
        {
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        return cart;
    }

    public async Task<Cart?> UpdateProductQuantityInCartAsync(Guid cartId, int productId, int quantity)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart == null) return null;

        var item = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);

        if (item != null)
        {
            item.Quantity = quantity;
            await _context.SaveChangesAsync();
        }

        return cart;
    }
}