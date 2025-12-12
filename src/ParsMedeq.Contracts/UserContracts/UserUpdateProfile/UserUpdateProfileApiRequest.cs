namespace ParsMedeQ.Contracts.UserContracts.UserUpdateProfile;

public readonly record struct UserUpdateProfileApiRequest(
    string FirstName,
    string LastName,
    string NationalCode,
    string Email);
