using SRH.PrimitiveTypes.Result;
using MediatR;

namespace SRH.MediatRMessaging.Commands;
public interface IPrimitiveResultCommandHandler<TQuery, TResponse> : IRequestHandler<TQuery, PrimitiveResult<TResponse>>
    where TQuery : IPrimitiveResultCommand<TResponse>
{ }

public interface IPrimitiveResultCommandHandler<TQuery> : IRequestHandler<TQuery, PrimitiveResult>
    where TQuery : IPrimitiveResultCommand
{ }



