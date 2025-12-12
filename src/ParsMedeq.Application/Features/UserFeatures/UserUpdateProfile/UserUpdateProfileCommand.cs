namespace ParsMedeQ.Application.Features.UserFeatures.UserUpdateProfile;
public sealed record UserUpdateProfileCommand(
    int UserId,
    string FirstName,
    string LastName,
    string NationalCode,
    string Email) : IPrimitiveResultCommand<UserUpdateProfileCommandResponse>
{ }
