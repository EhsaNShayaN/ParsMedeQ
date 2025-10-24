using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.TicketFeatures.AddTicketFeature;

public sealed record class AddTicketCommand(
    string Title,
    string Description,
    FileData? ImageInfo) : IPrimitiveResultCommand<AddTicketCommandResponse>,
    IValidatableRequest<AddTicketCommand>
{
    public ValueTask<PrimitiveResult<AddTicketCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Title)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}