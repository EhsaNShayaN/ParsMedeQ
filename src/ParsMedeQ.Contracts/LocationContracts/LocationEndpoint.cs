using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts.LocationContracts;

public sealed class LocationEndpoint : ApiEndpointBase
{
    const string _tag = "Location";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.Location;

    public EndpointInfo Locations { get; private set; }

    public LocationEndpoint()
    {
        Locations = new EndpointInfo(
           this.GetUrl("list"),
           this.GetUrl("list"),
           "List of Locations",
           "لیست استان و شهر",
           _tag);
    }
}

