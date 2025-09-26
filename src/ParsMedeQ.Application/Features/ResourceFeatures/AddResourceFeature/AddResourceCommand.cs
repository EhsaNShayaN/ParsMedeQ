using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ResourceFeatures.AddResourceFeature;

public sealed record class AddResourceCommand
(
    int TableId,
    string Title,
    string Language,
    int Price,
    int Discount,
    string Description,
    string PublishInfo,
    string Publisher,
    int ResourceCategoryId,
    string Abstract,
    string Anchors,
    DateTime? ExpirationDate,
    string Keywords,
    string PublishDate,
    byte[] Image,
    string ImageExtension,
    byte[] File,
    string FileExtension) : IPrimitiveResultCommand<AddResourceCommandResponse>,
    IValidatableRequest<AddResourceCommand>
{
    public ValueTask<PrimitiveResult<AddResourceCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(string.IsNullOrWhiteSpace(value.Title))
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "عشق ارسالی نامعتبر است"))
                ]);
}