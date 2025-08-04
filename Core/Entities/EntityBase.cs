using Core.Interfaces;

namespace Core.Entities;

public class EntityBase<TId>: IEntityBase
{
    // TId always int or GUID
    public TId Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
