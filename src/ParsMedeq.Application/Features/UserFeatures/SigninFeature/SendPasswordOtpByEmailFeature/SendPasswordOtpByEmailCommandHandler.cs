using Microsoft.FeatureManagement;
using ParsMedeQ.Domain.Types.Email;
using ParsMedeQ.Domain.Types.UserId;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SendPasswordOtpByEmailFeature;

public sealed class SendPasswordOtpByEmailCommandHandler : IPrimitiveResultCommandHandler<SendPasswordOtpByEmailCommand, SendPasswordOtpByEmailCommandResponse>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IOtpServiceFactory _otpServiceFactory;
    private readonly IFeatureManager _featureManager;

    public SendPasswordOtpByEmailCommandHandler(
        IReadUnitOfWork readUnitOfWork,
        IFeatureManager featureManager,
        IOtpServiceFactory otpServiceFactory)
    {
        this._readUnitOfWork = readUnitOfWork;
        this._featureManager = featureManager;
        this._otpServiceFactory = otpServiceFactory;
    }
    public async Task<PrimitiveResult<SendPasswordOtpByEmailCommandResponse>> Handle(
        SendPasswordOtpByEmailCommand request,
        CancellationToken cancellationToken)
    {
        var otpService = await _otpServiceFactory.Create();
        return await this._readUnitOfWork
            .UserReadRepository
            .GetOneOrDefault(
                x => x.Email.Equals(request.Email),
                x => new { x.Id, x.Email },
                new { Id = UserIdType.Empty, Email = EmailType.Empty },
                cancellationToken)
            .MapIf(
                user => user.Email.IsDefault(),
                _ => ValueTask.FromResult(PrimitiveResult.Failure<SendPasswordOtpByEmailCommandResponse>("", "ایمیلی برای شما تعریف نشده است")),
                user => otpService
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