namespace ParsMedeq.Application.Services.EmailSenderService;
public interface IEmailSenderService
{
    ValueTask<PrimitiveResult> Send(string to, string title, string body, bool isHtml, CancellationToken cancellationToken);
}
