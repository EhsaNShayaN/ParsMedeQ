using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts.PaymentContracts;

public sealed class PaymentEndpoint : ApiEndpointBase
{
    const string _tag = "Payment";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.Payment;

    public EndpointInfo Payments { get; private set; }
    public EndpointInfo AddPayment { get; private set; }
    public EndpointInfo ConfirmPayment { get; private set; }
    public EndpointInfo FailPayment { get; private set; }

    public PaymentEndpoint()
    {
        Payments = new EndpointInfo(
           this.GetUrl("list"),
           this.GetUrl("list"),
           "List of Payments",
           "لیست پرداخت ها",
           _tag);

        AddPayment = new EndpointInfo(
           this.GetUrl("add"),
           this.GetUrl("add"),
           "Add Payment",
           "افزودن پرداخت",
           _tag);

        ConfirmPayment = new EndpointInfo(
           this.GetUrl("confirm"),
           this.GetUrl("confirm"),
           "Confirm Payment",
           "تائید پرداخت",
           _tag);

        FailPayment = new EndpointInfo(
           this.GetUrl("fail"),
           this.GetUrl("fail"),
           "Fail Payment",
           "رد پرداخت",
           _tag);
    }
}

