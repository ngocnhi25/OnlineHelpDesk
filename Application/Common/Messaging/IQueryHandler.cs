using MediatR;
using SharedKernel;

namespace Application.Common.Messaging
{
    public interface IQueryHandler<in TQuery> 
        : IRequestHandler<TQuery, Result>
        where TQuery : IQuery
    {
    }

    public interface IQueryHandler<in TQuery, TResponse>
        : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
