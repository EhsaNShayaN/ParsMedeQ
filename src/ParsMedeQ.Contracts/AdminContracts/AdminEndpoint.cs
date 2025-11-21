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
    public EndpointInfo PeriodicServices { get; private set; }
    public EndpointInfo AddPeriodicService { get; private set; }
    public EndpointInfo DonePeriodicService { get; private set; }
    public EndpointInfo Sections { get; private set; }
    public EndpointInfo AddSection { get; private set; }

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

        this.PeriodicServices = new EndpointInfo(
            this.GetUrl("periodicService/list"),
            this.GetUrl("periodicService/list"),
            "Admin PeriodicServices",
            "Admin PeriodicServices",
            _tag);

        this.AddPeriodicService = new EndpointInfo(
            this.GetUrl("periodicService/add"),
            this.GetUrl("periodicService/add"),
            "Admin Add PeriodicService",
            "Admin Add PeriodicService",
            _tag);

        this.DonePeriodicService = new EndpointInfo(
            this.GetUrl("periodicService/done"),
            this.GetUrl("periodicService/done"),
            "Admin Done PeriodicService",
            "Admin Done PeriodicService",
            _tag);

        this.Sections = new EndpointInfo(
            this.GetUrl("section/list"),
            this.GetUrl("section/list"),
            "List of Sections",
            "List of Sections",
            _tag);

        this.AddSection = new EndpointInfo(
            this.GetUrl("section/add"),
            this.GetUrl("section/add"),
            "Add Section",
            "Add Section",
            _tag);
    }
}