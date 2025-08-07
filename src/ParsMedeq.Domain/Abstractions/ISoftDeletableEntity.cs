namespace EShop.Domain.Abstractions;

public interface ISoftDeletableEntity
{
    bool IsDeleted { get; }
}
