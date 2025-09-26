namespace ParsMedeQ.Contracts.ProductContracts.ProductCategoryListContract;
public readonly record struct ProductCategoryListApiResponse(
    int Id,
    string Title,
    string Description,
    int? ParentId,
    string CreationDate,
    string Image);