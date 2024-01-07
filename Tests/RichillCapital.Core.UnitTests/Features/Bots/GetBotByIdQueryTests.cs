using FluentAssertions;

using NSubstitute;

using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.Enumerations;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.Features.Bots.GetById;
using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.UnitTests.Features.Bots;

[TestClass]
public sealed class GetBotByIdQueryTests
{
    private static readonly GetBotByIdQuery Query = new("XQ-IS-TW-M5-0100");

    private readonly GetBotByIdQueryHandler _handler;
    private readonly IReadonlyRepository<Bot> _botRepository =
        Substitute.For<IReadonlyRepository<Bot>>();

    public GetBotByIdQueryTests() => _handler = new(_botRepository);

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
        var result = await _handler.Handle(Query, default);

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
            new BotId(Query.BotId),
            new Name(string.Empty),
            new Description(string.Empty),
            TradingPlatform.XQ);

        _botRepository
            .GetByIdAsync(
                Arg.Any<BotId>(),
                Arg.Any<CancellationToken>())
            .Returns(Maybe<Bot>.WithValue(bot));

        // Act
        var result = await _handler.Handle(Query, default);

        // Assert
        await _botRepository.Received(1)
            .GetByIdAsync(
                Arg.Any<BotId>(),
                Arg.Any<CancellationToken>());

        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(Query.BotId);
    }
}