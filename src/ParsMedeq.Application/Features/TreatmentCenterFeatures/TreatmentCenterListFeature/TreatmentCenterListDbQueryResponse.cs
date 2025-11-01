namespace ParsMedeQ.Application.Features.TreatmentCenterFeatures.TreatmentCenterListFeature;

public readonly record struct TreatmentCenterListDbQueryResponse(
    int Id,
    int LocationId,
    string Title,
    string Description,
    string Image,
    DateTime CreationDate);