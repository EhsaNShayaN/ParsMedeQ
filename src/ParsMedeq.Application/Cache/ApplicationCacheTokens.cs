using SRH.CacheProvider;

namespace ParsMedeq.Application.Cache;
public static class ApplicationCacheTokens
{
    readonly static CacheToken _otpRootToken = new("OTP", TimeSpan.FromMinutes(2));

    public readonly static CacheTokenKey LoginOTP = new("Login", _otpRootToken);
    public readonly static CacheTokenKey SignupOTP = new("Signup", _otpRootToken);
    public readonly static CacheTokenKey SetPasswordOTP = new("SetPassword", _otpRootToken);
    public readonly static CacheTokenKey SignContractOTP = new("SignContract", _otpRootToken);

    public static CacheTokenKey CreateOTPKey(string key, CacheTokenKey token) => new($"{token.Name}:{key}", token.Parent);
}
