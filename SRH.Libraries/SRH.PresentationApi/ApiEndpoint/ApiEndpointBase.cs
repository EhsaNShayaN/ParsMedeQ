namespace SRH.PresentationApi.ApiEndpoint;

public abstract class ApiEndpointBase
{
    protected abstract ApiEndpointItem MyEndpoint { get; }

    static bool IsNullOrWhiteSpaceOrEmpty(string? src) => src is null || string.IsNullOrWhiteSpace(src) || string.IsNullOrEmpty(src);
    static bool IsNotNullOrWhiteSpaceOrEmpty(string? src) => !IsNullOrWhiteSpaceOrEmpty(src);
    static IEnumerable<string> GetUrlRecursive(ApiEndpointItem? item)
    {
        if (item is null) return Enumerable.Empty<string>();
        var result = new List<string>
        {
            item.Endpoint
        };

        var parents = GetUrlRecursive(item.Parent).ToList();

        foreach (var p in parents)
        {
            if (IsNullOrWhiteSpaceOrEmpty(p)) break;
            result.Add(p);
        }

        return result;
    }
    public List<string> Reverse(IEnumerable<string> list)
    {
        var result = list.ToList();
        result.Reverse();
        return result;
    }
    protected string GetUrl() => string.Join("/", Reverse(GetUrlRecursive(MyEndpoint)));
    protected string GetUrlWithParameters(string lastEndpoint, string[]? urlParameters) =>
        $"{this.GetUrl(lastEndpoint)}{(urlParameters?.Any() ?? false ? $"/{string.Join("/", urlParameters)}" : string.Empty)}";
    protected string GetUrl(string lastEndpoint) => string.Join("/", Reverse(GetUrlRecursive(MyEndpoint)).Concat(new string[1] { lastEndpoint }).Where(x => IsNotNullOrWhiteSpaceOrEmpty(x)));

    public override string ToString() => GetUrl();

}
