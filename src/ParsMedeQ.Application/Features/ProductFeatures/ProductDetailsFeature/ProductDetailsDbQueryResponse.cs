namespace ParsMedeQ.Application.Features.ProductFeatures.ProductDetailsFeature;

public sealed class ProductDetailsDbQueryResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Abstract { get; set; } = string.Empty;
    public string Anchors { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public int ProductCategoryId { get; set; }
    public string ProductCategoryTitle { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public int? FileId { get; set; }
    public string Language { get; set; } = string.Empty;
    public string PublishDate { get; set; } = string.Empty;
    public string PublishInfo { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public int? Price { get; set; }
    public int? Discount { get; set; }
    public bool IsVip { get; set; }
    public int DownloadCount { get; set; }
    public int? Ordinal { get; set; }
    public bool Deleted { get; set; }
    public bool Disabled { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public DateTime CreationDate { get; set; }
    public bool Registered { get; set; }
    public ProductCategoryDbQueryResponse[] ProductCategories { get; set; } = [];
}
public sealed record ProductCategoryDbQueryResponse(int Id, string Title);