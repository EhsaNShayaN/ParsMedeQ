using Microsoft.AspNetCore.Http;
using ParsMedeQ.Contracts.Helpers;
using System.Reflection;

namespace ParsMedeQ.Contracts.ResourceContracts.AddResourceContract;

public sealed class AddResourceApiRequest
{
    public int TableId { get; private set; }
    public string Title { get; private set; }
    public string Language { get; private set; }
    public bool IsVip { get; private set; }
    public int Price { get; private set; }
    public int Discount { get; private set; }
    public string Description { get; private set; }
    public string PublishInfo { get; private set; }
    public string Publisher { get; private set; }
    public int ResourceCategoryId { get; private set; }
    public string ResourceCategoryTitle { get; private set; }
    public string Abstract { get; private set; }
    public AnchorInfo[] Anchors { get; private set; }
    public string ExpirationDate { get; private set; }
    public string ExpirationTime { get; private set; }
    public string Keywords { get; private set; }
    public string PublishDate { get; private set; }
    public IFormFile Image { get; private set; }
    public IFormFile File { get; private set; }

    public static async ValueTask<AddResourceApiRequest?> BindAsync(HttpContext context, ParameterInfo _)
    {
        var form = await context.Request.ReadFormAsync(context.RequestAborted).ConfigureAwait(false);
        return FormBinderHelper.Bind<AddResourceApiRequest>(form);
    }
}

public readonly record struct AnchorInfo
(
    string Id,
    string Name,
    string Desc
);