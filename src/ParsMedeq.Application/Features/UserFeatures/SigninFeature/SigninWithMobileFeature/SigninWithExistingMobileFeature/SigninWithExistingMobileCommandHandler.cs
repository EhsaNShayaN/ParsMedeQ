using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Domain.DomainServices.SigninService;
using ParsMedeQ.Domain.Helpers;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SigninWithExistingMobileFeature;

public sealed class SigninWithExistingMobileCommandHandler : IPrimitiveResultCommandHandler<
    SigninWithExistingMobileCommand,
    UserTokenInfo>
{
    private readonly ISigninService _signinService;
    private readonly IOtpServiceFactory _otpServiceFactory;

    public SigninWithExistingMobileCommandHandler(
        ISigninService signinService,
        IOtpServiceFactory otpServiceFactory)
    {
        this._signinService = signinService;
        this._otpServiceFactory = otpServiceFactory;
    }

    public async Task<PrimitiveResult<UserTokenInfo>> Handle(SigninWithExistingMobileCommand request, CancellationToken cancellationToken)
    {
        return
            await ContextualResult<LoginContext>.Create(new(request, cancellationToken))
                .Execute(SetMobile)
                .Execute(ValidateOtp)
                .Execute(SigninMobileExists)
                .Map(ctx => new UserTokenInfo(
                    HashIdsHelper.Instance.EncodeLong(ctx.SigninResult.UserId),
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
    ValueTask<PrimitiveResult<LoginContext>> SigninMobileExists(LoginContext ctx)
    {
        return this._signinService.SigninWithExistingMobile(
            ctx.Mobile,
            ctx.CancellationToken)
            .Map(ctx.SetSigninResult);
    }
}
sealed record LoginContext(SigninWithExistingMobileCommand Request, CancellationToken CancellationToken)
{
    public MobileType Mobile { get; private set; }
    public SigninResult SigninResult { get; private set; }

    public LoginContext SetMobile(MobileType value) => this with { Mobile = value };
    public LoginContext SetSigninResult(SigninResult value) => this with { SigninResult = value };
}