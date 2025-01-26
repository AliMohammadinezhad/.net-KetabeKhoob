using Common.Query.Filter;
using MediatR;

namespace Common.Query;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : class
{

}

public class QueryFilter<TResponse, TParam>(TParam filterParams) : IQuery<TResponse>
    where TResponse : BaseFilter
    where TParam : BaseFilterParam
{
    public TParam FilterParams { get; set; } = filterParams;
}