using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.MediaAggregate;

namespace ParsMedeQ.Domain.Aggregates.ProductAggregate.Entities;
public sealed class ProductMedia : EntityBase<int>
{
    #region " Properties "
    public int ProductId { get; private set; }
    public int MediaId { get; private set; }
    public int Ordinal { get; private set; }
    #endregion

    #region " Navigation Properties "
    public Product Product { get; private set; } = null!;
    public Media Media { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private ProductMedia() : base(0) { }
    public ProductMedia(int id) : base(id) { }
    #endregion

    #region " Factory "
    internal static PrimitiveResult<ProductMedia> Create(
        int productId,
        int mediaId,
        int ordinal) => PrimitiveResult.Success(
            new ProductMedia
            {
                ProductId = productId,
                MediaId = mediaId,
                Ordinal = ordinal
            });
    internal PrimitiveResult<ProductMedia> Update(
        int productId,
        int mediaId,
        int ordinal)
    {
        this.ProductId = productId;
        this.MediaId = mediaId;
        this.Ordinal = ordinal;
        return this;
    }
    #endregion
}
