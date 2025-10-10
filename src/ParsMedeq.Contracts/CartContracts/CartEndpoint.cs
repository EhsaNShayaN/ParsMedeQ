using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts.CartContracts;

public sealed class CartEndpoint : ApiEndpointBase
{
    const string _tag = "Cart";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.Cart;

    public EndpointInfo List { get; private set; }
    public EndpointInfo Add { get; private set; }
    public EndpointInfo Merge { get; private set; }
    public EndpointInfo Remove { get; private set; }

    public CartEndpoint()
    {
        List = new EndpointInfo(
           this.GetUrl("list"),
           this.GetUrl("list"),
           "list",
           "لیست سبد",
           _tag);
        
        Remove = new EndpointInfo(
           this.GetUrl("remove"),
           this.GetUrl("remove"),
           "remove",
           "حذف از سبد",
           _tag);

        Add = new EndpointInfo(
           this.GetUrl("add"),
           this.GetUrl("add"),
           "add",
           "افزودن به سبد",
           _tag);

        Merge = new EndpointInfo(
           this.GetUrl("merge"),
           this.GetUrl("merge"),
           "merge",
           "ادغام سبد",
           _tag);
    }
}

