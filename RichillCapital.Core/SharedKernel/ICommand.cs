using MediatR;

namespace RichillCapital.Core.SharedKernel;

public interface ICommand<TResult> :
    IRequest<TResult>
{
}