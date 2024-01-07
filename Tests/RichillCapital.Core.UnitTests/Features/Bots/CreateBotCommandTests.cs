using System.Linq.Expressions;

using FluentAssertions;

using NSubstitute;

using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.Features.Bots.Create;
using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.UnitTests.Features.Bots;

[TestClass]
public sealed class CreateBotCommandTests
{
    private static readonly CreateBotCommand Command =
        new(string.Empty, string.Empty, string.Empty, "XQ");

    private readonly CreateBotCommandHandler _handler;
    private readonly IRepository<Bot> _botRepository = Substitute.For<IRepository<Bot>>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    public CreateBotCommandTests()
    {
        _handler = new(_botRepository, _unitOfWork);
    }

    [TestMethod]
    public async Task When_BotIdIsNotUnique_Should_ReturnFailure()
    {
        // Arrange
        _botRepository
            .AnyAsync(
                Arg.Any<Expression<Func<Bot, bool>>>(),
                Arg.Any<CancellationToken>())
            .Returns(true);

        // Act
        var result = await _handler.Handle(Command, default);

        // Assert
        await _botRepository.Received(1).AnyAsync(
            Arg.Any<Expression<Func<Bot, bool>>>(),
            Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeFalse();
        result.Error.Type.Should().Be(ErrorType.Conflict);
    }

    [TestMethod]
    public async Task When_BotIdIsUnique_Should_ReturnSuccess()
    {
        // Arrange
        _botRepository
            .AnyAsync(
                Arg.Any<Expression<Func<Bot, bool>>>(),
                Arg.Any<CancellationToken>())
            .Returns(false);

        // Act
        var result = await _handler.Handle(Command, default);

        // Assert
        await _botRepository.Received(1).AnyAsync(
            Arg.Any<Expression<Func<Bot, bool>>>(),
            Arg.Any<CancellationToken>());

        _botRepository.Received(1).Add(Arg.Any<Bot>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<BotId>();
    }
}