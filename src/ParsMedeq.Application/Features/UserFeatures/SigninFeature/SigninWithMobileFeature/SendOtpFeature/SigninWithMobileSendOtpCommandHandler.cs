using Microsoft.FeatureManagement;

namespace ParsMedeq.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SendOtpFeature;

public sealed class SigninWithMobileSendOtpCommandHandler : IPrimitiveResultCommandHandler<SigninWithMobileSendOtpCommand, SigninWithMobileSendOtpCommandResponse>
{
    private readonly IReadUnitOfWork _taxMemoryReadUnitOfWork;
    private readonly IOtpService _otpService;
    private readonly IFeatureManager _featureManager;

    public SigninWithMobileSendOtpCommandHandler(
        IReadUnitOfWork taxMemoryReadUnitOfWork,
        IOtpService otpService,
        IFeatureManager featureManager)
    {
        this._otpService = otpService;
        this._featureManager = featureManager;
        this._taxMemoryReadUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<SigninWithMobileSendOtpCommandResponse>> Handle(
        SigninWithMobileSendOtpCommand request,
        CancellationToken cancellationToken)
    {
        return await MobileType.Create(request.Mobile)
            .Map(mobile => this._otpService.SendSMS(
                mobile,
                ApplicationCacheTokens.CreateOTPKey(mobile.Value.ToString(), ApplicationCacheTokens.LoginOTP),
                cancellationToken)
            .Map(otp => IsDummy().Map(d => d ? otp : string.Empty))
            .Map(otp => new SigninWithMobileSendOtpCommandResponse(otp)))
            .ConfigureAwait(false);
    }
    async ValueTask<PrimitiveResult<bool>> IsDummy()
    {
        return await this._featureManager.IsEnabledAsync(ApplicationFeatureFlags.DummyOTP).ConfigureAwait(false);
    }
}