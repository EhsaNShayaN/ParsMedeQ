using EShop.Application.Errors;
using EShop.Domain.Aggregates.UserAggregate.UserEntity;
using EShop.Domain.Aggregates.UserAggregate.Validators;
using EShop.Domain.DomainServices.SigninService;
using EShop.Domain.Types.FullName;
using EShop.Domain.Types.Password;
using EShop.Domain.Types.UserId;
using Medallion.Threading;

namespace EShop.Application.Services.Signin;
internal sealed class SigninService : ISigninService
{
    private readonly IEShopReadUnitOfWork _eshopReadUnitOfWork;
    private readonly IEShopWriteUnitOfWork _eshopWriteUnitOfWork;
    private readonly IUserValidatorService _userValidatorService;
    private readonly IDistributedLockProvider _distributedLockProvider;

    public SigninService(
        IEShopWriteUnitOfWork eshopWriteUnitOfWork,
        IUserValidatorService userValidatorService,
        IEShopReadUnitOfWork eshopReadUnitOfWork,
        IDistributedLockProvider distributedLockProvider)
    {
        this._distributedLockProvider = distributedLockProvider;
        this._eshopReadUnitOfWork = eshopReadUnitOfWork;
        this._userValidatorService = userValidatorService;
        this._eshopWriteUnitOfWork = eshopWriteUnitOfWork;
    }

    public async ValueTask<PrimitiveResult<SigninResult>> SigninOrSignupIfMobileNotExists(MobileType mobile, UserIdType registrantId, CancellationToken cancellationToken)
    {
        var sessionName = $"SigninOrSignupIfMobileNotExists:{mobile.Value}";

        using var handle = await this._distributedLockProvider.TryAcquireLockAsync(sessionName, TimeSpan.Zero, cancellationToken);
        if (handle is null) return ApplicationErrors.CreateTooManyRequestsError<SigninResult>();

        var user = await this._eshopReadUnitOfWork
            .UserReadRepository
            .FindByMobile(mobile, cancellationToken)
            .ConfigureAwait(false);

        if (user.IsSuccess) return new SigninResult(
            user.Value.Id,
            user.Value.FullName,
            user.Value.Mobile);

        return await User.CreateUnknownUser(
                registrantId,
                mobile,
                this._userValidatorService,
                cancellationToken)
            .Map(newUser => this._eshopWriteUnitOfWork.UserWriteRepository.AddUser(newUser, cancellationToken))
            .Map(newUser => this._eshopWriteUnitOfWork.SaveChangesAsync().Map(_ => newUser))
            .Map(newUser => new SigninResult(
                newUser.Id,
                newUser.FullName,
                newUser.Mobile))
            .ConfigureAwait(false);
    }
    public async ValueTask<PrimitiveResult<SigninResult>> SigninWithExistingMobile(MobileType mobile, UserIdType registrantId, CancellationToken cancellationToken)
    {
        var sessionName = $"SigninMobileExists:{mobile.Value}";

        using var handle = await this._distributedLockProvider.TryAcquireLockAsync(sessionName, TimeSpan.Zero, cancellationToken);
        if (handle is null) return ApplicationErrors.CreateTooManyRequestsError<SigninResult>();

        var user = await this._eshopReadUnitOfWork
            .UserReadRepository
            .FindByMobile(mobile, cancellationToken)
            .ConfigureAwait(false);

        if (user.IsSuccess) return new SigninResult(
            user.Value.Id,
            user.Value.FullName,
            user.Value.Mobile);

        return await ValueTask.FromResult(PrimitiveResult.Failure<SigninResult>("", "کاربر مورد نظر یافت نشد"));
    }
    public async ValueTask<PrimitiveResult<SigninResult>> SignupIfMobileNotExists(MobileType mobile, FullNameType fullname, UserIdType registrantId, CancellationToken cancellationToken)
    {
        var sessionName = $"SignupIfMobileNotExists:{mobile.Value}";

        using var handle = await this._distributedLockProvider.TryAcquireLockAsync(sessionName, TimeSpan.Zero, cancellationToken);
        if (handle is null) return ApplicationErrors.CreateTooManyRequestsError<SigninResult>();

        var user = await this._eshopReadUnitOfWork
            .UserReadRepository
            .FindByMobile(mobile, cancellationToken)
            .ConfigureAwait(false);

        if (user.IsSuccess) return PrimitiveResult.Failure<SigninResult>("", "شماره موبایل مورد نظر وجود دارد");

        return await User.CreateUser(
                registrantId,
                mobile,
                fullname,
                PasswordType.Empty,
                this._userValidatorService,
                cancellationToken)
            .Map(newUser => this._eshopWriteUnitOfWork.UserWriteRepository.AddUser(newUser, cancellationToken))
            .Map(newUser => this._eshopWriteUnitOfWork.SaveChangesAsync().Map(_ => newUser))
            .Map(newUser => new SigninResult(
                newUser.Id,
                newUser.FullName,
                newUser.Mobile))
            .ConfigureAwait(false);
    }
}
