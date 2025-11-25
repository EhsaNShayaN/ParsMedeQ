namespace ParsMedeQ.Contracts.AdminContracts.AdminSectionListContract;
public readonly record struct AdminSectionListApiResponse(
    int Id,
    string Title,
    string Description,
    string Image,
    bool Hidden);