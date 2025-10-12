using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts.CommentContracts;

public sealed class CommentEndpoint : ApiEndpointBase
{
    const string _tag = "Comment";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.Comment;

    public EndpointInfo Comments { get; private set; }
    public EndpointInfo AddComment { get; private set; }

    public CommentEndpoint()
    {
        Comments = new EndpointInfo(
           this.GetUrl("list"),
           this.GetUrl("list"),
           "List of Comments",
           "لیست نظرات",
           _tag);

        AddComment = new EndpointInfo(
           this.GetUrl("add"),
           this.GetUrl("add"),
           "Add Comment",
           "افزودن نظر",
           _tag);
    }
}

