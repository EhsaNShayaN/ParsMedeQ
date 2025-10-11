using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts.CommentContracts;

public sealed class CommentEndpoint : ApiEndpointBase
{
    const string _tag = "Comment";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.Comment;

    public EndpointInfo List { get; private set; }
    public EndpointInfo Add { get; private set; }

    public CommentEndpoint()
    {
        List = new EndpointInfo(
           this.GetUrl("list"),
           this.GetUrl("list"),
           "list",
           "لیست سبد",
           _tag);

        Add = new EndpointInfo(
           this.GetUrl("add"),
           this.GetUrl("add"),
           "add",
           "افزودن به سبد",
           _tag);
    }
}

