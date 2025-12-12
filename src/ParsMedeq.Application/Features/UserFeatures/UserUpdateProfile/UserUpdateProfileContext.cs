using ParsMedeQ.Domain.Aggregates.UserAggregate;
using ParsMedeQ.Domain.Types.Email;
using ParsMedeQ.Domain.Types.FirstName;
using ParsMedeQ.Domain.Types.FullName;
using ParsMedeQ.Domain.Types.LastName;

namespace ParsMedeQ.Application.Features.UserFeatures.UserUpdateProfile;

sealed record UserUpdateProfileContext(UserUpdateProfileCommand Request, CancellationToken CancellationToken)
{
    public User UserInfo { get; private set; } = null!;
    public FirstNameType FirstName { get; private set; }
    public LastNameType LastName { get; private set; }
    public FullNameType FullName { get; private set; }
    public string NationalCode { get; private set; }
    public EmailType Email { get; private set; }

    public UserUpdateProfileContext SetUserInfo(User value) => this with { UserInfo = value };
    public UserUpdateProfileContext SetFirstName(FirstNameType value) => this with { FirstName = value };
    public UserUpdateProfileContext SetLastName(LastNameType value) => this with { LastName = value };
    public UserUpdateProfileContext SetFullName(FullNameType value) => this with { FullName = value };
    public UserUpdateProfileContext SetNationalCode(string value) => this with { NationalCode = value };
    public UserUpdateProfileContext SetEmail(EmailType value) => this with { Email = value };
}

