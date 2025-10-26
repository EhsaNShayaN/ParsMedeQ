namespace ParsMedeQ.Contracts.TicketContracts.TicketListContract;
public readonly record struct TicketListApiResponse(
    int Id,
    string FullName,
    string Title,
    string Description,
    byte Status,
    string StatusText,
    string MediaPath,
    int Code,
    DateTime CreationDate,
    TicketAnswerApiResponse[] Answers);

public readonly record struct TicketAnswerApiResponse(
    int Id,
    string FullName,
    string Answer,
    string MediaPath,
    DateTime CreationDate);