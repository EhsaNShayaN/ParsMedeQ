using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Domain.Aggregates.UserAggregate.Validators;
using ParsMedeQ.Domain.Helpers;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.ChangePasswordFeature;
public sealed class ChangePasswordCommandHandler : IPrimitiveResultCommandHandler<ChangePasswordCommand, ChangePasswordCommandResponse>
{
    private readonly IUserValidatorService _userValidatorService;
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IUserContextAccessor _userContextAccessor;

    public ChangePasswordCommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IUserValidatorService userValidatorService,
        IUserContextAccessor userContextAccessor)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._userValidatorService = userValidatorService;
        this._userContextAccessor = userContextAccessor;
    }

    public async Task<PrimitiveResult<ChangePasswordCommandResponse>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        return await this._writeUnitOfWork.UserWriteRepository
            .FindById(_userContextAccessor.GetCurrent().UserId, cancellationToken)
            .Ensure(user => PasswordHelper.VerifyPassword(request.OldPassword, user.Password.Value, user.Password.Salt).IsSuccess,
            PrimitiveError.Create("", "رمز عبور قبلی اشتباه است"))
            .Map(user => PasswordHelper.HashAndSaltPassword(request.Password)
                .Map(generatedPass => (User: user, Pass: generatedPass)))
            .Map(data => this._writeUnitOfWork.UserWriteRepository.FindById(data.User.Id, cancellationToken)
                .Bind(user => user.UpdatePassword(data.Pass.HashedPassword, data.Pass.Salt, this._userValidatorService)))
                .Bind(user => this._writeUnitOfWork.UserWriteRepository.UpdatePassword(user, cancellationToken))
                .Bind(user => this._writeUnitOfWork.SaveChangesAsync(cancellationToken).Map(_ => user))
                .Map(user => new ChangePasswordCommandResponse(true))
                .ConfigureAwait(false);
    }
}
