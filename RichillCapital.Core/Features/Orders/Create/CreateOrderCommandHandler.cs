using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Features.Orders.Create;

internal sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Result<OrderId>>
{
    private readonly IRepository<Account> _accountRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        IRepository<Order> orderRepository,
        IRepository<Account> accountRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<OrderId>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = Order.Create(AccountId.From(command.AccountId));

        _orderRepository.Add(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}