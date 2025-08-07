using EShop.Domain.Aggregates.ProductAggregate;

namespace EShop.Application.Features.EShopFeatures.ProductFeatures;
public sealed record CreateProductCommand(int ProductTypeId, int ModelId, string Slug, string Title, string Specifications) : IPrimitiveResultCommand<Product>;

internal sealed class CreateProductCommandHanler : IPrimitiveResultCommandHandler<CreateProductCommand, Product>
{
    private readonly IEShopWriteUnitOfWork _eShopWriteUnitOfWork;

    public CreateProductCommandHanler(IEShopWriteUnitOfWork eShopWriteUnitOfWork)
    {
        this._eShopWriteUnitOfWork = eShopWriteUnitOfWork;
    }

    public async Task<PrimitiveResult<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        return await Product.Create(
            request.ProductTypeId,
            request.ModelId,
            request.Slug,
            request.Title,
            request.Specifications)
            .Map(newProduct => this._eShopWriteUnitOfWork.ProductWriteRepository.AddNewProduct(newProduct).Map(_ => newProduct))
            .Map(newProduct => this._eShopWriteUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => newProduct))
            .ConfigureAwait(false);
    }
}
