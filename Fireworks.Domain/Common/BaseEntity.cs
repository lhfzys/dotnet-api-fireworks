using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Fireworks.Domain.Contracts;
using Fireworks.Domain.Events;

namespace Fireworks.Domain.Common;

public abstract class BaseEntity<TId>:IEntity<TId>
{
    public TId Id { get; protected init; } = default!;
    
    [NotMapped]
    public Collection<DomainEvent> DomainEvents { get; } = new();

    public void QueueDomainEvent(DomainEvent @event)
    {
        if (!DomainEvents.Contains(@event))
            DomainEvents.Add(@event);
    }
}
public abstract class BaseEntity : BaseEntity<Guid>
{
    protected BaseEntity() => Id = Guid.NewGuid();
}