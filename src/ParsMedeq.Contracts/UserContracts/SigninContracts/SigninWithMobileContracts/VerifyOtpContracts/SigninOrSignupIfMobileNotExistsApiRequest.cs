namespace EShop.Contracts.UserContracts.SigninContracts.SigninWithMobileContracts.VerifyOtpContracts;

public readonly record struct SigninOrSignupIfMobileNotExistsApiRequest(string Mobile, string Otp);

