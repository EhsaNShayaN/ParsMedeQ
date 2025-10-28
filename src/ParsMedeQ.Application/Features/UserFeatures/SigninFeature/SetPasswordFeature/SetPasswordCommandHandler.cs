using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Domain.Aggregates.UserAggregate;
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
            .FindById(_userContextAccessor.GetCurrent().UserId, cancellationToken)
            .MapIf(
            user => !string.IsNullOrWhiteSpace(user.Password.Value),
            _ => ValueTask.FromResult(PrimitiveResult.Failure<SetPasswordCommandResponse>("", "رمز عبور قبلا برای شما تعریف شده است")),
            async user => await SetPassword(request, user, cancellationToken))
            .ConfigureAwait(false);
    }

    ValueTask<PrimitiveResult<SetPasswordCommandResponse>> SetPassword(SetPasswordCommand request, User user, CancellationToken cancellationToken)
    {
        return PasswordHelper.HashAndSaltPassword(request.Password)
            .Map(generatedPass => user.UpdatePassword(
                generatedPass.HashedPassword,
                generatedPass.Salt,
                this._userValidatorService))
            .Map(u => this._writeUnitOfWork.UserWriteRepository.UpdatePassword(user, cancellationToken))
            .Map(u => this._writeUnitOfWork.SaveChangesAsync(cancellationToken))
            .Map(count => new SetPasswordCommandResponse(count > 0))
            ;
    }
}
