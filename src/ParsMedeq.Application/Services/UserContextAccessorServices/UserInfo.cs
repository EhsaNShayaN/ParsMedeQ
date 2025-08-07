using EShop.Domain.Types.Email;
using EShop.Domain.Types.FullName;

namespace EShop.Application.Services.UserContextAccessorServices;

public readonly record struct UserInfo(
    EmailType UserEmail,
    MobileType UserMobile,
    bool UserMobileConfirmed,
    bool UserEmailConfirmed,
    FullNameType UserFullname)
{
    public readonly static UserInfo Empty = new(
        EmailType.Empty,
        MobileType.Empty,
        false,
        false,
        FullNameType.Empty);
}