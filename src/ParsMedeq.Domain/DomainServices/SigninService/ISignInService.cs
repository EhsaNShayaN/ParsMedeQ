using ParsMedeQ.Domain.Types.FullName;
using ParsMedeQ.Domain.Types.Mobile;
using ParsMedeQ.Domain.Types.UserId;

namespace ParsMedeQ.Domain.DomainServices.SigninService;
public interface ISigninService
{
    ValueTask<PrimitiveResult<SigninResult>> SigninOrSignupIfMobileNotExists(MobileType mobile, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<SigninResult>> SigninWithExistingMobile(MobileType mobile, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<SigninResult>> SignupIfMobileNotExists(
        MobileType mobile,
        FullNameType fullname,
        CancellationToken cancellationToken);
}
public readonly record struct SigninResult(
    UserIdType UserId,
    FullNameType FullName,
    MobileType Mobile);