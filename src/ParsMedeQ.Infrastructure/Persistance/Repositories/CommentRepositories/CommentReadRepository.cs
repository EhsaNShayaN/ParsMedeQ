using ParsMedeQ.Application.Features.CommentFeatures.CommentListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Persistance.Schema.CommentRepositories;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Extensions;
using SRH.Persistance.Models;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.CommentRepositories;
internal sealed class CommentReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, ICommentReadRepository
{
    public CommentReadRepository(ReadDbContext dbContext) : base(dbContext) { }

    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<CommentListDbQueryResponse>>> FilterComments(
        BasePaginatedQuery paginated,
        int? userId,
        int? relatedId,
        int lastId,
        CancellationToken cancellationToken)
    {
        static BasePaginatedApiResponse<CommentListDbQueryResponse> MapResult(
            PaginateListResult<CommentListDbQueryResponse> paginatedData,
            BasePaginatedQuery pageinated)
        {
            var data = paginatedData.Data.ToArray();
            return new BasePaginatedApiResponse<CommentListDbQueryResponse>(
                data,
                paginatedData.Total,
                pageinated.PageIndex,
                pageinated.PageSize)
            {
                LastId = data.Length > 0 ? data.Last().Id : 0
            };
        }

        var query =
            from res in this.DbContext.Comment
            where
                (userId ?? 0) == 0 || res.UserId.Equals(userId) &&
                (relatedId ?? 0) == 0 || res.RelatedId.Equals(relatedId)
            select new CommentListDbQueryResponse
            {
                Id = res.Id,
                Name = res.User.FullName.GetValue(),
                Icon = res.Icon,
                Description = res.Description,
                RelatedId = res.RelatedId,
                TableId = res.TableId,
                TableName = res.TableName,
                Data = res.Data,
                Answers = res.Answers,
                IsConfirmed = res.IsConfirmed,
                CreationDate = res.CreationDate,
            };

        if (lastId.Equals(0))
            return query.Paginate(
                PaginateQuery.Create(paginated.PageIndex, paginated.PageSize),
                s => s.Id,
                PaginateOrder.DESC,
                cancellationToken)
                .Map(data => MapResult(data, paginated));

        return query.PaginateOverPK(
            paginated.PageSize,
            lastId,
            PaginateOrder.DESC,
            cancellationToken)
            .Map(data => MapResult(data, paginated));
    }
}