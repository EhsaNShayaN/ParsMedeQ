using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ProductFeatures.UpdateProductFeature;

public sealed record class UpdateProductCommand(
    int Id,
    int TableId,
    string Title,
    string Description,
    string Abstract,
    string Anchors,
    string Keywords,

    int ProductCategoryId,
    string Language,
    string PublishDate,
    string PublishInfo,
    string Publisher,
    int Price,
    int Discount,
    DateTime? ExpirationDate,
    byte[] Image,
    string ImageExtension,
    byte[] File,
    string FileExtension,
    string OldImagePath,
    int? OldFileId) : IPrimitiveResultCommand<UpdateProductCommandResponse>,
    IValidatableRequest<UpdateProductCommand>
{
    public ValueTask<PrimitiveResult<UpdateProductCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Language)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}