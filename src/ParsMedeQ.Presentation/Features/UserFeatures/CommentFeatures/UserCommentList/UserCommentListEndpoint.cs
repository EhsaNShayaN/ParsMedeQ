using ParsMedeQ.Application.Features.CommentFeatures.CommentListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.CommentContracts.CommentListContract;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.UserFeatures.CommentFeatures.UserCommentList;

sealed class UserCommentListEndpoint : EndpointHandlerBase<
    UserCommentListApiRequest,
    CommentListQuery,
    BasePaginatedApiResponse<CommentListDbQueryResponse>,
    BasePaginatedApiResponse<CommentListApiResponse>>
{
    protected override bool NeedTaxPayerFile => true;
    protected override bool NeedAuthentication => true;

    public UserCommentListEndpoint(
        IPresentationMapper<UserCommentListApiRequest, CommentListQuery> requestMapper,
        IPresentationMapper<BasePaginatedApiResponse<CommentListDbQueryResponse>, BasePaginatedApiResponse<CommentListApiResponse>> responseMapper)
        : base(
            Endpoints.User.Comments,
            HttpMethod.Post,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
sealed class UserCommentListApiRequestMapper : IPresentationMapper<
    UserCommentListApiRequest,
    CommentListQuery>
{
    public ValueTask<PrimitiveResult<CommentListQuery>> Map(
        UserCommentListApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new CommentListQuery(src.RelatedId, false)
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