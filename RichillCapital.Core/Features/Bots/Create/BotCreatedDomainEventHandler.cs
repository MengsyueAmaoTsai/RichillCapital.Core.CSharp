using Microsoft.Extensions.Logging;

using RichillCapital.Core.Domain.Events;
using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.Features.Bots.Create;

internal sealed class BotCreatedDomainEventHandler : IDomainEventHandler<BotCreatedDomainEvent>
{
    private readonly ILogger<BotCreatedDomainEventHandler> _logger;

    public BotCreatedDomainEventHandler(ILogger<BotCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(BotCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event}", domainEvent.GetType().Name);
        return Task.CompletedTask;
    }
}