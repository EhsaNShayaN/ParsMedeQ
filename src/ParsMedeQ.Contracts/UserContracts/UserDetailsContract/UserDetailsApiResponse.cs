namespace ParsMedeQ.Contracts.UserContracts.UserDetailsContract;
public readonly record struct UserDetailsApiResponse(
    int Id,
    string FullName,
    string Email,
    string Mobile,
    bool PasswordMustBeSet,
    bool IsEmailConfirmed,
    bool IsMobileConfirmed);