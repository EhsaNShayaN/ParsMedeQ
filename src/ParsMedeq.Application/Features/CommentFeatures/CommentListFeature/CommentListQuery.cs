using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.CommentFeatures.CommentListFeature;
public sealed record CommentListQuery(int? RelatedId, bool? IsAdmin) : BasePaginatedQuery, IPrimitiveResultQuery<BasePaginatedApiResponse<CommentListDbQueryResponse>>;

sealed class CommentListQueryHandler : IPrimitiveResultQueryHandler<CommentListQuery, BasePaginatedApiResponse<CommentListDbQueryResponse>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IUserContextAccessor _userContextAccessor;

    public CommentListQueryHandler(
        IReadUnitOfWork taxMemoryReadUnitOfWork,
        IUserContextAccessor userContextAccessor)
    {
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
        this._userContextAccessor = userContextAccessor;
    }
    public async Task<PrimitiveResult<BasePaginatedApiResponse<CommentListDbQueryResponse>>> Handle(CommentListQuery request, CancellationToken cancellationToken)
    {
        int? userId = !request.IsAdmin.HasValue || request.IsAdmin.Value ? null : this._userContextAccessor.Current.UserId;
        return await this._readUnitOfWork.CommentReadRepository.FilterComments(
            request,
            userId,
            request.RelatedId,
            request.LastId,
            cancellationToken)
        .ConfigureAwait(false);
    }
}