namespace ParsMedeQ.Contracts.ProductContracts.ProductListContract;

public record ProductListApiRequest(int TableId) : BasePaginatedApiRequest;