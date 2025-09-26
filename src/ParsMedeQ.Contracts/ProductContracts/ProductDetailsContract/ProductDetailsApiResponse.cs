namespace ParsMedeQ.Contracts.ProductContracts.ProductDetailsContract;
public readonly record struct ProductDetailsApiResponse(
    int Id,
    int? ProductCategoryId,
    string ProductCategoryTitle,
    string Title,
    string Description,
    string Image,
    int? FileId,
    int? Price,
    int? Discount,
    bool Deleted,
    bool Disabled,
    string CreationDate,
    bool Registered);