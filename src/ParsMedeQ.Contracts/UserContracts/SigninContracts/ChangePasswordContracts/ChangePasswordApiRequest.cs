namespace ParsMedeQ.Contracts.UserContracts.SigninContracts.ChangePasswordContracts;
public readonly record struct ChangePasswordApiRequest(
    string Otp,
    string Password);