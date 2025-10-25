using ParsMedeQ.Application.Helpers;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.CommentFeatures.CommentListFeature;
public sealed record CommentListQuery(int? RelatedId, bool? IsAdmin) : BasePaginatedQuery, IPrimitiveResultQuery<BasePaginatedApiResponse<CommentListDbQueryResponse>>;

sealed class CommentListQueryHandler : IPrimitiveResultQueryHandler<CommentListQuery, BasePaginatedApiResponse<CommentListDbQueryResponse>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public CommentListQueryHandler(
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<BasePaginatedApiResponse<CommentListDbQueryResponse>>> Handle(CommentListQuery request, CancellationToken cancellationToken)
    => await this._readUnitOfWork.CommentReadRepository.FilterComments(
            request,
            request.RelatedId,
            request.LastId,
            cancellationToken)
        .ConfigureAwait(false);
}