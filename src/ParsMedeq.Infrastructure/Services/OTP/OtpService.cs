using Medallion.Threading;
using Microsoft.Extensions.Caching.Memory;
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
    private readonly IMemoryCache _memoryCache;
    private readonly ICacheProvider _cacheProvider;
    private readonly ISmsSenderService _smsSenderService;
    private readonly IEmailSenderService _emailSenderService;
    private readonly IDistributedLockProvider _distributedLockProvider;
    private readonly MemoryCacheEntryOptions cacheEntryOptions;
    private readonly IFeatureManager _featureManager;

    public OtpService(
        IMemoryCache memoryCache,
        ICacheProvider cacheProvider,
        ISmsSenderService smsSenderService,
        IEmailSenderService emailSenderService,
        IDistributedLockProvider distributedLockProvider,
        IFeatureManager featureManager)
    {
        this._memoryCache = memoryCache;
        this._cacheProvider = cacheProvider;
        this._smsSenderService = smsSenderService;
        this._emailSenderService = emailSenderService;
        this._distributedLockProvider = distributedLockProvider;
        this.cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(2)
        };
        this._featureManager = featureManager;
    }

    public PrimitiveResult SendOtp(string mobile)
    {
        Random rnd = new Random();
        int code = rnd.Next(100000, 999999);  // creates a number by 6 digit
        // TODO: send sms
        this._memoryCache.Set(mobile, code, this.cacheEntryOptions);
        return PrimitiveResult.Success();
    }
    public PrimitiveResult CheckOtp(
        string mobile,
        string otp)
    {
        // todo: check otp on memory
        var result = this._memoryCache.Get(mobile)!.ToString();
        if (!string.IsNullOrEmpty(result) && otp == result)
            return PrimitiveResult.Success();
        return PrimitiveResult.Failure("", "Code is Wrong");
    }
    public async ValueTask<PrimitiveResult<string>> SendSMS(
        MobileType mobile,
        CacheTokenKey token,
        CancellationToken cancellationToken)
    {
        var otp = PasswordGeneratorHelper.Generate(6, PasswordGeneratorHelper.PasswordGeneratorOptions.UseDigits);

        return await this.HasRedis()
            .Map(hasRedis => hasRedis
        ? this._cacheProvider.Set(
            token,
            otp,
            cancellationToken)
        : this.SetInMemory(token, otp))
            .Map(async _ => await Task.Run(() => this._smsSenderService.SendVerificationCode(mobile.Value, otp, cancellationToken).ConfigureAwait(false)))
            .Map(_ => otp)
            .ConfigureAwait(false);

        /*return await this._cacheProvider.Set(
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
            .ConfigureAwait(false);*/
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
        if ((await this.HasRedis()).Value)
        {
            using var handle = await this._distributedLockProvider.TryAcquireLockAsync(token.GetCacheKey(), TimeSpan.Zero, cancellationToken);
            if (handle is null)
            {
                return ApplicationErrors.CreateTooManyRequestsError();
            }
        }

        return await this.HasRedis()
        .Map(hasRedis => hasRedis
        ? this._cacheProvider.Get(token, string.Empty, cancellationToken)
        : this.GetFromMemory(token))
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
    async ValueTask<PrimitiveResult<bool>> HasRedis()
    {
        return await this._featureManager.IsEnabledAsync(ApplicationFeatureFlags.OTPWithRedis).ConfigureAwait(false);
    }
    ValueTask<PrimitiveResult> SetInMemory(object token, string otp)
    {
        this._memoryCache.Set(token, otp, this.cacheEntryOptions);
        return ValueTask.FromResult(PrimitiveResult.Success());
    }
    ValueTask<PrimitiveResult<string>> GetFromMemory(object token)
    {
        var result = this._memoryCache.Get(token);
        return ValueTask.FromResult(PrimitiveResult.Success(result?.ToString() ?? ""));
    }
}