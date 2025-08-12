using ParsMedeq.Application.Errors;
using ParsMedeq.Application.Services.EmailSenderService;
using ParsMedeq.Application.Services.OTP;
using ParsMedeq.Application.Services.SmsSenderService;
using ParsMedeq.Domain.Helpers;
using Medallion.Threading;
using Microsoft.Extensions.Caching.Memory;
using SRH.CacheProvider;

namespace ParsMedeq.Infrastructure.Services.OTP;
public sealed class OtpService : IOtpService
{
    private readonly IMemoryCache _memoryCache;
    private readonly ICacheProvider _dpiCacheProvider;
    private readonly ISmsSenderService _smsSenderService;
    private readonly IEmailSenderService _emailSenderService;
    private readonly IDistributedLockProvider _distributedLockProvider;
    private readonly MemoryCacheEntryOptions cacheEntryOptions;

    public OtpService(
        IMemoryCache memoryCache,
        ICacheProvider dpiCacheProvider,
        ISmsSenderService smsSenderService,
        IEmailSenderService emailSenderService,
        IDistributedLockProvider distributedLockProvider)
    {
        this._memoryCache = memoryCache;
        this._dpiCacheProvider = dpiCacheProvider;
        this._smsSenderService = smsSenderService;
        this._emailSenderService = emailSenderService;
        this._distributedLockProvider = distributedLockProvider;
        this.cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(2)
        };
    }

    public PrimitiveResult SendOtp(string phoneNumber)
    {
        Random rnd = new Random();
        int code = rnd.Next(100000, 999999);  // creates a number by 6 digit
        // TODO: send sms
        this._memoryCache.Set(phoneNumber, code, this.cacheEntryOptions);
        return PrimitiveResult.Success();
    }

    public PrimitiveResult CheckOtp(string phoneNumber, string otp)
    {
        // todo: check otp on memory
        var result = this._memoryCache.Get(phoneNumber)!.ToString();
        if (!string.IsNullOrEmpty(result) && otp == result)
            return PrimitiveResult.Success();
        return PrimitiveResult.Failure("", "Code is Wrong");
    }

    public async ValueTask<PrimitiveResult<string>> SendSMS(MobileType mobile, CacheTokenKey token, CancellationToken cancellationToken)
    {
        var otp = PasswordGeneratorHelper.Generate(6, PasswordGeneratorHelper.PasswordGeneratorOptions.UseDigits);

        return await this._dpiCacheProvider.Set(
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
    public async ValueTask<PrimitiveResult<string>> SendEmail(EmailType email, string subject, string body, CacheTokenKey token, CancellationToken cancellationToken)
    {
        var otp = PasswordGeneratorHelper.Generate(6, PasswordGeneratorHelper.PasswordGeneratorOptions.UseDigits);

        return await this._dpiCacheProvider.Set(
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
        if (handle is null) return ApplicationErrors.CreateTooManyRequestsError();

        return await this._dpiCacheProvider.Get(token, string.Empty, cancellationToken)
            .Map(value => !string.IsNullOrWhiteSpace(value) && value.Equals(otp))
            .Map(result =>
            {
                return (removeStrategy, result) switch
                {
                    (OtpServiceValidationRemoveKeyStrategy.RemoveAlways, _) => this._dpiCacheProvider.Remove(token, cancellationToken).Map(() => result),
                    (OtpServiceValidationRemoveKeyStrategy.RemoveIfSuccess, true) => this._dpiCacheProvider.Remove(token, cancellationToken).Map(() => result),
                    (OtpServiceValidationRemoveKeyStrategy.RemoveIfFailure, false) => this._dpiCacheProvider.Remove(token, cancellationToken).Map(() => result),
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