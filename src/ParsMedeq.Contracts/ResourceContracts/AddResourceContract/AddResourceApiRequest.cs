using Microsoft.AspNetCore.Http;
using ParsMedeQ.Contracts.Helpers;
using System.Reflection;

namespace ParsMedeQ.Contracts.ResourceContracts.AddResourceContract;

public readonly record struct AddResourceApiRequest
(
    int TableId,
    string Title,
    string MimeType,
    string Language,
    bool IsVip,
    int Price,
    int Discount,
    string Description,
    string PublishInfo,
    string Publisher,
    int ResourceCategoryId,
    string ResourceCategoryTitle,
    string Abstract,
    AnchorInfo[] Anchors,
    string ExpirationDate,
    string ExpirationTime,
    string Keywords,
    string PublishDate,
    int[] Categories,
    IFormFile Image,
    IFormFile File)
{
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