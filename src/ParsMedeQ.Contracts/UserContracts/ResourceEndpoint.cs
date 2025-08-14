using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts.UserContracts;

public sealed class ResourceEndpoint : ApiEndpointBase
{
    const string _tag = "Resource";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.Resource;

    public EndpointInfo AddResource { get; private set; }
    public EndpointInfo Resources { get; private set; }
    public EndpointInfo Resource { get; private set; }
    public EndpointInfo AddResourceCategory { get; private set; }
    public EndpointInfo ResourceCategories { get; private set; }
    public EndpointInfo ResourceCategory { get; private set; }

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

        Resource = new EndpointInfo(
           this.GetUrl("details"),
           this.GetUrl("details"),
           "Details of Resource",
           "جزئیات خبر، مقاله و ...",
           _tag);

        AddResourceCategory = new EndpointInfo(
           this.GetUrl("addCategory"),
           this.GetUrl("addCategory"),
           "Add Resource",
           "افزودن خبر، مقاله و ...",
           _tag);

        ResourceCategories = new EndpointInfo(
           this.GetUrl("listCategories"),
           this.GetUrl("listCategories"),
           "List of ResourceCategories",
           "لیست اخبار، مقاله و ...",
           _tag);

        ResourceCategory = new EndpointInfo(
           this.GetUrl("categoryDetails"),
           this.GetUrl("categoryDetails"),
           "Details of Resourcecategory",
           "جزئیات دسته بندی خبر، مقاله و ...",
           _tag);
    }
}

