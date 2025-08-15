namespace ParsMedeQ.Contracts.ResourceContracts.AddResourceCategoryContract;
public readonly record struct AddResourceCategoryApiRequest(
    int TableId,
    string Title,
    string Description,
    int? ParentId);