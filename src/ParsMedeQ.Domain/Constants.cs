namespace ParsMedeQ.Domain;
public static class Constants
{
    public const string LangCode_Farsi = "FA";
}
public enum Tables
{
    Article = 1,
    News = 2,
    Clip = 3,
    Product = 4,
    Payment = 5,
    Order = 6,
    Ticket = 7,
    TicketAnswers = 8,
}
public enum OrderStatus
{
    Pending = 0,
    Paid = 1,
    Shipped = 2,
    Completed = 3,
    Cancelled = 4,
}
public enum PaymentMethods
{
    Gateway = 0,
}
public enum PaymentStatus
{
    Pending = 0,
    Success = 1,
    Failed = 2,
    Refunded = 3,
}
public enum PaymentLogTypes
{
    Request = 0,
    Response = 1,
    Callback = 2,
}