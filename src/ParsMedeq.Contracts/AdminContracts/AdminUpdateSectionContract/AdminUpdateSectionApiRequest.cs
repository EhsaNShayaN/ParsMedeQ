using Microsoft.AspNetCore.Http;
using ParsMedeQ.Contracts.Helpers;
using System.Reflection;

namespace ParsMedeQ.Contracts.AdminContracts.AdminUpdateSectionContract;

public sealed class AdminUpdateSectionApiRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
    public string OldImagePath { get; set; } = string.Empty;
    public static async ValueTask<AdminUpdateSectionApiRequest?> BindAsync(HttpContext context, ParameterInfo _)
    {
        var form = await context.Request.ReadFormAsync(context.RequestAborted).ConfigureAwait(false);
        return FormBinderHelper.Bind<AdminUpdateSectionApiRequest>(form);
    }
}