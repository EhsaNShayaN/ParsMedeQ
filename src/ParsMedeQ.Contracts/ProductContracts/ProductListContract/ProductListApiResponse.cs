namespace ParsMedeQ.Contracts.ProductContracts.ProductListContract;
public readonly record struct ProductListApiResponse(
    int Id,
    int? ProductCategoryId,
    string ProductCategoryTitle,
    string Title,
    string Image,
    int? Price,
    int? Discount,
    string CreationDate);