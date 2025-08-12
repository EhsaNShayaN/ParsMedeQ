namespace ParsMedeq.Domain.Abstractions;

public interface ISoftDeletableEntity
{
    bool IsDeleted { get; }
}
