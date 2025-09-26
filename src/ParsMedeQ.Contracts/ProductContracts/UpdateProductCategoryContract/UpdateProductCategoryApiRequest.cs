using Microsoft.AspNetCore.Http;
using ParsMedeQ.Contracts.Helpers;
using ParsMedeQ.Contracts.ResourceContracts.UpdateResourceContract;
using System.Reflection;

namespace ParsMedeQ.Contracts.ProductContracts.UpdateProductCategoryContract;
public sealed class UpdateProductCategoryApiRequest
{
    public int Id { get; set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int? ParentId { get; private set; }
    public IFormFile? Image { get; private set; }
    public string OldImagePath { get; private set; } = string.Empty;
    public static async ValueTask<UpdateResourceApiRequest?> BindAsync(HttpContext context, ParameterInfo _)
    {
        var form = await context.Request.ReadFormAsync(context.RequestAborted).ConfigureAwait(false);
        return FormBinderHelper.Bind<UpdateResourceApiRequest>(form);
    }
}
