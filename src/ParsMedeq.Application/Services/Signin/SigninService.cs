﻿using ParsMedeQ.Application.Errors;
using ParsMedeQ.Domain.Aggregates.UserAggregate.UserEntity;
using ParsMedeQ.Domain.Aggregates.UserAggregate.Validators;
using ParsMedeQ.Domain.DomainServices.SigninService;
using ParsMedeQ.Domain.Types.FullName;
using ParsMedeQ.Domain.Types.Password;
using ParsMedeQ.Domain.Types.UserId;
using Medallion.Threading;

namespace ParsMedeQ.Application.Services.Signin;
internal sealed class SigninService : ISigninService
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IUserValidatorService _userValidatorService;
    private readonly IDistributedLockProvider _distributedLockProvider;

    public SigninService(
        IWriteUnitOfWork writeUnitOfWork,
        IUserValidatorService userValidatorService,
        IReadUnitOfWork readUnitOfWork,
        IDistributedLockProvider distributedLockProvider)
    {
        this._distributedLockProvider = distributedLockProvider;
        this._readUnitOfWork = readUnitOfWork;
        this._userValidatorService = userValidatorService;
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async ValueTask<PrimitiveResult<SigninResult>> SigninOrSignupIfMobileNotExists(MobileType mobile, UserIdType registrantId, CancellationToken cancellationToken)
    {
        var sessionName = $"SigninOrSignupIfMobileNotExists:{mobile.Value}";

        using var handle = await this._distributedLockProvider.TryAcquireLockAsync(sessionName, TimeSpan.Zero, cancellationToken);
        if (handle is null) return ApplicationErrors.CreateTooManyRequestsError<SigninResult>();

        var user = await this._readUnitOfWork
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
            .Map(newUser => this._writeUnitOfWork.UserWriteRepository.AddUser(newUser, cancellationToken))
            .Map(newUser => this._writeUnitOfWork.SaveChangesAsync().Map(_ => newUser))
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

        var user = await this._readUnitOfWork
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

        var user = await this._readUnitOfWork
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
            .Map(newUser => this._writeUnitOfWork.UserWriteRepository.AddUser(newUser, cancellationToken))
            .Map(newUser => this._writeUnitOfWork.SaveChangesAsync().Map(_ => newUser))
            .Map(newUser => new SigninResult(
                newUser.Id,
                newUser.FullName,
                newUser.Mobile))
            .ConfigureAwait(false);
    }
}
