namespace ParsMedeQ.Contracts.ResourceCategoryContracts.ResourceCategoryListContract;
public readonly record struct ResourceCategoryDetailsApiResponse(
    int Id,
    int TableId,
    string Title,
    string Description,
    int Count,
    int? ParentId,
    string CreationDate
);