using System.Collections.ObjectModel;

using FluentAssertions;

using NSubstitute;

using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.Enumerations;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.Features.Bots;
using RichillCapital.Core.Features.Bots.GetById;
using RichillCapital.Core.Features.Bots.List;
using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.UnitTests.Features.Bots;

[TestClass]
public sealed class ListBotsQueryTests
{
    private static readonly ListBotsQuery Query = new();

    private readonly ListBotsQueryHandler _handler;
    private readonly IReadonlyRepository<Bot> _botRepository =
        Substitute.For<IReadonlyRepository<Bot>>();

    public ListBotsQueryTests() => _handler = new(_botRepository);

    [TestMethod]
    public async Task When_QueryIsValid_Should_ReturnSuccess()
    {
        // Arrange
        var users = new List<Bot>()
        {
            Bot.Create(new BotId(string.Empty), new Name(string.Empty), new Description(string.Empty), TradingPlatform.XQ),
            Bot.Create(new BotId(string.Empty), new Name(string.Empty), new Description(string.Empty), TradingPlatform.XQ),
        };

        _botRepository
            .ListAsync(Arg.Any<CancellationToken>())
            .Returns(users);

        // Act
        var result = await _handler.Handle(Query, default);

        // Assert
        await _botRepository
            .Received(1)
            .ListAsync(Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<ReadOnlyCollection<BotDto>>();
        result.Value.Should().HaveCount(2);
    }
}