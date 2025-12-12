namespace ParsMedeQ.Contracts.UserContracts.UserDetailsContract;
public readonly record struct UserDetailsApiResponse(
    int Id,
    string FirstName,
    string LastName,
    string FullName,
    string Email,
    string Mobile,
    string NationalCode,
    bool PasswordMustBeSet,
    bool IsEmailConfirmed,
    bool IsMobileConfirmed);