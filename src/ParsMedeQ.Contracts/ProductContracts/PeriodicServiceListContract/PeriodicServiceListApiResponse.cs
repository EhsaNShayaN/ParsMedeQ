namespace ParsMedeQ.Contracts.ProductContracts.PeriodicServiceListContract;
public readonly record struct PeriodicServiceListApiResponse(
    int Id,
    int UserId,
    string FullName,
    int ProductId,
    string ProductTitle,
    string ServiceDate,
    bool Done,
    bool HasNext,
    string CreationDate);