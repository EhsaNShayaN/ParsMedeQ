using SRH.PrimitiveTypes.Result;
using MediatR;

namespace SRH.MediatRMessaging.Queries;

public interface IPrimitiveResultQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, PrimitiveResult<TResponse>>
    where TQuery : IPrimitiveResultQuery<TResponse>
{ }



