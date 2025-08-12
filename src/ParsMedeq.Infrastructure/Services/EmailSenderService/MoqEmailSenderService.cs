using ParsMedeq.Application;
using ParsMedeq.Application.Services.EmailSenderService;
using Microsoft.FeatureManagement;
using SRH.PrimitiveTypes.Result;

namespace ParsMedeq.Infrastructure.Services.EmailSenderService;
public sealed class MoqEmailSenderService : IEmailSenderService
{
    private readonly IEmailSenderService _mainService;
    private readonly IFeatureManager _featureManager;

    public MoqEmailSenderService(IEmailSenderService mainService, IFeatureManager featureManager)
    {
        this._mainService = mainService;
        this._featureManager = featureManager;
    }

    public async ValueTask<PrimitiveResult> Send(string to, string title, string body, bool isHtml, CancellationToken cancellationToken)
    {
        if (await this._featureManager.IsEnabledAsync(ApplicationFeatureFlags.DummyEmail))
        {
            return await ValueTask.FromResult(PrimitiveResult.Success());
        }
        return await this._mainService.Send(to, title, body, isHtml, cancellationToken);
    }
}
