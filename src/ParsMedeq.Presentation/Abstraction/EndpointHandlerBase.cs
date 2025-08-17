using MediatR;
using Microsoft.AspNetCore.Http;

namespace ParsMedeQ.Presentation.Abstraction;
internal static class EndpointHandlerBase
{
    internal static ValueTask<PrimitiveResult<TEndpointResponse>> DefaultHandlerResponseMapper<THandlerResponse, TEndpointResponse>(THandlerResponse handlerResponse, CancellationToken cancellationToken) =>
       handlerResponse is null
           ? ValueTask.FromResult(PrimitiveResult.InternalFailure<TEndpointResponse>("", "Handler response is null"))
           : ValueTask.FromResult(PrimitiveResult.Success(handlerResponse.Adapt<TEndpointResponse>()));
    internal static ValueTask<PrimitiveResult<THandlerRequest>> DefaultMapper<TApiRequest, THandlerRequest>(TApiRequest apiRequest, CancellationToken cancellationToken) =>
        ValueTask.FromResult(PrimitiveResult.Success(apiRequest.Adapt<THandlerRequest>()));
}

internal abstract class EndpointHandlerBase<THandlerRequest, THandlerResponse, TEndpointResponse> : MinimalApiHandlerBase<THandlerRequest, TEndpointResponse>
    where THandlerRequest : IRequest<PrimitiveResult<THandlerResponse>>
{
    protected virtual bool NeedAuthentication { get; } = true;
    protected virtual bool NeedTaxPayerFile { get; } = true;
    protected virtual bool AdminPrivilages { get; } = false;

    protected virtual Delegate EndpointDelegate =>
      (ISender sender, CancellationToken cancellationToken) => this.CallMediatRHandler(
          sender,
          () => ValueTask.FromResult(PrimitiveResult.Success(this._handlerRequestFactory.Invoke())),
          cancellationToken);

    private readonly Func<THandlerRequest> _handlerRequestFactory = null!;

    #region " Fields "
    protected readonly Func<THandlerResponse, CancellationToken, ValueTask<PrimitiveResult<TEndpointResponse>>> _handlerResponseMapper;
    protected readonly Func<PrimitiveResult<TEndpointResponse>, IResult> _responseFactory;
    #endregion

    protected EndpointHandlerBase(
        EndpointInfo endpointInfo,
        HttpMethod httpMethod,
        Func<THandlerRequest> handlerRequestFactory,
        Func<THandlerResponse, CancellationToken, ValueTask<PrimitiveResult<TEndpointResponse>>> handlerResponseMapper,
        Func<PrimitiveResult<TEndpointResponse>, IResult> responseFactory) : base(endpointInfo, false, httpMethod)
    {
        this._handlerRequestFactory = handlerRequestFactory;
        this._handlerResponseMapper = handlerResponseMapper;
        this._responseFactory = responseFactory;
    }

    protected EndpointHandlerBase(
        EndpointInfo endpointInfo,
        HttpMethod httpMethod,
        Func<THandlerRequest> handlerRequestFactory,
        IPresentationMapper<THandlerResponse, TEndpointResponse> handlerResponseMapper) : this(
            endpointInfo,
            httpMethod,
            handlerRequestFactory,
            handlerResponseMapper.Map,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected EndpointHandlerBase(
       EndpointInfo endpointInfo,
       HttpMethod httpMethod,
       Func<THandlerResponse, CancellationToken, ValueTask<PrimitiveResult<TEndpointResponse>>> handlerResponseMapper,
       Func<PrimitiveResult<TEndpointResponse>, IResult> responseFactory) : base(endpointInfo, false, httpMethod)
    {
        this._handlerResponseMapper = handlerResponseMapper;
        this._responseFactory = responseFactory;
    }

    protected EndpointHandlerBase(
       EndpointInfo endpointInfo,
       HttpMethod httpMethod,
       Func<THandlerResponse, CancellationToken, ValueTask<PrimitiveResult<TEndpointResponse>>> handlerResponseMapper)
        : this(endpointInfo, httpMethod, handlerResponseMapper, DefaultResponseFactory.Instance.CreateOk)
    { }


    public override RouteHandlerBuilder AddRoute(IEndpointRouteBuilder routeBuilder)
    {
        var result = this.MapRoute(routeBuilder, this.EndpointDelegate);

        /*if (this.NeedTaxPayerAuthentication && !this.AdminPrivilages)
        {
            result.AddEndpointFilter<TaxPayerAuthenticationEndpointFilter>();
        }

        if (this.NeedTaxPayerFile && !this.AdminPrivilages)
        {
            result.AddEndpointFilter<TaxPayerFileEndpointFilter>();
        }*/

        return result;
    }

    protected virtual async ValueTask<IResult> CallMediatRHandler(
        ISender sender,
        Func<ValueTask<PrimitiveResult<THandlerRequest>>> mediatRRequestCreator,
        CancellationToken cancellationToken)
    {
        var result = await
            mediatRRequestCreator.Invoke()
            .Map(handlerRequest => sender.SendRequest(handlerRequest, cancellationToken))
            .Map(handlerResponse => this._handlerResponseMapper.Invoke(handlerResponse, cancellationToken))
            .ConfigureAwait(false);

        return this._responseFactory.Invoke(result);
    }
}
internal abstract class EndpointHandlerBase<TApiRequest, THandlerRequest, THandlerResponse, TEndpointResponse> : EndpointHandlerBase<THandlerRequest, THandlerResponse, TEndpointResponse>
    where THandlerRequest : IRequest<PrimitiveResult<THandlerResponse>>
{
    #region " Fields "
    private readonly Func<TApiRequest, CancellationToken, ValueTask<PrimitiveResult<THandlerRequest>>> _requestFactory;
    #endregion

    protected override Delegate EndpointDelegate =>
        (TApiRequest request, ISender sender, CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => this._requestFactory.Invoke(request, cancellationToken),
            cancellationToken);

    #region " Constructors "
    protected EndpointHandlerBase(
        EndpointInfo endpointInfo,
        HttpMethod httpMethod,
        Func<TApiRequest, CancellationToken, ValueTask<PrimitiveResult<THandlerRequest>>> requestFactory,
        Func<THandlerResponse, CancellationToken, ValueTask<PrimitiveResult<TEndpointResponse>>> handlerResponseMapper,
        Func<PrimitiveResult<TEndpointResponse>, IResult> responseFactory) : base(endpointInfo, httpMethod, handlerResponseMapper, responseFactory)
    {
        this._requestFactory = requestFactory;
    }

    protected EndpointHandlerBase(
       EndpointInfo endpointInfo,
       HttpMethod httpMethod,
       Func<TApiRequest, CancellationToken, ValueTask<PrimitiveResult<THandlerRequest>>> requestFactory,
       Func<PrimitiveResult<TEndpointResponse>, IResult> responseFactory) : this(endpointInfo,
            httpMethod,
            requestFactory,
            EndpointHandlerBase.DefaultHandlerResponseMapper<THandlerResponse, TEndpointResponse>,
            responseFactory)
    { }

    protected EndpointHandlerBase(
        EndpointInfo endpointInfo,
        HttpMethod httpMethod,
        Func<TApiRequest, CancellationToken, ValueTask<PrimitiveResult<THandlerRequest>>> requestFactory,
        IPresentationResponseFactory presentationResponseFactory,
        Func<DefaultApiResponse<TEndpointResponse>, IResult> okFunc) : this(endpointInfo,
           httpMethod,
           requestFactory,
           EndpointHandlerBase.DefaultHandlerResponseMapper<THandlerResponse, TEndpointResponse>,
           apiResponse => presentationResponseFactory.Create(apiResponse, okFunc))
    { }


    protected EndpointHandlerBase(
      EndpointInfo endpointInfo,
      HttpMethod httpMethod,
      Func<TApiRequest, CancellationToken, ValueTask<PrimitiveResult<THandlerRequest>>> requestFactory,
      IPresentationResponseFactory presentationResponseFactory) : this(endpointInfo,
           httpMethod,
           requestFactory,
           presentationResponseFactory,
           TypedResults.Ok)
    { }

    protected EndpointHandlerBase(
        EndpointInfo endpointInfo,
        HttpMethod httpMethod,
        IPresentationMapper<TApiRequest, THandlerRequest> requestMapper,
        IPresentationMapper<THandlerResponse, TEndpointResponse> handlerResponseMapper,
        Func<PrimitiveResult<TEndpointResponse>, IResult> responseFactory) : this(endpointInfo,
            httpMethod,
            requestMapper.Map,
            handlerResponseMapper.Map,
            responseFactory)
    { }

    protected EndpointHandlerBase(
       EndpointInfo endpointInfo,
       HttpMethod httpMethod,
       IPresentationMapper<TApiRequest, THandlerRequest> requestMapper,
       IPresentationMapper<THandlerResponse, TEndpointResponse> handlerResponseMapper) : this(endpointInfo,
           httpMethod,
           requestMapper.Map,
           handlerResponseMapper.Map,
           DefaultResponseFactory.Instance.CreateOk)
    { }

    protected EndpointHandlerBase(
        EndpointInfo endpointInfo,
        HttpMethod httpMethod,
        IPresentationMapper<TApiRequest, THandlerRequest> requestMapper,
        Func<PrimitiveResult<TEndpointResponse>, IResult> responseFactory) : this(endpointInfo,
            httpMethod,
            requestMapper.Map,
            EndpointHandlerBase.DefaultHandlerResponseMapper<THandlerResponse, TEndpointResponse>,
            responseFactory)
    { }

    protected EndpointHandlerBase(
        EndpointInfo endpointInfo,
        HttpMethod httpMethod) : this(endpointInfo,
            httpMethod,
            EndpointHandlerBase.DefaultMapper<TApiRequest, THandlerRequest>,
            EndpointHandlerBase.DefaultHandlerResponseMapper<THandlerResponse, TEndpointResponse>,
            DefaultResponseFactory.Instance.CreateOk)
    { }
    #endregion

    //public override RouteHandlerBuilder AddRoute(IEndpointRouteBuilder routeBuilder)
    //{
    //    var result = this.MapRoute(
    //        routeBuilder,
    //        async (
    //            TApiRequest request,
    //            ISender sender,
    //            CancellationToken cancellationToken) =>
    //        {
    //            var result = await this._requestFactory.Invoke(request, cancellationToken)
    //                .Map(handlerRequest => sender.SendRequest(handlerRequest, cancellationToken))
    //                .Map(handlerResponse => this._handlerResponseMapper.Invoke(handlerResponse, cancellationToken))
    //                .ConfigureAwait(false);

    //            return this._responseFactory.Invoke(result);
    //        });

    //    if (this.NeedTaxPayerAuthentication)
    //    {
    //        result.AddEndpointFilter<TaxPayerAuthenticationEndpointFilter>();
    //    }

    //    if (this.NeedTaxPayerFile)
    //    {
    //        result.AddEndpointFilter<TaxPayerFileEndpointFilter>();
    //    }

    //    return result;
    //}

}

/// <summary>
/// ای پی آی هایی که درخواستی برای آن ها ارسال نمیشود
/// </summary>
/// <typeparam name="THandlerRequest"></typeparam>
/// <typeparam name="THandlerResponse"></typeparam>
/// <typeparam name="TEndpointResponse"></typeparam>
internal abstract class GetEndpointHandlerBase<THandlerRequest, THandlerResponse, TEndpointResponse>
    : EndpointHandlerBase<THandlerRequest, THandlerResponse, TEndpointResponse>
    where THandlerRequest : IRequest<PrimitiveResult<THandlerResponse>>
{
    protected GetEndpointHandlerBase(
        EndpointInfo endpointInfo,
        HttpMethod httpMethod,
        Func<THandlerRequest> requestFactory,
        Func<THandlerResponse, CancellationToken, ValueTask<PrimitiveResult<TEndpointResponse>>> handlerResponseMapper,
        Func<PrimitiveResult<TEndpointResponse>, IResult> responseFactory) : base(
            endpointInfo,
            httpMethod,
            requestFactory,
            handlerResponseMapper,
            responseFactory)
    {
    }

    protected GetEndpointHandlerBase(
        EndpointInfo endpointInfo,
        HttpMethod httpMethod,
        Func<THandlerRequest> requestFactory,
        IPresentationMapper<THandlerResponse, TEndpointResponse> responseMapper) : base(
            endpointInfo,
            httpMethod,
            requestFactory,
            responseMapper.Map,
            DefaultResponseFactory.Instance.CreateOk)
    {
    }
    protected GetEndpointHandlerBase(
        EndpointInfo endpointInfo,
        HttpMethod httpMethod,
        Func<THandlerRequest> requestFactory) : this(
            endpointInfo,
            httpMethod,
            requestFactory,
            EndpointHandlerBase.DefaultHandlerResponseMapper<THandlerResponse, TEndpointResponse>,
            DefaultResponseFactory.Instance.CreateOk)
    {
    }
}