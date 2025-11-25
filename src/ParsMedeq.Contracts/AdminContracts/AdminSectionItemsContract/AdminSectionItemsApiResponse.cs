namespace ParsMedeQ.Contracts.AdminContracts.AdminSectionItemsContract;
public readonly record struct AdminSectionItemsApiResponse(
    int Id,
    int SectionId,
    string Title,
    string Description,
    string Image,
    bool Hidden);