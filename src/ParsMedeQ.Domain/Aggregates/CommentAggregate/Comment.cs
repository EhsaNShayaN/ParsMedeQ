using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.UserAggregate.UserEntity;

namespace ParsMedeQ.Domain.Aggregates.CommentAggregate;
public sealed class Comment : EntityBase<int>
{
    #region " Properties "
    public int UserId { get; private set; }
    public string? Icon { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int RelatedId { get; private set; }
    public int TableId { get; private set; }
    public string TableName { get; private set; } = string.Empty;
    public string? Data { get; private set; } = string.Empty;
    public string? Answers { get; private set; } = string.Empty;
    public bool? IsConfirmed { get; private set; }
    public DateTime CreationDate { get; private set; }
    #endregion

    #region " Navigation Properties "
    public User User { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private Comment() : base(0) { }
    public Comment(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<Comment> Create(
        int userId,
        int tableId,
        string tableName,
        int relatedId,
        string icon,
        string description/*,
        string data,
        string answers,
        bool isConfirmed,
        DateTime creationDate*/)
    {
        return PrimitiveResult.Success(
            new Comment()
            {
                UserId = userId,
                Icon = icon,
                Description = description,
                RelatedId = relatedId,
                TableId = tableId,
                TableName = tableName,
                /*Data = data,
                Answers = answers,
                IsConfirmed = isConfirmed,
                CreationDate = creationDate*/
                CreationDate = DateTime.Now
            });
    }
    #endregion
}