namespace Core.Entities;

public class Order: EntityBase<Guid>
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!; 
    
    // maybe make class address and save it like a json
    public string Address { get; set; } = null!;
}