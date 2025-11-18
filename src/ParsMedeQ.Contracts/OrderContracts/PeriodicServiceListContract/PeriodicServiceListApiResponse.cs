namespace ParsMedeQ.Contracts.OrderContracts.PeriodicServiceListContract;
public readonly record struct PeriodicServiceListApiResponse(
    string Id,
    string FullName,
    int RelatedId,
    string ServiceDate,
    bool Done,
    bool HasNext,
    string GuarantyExpirationDate);