namespace ParsMedeQ.Application.Features.ProductFeatures.ProductCategoryListFeature;

public sealed class ProductCategoryListDbQueryResponse
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public DateTime CreationDate { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}