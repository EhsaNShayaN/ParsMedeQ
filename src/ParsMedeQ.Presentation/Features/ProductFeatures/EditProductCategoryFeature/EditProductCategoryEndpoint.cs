using ParsMedeQ.Application;
using ParsMedeQ.Application.Features.ProductFeatures.UpdateProductCategoryFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ProductContracts.UpdateProductCategoryContract;

namespace ParsMedeQ.Presentation.Features.ProductFeatures.EditProductCategoryFeature;
sealed class EditProductCategoryEndpoint : EndpointHandlerBase<
    UpdateProductCategoryApiRequest,
    UpdateProductCategoryCommand,
    UpdateProductCategoryCommandResponse,
    UpdateProductCategoryApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public EditProductCategoryEndpoint(
        IPresentationMapper<UpdateProductCategoryApiRequest, UpdateProductCategoryCommand> apiRequestMapper
        ) : base(
            Endpoints.Product.EditProductCategory,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class UpdateProductCategoryApiRequestMapper : IPresentationMapper<UpdateProductCategoryApiRequest, UpdateProductCategoryCommand>
{
    public IFileService _fileService { get; set; }

    public UpdateProductCategoryApiRequestMapper(IFileService fileService) => this._fileService = fileService;

    public async ValueTask<PrimitiveResult<UpdateProductCategoryCommand>> Map(UpdateProductCategoryApiRequest src, CancellationToken cancellationToken)
    {
        var imageInfo = await _fileService.ReadStream(src.Image).ConfigureAwait(false);
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new UpdateProductCategoryCommand(
                    src.Id,
                    src.Title,
                    src.Description,
                    src.ParentId,
                    imageInfo,
                    src.OldImagePath)));
    }
}