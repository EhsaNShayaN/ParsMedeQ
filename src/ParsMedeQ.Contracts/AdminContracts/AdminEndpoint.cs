using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts.AdminContracts;

public sealed class AdminEndpoint : ApiEndpointBase
{
    const string _tag = "Admin";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.Admin;

    public EndpointInfo Comments { get; private set; }
    public EndpointInfo Tickets { get; private set; }

    public EndpointInfo Orders { get; private set; }
    public EndpointInfo Payments { get; private set; }


    public AdminEndpoint()
    {
        this.Comments = new EndpointInfo(
    this.GetUrl("comment/list"),
    this.GetUrl("comment/list"),
    "Admin Comments",
    "Admin Comments",
    _tag);

        this.Tickets = new EndpointInfo(
    this.GetUrl("ticket/list"),
    this.GetUrl("ticket/list"),
    "Admin Tickets",
    "Admin Tickets",
    _tag);

        this.Orders = new EndpointInfo(
            this.GetUrl("order/list"),
            this.GetUrl("order/list"),
            "Admin Orders",
            "Admin Orders",
            _tag);

        this.Payments = new EndpointInfo(
            this.GetUrl("payment/list"),
            this.GetUrl("payment/list"),
            "Admin Payments",
            "Admin Payments",
            _tag);
    }
}

