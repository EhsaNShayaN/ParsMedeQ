namespace ParsMedeQ.Contracts.TreatmentCenterContracts.TreatmentCenterListContract;
public record TreatmentCenterListApiRequest(
    string Query,
    int ProvinceId,
    int CityId) : BasePaginatedApiRequest;