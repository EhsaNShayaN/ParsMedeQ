﻿using Microsoft.FeatureManagement;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SendOtpFeature;

public sealed class SigninWithMobileSendOtpCommandHandler : IPrimitiveResultCommandHandler<SigninWithMobileSendOtpCommand, SigninWithMobileSendOtpCommandResponse>
{
    private readonly IOtpServiceFactory _otpServiceFactory;
    private readonly IFeatureManager _featureManager;

    public SigninWithMobileSendOtpCommandHandler(
        IOtpServiceFactory otpServiceFactory,
        IFeatureManager featureManager)
    {
        this._otpServiceFactory = otpServiceFactory;
        this._featureManager = featureManager;
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
            .Map(otp => IsDummy().Map(d => d ? otp : string.Empty))
            .Map(otp => new SigninWithMobileSendOtpCommandResponse(otp)))
            .ConfigureAwait(false);
    }
    async ValueTask<PrimitiveResult<bool>> IsDummy()
    {
        return await this._featureManager.IsEnabledAsync(ApplicationFeatureFlags.DummyOTP).ConfigureAwait(false);
    }
}