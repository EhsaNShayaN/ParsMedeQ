namespace ParsMedeq.Contracts.UserContracts.SigninContracts.ResetPasswordContracts;
public readonly record struct ResetPasswordApiRequest(
    string Mobile,
    string Otp,
    string Password);