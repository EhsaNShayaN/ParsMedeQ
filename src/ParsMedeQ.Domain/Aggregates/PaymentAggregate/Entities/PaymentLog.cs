using ParsMedeQ.Domain.Abstractions;

namespace ParsMedeQ.Domain.Aggregates.PaymentAggregate.Entities;
public sealed class PaymentLog : EntityBase<int>
{
    #region " Properties "
    public int PaymentId { get; private set; }
    public string LogType { get; private set; } = string.Empty;
    public string RawData { get; private set; } = string.Empty;
    public DateTime CreationDate { get; private set; } = DateTime.Now;
    #endregion

    #region " Navigation Properties "
    public Payment? Payment { get; private set; }
    #endregion

    #region " Constructors "
    private PaymentLog() : base(0) { }
    public PaymentLog(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<PaymentLog> Create(
        int paymentId,
        string logType,
        string rawData,
        DateTime creationDate)
    {
        return PrimitiveResult.Success(
            new PaymentLog()
            {
                PaymentId = paymentId,
                LogType = logType,
                RawData = rawData,
                CreationDate = creationDate
            });
    }
    #endregion
}