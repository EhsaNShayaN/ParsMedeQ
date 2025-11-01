using ParsMedeQ.Domain;
using ParsMedeQ.Domain.Aggregates.TreatmentCenterAggregate;

namespace ParsMedeQ.Application.Features.TreatmentCenterFeatures.AddTreatmentCenterFeature;
public sealed class AddTreatmentCenterCommandHandler : IPrimitiveResultCommandHandler<AddTreatmentCenterCommand, AddTreatmentCenterCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IFileService _fileService;

    public AddTreatmentCenterCommandHandler(
        IWriteUnitOfWork writeUnitOfWork, IFileService fileService)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._fileService = fileService;
    }

    public async Task<PrimitiveResult<AddTreatmentCenterCommandResponse>> Handle(AddTreatmentCenterCommand request, CancellationToken cancellationToken)
    {
        return await TreatmentCenter.Create(request.LocationId)
            .Map(resource => UploadFile(this._fileService, request.ImageInfo.Bytes, request.ImageInfo.Extension, "Images", cancellationToken)
                .Map(imagePath => (resource, imagePath)))
            .Map(data => data.resource.AddTranslation(Constants.LangCode_Farsi.ToLower(), request.Title, request.Description, data.imagePath)
                .Map(() => data.resource))
            .Map(treatmentCenter => this._writeUnitOfWork.TreatmentCenterWriteRepository.AddTreatmentCenter(treatmentCenter)
            .Map(treatmentCenter => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => treatmentCenter))
            .Map(treatmentCenter => new AddTreatmentCenterCommandResponse(treatmentCenter is not null)))
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
