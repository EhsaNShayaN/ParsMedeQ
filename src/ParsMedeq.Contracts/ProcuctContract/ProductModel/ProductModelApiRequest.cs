namespace ParsMedeq.Contracts.ProcuctContract.ProductModel;

public record ProductModelApiRequest() : BasePaginatedApiRequest;
public record ProductModelByRequestApiRequest : BasePaginatedApiRequest
{
    public required string RequestId { get; set; }

}
public record ProductModelByFilterApiRequest : BasePaginatedApiRequest
{
    public byte? Type { get; set; }
    public byte? Pattern { get; set; }
    public byte? Subject { get; set; }
}