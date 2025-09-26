using ParsMedeQ.Domain.Types.Email;
using SRH.CacheProvider;

namespace ParsMedeQ.Application.Services.OTP;
public interface IOtpService
{
    PrimitiveResult SendOtp(string mobile);
    PrimitiveResult CheckOtp(
        string mobile,
        string otp);
    ValueTask<PrimitiveResult<string>> SendSMS(
        MobileType mobile,
        CacheTokenKey token,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<string>> SendEmail(
        EmailType email,
        string subject,
        string body,
        CacheTokenKey token,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult> Validate(
        string otp,
        CacheTokenKey token,
        OtpServiceValidationRemoveKeyStrategy removeStrategy,
        CancellationToken cancellationToken);
}

public enum OtpServiceValidationRemoveKeyStrategy
{
    RemoveAlways,
    RemoveIfSuccess,
    RemoveIfFailure
}