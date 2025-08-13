namespace ParsMedeQ.Contracts.ResourceContracts.AddResourceContracts;
public readonly record struct AddResourceApiRequest(
    int Id,
    int TableId,
    string Title,
    string Abstract,
    string Anchors,
    string Description,
    string Keywords,
    int ResourceCategoryId,
    string ResourceCategoryTitle,
    string Image,
    string MimeType,
    string Doc,
    string Language,
    string PublishDate,
    string PublishInfo,
    string Publisher,
    int? Price,
    int? Discount,
    bool IsVip,
    int DownloadCount,
    int? Ordinal,
    bool Deleted,
    bool Disabled,
    DateTime? ExpirationDate,
    DateTime CreationDate
);

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