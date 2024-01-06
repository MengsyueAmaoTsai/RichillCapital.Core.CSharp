using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Features.Users.CreateSimulatedAccount;

public sealed record CreateSimulatedAccountCommand(
    string UserId,
    string AccountName,
    string Currency,
    int InitialBalance) :
    ICommand<Result<AccountId>>;