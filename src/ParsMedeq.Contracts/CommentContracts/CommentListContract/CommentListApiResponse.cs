namespace ParsMedeQ.Contracts.CommentContracts.CommentListContract;
public readonly record struct CommentListApiResponse(
    int Id,
    string Name,
    string Icon,
    string Description,
    int RelatedId,
    int TableId,
    string TableName,
    string Data,
    string Answers,
    bool? IsConfirmed,
    string CreationDate);