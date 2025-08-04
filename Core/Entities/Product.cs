namespace Core.Entities;

public class Product: EntityBase<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
    
    // many images
    public virtual ICollection<ProductImage> Images { get; set; }
    
    // one category, but this category may have a parent category...
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    
    // user can create a product
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    
    public decimal Price { get; set; }
    
    public Guid CartId { get; set; }
    public virtual Cart Cart { get; set; }
}

