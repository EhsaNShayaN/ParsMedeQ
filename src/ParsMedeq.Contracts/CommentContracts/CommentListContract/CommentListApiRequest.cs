namespace ParsMedeQ.Contracts.CommentContracts.CommentListContract;

public record CommentListApiRequest(int? RelatedId) : BasePaginatedApiRequest;
public record AdminCommentListApiRequest(int? RelatedId) : BasePaginatedApiRequest;
public record UserCommentListApiRequest(int? RelatedId) : BasePaginatedApiRequest;