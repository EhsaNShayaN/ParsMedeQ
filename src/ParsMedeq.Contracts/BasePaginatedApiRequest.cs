namespace ParsMedeq.Contracts;

public abstract record BasePaginatedApiRequest
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public int LastId { get; set; } = 0;
}