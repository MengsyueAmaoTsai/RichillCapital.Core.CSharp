using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.Enumerations;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Features.Users.CreateSimulatedAccount;

internal sealed class CreateSimulatedAccountCommandHandler :
    ICommandHandler<CreateSimulatedAccountCommand, Result<AccountId>>
{
    private readonly IReadonlyRepository<User> _userRepository;
    private readonly IRepository<Account> _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSimulatedAccountCommandHandler(
        IReadonlyRepository<User> userRepository,
        IRepository<Account> accountRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AccountId>> Handle(CreateSimulatedAccountCommand command, CancellationToken cancellationToken)
    {
        var userId = UserId.From(command.UserId);
        var name = new Name(command.AccountName);
        var currencyResult = Currency.FromName(command.Currency, true);

        if (!currencyResult.IsSuccess)
        {
            return currencyResult.Error;
        }

        if (!await _userRepository.AnyAsync(user => user.Id == userId, cancellationToken))
        {
            return Error.NotFound("User not found.");
        }

        if (await _accountRepository.AnyAsync(account => account.Name == name, cancellationToken))
        {
            return Error.Conflict("Duplicated account name.");
        }

        var account = Account.Create(
            AccountId.NewSimulatedId(),
            new Name(command.AccountName),
            currencyResult.Value,
            command.InitialBalance);

        _accountRepository.Add(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return account.Id;
    }
}