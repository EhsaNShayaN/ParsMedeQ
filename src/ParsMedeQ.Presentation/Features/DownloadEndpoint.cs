using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.GeneralFeatures.DownloadFeature;
using ParsMedeQ.Contracts;

namespace ParsMedeQ.Presentation.Features;
internal sealed class DownloadEndpoint : EndpointHandlerBase<
    DownloadQuery,
    DownloadQueryResponse,
    DownloadApiResponse>
{
    protected override bool NeedTaxPayerFile => false;

    public DownloadEndpoint(IPresentationMapper<DownloadQueryResponse, DownloadApiResponse> handlerResponseMapper) :
        base(
        Endpoints.General.Download,
        HttpMethod.Get,
        handlerResponseMapper.Map,
        response => TypedResults.File(response.Value.FileContent, response.Value.MimeType, response.Value.FileName, true))
    {
    }
    protected override Delegate EndpointDelegate =>
        (int id, ISender sender, CancellationToken cancellationToken) =>
            this.CallMediatRHandler(sender,
                () => ValueTask.FromResult(
                        PrimitiveResult.Success(
                            new DownloadQuery(id))), cancellationToken);
}
sealed class DownloadApiResponseMpper : IPresentationMapper<DownloadQueryResponse, DownloadApiResponse>
{
    public ValueTask<PrimitiveResult<DownloadApiResponse>> Map(DownloadQueryResponse src, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new DownloadApiResponse(src.FileInfo.Bytes, src.FileInfo.Name, src.FileInfo.MimeType)));
    }
}
sealed record DownloadApiResponse(byte[] FileContent, string FileName, string MimeType);