namespace ParsMedeQ.Contracts.TreatmentCenterContracts.TreatmentCenterDetailsContract;
public readonly record struct TreatmentCenterDetailsApiResponse(
    int Id,
    int LocationId,
    string Title,
    string Description,
    string Image,
    string CreationDate);