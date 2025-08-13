namespace ParsMedeQ.Domain.Abstractions;

public interface ISoftDeletableEntity
{
    bool IsDeleted { get; }
}
