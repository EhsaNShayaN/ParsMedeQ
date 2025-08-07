namespace EShop.Contracts.UserContracts.SigninContracts.SigninWithMobileContracts.VerifyOtpContracts;

public readonly record struct SigninWithExistingMobileApiResponse(
    string Token,
    string Fullname,
    string Mobile);