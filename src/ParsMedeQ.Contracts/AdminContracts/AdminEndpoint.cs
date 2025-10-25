using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts.AdminContracts;

public sealed class AdminEndpoint : ApiEndpointBase
{
    const string _tag = "Admin";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.Admin;

    public EndpointInfo Comments { get; private set; }


    public AdminEndpoint()
    {
        this.Comments = new EndpointInfo(
    this.GetUrl("comment/list"),
    this.GetUrl("comment/list"),
    "Admin Comments",
    "Admin Comments",
    _tag);
    }
}

