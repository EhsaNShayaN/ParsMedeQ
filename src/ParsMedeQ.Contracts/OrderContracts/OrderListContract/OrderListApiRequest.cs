namespace ParsMedeQ.Contracts.OrderContracts.OrderListContract;

public record OrderListApiRequest() : BasePaginatedApiRequest;
public record AdminOrderListApiRequest() : BasePaginatedApiRequest;
public record UserOrderListApiRequest() : BasePaginatedApiRequest;