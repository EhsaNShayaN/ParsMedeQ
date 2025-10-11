using ParsMedeQ.Application.Persistance.Schema.CommentRepositories;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.CommentRepositories;
internal sealed class CommentReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, ICommentReadRepository
{
    public CommentReadRepository(ReadDbContext dbContext) : base(dbContext) { }

    /*public ValueTask<PrimitiveResult<BasePaginatedApiResponse<CommentListDbQueryResponse>>> FilterComments(
        BasePaginatedQuery paginated,
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

        var query0 =
            from res in this.DbContext.Comment
            where (relatedId ?? 0) == 0 || res.RelatedId.Equals(relatedId)
            select new CommentListDbQueryResponse
            {
                Id = res.Id,
                Title = res.CommentTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Title ?? string.Empty,
                CommentCategoryId = res.CommentCategoryId,
                CommentCategoryTitle = res.CommentCategory.CommentCategoryTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Title ?? string.Empty,
                Image = res.CommentTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Image ?? string.Empty,
                Price = res.Price,
                Discount = res.Discount,
                Deleted = res.Deleted,
                Disabled = res.Disabled,
                CreationDate = res.CreationDate,
            };

        if (lastId.Equals(0))
            return query0.Paginate(
                PaginateQuery.Create(paginated.PageIndex, paginated.PageSize),
                s => s.Id,
                PaginateOrder.DESC,
                cancellationToken)
                .Map(data => MapResult(data, paginated));

        return query0.PaginateOverPK(
            paginated.PageSize,
            lastId,
            PaginateOrder.DESC,
            cancellationToken)
            .Map(data => MapResult(data, paginated));
    }*/
}