using ParsMedeQ.Application.Features.CommentFeatures.CommentListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.CommentRepositories;
public interface ICommentReadRepository : IDomainRepository
{
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<CommentListDbQueryResponse>>> FilterComments(
        BasePaginatedQuery paginated,
        int? userId,
        int? relatedId,
        int lastId,
        CancellationToken cancellationToken);
}
