namespace ParsMedeQ.Application.Features.CommentFeatures.CommentListFeature;

public sealed class CommentListDbQueryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }=string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int RelatedId { get; set; }
    public int TableId { get; set; }
    public string TableName { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
    public string Answers { get; set; } = string.Empty;
    public bool? IsConfirmed { get; set; }
    public DateTime CreationDate { get; set; }
}