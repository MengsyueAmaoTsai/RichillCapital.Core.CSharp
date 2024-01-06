using MediatR;

namespace RichillCapital.Core.SharedKernel;

public abstract record DomainEvent : INotification
{
    public DateTimeOffset OccurredTime { get; protected set; } = DateTimeOffset.UtcNow;
}