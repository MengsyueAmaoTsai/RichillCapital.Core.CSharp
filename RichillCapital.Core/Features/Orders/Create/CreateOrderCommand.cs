using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Features.Orders.Create;

public sealed record CreateOrderCommand(
    string AccountId,
    string Symbol,
    string TradeType,
    string OrderType,
    decimal Quantity,
    string TimeInForce,
    decimal Price) :
    ICommand<Result<OrderId>>;