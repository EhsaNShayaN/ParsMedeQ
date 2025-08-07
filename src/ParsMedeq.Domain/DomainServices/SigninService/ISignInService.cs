using EShop.Domain.Types.FullName;
using EShop.Domain.Types.Mobile;
using EShop.Domain.Types.UserId;

namespace EShop.Domain.DomainServices.SigninService;
public interface ISigninService
{
    ValueTask<PrimitiveResult<SigninResult>> SigninOrSignupIfMobileNotExists(MobileType mobile, UserIdType registrantId, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<SigninResult>> SigninWithExistingMobile(MobileType mobile, UserIdType registrantId, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<SigninResult>> SignupIfMobileNotExists(
        MobileType mobile,
        FullNameType fullname,
        UserIdType registrantId,
        CancellationToken cancellationToken);
}
public readonly record struct SigninResult(
    UserIdType UserId,
    FullNameType FullName,
    MobileType Mobile);