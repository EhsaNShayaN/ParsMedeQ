namespace ParsMedeQ.Contracts.SectionContracts.SectionItemsContract;
public readonly record struct SectionItemsApiResponse(
    int Id,
    int SectionId,
    string Title,
    string Description,
    string Image,
    bool Hidden);