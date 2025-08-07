using Microsoft.AspNetCore.Builder;

namespace SRH.PresentationApi.MediatR.ApiEndpointHandlers;

public abstract class MinimalApiEndpointMediatRHandlerBase<TRequest, TResponse> : MinimalApiHandlerBase<TRequest, TResponse>
    where TRequest : IRequest<PrimitiveResult<TResponse>>
{
    private readonly Func<TRequest> _requestFactory;
    private readonly Func<PrimitiveResult<TResponse>, ValueTask<IResult>> _responseFactory;

    protected MinimalApiEndpointMediatRHandlerBase(
        EndpointInfo endpointInfo,
        HttpMethod method,
        Func<TRequest> requestFactory,
        Func<PrimitiveResult<TResponse>, ValueTask<IResult>> responseFactory) : base(endpointInfo, false, method)
    {
        this._requestFactory = requestFactory;
        this._responseFactory = responseFactory;
    }

    public override RouteHandlerBuilder AddRoute(IEndpointRouteBuilder app) => this.MapRoute(
        app,
        async (ISender sender, CancellationToken cancellationToken) =>
        {
            var request = this._requestFactory();
            var result = await sender.Send(request, cancellationToken);
            return await this._responseFactory.Invoke(result).ConfigureAwait(false);
        });
}
public abstract class MinimalApiEndpointMediatRHandlerBase<TApiRequest, TRequest, TResponse> : MinimalApiHandlerBase<TRequest, TResponse>
    where TRequest : IRequest<PrimitiveResult<TResponse>>
{
    private readonly Func<TApiRequest, TRequest> _requestFactory;
    private readonly Func<PrimitiveResult<TResponse>, ValueTask<IResult>> _responseFactory;

    protected MinimalApiEndpointMediatRHandlerBase(
        EndpointInfo endpointInfo,
        HttpMethod method,
        Func<TApiRequest, TRequest> requestFactory,
        Func<PrimitiveResult<TResponse>, ValueTask<IResult>> responseFactory) : base(endpointInfo, false, method)
    {
        this._requestFactory = requestFactory;
        this._responseFactory = responseFactory;
    }

    public override RouteHandlerBuilder AddRoute(IEndpointRouteBuilder app) => this.MapRoute(
        app,
        async (TApiRequest apiRequest, ISender sender, CancellationToken cancellationToken) =>
        {
            var request = this._requestFactory(apiRequest);
            var result = await sender.Send(request, cancellationToken);
            return await this._responseFactory.Invoke(result).ConfigureAwait(false);
        });
}
public abstract class MinimalApiEndpointMediatRWithDefaultMapperHandlerBase<TApiRequest, TRequest, TResponse> : MinimalApiEndpointMediatRHandlerBase<TApiRequest, TRequest, TResponse>
    where TRequest : IRequest<PrimitiveResult<TResponse>>
{
    protected MinimalApiEndpointMediatRWithDefaultMapperHandlerBase(
        EndpointInfo endpointInfo,
        HttpMethod method,
        IMapper mapper,
        Func<PrimitiveResult<TResponse>, ValueTask<IResult>> responseFactory) : base(
            endpointInfo,
            method,
            mapper.Map<TApiRequest, TRequest>,
            responseFactory)
    {
    }
}


public abstract class MinimalApiEndpointMediatRStreamHandlerBase<TRequest, TResponse> : MinimalApiHandlerBase<TRequest, TResponse>
    where TRequest : IStreamRequest<TResponse>
{
    private readonly Func<TRequest> _requestFactory;

    protected MinimalApiEndpointMediatRStreamHandlerBase(
        EndpointInfo endpointInfo,
        HttpMethod method,
        Func<TRequest> requestFactory) : base(endpointInfo, false, method)
    {
        this._requestFactory = requestFactory;
    }

    public override RouteHandlerBuilder AddRoute(IEndpointRouteBuilder app) => this.MapRoute(app, this.Handle);
    private async IAsyncEnumerable<TResponse> Handle(ISender sender, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var responseItem in sender.CreateStream(this._requestFactory.Invoke(), cancellationToken).WithCancellation(cancellationToken))
        {
            yield return responseItem;
        }
    }
}
public abstract class MinimalApiEndpointMediatRStreamHandlerBase<TApiRequest, TRequest, TResponse> : MinimalApiHandlerBase<TRequest, TResponse>
    where TRequest : IStreamRequest<TResponse>
{
    private readonly Func<TApiRequest, TRequest> _requestFactory;

    protected MinimalApiEndpointMediatRStreamHandlerBase(
        EndpointInfo endpointInfo,
        HttpMethod method,
        Func<TApiRequest, TRequest> requestFactory) : base(endpointInfo, false, method)
    {
        this._requestFactory = requestFactory;
    }

    public override RouteHandlerBuilder AddRoute(IEndpointRouteBuilder app) => this.MapRoute(app, Handle);
    private async IAsyncEnumerable<TResponse> Handle(TApiRequest apiRequest, ISender sender, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var responseItem in sender.CreateStream(this._requestFactory.Invoke(apiRequest), cancellationToken).WithCancellation(cancellationToken))
        {
            yield return responseItem;
        }
    }
}
public abstract class MinimalApiEndpointMediatRStreamWithDefaultMapperHandlerBase<TApiRequest, TRequest, TResponse> : MinimalApiHandlerBase<TRequest, TResponse>
    where TRequest : IStreamRequest<TResponse>
{
    private readonly Func<TApiRequest, TRequest> _requestFactory;

    protected MinimalApiEndpointMediatRStreamWithDefaultMapperHandlerBase(
        EndpointInfo endpointInfo,
        HttpMethod method,
        IMapper mapper) : base(endpointInfo, false, method)
    {
        this._requestFactory = req => mapper.Map<TApiRequest, TRequest>(req);
    }

    public override RouteHandlerBuilder AddRoute(IEndpointRouteBuilder app) => this.MapRoute(app, Handle);
    private async IAsyncEnumerable<TResponse> Handle(TApiRequest apiRequest, ISender sender, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var responseItem in sender.CreateStream(this._requestFactory.Invoke(apiRequest), cancellationToken).WithCancellation(cancellationToken))
        {
            yield return responseItem;
        }
    }
}