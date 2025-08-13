using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts.UserContracts;

public sealed class ResourceEndpoint : ApiEndpointBase
{
    const string _tag = "Resource";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.Resource;

    public EndpointInfo AddResource { get; private set; }
    public EndpointInfo Resources { get; private set; }

    public ResourceEndpoint()
    {
        AddResource = new EndpointInfo(
           this.GetUrl("add"),
           this.GetUrl("add"),
           "Add Resource",
           "افزودن خبر، مقاله و ...",
           _tag);

        Resources = new EndpointInfo(
           this.GetUrl("list"),
           this.GetUrl("list"),
           "List of Resources",
           "لیست اخبار، مقاله و ...",
           _tag);
    }
}

