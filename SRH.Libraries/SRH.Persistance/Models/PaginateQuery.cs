namespace SRH.Persistance.Models;

public readonly record struct PaginateQuery
{
    private readonly int _pageIndex;
    private readonly int _pageSize;

    public int PageIndex => _pageIndex;
    public int PageSize => _pageSize;

    private PaginateQuery(int pageIndex, int pageSize)
    {
        _pageIndex = pageIndex;
        _pageSize = pageSize;
    }

    public static PaginateQuery Create(int pageIndex, int pageSize) => new(pageIndex, pageSize);
}
