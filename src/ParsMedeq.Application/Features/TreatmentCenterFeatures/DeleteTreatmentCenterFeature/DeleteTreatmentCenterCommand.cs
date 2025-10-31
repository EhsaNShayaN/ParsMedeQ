using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.TreatmentCenterFeatures.DeleteTreatmentCenterFeature;

public sealed record class DeleteTreatmentCenterCommand(int Id) :
    IPrimitiveResultCommand<DeleteTreatmentCenterCommandResponse>,
    IValidatableRequest<DeleteTreatmentCenterCommand>
{
    public ValueTask<PrimitiveResult<DeleteTreatmentCenterCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(Id > 0)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "عشق ارسالی نامعتبر است"))
                ]);
}