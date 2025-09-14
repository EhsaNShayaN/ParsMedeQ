namespace ParsMedeQ.Domain.Aggregates.ProductAggregate;
public sealed class Product
{
    public int Id { get; set; }
    private List<ProductTranslation> _productTranslations = [];
    public IReadOnlyCollection<ProductTranslation> ProductTranslations => this._productTranslations.AsReadOnly();
}
public sealed class ProductTranslation
{
}
