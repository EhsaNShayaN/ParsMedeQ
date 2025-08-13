using Microsoft.FeatureManagement;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SendOtpToExistingMobileFeature;

public sealed class SendOtpToExistingMobileCommandHandler : IPrimitiveResultCommandHandler<SendOtpToExistingMobileCommand, SendOtpToExistingMobileCommandResponse>
{
    private readonly IReadUnitOfWork _taxMemoryReadUnitOfWork;
    private readonly IOtpService _otpService;
    private readonly IFeatureManager _featureManager;

    public SendOtpToExistingMobileCommandHandler(
        IReadUnitOfWork taxMemoryReadUnitOfWork,
        IOtpService otpService,
        IFeatureManager featureManager)
    {
        this._otpService = otpService;
        this._featureManager = featureManager;
        this._taxMemoryReadUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<SendOtpToExistingMobileCommandResponse>> Handle(
        SendOtpToExistingMobileCommand request,
        CancellationToken cancellationToken)
    {
        return await MobileType.Create(request.Mobile)
            .Map(mobile => _taxMemoryReadUnitOfWork.UserReadRepository.FindByMobile(mobile, cancellationToken))
            .Map(user => this._otpService.SendSMS(
                user.Mobile,
                ApplicationCacheTokens.CreateOTPKey(user.Mobile.Value.ToString(), ApplicationCacheTokens.LoginOTP),
                cancellationToken)
            .Map(otp => IsDummy().Map(d => d ? otp : string.Empty))
            .Map(otp => new SendOtpToExistingMobileCommandResponse(otp)))
            .ConfigureAwait(false);
    }
    async ValueTask<PrimitiveResult<bool>> IsDummy()
    {
        return await this._featureManager.IsEnabledAsync(ApplicationFeatureFlags.DummyOTP).ConfigureAwait(false);
    }
}