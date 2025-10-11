namespace ParsMedeQ.Contracts.CommentContracts.CommentListContract;

public record CommentListApiRequest(int? RelatedId) : BasePaginatedApiRequest;