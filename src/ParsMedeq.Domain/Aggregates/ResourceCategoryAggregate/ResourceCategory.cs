using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.ResourceAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate.Entities;

namespace ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;
public sealed class ResourceCategory : EntityBase<int>
{
    #region " Fields "
    private List<Resource> _resources = [];
    private List<ResourceCategory> _resourceCategories = [];
    private List<ResourceCategoryRelations> _resourceCategoryRelations = [];
    private List<ResourceCategoryTranslation> _resourceCategoryTranslations = [];
    #endregion

    #region " Properties "
    public int TableId { get; private set; }
    public int Count { get; private set; }
    public int? ParentId { get; private set; }
    public DateTime CreationDate { get; private set; }
    #endregion

    #region " Navigation Properties "
    public ResourceCategory? Parent { get; private set; }
    public IReadOnlyCollection<Resource> Resources => this._resources.AsReadOnly();
    public IReadOnlyCollection<ResourceCategoryRelations> ResourceCategoryRelations => this._resourceCategoryRelations.AsReadOnly();
    public IReadOnlyCollection<ResourceCategory> Children => this._resourceCategories.AsReadOnly();
    public IReadOnlyCollection<ResourceCategoryTranslation> ResourceCategoryTranslations => this._resourceCategoryTranslations.AsReadOnly();
    #endregion

    #region " Constructors "
    private ResourceCategory() : base(0) { }
    public ResourceCategory(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<ResourceCategory> Create(
        string title,
        string description,
        int tableId,
        int count,
        int? parentId,
        DateTime creationDate) => PrimitiveResult.Success(
            new ResourceCategory
            {
                TableId = tableId,
                Count = count,
                ParentId = parentId,
                CreationDate = creationDate,
            });

    public ValueTask<PrimitiveResult<ResourceCategory>> Update(
        int? parentId)
    {
        this.ParentId = parentId;
        return ValueTask.FromResult(PrimitiveResult.Success(this));
    }

    public ValueTask<PrimitiveResult<ResourceCategory>> Update(
        int? parentId,
        string langCode,
        string title,
        string description)
    {
        return this.Update(parentId)
             .Map(_ => this.UpdateTranslation(langCode, title, description).Map(() => this));
    }
    #endregion

    public ValueTask<PrimitiveResult> AddTranslation(
        string langCode,
        string title,
        string description)
    {
        return ResourceCategoryTranslation.Create(langCode, title, description)
            .OnSuccess(newTranslation => this._resourceCategoryTranslations.Add(newTranslation.Value))
            .Match(
                success => PrimitiveResult.Success(),
                PrimitiveResult.Failure
            );
    }

    public ValueTask<PrimitiveResult> UpdateTranslation(
        string langCode,
        string title,
        string description)
    {
        var currentTranslation = _resourceCategoryTranslations.FirstOrDefault(s => s.LanguageCode.Equals(langCode, StringComparison.OrdinalIgnoreCase));
        if (currentTranslation is null)
        {
            return this.AddTranslation(langCode, title, description);
        }
        return currentTranslation.Update(title, description)
            .Match(
                _ => PrimitiveResult.Success(),
                PrimitiveResult.Failure
            );
    }
}