using Microsoft.Extensions.Logging;

using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.Enumerations;
using RichillCapital.Core.Domain.Events;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.Features.Bots.Create;

internal sealed class BotCreatedDomainEventHandler :
    IDomainEventHandler<BotCreatedDomainEvent>
{
    private readonly ILogger<BotCreatedDomainEventHandler> _logger;
    private readonly IRepository<Account> _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BotCreatedDomainEventHandler(
        ILogger<BotCreatedDomainEventHandler> logger,
        IRepository<Account> accountRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        BotCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "{event} - {id}",
            domainEvent.GetType().Name,
            domainEvent.BotId.Value);

        var accounts = new List<Account>()
        {
            Account.Create(
                AccountId.NewSimulatedId(),
                new Name(string.Empty),
                Currency.TWD,
                3000000),
            Account.Create(
                AccountId.NewMockId(),
                new Name(string.Empty),
                Currency.TWD,
                3000000),
        };

        _accountRepository.AddRange(accounts);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}