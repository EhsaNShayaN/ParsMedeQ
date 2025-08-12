using ParsMedeq.Application.Services.SmsSenderService;
using ParsMedeq.Infrastructure.Helpers;
using Microsoft.Extensions.Options;

namespace ParsMedeq.Infrastructure.Services.SmsSenderService;

public sealed class SmsSenderService : ISmsSenderService
{
    public const string HttpClientName = "Sms.ir";
    public const string ApiKey = "olcVeeP7NzWK1xix5KZB5hKzbJc2rtHYildayNiIqMdA7zNPdszH2dhbjHP9PAFU";
    const int VerificationTemplateId = 124778;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptionsSnapshot<SmsOptions> _opts;

    public SmsSenderService(IHttpClientFactory httpClientFactory, IOptionsSnapshot<SmsOptions> opts)
    {
        this._httpClientFactory = httpClientFactory;
        this._opts = opts;
    }

    public ValueTask<PrimitiveResult> Send(string to, string message, CancellationToken cancellationToken) =>
        ValueTask.FromResult(PrimitiveResult.Success());

    public ValueTask<PrimitiveResult> SendVerificationCode(string to, string message, CancellationToken cancellationToken)
    {
        Task.Run(() => HttpProxyHelper.PostRequest<VerifySmsRequest, SmsBaseResponse<VerifyResponse>>(
                () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
                "v1/send/verify",
                new VerifySmsRequest(to, this._opts.Value.VerificationTemplateId, [new VerifySMSParameter("code", message)]),
                cancellationToken), cancellationToken);

        return ValueTask.FromResult(PrimitiveResult.Success());
    }

}
