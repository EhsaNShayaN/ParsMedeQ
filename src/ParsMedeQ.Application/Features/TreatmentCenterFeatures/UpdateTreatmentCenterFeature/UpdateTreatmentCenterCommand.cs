using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.TreatmentCenterFeatures.UpdateTreatmentCenterFeature;

public sealed record class UpdateTreatmentCenterCommand(
    int Id,
    int ProvinceId,
    int CityId,
    string Title,
    string Description,
    FileData ImageInfo,
    string OldImagePath) : IPrimitiveResultCommand<UpdateTreatmentCenterCommandResponse>,
    IValidatableRequest<UpdateTreatmentCenterCommand>
{
    public ValueTask<PrimitiveResult<UpdateTreatmentCenterCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Title)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}