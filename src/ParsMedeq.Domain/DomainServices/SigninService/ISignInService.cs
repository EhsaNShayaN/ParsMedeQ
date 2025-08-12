using ParsMedeq.Domain.Types.FullName;
using ParsMedeq.Domain.Types.Mobile;
using ParsMedeq.Domain.Types.UserId;

namespace ParsMedeq.Domain.DomainServices.SigninService;
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