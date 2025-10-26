namespace ParsMedeQ.Contracts.CommentContracts.AddCommentContract;
public readonly record struct AddCommentApiRequest(
    int TableId,
    string TableName,
    int RelatedId,
    string Icon,
    string Description);