namespace ParsMedeQ.Contracts.AdminContracts.AdminUpdateSectionByListContract;

public readonly record struct AdminUpdateSectionByListApiRequest(
    int Id,
    AdminUpdateSectionByListItemApiRequest[] Items);

public readonly record struct AdminUpdateSectionByListItemApiRequest(
    string Title,
    string Description);