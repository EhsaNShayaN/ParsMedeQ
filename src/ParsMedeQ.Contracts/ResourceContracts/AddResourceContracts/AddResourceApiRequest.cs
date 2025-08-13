namespace ParsMedeQ.Contracts.ResourceContracts.AddResourceContracts;
public readonly record struct AddResourceApiRequest
(
    int? Id,
    int TableId,
    string Title,
    string Image,
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
    string Doc);
/*public readonly record struct IdTitleRequest(
    int Id,
    string Title
);*/

public readonly record struct AnchorInfo
(
    string Id,
    string Name,
    string Desc
);