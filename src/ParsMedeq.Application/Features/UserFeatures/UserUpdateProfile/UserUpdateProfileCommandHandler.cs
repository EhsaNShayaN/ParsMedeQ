using ParsMedeQ.Domain.Aggregates.UserAggregate.Validators;
using ParsMedeQ.Domain.Types.Email;
using ParsMedeQ.Domain.Types.FirstName;
using ParsMedeQ.Domain.Types.FullName;
using ParsMedeQ.Domain.Types.LastName;

namespace ParsMedeQ.Application.Features.UserFeatures.UserUpdateProfile;
public sealed class UserUpdateProfileCommandHandler : IPrimitiveResultCommandHandler<UserUpdateProfileCommand, UserUpdateProfileCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IUserValidatorService _userValidatorService;

    public UserUpdateProfileCommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IUserValidatorService userValidatorService)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._userValidatorService = userValidatorService;
    }

    public async Task<PrimitiveResult<UserUpdateProfileCommandResponse>> Handle(UserUpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var result = await ContextualResult<UserUpdateProfileContext>.Create(
            new UserUpdateProfileContext(request, cancellationToken))
            .Execute(GetUserInfo)
            .Execute(SetFirstName)
            .Execute(SetLastName)
            .Execute(SetFullName)
            .Execute(SetNationalCode)
            .Execute(SetEmail)
            .Execute(UpdateUser)
            .Execute(UpdateUserInDatabase)
            .Map(ctx => ctx.UserInfo)
            .Map(user => new UserUpdateProfileCommandResponse(user is not null))
            .ConfigureAwait(false);
        return result;
    }
    ValueTask<PrimitiveResult<UserUpdateProfileContext>> GetUserInfo(UserUpdateProfileContext ctx)
    {
        return this._writeUnitOfWork
            .UserWriteRepository
            .FindById(ctx.Request.UserId, ctx.CancellationToken)
            .Map(ctx.SetUserInfo);
    }
    ValueTask<PrimitiveResult<UserUpdateProfileContext>> SetFirstName(UserUpdateProfileContext ctx)
    {
        return
            FirstNameType.Create(ctx.Request.FirstName)
            .Map(ctx.SetFirstName);
    }
    ValueTask<PrimitiveResult<UserUpdateProfileContext>> SetLastName(UserUpdateProfileContext ctx)
    {
        return
            LastNameType.Create(ctx.Request.LastName)
            .Map(ctx.SetLastName);
    }
    ValueTask<PrimitiveResult<UserUpdateProfileContext>> SetFullName(UserUpdateProfileContext ctx)
    {
        return
            FullNameType.Create(ctx.FirstName.Value, ctx.LastName.Value)
            .Map(ctx.SetFullName);
    }
    ValueTask<PrimitiveResult<UserUpdateProfileContext>> SetNationalCode(UserUpdateProfileContext ctx)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(ctx.SetNationalCode(ctx.Request.NationalCode)));
    }
    ValueTask<PrimitiveResult<UserUpdateProfileContext>> SetEmail(UserUpdateProfileContext ctx)
    {
        return
            EmailType.Create(ctx.Request.Email)
            .Map(ctx.SetEmail);
    }
    ValueTask<PrimitiveResult<UserUpdateProfileContext>> UpdateUser(UserUpdateProfileContext ctx)
    {
        return ctx.UserInfo.UpdateProfile(
            ctx.FirstName,
            ctx.LastName,
            ctx.Email,
            ctx.NationalCode,
            this._userValidatorService,
            ctx.CancellationToken)
            .Map(ctx.SetUserInfo);
    }
    ValueTask<PrimitiveResult<UserUpdateProfileContext>> UpdateUserInDatabase(UserUpdateProfileContext ctx)
    {
        return this._writeUnitOfWork
            .UserWriteRepository
            .UpdateUser(ctx.UserInfo)
            .Map(_ => this._writeUnitOfWork.SaveChangesAsync(ctx.CancellationToken).Map(_ => ctx));

    }
}