using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class User : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; set; }
    public string ProfilePicture { get; set; } = "pfp_1.png";
    public virtual ICollection<Product> RealEstates { get; set; } = new List<Product>();
}