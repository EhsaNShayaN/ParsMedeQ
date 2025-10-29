namespace ParsMedeQ.Application.Features.ProductFeatures.ProductListFeature;

public sealed class ProductListDbQueryResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ProductCategoryId { get; set; }
    public string ProductCategoryTitle { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public int? Price { get; set; }
    public int? Discount { get; set; }
    public int GuarantyExpirationTime { get; set; }
    public int PeriodicServiceInterval { get; set; }
    public bool Deleted { get; set; }
    public bool Disabled { get; set; }
    public DateTime CreationDate { get; set; }
}