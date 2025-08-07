using EShop.Application;
using EShop.Application.Services.SmsSenderService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;

namespace EShop.Infrastructure.Services.SmsSenderService;
public sealed class MoqSmsSenderService : ISmsSenderService
{
    private readonly ISmsSenderService _mainService;
    private readonly IFeatureManager _featureManager;

    public MoqSmsSenderService(ISmsSenderService mainService, IFeatureManager featureManager)
    {
        this._mainService = mainService;
        this._featureManager = featureManager;
    }

    public async ValueTask<PrimitiveResult> Send(string to, string message, CancellationToken cancellationToken)
    {
        if (await this._featureManager.IsEnabledAsync(ApplicationFeatureFlags.DummyOTP))
        {
            return await ValueTask.FromResult(PrimitiveResult.Success());
        }
        return await this._mainService.Send(to, message, cancellationToken);
    }

    public async ValueTask<PrimitiveResult> SendVerificationCode(string to, string message, CancellationToken cancellationToken)
    {
        if (await this._featureManager.IsEnabledAsync(ApplicationFeatureFlags.DummyOTP))
        {
            return await ValueTask.FromResult(PrimitiveResult.Success());
        }
        return await this._mainService.SendVerificationCode(to, message, cancellationToken);
    }
}
public sealed record SmsBaseResponse<T>(T Data, int Status, string Message);
public sealed record VerifyResponse(int MessageId, decimal Cost);
public sealed record VerifySmsRequest(string Mobile, int TemplateId, VerifySMSParameter[] Parameters);
public sealed record VerifySMSParameter(string Name, string Value);
public sealed class SmsLoggerDelegatingHandler : DelegatingHandler
{
    private readonly ILogger<SmsLoggerDelegatingHandler> _logger;

    public SmsLoggerDelegatingHandler(ILogger<SmsLoggerDelegatingHandler> logger)
    {
        this._logger = logger;
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            this._logger.LogInformation($"Sending sms ....");


            var result = await base.SendAsync(request, cancellationToken);

            this._logger.LogInformation($"Sms sent....");

            return result;
        }
        catch (Exception ex)
        {
            this._logger.LogError(ex, $"Sms sending error!");
            throw;
        }
    }
}
public sealed class SmsApiKeyDelegatingHandler : DelegatingHandler
{
    private readonly IOptionsSnapshot<SmsOptions> _opts;

    public SmsApiKeyDelegatingHandler(IOptionsSnapshot<SmsOptions> opts)
    {
        this._opts = opts;
    }
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Remove(this._opts.Value.ApiKeyHeader);
        request.Headers.TryAddWithoutValidation(this._opts.Value.ApiKeyHeader, this._opts.Value.ApiKey);

        return base.SendAsync(request, cancellationToken);
    }
}