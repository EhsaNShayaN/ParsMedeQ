using Microsoft.AspNetCore.Http;
using ParsMedeQ.Contracts.Helpers;
using System.Reflection;

namespace ParsMedeQ.Contracts.ProductContracts.AddProductContract;

public sealed class AddProductApiRequest
{
    public string Title { get; private set; } = null!;
    public string Language { get; private set; } = string.Empty;
    public int Price { get; private set; }
    public int Discount { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string PublishInfo { get; private set; } = string.Empty;
    public string Publisher { get; private set; } = string.Empty;
    public int ProductCategoryId { get; private set; }
    public string Abstract { get; private set; } = string.Empty;
    public string Anchors { get; private set; } = string.Empty;
    public string Keywords { get; private set; } = string.Empty;
    public string PublishDate { get; private set; } = string.Empty;
    public int GuarantyExpirationTime { get; private set; }
    public int PeriodicServiceInterval { get; private set; }
    public IFormFile? Image { get; private set; }
    public IFormFile? File { get; private set; }
    public IFormFile[] NewFiles { get; private set; }
    public AddProductGalleryApiRequest? Gallery { get; private set; }

    public static async ValueTask<AddProductApiRequest?> BindAsync(HttpContext context, ParameterInfo _)
    {
        var form = await context.Request.ReadFormAsync(context.RequestAborted).ConfigureAwait(false);
        return FormBinderHelper.Bind<AddProductApiRequest>(form);
    }
}
public sealed class AddProductGalleryApiRequest
{
    public AddProductExistingImageApiRequest? Existing { get; set; }
    public int[]? Deleted { get; private set; }
}
public sealed class AddProductExistingImageApiRequest
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
}