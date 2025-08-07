using SRH.PrimitiveTypes.Result;
using MediatR;

namespace SRH.MediatRMessaging.Queries;

public interface IPrimitiveResultQuery<TResponse> : IRequest<PrimitiveResult<TResponse>> { }



