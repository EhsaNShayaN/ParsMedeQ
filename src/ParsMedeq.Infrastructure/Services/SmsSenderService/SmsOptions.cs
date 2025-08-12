namespace ParsMedeq.Infrastructure.Services.SmsSenderService;

public sealed class SmsOptions
{
    public string BaseUrl { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string ApiKeyHeader { get; set; } = string.Empty;
    public int VerificationTemplateId { get; set; }
}
