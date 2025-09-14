namespace ParsMedeQ.Contracts.ProductContracts.UpdateProductCategoryContract;
public readonly record struct UpdateProductCategoryApiRequest(
    int Id,
    string Title,
    string Description,
    int? ParentId);