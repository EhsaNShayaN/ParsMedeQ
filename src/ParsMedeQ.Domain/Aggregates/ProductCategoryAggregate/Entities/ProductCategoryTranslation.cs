using ParsMedeQ.Domain.Abstractions;

namespace ParsMedeQ.Domain.Aggregates.ProductCategoryAggregate.Entities;
public sealed class ProductCategoryTranslation : EntityBase<int>
{
    #region " Properties "
    public string LanguageCode { get; private set; } = null!;
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int ProductCategoryId { get; private set; }
    public string Image { get; private set; } = string.Empty;
    #endregion

    #region " Navigation Properties "
    public ProductCategory ProductCategory { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private ProductCategoryTranslation() : base(0) { }
    public ProductCategoryTranslation(int id) : base(id) { }
    #endregion

    #region " Factory "
    internal static PrimitiveResult<ProductCategoryTranslation> Create(
        string languageCode,
        string title,
        string description,
        string image) => PrimitiveResult.Success(
            new ProductCategoryTranslation
            {
                LanguageCode = languageCode,
                Title = title,
                Description = description,
                Image = image
            });
    internal PrimitiveResult<ProductCategoryTranslation> Update(
        string title,
        string description,
        string image)
    {
        this.Title = title;
        this.Description = description;
        this.Image = image;
        return this;
    }
    #endregion
}
