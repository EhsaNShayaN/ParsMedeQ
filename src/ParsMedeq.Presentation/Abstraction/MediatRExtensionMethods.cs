using MediatR;

namespace EShop.Presentation.Abstraction;

public static class MediatRExtensionMethods
{
    public async static ValueTask<PrimitiveResult<TResponse>> SendRequest<TResponse>(this ISender src,
        IRequest<PrimitiveResult<TResponse>> request,
        CancellationToken cancellationToken) => await src.Send(request, cancellationToken).ConfigureAwait(false);
}

