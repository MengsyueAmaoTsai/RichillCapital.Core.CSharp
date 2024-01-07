using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.Domain.Events;

public sealed record class SignalEmittedDomainEvent() :
    DomainEvent
{
}