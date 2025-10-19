using Microsoft.FeatureManagement;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SendPasswordOtpBySMSFeature;

public sealed class SendPasswordOtpBySMSCommandHandler : IPrimitiveResultCommandHandler<SendPasswordOtpBySMSCommand, SendPasswordOtpBySMSCommandResponse>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IOtpServiceFactory _otpServiceFactory;
    private readonly IFeatureManager _featureManager;

    public SendPasswordOtpBySMSCommandHandler(
        IReadUnitOfWork readUnitOfWork,
        IFeatureManager featureManager,
        IOtpServiceFactory otpServiceFactory)
    {
        this._readUnitOfWork = readUnitOfWork;
        this._featureManager = featureManager;
        this._otpServiceFactory = otpServiceFactory;
    }
    public async Task<PrimitiveResult<SendPasswordOtpBySMSCommandResponse>> Handle(
        SendPasswordOtpBySMSCommand request,
        CancellationToken cancellationToken)
    {
        var otpService = await _otpServiceFactory.Create();
        return await this._readUnitOfWork
            .UserReadRepository
            .GetOneOrDefault(
                x => x.Mobile.Equals(MobileType.CreateUnsafe(request.Mobile)),
                x => new { x.Id, x.Mobile },
                new { Id = 0, Mobile = MobileType.Empty },
                cancellationToken)
            .MapIf(
                user => user.Mobile.IsDefault(),
                _ => ValueTask.FromResult(PrimitiveResult.Failure<SendPasswordOtpBySMSCommandResponse>("", "موبایلی برای شما تعریف نشده است")),
                user => otpService.SendSMS(
                user.Mobile,
                ApplicationCacheTokens.CreateOTPKey(user.Id.ToString(), ApplicationCacheTokens.SetPasswordOTP),
                cancellationToken)
                    .Map(otp => IsDummy().Map(d => d ? otp : string.Empty))
                    .Map(otp => new SendPasswordOtpBySMSCommandResponse(otp)))
            .ConfigureAwait(false);
    }
    async ValueTask<PrimitiveResult<bool>> IsDummy()
    {
        return await this._featureManager.IsEnabledAsync(ApplicationFeatureFlags.DummyOTP).ConfigureAwait(false);
    }
}