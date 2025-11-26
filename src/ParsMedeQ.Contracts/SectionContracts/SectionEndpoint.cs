using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts.SectionContracts;

public sealed class SectionEndpoint : ApiEndpointBase
{
    const string _tag = "Section";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.Section;

    public EndpointInfo List { get; private set; }
    public EndpointInfo Items { get; private set; }

    public SectionEndpoint()
    {
        this.List = new EndpointInfo(
            this.GetUrl("list"),
            this.GetUrl("list"),
            "List of Sections",
            "List of Sections",
            _tag);

        this.Items = new EndpointInfo(
            this.GetUrl("items"),
            this.GetUrl("items"),
            "List of Section Items",
            "List of Section Items",
            _tag);
    }
}

