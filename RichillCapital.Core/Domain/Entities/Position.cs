using RichillCapital.Core.Domain.Events;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.Domain.Entities;

public sealed class Position : Entity<PositionId>
{
    private Position(
        PositionId id,
        AccountId accountId)
        : base(id)
    {
        AccountId = accountId;
    }

    public AccountId AccountId { get; private set; }

    public static Position Open(AccountId accountId)
    {
        var position = new Position(
            PositionId.New(),
            accountId);

        position.RegisterDomainEvent(new PositionOpenedDomainEvent(position.Id));

        return position;
    }
}