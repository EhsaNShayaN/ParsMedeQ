namespace ParsMedeQ.Contracts.LocationContracts.LocationListContract;
public readonly record struct LocationListApiResponse(
    int Id,
    int? ParentId,
    string Title);