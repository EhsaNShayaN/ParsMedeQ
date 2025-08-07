using SRH.PresentationApi.ApiEndpoint;

namespace EShop.Contracts.UserContracts;

public sealed class ProductEndpoint : ApiEndpointBase
{
    const string _tag = "Product";

    protected override ApiEndpointItem MyEndpoint => EShopEndpointMetadata.Product;

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

