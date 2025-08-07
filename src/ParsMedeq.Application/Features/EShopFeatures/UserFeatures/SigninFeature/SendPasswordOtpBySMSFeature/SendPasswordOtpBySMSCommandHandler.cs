using EShop.Domain.Types.UserId;
using Microsoft.FeatureManagement;

namespace EShop.Application.Features.EShopFeatures.UserFeatures.SigninFeature.SendPasswordOtpBySMSFeature;

public sealed class SendPasswordOtpBySMSCommandHandler : IPrimitiveResultCommandHandler<SendPasswordOtpBySMSCommand, SendPasswordOtpBySMSCommandResponse>
{
    private readonly IEShopReadUnitOfWork _taxMemoryReadUnitOfWork;
    private readonly IOtpService _otpService;
    private readonly IFeatureManager _featureManager;

    public SendPasswordOtpBySMSCommandHandler(
        IEShopReadUnitOfWork taxMemoryReadUnitOfWork,
        IOtpService otpService,
        IFeatureManager featureManager)
    {
        this._taxMemoryReadUnitOfWork = taxMemoryReadUnitOfWork;
        this._otpService = otpService;
        this._featureManager = featureManager;
    }
    public async Task<PrimitiveResult<SendPasswordOtpBySMSCommandResponse>> Handle(
        SendPasswordOtpBySMSCommand request,
        CancellationToken cancellationToken)
    {
        return await this._taxMemoryReadUnitOfWork
            .UserReadRepository
            .GetOneOrDefault(
                x => x.Mobile.Equals(MobileType.CreateUnsafe(request.Mobile)),
                x => new { x.Id, x.Mobile },
                new { Id = UserIdType.Empty, Mobile = MobileType.Empty },
                cancellationToken)
            .MapIf(
                user => user.Mobile.IsDefault(),
                _ => ValueTask.FromResult(PrimitiveResult.Failure<SendPasswordOtpBySMSCommandResponse>("", "موبایلی برای شما تعریف نشده است")),
                user => this._otpService.SendSMS(
                user.Mobile,
                ApplicationCacheTokens.CreateOTPKey(user.Id.Value.ToString(), ApplicationCacheTokens.SetPasswordOTP),
                cancellationToken)
                    .Map(otp => IsDummy().Map(d => d ? otp : string.Empty))
                    .Map(otp => new SendPasswordOtpBySMSCommandResponse(otp)))
            .ConfigureAwait(false);
    }
    async ValueTask<PrimitiveResult<bool>> IsDummy()
    {
        return await this._featureManager.IsEnabledAsync(ApplicationFeatureFlags.DummyOTP).ConfigureAwait(false);
    }
}