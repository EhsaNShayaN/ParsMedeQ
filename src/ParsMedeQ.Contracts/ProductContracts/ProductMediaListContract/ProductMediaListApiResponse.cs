namespace ParsMedeQ.Contracts.ProductContracts.ProductMediaListContract;
public readonly record struct ProductMediaListApiResponse(
    int Id,
    int ProductId,
    int MediaId,
    int Ordinal,
    string Path);