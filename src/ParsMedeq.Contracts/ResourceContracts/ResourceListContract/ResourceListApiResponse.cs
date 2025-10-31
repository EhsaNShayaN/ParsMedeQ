namespace ParsMedeQ.Contracts.ResourceContracts.ResourceListContract;
public readonly record struct ResourceListApiResponse(
    int Id,
    int TableId,
    int? ResourceCategoryId,
    string ResourceCategoryTitle,
    string Title,
    //////////////////////////
    string Keywords,
    string Abstract,
    string Description,
    //////////////////////////
    string Image,
    int? FileId,
    string Language,
    int Price,
    int Discount,
    int DownloadCount,
    int? Ordinal,
    bool Expired,
    string CreationDate);