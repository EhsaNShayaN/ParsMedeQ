using ParsMedeq.Application.Services.EmailSenderService;
using FluentEmail.Core;
using SRH.PrimitiveTypes.Result;

namespace ParsMedeq.Infrastructure.Services.EmailSenderService;

public sealed class EmailSenderService : IEmailSenderService
{
    private readonly IFluentEmail _fluentEmail;

    public EmailSenderService(IFluentEmail fluentEmail)
    {
        this._fluentEmail = fluentEmail;
    }

    public ValueTask<PrimitiveResult> Send(string to, string title, string body, bool isHtml, CancellationToken cancellationToken)
    {
        var sendResponse = this._fluentEmail
            .To(to)
            //.CC("shayan.e@dpi.ir")
            .Subject(title)
            .Body(body, isHtml)
            .Send();
        return
            sendResponse.Successful ?
            ValueTask.FromResult(PrimitiveResult.Success()) :
            ValueTask.FromResult(PrimitiveResult.Failure("", "خطا در ارسال ایمیل"));
    }
}