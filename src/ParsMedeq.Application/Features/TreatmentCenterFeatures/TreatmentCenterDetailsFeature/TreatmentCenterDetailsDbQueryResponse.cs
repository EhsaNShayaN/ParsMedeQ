namespace ParsMedeQ.Application.Features.TreatmentCenterFeatures.TreatmentCenterDetailsFeature;

public readonly record struct TreatmentCenterDetailsDbQueryResponse(
    int Id,
    int ProvinceId,
    int CityId,
    string Title,
    string Description,
    string Image,
    DateTime CreationDate);