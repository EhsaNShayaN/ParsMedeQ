namespace ParsMedeQ.Application.Features.ResourceFeatures.AddResourceFeature;
public sealed class AddResourceCommandHandler : IPrimitiveResultCommandHandler<AddResourceCommand, AddResourceCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public AddResourceCommandHandler(
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<AddResourceCommandResponse>> Handle(AddResourceCommand request, CancellationToken cancellationToken)
    {
        return await
                MobileType.Create(request.Doc)
                .Map(mobile => this._writeUnitOfWork.UserWriteRepository
                    .FindByMobile(mobile, cancellationToken)
                    .Map(user => new AddResourceCommandResponse(user is not null)))
                .ConfigureAwait(false);
    }
}
