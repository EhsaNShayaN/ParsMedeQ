using EShop.Application.Services.UserContextAccessorServices;
using EShop.Domain.DomainServices.SigninService;
using EShop.Domain.Helpers;
using EShop.Domain.Types.UserId;

namespace EShop.Application.Features.EShopFeatures.UserFeatures.SigninFeature.SigninWithMobileFeature.SigninOrSignupIfMobileNotExistsFeature;

public sealed class SigninOrSignupIfMobileNotExistsCommandHandler : IPrimitiveResultCommandHandler<
    SigninOrSignupIfMobileNotExistsCommand,
    UserTokenInfo>
{
    private readonly IEShopReadUnitOfWork _taxMemoryReadUnitOfWork;
    private readonly ISigninService _signinService;
    private readonly IOtpService _otpService;

    public SigninOrSignupIfMobileNotExistsCommandHandler(
        IEShopReadUnitOfWork taxMemoryReadUnitOfWork,
        ISigninService signinService,
        IOtpService otpService)
    {
        this._signinService = signinService;
        this._taxMemoryReadUnitOfWork = taxMemoryReadUnitOfWork;
        this._otpService = otpService;
    }

    public async Task<PrimitiveResult<UserTokenInfo>> Handle(SigninOrSignupIfMobileNotExistsCommand request, CancellationToken cancellationToken)
    {
        return
            await ContextualResult<LoginContext>.Create(new(request, cancellationToken))
                .Execute(SetMobile)
                .Execute(ValidateOtp)
                .Execute(SigninOrSignupIfMobileNotExists)
                .Map(ctx => new UserTokenInfo(
                    HashIdsHelper.Instance.EncodeLong(ctx.SigninResult.UserId.Value),
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
}
sealed record LoginContext(SigninOrSignupIfMobileNotExistsCommand Request, CancellationToken CancellationToken)
{
    public MobileType Mobile { get; private set; }
    public SigninResult SigninResult { get; private set; }

    public LoginContext SetMobile(MobileType value) => this with { Mobile = value };
    public LoginContext SetSigninResult(SigninResult value) => this with { SigninResult = value };
}