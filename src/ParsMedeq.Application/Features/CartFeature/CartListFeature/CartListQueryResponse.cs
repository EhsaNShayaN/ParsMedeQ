namespace ParsMedeQ.Application.Features.CartFeature.GetCartFeature;
public sealed record CartListQueryResponse(
    int Id,
    int? UserId,
    Guid? AnonymousId,
    GetCartItemQueryResponse[] CartItems);
public sealed record GetCartItemQueryResponse(
int TableId,
int RelatedId,
string RelatedName,
decimal UnitPrice,
int Quantity);
