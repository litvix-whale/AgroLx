namespace Core.Interfaces;

public interface IEntityBase
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
}
