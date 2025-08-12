using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeq.Contracts.UserContracts;

public sealed class ProductEndpoint : ApiEndpointBase
{
    const string _tag = "Product";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.Product;

    public EndpointInfo ProductModels { get; private set; }


    public ProductEndpoint()
    {
        ProductModels = new EndpointInfo(
            this.GetUrl("productModels"),
            this.GetUrl("productModels"),
            "product models",
            "لیست مدل محصولات",
            _tag);
    }
}

