using EShop.Application.Helpers;
using EShop.Application.Persistance.ESopSchema.ProductRepositories;
using EShop.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Extensions;
using SRH.Persistance.Models;

namespace EShop.Infrastructure.Persistance.Repositories.ProductRepositories;
internal sealed class ProductReadRepository : GenericPrimitiveReadRepositoryBase<EshopReadDbContext>, IProductReadRepository
{
    public ProductReadRepository(EshopReadDbContext dbContext) : base(dbContext) { }

    public async ValueTask<PrimitiveResult<BasePaginatedApiResponse<ProductModel>>> FilterProductModels(
        BasePaginatedQuery pageinated,
        CancellationToken cancellationToken)
    {
        static BasePaginatedApiResponse<ProductModel> MapResult(
            PaginateListResult<ProductModel> paginatedData,
            BasePaginatedQuery pageinated)
        {
            var data = paginatedData.Data.ToArray();
            return new BasePaginatedApiResponse<ProductModel>(
                data,
                paginatedData.Total,
                pageinated.PageIndex,
                pageinated.PageSize)
            {
                LastId = data.Length > 0 ? data.Last().Id : 0
            };
        }

        var baseQuery = this.DbContext
            .ProductModel
            .Include(s => s.Brand)
            .AsSplitQuery();


        if (pageinated.LastId.Equals(0))
        {
            return await baseQuery.Paginate(
                PaginateQuery.Create(pageinated.PageIndex, pageinated.PageSize),
                s => s.Id,
                PaginateOrder.DESC,
                cancellationToken)
                .Map(data => MapResult(data, pageinated));
        }

        return await baseQuery.PaginateOverPK(
            pageinated.PageSize,
            pageinated.LastId,
            PaginateOrder.DESC,
            cancellationToken)
            .Map(data => MapResult(data, pageinated));
    }
}

