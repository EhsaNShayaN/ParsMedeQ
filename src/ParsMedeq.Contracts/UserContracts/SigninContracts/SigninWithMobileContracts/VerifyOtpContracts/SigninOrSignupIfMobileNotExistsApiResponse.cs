namespace ParsMedeQ.Contracts.UserContracts.SigninContracts.SigninWithMobileContracts.VerifyOtpContracts;

public readonly record struct SigninOrSignupIfMobileNotExistsApiResponse(
    string Token,
    string Fullname,
    string Mobile);