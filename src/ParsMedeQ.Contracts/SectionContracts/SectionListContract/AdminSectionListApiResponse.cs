namespace ParsMedeQ.Contracts.SectionContracts.AdminSectionListContract;
public readonly record struct AdminSectionListApiResponse(
    int Id,
    int SectionId,
    string Title,
    string Description,
    string Image,
    bool Hidden);