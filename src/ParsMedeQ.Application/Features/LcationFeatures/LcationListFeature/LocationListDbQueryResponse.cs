namespace ParsMedeQ.Application.Features.LcationFeatures.LcationListFeature;

public readonly record struct LocationListDbQueryResponse(
    int Id,
    int? ParentId,
    string Title);