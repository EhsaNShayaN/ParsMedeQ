using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.CommentFeatures.CommentListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.CommentContracts.CommentListContract;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.CommentFeatures.CommentList;

sealed class CommentListEndpoint : EndpointHandlerBase<
    CommentListApiRequest,
    CommentListQuery,
    BasePaginatedApiResponse<CommentListDbQueryResponse>,
    BasePaginatedApiResponse<CommentListApiResponse>>
{
    protected override bool NeedTaxPayerFile => true;

    public CommentListEndpoint(
        IPresentationMapper<CommentListApiRequest, CommentListQuery> requestMapper,
        IPresentationMapper<BasePaginatedApiResponse<CommentListDbQueryResponse>, BasePaginatedApiResponse<CommentListApiResponse>> responseMapper)
        : base(
            Endpoints.Comment.Comments,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] CommentListApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(
                PrimitiveResult.Success(
                    new CommentListQuery(request.RelatedId)
                    {
                        PageIndex = request.PageIndex,
                        PageSize = request.PageSize,
                    })),
            cancellationToken);

}
sealed class CommentListApiRequestMapper : IPresentationMapper<
    CommentListApiRequest,
    CommentListQuery>
{
    public ValueTask<PrimitiveResult<CommentListQuery>> Map(
        CommentListApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new CommentListQuery(src.RelatedId)
                {
                    PageIndex = src.PageIndex,
                    PageSize = src.PageSize,
                    LastId = src.LastId,
                }));
    }
}
sealed class CommentListApiResponseMapper : IPresentationMapper<
    BasePaginatedApiResponse<CommentListDbQueryResponse>,
    BasePaginatedApiResponse<CommentListApiResponse>>
{
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<CommentListApiResponse>>> Map(
        BasePaginatedApiResponse<CommentListDbQueryResponse> src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    new BasePaginatedApiResponse<CommentListApiResponse>(src.Items.Select(data =>
                    new CommentListApiResponse(
                        data.Id,
                        data.Name,
                        data.Icon,
                        data.Description,
                        data.RelatedId,
                        data.TableId,
                        data.TableName,
                        data.Data,
                        data.Answers,
                        data.IsConfirmed,
                        data.CreationDate.ToPersianDate()))
                    .ToArray(), src.TotalCount, src.PageIndex, src.PageSize)
                    {
                        LastId = src.LastId
                    }));
    }
}