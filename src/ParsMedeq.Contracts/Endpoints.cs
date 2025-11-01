using ParsMedeQ.Contracts.AdminContracts;
using ParsMedeQ.Contracts.CartContracts;
using ParsMedeQ.Contracts.CommentContracts;
using ParsMedeQ.Contracts.GeneralContracts;
using ParsMedeQ.Contracts.LocationContracts;
using ParsMedeQ.Contracts.OrderContracts;
using ParsMedeQ.Contracts.PaymentContracts;
using ParsMedeQ.Contracts.ProductContracts;
using ParsMedeQ.Contracts.ResourceContracts;
using ParsMedeQ.Contracts.TicketContracts;
using ParsMedeQ.Contracts.TreatmentCenterContracts;
using ParsMedeQ.Contracts.UserContracts;
using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts;

public static class Endpoints
{
    public readonly static AdminEndpoint Admin = new AdminEndpoint();
    public readonly static UserEndpoint User = new();
    public readonly static ResourceEndpoint Resource = new();
    public readonly static ProductEndpoint Product = new();
    public readonly static GeneralEndpoint General = new();
    public readonly static CartEndpoint Cart = new();
    public readonly static CommentEndpoint Comment = new();
    public readonly static TicketEndpoint Ticket = new();
    public readonly static OrderEndpoint Order = new();
    public readonly static PaymentEndpoint Payment = new();
    public readonly static TreatmentCenterEndpoint TreatmentCenter = new();
    public readonly static LocationEndpoint Location = new();
}
public static class EndpointMetadata
{
    public readonly static ApiEndpointItem Api = new("api", null);
    public readonly static ApiEndpointItem V1 = new("v1", Api);

    public readonly static ApiEndpointItem Admin = new("admin", V1);
    public readonly static ApiEndpointItem User = new("user", V1);
    public readonly static ApiEndpointItem Resource = new("resource", V1);
    public readonly static ApiEndpointItem Product = new("product", V1);
    public readonly static ApiEndpointItem General = new("general", V1);
    public readonly static ApiEndpointItem Cart = new("cart", V1);
    public readonly static ApiEndpointItem Comment = new("comment", V1);
    public readonly static ApiEndpointItem Order = new("order", V1);
    public readonly static ApiEndpointItem Payment = new("payment", V1);
    public readonly static ApiEndpointItem Ticket = new("ticket", V1);
    public readonly static ApiEndpointItem TreatmentCenter = new("treatmentCenter", V1);
    public readonly static ApiEndpointItem Location = new("location", V1);
}