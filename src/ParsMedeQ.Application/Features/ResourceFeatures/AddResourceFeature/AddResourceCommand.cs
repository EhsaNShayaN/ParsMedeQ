using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ResourceFeatures.AddResourceFeature;

public sealed record class AddResourceCommand
(
    int? Id,
    int TableId,
    string Title,
    string Image,
    string MimeType,
    string Language,
    bool IsVip,
    int Price,
    int Discount,
    string Description,
    string PublishInfo,
    string Publisher,
    int ResourceCategoryId,
    string ResourceCategoryTitle,
    string Abstract,
    string Anchors,
    DateTime? ExpirationDate,
    string ExpirationTime,
    string Keywords,
    string PublishDate,
    int[] Categories,
    string Doc) : IPrimitiveResultCommand<AddResourceCommandResponse>,
    IValidatableRequest<AddResourceCommand>
{
    public ValueTask<PrimitiveResult<AddResourceCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => MobileType.Create(value.Doc)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}