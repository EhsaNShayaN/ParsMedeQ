namespace ParsMedeQ.Application.Features.TreatmentCenterFeatures.TreatmentCenterDetailsFeature;

public readonly record struct TreatmentCenterDetailsDbQueryResponse(
    int Id,
    int LocationId,
    string Title,
    string Description,
    string Image,
    DateTime CreationDate);