using System.Data;

using FluentAssertions;

using NSubstitute;

using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Features.Users;
using RichillCapital.Core.Features.Users.List;
using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

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

    public async Task When_QueryIsValid_Should_ReturnSuccess()
    {
        _userRepository
            .ListAsync(Arg.Any<CancellationToken>())
            .Returns(Arg.Any<List<User>>());

        // Act
        var result = await _handler.Handle(Query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<IEnumerable<UserDto>>();
    }
}