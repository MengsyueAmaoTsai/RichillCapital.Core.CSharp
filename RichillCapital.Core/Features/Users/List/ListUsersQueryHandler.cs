using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Features.Users.List;

internal sealed class ListUsersQueryHandler :
    IQueryHandler<ListUsersQuery, Result<IEnumerable<UserDto>>>
{
    public ListUsersQueryHandler()
    {
    }

    public Task<Result<IEnumerable<UserDto>>> Handle(ListUsersQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}