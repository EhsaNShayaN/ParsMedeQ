using ParsMedeQ.Domain.Aggregates.UserAggregate.Validators;
using ParsMedeQ.Domain.Helpers;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.ResetPasswordFeature;
public sealed class ResetPasswordCommandHandler : IPrimitiveResultCommandHandler<ResetPasswordCommand, ResetPasswordCommandResponse>
{
    private readonly IOtpService _otpService;
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IUserValidatorService _userValidatorService;

    public ResetPasswordCommandHandler(
        IOtpService otpService,
        IWriteUnitOfWork writeUnitOfWork,
        IUserValidatorService userValidatorService)
    {
        this._otpService = otpService;
        this._writeUnitOfWork = writeUnitOfWork;
        this._userValidatorService = userValidatorService;
    }

    public async Task<PrimitiveResult<ResetPasswordCommandResponse>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        return await
                MobileType.Create(request.Mobile)
                .Map(mobile => this._writeUnitOfWork.UserWriteRepository
                    .FindByMobile(mobile, cancellationToken)
                    .MapIf(
                        user => user.Mobile.IsDefault(),
                        _ => ValueTask.FromResult(PrimitiveResult.Failure<ResetPasswordCommandResponse>("", "موبایلی برای شما تعریف نشده است")),
                        user => this._otpService.Validate(
                            request.Otp,
                            ApplicationCacheTokens.CreateOTPKey(user.Id.Value.ToString(), ApplicationCacheTokens.SetPasswordOTP),
                            OtpServiceValidationRemoveKeyStrategy.RemoveIfSuccess,
                            cancellationToken)
                        .Map(() => PasswordHelper.HashAndSaltPassword(request.Password))
                        .Map(generatedPass => this._writeUnitOfWork.UserWriteRepository.FindById(user.Id, cancellationToken)
                        .Bind(user => user.UpdatePassword(generatedPass.HashedPassword, generatedPass.Salt, this._userValidatorService)))
                        .Bind(user => this._writeUnitOfWork.UserWriteRepository.UpdatePassword(user, cancellationToken))
                        .Bind(user => this._writeUnitOfWork.SaveChangesAsync(cancellationToken).Map(_ => user))
                        .Map(user => new ResetPasswordCommandResponse(true))))
                .ConfigureAwait(false);
    }
}
