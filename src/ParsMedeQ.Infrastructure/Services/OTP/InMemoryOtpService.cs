using Medallion.Threading;
using Microsoft.Extensions.Caching.Memory;
using ParsMedeQ.Application.Errors;
using ParsMedeQ.Application.Services.EmailSenderService;
using ParsMedeQ.Application.Services.OTP;
using ParsMedeQ.Application.Services.SmsSenderService;
using ParsMedeQ.Domain.Helpers;
using SRH.CacheProvider;

namespace ParsMedeQ.Infrastructure.Services.OTP;
public sealed class InMemoryOtpService : IOtpService
{
    private readonly IMemoryCache _memoryCache;
    private readonly ISmsSenderService _smsSenderService;
    private readonly IEmailSenderService _emailSenderService;
    private readonly IDistributedLockProvider _distributedLockProvider;
    private readonly MemoryCacheEntryOptions cacheEntryOptions;

    public InMemoryOtpService(
        IMemoryCache memoryCache,
        ISmsSenderService smsSenderService,
        IEmailSenderService emailSenderService,
        IDistributedLockProvider distributedLockProvider)
    {
        this._memoryCache = memoryCache;
        this._smsSenderService = smsSenderService;
        this._emailSenderService = emailSenderService;
        this._distributedLockProvider = distributedLockProvider;
        this.cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(2)
        };
    }

    public async ValueTask<PrimitiveResult<string>> SendSMS(
        MobileType mobile,
        CacheTokenKey token,
        CancellationToken cancellationToken)
    {
        var otp = PasswordGeneratorHelper.Generate(6, PasswordGeneratorHelper.PasswordGeneratorOptions.UseDigits);

        return await this.SetInMemory(
            token,
            otp)
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

        return await this.SetInMemory(
            token,
            otp)
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

        return await this.GetFromMemory(token)
            .Map(value => !string.IsNullOrWhiteSpace(value) && value.Equals(otp))
            .Map(result =>
            {
                return (removeStrategy, result) switch
                {
                    (OtpServiceValidationRemoveKeyStrategy.RemoveAlways, _) => this.RemoveFromMemory(token).Map(() => result),
                    (OtpServiceValidationRemoveKeyStrategy.RemoveIfSuccess, true) => this.RemoveFromMemory(token).Map(() => result),
                    (OtpServiceValidationRemoveKeyStrategy.RemoveIfFailure, false) => this.RemoveFromMemory(token).Map(() => result),
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
    PrimitiveResult SetInMemory(object token, string otp)
    {
        this._memoryCache.Set(token, otp, this.cacheEntryOptions);
        return PrimitiveResult.Success();
    }
    PrimitiveResult<string> GetFromMemory(object token)
    {
        var result = this._memoryCache.Get(token);
        return PrimitiveResult.Success(result?.ToString() ?? "");
    }
    PrimitiveResult RemoveFromMemory(object token)
    {
        this._memoryCache.Remove(token);
        return PrimitiveResult.Success();
    }
}