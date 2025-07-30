namespace Core.Entities;

public class Cart:EntityBase<Guid>
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;

    public virtual ICollection<CartItem> CartItems { get; set; } = null!;
    
    public virtual ICollection<Product> Products { get; set; } = null!;
}

