using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class User : IdentityUser<Guid>
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}