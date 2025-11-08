using ParsMedeQ.Domain.Aggregates.ServiceAggregate;

namespace ParsMedeQ.Application.Features.ServiceFeatures.CreateServiceFeature;

public sealed class CreateServiceCommandHandler : IPrimitiveResultCommandHandler<CreateServiceCommand, CreateServiceCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IFileService _fileService;

    public CreateServiceCommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IFileService fileService)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._fileService = fileService;
    }

    public async Task<PrimitiveResult<CreateServiceCommandResponse>> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        return await Service.Create(request.TypeId)
            .Map(item => UploadFile(this._fileService, request.ImageInfo?.Bytes, request.ImageInfo?.Extension, "Images", cancellationToken)
                .Map(imagePath => (item, imagePath)))
            .Map(data => data.item.AddTranslation(Constants.LangCode_Farsi.ToLower(), request.Title, request.Description, data.imagePath)
                .Map(() => data.item))
            .Map(Service => this._writeUnitOfWork.ServiceWriteRepository.AddService(Service)
            .Map(Service => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => Service))
            .Map(Service => new CreateServiceCommandResponse(Service is not null)))
            .ConfigureAwait(false);
    }

    static async ValueTask<PrimitiveResult<string>> UploadFile(
        IFileService fileService,
        byte[] bytes,
        string fileExtension,
        string fodlerName,
        CancellationToken cancellationToken)
    {
        if ((bytes?.Length ?? 0) == 0) return string.Empty;
        var result = await fileService.UploadFile(bytes, fileExtension, fodlerName, cancellationToken).ConfigureAwait(false);
        if (string.IsNullOrWhiteSpace(result)) return PrimitiveResult.Failure<string>("", "Can not upload file");

        return result;
    }
}
