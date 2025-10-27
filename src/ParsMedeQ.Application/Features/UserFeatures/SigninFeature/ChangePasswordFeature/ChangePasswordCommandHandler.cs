using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Domain.Aggregates.UserAggregate.Validators;
using ParsMedeQ.Domain.Helpers;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.ChangePasswordFeature;
public sealed class ChangePasswordCommandHandler : IPrimitiveResultCommandHandler<ChangePasswordCommand, ChangePasswordCommandResponse>
{
    private readonly IOtpServiceFactory _otpServiceFactory;
    private readonly IUserValidatorService _userValidatorService;
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IUserContextAccessor _userContextAccessor;

    public ChangePasswordCommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IUserValidatorService userValidatorService,
        IOtpServiceFactory otpServiceFactory,
        IUserContextAccessor userContextAccessor)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._userValidatorService = userValidatorService;
        this._otpServiceFactory = otpServiceFactory;
        this._userContextAccessor = userContextAccessor;
    }

    public async Task<PrimitiveResult<ChangePasswordCommandResponse>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var otpService = await _otpServiceFactory.Create();
        return await this._writeUnitOfWork.UserWriteRepository
                    .FindById(_userContextAccessor.Current.UserId, cancellationToken)
                    .MapIf(
                        user => user.Mobile.IsDefault(),
                        _ => ValueTask.FromResult(PrimitiveResult.Failure<ChangePasswordCommandResponse>("", "موبایلی برای شما تعریف نشده است")),
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
                        .Map(user => new ChangePasswordCommandResponse(true)))
                .ConfigureAwait(false);
    }
}
