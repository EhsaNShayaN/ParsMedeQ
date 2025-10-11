using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.CommentFeatures.AddCommentFeature;

public sealed record class AddCommentCommand(
    int TableId,
    string TableName,
    int RelatedId,
    string Icon,
    string Description) : IPrimitiveResultCommand<AddCommentCommandResponse>,
    IValidatableRequest<AddCommentCommand>
{
    public ValueTask<PrimitiveResult<AddCommentCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.TableId)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}