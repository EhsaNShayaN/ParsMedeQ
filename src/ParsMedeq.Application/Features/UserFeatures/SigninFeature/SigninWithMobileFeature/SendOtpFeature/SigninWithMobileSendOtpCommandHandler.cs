using Microsoft.FeatureManagement;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SendOtpFeature;

public sealed class SigninWithMobileSendOtpCommandHandler : IPrimitiveResultCommandHandler<
    SigninWithMobileSendOtpCommand,
    SigninWithMobileSendOtpCommandResponse>
{
    private readonly IOtpServiceFactory _otpServiceFactory;
    private readonly IFeatureManager _featureManager;
    private readonly IReadUnitOfWork _readUnitOfWork;

    public SigninWithMobileSendOtpCommandHandler(
        IOtpServiceFactory otpServiceFactory,
        IFeatureManager featureManager,
        IReadUnitOfWork readUnitOfWork)
    {
        this._otpServiceFactory = otpServiceFactory;
        this._featureManager = featureManager;
        this._readUnitOfWork = readUnitOfWork;
    }
    public async Task<PrimitiveResult<SigninWithMobileSendOtpCommandResponse>> Handle(
        SigninWithMobileSendOtpCommand request,
        CancellationToken cancellationToken)
    {
        var otpService = await _otpServiceFactory.Create();
        return await MobileType.Create(request.Mobile)
            .Map(mobile => otpService.SendSMS(
                mobile,
                ApplicationCacheTokens.CreateOTPKey(mobile.Value.ToString(), ApplicationCacheTokens.LoginOTP),
                cancellationToken)
            .Map(otp => IsDummy().Map(d => d ? otp : string.Empty).Map(otp => (otp, mobile)))
            .Map(data => this._readUnitOfWork.UserReadRepository.FindByMobile(data.mobile, cancellationToken)
            .Map(user => (flag: user is null, data.otp)))
            .Map(data => new SigninWithMobileSendOtpCommandResponse(data.otp, data.flag)))
            .ConfigureAwait(false);
    }
    async ValueTask<PrimitiveResult<bool>> IsDummy()
    {
        return await this._featureManager.IsEnabledAsync(ApplicationFeatureFlags.DummyOTP).ConfigureAwait(false);
    }
}