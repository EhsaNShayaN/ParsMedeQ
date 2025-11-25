using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.SectionFeatures.SectionListFeature;
public sealed record SectionItemsQuery() : IPrimitiveResultQuery<SectionListDbQueryResponse[]>;

sealed class SectionItemsQueryHandler : IPrimitiveResultQueryHandler<SectionItemsQuery, SectionListDbQueryResponse[]>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IUserLangContextAccessor _userLangContextAccessor;

    public SectionItemsQueryHandler(
        IReadUnitOfWork readUnitOfWork,
        IUserLangContextAccessor userLangContextAccessor)
    {
        this._readUnitOfWork = readUnitOfWork;
        this._userLangContextAccessor = userLangContextAccessor;
    }

    public async Task<PrimitiveResult<SectionListDbQueryResponse[]>> Handle(SectionItemsQuery request, CancellationToken cancellationToken)
    {
        return await _readUnitOfWork.SectionReadRepository.GetAllTranslations(
            _userLangContextAccessor.GetCurrentLang(),
            cancellationToken).ConfigureAwait(false);
    }
}