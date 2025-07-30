using Core.Interfaces;

namespace Core.Entities;

public class EntityBase<TId>: IEntityBase
{
    // TId usually int or GUID
    public TId Id { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
