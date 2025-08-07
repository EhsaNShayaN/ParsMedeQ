using SRH.PrimitiveTypes.Result;

namespace SRH.MediatRMessaging;

public interface IValidatableRequest<T>
{
    ValueTask<PrimitiveResult<T>> Validate();
}
