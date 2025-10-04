using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts.ProductContracts;

public sealed class ProductEndpoint : ApiEndpointBase
{
    const string _tag = "Product";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.Product;

    public EndpointInfo AddProduct { get; private set; }
    public EndpointInfo EditProduct { get; private set; }
    public EndpointInfo Products { get; private set; }
    public EndpointInfo Product { get; private set; }
    public EndpointInfo AddProductCategory { get; private set; }
    public EndpointInfo EditProductCategory { get; private set; }
    public EndpointInfo ProductCategories { get; private set; }
    public EndpointInfo ProductCategory { get; private set; }
    public EndpointInfo ProductMediaList { get; private set; }
    public EndpointInfo AddProductMedia { get; private set; }
    public EndpointInfo DeleteProductMedia { get; private set; }

    public ProductEndpoint()
    {
        AddProduct = new EndpointInfo(
           this.GetUrl("add"),
           this.GetUrl("add"),
           "Add Product",
           "افزودن خبر، مقاله و ...",
           _tag);

        EditProduct = new EndpointInfo(
           this.GetUrl("edit"),
           this.GetUrl("edit"),
           "Edit Product",
           "ویرایش خبر، مقاله و ...",
           _tag);

        Products = new EndpointInfo(
           this.GetUrl("list"),
           this.GetUrl("list"),
           "List of Products",
           "لیست اخبار، مقاله و ...",
           _tag);

        Product = new EndpointInfo(
           this.GetUrl("details"),
           this.GetUrl("details"),
           "Details of Product",
           "جزئیات خبر، مقاله و ...",
           _tag);

        AddProductCategory = new EndpointInfo(
           this.GetUrl("category/add"),
           this.GetUrl("category/add"),
           "Add Product Category",
           "افزودن دسته بندی خبر، مقاله و ...",
           _tag);

        EditProductCategory = new EndpointInfo(
           this.GetUrl("category/edit"),
           this.GetUrl("category/edit"),
           "Edit Product Category",
           "ویرایش دسته بندی خبر، مقاله و ...",
           _tag);

        ProductCategories = new EndpointInfo(
           this.GetUrl("category/list"),
           this.GetUrl("category/list"),
           "List of ProductCategories",
           "لیست اخبار، مقاله و ...",
           _tag);

        ProductCategory = new EndpointInfo(
           this.GetUrl("category/details"),
           this.GetUrl("category/details"),
           "Details of Productcategory",
           "جزئیات دسته بندی خبر، مقاله و ...",
           _tag);

        ProductMediaList = new EndpointInfo(
           this.GetUrl("media/list"),
           this.GetUrl("media/list"),
           "ProductMediaList",
           "ProductMediaList",
           _tag);

        AddProductMedia = new EndpointInfo(
           this.GetUrl("media/add"),
           this.GetUrl("media/add"),
           "AddProductMedia",
           "AddProductMedia",
           _tag);

        DeleteProductMedia = new EndpointInfo(
           this.GetUrl("media/delete"),
           this.GetUrl("media/delete"),
           "DeleteProductMedia",
           "DeleteProductMedia",
           _tag);
    }
}

