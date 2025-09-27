using Medallion.Threading;
using Microsoft.FeatureManagement;
using ParsMedeQ.Application;
using ParsMedeQ.Application.Errors;
using ParsMedeQ.Application.Services.EmailSenderService;
using ParsMedeQ.Application.Services.OTP;
using ParsMedeQ.Application.Services.SmsSenderService;
using ParsMedeQ.Domain.Helpers;
using SRH.CacheProvider;

namespace ParsMedeQ.Infrastructure.Services.OTP;
public sealed class OtpService : IOtpService
{
    private readonly ICacheProvider _cacheProvider;
    private readonly ISmsSenderService _smsSenderService;
    private readonly IEmailSenderService _emailSenderService;
    private readonly IDistributedLockProvider _distributedLockProvider;

    public OtpService(
        ICacheProvider cacheProvider,
        ISmsSenderService smsSenderService,
        IEmailSenderService emailSenderService,
        IDistributedLockProvider distributedLockProvider)
    {
        this._cacheProvider = cacheProvider;
        this._smsSenderService = smsSenderService;
        this._emailSenderService = emailSenderService;
        this._distributedLockProvider = distributedLockProvider;
    }

    public async ValueTask<PrimitiveResult<string>> SendSMS(
        MobileType mobile,
        CacheTokenKey token,
        CancellationToken cancellationToken)
    {
        var otp = PasswordGeneratorHelper.Generate(6, PasswordGeneratorHelper.PasswordGeneratorOptions.UseDigits);

        return await this._cacheProvider.Set(
            token,
            otp,
            cancellationToken)
            .Map(async () =>
            {
                await Task.Run(() => this._smsSenderService.SendVerificationCode(mobile.Value, otp, cancellationToken))
                    .ConfigureAwait(false);
                return PrimitiveResult.Success();
            })
            .Map(_ => otp)
            .ConfigureAwait(false);
    }
    public async ValueTask<PrimitiveResult<string>> SendEmail(
        EmailType email,
        string subject,
        string body,
        CacheTokenKey token,
        CancellationToken cancellationToken)
    {
        var otp = PasswordGeneratorHelper.Generate(6, PasswordGeneratorHelper.PasswordGeneratorOptions.UseDigits);

        return await this._cacheProvider.Set(
            token,
            otp,
            cancellationToken)
            .Map(async () =>
            {
                await Task.Run(() => this._emailSenderService.Send(email.Value, subject, body.Replace("{{otp}}", otp), true, cancellationToken))
                    .ConfigureAwait(false);
                return PrimitiveResult.Success();
            })
            .Map(_ => otp)
            .ConfigureAwait(false);
    }
    public async ValueTask<PrimitiveResult> Validate(
        string otp,
        CacheTokenKey token,
        OtpServiceValidationRemoveKeyStrategy removeStrategy,
        CancellationToken cancellationToken)
    {
        using var handle = await this._distributedLockProvider.TryAcquireLockAsync(token.GetCacheKey(), TimeSpan.Zero, cancellationToken);
        if (handle is null)
        {
            return ApplicationErrors.CreateTooManyRequestsError();
        }

        return await this._cacheProvider.Get(token, string.Empty, cancellationToken)
        .Map(value => !string.IsNullOrWhiteSpace(value) && value.Equals(otp))
        .Map(result =>
        {
            return (removeStrategy, result) switch
            {
                (OtpServiceValidationRemoveKeyStrategy.RemoveAlways, _) => this._cacheProvider.Remove(token, cancellationToken).Map(() => result),
                (OtpServiceValidationRemoveKeyStrategy.RemoveIfSuccess, true) => this._cacheProvider.Remove(token, cancellationToken).Map(() => result),
                (OtpServiceValidationRemoveKeyStrategy.RemoveIfFailure, false) => this._cacheProvider.Remove(token, cancellationToken).Map(() => result),
                _ => ValueTask.FromResult(PrimitiveResult.Success(result))
            };
        })
        .Match
        (
            success => success ? PrimitiveResult.Success() : PrimitiveResult.Failure("", "کد یک بار مصرف اشتباه است"),
            _ => PrimitiveResult.Failure("", "خطا در بررسی کد یکبار مصرف")
        )
        .ConfigureAwait(false);
    }
}
public sealed class OtpServiceFactory : IOtpServiceFactory
{
    private readonly IFeatureManager _featureManager;
    private readonly IServiceProvider _serviceProvider;

    public OtpServiceFactory(
        IFeatureManager featureManager,
        IServiceProvider serviceProvider)
    {
        this._featureManager = featureManager;
        this._serviceProvider = serviceProvider;
    }

    public async ValueTask<IOtpService> Create()
    {
        IOtpService? result;
        var hasRedis = await HasRedis();
        if (hasRedis)
        {
            result = this._serviceProvider.GetKeyedService<IOtpService>("redis");
        }
        else
        {
            result = this._serviceProvider.GetKeyedService<IOtpService>("inMemory");
        }
        if (result is null) throw new InvalidOperationException("");
        return result;
    }
    async ValueTask<bool> HasRedis()
    {
        return await this._featureManager.IsEnabledAsync(ApplicationFeatureFlags.OTPWithRedis);
    }
}