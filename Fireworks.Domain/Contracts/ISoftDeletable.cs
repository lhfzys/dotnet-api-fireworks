namespace Fireworks.Domain.Contracts;

public interface ISoftDeletable
{
    DateTimeOffset? Deleted { get; set; }
    Guid? DeletedBy { get; set; }
}