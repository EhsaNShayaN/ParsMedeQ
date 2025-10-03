namespace ParsMedeQ.Application.Features.ResourceFeatures.ResourceListFeature;

public sealed class ResourceListDbQueryResponse
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ResourceCategoryId { get; set; }
    public string ResourceCategoryTitle { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public int? Price { get; set; }
    public int? Discount { get; set; }
    public int DownloadCount { get; set; }
    public int Stock { get; set; }
    public bool Deleted { get; set; }
    public bool Disabled { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public DateTime CreationDate { get; set; }
}