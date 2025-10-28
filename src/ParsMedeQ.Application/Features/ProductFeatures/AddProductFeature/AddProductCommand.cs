using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ProductFeatures.AddProductFeature;

public sealed record class AddProductCommand
(
    string Title,
    string Language,
    int Price,
    int Discount,
    string Description,
    string PublishInfo,
    string Publisher,
    int ProductCategoryId,
    string Abstract,
    string Anchors,
    string Keywords,
    string PublishDate,
    int WarrantyExpirationTime,
    int PeriodicServiceInterval,
    FileData? ImageInfo,
    FileData? FileInfo) : IPrimitiveResultCommand<AddProductCommandResponse>,
    IValidatableRequest<AddProductCommand>
{
    public ValueTask<PrimitiveResult<AddProductCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(string.IsNullOrWhiteSpace(value.Title))
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "عشق ارسالی نامعتبر است"))
                ]);
}