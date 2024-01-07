using System.Linq.Expressions;

using FluentAssertions;

using NSubstitute;

using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.Features.Users.CreateSimulatedAccount;
using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.UnitTests.Features.Users;

[TestClass]
public sealed class CreateSimulatedAccountTests
{
    private static readonly CreateSimulatedAccountCommand Command =
        new("UserId", "AccountName", "TWD", 3_000_000);

    private readonly CreateSimulatedAccountCommandHandler _handler;
    private readonly IReadonlyRepository<User> _userRepository = Substitute.For<IReadonlyRepository<User>>();
    private readonly IRepository<Account> _accountRepository = Substitute.For<IRepository<Account>>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    public CreateSimulatedAccountTests()
    {
        _handler = new(_userRepository, _accountRepository, _unitOfWork);
    }

    [TestMethod]
    public async Task When_UserNotFound_Should_ReturnFailure()
    {
        // Arrange
        _userRepository
            .AnyAsync(
                Arg.Any<Expression<Func<User, bool>>>(),
                Arg.Any<CancellationToken>())
            .Returns(false);

        // Act
        var result = await _handler.Handle(Command, default);

        // Assert
        await _userRepository
            .Received(1)
            .AnyAsync(
                Arg.Any<Expression<Func<User, bool>>>(),
                Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeFalse();
        result.Error.Type.Should().Be(ErrorType.NotFound);
    }

    [TestMethod]
    public async Task When_AccountNameIsNotUnique_Should_ReturnFailure()
    {
        // Arrange
        _userRepository
            .AnyAsync(
                Arg.Any<Expression<Func<User, bool>>>(),
                Arg.Any<CancellationToken>())
            .Returns(true);

        _accountRepository
            .AnyAsync(
                Arg.Any<Expression<Func<Account, bool>>>(),
                Arg.Any<CancellationToken>())
            .Returns(true);

        // Act
        var result = await _handler.Handle(Command, default);

        // Assert
        await _userRepository
            .Received(1)
            .AnyAsync(
                Arg.Any<Expression<Func<User, bool>>>(),
                Arg.Any<CancellationToken>());

        await _accountRepository
            .Received(1)
            .AnyAsync(
                Arg.Any<Expression<Func<Account, bool>>>(),
                Arg.Any<CancellationToken>());

        result.IsSuccess.Should().BeFalse();
        result.Error.Type.Should().Be(ErrorType.Conflict);
    }

    [TestMethod]
    public async Task When_AccountNameIsUnique_Should_ReturnSuccess()
    {
        // Arrange
        _userRepository
            .AnyAsync(
                Arg.Any<Expression<Func<User, bool>>>(),
                Arg.Any<CancellationToken>())
            .Returns(true);

        _accountRepository
            .AnyAsync(
                Arg.Any<Expression<Func<Account, bool>>>(),
                Arg.Any<CancellationToken>())
            .Returns(false);

        var result = await _handler.Handle(Command, default);

        // Assert
        await _userRepository
            .Received(1)
            .AnyAsync(
                Arg.Any<Expression<Func<User, bool>>>(),
                Arg.Any<CancellationToken>());

        await _accountRepository
            .Received(1)
            .AnyAsync(
                Arg.Any<Expression<Func<Account, bool>>>(),
                Arg.Any<CancellationToken>());

        _accountRepository.Received(1).Add(Arg.Any<Account>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());

        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.Null);
        result.Value.Should().BeOfType<AccountId>();
        result.Value.Value.Should().StartWith(AccountId.SimulatedPrefix);
    }
}