namespace EShop.Contracts.UserContracts.SigninContracts.SigninWithMobileContracts.VerifyOtpContracts;

public readonly record struct SigninWithExistingMobileApiRequest(string Mobile, string Otp);

