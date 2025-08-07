using EShop.Domain.Types.Email;
using EShop.Domain.Types.UserId;
using Microsoft.FeatureManagement;

namespace EShop.Application.Features.EShopFeatures.UserFeatures.SigninFeature.SendPasswordOtpByEmailFeature;

public sealed class SendPasswordOtpByEmailCommandHandler : IPrimitiveResultCommandHandler<SendPasswordOtpByEmailCommand, SendPasswordOtpByEmailCommandResponse>
{
    private readonly IEShopReadUnitOfWork _taxMemoryReadUnitOfWork;
    private readonly IOtpService _otpService;
    private readonly IFeatureManager _featureManager;

    public SendPasswordOtpByEmailCommandHandler(
        IEShopReadUnitOfWork taxMemoryReadUnitOfWork,
        IOtpService otpService,
        IFeatureManager featureManager)
    {
        this._taxMemoryReadUnitOfWork = taxMemoryReadUnitOfWork;
        this._otpService = otpService;
        this._featureManager = featureManager;
    }
    public async Task<PrimitiveResult<SendPasswordOtpByEmailCommandResponse>> Handle(
        SendPasswordOtpByEmailCommand request,
        CancellationToken cancellationToken)
    {
        return await this._taxMemoryReadUnitOfWork
            .UserReadRepository
            .GetOneOrDefault(
                x => x.Email.Equals(request.Email),
                x => new { x.Id, x.Email },
                new { Id = UserIdType.Empty, Email = EmailType.Empty },
                cancellationToken)
            .MapIf(
                user => user.Email.IsDefault(),
                _ => ValueTask.FromResult(PrimitiveResult.Failure<SendPasswordOtpByEmailCommandResponse>("", "ایمیلی برای شما تعریف نشده است")),
                user => this._otpService
                .SendEmail(
                    user.Email,
                    "ورود به سیستم", $"کد یکبار مصرف : {{otp}}",
                    ApplicationCacheTokens.CreateOTPKey(user.Id.Value.ToString(), ApplicationCacheTokens.SetPasswordOTP),
                    cancellationToken)
                .Map(otp => IsDummy().Map(d => d ? otp : string.Empty))
                .Map(otp => new SendPasswordOtpByEmailCommandResponse(otp)))
            .ConfigureAwait(false);

    }
    async ValueTask<PrimitiveResult<bool>> IsDummy()
    {
        return await this._featureManager.IsEnabledAsync(ApplicationFeatureFlags.DummyOTP).ConfigureAwait(false);
    }
}