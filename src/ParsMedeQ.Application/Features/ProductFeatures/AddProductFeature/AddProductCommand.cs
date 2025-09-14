using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ProductFeatures.AddProductFeature;

public sealed record class AddProductCommand
(
    int TableId,
    string Title,
    string Language,
    bool IsVip,
    int Price,
    int Discount,
    string Description,
    string PublishInfo,
    string Publisher,
    int ProductCategoryId,
    string Abstract,
    string Anchors,
    DateTime? ExpirationDate,
    string Keywords,
    string PublishDate,
    byte[] Image,
    string ImageExtension,
    byte[] File,
    string FileExtension) : IPrimitiveResultCommand<AddProductCommandResponse>,
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