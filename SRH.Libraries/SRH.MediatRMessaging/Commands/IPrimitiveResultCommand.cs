using SRH.PrimitiveTypes.Result;
using MediatR;

namespace SRH.MediatRMessaging.Commands;

public interface IPrimitiveResultCommand<TResponse> : IRequest<PrimitiveResult<TResponse>> { }
public interface IPrimitiveResultCommand : IRequest<PrimitiveResult> { }