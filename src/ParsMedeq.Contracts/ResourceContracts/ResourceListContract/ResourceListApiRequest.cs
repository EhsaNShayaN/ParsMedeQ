namespace ParsMedeQ.Contracts.ResourceContracts.ResourceListContract;

public record ResourceListApiRequest(int TableId) : BasePaginatedApiRequest;