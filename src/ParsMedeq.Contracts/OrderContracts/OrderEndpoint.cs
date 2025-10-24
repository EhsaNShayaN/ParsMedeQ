using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts.OrderContracts;

public sealed class OrderEndpoint : ApiEndpointBase
{
    const string _tag = "Order";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.Order;

    public EndpointInfo Details { get; private set; }
    public EndpointInfo Orders { get; private set; }
    public EndpointInfo AddOrder { get; private set; }

    public OrderEndpoint()
    {
        Details = new EndpointInfo(
           this.GetUrl("details"),
           this.GetUrl("details"),
           "Order Details",
           "جزپیات سفارش",
           _tag);
     
        Orders = new EndpointInfo(
           this.GetUrl("list"),
           this.GetUrl("list"),
           "List of Orders",
           "لیست سفارشات",
           _tag);

        AddOrder = new EndpointInfo(
           this.GetUrl("add"),
           this.GetUrl("add"),
           "Add Order",
           "افزودن سفارش",
           _tag);
    }
}

