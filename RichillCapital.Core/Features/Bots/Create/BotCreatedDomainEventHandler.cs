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
    private readonly IRepository<Account> _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BotCreatedDomainEventHandler(
        IRepository<Account> accountRepository,
        IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        BotCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
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