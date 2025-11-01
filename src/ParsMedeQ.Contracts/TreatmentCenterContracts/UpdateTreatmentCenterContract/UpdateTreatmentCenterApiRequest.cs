using Microsoft.AspNetCore.Http;
using ParsMedeQ.Contracts.Helpers;
using System.Reflection;

namespace ParsMedeQ.Contracts.TreatmentCenterContracts.UpdateTreatmentCenterContract;
public sealed class UpdateTreatmentCenterApiRequest
{
    public int Id { get; set; }
    public int ProvinceId { get; set; }
    public int CityId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public IFormFile Image { get; private set; } = null!;
    public string OldImagePath { get; private set; } = string.Empty;
    public static async ValueTask<UpdateTreatmentCenterApiRequest?> BindAsync(HttpContext context, ParameterInfo _)
    {
        var form = await context.Request.ReadFormAsync(context.RequestAborted).ConfigureAwait(false);
        return FormBinderHelper.Bind<UpdateTreatmentCenterApiRequest>(form);
    }
}
