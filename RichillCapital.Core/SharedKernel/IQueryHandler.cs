using MediatR;

namespace RichillCapital.Core.SharedKernel;

public interface IQueryHandler<TQuery, TResult> :
    IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    new Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
}