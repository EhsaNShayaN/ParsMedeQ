using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.UserAggregate.UserEntity;

namespace ParsMedeQ.Domain.Aggregates.TicketAggregate.Entities;

public sealed class TicketAnswer : EntityBase<int>
{
    #region " Properties "
    public int TicketId { get; private set; }
    public int? UserId { get; private set; }
    public string Answer { get; private set; } = string.Empty;
    public string MediaPath { get; private set; } = string.Empty;
    public DateTime CreationDate { get; private set; }
    #endregion

    #region " Navigation Properties "
    public Ticket Ticket { get; private set; } = null!;
    public User Users { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private TicketAnswer() : base(0) { }
    public TicketAnswer(int id) : base(id) { }
    #endregion

    #region " Factory "
    internal static PrimitiveResult<TicketAnswer> Create(
        int ticketId,
        int userId,
        string answer,
        string mediaPath,
        DateTime creationDate)
    {
        return PrimitiveResult.Success(
            new TicketAnswer()
            {
                TicketId = ticketId,
                UserId = userId,
                Answer = answer,
                MediaPath = mediaPath,
                CreationDate = creationDate
            });
    }
    #endregion
}
