using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class User : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? ProfilePicture { get; set; }
    
    public virtual  Cart? Cart { get; set; }
    
    public virtual ICollection<Product>? Products { get; set; }
    
    public virtual ICollection<Order>? Orders { get; set; }
}