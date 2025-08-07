namespace SRH.RequestId;

public interface IRequestIdContextAccessor
{
    RequestIdContext? Current { set; }

    RequestIdContext GetCurrentRequestIdContext();
}

