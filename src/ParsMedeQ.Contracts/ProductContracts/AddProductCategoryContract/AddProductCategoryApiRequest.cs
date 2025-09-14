namespace ParsMedeQ.Contracts.ProductContracts.AddProductCategoryContract;
public readonly record struct AddProductCategoryApiRequest(
    int TableId,
    string Title,
    string Description,
    int? ParentId);