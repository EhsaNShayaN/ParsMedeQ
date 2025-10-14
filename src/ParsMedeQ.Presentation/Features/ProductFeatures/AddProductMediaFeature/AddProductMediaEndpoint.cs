using ParsMedeQ.Application;
using ParsMedeQ.Application.Features.ProductFeatures.AddProductMediaFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ProductContracts.AddProductMediaContract;

namespace ParsMedeQ.Presentation.Features.ProductFeatures.AddProductMediaFeature;
sealed class AddProductMediaEndpoint : EndpointHandlerBase<
    AddProductMediaApiRequest,
    AddProductMediaCommand,
    AddProductMediaCommandResponse,
    AddProductMediaApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public AddProductMediaEndpoint(
        IPresentationMapper<AddProductMediaApiRequest, AddProductMediaCommand> apiRequestMapper
        ) : base(
            Endpoints.Product.AddProductMedia,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class AddProductMediaApiRequestMapper : IPresentationMapper<AddProductMediaApiRequest, AddProductMediaCommand>
{
    public IFileService _fileService { get; set; }

    public AddProductMediaApiRequestMapper(IFileService fileService) => this._fileService = fileService;

    public async ValueTask<PrimitiveResult<AddProductMediaCommand>> Map(AddProductMediaApiRequest src, CancellationToken cancellationToken)
    {


        List<FileData> files = [];
        foreach (var file in src.Files)
        {
            var fileInfo = await _fileService.ReadStream(file).ConfigureAwait(false);
            files.Add(fileInfo.Value);
        }

        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new AddProductMediaCommand(
                    src.ProductId,
                    files.ToArray())));
    }
}