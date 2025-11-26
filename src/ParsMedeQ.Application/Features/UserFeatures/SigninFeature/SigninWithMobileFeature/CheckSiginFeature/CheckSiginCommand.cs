namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.CheckSiginFeature;
public sealed record CheckSiginCommand(string Mobile) :
    IPrimitiveResultCommand<CheckSiginCommandResponse>,
    IValidatableRequest<CheckSiginCommand>
{
    public ValueTask<PrimitiveResult<CheckSiginCommand>> Validate()
    {
        return MobileType.Create(this.Mobile)
        .Map(_ => this);
    }
}
