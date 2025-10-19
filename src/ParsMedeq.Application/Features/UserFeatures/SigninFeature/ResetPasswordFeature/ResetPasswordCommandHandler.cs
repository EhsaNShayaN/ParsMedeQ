using ParsMedeQ.Domain.Aggregates.UserAggregate.Validators;
using ParsMedeQ.Domain.Helpers;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.ResetPasswordFeature;
public sealed class ResetPasswordCommandHandler : IPrimitiveResultCommandHandler<ResetPasswordCommand, ResetPasswordCommandResponse>
{
    private readonly IOtpServiceFactory _otpServiceFactory;
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IUserValidatorService _userValidatorService;

    public ResetPasswordCommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IUserValidatorService userValidatorService,
        IOtpServiceFactory otpServiceFactory)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._userValidatorService = userValidatorService;
        this._otpServiceFactory = otpServiceFactory;
    }

    public async Task<PrimitiveResult<ResetPasswordCommandResponse>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var otpService = await _otpServiceFactory.Create();
        return await
                MobileType.Create(request.Mobile)
                .Map(mobile => this._writeUnitOfWork.UserWriteRepository
                    .FindByMobile(mobile, cancellationToken)
                    .MapIf(
                        user => user.Mobile.IsDefault(),
                        _ => ValueTask.FromResult(PrimitiveResult.Failure<ResetPasswordCommandResponse>("", "موبایلی برای شما تعریف نشده است")),
                        user => otpService.Validate(
                            request.Otp,
                            ApplicationCacheTokens.CreateOTPKey(user.Id.ToString(), ApplicationCacheTokens.SetPasswordOTP),
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
