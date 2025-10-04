using Microsoft.AspNetCore.Http;
using ParsMedeQ.Contracts.Helpers;
using System.Reflection;

namespace ParsMedeQ.Contracts.ProductContracts.AddProductMediaContract;
public sealed class AddProductMediaApiRequest
{
    public IFormFile[] Files { get; private set; }
    public static async ValueTask<AddProductMediaApiRequest?> BindAsync(HttpContext context, ParameterInfo _)
    {
        var form = await context.Request.ReadFormAsync(context.RequestAborted).ConfigureAwait(false);
        return FormBinderHelper.Bind<AddProductMediaApiRequest>(form);
    }
}
