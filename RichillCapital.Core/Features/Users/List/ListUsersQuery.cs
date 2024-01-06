using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Features.Users.List;

public sealed record ListUsersQuery() :
    IQuery<Result<IEnumerable<UserDto>>>;