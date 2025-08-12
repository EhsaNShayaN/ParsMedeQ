namespace ParsMedeq.Application.Services.SmsSenderService;
public interface ISmsSenderService
{
    ValueTask<PrimitiveResult> Send(string to, string message, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult> SendVerificationCode(string to, string message, CancellationToken cancellationToken);
}
