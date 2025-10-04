namespace ParsMedeQ.Application.Features.ProductFeatures.ProductMediaListFeature;

public sealed class ProductMediaListDbQueryResponse
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int MediaId { get; set; }
    public int Ordinal { get; set; }
    public string Path { get; set; } = string.Empty;
}