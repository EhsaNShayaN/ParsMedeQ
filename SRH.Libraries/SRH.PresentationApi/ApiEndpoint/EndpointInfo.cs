namespace SRH.PresentationApi.ApiEndpoint;
public readonly record struct EndpointInfo(
    string Url,
    string UrlWithParameters,
    string Name,
    string Description,
    string Tag);
