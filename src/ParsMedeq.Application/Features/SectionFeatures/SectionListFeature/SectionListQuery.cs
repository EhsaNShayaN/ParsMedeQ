using ParsMedeQ.Domain.Aggregates.SectionAggregate;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.SectionFeatures.SectionListFeature;
public sealed record SectionListQuery() : IPrimitiveResultQuery<SectionListDbQueryResponse[]>;

sealed class SectionListQueryHandler : IPrimitiveResultQueryHandler<SectionListQuery, SectionListDbQueryResponse[]>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IUserLangContextAccessor _userLangContextAccessor;

    public SectionListQueryHandler(
        IReadUnitOfWork readUnitOfWork,
        IUserLangContextAccessor userLangContextAccessor)
    {
        this._readUnitOfWork = readUnitOfWork;
        this._userLangContextAccessor = userLangContextAccessor;
    }

    public async Task<PrimitiveResult<SectionListDbQueryResponse[]>> Handle(SectionListQuery request, CancellationToken cancellationToken)
    {
        return await _readUnitOfWork.SectionReadRepository.GetAll(
            _userLangContextAccessor.GetCurrentLang(),
            cancellationToken).ConfigureAwait(false);
    }
}