using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts.TreatmentCenterContracts;

public sealed class TreatmentCenterEndpoint : ApiEndpointBase
{
    const string _tag = "TreatmentCenter";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.TreatmentCenter;

    public EndpointInfo AddTreatmentCenter { get; private set; }
    public EndpointInfo EditTreatmentCenter { get; private set; }
    public EndpointInfo DeleteTreatmentCenter { get; private set; }
    public EndpointInfo TreatmentCenters { get; private set; }
    public EndpointInfo TreatmentCenter { get; private set; }

    public TreatmentCenterEndpoint()
    {
        AddTreatmentCenter = new EndpointInfo(
           this.GetUrl("add"),
           this.GetUrl("add"),
           "Add Treatment Center",
           "افزودن سانتر درمانی...",
           _tag);

        EditTreatmentCenter = new EndpointInfo(
           this.GetUrl("edit"),
           this.GetUrl("edit"),
           "Edit Treatment Center",
           "ویرایش سانتر درمانی...",
           _tag);

        DeleteTreatmentCenter = new EndpointInfo(
           this.GetUrl("delete"),
           this.GetUrl("delete"),
           "Delete Treatment Center",
           "حذف سانتر درمانی...",
           _tag);

        TreatmentCenters = new EndpointInfo(
           this.GetUrl("list"),
           this.GetUrl("list"),
           "List of TreatmentCenters",
           "لیست سانترهای درمانی...",
           _tag);

        TreatmentCenter = new EndpointInfo(
           this.GetUrl("details"),
           this.GetUrl("details"),
           "Details of TreatmentCenter",
           "جزئیات سانتر درمانی...",
           _tag);
    }
}

