namespace ParsMedeQ.Contracts.ProductContracts.ProductListContract;

public record ProductListApiRequest(int? ProductCategoryId) : BasePaginatedApiRequest;