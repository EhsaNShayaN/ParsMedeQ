namespace ParsMedeQ.Contracts.TicketContracts.TicketListContract;

public record TicketListApiRequest(int? RelatedId) : BasePaginatedApiRequest;