namespace Common.Query.Filter;

public class BaseFilter
{
    public int EntityCount { get; private set; }
    public int CurrentPage { get; private set; }
    public int PageCount { get; private set; }
    public int StartPage { get; private set; }
    public int EndPage { get; private set; }
    public int Take { get; private set; }

    public void GeneratePaging(IQueryable<object> data, int take, int currentPage)
    {
        var entityCount = data.Count();
        var pageCount = (int)Math.Ceiling(entityCount/ (double)take);
        PageCount = pageCount;
        EntityCount = entityCount;
        CurrentPage = currentPage;
        Take = take;
        EndPage = (currentPage + 5 > pageCount) ? pageCount : currentPage + 5;
        StartPage = (currentPage - 4 <= pageCount) ? 1 : currentPage - 4;
    }
}

public class BaseFilter<TData, TParam> : BaseFilter
{
    public List<TData?> Data { get; set; } = [];
    public required TParam FilterParams { get; set; }
}