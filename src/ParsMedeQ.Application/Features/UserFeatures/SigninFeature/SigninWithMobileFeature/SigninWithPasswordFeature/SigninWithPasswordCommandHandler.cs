using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Domain.DomainServices.SigninService;
using ParsMedeQ.Domain.Helpers;
using ParsMedeQ.Domain.Types.Password;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SigninWithPasswordFeature;

public sealed class SigninWithPasswordCommandHandler : IPrimitiveResultCommandHandler<
    SigninWithPasswordCommand,
    UserTokenInfo>
{
    private readonly ISigninService _signinService;

    public SigninWithPasswordCommandHandler(
        ISigninService signinService)
    {
        this._signinService = signinService;
    }

    public async Task<PrimitiveResult<UserTokenInfo>> Handle(SigninWithPasswordCommand request, CancellationToken cancellationToken)
    {
        return
            await ContextualResult<LoginContext>.Create(new(request, cancellationToken))
                .Execute(SetMobile)
                .Execute(SetPassword)
                .Execute(SigninWithMobileAndPassword)
                .Map(ctx => new UserTokenInfo(
                    HashIdsHelper.Instance.EncodeLong(ctx.SigninResult.UserId),
                    ctx.SigninResult.FullName.GetValue(),
                    ctx.Mobile.Value))
            .ConfigureAwait(false);
    }

    ValueTask<PrimitiveResult<LoginContext>> SetMobile(LoginContext ctx) => MobileType.Create(ctx.Request.Mobile).Map(ctx.SetMobile);
    ValueTask<PrimitiveResult<LoginContext>> SetPassword(LoginContext ctx)
    {
        return PasswordHelper.HashAndSaltPassword(ctx.Request.Password)
            .Map(genPass => PasswordType.Create(genPass.HashedPassword, genPass.Salt)
            .Map(ctx.SetPassword));
    }
    ValueTask<PrimitiveResult<LoginContext>> SigninWithMobileAndPassword(LoginContext ctx)
    {
        return this._signinService.SigninWithMobileAndPassword(
            ctx.Mobile,
            ctx.Password,
            ctx.CancellationToken)
            .Map(ctx.SetSigninResult);
    }
}
sealed record LoginContext(SigninWithPasswordCommand Request, CancellationToken CancellationToken)
{
    public MobileType Mobile { get; private set; }
    public PasswordType Password { get; private set; }
    public SigninResult SigninResult { get; private set; }

    public LoginContext SetMobile(MobileType value) => this with { Mobile = value };
    public LoginContext SetPassword(PasswordType value) => this with { Password = value };
    public LoginContext SetSigninResult(SigninResult value) => this with { SigninResult = value };
}