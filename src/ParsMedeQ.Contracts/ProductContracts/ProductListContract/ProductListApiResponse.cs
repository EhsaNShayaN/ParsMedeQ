namespace ParsMedeQ.Contracts.ProductContracts.ProductListContract;
public readonly record struct ProductListApiResponse(
    int Id,
    int? ProductCategoryId,
    string ProductCategoryTitle,
    string Title,
    string Image,
    string Language,
    int? Price,
    int? Discount,
    int DownloadCount,
    int? Ordinal,
    bool Expired,
    string CreationDate);