namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SendOtpFeature;

public readonly record struct SigninWithMobileSendOtpCommandResponse(
    string Otp,
    bool Flag);
