using RichillCapital.Core.Domain.Enumerations;
using RichillCapital.Core.Domain.Events;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.Domain.Entities;

public sealed class Bot : Entity<BotId>
{
    private Bot(
        BotId id,
        Name name,
        Description description,
        TradingPlatform platform)
        : base(id)
    {
        Name = name;
        Description = description;
        Platform = platform;
    }

    public Name Name { get; private set; }

    public Description Description { get; private set; }

    public TradingPlatform Platform { get; private set; }

    public static Bot Create(
        BotId id,
        Name name,
        Description description,
        TradingPlatform platform)
    {
        var bot = new Bot(id, name, description, platform);

        bot.RegisterDomainEvent(new BotCreatedDomainEvent(bot.Id));

        return bot;
    }
}