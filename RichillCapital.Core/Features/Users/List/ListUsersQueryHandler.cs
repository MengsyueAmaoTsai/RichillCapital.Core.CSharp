using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Features.Users.List;

internal sealed class ListUsersQueryHandler :
    IQueryHandler<ListUsersQuery, Result<IEnumerable<UserDto>>>
{
    private readonly IReadonlyRepository<User> _userRepository;

    public ListUsersQueryHandler(IReadonlyRepository<User> userRepository) =>
        _userRepository = userRepository;

    public async Task<Result<IEnumerable<UserDto>>> Handle(ListUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _userRepository.ListAsync(cancellationToken);

        return users
            .Select(user => new UserDto(
                user.Id.Value,
                user.Email.Value,
                user.Name.Value))
            .ToList()
            .AsReadOnly();
    }
}