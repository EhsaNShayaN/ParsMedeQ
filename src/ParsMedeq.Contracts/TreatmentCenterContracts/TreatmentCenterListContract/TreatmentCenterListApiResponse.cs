namespace ParsMedeQ.Contracts.TreatmentCenterContracts.TreatmentCenterListContract;
public readonly record struct TreatmentCenterListApiResponse(
    int Id,
    int LocationId,
    string Title,
    string Description,
    string Image,
    string CreationDate);