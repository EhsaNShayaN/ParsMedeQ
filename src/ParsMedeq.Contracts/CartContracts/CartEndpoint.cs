using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts.CartContracts;

public sealed class CartEndpoint : ApiEndpointBase
{
    const string _tag = "Cart";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.Cart;

    public EndpointInfo Carts { get; private set; }
    public EndpointInfo AddCart { get; private set; }
    public EndpointInfo MergeCarts { get; private set; }
    public EndpointInfo RemoveCart { get; private set; }

    public CartEndpoint()
    {
        Carts = new EndpointInfo(
           this.GetUrl("list"),
           this.GetUrl("list"),
           "List of Carts",
           "لیست سبد",
           _tag);

        RemoveCart = new EndpointInfo(
           this.GetUrl("remove"),
           this.GetUrl("remove"),
           "Remove Cart",
           "حذف از سبد",
           _tag);

        AddCart = new EndpointInfo(
           this.GetUrl("add"),
           this.GetUrl("add"),
           "Add to cart",
           "افزودن به سبد",
           _tag);

        MergeCarts = new EndpointInfo(
           this.GetUrl("merge"),
           this.GetUrl("merge"),
           "Merge Carts",
           "ادغام سبد",
           _tag);
    }
}

