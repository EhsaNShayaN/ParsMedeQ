using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.TicketAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.UserAggregate.UserEntity;

namespace ParsMedeQ.Domain.Aggregates.TicketAggregate;

public sealed class Ticket : EntityBase<int>
{
    #region " Fields "
    private List<TicketAnswer> _ticketAnswerss = [];
    #endregion

    #region " Properties "
    public int UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public byte Status { get; private set; }
    public string MediaPath { get; private set; } = string.Empty;
    public int Code { get; private set; }
    public DateTime CreationDate { get; private set; }
    #endregion

    #region " Navigation Properties "
    public User Users { get; private set; } = null!;
    public IReadOnlyCollection<TicketAnswer> TicketAnswers => this._ticketAnswerss.AsReadOnly();
    #endregion

    #region " Constructors "
    private Ticket() : base(0) { }
    public Ticket(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<Ticket> Create(
        int userId,
        string title,
        string description,
        byte status,
        string mediaPath)
    {
        return PrimitiveResult.Success(
            new Ticket()
            {
                UserId = userId,
                Title = title,
                Description = description,
                Status = status,
                MediaPath = mediaPath,
                CreationDate = DateTime.UtcNow
            });
    }
    #endregion
}

