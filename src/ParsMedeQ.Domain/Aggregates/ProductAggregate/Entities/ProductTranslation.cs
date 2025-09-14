using ParsMedeQ.Domain.Abstractions;

namespace ParsMedeQ.Domain.Aggregates.ProductAggregate.Entities;
public sealed class ProductTranslation : EntityBase<int>
{
    #region " Properties "
    public int ProductId { get; private set; }
    public string LanguageCode { get; private set; } = null!;
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = string.Empty;
    public string Abstract { get; private set; } = string.Empty;
    public string Anchors { get; private set; } = string.Empty;
    public string Keywords { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;
    public int? FileId { get; private set; }
    #endregion

    #region " Navigation Properties "
    public Product Product { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private ProductTranslation() : base(0) { }
    public ProductTranslation(int id) : base(id) { }
    #endregion

    #region " Factory "
    internal static PrimitiveResult<ProductTranslation> Create(
        string languageCode,
        string title,
        string description,
        string @abstract,
        string anchors,
        string keywords,
        string imagePath,
        int? fileId) => PrimitiveResult.Success(
            new ProductTranslation
            {
                LanguageCode = languageCode,
                Title = title,
                Description = description,
                Abstract = @abstract,
                Anchors = anchors,
                Keywords = keywords,
                Image = imagePath,
                FileId = fileId
            });
    internal PrimitiveResult<ProductTranslation> Update(
        string title,
        string description,
        string @abstract,
        string anchors,
        string keywords,
        string imagePath,
        int? fileId)
    {
        this.Title = title;
        this.Description = description;
        this.Abstract = @abstract;
        this.Anchors = anchors;
        this.Keywords = keywords;
        this.Image = imagePath;
        this.FileId = fileId;
        return this;
    }
    #endregion
}
