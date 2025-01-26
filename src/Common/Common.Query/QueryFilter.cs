using Common.Query.Filter;

namespace Common.Query;

public class QueryFilter<TResponse, TParam>(TParam filterParams) : IQuery<TResponse>
    where TResponse : BaseFilter
    where TParam : BaseFilterParam
{
    public TParam FilterParams { get; set; } = filterParams;
}