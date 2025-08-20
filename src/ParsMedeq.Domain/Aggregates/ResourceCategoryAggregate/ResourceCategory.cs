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
    /*public static async ValueTask<PrimitiveResult<ResourceCategory>> Create(
        string title,
        string description,
        int tableId,
        int count,
        int? parentId,
        DateTime creationDate)
    {
        var result = await ResourceCategoryTranslation.Create(title, description)
            .Map(translation =>
            {
                var resourceCategory = new ResourceCategory
                {
                    TableId = tableId,
                    Count = count,
                    ParentId = parentId,
                    CreationDate = creationDate,
                };
                return (translation, resourceCategory);
            })
            .Map(data =>
            {
                data.resourceCategory.ResourceCategoryTranslations.Append(data.translation);
                return data.resourceCategory;
            }).ConfigureAwait(false);
        return result;
    }*/

    public static async ValueTask<PrimitiveResult<ResourceCategory>> Create(
        string title,
        string description,
        int tableId,
        int count,
        int? parentId,
        DateTime creationDate)
    {
        return PrimitiveResult.Success(
            new ResourceCategory
            {
                TableId = tableId,
                Count = count,
                ParentId = parentId,
                CreationDate = creationDate,
            });
    }
    public PrimitiveResult<ResourceCategory> Update(
        string title,
        string description,
        int? parentId)
    {
        //this.Title = title;
        //this.Description = description;
        this.ParentId = parentId;

        return this;
    }
    #endregion

    public ValueTask<PrimitiveResult> AddTranslation(string langCode, string title, string description)
    {
        return ResourceCategoryTranslation.Create(langCode, title, description)
            .OnSuccess(newTranslation => this._resourceCategoryTranslations.Add(newTranslation.Value))
            .Match(
                success => PrimitiveResult.Success(),
                PrimitiveResult.Failure
            );
    }
}