using ParsMedeq.Domain.Types.Email;
using SRH.MediatRMessaging;

namespace ParsMedeq.Application.Features.UserFeatures.SigninFeature.SendPasswordOtpByEmailFeature;
public sealed record SendPasswordOtpByEmailCommand(string Email) :
    IPrimitiveResultCommand<SendPasswordOtpByEmailCommandResponse>,
    IValidatableRequest<SendPasswordOtpByEmailCommand>
{
    public ValueTask<PrimitiveResult<SendPasswordOtpByEmailCommand>> Validate()
    {
        return EmailType.Create(this.Email)
        .Map(_ => this);
    }
}
