namespace Core.Entities;

public class CartItem: EntityBase<int>
{
    public Guid CartId { get; set; }
    public virtual Cart Cart { get; set; } = null!;
    
    public int ProductId { get; set; }
    public virtual Product Product { get; set; } = null!;
    
    public int Quantity { get; set; }
    
    // additionaly (color, garanty, etc.)
}