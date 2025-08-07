using EShop.Domain.Types.Email;
using EShop.Domain.Types.Mobile;
using SRH.CacheProvider;

namespace EShop.Application.Services.OTP;
public interface IOtpService
{
    PrimitiveResult SendOtp(string phoneNumber);
    PrimitiveResult CheckOtp(string phoneNumber, string otp);

    ValueTask<PrimitiveResult<string>> SendSMS(MobileType mobile, CacheTokenKey token, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<string>> SendEmail(EmailType email, string subject, string body, CacheTokenKey token, CancellationToken cancellationToken);

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