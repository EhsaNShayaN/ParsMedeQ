namespace ParsMedeQ.Application.Features.ResourceFeatures.ResourceCategoryListFeature;

public sealed class ResourceCategoryListDbQueryResponse
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public int Count { get; set; }
    public int? ParentId { get; set; }
    public DateTime CreationDate { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}