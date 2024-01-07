using NSubstitute;

using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.Events;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.Features.Bots.Create;
using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.UnitTests.Features.Bots;

[TestClass]
public sealed class BotCreatedDomainEventTests
{
    private static readonly BotCreatedDomainEvent DomainEvent = new(new BotId("XQ-IS-TW-M5-0100"));

    private readonly BotCreatedDomainEventHandler _handler;

    private readonly IRepository<Account> _accountRepository =
        Substitute.For<IRepository<Account>>();

    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    public BotCreatedDomainEventTests()
    {
        _handler = new(_accountRepository, _unitOfWork);
    }

    [TestMethod]
    public async Task When_BotCreated_Should_HandleDomainEvent()
    {
        // Act
        await _handler.Handle(DomainEvent, default);

        _accountRepository
            .Received(1)
            .AddRange(Arg.Any<List<Account>>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}