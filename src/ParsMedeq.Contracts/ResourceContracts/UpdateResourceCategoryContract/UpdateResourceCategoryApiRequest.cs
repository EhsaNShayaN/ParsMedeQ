namespace ParsMedeQ.Contracts.ResourceContracts.UpdateResourceCategoryContract;
public readonly record struct UpdateResourceCategoryApiRequest(
    int Id,
    string Title,
    string Description,
    int? ParentId);