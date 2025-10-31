namespace ParsMedeQ.Contracts.TreatmentCenterContracts.TreatmentCenterListContract;
public readonly record struct TreatmentCenterListApiResponse(
    int Id,
    int ProvinceId,
    int CityId,
    string Title,
    string Description,
    string Image,
    string CreationDate);