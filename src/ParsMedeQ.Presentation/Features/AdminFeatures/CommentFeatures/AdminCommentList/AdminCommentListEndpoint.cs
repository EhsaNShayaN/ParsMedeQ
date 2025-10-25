using ParsMedeQ.Application.Features.CommentFeatures.CommentListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.CommentContracts.CommentListContract;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.AdminFeatures.CommentFeatures.AdminCommentList;

sealed class AdminCommentListEndpoint : EndpointHandlerBase<
    CommentListApiRequest,
    CommentListQuery,
    BasePaginatedApiResponse<CommentListDbQueryResponse>,
    BasePaginatedApiResponse<CommentListApiResponse>>
{
    protected override bool NeedTaxPayerFile => false;
    protected override bool NeedAdminPrivilage => true;
    protected override bool NeedAuthentication => true;

    public AdminCommentListEndpoint(
        IPresentationMapper<CommentListApiRequest, CommentListQuery> requestMapper,
        IPresentationMapper<BasePaginatedApiResponse<CommentListDbQueryResponse>, BasePaginatedApiResponse<CommentListApiResponse>> responseMapper)
        : base(
            Endpoints.Admin.Comments,
            HttpMethod.Post,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
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
                new CommentListQuery(src.RelatedId, true)
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