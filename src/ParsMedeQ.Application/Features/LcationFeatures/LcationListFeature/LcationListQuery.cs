using ParsMedeQ.Application.Features.LcationFeatures.LcationListFeature;
using ParsMedeQ.Application.Services.UserLangServices;
using Polly;
using Polly.Contrib.DuplicateRequestCollapser;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ProductFeatures.LocationListFeature;
public sealed record LocationListQuery() : IPrimitiveResultQuery<LocationListDbQueryResponse[]>;

sealed class LocationListQueryHandler : IPrimitiveResultQueryHandler<LocationListQuery, LocationListDbQueryResponse[]>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IAsyncRequestCollapserPolicy _asyncRequestCollapserPolicy;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public LocationListQueryHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IAsyncRequestCollapserPolicy asyncRequestCollapserPolicy,
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._asyncRequestCollapserPolicy = asyncRequestCollapserPolicy;
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<LocationListDbQueryResponse[]>> Handle(LocationListQuery request, CancellationToken cancellationToken)
    {
        var pollyContext = new Context($"{this.GetType().FullName}");
        return await this._asyncRequestCollapserPolicy.ExecuteAsync(_ =>
            this.HandleCore(request, cancellationToken), pollyContext)
           .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<LocationListDbQueryResponse[]>> HandleCore(
        LocationListQuery request,
        CancellationToken cancellationToken) =>
        await this._readUnitOfWork.LocationReadRepository.FilterLocations(
            _userLangContextAccessor.GetCurrentLang(),
            cancellationToken)
        .ConfigureAwait(false);
}