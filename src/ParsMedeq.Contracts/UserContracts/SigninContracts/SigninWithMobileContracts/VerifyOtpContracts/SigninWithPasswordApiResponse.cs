namespace ParsMedeQ.Contracts.UserContracts.SigninContracts.SigninWithMobileContracts.VerifyOtpContracts;

public readonly record struct SigninWithPasswordApiResponse(
    string Token,
    string Fullname,
    string Mobile);