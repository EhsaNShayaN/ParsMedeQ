using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Services.TokenGeneratorService;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Domain.DomainServices.SigninService;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SigninOrSignupIfMobileNotExistsFeature;

public sealed class SigninOrSignupIfMobileNotExistsCommandHandler : IPrimitiveResultCommandHandler<
    SigninOrSignupIfMobileNotExistsCommand,
    UserTokenInfo>
{
    #region " Fields "
    private readonly ISigninService _signinService;
    private readonly IOtpServiceFactory _otpServiceFactory;
    private readonly ITokenGeneratorService _tokenGeneratorService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    #endregion

    public SigninOrSignupIfMobileNotExistsCommandHandler(
        ISigninService signinService,
        IOtpServiceFactory otpServiceFactory,
        ITokenGeneratorService tokenGeneratorService,
        IHttpContextAccessor httpContextAccessor)
    {
        this._signinService = signinService;
        this._otpServiceFactory = otpServiceFactory;
        this._tokenGeneratorService = tokenGeneratorService;
        this._httpContextAccessor = httpContextAccessor;
    }

    public async Task<PrimitiveResult<UserTokenInfo>> Handle(SigninOrSignupIfMobileNotExistsCommand request, CancellationToken cancellationToken)
    {
        return
            await ContextualResult<LoginContext>.Create(new(request, cancellationToken))
                .Execute(this.SetMobile)
                .Execute(this.ValidateOtp)
                .Execute(this.SigninOrSignupIfMobileNotExists)
                .Execute(this.GenerateToken)
                .OnSuccess(_ =>
                {
                    this._tokenGeneratorService.PersistToken(
                        _.Value.Token!,
                        this._httpContextAccessor.HttpContext!);
                })
                .Map(ctx => new UserTokenInfo(
                    ctx.Token,
                    ctx.SigninResult.FullName.GetValue(),
                    ctx.Mobile.Value))
            .ConfigureAwait(false);
    }

    ValueTask<PrimitiveResult<LoginContext>> SetMobile(LoginContext ctx) => MobileType.Create(ctx.Request.Mobile).Map(ctx.SetMobile);
    async ValueTask<PrimitiveResult<LoginContext>> ValidateOtp(LoginContext ctx)
    {
        var otpService = await _otpServiceFactory.Create();
        return await otpService.Validate(
            ctx.Request.Otp,
            ApplicationCacheTokens.CreateOTPKey(ctx.Mobile.Value, ApplicationCacheTokens.LoginOTP),
            OtpServiceValidationRemoveKeyStrategy.RemoveIfSuccess,
            ctx.CancellationToken).Map(() => ctx);
    }
    ValueTask<PrimitiveResult<LoginContext>> SigninOrSignupIfMobileNotExists(LoginContext ctx)
    {
        return this._signinService.SigninOrSignupIfMobileNotExists(
            ctx.Mobile,
            ctx.CancellationToken)
            .Map(ctx.SetSigninResult);
    }
    ValueTask<PrimitiveResult<LoginContext>> GenerateToken(LoginContext ctx)
    {
        return this._tokenGeneratorService
            .GenerateToken(
                ctx.SigninResult.UserId,
                AuthenticationHelper.WEB_AUDIENCE,
                ctx.CancellationToken)
            .Map(ctx.SetGenerateTokenResult);
    }
}
sealed class LoginContext
{
    public SigninOrSignupIfMobileNotExistsCommand Request { get; }
    public CancellationToken CancellationToken { get; }
    public MobileType Mobile { get; private set; }
    public SigninResult SigninResult { get; private set; }
    public string Token { get; private set; } = null;

    public LoginContext(SigninOrSignupIfMobileNotExistsCommand request, CancellationToken cancellationToken)
    {
        this.Request = request;
        this.CancellationToken = cancellationToken;
    }

    public LoginContext SetMobile(MobileType value) { this.Mobile = value; return this; }
    public LoginContext SetSigninResult(SigninResult value) { this.SigninResult = value; return this; }
    public LoginContext SetGenerateTokenResult(string value) { this.Token = value; return this; }
}