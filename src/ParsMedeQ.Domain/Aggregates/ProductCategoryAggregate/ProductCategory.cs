using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.ProductAggregate;
using ParsMedeQ.Domain.Aggregates.ProductCategoryAggregate.Entities;

namespace ParsMedeQ.Domain.Aggregates.ProductCategoryAggregate;
public sealed class ProductCategory : EntityBase<int>
{
    #region " Fields "
    private List<Product> _Products = [];
    private List<ProductCategory> _ProductCategories = [];
    private List<ProductCategoryTranslation> _ProductCategoryTranslations = [];
    #endregion

    #region " Properties "
    public int? ParentId { get; private set; }
    public DateTime CreationDate { get; private set; }
    #endregion

    #region " Navigation Properties "
    public ProductCategory? Parent { get; private set; }
    public IReadOnlyCollection<Product> Products => this._Products.AsReadOnly();
    public IReadOnlyCollection<ProductCategory> Children => this._ProductCategories.AsReadOnly();
    public IReadOnlyCollection<ProductCategoryTranslation> ProductCategoryTranslations => this._ProductCategoryTranslations.AsReadOnly();
    #endregion

    #region " Constructors "
    private ProductCategory() : base(0) { }
    public ProductCategory(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<ProductCategory> Create(
        int? parentId,
        DateTime creationDate) => PrimitiveResult.Success(
            new ProductCategory
            {
                ParentId = parentId,
                CreationDate = creationDate,
            });

    public ValueTask<PrimitiveResult<ProductCategory>> Update(
        int? parentId)
    {
        this.ParentId = parentId;
        return ValueTask.FromResult(PrimitiveResult.Success(this));
    }

    public ValueTask<PrimitiveResult<ProductCategory>> Update(
        int? parentId,
        string langCode,
        string title,
        string description,
        string image)
    {
        return this.Update(parentId)
             .Map(_ => this.UpdateTranslation(langCode, title, description, image).Map(() => this));
    }
    #endregion

    public ValueTask<PrimitiveResult> AddTranslation(
        string langCode,
        string title,
        string description,
        string image)
    {
        return ProductCategoryTranslation.Create(langCode, title, description, image)
            .OnSuccess(newTranslation => this._ProductCategoryTranslations.Add(newTranslation.Value))
            .Match(
                success => PrimitiveResult.Success(),
                PrimitiveResult.Failure
            );
    }

    public ValueTask<PrimitiveResult> UpdateTranslation(
        string langCode,
        string title,
        string description,
        string image)
    {
        var currentTranslation = _ProductCategoryTranslations.FirstOrDefault(s => s.LanguageCode.Equals(langCode, StringComparison.OrdinalIgnoreCase));
        if (currentTranslation is null)
        {
            return this.AddTranslation(langCode, title, description, image);
        }
        return currentTranslation.Update(title, description, image)
            .Match(
                _ => PrimitiveResult.Success(),
                PrimitiveResult.Failure
            );
    }
}