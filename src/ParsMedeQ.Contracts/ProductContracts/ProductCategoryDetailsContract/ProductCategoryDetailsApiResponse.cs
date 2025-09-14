namespace ParsMedeQ.Contracts.ProductContracts.ProductCategoryDetailsContract;
public readonly record struct ProductCategoryDetailsApiResponse(
    int Id,
    string Title,
    string Description,
    int? ParentId,
    string CreationDate
);