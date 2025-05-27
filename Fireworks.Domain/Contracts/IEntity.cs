using System.Collections.ObjectModel;
using Fireworks.Domain.Events;

namespace Fireworks.Domain.Contracts;

public interface IEntity
{
    Collection<DomainEvent> DomainEvents { get; }
}
public interface IEntity<out TId> : IEntity
{
    TId Id { get; }
}