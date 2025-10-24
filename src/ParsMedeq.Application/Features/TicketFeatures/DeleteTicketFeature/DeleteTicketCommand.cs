using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.TicketFeatures.DeleteTicketFeature;

public sealed record class DeleteTicketCommand(int Id) :
    IPrimitiveResultCommand<DeleteTicketCommandResponse>,
    IValidatableRequest<DeleteTicketCommand>
{
    public ValueTask<PrimitiveResult<DeleteTicketCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(Id > 0)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "عشق ارسالی نامعتبر است"))
                ]);
}