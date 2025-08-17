namespace ParsMedeQ.Contracts.ResourceContracts.ResourceListContract;
public readonly record struct ResourceListApiResponse(
    int Id,
    int TableId,
    int? ResourceCategoryId,
    string ResourceCategoryTitle,
    string Title,
    string Image,
    string Language,
    int? Price,
    int? Discount,
    bool IsVip,
    int DownloadCount,
    int? Ordinal,
    bool Expired,
    string CreationDate);