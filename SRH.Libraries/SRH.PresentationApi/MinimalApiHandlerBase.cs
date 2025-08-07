using SRH.PresentationApi.ApiEndpoint;
using SRH.PresentationApi.MinimalApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Diagnostics;

namespace SRH.PresentationApi;

public abstract class MinimalApiHandlerBase<TRequest, TResponse> : IMinimalApiEndpoint
{
    private readonly static string[] _Get = [HttpMethod.Get.Method];
    private readonly static string[] _Post = [HttpMethod.Post.Method];
    private readonly static string[] _Put = [HttpMethod.Put.Method];
    private readonly static string[] _Delete = [HttpMethod.Delete.Method];
    private readonly static string[] _Head = [HttpMethod.Head.Method];
    private readonly static string[] _Patch = [HttpMethod.Patch.Method];
    private readonly static string[] _Trace = [HttpMethod.Trace.Method];
    private readonly static string[] _Options = [HttpMethod.Options.Method];

    protected readonly EndpointInfo _endpointInfo;
    protected readonly bool _isStream;
    protected readonly HttpMethod _method;

    public MinimalApiHandlerBase(
        EndpointInfo endpointInfo,
        bool isStream,
        HttpMethod method)
    {
        this._endpointInfo = endpointInfo;
        this._isStream = isStream;
        this._method = method;
    }

    public abstract RouteHandlerBuilder AddRoute(IEndpointRouteBuilder routeBuilder);

    protected virtual RouteHandlerBuilder MapRoute(IEndpointRouteBuilder app, Delegate @delegate) =>
        app.MapMethods(
            this.GetEndpointPattern(),
            this.GetHttpMethod(),
            @delegate)
        .WithDescription(this._endpointInfo.Description)
        .WithDisplayName(this._endpointInfo.Description)
        .WithName(this._endpointInfo.Name)
        .WithTags(this._endpointInfo.Tag);

    protected virtual string GetEndpointPattern() => this._endpointInfo.UrlWithParameters;

    private string[] GetHttpMethod() =>
        this._method.Method.ToUpper() switch
        {
            "GET" => _Get,
            "POST" => _Post,
            "PUT" => _Put,
            "DELETE" => _Delete,
            "HEAD" => _Head,
            "PATCH" => _Patch,
            "TRACE" => _Trace,
            "OPTIONS" => _Options,
            _ => throw new UnreachableException()
        };
}
