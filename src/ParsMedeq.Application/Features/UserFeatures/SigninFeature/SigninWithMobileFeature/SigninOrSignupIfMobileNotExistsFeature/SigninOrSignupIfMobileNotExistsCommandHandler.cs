using ParsMedeQ.Application.Services.UserAuthenticationServices;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Domain.DomainServices.SigninService;
using ParsMedeQ.Domain.Types.UserId;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SigninOrSignupIfMobileNotExistsFeature;

public sealed class SigninOrSignupIfMobileNotExistsCommandHandler : IPrimitiveResultCommandHandler<
    SigninOrSignupIfMobileNotExistsCommand,
    UserTokenInfo>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly ISigninService _signinService;
    private readonly IOtpService _otpService;
    private readonly IUserAuthenticationTokenService _userAuthenticationTokenService;

    public SigninOrSignupIfMobileNotExistsCommandHandler(
        IReadUnitOfWork readUnitOfWork,
        ISigninService signinService,
        IOtpService otpService,
        IUserAuthenticationTokenService userAuthenticationTokenService)
    {
        this._signinService = signinService;
        this._readUnitOfWork = readUnitOfWork;
        this._otpService = otpService;
        this._userAuthenticationTokenService = userAuthenticationTokenService;
    }

    public async Task<PrimitiveResult<UserTokenInfo>> Handle(SigninOrSignupIfMobileNotExistsCommand request, CancellationToken cancellationToken)
    {
        return
            await ContextualResult<LoginContext>.Create(new(request, cancellationToken))
                .Execute(SetMobile)
                .Execute(ValidateOtp)
                .Execute(SigninOrSignupIfMobileNotExists)
                .Execute(SetToken)
                .Map(ctx => new UserTokenInfo(
                    //HashIdsHelper.Instance.EncodeLong(ctx.SigninResult.UserId.Value),
                    ctx.Token,
                    ctx.SigninResult.FullName.GetValue(),
                    ctx.Mobile.Value))
            .ConfigureAwait(false);
    }

    ValueTask<PrimitiveResult<LoginContext>> SetMobile(LoginContext ctx) => MobileType.Create(ctx.Request.Mobile).Map(ctx.SetMobile);
    ValueTask<PrimitiveResult<LoginContext>> ValidateOtp(LoginContext ctx)
    {
        return this._otpService.Validate(
            ctx.Request.Otp,
            ApplicationCacheTokens.CreateOTPKey(ctx.Mobile.Value, ApplicationCacheTokens.LoginOTP),
            OtpServiceValidationRemoveKeyStrategy.RemoveIfSuccess,
            ctx.CancellationToken).Map(() => ctx);
    }
    ValueTask<PrimitiveResult<LoginContext>> SigninOrSignupIfMobileNotExists(LoginContext ctx)
    {
        return this._signinService.SigninOrSignupIfMobileNotExists(
            ctx.Mobile,
            UserIdType.Empty, ctx.CancellationToken)
            .Map(ctx.SetSigninResult);
    }
    ValueTask<PrimitiveResult<LoginContext>> SetToken(LoginContext ctx)
    {
        return this._userAuthenticationTokenService.GenerateToken(ctx.Mobile.Value)
            .Map(ctx.SetToken);
    }
}
sealed record LoginContext(SigninOrSignupIfMobileNotExistsCommand Request, CancellationToken CancellationToken)
{
    public MobileType Mobile { get; private set; }
    public SigninResult SigninResult { get; private set; }
    public string Token { get; private set; }

    public LoginContext SetMobile(MobileType value) => this with { Mobile = value };
    public LoginContext SetSigninResult(SigninResult value) => this with { SigninResult = value };
    public LoginContext SetToken(string value) => this with { Token = value };
}