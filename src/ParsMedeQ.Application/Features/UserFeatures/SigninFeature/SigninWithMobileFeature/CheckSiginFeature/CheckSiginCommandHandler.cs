namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.CheckSiginFeature;

public sealed class CheckSiginCommandHandler : IPrimitiveResultCommandHandler<
    CheckSiginCommand,
    CheckSiginCommandResponse>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public CheckSiginCommandHandler(IReadUnitOfWork readUnitOfWork)
    {
        this._readUnitOfWork = readUnitOfWork;
    }
    public async Task<PrimitiveResult<CheckSiginCommandResponse>> Handle(
        CheckSiginCommand request,
        CancellationToken cancellationToken)
    {
        return await MobileType.Create(request.Mobile)
            .Map(mobile => this._readUnitOfWork.UserReadRepository.FindByMobile(mobile, cancellationToken))
            .Match(
                user => new CheckSiginCommandResponse(string.IsNullOrWhiteSpace(user.Password.Value) ? "otp" : "password"),
                user => new CheckSiginCommandResponse("otp"));
    }
}