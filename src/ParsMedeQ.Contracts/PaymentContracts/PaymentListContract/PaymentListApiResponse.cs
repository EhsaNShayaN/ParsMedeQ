namespace ParsMedeQ.Contracts.PaymentContracts.PaymentListContract;
public readonly record struct PaymentListApiResponse(
    int Id,
    string FullName,
    string Title,
    string Description,
    byte Status,
    string StatusText,
    string MediaPath,
    int Code,
    DateTime CreationDate,
    PaymentAnswerApiResponse[] Answers);

public readonly record struct PaymentAnswerApiResponse(
    int Id,
    string FullName,
    string Answer,
    string MediaPath,
    DateTime CreationDate);