namespace EShop.Application.Helpers;
public abstract record BasePaginatedQuery
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public int LastId { get; set; }
}
public record BasePaginatedApiResponse<T>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public T[] Items { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
    public object? LastId { get; set; }

    //TODO : We must use factory instead. use immutable approach
    public BasePaginatedApiResponse(T[] items, int count, int currentPage, int pageSize)
    {
        this.PageSize = pageSize;
        this.PageIndex = currentPage;
        this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        this.TotalCount = count;
        this.Items = items;
        this.HasPrevious = this.PageIndex > 1;
        this.HasNext = this.PageIndex < this.TotalPages;
    }
}