using ParsMedeq.Domain.Aggregates.ProductAggregate;

namespace ParsMedeq.Application.Features.ProductFeatures;
public sealed record CreateProductCommand(int ProductTypeId, int ModelId, string Slug, string Title, string Specifications) : IPrimitiveResultCommand<Product>;

internal sealed class CreateProductCommandHanler : IPrimitiveResultCommandHandler<CreateProductCommand, Product>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public CreateProductCommandHanler(IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        return null;
        /*return await Product.Create(
            request.ProductTypeId,
            request.ModelId,
            request.Slug,
            request.Title,
            request.Specifications)
            .Map(newProduct => this._writeUnitOfWork.ProductWriteRepository.AddNewProduct(newProduct).Map(_ => newProduct))
            .Map(newProduct => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => newProduct))
            .ConfigureAwait(false);*/
    }
}
