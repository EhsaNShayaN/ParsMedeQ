namespace ParsMedeQ.Application.Features.ProductFeatures.ProductCategoryDetailsFeature;

public sealed class ProductCategoryDetailsDbQueryResponse
{
    public int Id { get; set; }
    public int TableId { get; private set; }
    public int Count { get; private set; }
    public int? ParentId { get; private set; }
    public DateTime CreationDate { get; private set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}