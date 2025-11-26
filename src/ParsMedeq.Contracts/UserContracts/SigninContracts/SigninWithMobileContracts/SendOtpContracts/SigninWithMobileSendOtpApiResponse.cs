namespace ParsMedeQ.Contracts.UserContracts.SigninContracts.SigninWithMobileContracts.SendOtpContracts;

public readonly record struct SigninWithMobileSendOtpApiResponse(
    string Otp,
    bool Flag);
