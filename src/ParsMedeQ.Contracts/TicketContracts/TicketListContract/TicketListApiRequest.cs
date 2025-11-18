namespace ParsMedeQ.Contracts.TicketContracts.TicketListContract;

public record TicketListApiRequest(int? RelatedId) : BasePaginatedApiRequest;
public record AdminTicketListApiRequest(int? RelatedId) : BasePaginatedApiRequest;
public record UserTicketListApiRequest(int? RelatedId) : BasePaginatedApiRequest;