using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts.TicketContracts;

public sealed class TicketEndpoint : ApiEndpointBase
{
    const string _tag = "Ticket";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.Ticket;

    public EndpointInfo Tickets { get; private set; }
    public EndpointInfo AddTicket { get; private set; }

    public TicketEndpoint()
    {
        Tickets = new EndpointInfo(
           this.GetUrl("list"),
           this.GetUrl("list"),
           "List of Tickets",
           "لیست نظرات",
           _tag);

        AddTicket = new EndpointInfo(
           this.GetUrl("add"),
           this.GetUrl("add"),
           "Add Ticket",
           "افزودن نظر",
           _tag);
    }
}

