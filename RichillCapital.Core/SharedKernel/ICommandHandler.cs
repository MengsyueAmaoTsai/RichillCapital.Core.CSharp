using MediatR;

namespace RichillCapital.Core.SharedKernel;

public interface ICommandHandler<TCommand, TResult> :
    IRequestHandler<TCommand, TResult>
    where TCommand : IQuery<TResult>
{
    new Task<TResult> Handle(TCommand command, CancellationToken cancellationToken);
}