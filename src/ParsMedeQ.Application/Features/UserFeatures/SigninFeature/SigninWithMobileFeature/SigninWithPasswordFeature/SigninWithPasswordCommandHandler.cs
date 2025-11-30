using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Services.TokenGeneratorService;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Domain.DomainServices.SigninService;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SigninWithPasswordFeature;

public sealed class SigninWithPasswordCommandHandler : IPrimitiveResultCommandHandler<
    SigninWithPasswordCommand,
    UserTokenInfo>
{
    private readonly ISigninService _signinService;
    private readonly ITokenGeneratorService _tokenGeneratorService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SigninWithPasswordCommandHandler(
        ISigninService signinService,
        ITokenGeneratorService tokenGeneratorService,
        IHttpContextAccessor httpContextAccessor
        )
    {
        this._signinService = signinService;
        this._tokenGeneratorService = tokenGeneratorService;
        this._httpContextAccessor = httpContextAccessor;
    }

    public async Task<PrimitiveResult<UserTokenInfo>> Handle(SigninWithPasswordCommand request, CancellationToken cancellationToken)
    {
        return
            await ContextualResult<LoginContext>.Create(new(request, cancellationToken))
                .Execute(SetMobile)
                .Execute(SigninWithMobileAndPassword)
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
    ValueTask<PrimitiveResult<LoginContext>> SigninWithMobileAndPassword(LoginContext ctx)
    {
        return this._signinService.SigninWithMobileAndPassword(
            ctx.Mobile,
            ctx.Request.Password,
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
sealed record LoginContext(SigninWithPasswordCommand Request, CancellationToken CancellationToken)
{
    public MobileType Mobile { get; private set; } = null!;
    public SigninResult SigninResult { get; private set; }
    public string Token { get; private set; } = string.Empty;

    public LoginContext SetMobile(MobileType value) => this with { Mobile = value };
    public LoginContext SetSigninResult(SigninResult value) => this with { SigninResult = value };
    public LoginContext SetGenerateTokenResult(string value) { this.Token = value; return this; }

}