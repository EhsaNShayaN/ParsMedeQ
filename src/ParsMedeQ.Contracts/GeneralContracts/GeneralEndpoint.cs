using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts.GeneralContracts;

public sealed class GeneralEndpoint : ApiEndpointBase
{
    const string _tag = "General";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.General;

    public EndpointInfo Download { get; private set; }

    public GeneralEndpoint()
    {
        Download = new EndpointInfo(
           this.GetUrl("Download"),
           this.GetUrl("Download"),
           "Download",
           "دانلود",
           _tag);
    }
}

