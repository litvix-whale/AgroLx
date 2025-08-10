namespace Core.Entities;
public class ProductImage:EntityBase<int>
{
    // save url in bd and use cloud for images (maybe better saving decoded images in db)
    public string Url { get; set; } = null!;
    
    // many images for one product
    public int ProductId { get; set; }
    public virtual Product Product { get; set; } = null!;
}
