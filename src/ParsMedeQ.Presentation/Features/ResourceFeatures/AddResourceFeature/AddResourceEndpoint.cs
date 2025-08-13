using ParsMedeQ.Application.Features.ResourceFeatures.AddResourceFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ResourceContracts.AddResourceContracts;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.ResourceFeatures.AddResourceFeature;
sealed class AddResourceEndpoint : EndpointHandlerBase<
    AddResourceApiRequest,
    AddResourceCommand,
    AddResourceCommandResponse,
    AddResourceApiResponse>
{
    protected override bool NeedTaxPayerAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public AddResourceEndpoint(
        IPresentationMapper<AddResourceApiRequest, AddResourceCommand> apiRequestMapper
        ) : base(
            Endpoints.Resource.AddResource,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class AddResourceApiRequestMapper : IPresentationMapper<AddResourceApiRequest, AddResourceCommand>
{
    public ValueTask<PrimitiveResult<AddResourceCommand>> Map(AddResourceApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new AddResourceCommand(
                    src.Id,
                    src.TableId,
                    src.Title,
                    src.Image,
                    src.MimeType,
                    src.Language,
                    src.IsVip,
                    src.Price,
                    src.Discount,
                    src.Description,
                    src.PublishInfo,
                    src.Publisher,
                    src.ResourceCategoryId,
                    src.ResourceCategoryTitle,
                    src.Abstract,
                    src.Anchors.Length > 0 ? Newtonsoft.Json.JsonConvert.SerializeObject(src.Anchors) : string.Empty,
                    string.IsNullOrEmpty(src.ExpirationDate) ? default(DateTime?) : src.ExpirationDate.ToGeorgianDate(),
                    src.ExpirationTime,
                    src.Keywords,
                    src.PublishDate,
                    src.Categories,
                    src.Doc)));
}