namespace Core.Interfaces;

public interface IEntityBase
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
