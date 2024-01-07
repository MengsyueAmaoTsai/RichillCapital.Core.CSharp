using RichillCapital.Core.Domain.Enumerations;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Features.Signals.Emit;

internal sealed class EmitSignalCommandHandler :
    ICommandHandler<EmitSignalCommand, Result>
{
    private readonly IRepository<Signal> _signalRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EmitSignalCommandHandler(
        IRepository<Signal> signalRepository,
        IUnitOfWork unitOfWork)
    {
        _signalRepository = signalRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(EmitSignalCommand command, CancellationToken cancellationToken)
    {
        var symbol = new Symbol(command.Symbol);
        var tradeTypeResult = TradeType.FromName(command.TradeType, true);

        if (!tradeTypeResult.IsSuccess)
        {
            return tradeTypeResult.Error;
        }

        var signal = Signal.Emit(
            command.Time,
            symbol,
            tradeTypeResult.Value,
            command.Quantity,
            command.Price);

        _signalRepository.Add(signal);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}