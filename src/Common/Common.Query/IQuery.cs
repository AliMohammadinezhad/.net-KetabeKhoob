using MediatR;

namespace Common.Query;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : class
{

}