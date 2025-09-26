using Microsoft.AspNetCore.Http;
using ParsMedeQ.Contracts.Helpers;
using System.Reflection;

namespace ParsMedeQ.Contracts.ProductContracts.AddProductCategoryContract;
public sealed class AddProductCategoryApiRequest
{
    public int TableId { get; set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int? ParentId { get; private set; }
    public IFormFile? Image { get; private set; }
    public static async ValueTask<AddProductCategoryApiRequest?> BindAsync(HttpContext context, ParameterInfo _)
    {
        var form = await context.Request.ReadFormAsync(context.RequestAborted).ConfigureAwait(false);
        return FormBinderHelper.Bind<AddProductCategoryApiRequest>(form);
    }
}
