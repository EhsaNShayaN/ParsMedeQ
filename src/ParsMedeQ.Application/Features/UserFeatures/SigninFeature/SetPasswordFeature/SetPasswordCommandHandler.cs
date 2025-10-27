using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Domain.Aggregates.UserAggregate.Validators;
using ParsMedeQ.Domain.Helpers;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SetPasswordFeature;
public sealed class SetPasswordCommandHandler : IPrimitiveResultCommandHandler<SetPasswordCommand, SetPasswordCommandResponse>
{
    private readonly IUserValidatorService _userValidatorService;
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IUserContextAccessor _userContextAccessor;

    public SetPasswordCommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IUserValidatorService userValidatorService,
        IUserContextAccessor userContextAccessor)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._userValidatorService = userValidatorService;
        this._userContextAccessor = userContextAccessor;
    }

    public async Task<PrimitiveResult<SetPasswordCommandResponse>> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
    {
        return await this._writeUnitOfWork.UserWriteRepository
                    .FindById(_userContextAccessor.Current.UserId, cancellationToken)
                    .MapIf(
                        user => user.Mobile.IsDefault(),
                        _ => ValueTask.FromResult(PrimitiveResult.Failure<SetPasswordCommandResponse>("", "موبایلی برای شما تعریف نشده است")),
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
                        .Map(user => new SetPasswordCommandResponse(true)))
                .ConfigureAwait(false);
    }
}
