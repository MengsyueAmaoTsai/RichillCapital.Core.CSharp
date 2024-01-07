using FluentAssertions;

using NSubstitute;

using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.Enumerations;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.Features.Bots.Delete;
using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.UnitTests.Features.Bots;

[TestClass]
public sealed class DeleteBotCommandTests
{
    private static readonly DeleteBotCommand Command = new("XQ-IS-TW-M5-0100");

    private readonly DeleteBotCommandHandler _handler;
    private readonly IRepository<Bot> _botRepository = Substitute.For<IRepository<Bot>>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    public DeleteBotCommandTests()
    {
        _handler = new(_botRepository, _unitOfWork);
    }

    [TestMethod]
    public async Task When_BotNotFound_Should_ReturnFailure()
    {
        // Arrange
        _botRepository
            .GetByIdAsync(
                Arg.Any<BotId>(),
                Arg.Any<CancellationToken>())
            .Returns(Maybe<Bot>.NoValue);

        // Act
        var result = await _handler.Handle(Command, default);

        // Assert
        await _botRepository.Received(1).GetByIdAsync(
            Arg.Any<BotId>(),
            Arg.Any<CancellationToken>());

        result.IsSuccess.Should().BeFalse();
        result.Error.Type.Should().Be(ErrorType.NotFound);
    }

    [TestMethod]
    public async Task When_BotFound_Should_ReturnSuccess()
    {
        // Arrange
        var bot = Bot.Create(
            new BotId(Command.BotId),
            new Name(string.Empty),
            new Description(string.Empty),
            TradingPlatform.XQ);

        _botRepository
            .GetByIdAsync(
                Arg.Any<BotId>(),
                Arg.Any<CancellationToken>())
            .Returns(Maybe<Bot>.WithValue(bot));

        // Act
        var result = await _handler.Handle(Command, default);

        // Assert
        await _botRepository.Received(1)
            .GetByIdAsync(
                Arg.Any<BotId>(),
                Arg.Any<CancellationToken>());

        _botRepository.Received(1)
            .Remove(Arg.Any<Bot>());

        await _unitOfWork.Received(1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());

        result.IsSuccess.Should().BeTrue();
    }
}