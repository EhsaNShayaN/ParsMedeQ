namespace ParsMedeQ.Application.Features.ProductFeatures.ProductDetailsFeature;

public sealed class ProductDetailsDbQueryResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ProductCategoryId { get; set; }
    public string ProductCategoryTitle { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public int? FileId { get; set; }
    public int? Price { get; set; }
    public int? Discount { get; set; }
    public int GuarantyExpirationTime { get; set; }
    public int PeriodicServiceInterval { get; set; }
    public bool Deleted { get; set; }
    public bool Disabled { get; set; }
    public DateTime CreationDate { get; set; }
    public bool Registered { get; set; }
    public ProductMediaDbQueryResponse[] Images { get; set; }
}
public sealed class ProductMediaDbQueryResponse
{
    public int Id { get; set; }
    public string Path { get; set; } = string.Empty;
    public int Ordinal { get; set; }
}

public sealed record ProductCategoryDbQueryResponse(int Id, string Title);