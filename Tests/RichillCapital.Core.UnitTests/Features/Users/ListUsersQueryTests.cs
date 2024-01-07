using System.Collections.ObjectModel;

using FluentAssertions;

using NSubstitute;

using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.Features.Users;
using RichillCapital.Core.Features.Users.List;
using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.UnitTests.Features.Users;

[TestClass]
public sealed class ListUsersQueryTests
{
    private static readonly ListUsersQuery Query = new();
    private readonly ListUsersQueryHandler _handler;
    private readonly IReadonlyRepository<User> _userRepository =
        Substitute.For<IReadonlyRepository<User>>();

    public ListUsersQueryTests()
    {
        _handler = new(_userRepository);
    }

    [TestMethod]
    public async Task When_QueryIsValid_Should_ReturnSuccess()
    {
        // Arrange
        var users = new List<User>()
        {
            User.Create(new UserId(string.Empty), new Email(string.Empty), new Name(string.Empty)),
            User.Create(new UserId(string.Empty), new Email(string.Empty), new Name(string.Empty)),
        };

        _userRepository
            .ListAsync(Arg.Any<CancellationToken>())
            .Returns(users);

        // Act
        var result = await _handler.Handle(Query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<ReadOnlyCollection<UserDto>>();
        result.Value.Should().HaveCount(2);
    }
}