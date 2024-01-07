using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.Domain.Events;

public sealed record class PositionOpenedDomainEvent(
    PositionId PositionId) :
    DomainEvent
{
}