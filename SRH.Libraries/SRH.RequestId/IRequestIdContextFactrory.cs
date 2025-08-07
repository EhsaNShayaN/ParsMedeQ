namespace SRH.RequestId;

public interface IRequestIdContextFactrory
{
    RequestIdContext Create(string requestId, string correlationId);
    void Dispose();
}

