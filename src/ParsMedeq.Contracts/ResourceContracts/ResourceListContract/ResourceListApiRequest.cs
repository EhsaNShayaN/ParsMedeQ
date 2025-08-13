namespace ParsMedeQ.Contracts.ResourceContracts.ResourceListContract;

public record ResourceListApiRequest() : BasePaginatedApiRequest;
public record InvoiceDraftListByRequestApiRequest : BasePaginatedApiRequest
{
    public required string RequestId { get; set; }

}
public record InvoiceDraftListByFilterApiRequest : BasePaginatedApiRequest
{
    public byte? Type { get; set; }
    public byte? Pattern { get; set; }
    public byte? Subject { get; set; }
    public byte? Status { get; set; }
}