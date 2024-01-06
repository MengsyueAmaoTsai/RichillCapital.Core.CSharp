using MediatR;

namespace RichillCapital.Core.SharedKernel;

public interface IQuery<TResult> :
    IRequest<TResult>
{
}